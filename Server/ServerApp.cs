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
using PADIbookCommonLib;

namespace Server
{
    [Serializable]
    public static class ServerApp
    {

        public static User _user;

        //null no primario
        public static string _primaryURI;

        //public static string _replicaOneURI;

        //public static string _replicaTwoURI;

        public static string _clientUri;

        public static string _myUri;

        public static ServerForm _form;

        public static string _serverPort;

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
            TcpChannel channel;
            string myUri;
            //ThreadStart leaderThreadStart = new ThreadStart(leaderCheckReplicas);
            /*ThreadStart replicaThreadStart = new ThreadStart(replicaCheckLeader);
            ThreadStart freezeThreadStart = new ThreadStart(check_freeze_time);*/

            //_leaderThread = new Thread(leaderThreadStart);
            /*_replicaThread = new Thread(replicaThreadStart);
            _freezeThread = new Thread(freezeThreadStart);*/

            if (args.Length == 0 || args.Length > 2)
            {
                System.Windows.Forms.MessageBox.Show("Invalid server invocation");
                return;
            }

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

            /*RemotingConfiguration.RegisterWellKnownServiceType(typeof(ReplicationServicesObject),
                    ServicesNames.ReplicationServicesName,
                    WellKnownObjectMode.Singleton);*/

            myUri = (((ChannelDataStore)channel.ChannelData).ChannelUris)[0];
            _serverPort = args[0];

            _myUri = myUri;

            if (args.Length == 2)
            {
                //_isLeader = false;
                System.Windows.Forms.MessageBox.Show("Invalid server invocation");
                return;
                /*_primaryURI = args[1];

                _replicaOneURI = myUri;
                _form = new ServerForm(myUri, myUri);
                _replicaThread.Start();*/
            }
            else
            {
                //_isLeader = true;
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
                    //_leaderThread.Start();
                }
                catch (FileNotFoundException) { _user = new User(); _user.SpoofAdress = _myUri; }

