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
using PADIbookCommonLib;


namespace PKI
{
    public class PKIService
    {
        private const int PKI_PORT = 50000;
        private ConsoleThread _output;
        private Thread _outputThread;
        private int _beep = 0;

        private static RSACryptoServiceProvider _rsa;

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

            Console.WriteLine("PKI Service at: " + PkiURI);

            while (true) 
            {
                if (!_outputThread.IsAlive)
                    break;

                beep();
            }
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

        public static SignedEntry sign(UserEntry ue)
        {
            SignedEntry signedEntry = new SignedEntry();
            signedEntry.Entry = ue;

            byte[] data = Encoding.Default.GetBytes(ue.ToString());
            signedEntry.Signature = _rsa.SignData(data, "SHA1");

            return signedEntry;
            
        }

        private static void loadRsaProvider()
        {
            _rsa = new RSACryptoServiceProvider();
            try
            {
                Console.WriteLine("PKI Public Key exists, loading to memory.");
                TextReader tr = new StreamReader(".\\pki.xml");
                _rsa.FromXmlString(tr.ReadToEnd());
                tr.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No Public Key for PKI exists, creating one.");
                TextWriter tw = new StreamWriter(".\\pki.xml");
                tw.Write(_rsa.ToXmlString(true));
                tw.Close();

                tw = new StreamWriter(".\\pki_pub.xml");
                tw.Write(_rsa.ToXmlString(false));
                tw.Close();
            }
        }

        static void Main(string[] args)
        {   
            // start PKI service
            Console.WriteLine("Starting PKI Service...");
            
            PKIService service = new PKIService();
            loadRsaProvider();
            
            service.Run();

            Console.WriteLine("World domination accomplished.");
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
                    else if (inBuffer[0].Equals("list"))
                        listUsers();

                    inBuffer.RemoveAt(0);

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

        private void listUsers()
        {
            Console.WriteLine("Want to list all users here... not yet sure how.");
        }

    }
}
