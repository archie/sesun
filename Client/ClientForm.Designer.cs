namespace Client
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.MenuMyMural = new System.Windows.Forms.ToolStripButton();
            this.MenuMural = new System.Windows.Forms.ToolStripButton();
            this.MenuFriends = new System.Windows.Forms.ToolStripButton();
            this.MenuLookup = new System.Windows.Forms.ToolStripButton();
            this.MenuSettings = new System.Windows.Forms.ToolStripButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.myMuralText = new System.Windows.Forms.TextBox();
            this.myMuralSend = new System.Windows.Forms.Button();
            this.settingsName = new System.Windows.Forms.TextBox();
            this.settingsNameLabel = new System.Windows.Forms.Label();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.titulo = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mymuralClear = new System.Windows.Forms.Button();
            this.button5Clear = new System.Windows.Forms.Button();
            this.lookupName = new System.Windows.Forms.TextBox();
            this.lookupSex = new System.Windows.Forms.ComboBox();
            this.lookupNameLabel = new System.Windows.Forms.Label();
            this.lookupNameButton = new System.Windows.Forms.Button();
            this.friendsPendingBox = new System.Windows.Forms.CheckedListBox();
            this.friendsFriendsBox = new System.Windows.Forms.CheckedListBox();
            this.friendsAddUriText = new System.Windows.Forms.TextBox();
            this.friendsPendingAcceptButton = new System.Windows.Forms.Button();
            this.friendsFriendsRemoveButton = new System.Windows.Forms.Button();
            this.friendsPendingRejectButton = new System.Windows.Forms.Button();
            this.friendsAddUriButton = new System.Windows.Forms.Button();
            this.friendsFriendsLabel = new System.Windows.Forms.Label();
            this.friendsPendingLabel = new System.Windows.Forms.Label();
            this.friendsAddUriLabel = new System.Windows.Forms.Label();
            this.connectServerPortText = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.connectMyPortText = new System.Windows.Forms.TextBox();
            this.connectLabel1 = new System.Windows.Forms.Label();
            this.connectLabel2 = new System.Windows.Forms.Label();
            this.lookupResultTextBox = new System.Windows.Forms.RichTextBox();
            this.settingsAddress = new System.Windows.Forms.TextBox();
            this.settingsAdressLabel = new System.Windows.Forms.Label();
            this.settingsObjectsRichTextBox2 = new System.Windows.Forms.RichTextBox();
            this.settingsAddObjTextBox = new System.Windows.Forms.TextBox();
            this.settingsAddObjButton = new System.Windows.Forms.Button();
            this.settingsObjLabel = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuMyMural,
            this.MenuMural,
            this.MenuFriends,
            this.MenuLookup,
            this.MenuSettings});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.toolStrip1.Size = new System.Drawing.Size(342, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // MenuMyMural
            // 
            this.MenuMyMural.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuMyMural.Image = ((System.Drawing.Image)(resources.GetObject("MenuMyMural.Image")));
            this.MenuMyMural.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuMyMural.Name = "MenuMyMural";
            this.MenuMyMural.Size = new System.Drawing.Size(36, 36);
            this.MenuMyMural.Text = "MyMural";
            this.MenuMyMural.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // MenuMural
            // 
            this.MenuMural.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuMural.Image = ((System.Drawing.Image)(resources.GetObject("MenuMural.Image")));
            this.MenuMural.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuMural.Name = "MenuMural";
            this.MenuMural.Size = new System.Drawing.Size(36, 36);
            this.MenuMural.Text = "Mural";
            this.MenuMural.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // MenuFriends
            // 
            this.MenuFriends.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuFriends.Image = ((System.Drawing.Image)(resources.GetObject("MenuFriends.Image")));
            this.MenuFriends.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuFriends.Name = "MenuFriends";
            this.MenuFriends.Size = new System.Drawing.Size(36, 36);
            this.MenuFriends.Text = "Friends";
            this.MenuFriends.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // MenuLookup
            // 
            this.MenuLookup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuLookup.Image = ((System.Drawing.Image)(resources.GetObject("MenuLookup.Image")));
            this.MenuLookup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuLookup.Name = "MenuLookup";
            this.MenuLookup.Size = new System.Drawing.Size(36, 36);
            this.MenuLookup.Text = "Lookup";
            this.MenuLookup.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // MenuSettings
            // 
            this.MenuSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuSettings.Image = ((System.Drawing.Image)(resources.GetObject("MenuSettings.Image")));
            this.MenuSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuSettings.Name = "MenuSettings";
            this.MenuSettings.Size = new System.Drawing.Size(36, 36);
            this.MenuSettings.Text = "Settings";
            this.MenuSettings.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextBox1.Location = new System.Drawing.Point(12, 56);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(318, 228);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // myMuralText
            // 
            this.myMuralText.Location = new System.Drawing.Point(12, 293);
            this.myMuralText.MaxLength = 256;
            this.myMuralText.Multiline = true;
            this.myMuralText.Name = "myMuralText";
            this.myMuralText.Size = new System.Drawing.Size(237, 80);
            this.myMuralText.TabIndex = 2;
            this.myMuralText.Visible = false;
            // 
            // myMuralSend
            // 
            this.myMuralSend.Location = new System.Drawing.Point(255, 293);
            this.myMuralSend.Name = "myMuralSend";
            this.myMuralSend.Size = new System.Drawing.Size(75, 23);
            this.myMuralSend.TabIndex = 3;
            this.myMuralSend.Text = "Send";
            this.myMuralSend.UseVisualStyleBackColor = true;
            this.myMuralSend.Visible = false;
            this.myMuralSend.Click += new System.EventHandler(this.myMuralSend_Click);
            // 
            // settingsName
            // 
            this.settingsName.Location = new System.Drawing.Point(105, 60);
            this.settingsName.Name = "settingsName";
            this.settingsName.Size = new System.Drawing.Size(196, 20);
            this.settingsName.TabIndex = 4;
            this.settingsName.Visible = false;
            // 
            // settingsNameLabel
            // 
            this.settingsNameLabel.AutoSize = true;
            this.settingsNameLabel.Location = new System.Drawing.Point(27, 60);
            this.settingsNameLabel.Name = "settingsNameLabel";
            this.settingsNameLabel.Size = new System.Drawing.Size(45, 13);
            this.settingsNameLabel.TabIndex = 5;
            this.settingsNameLabel.Text = "Node Id";
            this.settingsNameLabel.Visible = false;
            this.settingsNameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(125, 350);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonSettings.TabIndex = 11;
            this.buttonSettings.Text = "Save";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Visible = false;
            this.buttonSettings.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(174, 350);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "refresh";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // titulo
            // 
            this.titulo.AutoSize = true;
            this.titulo.BackColor = System.Drawing.SystemColors.MenuBar;
            this.titulo.Font = new System.Drawing.Font("Tw Cen MT", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titulo.Location = new System.Drawing.Point(204, 6);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(130, 24);
            this.titulo.TabIndex = 14;
            this.titulo.Text = "My Messages";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 382);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(342, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(250, 17);
            this.toolStripStatusLabel1.Text = "SIRS - I.S.T project by Mario, Marcus, Navaneeth";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // mymuralClear
            // 
            this.mymuralClear.Location = new System.Drawing.Point(255, 317);
            this.mymuralClear.Name = "mymuralClear";
            this.mymuralClear.Size = new System.Drawing.Size(75, 23);
            this.mymuralClear.TabIndex = 16;
            this.mymuralClear.Text = "Clear";
            this.mymuralClear.UseVisualStyleBackColor = true;
            this.mymuralClear.Visible = false;
            this.mymuralClear.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5Clear
            // 
            this.button5Clear.Location = new System.Drawing.Point(93, 350);
            this.button5Clear.Name = "button5Clear";
            this.button5Clear.Size = new System.Drawing.Size(75, 23);
            this.button5Clear.TabIndex = 17;
            this.button5Clear.Text = "Clear";
            this.button5Clear.UseVisualStyleBackColor = true;
            this.button5Clear.Visible = false;
            this.button5Clear.Click += new System.EventHandler(this.button5Clear_Click);
            // 
            // lookupName
            // 
            this.lookupName.Location = new System.Drawing.Point(120, 275);
            this.lookupName.Name = "lookupName";
            this.lookupName.Size = new System.Drawing.Size(162, 20);
            this.lookupName.TabIndex = 19;
            this.lookupName.Visible = false;
            // 
            // lookupSex
            // 
            this.lookupSex.FormattingEnabled = true;
            this.lookupSex.Items.AddRange(new object[] {
            "M",
            "F"});
            this.lookupSex.Location = new System.Drawing.Point(199, 346);
            this.lookupSex.Name = "lookupSex";
            this.lookupSex.Size = new System.Drawing.Size(83, 21);
            this.lookupSex.TabIndex = 21;
            this.lookupSex.Text = "M";
            this.lookupSex.Visible = false;
            // 
            // lookupNameLabel
            // 
            this.lookupNameLabel.AutoSize = true;
            this.lookupNameLabel.Location = new System.Drawing.Point(13, 277);
            this.lookupNameLabel.Name = "lookupNameLabel";
            this.lookupNameLabel.Size = new System.Drawing.Size(71, 13);
            this.lookupNameLabel.TabIndex = 23;
            this.lookupNameLabel.Text = "Lookup by ID";
            this.lookupNameLabel.Visible = false;
            // 
            // lookupNameButton
            // 
            this.lookupNameButton.Location = new System.Drawing.Point(288, 275);
            this.lookupNameButton.Name = "lookupNameButton";
            this.lookupNameButton.Size = new System.Drawing.Size(42, 23);
            this.lookupNameButton.TabIndex = 26;
            this.lookupNameButton.Text = "Go!";
            this.lookupNameButton.UseVisualStyleBackColor = true;
            this.lookupNameButton.Visible = false;
            this.lookupNameButton.Click += new System.EventHandler(this.lookupNameButton_Click_1);
            // 
            // friendsPendingBox
            // 
            this.friendsPendingBox.CheckOnClick = true;
            this.friendsPendingBox.FormattingEnabled = true;
            this.friendsPendingBox.Location = new System.Drawing.Point(12, 253);
            this.friendsPendingBox.Name = "friendsPendingBox";
            this.friendsPendingBox.Size = new System.Drawing.Size(243, 79);
            this.friendsPendingBox.TabIndex = 29;
            this.friendsPendingBox.Visible = false;
            // 
            // friendsFriendsBox
            // 
            this.friendsFriendsBox.CheckOnClick = true;
            this.friendsFriendsBox.FormattingEnabled = true;
            this.friendsFriendsBox.Location = new System.Drawing.Point(13, 60);
            this.friendsFriendsBox.Name = "friendsFriendsBox";
            this.friendsFriendsBox.Size = new System.Drawing.Size(243, 169);
            this.friendsFriendsBox.TabIndex = 30;
            this.friendsFriendsBox.Visible = false;
            // 
            // friendsAddUriText
            // 
            this.friendsAddUriText.Location = new System.Drawing.Point(12, 353);
            this.friendsAddUriText.Name = "friendsAddUriText";
            this.friendsAddUriText.Size = new System.Drawing.Size(243, 20);
            this.friendsAddUriText.TabIndex = 31;
            this.friendsAddUriText.Visible = false;
            // 
            // friendsPendingAcceptButton
            // 
            this.friendsPendingAcceptButton.Location = new System.Drawing.Point(261, 253);
            this.friendsPendingAcceptButton.Name = "friendsPendingAcceptButton";
            this.friendsPendingAcceptButton.Size = new System.Drawing.Size(69, 23);
            this.friendsPendingAcceptButton.TabIndex = 32;
            this.friendsPendingAcceptButton.Text = "Accept";
            this.friendsPendingAcceptButton.UseVisualStyleBackColor = true;
            this.friendsPendingAcceptButton.Visible = false;
            this.friendsPendingAcceptButton.Click += new System.EventHandler(this.friendsPendingAcceptButton_Click);
            // 
            // friendsFriendsRemoveButton
            // 
            this.friendsFriendsRemoveButton.Location = new System.Drawing.Point(262, 60);
            this.friendsFriendsRemoveButton.Name = "friendsFriendsRemoveButton";
            this.friendsFriendsRemoveButton.Size = new System.Drawing.Size(68, 23);
            this.friendsFriendsRemoveButton.TabIndex = 33;
            this.friendsFriendsRemoveButton.Text = "Remove";
            this.friendsFriendsRemoveButton.UseVisualStyleBackColor = true;
            this.friendsFriendsRemoveButton.Visible = false;
            this.friendsFriendsRemoveButton.Click += new System.EventHandler(this.friendsFriendsRemoveButton_Click);
            // 
            // friendsPendingRejectButton
            // 
            this.friendsPendingRejectButton.Location = new System.Drawing.Point(261, 279);
            this.friendsPendingRejectButton.Name = "friendsPendingRejectButton";
            this.friendsPendingRejectButton.Size = new System.Drawing.Size(68, 23);
            this.friendsPendingRejectButton.TabIndex = 34;
            this.friendsPendingRejectButton.Text = "Reject";
            this.friendsPendingRejectButton.UseVisualStyleBackColor = true;
            this.friendsPendingRejectButton.Visible = false;
            this.friendsPendingRejectButton.Click += new System.EventHandler(this.friendsPendingRejectButton_Click);
            // 
            // friendsAddUriButton
            // 
            this.friendsAddUriButton.Location = new System.Drawing.Point(261, 353);
            this.friendsAddUriButton.Name = "friendsAddUriButton";
            this.friendsAddUriButton.Size = new System.Drawing.Size(69, 23);
            this.friendsAddUriButton.TabIndex = 35;
            this.friendsAddUriButton.Text = "Add";
            this.friendsAddUriButton.UseVisualStyleBackColor = true;
            this.friendsAddUriButton.Visible = false;
            this.friendsAddUriButton.Click += new System.EventHandler(this.friendsAddUriButton_Click);
            // 
            // friendsFriendsLabel
            // 
            this.friendsFriendsLabel.AutoSize = true;
            this.friendsFriendsLabel.Location = new System.Drawing.Point(13, 44);
            this.friendsFriendsLabel.Name = "friendsFriendsLabel";
            this.friendsFriendsLabel.Size = new System.Drawing.Size(72, 13);
            this.friendsFriendsLabel.TabIndex = 36;
            this.friendsFriendsLabel.Text = "Known nodes";
            this.friendsFriendsLabel.Visible = false;
            // 
            // friendsPendingLabel
            // 
            this.friendsPendingLabel.AutoSize = true;
            this.friendsPendingLabel.Location = new System.Drawing.Point(12, 234);
            this.friendsPendingLabel.Name = "friendsPendingLabel";
            this.friendsPendingLabel.Size = new System.Drawing.Size(116, 13);
            this.friendsPendingLabel.TabIndex = 37;
            this.friendsPendingLabel.Text = "Pending node requests";
            this.friendsPendingLabel.Visible = false;
            // 
            // friendsAddUriLabel
            // 
            this.friendsAddUriLabel.AutoSize = true;
            this.friendsAddUriLabel.Location = new System.Drawing.Point(12, 336);
            this.friendsAddUriLabel.Name = "friendsAddUriLabel";
            this.friendsAddUriLabel.Size = new System.Drawing.Size(86, 13);
            this.friendsAddUriLabel.TabIndex = 38;
            this.friendsAddUriLabel.Text = "Add node by link";
            this.friendsAddUriLabel.Visible = false;
            // 
            // connectServerPortText
            // 
            this.connectServerPortText.Location = new System.Drawing.Point(135, 151);
            this.connectServerPortText.Name = "connectServerPortText";
            this.connectServerPortText.Size = new System.Drawing.Size(75, 20);
            this.connectServerPortText.TabIndex = 39;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(135, 177);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 40;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // connectMyPortText
            // 
            this.connectMyPortText.Location = new System.Drawing.Point(135, 109);
            this.connectMyPortText.Name = "connectMyPortText";
            this.connectMyPortText.Size = new System.Drawing.Size(75, 20);
            this.connectMyPortText.TabIndex = 41;
            // 
            // connectLabel1
            // 
            this.connectLabel1.AutoSize = true;
            this.connectLabel1.Location = new System.Drawing.Point(135, 92);
            this.connectLabel1.Name = "connectLabel1";
            this.connectLabel1.Size = new System.Drawing.Size(42, 13);
            this.connectLabel1.TabIndex = 42;
            this.connectLabel1.Text = "My port";
            // 
            // connectLabel2
            // 
            this.connectLabel2.AutoSize = true;
            this.connectLabel2.Location = new System.Drawing.Point(135, 134);
            this.connectLabel2.Name = "connectLabel2";
            this.connectLabel2.Size = new System.Drawing.Size(59, 13);
            this.connectLabel2.TabIndex = 43;
            this.connectLabel2.Text = "Server port";
            // 
            // lookupResultTextBox
            // 
            this.lookupResultTextBox.Location = new System.Drawing.Point(13, 56);
            this.lookupResultTextBox.Name = "lookupResultTextBox";
            this.lookupResultTextBox.Size = new System.Drawing.Size(317, 228);
            this.lookupResultTextBox.TabIndex = 44;
            this.lookupResultTextBox.Text = "";
            this.lookupResultTextBox.Visible = false;
            // 
            // settingsAddress
            // 
            this.settingsAddress.Location = new System.Drawing.Point(105, 89);
            this.settingsAddress.Name = "settingsAddress";
            this.settingsAddress.Size = new System.Drawing.Size(196, 20);
            this.settingsAddress.TabIndex = 45;
            this.settingsAddress.Visible = false;
            // 
            // settingsAdressLabel
            // 
            this.settingsAdressLabel.AutoSize = true;
            this.settingsAdressLabel.Location = new System.Drawing.Point(27, 92);
            this.settingsAdressLabel.Name = "settingsAdressLabel";
            this.settingsAdressLabel.Size = new System.Drawing.Size(75, 13);
            this.settingsAdressLabel.TabIndex = 46;
            this.settingsAdressLabel.Text = "Spoof address";
            this.settingsAdressLabel.Visible = false;
            // 
            // settingsObjectsRichTextBox2
            // 
            this.settingsObjectsRichTextBox2.Location = new System.Drawing.Point(105, 116);
            this.settingsObjectsRichTextBox2.Name = "settingsObjectsRichTextBox2";
            this.settingsObjectsRichTextBox2.Size = new System.Drawing.Size(196, 115);
            this.settingsObjectsRichTextBox2.TabIndex = 47;
            this.settingsObjectsRichTextBox2.Text = "";
            // 
            // settingsAddObjTextBox
            // 
            this.settingsAddObjTextBox.Location = new System.Drawing.Point(105, 238);
            this.settingsAddObjTextBox.Name = "settingsAddObjTextBox";
            this.settingsAddObjTextBox.Size = new System.Drawing.Size(144, 20);
            this.settingsAddObjTextBox.TabIndex = 48;
            // 
            // settingsAddObjButton
            // 
            this.settingsAddObjButton.Location = new System.Drawing.Point(254, 238);
            this.settingsAddObjButton.Name = "settingsAddObjButton";
            this.settingsAddObjButton.Size = new System.Drawing.Size(47, 23);
            this.settingsAddObjButton.TabIndex = 49;
            this.settingsAddObjButton.Text = "Add";
            this.settingsAddObjButton.UseVisualStyleBackColor = true;
            this.settingsAddObjButton.Click += new System.EventHandler(this.settingsAddObjButton_Click);
            // 
            // settingsObjLabel
            // 
            this.settingsObjLabel.AutoSize = true;
            this.settingsObjLabel.Location = new System.Drawing.Point(27, 116);
            this.settingsObjLabel.Name = "settingsObjLabel";
            this.settingsObjLabel.Size = new System.Drawing.Size(43, 13);
            this.settingsObjLabel.TabIndex = 50;
            this.settingsObjLabel.Text = "Objects";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(342, 404);
            this.Controls.Add(this.settingsObjLabel);
            this.Controls.Add(this.settingsAddObjButton);
            this.Controls.Add(this.settingsAddObjTextBox);
            this.Controls.Add(this.settingsObjectsRichTextBox2);
            this.Controls.Add(this.settingsAdressLabel);
            this.Controls.Add(this.settingsAddress);
            this.Controls.Add(this.connectLabel2);
            this.Controls.Add(this.connectLabel1);
            this.Controls.Add(this.connectMyPortText);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.connectServerPortText);
            this.Controls.Add(this.friendsAddUriLabel);
            this.Controls.Add(this.friendsPendingLabel);
            this.Controls.Add(this.friendsFriendsLabel);
            this.Controls.Add(this.friendsFriendsRemoveButton);
            this.Controls.Add(this.lookupNameLabel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.titulo);
            this.Controls.Add(this.settingsNameLabel);
            this.Controls.Add(this.settingsName);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lookupNameButton);
            this.Controls.Add(this.friendsAddUriButton);
            this.Controls.Add(this.button5Clear);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.lookupName);
            this.Controls.Add(this.myMuralSend);
            this.Controls.Add(this.friendsPendingBox);
            this.Controls.Add(this.friendsPendingRejectButton);
            this.Controls.Add(this.friendsPendingAcceptButton);
            this.Controls.Add(this.lookupResultTextBox);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lookupSex);
            this.Controls.Add(this.myMuralText);
            this.Controls.Add(this.friendsAddUriText);
            this.Controls.Add(this.mymuralClear);
            this.Controls.Add(this.friendsFriendsBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClientForm";
            this.Text = "Node Client";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton MenuMyMural;
        private System.Windows.Forms.ToolStripButton MenuMural;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox myMuralText;
        private System.Windows.Forms.Button myMuralSend;
        private System.Windows.Forms.TextBox settingsName;
        private System.Windows.Forms.Label settingsNameLabel;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.ToolStripButton MenuSettings;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button mymuralClear;
        private System.Windows.Forms.Button button5Clear;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton MenuLookup;
        private System.Windows.Forms.TextBox lookupName;
        private System.Windows.Forms.ComboBox lookupSex;
        private System.Windows.Forms.Label lookupNameLabel;
        private System.Windows.Forms.Button lookupNameButton;
        private System.Windows.Forms.ToolStripButton MenuFriends;
        private System.Windows.Forms.CheckedListBox friendsPendingBox;
        private System.Windows.Forms.CheckedListBox friendsFriendsBox;
        private System.Windows.Forms.TextBox friendsAddUriText;
        private System.Windows.Forms.Button friendsPendingAcceptButton;
        private System.Windows.Forms.Button friendsFriendsRemoveButton;
        private System.Windows.Forms.Button friendsPendingRejectButton;
        private System.Windows.Forms.Button friendsAddUriButton;
        private System.Windows.Forms.Label friendsFriendsLabel;
        private System.Windows.Forms.Label friendsPendingLabel;
        private System.Windows.Forms.Label friendsAddUriLabel;
        private System.Windows.Forms.TextBox connectServerPortText;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox connectMyPortText;
        private System.Windows.Forms.Label connectLabel1;
        private System.Windows.Forms.Label connectLabel2;
        private System.Windows.Forms.RichTextBox lookupResultTextBox;
        private System.Windows.Forms.TextBox settingsAddress;
        private System.Windows.Forms.Label settingsAdressLabel;
        private System.Windows.Forms.RichTextBox settingsObjectsRichTextBox2;
        private System.Windows.Forms.TextBox settingsAddObjTextBox;
        private System.Windows.Forms.Button settingsAddObjButton;
        private System.Windows.Forms.Label settingsObjLabel;
    }
}

