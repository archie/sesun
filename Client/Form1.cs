using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using PADIbookCommonLib;
using System.Runtime.Remoting.Messaging;

namespace Client
{
    public partial class Form1 : Form
    {
        private ServerServices _server;

        public delegate Post RemoteAsyncPostDelegate(Post post);

        private delegate void UpdateTextBoxesDelegate();

        public Form1()
        {
            //TODO: mudar o uri pra receber de algum sitio
            _server = (ServerServices)Activator.GetObject(
                typeof(ServerServices),
                "tcp://localhost:58510/ServerServices");
            InitializeComponent();
            clearForm();
            refreshUserFromServer();
            getUserPosts();
            refreshMyMural();
        }

        private void getUserPosts()
        {
            ClientApp._user.UserPosts = _server.getAllUserPosts();
        }

        private void refreshUserFromServer()
        {
            ClientApp._user = _server.returnUserInstance();
            refreshUserFromClient();
        }

        private void refreshUserFromClient()
        {
            int y = 0;
            this.settingsName.Text = ClientApp._user.Username;
            this.settingsSex.Text = ClientApp._user.Gender;
            this.settingsBirthDateTime.Value = ClientApp._user.BirthDate;

            foreach (int i in this.settingsInterests.CheckedIndices)
            {
                this.settingsInterests.SetItemCheckState(i, CheckState.Unchecked);

            }
            foreach (Interest i in ClientApp._user.Interests)
            {
                y = this.settingsInterests.Items.IndexOf(i.ToString());
                this.settingsInterests.SetItemCheckState(y, CheckState.Checked);
            }

            this.friendsFriendsBox.Items.Clear();
            foreach (Friend i in ClientApp._user.Friends)
            {
                this.friendsFriendsBox.Items.Add(i.Name, CheckState.Unchecked);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            refreshMyMural();
            this.titulo.Text = "My Mural";
            clearForm();
            this.richTextBox1.Height = 228;
            this.richTextBox1.Visible = true;
            this.myMuralSend.Visible = true;
            this.mymuralClear.Visible = true;
            this.myMuralText.Visible = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.titulo.Text = "Mural";
            clearForm();
            this.richTextBox1.Height = 280;
            this.richTextBox1.Visible = true;
            this.button3.Visible = true;
            this.button5Clear.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime a = this.settingsBirthDateTime.Value;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            refreshUserFromClient();
            this.titulo.Text = "Settings";
            clearForm();

            this.settingsInterests.Visible = true;
            this.settingsName.Visible = true;
            this.settingsBirthDateTime.Visible = true;
            this.settingsSex.Visible = true;
            this.settingsNameLabel.Visible = true;
            this.settingsSexLabel.Visible = true;
            this.settingsBirthLabel.Visible = true;
            this.settingsInterestsLabel.Visible = true;
            this.buttonSettings.Visible = true;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            this.myMuralText.Clear();
        }

        private void button5Clear_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            this.myMuralText.Clear();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        //Settings
        private void button2_Click(object sender, EventArgs e)
        {
            //TODO ASSINCRONIZAR e mudar a ordem em q faz update
            ClientApp._user.BirthDate = this.settingsBirthDateTime.Value;
            ClientApp._user.Gender = this.settingsSex.Text;
            ClientApp._user.Username = this.settingsName.Text;
            ClientApp._user.Interests.Clear();
            String test = "";

            if (this.settingsInterests.CheckedIndices.Count > 5)
            {
                System.Windows.Forms.MessageBox.Show("Please select at most 5 interests.");
                return;
            }

            foreach (int i in this.settingsInterests.CheckedIndices)
            {
                ClientApp._user.Interests.Add((Interest)i);
            }
            foreach (Interest i in ClientApp._user.Interests)
            {
                test += i + " ";
            }
            System.Windows.Forms.MessageBox.Show("Saving profile!\r\nName: " + ClientApp._user.Username
                                                    + "\r\nGender: " + ClientApp._user.Gender
                                                    + "\r\nBirth date: " + ClientApp._user.BirthDate.ToString()
                                                    + "\r\nInterests: " + test);
            _server.modifyUserProfile(ClientApp._user);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.titulo.Text = "Lookup";
            clearForm();

            this.richTextBox1.Height = 228;
            this.richTextBox1.Visible = true;

            this.lookupAge.Visible = true;
            this.lookupInterest.Visible = true;
            this.lookupName.Visible = true;
            this.lookupSex.Visible = true;

            this.lookupInterestLabel.Visible = true;
            this.lookupNameLabel.Visible = true;
            this.lookupSexAgeLabel.Visible = true;
            this.lookupInterestButton.Visible = true;
            this.lookupNameButton.Visible = true;
            this.lookupSexAgeButton.Visible = true;
        }

        private void myMuralSend_Click(object sender, EventArgs e)
        {
            /*   if (myMuralText.Text == "")
                   MessageBox.Show("A caixa de texto ta vazia");
               else
               {
                   Post post = new Post(myMuralText.Text);
                   XmlSerializer x = new XmlSerializer(post.GetType());
                   TextWriter myTextWriter = new StringWriter();

                   x.Serialize(myTextWriter, post);

                   MessageBox.Show("Vou adicionar um post: " + myTextWriter.ToString());

                   _server.sendPost(myTextWriter.ToString());

                   myTextWriter.Close();*/
            Post post = new Post(myMuralText.Text);

            AsyncCallback RemoteCallback = new AsyncCallback(this.RemoteAsyncSendPostCallBack);
            RemoteAsyncPostDelegate RemoteDel = new RemoteAsyncPostDelegate(_server.sendPost);
            IAsyncResult RemAr = RemoteDel.BeginInvoke(post, RemoteCallback, null);
            this.myMuralText.Clear();
        }

        public void refreshMyMural()
        {
            this.richTextBox1.Clear();

            foreach (Post p in ClientApp._user.UserPosts)
                richTextBox1.Text += (p.TimeStamp + " <" + ClientApp._user.Username + "> " + p.Text + "\n");
        }

        public void RemoteAsyncSendPostCallBack(IAsyncResult ar)
        {
            try
            {
                RemoteAsyncPostDelegate del = (RemoteAsyncPostDelegate)((AsyncResult)ar).AsyncDelegate;

                Post newPost = del.EndInvoke(ar);
                ClientApp._user.addPost(newPost);

                UpdateTextBoxesDelegate updateDelegate = new UpdateTextBoxesDelegate(refreshMyMural);
                ClientApp._form.BeginInvoke(updateDelegate);

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show(e.Message); }


            return;
        }

        public void clearForm()
        {
            this.richTextBox1.Visible = false;

            this.buttonSettings.Visible = false;
            this.button3.Visible = false;
            this.button5Clear.Visible = false;

            this.myMuralSend.Visible = false;
            this.mymuralClear.Visible = false;
            this.myMuralText.Visible = false;

            this.settingsInterests.Visible = false;
            this.settingsName.Visible = false;
            this.settingsBirthDateTime.Visible = false;
            this.settingsSex.Visible = false;
            this.settingsNameLabel.Visible = false;
            this.settingsSexLabel.Visible = false;
            this.settingsBirthLabel.Visible = false;
            this.settingsInterestsLabel.Visible = false;

            this.lookupAge.Visible = false;
            this.lookupInterest.Visible = false;
            this.lookupName.Visible = false;
            this.lookupSex.Visible = false;

            this.lookupInterestLabel.Visible = false;
            this.lookupNameLabel.Visible = false;
            this.lookupSexAgeLabel.Visible = false;
            this.lookupInterestButton.Visible = false;
            this.lookupNameButton.Visible = false;
            this.lookupSexAgeButton.Visible = false;

            this.friendsAddUriButton.Visible = false;
            this.friendsAddUriLabel.Visible = false;
            this.friendsAddUriText.Visible = false;
            this.friendsFriendsBox.Visible = false;
            this.friendsFriendsLabel.Visible = false;
            this.friendsFriendsRemoveButton.Visible = false;
            this.friendsPendingAcceptButton.Visible = false;
            this.friendsPendingBox.Visible = false;
            this.friendsPendingLabel.Visible = false;
            this.friendsPendingRejectButton.Visible = false;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.titulo.Text = "Friends";
            clearForm();
            //refreshUserFromClient();
            this.friendsAddUriButton.Visible = true;
            this.friendsAddUriLabel.Visible = true;
            this.friendsAddUriText.Visible = true;
            this.friendsFriendsBox.Visible = true;
            this.friendsFriendsLabel.Visible = true;
            this.friendsFriendsRemoveButton.Visible = true;
            this.friendsPendingAcceptButton.Visible = true;
            this.friendsPendingBox.Visible = true;
            this.friendsPendingLabel.Visible = true;
            this.friendsPendingRejectButton.Visible = true;
        }

        private void friendsFriendsRemoveButton_Click(object sender, EventArgs e)
        {
            //TODO actualizar lista amigos no server
            foreach (String i in this.friendsFriendsBox.CheckedItems)
            {
                ClientApp._user.removeFriend(i);
            }
            refreshUserFromClient();
        }
    }
}
