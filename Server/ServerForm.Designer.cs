namespace Server
{
    partial class ServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.posts = new System.Windows.Forms.RichTextBox();
            this.friends = new System.Windows.Forms.RichTextBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.settings = new System.Windows.Forms.RichTextBox();
            this.redirectionRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.freeze_period = new System.Windows.Forms.NumericUpDown();
            this.freeze_buttom = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.objectsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.debugRich = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.freeze_period)).BeginInit();
            this.SuspendLayout();
            // 
            // posts
            // 
            this.posts.Location = new System.Drawing.Point(12, 53);
            this.posts.Name = "posts";
            this.posts.Size = new System.Drawing.Size(225, 122);
            this.posts.TabIndex = 0;
            this.posts.Text = "";
            this.posts.TextChanged += new System.EventHandler(this.posts_TextChanged);
            // 
            // friends
            // 
            this.friends.Location = new System.Drawing.Point(251, 53);
            this.friends.Name = "friends";
            this.friends.Size = new System.Drawing.Size(225, 162);
            this.friends.TabIndex = 1;
            this.friends.Text = "";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(402, 383);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // settings
            // 
            this.settings.Location = new System.Drawing.Point(13, 305);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(224, 70);
            this.settings.TabIndex = 3;
            this.settings.Text = "";
            // 
            // redirectionRichTextBox
            // 
            this.redirectionRichTextBox.Location = new System.Drawing.Point(251, 234);
            this.redirectionRichTextBox.Name = "redirectionRichTextBox";
            this.redirectionRichTextBox.Size = new System.Drawing.Size(225, 145);
            this.redirectionRichTextBox.TabIndex = 4;
            this.redirectionRichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.MidnightBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(191, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Server Monitor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.MidnightBlue;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(9, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Messages";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.MidnightBlue;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(12, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Node Info";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.MidnightBlue;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(248, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Known nodes";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.MidnightBlue;
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(248, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Redirection List";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // freeze_period
            // 
            this.freeze_period.Location = new System.Drawing.Point(195, 383);
            this.freeze_period.Name = "freeze_period";
            this.freeze_period.Size = new System.Drawing.Size(42, 20);
            this.freeze_period.TabIndex = 14;
            this.freeze_period.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // freeze_buttom
            // 
            this.freeze_buttom.Location = new System.Drawing.Point(326, 383);
            this.freeze_buttom.Name = "freeze_buttom";
            this.freeze_buttom.Size = new System.Drawing.Size(75, 23);
            this.freeze_buttom.TabIndex = 16;
            this.freeze_buttom.Text = "Freeze";
            this.freeze_buttom.UseVisualStyleBackColor = true;
            this.freeze_buttom.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.MidnightBlue;
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(106, 383);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Freeze Period";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // objectsRichTextBox
            // 
            this.objectsRichTextBox.Location = new System.Drawing.Point(12, 193);
            this.objectsRichTextBox.Name = "objectsRichTextBox";
            this.objectsRichTextBox.Size = new System.Drawing.Size(225, 93);
            this.objectsRichTextBox.TabIndex = 18;
            this.objectsRichTextBox.Text = "";
            this.objectsRichTextBox.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.MidnightBlue;
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(13, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Objects";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(251, 383);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 21;
            this.loadButton.Tag = "Load User XML";
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // debugRich
            // 
            this.debugRich.Location = new System.Drawing.Point(12, 380);
            this.debugRich.Name = "debugRich";
            this.debugRich.Size = new System.Drawing.Size(88, 26);
            this.debugRich.TabIndex = 22;
            this.debugRich.Text = "";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(489, 414);
            this.Controls.Add(this.debugRich);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.objectsRichTextBox);
            this.Controls.Add(this.freeze_buttom);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.freeze_period);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.redirectionRichTextBox);
            this.Controls.Add(this.settings);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.friends);
            this.Controls.Add(this.posts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerForm";
            this.Text = "Node Server";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.freeze_period)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox posts;
        private System.Windows.Forms.RichTextBox friends;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.RichTextBox settings;
        private System.Windows.Forms.RichTextBox redirectionRichTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown freeze_period;
        private System.Windows.Forms.Button freeze_buttom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox objectsRichTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.RichTextBox debugRich;
    }
}

