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
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using PADIbookCommonLib;
using System.Runtime.Remoting.Messaging;

namespace Client
{

    public delegate void UpdateTextBoxesDelegate();
    public delegate void UpdateLookupNameDelegate(String name, String uri, List<RedirectionFile> redList);

    public partial class ClientForm : Form
    {

        private ClientToServerServices _server;

        private List<Post> _sortedRefreshView;

        public ClientToServerServices Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public ClientForm()
        {
            InitializeComponent();
            _sortedRefreshView = new List<Post>();
            this.MenuFriends.Enabled = false;
            this.MenuLookup.Enabled = false;
            this.MenuMural.Enabled = false;
            this.MenuMyMural.Enabled = false;
            this.MenuSettings.Enabled = false;
            //this.settingsAddress.Text += ClientApp._myUri;
            startClient(58510, "tcp://169.254.28.0:58511");
        }

        public ClientForm(int port, string serverUri)
        {
            InitializeComponent();
            _sortedRefreshView = new List<Post>();
            startClient(port, serverUri);
        }

        public void startClient(int myPort, string serverUri)
        {
            this.connectButton.Visible = false;
            this.connectServerPortText.Visible = false;
            this.connectMyPortText.Visible = false;
            this.connectLabel1.Visible = false;
            this.connectLabel2.Visible = false;

            ClientApp._serverUri = serverUri;

            TcpChannel channel = new TcpChannel(myPort);
            ChannelServices.RegisterChannel(channel, true);

            //NOVO : esta a servir ClientServicesObject
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(ClientServicesObject),
                    ServicesNames.ClientServicesName,
                    WellKnownObjectMode.Singleton);

            _server = (ClientToServerServices)Activator.GetObject(
                    typeof(ClientToServerServices),
                    ClientApp._serverUri + "/" + ServicesNames.ClientToServerServicesName);

            ClientApp._myUri = (((ChannelDataStore)channel.ChannelData).ChannelUris)[0];

            
            ClientApp._connected = _server.registerClient(ClientApp._myUri);

            if (!ClientApp._connected)
                System.Windows.Forms.MessageBox.Show(Messages.ServiceNotAvailable);

            clearForm();
            refreshUserFromServer();
            refreshMyMural();
            refreshMyPendingFriends();

            this.settingsAddress.Text += ClientApp._myUri;

            this.MenuFriends.Enabled = true;
            this.MenuLookup.Enabled = true;
            this.MenuMural.Enabled = true;
            this.MenuMyMural.Enabled = true;
            this.MenuSettings.Enabled = true;

            this.titulo.Text = "My Messages";
            this.richTextBox1.Height = 228;
            this.richTextBox1.Visible = true;
            this.myMuralSend.Visible = true;
            this.mymuralClear.Visible = true;
            this.myMuralText.Visible = true;

            this.Text = ClientApp._user.Username + " @ " + ClientApp._myUri;
        }

        public void showViewMural()
        {
            this.richTextBox1.Clear();
            foreach (Post p in _sortedRefreshView)
            {
                this.richTextBox1.Text += p.TimeStamp + " <" + p.Autor + "> " + p.Text + "\r\n";
            }
        }

        public void refreshViewMural(List<Post> listaposts)
        {
            sortAndAddPostList(listaposts);
            this.richTextBox1.Clear();
            foreach (Post p in _sortedRefreshView)
            {
                this.richTextBox1.Text += p.TimeStamp + " <" + p.Autor + "> " + p.Text + "\r\n";
            }
        }

        public void refreshLookupName(String name, String uri, List<RedirectionFile> redList)
        {
            foreach(RedirectionFile red in redList)//XPTO
                this.lookupResultTextBox.Text += red.FileNameHash + " @ "+ red.Uri + ")\r\n";
        }

          public void sortAndAddPostList(List<Post> lista)
        {
            Dictionary<DateTime, Post> sorted = new Dictionary<DateTime, Post>();
            List<DateTime> dlist = new List<DateTime>();

            foreach (Post p in lista)
            {
                if (!sorted.ContainsKey(p.TimeStamp))
                {
                    sorted.Add(p.TimeStamp, p);
                }
            }
            foreach (Post p in ClientApp._user.UserPosts)
            {
                if (!sorted.ContainsKey(p.TimeStamp))
                {
                    sorted.Add(p.TimeStamp, p);
                }
            }
            foreach (Post p in _sortedRefreshView)
            {
                if (!sorted.ContainsKey(p.TimeStamp))
                {
                    sorted.Add(p.TimeStamp, p);
                }
            }

            dlist = sorted.Keys.ToList();
            dlist.Sort();
            _sortedRefreshView.Clear();
            foreach (DateTime d in dlist)
            {
                _sortedRefreshView.Add(sorted[d]);
            }
        }

