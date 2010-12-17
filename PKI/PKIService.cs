using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
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
        private System.Timers.Timer _cleanupTimer;
        private PKIServiceNodeToObject _pkiServiceObject;
        public static RSACryptoServiceProvider _rsa;
        public static string PKIURI;

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
            PKIURI = (((ChannelDataStore)channel.ChannelData).ChannelUris)[0];

            Console.WriteLine("PKI Service at: " + PKIURI);

            _pkiServiceObject = (PKIServiceNodeToObject)Activator.GetObject(
                typeof(PKIServiceNodeToObject), PKIService.PKIURI + "/PKIService");

            _cleanupTimer = new System.Timers.Timer(10000); // perform cleanup and beep every 10s
            _cleanupTimer.Elapsed += new ElapsedEventHandler(periodicCleanupOfPendingUsers);
            _cleanupTimer.Start();

            while (true) 
            {
                if (!_outputThread.IsAlive)
                    break;
            }
        }

        private void periodicCleanupOfPendingUsers(object sender, ElapsedEventArgs events)
        {
            _pkiServiceObject.EmptyPendingUsers();
            Console.Write(".");
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
                Console.WriteLine("PKI Public+Private Key exists, loading to memory.");
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
                    else if (inBuffer[0].Equals("pending"))
                        listPendingUsers();
                    else if (inBuffer[0].Equals("key"))
                        printPKIkeys();
                    else if (inBuffer[0].Equals("clear"))
                        clearDatabase();
                    else
                    {
                        Console.WriteLine("Invalid command.");
                    }

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
            PKIServiceNodeToObject pkiService = (PKIServiceNodeToObject)Activator.GetObject(
                typeof(PKIServiceNodeToObject), PKIService.PKIURI + "/PKIService");
            Console.WriteLine("------------ List of users -------------");
            foreach (UserEntry e in pkiService.GetUserDatabase())
            {
                Console.WriteLine("> " + e.NodeId + "\t" + e.Address);
            }
            Console.WriteLine("------------- End of list --------------");
        }

        private void clearDatabase()
        {
            PKIServiceNodeToObject pkiService = (PKIServiceNodeToObject)Activator.GetObject(
                typeof(PKIServiceNodeToObject), PKIService.PKIURI + "/PKIService");
            pkiService.ClearDatabase();
            Console.WriteLine("Cleared user database.");
        }

        private void listPendingUsers()
        {
            PKIServiceNodeToObject pkiService = (PKIServiceNodeToObject)Activator.GetObject(
                typeof(PKIServiceNodeToObject), PKIService.PKIURI + "/PKIService");
            Console.WriteLine("-------- List of pending users ---------");
            foreach (UserEntry e in pkiService.GetPendingUserDatabase())
            {
                Console.WriteLine("> " + e.NodeId + "\t" + e.Address);
            }
            Console.WriteLine("------------- End of list --------------");
        }

        private void printPKIkeys()
        {
            Console.WriteLine("---------------- Key -------------------");
            Console.WriteLine(PKIService._rsa.ToXmlString(true));
            Console.WriteLine("----------------------------------------");
        }

    }
}
