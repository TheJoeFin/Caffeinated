namespace Caffeinated {
    partial class AboutForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            VersionTxtBlk = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            dmndLbl = new System.Windows.Forms.LinkLabel();
            caffeineLbl = new System.Windows.Forms.LinkLabel();
            label1 = new System.Windows.Forms.Label();
            OriginallyByLink = new System.Windows.Forms.LinkLabel();
            ForkedByTheJoeFinLink = new System.Windows.Forms.LinkLabel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            RateLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // VersionTxtBlk
            // 
            VersionTxtBlk.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            VersionTxtBlk.AutoSize = true;
            VersionTxtBlk.Location = new System.Drawing.Point(22, 325);
            VersionTxtBlk.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            VersionTxtBlk.Name = "VersionTxtBlk";
            VersionTxtBlk.Size = new System.Drawing.Size(148, 32);
            VersionTxtBlk.TabIndex = 13;
            VersionTxtBlk.Text = "Version 1.8.0";
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label3.Location = new System.Drawing.Point(22, 381);
            label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(399, 86);
            label3.TabIndex = 14;
            label3.Text = "Prevents your PC from going to sleep or displaying the screen saver.";
            // 
            // dmndLbl
            // 
            dmndLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dmndLbl.AutoSize = true;
            dmndLbl.Location = new System.Drawing.Point(44, 467);
            dmndLbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            dmndLbl.Name = "dmndLbl";
            dmndLbl.Size = new System.Drawing.Size(0, 32);
            dmndLbl.TabIndex = 17;
            dmndLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            dmndLbl.LinkClicked += linkLbl_LinkClicked;
            // 
            // caffeineLbl
            // 
            caffeineLbl.AutoSize = true;
            caffeineLbl.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            caffeineLbl.Location = new System.Drawing.Point(22, 467);
            caffeineLbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            caffeineLbl.Name = "caffeineLbl";
            caffeineLbl.Size = new System.Drawing.Size(329, 32);
            caffeineLbl.TabIndex = 18;
            caffeineLbl.Text = "Inspired by Caffeine for OS X.";
            caffeineLbl.LinkClicked += linkLbl_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            label1.Location = new System.Drawing.Point(22, 268);
            label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(183, 41);
            label1.TabIndex = 20;
            label1.Text = "Caffeinated";
            label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // OriginallyByLink
            // 
            OriginallyByLink.AutoSize = true;
            OriginallyByLink.LinkArea = new System.Windows.Forms.LinkArea(14, 18);
            OriginallyByLink.Location = new System.Drawing.Point(22, 562);
            OriginallyByLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            OriginallyByLink.Name = "OriginallyByLink";
            OriginallyByLink.Size = new System.Drawing.Size(217, 38);
            OriginallyByLink.TabIndex = 21;
            OriginallyByLink.TabStop = true;
            OriginallyByLink.Text = "Originally by dmnd";
            OriginallyByLink.UseCompatibleTextRendering = true;
            OriginallyByLink.LinkClicked += linkLabel1_LinkClicked;
            // 
            // ForkedByTheJoeFinLink
            // 
            ForkedByTheJoeFinLink.AutoSize = true;
            ForkedByTheJoeFinLink.LinkArea = new System.Windows.Forms.LinkArea(10, 19);
            ForkedByTheJoeFinLink.Location = new System.Drawing.Point(257, 562);
            ForkedByTheJoeFinLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ForkedByTheJoeFinLink.Name = "ForkedByTheJoeFinLink";
            ForkedByTheJoeFinLink.Size = new System.Drawing.Size(232, 38);
            ForkedByTheJoeFinLink.TabIndex = 22;
            ForkedByTheJoeFinLink.TabStop = true;
            ForkedByTheJoeFinLink.Text = "Forked by TheJoeFin";
            ForkedByTheJoeFinLink.UseCompatibleTextRendering = true;
            ForkedByTheJoeFinLink.LinkClicked += ForkedByTheJoeFinLink_LinkClicked;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Caffeine_Black_96;
            pictureBox1.Location = new System.Drawing.Point(22, 26);
            pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(227, 224);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 23;
            pictureBox1.TabStop = false;
            // 
            // RateLabel
            // 
            RateLabel.AutoSize = true;
            RateLabel.Location = new System.Drawing.Point(44, 632);
            RateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            RateLabel.Name = "RateLabel";
            RateLabel.Size = new System.Drawing.Size(426, 32);
            RateLabel.TabIndex = 24;
            RateLabel.TabStop = true;
            RateLabel.Text = "Rate Windows Caffeinated in the Store";
            RateLabel.LinkClicked += RateLabel_LinkClicked;
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(491, 716);
            Controls.Add(RateLabel);
            Controls.Add(pictureBox1);
            Controls.Add(ForkedByTheJoeFinLink);
            Controls.Add(OriginallyByLink);
            Controls.Add(label1);
            Controls.Add(caffeineLbl);
            Controls.Add(dmndLbl);
            Controls.Add(label3);
            Controls.Add(VersionTxtBlk);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            Padding = new System.Windows.Forms.Padding(18, 22, 18, 22);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "About Caffeinated";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label VersionTxtBlk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel dmndLbl;
        private System.Windows.Forms.LinkLabel caffeineLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel OriginallyByLink;
        private System.Windows.Forms.LinkLabel ForkedByTheJoeFinLink;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel RateLabel;
    }
}