        private void getUserPosts()
        {
            //ClientApp._user.UserPosts = _server.getAllUserPosts();
        }

        private void refreshUserFromServer()
        {
            User user = _server.returnUserInstance();

            if(user == null) {
                System.Windows.Forms.MessageBox.Show(Messages.ServiceNotAvailable);
                return;
            }

            ClientApp._user = user;
            refreshUserFromClient();
        }

        public void refreshUserFromClient()
        {
            //int y = 0;
            this.settingsName.Text = ClientApp._user.Username;
            this.settingsAddress.Text = ClientApp._user.SpoofAdress;
            //this.settingsSex.Text = ClientApp._user.Gender;
            //this.settingsBirthDateTime.Value = ClientApp._user.BirthDate;

            /*foreach (int i in this.settingsInterests.CheckedIndices)
            {
                this.settingsInterests.SetItemCheckState(i, CheckState.Unchecked);

            }
            foreach (Interest i in ClientApp._user.Interests)
            {
                y = this.settingsInterests.Items.IndexOf(i.ToString());
                this.settingsInterests.SetItemCheckState(y, CheckState.Checked);
            }*/

            this.friendsFriendsBox.Items.Clear();
            foreach (Friend i in ClientApp._user.Friends)
            {
                this.friendsFriendsBox.Items.Add(i.Name, CheckState.Unchecked);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            refreshMyMural();
            this.titulo.Text = "My Messages";
            clearForm();
            this.richTextBox1.Height = 228;
            this.richTextBox1.Visible = true;
            this.myMuralSend.Visible = true;
            this.mymuralClear.Visible = true;
            this.myMuralText.Visible = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.titulo.Text = "All messages";
            this.richTextBox1.Clear();
            clearForm();
            this.richTextBox1.Height = 280;
            this.richTextBox1.Visible = true;
            this.button3.Visible = true;
            this.button5Clear.Visible = true;
            showViewMural();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //DateTime a = this.settingsBirthDateTime.Value;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            refreshUserFromClient();
            this.titulo.Text = "Settings";
            clearForm();

            //this.settingsInterests.Visible = true;
            this.settingsName.Visible = true;
            //this.settingsBirthDateTime.Visible = true;
            //this.settingsSex.Visible = true;
            this.settingsNameLabel.Visible = true;
            //this.settingsSexLabel.Visible = true;
            //this.settingsBirthLabel.Visible = true;
            //this.settingsInterestsLabel.Visible = true;
            this.buttonSettings.Visible = true;

            this.settingsAddress.Visible = true;
            this.settingsAdressLabel.Visible = true;
            //this.settingsMaliciousLabel.Visible = true;
            //this.settingsTamperingCheckBox.Visible = true;
            //this.settingsDontReplyCheckBox.Visible = true;
            
            this.settingsAddress.Clear();
            this.settingsAddress.Text += ClientApp._user.SpoofAdress;

            this.settingsObjectsRichTextBox2.Clear();
            foreach(ObjectFile file in ClientApp._user.ObjectList)
                this.settingsObjectsRichTextBox2.Text += file.FileName + " : " + file.Content + " ( size = " + file.Size + ")" + "\r\n";

            this.settingsObjectsRichTextBox2.Visible = true;
            this.settingsObjLabel.Visible = true;
            this.settingsAddObjTextBox.Visible = true;
            this.settingsAddObjButton.Visible = true;
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
            //ClientApp._user.BirthDate = this.settingsBirthDateTime.Value;
            //ClientApp._user.Gender = this.settingsSex.Text;
            //ClientApp._user.Interests.Clear();
            ClientApp._user.Username = this.settingsName.Text;
            ClientApp._user.SpoofAdress = this.settingsAddress.Text;
            //String test = "";

            this.Text = ClientApp._user.Username + " @ " + ClientApp._myUri;

            /*if (this.settingsInterests.CheckedIndices.Count > 5)
            {
                System.Windows.Forms.MessageBox.Show("Please select at most 5 interests.");
                return;
            }*/

            /*foreach (int i in this.settingsInterests.CheckedIndices)
            {
                ClientApp._user.Interests.Add((Interest)i);
            }
            foreach (Interest i in ClientApp._user.Interests)
            {
                test += i + " ";
            }*/

           _server.modifyUserProfile(ClientApp._user.Username,ClientApp._user.SpoofAdress);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.titulo.Text = "Lookup";
            clearForm();
            //this.lookupAge.Visible = true;
            //this.lookupInterest.Visible = true;
            this.lookupName.Visible = true;
            //this.lookupSex.Visible = true;

            //this.lookupInterestLabel.Visible = true;
            this.lookupNameLabel.Visible = true;
            //this.lookupSexAgeLabel.Visible = true;
            //this.lookupInterestButton.Visible = true;
            this.lookupNameButton.Visible = true;
            //this.lookupSexAgeButton.Visible = true;
            this.lookupResultTextBox.Visible = true;
        }

        private void myMuralSend_Click(object sender, EventArgs e)
        {
            Post post = new Post(myMuralText.Text, ClientApp._user.Username);

            AsyncCallback RemoteCallback = new AsyncCallback(remoteAsyncSendPostCallBack);
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

        public void refreshMyPendingFriends()
        {
            friendsPendingBox.Items.Clear();
            foreach (Friend friend in ClientApp._user.PendingFriends)
                friendsPendingBox.Items.Add(friend.Name, CheckState.Unchecked);
        }

        public void refreshMyFriends()
        {
            //MessageBox.Show(ClientApp._user.Username + " : form a refrescar");
            friendsFriendsBox.Items.Clear();
            foreach (Friend friend in ClientApp._user.Friends)
                friendsFriendsBox.Items.Add(friend.Name, CheckState.Unchecked);
        }

        public void remoteAsyncSendPostCallBack(IAsyncResult ar)
        {
            try
            {
                RemoteAsyncPostDelegate del = (RemoteAsyncPostDelegate)((AsyncResult)ar).AsyncDelegate;

                Post newPost = del.EndInvoke(ar);

                if (newPost == null)
                {
                    System.Windows.Forms.MessageBox.Show(Messages.ServiceNotAvailable);
                    return;
                }

                ClientApp._user.addPost(newPost);

                UpdateTextBoxesDelegate updateDelegate = new UpdateTextBoxesDelegate(refreshMyMural);
                ClientApp._form.BeginInvoke(updateDelegate);

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show(e.Message); }
            return;
        }

        public void clearForm()
        {
            this.lookupResultTextBox.Visible = false;
            this.richTextBox1.Visible = false;

            this.buttonSettings.Visible = false;
            this.button3.Visible = false;
            this.button5Clear.Visible = false;

            this.myMuralSend.Visible = false;
            this.mymuralClear.Visible = false;
            this.myMuralText.Visible = false;

            //this.settingsInterests.Visible = false;
            this.settingsName.Visible = false;
            //this.settingsBirthDateTime.Visible = false;
            //this.settingsSex.Visible = false;
            this.settingsNameLabel.Visible = false;
            //this.settingsSexLabel.Visible = false;
            //this.settingsBirthLabel.Visible = false;
            //this.settingsInterestsLabel.Visible = false;
            this.settingsAdressLabel.Visible = false;
            this.settingsAddress.Visible = false;
            //this.settingsMaliciousLabel.Visible = false;
            //this.settingsTamperingCheckBox.Visible = false;
            //this.settingsDontReplyCheckBox.Visible = false;
            this.settingsObjectsRichTextBox2.Visible = false;
            this.settingsObjLabel.Visible = false;
            this.settingsAddObjTextBox.Visible = false;
            this.settingsAddObjButton.Visible = false;

            //this.lookupAge.Visible = false;
            //this.lookupInterest.Visible = false;
            this.lookupName.Visible = false;
            this.lookupSex.Visible = false;

            //this.lookupInterestLabel.Visible = false;
            this.lookupNameLabel.Visible = false;
            //this.lookupSexAgeLabel.Visible = false;
            //this.lookupInterestButton.Visible = false;
            this.lookupNameButton.Visible = false;
            //this.lookupSexAgeButton.Visible = false;

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
            this.titulo.Text = "Nodes";
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

        //TODO: por assincrono (n consta para avaliacao) e actualizar lista amigos no server
        private void friendsFriendsRemoveButton_Click(object sender, EventArgs e)
        {
            /*
             * foreach (String i in this.friendsFriendsBox.CheckedItems)
            {
                ClientApp._user.removeFriend(i);
            }
            _server.modifyUserFriends(ClientApp._user.Friends);
            refreshUserFromClient();
            */
        }

        private void friendsAddUriButton_Click(object sender, EventArgs e)
        {
            try
            {
                string friendUri = friendsAddUriText.Text;
                //new Uri(friendUri);

                RemoteAsyncUriDelegate del = new RemoteAsyncUriDelegate(_server.sendFriendRequest);
                del.BeginInvoke(friendUri, null, null);
            }
            catch (UriFormatException)
            {
                System.Windows.Forms.MessageBox.Show("Invalid uri format");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshViewMural(new List<Post>());
            RemoteAsyncGetFriendPostsDelegate del = new RemoteAsyncGetFriendPostsDelegate(_server.getFriendsPosts);
            del.BeginInvoke(null, null);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            startClient(Int32.Parse(this.connectMyPortText.Text), "tcp://localhost:" + this.connectServerPortText.Text);

        }

        private void friendsPendingAcceptButton_Click(object sender, EventArgs e)
        {
            RemoteAsyncFriendDelegate del;

            foreach (object checkedItem in friendsPendingBox.CheckedItems)
            {
                string acceptedFriendName = friendsPendingBox.GetItemText(checkedItem);

                Friend acceptedFriend = ClientApp._user.getPendingFriend(acceptedFriendName);

                //MessageBox.Show(acceptedFriend.Name);

                del = new RemoteAsyncFriendDelegate(_server.acceptFriendRequest);

                del.BeginInvoke(acceptedFriend, null, null);
            }
        }

        private void friendsPendingRejectButton_Click(object sender, EventArgs e)
        {
            RemoteAsyncFriendDelegate del;

            foreach (object checkedItem in friendsPendingBox.CheckedItems)
            {
                string removeFriendName = friendsPendingBox.GetItemText(checkedItem);

                Friend removeFriend = ClientApp._user.getPendingFriend(removeFriendName);

                del = new RemoteAsyncFriendDelegate(_server.removeFriendRequest);

                del.BeginInvoke(removeFriend, RemoteAsyncRemovePendingFriendCallBack, null);

            }
        }

        public void RemoteAsyncRemovePendingFriendCallBack(IAsyncResult ar)
        {
            try
            {
                RemoteAsyncFriendDelegate del = (RemoteAsyncFriendDelegate)((AsyncResult)ar).AsyncDelegate;

                Friend friend = del.EndInvoke(ar);

                if (friend == null)
                {
                    System.Windows.Forms.MessageBox.Show(Messages.ServiceNotAvailable);
                    return;
                }

                ClientApp._user.removePendingFriend(friend.Name);

                UpdateTextBoxesDelegate updateDelegate = new UpdateTextBoxesDelegate(refreshMyPendingFriends);
                ClientApp._form.BeginInvoke(updateDelegate);

            }
            catch (Exception e) { System.Windows.Forms.MessageBox.Show(e.Message + "aqui"); }
            return;
        }

        private void lookupNameButton_Click_1(object sender, EventArgs e)
        {
            this.lookupResultTextBox.Clear();
            QueryByName q = new QueryByName(this.lookupName.Text, new List<String>(), new List<String>(),DateTime.Now);
            RemoteAsyncLookupNameDelegate del = new RemoteAsyncLookupNameDelegate(_server.lookupname);
            del.BeginInvoke(q, null, null);/*TODO CALLBACK*/
        }

        /*private void lookupInterestButton_Click(object sender, EventArgs e)
        {
            this.lookupResultTextBox.Clear();
            QueryByInterest q = new QueryByInterest(ClientApp._user.Username,this.lookupInterest.Text, new List<String>(), new List<String>());
            RemoteAsyncLookupInterestDelegate del = new RemoteAsyncLookupInterestDelegate(_server.lookupInterest);
            del.BeginInvoke(q, null, null);/*TODO CALLBACK
            
        }*/

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /*private void lookupSexAgeButton_Click(object sender, EventArgs e)
        {
            this.lookupResultTextBox.Clear();
            QueryByGenderAge q = new QueryByGenderAge(this.lookupSex.Text, (int)this.lookupAge.Value, new List<String>(), new List<String>());
            RemoteAsyncLookupSexAgeDelegate del = new RemoteAsyncLookupSexAgeDelegate(_server.lookupSexAge);
            del.BeginInvoke(q, null, null);/*TODO CALLBACK
        }*/

        private void ClientForm_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void settingsAddObjButton_Click(object sender, EventArgs e)
        {
            ObjectFile file = new ObjectFile(this.settingsAddObjTextBox.Text, "content", 7);
            RemoteAsyncObjectDelegate RemoteDel = new RemoteAsyncObjectDelegate(_server.shareObject);
            IAsyncResult RemAr = RemoteDel.BeginInvoke(file, null, null);
            ClientApp._user.addObject(file);
            this.settingsObjectsRichTextBox2.Clear();
            foreach (ObjectFile f in ClientApp._user.ObjectList)
                this.settingsObjectsRichTextBox2.Text += f.FileName + " : " + f.Content + " ( size = " + f.Size + ")" + "\r\n";
        }
    }
}
