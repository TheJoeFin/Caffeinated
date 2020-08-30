﻿namespace Caffeinated {
    partial class SettingsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DefaultDurationBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SettingsAtLaunchChkBox = new System.Windows.Forms.CheckBox();
            this.ActivateChkBox = new System.Windows.Forms.CheckBox();
            this.StartupChkBox = new System.Windows.Forms.CheckBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(181, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(453, 200);
            this.label1.TabIndex = 0;
            this.label1.Text = "Caffeinated is now running. You can find its icon in the notification area near t" +
    "he clock. Click it to keep your PC awake by disabling automatic sleep. Click the" +
    " icon again to allow automatic sleep.";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(181, 132);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(453, 68);
            this.label2.TabIndex = 1;
            this.label2.Text = "Right-click the notification area icon to show the Caffeinated menu.";
            // 
            // DefaultDurationBox
            // 
            this.DefaultDurationBox.FormattingEnabled = true;
            this.DefaultDurationBox.Location = new System.Drawing.Point(327, 323);
            this.DefaultDurationBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DefaultDurationBox.Name = "DefaultDurationBox";
            this.DefaultDurationBox.Size = new System.Drawing.Size(198, 33);
            this.DefaultDurationBox.TabIndex = 5;
            this.DefaultDurationBox.SelectedIndexChanged += new System.EventHandler(this.DefaultDurationBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 328);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Default duration:";
            // 
            // SettingsAtLaunchChkBox
            // 
            this.SettingsAtLaunchChkBox.AutoSize = true;
            this.SettingsAtLaunchChkBox.Checked = global::Caffeinated.Properties.Settings.Default.ShowSettingsAtLaunch;
            this.SettingsAtLaunchChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SettingsAtLaunchChkBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Caffeinated.Properties.Settings.Default, "ShowSettingsAtLaunch", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SettingsAtLaunchChkBox.Location = new System.Drawing.Point(186, 282);
            this.SettingsAtLaunchChkBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SettingsAtLaunchChkBox.Name = "SettingsAtLaunchChkBox";
            this.SettingsAtLaunchChkBox.Size = new System.Drawing.Size(293, 29);
            this.SettingsAtLaunchChkBox.TabIndex = 4;
            this.SettingsAtLaunchChkBox.Text = "Show this message upon launch";
            this.SettingsAtLaunchChkBox.UseVisualStyleBackColor = true;
            // 
            // ActivateChkBox
            // 
            this.ActivateChkBox.AutoSize = true;
            this.ActivateChkBox.Checked = global::Caffeinated.Properties.Settings.Default.ActivateAtLaunch;
            this.ActivateChkBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Caffeinated.Properties.Settings.Default, "ActivateAtLaunch", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ActivateChkBox.Location = new System.Drawing.Point(186, 240);
            this.ActivateChkBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ActivateChkBox.Name = "ActivateChkBox";
            this.ActivateChkBox.Size = new System.Drawing.Size(204, 29);
            this.ActivateChkBox.TabIndex = 3;
            this.ActivateChkBox.Text = "Activate upon launch";
            this.ActivateChkBox.UseVisualStyleBackColor = true;
            this.ActivateChkBox.CheckedChanged += new System.EventHandler(this.StartupChkBox_CheckedChanged);
            // 
            // StartupChkBox
            // 
            this.StartupChkBox.AutoSize = true;
            this.StartupChkBox.Location = new System.Drawing.Point(186, 198);
            this.StartupChkBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StartupChkBox.Name = "StartupChkBox";
            this.StartupChkBox.Size = new System.Drawing.Size(362, 29);
            this.StartupChkBox.TabIndex = 2;
            this.StartupChkBox.Text = "Automatically launch at Windows startup";
            this.StartupChkBox.UseVisualStyleBackColor = true;
            this.StartupChkBox.CheckedChanged += new System.EventHandler(this.LaunchWithWindowsLogin);
            // 
            // okBtn
            // 
            this.okBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okBtn.Location = new System.Drawing.Point(376, 378);
            this.okBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(124, 45);
            this.okBtn.TabIndex = 8;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelBtn.Location = new System.Drawing.Point(510, 378);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(124, 45);
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Caffeinated.Properties.Resources.Caffeine_Black_96;
            this.pictureBox1.Location = new System.Drawing.Point(17, 20);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(137, 160);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(653, 444);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DefaultDurationBox);
            this.Controls.Add(this.SettingsAtLaunchChkBox);
            this.Controls.Add(this.ActivateChkBox);
            this.Controls.Add(this.StartupChkBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Welcome to Caffeinated";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox StartupChkBox;
        private System.Windows.Forms.CheckBox ActivateChkBox;
        private System.Windows.Forms.CheckBox SettingsAtLaunchChkBox;
        private System.Windows.Forms.ComboBox DefaultDurationBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}