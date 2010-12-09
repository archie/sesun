using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Security.Cryptography;
using System.IO;


namespace PKI
{
    public class PKIService
    {
        private const int PKI_PORT = 50000;
        private ConsoleThread _output;
        private Thread _outputThread;
        private int _beep = 0;

        private static RSACryptoServiceProvider rsa;
        private static bool rsaFirstTimeCalled = true;

        private string _pkiURI;

        public string PkiURI
        {
            get { return _pkiURI; }
            set { _pkiURI = value; }
        }

        public PKIService() 
        {
            // start console thread
            _output = new ConsoleThread();
            _outputThread = new Thread(new ThreadStart(_output.Output));
            _outputThread.Start();
            while (!_outputThread.IsAlive) ;
        }

        public void Run()
        {
            TcpChannel channel = new TcpChannel(PKI_PORT);
            ChannelServices.RegisterChannel(channel, true);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(PKIServiceNodeToObject),
                    "PKIService", WellKnownObjectMode.Singleton);
            PkiURI = (((ChannelDataStore)channel.ChannelData).ChannelUris)[0];

            _output.AddMessage("PKI Service at: " + PkiURI);

            while (true) 
            {
                if (!_outputThread.IsAlive)
                    break;

                beep();
            }
        }

        public static SignedEntry sign(UserEntry ue)
        {
            SignedEntry se = new SignedEntry();
            se.Entry = ue;

            byte[] data = Encoding.Default.GetBytes(ue.ToString());

            if (rsaFirstTimeCalled)
            {
                rsa = new RSACryptoServiceProvider();
                try
                {
                    TextReader tr = new StreamReader("pki.xml");
                    rsa.FromXmlString(tr.ReadToEnd());
                    tr.Close();
                }
                catch (FileNotFoundException)
                {
                    TextWriter tw = new StreamWriter("pki.xml");
                    tw.Write(rsa.ToXmlString(true));
                    tw.Close();

                    tw = new StreamWriter("pki_pub.xml");
                    tw.Write(rsa.ToXmlString(false));
                    tw.Close();
                }
                rsaFirstTimeCalled = false;
            }
            
            byte[] signature = rsa.SignData(data, "SHA1");
            se.Signature = signature;

            return se;
            
        }


        private void beep()
        {
            _beep++;
            if (_beep % 100000000 == 0)
            {
                Console.WriteLine(".");
                _beep = 0;
            }
        }

        static void Main(string[] args)
        {   
            // start PKI service
            Console.WriteLine("Starting PKI Service...");
            
            PKIService service = new PKIService();
            service.Run();

            Console.WriteLine("World domination. PKI Service end.");
        }
    }

    public class ConsoleThread
    {
        private List<string> inBuffer;

        public ConsoleThread()
        {
            inBuffer = new List<string>();
        }

        public void Output()
        {
            while (true)
            {
                if (inBuffer.Count > 0)
                {
                    if (inBuffer[0].Equals("exit")) 
                        break;
                    else if ( inBuffer[0].Equals("test"))                   
                        runTest();
                    

                    foreach (string s in inBuffer)
                        Console.WriteLine(s);

                    inBuffer.Clear();
                }
                AddMessage(Console.ReadLine());
            }
            Console.WriteLine("Terminating Output thread");
        }

        public void AddMessage(string s)
        {
            inBuffer.Add(s);
        }

        private void runTest()
        {
            Console.WriteLine("Initiating test");
            NodePKIHelper nt = new NodePKIHelper("tcp://192.168.174.128", 50000);
            nt.TestRegister();
            Thread.Sleep(1000);
            nt.Test();
            Thread.Sleep(1000);
            nt.TestGetKey();
            inBuffer.RemoveAt(0);
            Console.WriteLine("Test completed");
        }
    }
}