                _primaryURI = myUri;
                _form = new ServerForm(myUri);
            }
            /*if (_user.Friends.Count == 0)
            {
                //MessageBox.Show(_user.Username + " : Nao tenho amigos snif snif...vou criar o anel geral");
                _user.IsRegisteredInAnelGeral = true;
            }*/
            Application.Run(_form);
        }

        /*private static void leaderCheckReplicas()
        {
            Boolean anyAlive;
            ReplicationServices replica;
            RemoteAsynchRemoveOtherReplicaDelegate del;

            while (true)
            {

                Thread.Sleep(Times.ReplicaIsAlive);

                //Verifica que ainda há pelo menos uma replica
                if (_serviceAvailable)
                {

                    anyAlive = false;

                    if (_replicaOneURI != null)
                    {
                        try
                        {
                            replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices),
                                _replicaOneURI + "/" + ServicesNames.ReplicationServicesName));

                            replica.isAlive();
                            //System.Windows.Forms.MessageBox.Show("Replica 1: i'm alive");

                            anyAlive = true;

                        }
                        catch (Exception)
                        {
                            _replicaOneURI = null;
                            if (_replicaTwoURI != null)
                            {
                                replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices),
                                   _replicaTwoURI + "/" + ServicesNames.ReplicationServicesName));

                                del = new RemoteAsynchRemoveOtherReplicaDelegate(replica.removeOtherReplica);
                                del.BeginInvoke(null, null);
                            }
                            _replicaOneURI = _replicaTwoURI;
                            _replicaTwoURI = null;
                            //System.Windows.Forms.MessageBox.Show("A replica 1 morreu!");
                        }
                    }

                    if (_replicaTwoURI != null)
                    {
                        try
                        {
                            replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices),
                                _replicaTwoURI + "/" + ServicesNames.ReplicationServicesName));

                            replica.isAlive();
                            //stem.Windows.Forms.MessageBox.Show("Replica 2: i'm alive");

                            anyAlive = true;

                        }
                        catch (Exception)
                        {
                            _replicaTwoURI = null;
                            if (anyAlive)
                            {
                                replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices),
                                       _replicaOneURI + "/" + ServicesNames.ReplicationServicesName));

                                del = new RemoteAsynchRemoveOtherReplicaDelegate(replica.removeOtherReplica);
                                del.BeginInvoke(null, null);
                            }
                            //System.Windows.Forms.MessageBox.Show("A replica 2 morreu!");
                        }
                    }

                    _serviceAvailable = anyAlive;

                    //System.Windows.Forms.MessageBox.Show("O servico " + (_serviceAvailable ? "" : "nao ") + "esta disponivel");
                }
            }
        }

        private static void replicaCheckLeader()
        {

            ReplicationServices replica, primary;

            string oldPrimary;

            while (true)
            {

                Thread.Sleep(Times.LeaderIsAlive);

                if (!_serviceAvailable)
                {
                    //System.Windows.Forms.MessageBox.Show("O primario morreu!");

                    primary = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices),
                                       _primaryURI + "/" + ServicesNames.ReplicationServicesName));
                    try
                    {
                        primary.isAlive();
                        _serviceAvailable = false;
                        continue;
                    }
                    catch (Exception) { }

                    if (_replicaTwoURI == null)
                    {
                        oldPrimary = _primaryURI;
                        _primaryURI = _myUri;
                        becomePrimary(oldPrimary);
                        return;
                    }
                    else
                    {
                        replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices),
                                       _replicaTwoURI + "/" + ServicesNames.ReplicationServicesName));

                        //System.Windows.Forms.MessageBox.Show("meu: " + _replicaOneURI);
                        oldPrimary = _primaryURI;
                        _primaryURI = replica.imTheLeader(_replicaOneURI);
                        //System.Windows.Forms.MessageBox.Show("O primario agora é " + _primaryURI);


                        if (_primaryURI.CompareTo(_replicaOneURI) == 0)
                        {
                            //System.Windows.Forms.MessageBox.Show(_primaryURI + " = " + _replicaOneURI);
                            becomePrimary(oldPrimary);
                            return;
                        }
                        else
                        {
                            _replicaTwoURI = null;
                            //Thread.Sleep(5000);
                            //System.Windows.Forms.MessageBox.Show("Agora o primario e o outro");
                        }
                    }

                }
                _serviceAvailable = false;
            }
        }

        private static void becomePrimary(string oldPrimary)
        {
            ClientServices client;
            ServerToServerServices friendServer;

            RemoteAsyncUriDelegate clientDelegate;
            RemoteAsyncChangeFriendUriDelegate friendDelegate;

            string friendUri;

            //sou o novo leader
            UpdateTitleDelegate del = new UpdateTitleDelegate(_form.updateTitle);
            _replicaOneURI = _replicaTwoURI;
            _replicaTwoURI = null;
            _serviceAvailable = true;
            del.Invoke(_primaryURI + " (primario)");
            _leaderThread.Start();

            if (_clientUri != null)
            {
                client = ((ClientServices)Activator.GetObject(typeof(ClientServices),
                                       _clientUri + "/" + ServicesNames.ClientServicesName));

                clientDelegate = new RemoteAsyncUriDelegate(client.changeLeader);
                clientDelegate.BeginInvoke(_myUri, null, null);
            }

            foreach (Friend friend in _user.Friends)
            {
                friendUri = friend.Uris.ElementAt(0);

                friendServer = (ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                                             friendUri + "/" + ServicesNames.ServerToServerServicesName);

                friendDelegate = new RemoteAsyncChangeFriendUriDelegate(friendServer.changeFriendUri);

                friendDelegate.BeginInvoke(oldPrimary, _myUri, null, null);
            }

            return;
        }

        private static void check_freeze_time() {

            if (_freeze) {
                Thread.Sleep(_freeze_period);
                _freeze = false;
            }
        
        
        }
         * */

    }
}
