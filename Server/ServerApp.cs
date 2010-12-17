using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
using System.Security.Cryptography;
using PADIbookCommonLib;

namespace Server
{
    [Serializable]
    public static class ServerApp
    {

        public static User _user;
        public static UserEntry _myUserEntry;

        public static RSACryptoServiceProvider _rsaProvider;
        public static NodePKIHelper _pkiCommunicator;

        private static System.Timers.Timer _freezeTimer;

        //null no primario
        public static string _primaryURI;

        //public static string _replicaOneURI;

        //public static string _replicaTwoURI;

        public static string _clientUri;

        public static string _myUri;

        public static ServerForm _form;

        public static string _serverPort;

        private static ServerToServerServicesObject _ServerToServerServiceObject;

        //public static Boolean _freeze;
        //public static int _delay;
        //public static int _freeze_period;

        //public static Boolean _isLeader;
        public static Boolean _serviceAvailable;

        public static Thread _leaderThread;
        /*public static Thread _replicaThread;
        public static Thread _freezeThread;*/


        /// <summary>
        /// The main entry point for the PADIbook ServerApp.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            

            if (args.Length == 0 || args.Length > 2)
            {
                System.Windows.Forms.MessageBox.Show("Invalid server invocation");
                return;
            }

            loadRPCServices(args);
            _serviceAvailable = true;

            try
            {
                User savedUser = new User();
                string filename = ".\\user" + _serverPort + ".txt";
                TextReader tr = new StreamReader(filename);
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(savedUser.GetType());
                savedUser = (User)x.Deserialize(tr);
                tr.Close();

                _user = savedUser;
            }
            catch (FileNotFoundException) 
            { 
                _user = new User(); 
                _user.SpoofAdress = _myUri; 
            }

            /* load or generate pub/priv key for node */
            loadNodePublicPrivateKeys();
            
            _form = new ServerForm(_myUri);

            /* PKI Communication */
            RSACryptoServiceProvider rsaPki = null;
            if ((rsaPki = readRSAPKI()) == null)
            {
                MessageBox.Show("Couldn't load pubkey of PKI.");
                Application.Exit();
            }
            _pkiCommunicator = new NodePKIHelper(_rsaProvider, rsaPki, args[1]);
            _myUserEntry = new UserEntry();
            _myUserEntry.NodeId = _user.Username;
            _myUserEntry.Address = _myUri;
            _myUserEntry.PubKey = _rsaProvider.ToXmlString(true);
            
            if (_pkiCommunicator.Register(_myUserEntry))
            {
                Application.Run(_form);
            }
            else
            {
                Application.Exit();
            }
            
        }

        private static RSACryptoServiceProvider readRSAPKI()
        {
            RSACryptoServiceProvider rsapki = new RSACryptoServiceProvider();
            // we assume all nodes get the PKI pubkey in a secure manner
            try
            {
                TextReader tr = new StreamReader(".\\pki_pub.xml");
                rsapki.FromXmlString(tr.ReadToEnd());
                tr.Close();
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            return rsapki;
        }

        private static void loadRPCServices(string[] args)
        {
            TcpChannel channel;
            channel = new TcpChannel(Int32.Parse(args[0]));
            ChannelServices.RegisterChannel(channel, true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(ClientToServerServicesObject),
                    ServicesNames.ClientToServerServicesName,
                    WellKnownObjectMode.Singleton);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(ServerToServerServicesObject),
                    ServicesNames.ServerToServerServicesName,
                    WellKnownObjectMode.Singleton);

            _myUri = (((ChannelDataStore)channel.ChannelData).ChannelUris)[0];
            _primaryURI = _myUri;
            _serverPort = args[0];


            _ServerToServerServiceObject = (ServerToServerServicesObject)Activator.GetObject(
                typeof(ServerToServerServicesObject),
                ServerApp._myUri + "/" + ServicesNames.ServerToServerServicesName);
        }

        private static void loadNodePublicPrivateKeys()
        {
            _rsaProvider = new RSACryptoServiceProvider();
            try
            {
                TextReader tr = new StreamReader(".\\user" + _serverPort + "keys.xml");
                _rsaProvider.FromXmlString(tr.ReadToEnd());
                tr.Close();
            }
            catch (FileNotFoundException)
            {
                TextWriter tw = new StreamWriter(".\\user" + _serverPort + "keys.xml");
                tw.Write(_rsaProvider.ToXmlString(true));
                tw.Close();
            }
        }

        public static RSACryptoServiceProvider GetCryptoProvider()
        {
            return _rsaProvider;
        }

        public static void FreezeService(decimal period)
        {
            int freezeTime = Int32.Parse(period.ToString())*1000;
            _freezeTimer = new System.Timers.Timer(freezeTime);
            _freezeTimer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            _freezeTimer.Start();
            _ServerToServerServiceObject.FreezeService(freezeTime);
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _freezeTimer.Stop();
            MessageBox.Show("Freeze timeout reached.");
        }
    }
}
