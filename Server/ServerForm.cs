using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using PADIbookCommonLib;
using System.Runtime.Remoting.Messaging;

namespace Server
{
    public delegate void UpdateTitleDelegate(string title);

    public partial class ServerForm : Form
    {

        public delegate User RemoteAsyncRegisterReplicaDelegate(string u);

        public ServerForm(string title)
        {
            InitializeComponent();
            this.Text = title;// +" (primário)";
            refreshTextBoxes();
            killer_init();
        }

        public ServerForm(string title, string uri)
        {
            InitializeComponent();
            this.Text = title + " (replica)";
            //initReplica(uri);
            refreshTextBoxes();
            killer_init();
        }

        public void killer_init(){
            //this.Closing += new CancelEventHandler(killerExitButton);         
        }

        private void killerExitButton(object sender, CancelEventArgs cArgs)
        {
            ServerApp._leaderThread.Abort();
            /*ServerApp._replicaThread.Abort();
            ServerApp._freezeThread.Abort();*/
            this.Dispose();
        }

        //TODO: tem que ficar assincrono
        /*public void initReplica(string uri)
        {
            try
            {
                //System.Windows.Forms.MessageBox.Show("isto -> " + ServerApp._primaryURI);
                ReplicationServices primary = (ReplicationServices)Activator.GetObject(
                        typeof(ReplicationServices),
                        ServerApp._primaryURI + "/" + ServicesNames.ReplicationServicesName);

                ServerApp._user = primary.registerReplica(uri);
                ServerApp._serviceAvailable = true;
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show(e.Message); }
        }*/

        /*public void RemoteAsyncRegisterReplicaCallBack(IAsyncResult ar)
        {
            try
            {
                RemoteAsyncRegisterReplicaDelegate del = (RemoteAsyncRegisterReplicaDelegate)((AsyncResult)ar).AsyncDelegate;
                User u = del.EndInvoke(ar);
                //MessageBox.Show(u.Username);
                ServerApp._user = u;
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show(e.Message); }
            return;
        }*/

        private void saveServerState()
        {
            User user = ServerApp._user;
            string filename = ".\\user" + ServerApp._serverPort + ".txt";
            string lastfilename = ".\\user" + ServerApp._serverPort + "_backup.txt";
            MessageBox.Show("Saving to "+filename);
            //System.IO.File.Move(@".\user.txt", @".\last_user.txt");
            if (File.Exists(filename))
            {
                File.Copy(filename, lastfilename,true);
                File.Delete(filename);
            }

            TextWriter tw = new StreamWriter(filename,false);
            
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(user.GetType());
            x.Serialize(tw, user);
            tw.Close();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            refreshTextBoxes();
            saveServerState();
        }

        private void refreshTextBoxes()
        {
            try
            {
                //available.Text = ServerApp._serviceAvailable ? "available" : "not Available";
                posts.Clear();
                friends.Clear();
                settings.Clear();
                redirectionRichTextBox.Clear();
                //replicasTextBox.Clear();
                objectsRichTextBox.Clear();

                this.Text = ServerApp._user.Username + " @ " + ServerApp._myUri;

                User user = ServerApp._user;

                foreach(RedirectionFile file in user.RedirectionList)
                    redirectionRichTextBox.Text += file.FileNameHash + " @ " + file.Uri + "\r\n";
                
                foreach(ObjectFile file in user.ObjectList)
                    objectsRichTextBox.Text += file.FileName + " : " + file.Content + " ( size = " + file.Size + ")" + "\r\n";

                foreach (Post post in user.UserPosts)
                    posts.Text += post.Text + "\r\n";

                foreach (Friend friend in user.Friends)
                    friends.Text += (friend.SucessorSwarm?"(Sucessor) ":"(Predecessor) ") + friend.Name + " ( " + friend.Uris.ElementAt(0) + " )\r\n";

                settings.Text += "ID: " + user.Username + "\r\nADDR: " + user.SpoofAdress + "\r\n";// +user.Gender + "\r\n" + user.BirthDate + "\r\n";

                /*foreach (Interest interest in user.Interests)
                    settings.Text += interest.ToString() + "\r\n";
                */
                foreach (Friend friend in user.PendingFriends)
                {
                    debugRich.Text += friend.Name + "\r\n";
                    foreach (string uri in friend.Uris)
                        debugRich.Text += uri + "\r\n";
                }

                //replicasTextBox.Text += "No replicas";//ServerApp._replicaOneURI + "\r\n" + ServerApp._replicaTwoURI;
            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show(e.StackTrace); }
        }

        public void updateTitle(string title)
        {
            this.Text = title;
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*if (((int)this.delay_seconds.Value) > ((int)this.freeze_period.Value)) {
                return;
            }

            ServerApp._delay = (((int)this.delay_seconds.Value) * 1000);
            ServerApp._freeze_period = (((int)this.freeze_period.Value) * 1000);
            ServerApp._freeze = true;
            ServerApp._freezeThread.Start();
            */
            ServerApp.FreezeService(this.freeze_period.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void delay_seconds_ValueChanged(object sender, EventArgs e)
        {

        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                User savedUser = new User();
                string filename = ".\\user" + ServerApp._serverPort + ".txt";
                TextReader tr = new StreamReader(filename);
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(savedUser.GetType());
                savedUser = (User)x.Deserialize(tr);
                tr.Close();

                ServerApp._user = savedUser;
            }
            catch (FileNotFoundException) { ServerApp._user = new User(); }
            refreshTextBoxes();
        }

        private void posts_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
