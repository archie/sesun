namespace Client
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.myMuralText = new System.Windows.Forms.TextBox();
            this.myMuralSend = new System.Windows.Forms.Button();
            this.settingsName = new System.Windows.Forms.TextBox();
            this.settingsNameLabel = new System.Windows.Forms.Label();
            this.settingsSexLabel = new System.Windows.Forms.Label();
            this.settingsSex = new System.Windows.Forms.ComboBox();
            this.settingsBirthDateTime = new System.Windows.Forms.DateTimePicker();
            this.settingsBirthLabel = new System.Windows.Forms.Label();
            this.settingsInterests = new System.Windows.Forms.CheckedListBox();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.settingsInterestsLabel = new System.Windows.Forms.Label();
            this.titulo = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mymuralClear = new System.Windows.Forms.Button();
            this.button5Clear = new System.Windows.Forms.Button();
            this.lookupInterest = new System.Windows.Forms.ComboBox();
            this.lookupName = new System.Windows.Forms.TextBox();
            this.lookupAge = new System.Windows.Forms.NumericUpDown();
            this.lookupSex = new System.Windows.Forms.ComboBox();
            this.lookupInterestLabel = new System.Windows.Forms.Label();
            this.lookupNameLabel = new System.Windows.Forms.Label();
            this.lookupSexAgeLabel = new System.Windows.Forms.Label();
            this.lookupInterestButton = new System.Windows.Forms.Button();
            this.lookupNameButton = new System.Windows.Forms.Button();
            this.lookupSexAgeButton = new System.Windows.Forms.Button();
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
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookupAge)).BeginInit();
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
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton5,
            this.toolStripButton4,
            this.toolStripButton3});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.toolStrip1.Size = new System.Drawing.Size(342, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton1.Text = "MyMural";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton2.Text = "Mural";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton5.Text = "Friends";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton4.Text = "Lookup";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton3.Text = "Settings";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
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
            // 
            // myMuralText
            // 
            this.myMuralText.Location = new System.Drawing.Point(12, 293);
            this.myMuralText.MaxLength = 256;
            this.myMuralText.Multiline = true;
            this.myMuralText.Name = "myMuralText";
            this.myMuralText.Size = new System.Drawing.Size(237, 80);
            this.myMuralText.TabIndex = 2;
            // 
            // myMuralSend
            // 
            this.myMuralSend.Location = new System.Drawing.Point(255, 293);
            this.myMuralSend.Name = "myMuralSend";
            this.myMuralSend.Size = new System.Drawing.Size(75, 23);
            this.myMuralSend.TabIndex = 3;
            this.myMuralSend.Text = "Send";
            this.myMuralSend.UseVisualStyleBackColor = true;
            this.myMuralSend.Click += new System.EventHandler(this.myMuralSend_Click);
            // 
            // settingsName
            // 
            this.settingsName.Location = new System.Drawing.Point(87, 60);
            this.settingsName.Name = "settingsName";
            this.settingsName.Size = new System.Drawing.Size(230, 20);
            this.settingsName.TabIndex = 4;
            // 
            // settingsNameLabel
            // 
            this.settingsNameLabel.AutoSize = true;
            this.settingsNameLabel.Location = new System.Drawing.Point(27, 60);
            this.settingsNameLabel.Name = "settingsNameLabel";
            this.settingsNameLabel.Size = new System.Drawing.Size(35, 13);
            this.settingsNameLabel.TabIndex = 5;
            this.settingsNameLabel.Text = "Name";
            this.settingsNameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // settingsSexLabel
            // 
            this.settingsSexLabel.AutoSize = true;
            this.settingsSexLabel.Location = new System.Drawing.Point(27, 87);
            this.settingsSexLabel.Name = "settingsSexLabel";
            this.settingsSexLabel.Size = new System.Drawing.Size(25, 13);
            this.settingsSexLabel.TabIndex = 6;
            this.settingsSexLabel.Text = "Sex";
            // 
            // settingsSex
            // 
            this.settingsSex.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.settingsSex.FormattingEnabled = true;
            this.settingsSex.Items.AddRange(new object[] {
            "M",
            "F"});
            this.settingsSex.Location = new System.Drawing.Point(87, 87);
            this.settingsSex.Name = "settingsSex";
            this.settingsSex.Size = new System.Drawing.Size(72, 21);
            this.settingsSex.TabIndex = 7;
            this.settingsSex.Text = "M";
            // 
            // settingsBirthDateTime
            // 
            this.settingsBirthDateTime.Location = new System.Drawing.Point(87, 114);
            this.settingsBirthDateTime.Name = "settingsBirthDateTime";
            this.settingsBirthDateTime.Size = new System.Drawing.Size(230, 20);
            this.settingsBirthDateTime.TabIndex = 8;
            this.settingsBirthDateTime.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // settingsBirthLabel
            // 
            this.settingsBirthLabel.AutoSize = true;
            this.settingsBirthLabel.Location = new System.Drawing.Point(27, 114);
            this.settingsBirthLabel.Name = "settingsBirthLabel";
            this.settingsBirthLabel.Size = new System.Drawing.Size(54, 13);
            this.settingsBirthLabel.TabIndex = 9;
            this.settingsBirthLabel.Text = "Birth Date";
            // 
            // settingsInterests
            // 
            this.settingsInterests.CheckOnClick = true;
            this.settingsInterests.FormattingEnabled = true;
            this.settingsInterests.Items.AddRange(new object[] {
            "Cars",
            "Comics",
            "Finance",
            "Games",
            "Hobbies",
            "Jobs",
            "Literatures",
            "Life",
            "Medicine",
            "Movies",
            "Music",
            "Nature",
            "Painting",
            "Personal",
            "Politics",
            "Religion",
            "Science",
            "Sports",
            "Travel"});
            this.settingsInterests.Location = new System.Drawing.Point(87, 141);
            this.settingsInterests.Name = "settingsInterests";
            this.settingsInterests.Size = new System.Drawing.Size(230, 199);
            this.settingsInterests.TabIndex = 10;
            this.settingsInterests.Visible = false;
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
            // 
            // settingsInterestsLabel
            // 
            this.settingsInterestsLabel.AutoSize = true;
            this.settingsInterestsLabel.Location = new System.Drawing.Point(27, 141);
            this.settingsInterestsLabel.Name = "settingsInterestsLabel";
            this.settingsInterestsLabel.Size = new System.Drawing.Size(47, 13);
            this.settingsInterestsLabel.TabIndex = 13;
            this.settingsInterestsLabel.Text = "Interests";
            // 
            // titulo
            // 
            this.titulo.AutoSize = true;
            this.titulo.BackColor = System.Drawing.SystemColors.MenuBar;
            this.titulo.Font = new System.Drawing.Font("Tw Cen MT", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titulo.Location = new System.Drawing.Point(226, 6);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(95, 24);
            this.titulo.TabIndex = 14;
            this.titulo.Text = "My Mural";
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
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(260, 17);
            this.toolStripStatusLabel1.Text = "PADIbook - I.S.T project by Mario, Bugg and Jose.";
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
            // lookupInterest
            // 
            this.lookupInterest.FormattingEnabled = true;
            this.lookupInterest.Items.AddRange(new object[] {
            "Cars",
            "Comics",
            "Finance",
            "Games",
            "Hobbies",
            "Jobs",
            "Literatures",
            "Life",
            "Medicine",
            "Movies",
            "Music",
            "Nature",
            "Painting",
            "Personal",
            "Politics",
            "Religion",
            "Science",
            "Sports",
            "Travel"});
            this.lookupInterest.Location = new System.Drawing.Point(120, 295);
            this.lookupInterest.Name = "lookupInterest";
            this.lookupInterest.Size = new System.Drawing.Size(162, 21);
            this.lookupInterest.TabIndex = 18;
            this.lookupInterest.Text = "Cars";
            this.lookupInterest.Visible = false;
            // 
            // lookupName
            // 
            this.lookupName.Location = new System.Drawing.Point(120, 322);
            this.lookupName.Name = "lookupName";
            this.lookupName.Size = new System.Drawing.Size(162, 20);
            this.lookupName.TabIndex = 19;
            this.lookupName.Visible = false;
            // 
            // lookupAge
            // 
            this.lookupAge.Location = new System.Drawing.Point(120, 346);
            this.lookupAge.Name = "lookupAge";
            this.lookupAge.Size = new System.Drawing.Size(72, 20);
            this.lookupAge.TabIndex = 20;
            this.lookupAge.Visible = false;
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
            // lookupInterestLabel
            // 
            this.lookupInterestLabel.AutoSize = true;
            this.lookupInterestLabel.Location = new System.Drawing.Point(13, 299);
            this.lookupInterestLabel.Name = "lookupInterestLabel";
            this.lookupInterestLabel.Size = new System.Drawing.Size(95, 13);
            this.lookupInterestLabel.TabIndex = 22;
            this.lookupInterestLabel.Text = "Lookup by Interest";
            this.lookupInterestLabel.Visible = false;
            // 
            // lookupNameLabel
            // 
            this.lookupNameLabel.AutoSize = true;
            this.lookupNameLabel.Location = new System.Drawing.Point(13, 324);
            this.lookupNameLabel.Name = "lookupNameLabel";
            this.lookupNameLabel.Size = new System.Drawing.Size(88, 13);
            this.lookupNameLabel.TabIndex = 23;
            this.lookupNameLabel.Text = "Lookup by Name";
            this.lookupNameLabel.Visible = false;
            // 
            // lookupSexAgeLabel
            // 
            this.lookupSexAgeLabel.AutoSize = true;
            this.lookupSexAgeLabel.Location = new System.Drawing.Point(13, 349);
            this.lookupSexAgeLabel.Name = "lookupSexAgeLabel";
            this.lookupSexAgeLabel.Size = new System.Drawing.Size(102, 13);
            this.lookupSexAgeLabel.TabIndex = 24;
            this.lookupSexAgeLabel.Text = "Lookup by Sex/Age";
            this.lookupSexAgeLabel.Visible = false;
            // 
            // lookupInterestButton
            // 
            this.lookupInterestButton.Location = new System.Drawing.Point(289, 295);
            this.lookupInterestButton.Name = "lookupInterestButton";
            this.lookupInterestButton.Size = new System.Drawing.Size(41, 23);
            this.lookupInterestButton.TabIndex = 25;
            this.lookupInterestButton.Text = "Go!";
            this.lookupInterestButton.UseVisualStyleBackColor = true;
            this.lookupInterestButton.Visible = false;
            // 
            // lookupNameButton
            // 
            this.lookupNameButton.Location = new System.Drawing.Point(288, 322);
            this.lookupNameButton.Name = "lookupNameButton";
            this.lookupNameButton.Size = new System.Drawing.Size(42, 23);
            this.lookupNameButton.TabIndex = 26;
            this.lookupNameButton.Text = "Go!";
            this.lookupNameButton.UseVisualStyleBackColor = true;
            this.lookupNameButton.Visible = false;
            // 
            // lookupSexAgeButton
            // 
            this.lookupSexAgeButton.Location = new System.Drawing.Point(288, 346);
            this.lookupSexAgeButton.Name = "lookupSexAgeButton";
            this.lookupSexAgeButton.Size = new System.Drawing.Size(42, 23);
            this.lookupSexAgeButton.TabIndex = 28;
            this.lookupSexAgeButton.Text = "Go!";
            this.lookupSexAgeButton.UseVisualStyleBackColor = true;
            this.lookupSexAgeButton.Visible = false;
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
            // 
            // friendsFriendsLabel
            // 
            this.friendsFriendsLabel.AutoSize = true;
            this.friendsFriendsLabel.Location = new System.Drawing.Point(13, 44);
            this.friendsFriendsLabel.Name = "friendsFriendsLabel";
            this.friendsFriendsLabel.Size = new System.Drawing.Size(58, 13);
            this.friendsFriendsLabel.TabIndex = 36;
            this.friendsFriendsLabel.Text = "My Friends";
            this.friendsFriendsLabel.Visible = false;
            // 
            // friendsPendingLabel
            // 
            this.friendsPendingLabel.AutoSize = true;
            this.friendsPendingLabel.Location = new System.Drawing.Point(12, 234);
            this.friendsPendingLabel.Name = "friendsPendingLabel";
            this.friendsPendingLabel.Size = new System.Drawing.Size(118, 13);
            this.friendsPendingLabel.TabIndex = 37;
            this.friendsPendingLabel.Text = "Pending friend requests";
            this.friendsPendingLabel.Visible = false;
            // 
            // friendsAddUriLabel
            // 
            this.friendsAddUriLabel.AutoSize = true;
            this.friendsAddUriLabel.Location = new System.Drawing.Point(12, 336);
            this.friendsAddUriLabel.Name = "friendsAddUriLabel";
            this.friendsAddUriLabel.Size = new System.Drawing.Size(88, 13);
            this.friendsAddUriLabel.TabIndex = 38;
            this.friendsAddUriLabel.Text = "Add friend by link";
            this.friendsAddUriLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(342, 404);
            this.Controls.Add(this.friendsFriendsBox);
            this.Controls.Add(this.friendsAddUriLabel);
            this.Controls.Add(this.friendsPendingLabel);
            this.Controls.Add(this.friendsFriendsLabel);
            this.Controls.Add(this.friendsAddUriButton);
            this.Controls.Add(this.friendsPendingRejectButton);
            this.Controls.Add(this.friendsFriendsRemoveButton);
            this.Controls.Add(this.friendsPendingAcceptButton);
            this.Controls.Add(this.friendsPendingBox);
            this.Controls.Add(this.lookupSexAgeButton);
            this.Controls.Add(this.lookupNameButton);
            this.Controls.Add(this.lookupInterestButton);
            this.Controls.Add(this.lookupSexAgeLabel);
            this.Controls.Add(this.lookupNameLabel);
            this.Controls.Add(this.lookupInterestLabel);
            this.Controls.Add(this.lookupSex);
            this.Controls.Add(this.lookupAge);
            this.Controls.Add(this.lookupName);
            this.Controls.Add(this.lookupInterest);
            this.Controls.Add(this.button5Clear);
            this.Controls.Add(this.mymuralClear);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.titulo);
            this.Controls.Add(this.settingsInterestsLabel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.settingsInterests);
            this.Controls.Add(this.settingsBirthLabel);
            this.Controls.Add(this.settingsBirthDateTime);
            this.Controls.Add(this.settingsSex);
            this.Controls.Add(this.settingsSexLabel);
            this.Controls.Add(this.settingsNameLabel);
            this.Controls.Add(this.settingsName);
            this.Controls.Add(this.myMuralSend);
            this.Controls.Add(this.myMuralText);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.friendsAddUriText);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PADIbook";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookupAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox myMuralText;
        private System.Windows.Forms.Button myMuralSend;
        private System.Windows.Forms.TextBox settingsName;
        private System.Windows.Forms.Label settingsNameLabel;
        private System.Windows.Forms.Label settingsSexLabel;
        private System.Windows.Forms.ComboBox settingsSex;
        private System.Windows.Forms.DateTimePicker settingsBirthDateTime;
        private System.Windows.Forms.Label settingsBirthLabel;
        private System.Windows.Forms.CheckedListBox settingsInterests;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label settingsInterestsLabel;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button mymuralClear;
        private System.Windows.Forms.Button button5Clear;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ComboBox lookupInterest;
        private System.Windows.Forms.TextBox lookupName;
        private System.Windows.Forms.NumericUpDown lookupAge;
        private System.Windows.Forms.ComboBox lookupSex;
        private System.Windows.Forms.Label lookupInterestLabel;
        private System.Windows.Forms.Label lookupNameLabel;
        private System.Windows.Forms.Label lookupSexAgeLabel;
        private System.Windows.Forms.Button lookupInterestButton;
        private System.Windows.Forms.Button lookupNameButton;
        private System.Windows.Forms.Button lookupSexAgeButton;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
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
    }
}

