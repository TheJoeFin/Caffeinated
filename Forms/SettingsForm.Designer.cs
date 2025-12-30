namespace Caffeinated {
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
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            scrollablePanel = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            label3 = new System.Windows.Forms.Label();
            DurationsListView = new System.Windows.Forms.ListView();
            SettingsAtLaunchChkBox = new System.Windows.Forms.CheckBox();
            ActivateChkBox = new System.Windows.Forms.CheckBox();
            StartupChkBox = new System.Windows.Forms.CheckBox();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            OffIconLbl = new System.Windows.Forms.Label();
            OnIconLbl = new System.Windows.Forms.Label();
            defaultRDBTN = new System.Windows.Forms.RadioButton();
            eyeZZZRDBTN = new System.Windows.Forms.RadioButton();
            mugRDBTN = new System.Windows.Forms.RadioButton();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            pictureBox4 = new System.Windows.Forms.PictureBox();
            pictureBox5 = new System.Windows.Forms.PictureBox();
            pictureBox6 = new System.Windows.Forms.PictureBox();
            pictureBox7 = new System.Windows.Forms.PictureBox();
            tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            CustomDurationLBL = new System.Windows.Forms.Label();
            durationEntryFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            hoursTextBox = new System.Windows.Forms.TextBox();
            hoursLabel = new System.Windows.Forms.Label();
            minutesTextBox = new System.Windows.Forms.TextBox();
            minutesLabel = new System.Windows.Forms.Label();
            addCustomDurationBTN = new System.Windows.Forms.Button();
            tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            tooltipFormatLBL = new System.Windows.Forms.Label();
            generalTooltipRDBTN = new System.Windows.Forms.RadioButton();
            specificTooltipRDBTN = new System.Windows.Forms.RadioButton();
            okBtn = new System.Windows.Forms.Button();
            cancelBtn = new System.Windows.Forms.Button();
            tooltipProvider = new System.Windows.Forms.ToolTip(components);
            scrollablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            durationEntryFlowPanel.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // scrollablePanel
            // 
            scrollablePanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            scrollablePanel.AutoScroll = true;
            scrollablePanel.Controls.Add(label1);
            scrollablePanel.Controls.Add(label2);
            scrollablePanel.Controls.Add(pictureBox1);
            scrollablePanel.Controls.Add(tableLayoutPanel1);
            scrollablePanel.Location = new System.Drawing.Point(0, 0);
            scrollablePanel.Name = "scrollablePanel";
            scrollablePanel.Size = new System.Drawing.Size(545, 685);
            scrollablePanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(127, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(350, 80);
            label1.TabIndex = 0;
            label1.Text = "Caffeinated is now running. You can find its icon in the notification area near the clock. Click it to keep your PC awake by disabling automatic sleep. Click the icon again to allow automatic sleep.";
            // 
            // label2
            // 
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            label2.Location = new System.Drawing.Point(127, 101);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(284, 41);
            label2.TabIndex = 1;
            label2.Text = "Right-click the notification area icon to show the Caffeinated menu.";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Caffeine_Black_96;
            pictureBox1.Location = new System.Drawing.Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(96, 96);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 5);
            tableLayoutPanel1.Controls.Add(SettingsAtLaunchChkBox, 0, 2);
            tableLayoutPanel1.Controls.Add(ActivateChkBox, 0, 1);
            tableLayoutPanel1.Controls.Add(StartupChkBox, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 7);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 6);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel5, 0, 3);
            tableLayoutPanel1.Location = new System.Drawing.Point(130, 134);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            tableLayoutPanel1.Size = new System.Drawing.Size(278, 511);
            tableLayoutPanel1.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label3, 0, 0);
            tableLayoutPanel2.Controls.Add(DurationsListView, 0, 1);
            tableLayoutPanel2.Location = new System.Drawing.Point(2, 119);
            tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel2.Size = new System.Drawing.Size(174, 96);
            tableLayoutPanel2.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(96, 15);
            label3.TabIndex = 9;
            label3.Text = "Default duration:";
            // 
            // DurationsListView
            // 
            DurationsListView.FullRowSelect = true;
            DurationsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            DurationsListView.Location = new System.Drawing.Point(3, 18);
            DurationsListView.MultiSelect = false;
            DurationsListView.Name = "DurationsListView";
            DurationsListView.OwnerDraw = true;
            DurationsListView.Scrollable = false;
            DurationsListView.Size = new System.Drawing.Size(168, 75);
            DurationsListView.TabIndex = 8;
            DurationsListView.UseCompatibleStateImageBehavior = false;
            DurationsListView.View = System.Windows.Forms.View.Details;
            DurationsListView.DrawColumnHeader += DurationsListView_DrawColumnHeader;
            DurationsListView.DrawItem += DurationsListView_DrawItem;
            DurationsListView.DrawSubItem += DurationsListView_DrawSubItem;
            DurationsListView.MouseClick += DurationsListView_MouseClick;
            // 
            // SettingsAtLaunchChkBox
            // 
            SettingsAtLaunchChkBox.AutoSize = true;
            SettingsAtLaunchChkBox.Location = new System.Drawing.Point(3, 53);
            SettingsAtLaunchChkBox.Name = "SettingsAtLaunchChkBox";
            SettingsAtLaunchChkBox.Size = new System.Drawing.Size(236, 19);
            SettingsAtLaunchChkBox.TabIndex = 5;
            SettingsAtLaunchChkBox.Text = "Show this settings window upon launch";
            SettingsAtLaunchChkBox.UseVisualStyleBackColor = true;
            SettingsAtLaunchChkBox.CheckedChanged += SettingsAtLaunchChkBox_CheckedChanged;
            // 
            // ActivateChkBox
            // 
            ActivateChkBox.AutoSize = true;
            ActivateChkBox.Location = new System.Drawing.Point(3, 28);
            ActivateChkBox.Name = "ActivateChkBox";
            ActivateChkBox.Size = new System.Drawing.Size(139, 19);
            ActivateChkBox.TabIndex = 4;
            ActivateChkBox.Text = "Activate upon launch";
            ActivateChkBox.UseVisualStyleBackColor = true;
            ActivateChkBox.CheckedChanged += ActivateChkBox_CheckedChanged;
            // 
            // StartupChkBox
            // 
            StartupChkBox.AutoSize = true;
            StartupChkBox.Location = new System.Drawing.Point(3, 3);
            StartupChkBox.Name = "StartupChkBox";
            StartupChkBox.Size = new System.Drawing.Size(244, 19);
            StartupChkBox.TabIndex = 3;
            StartupChkBox.Text = "Automatically launch at Windows startup";
            StartupChkBox.UseVisualStyleBackColor = true;
            StartupChkBox.CheckedChanged += StartupChkBox_CheckedChanged;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel3.Controls.Add(OffIconLbl, 1, 0);
            tableLayoutPanel3.Controls.Add(OnIconLbl, 2, 0);
            tableLayoutPanel3.Controls.Add(defaultRDBTN, 0, 1);
            tableLayoutPanel3.Controls.Add(eyeZZZRDBTN, 0, 2);
            tableLayoutPanel3.Controls.Add(mugRDBTN, 0, 3);
            tableLayoutPanel3.Controls.Add(pictureBox2, 1, 1);
            tableLayoutPanel3.Controls.Add(pictureBox3, 2, 1);
            tableLayoutPanel3.Controls.Add(pictureBox4, 1, 2);
            tableLayoutPanel3.Controls.Add(pictureBox5, 2, 2);
            tableLayoutPanel3.Controls.Add(pictureBox6, 1, 3);
            tableLayoutPanel3.Controls.Add(pictureBox7, 2, 3);
            tableLayoutPanel3.Location = new System.Drawing.Point(2, 273);
            tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 4;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            tableLayoutPanel3.Size = new System.Drawing.Size(274, 217);
            tableLayoutPanel3.TabIndex = 14;
            // 
            // OffIconLbl
            // 
            OffIconLbl.AutoSize = true;
            OffIconLbl.Location = new System.Drawing.Point(107, 0);
            OffIconLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            OffIconLbl.Name = "OffIconLbl";
            OffIconLbl.Size = new System.Drawing.Size(50, 15);
            OffIconLbl.TabIndex = 0;
            OffIconLbl.Text = "Off Icon";
            // 
            // OnIconLbl
            // 
            OnIconLbl.AutoSize = true;
            OnIconLbl.Location = new System.Drawing.Point(177, 0);
            OnIconLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            OnIconLbl.Name = "OnIconLbl";
            OnIconLbl.Size = new System.Drawing.Size(49, 15);
            OnIconLbl.TabIndex = 1;
            OnIconLbl.Text = "On Icon";
            // 
            // defaultRDBTN
            // 
            defaultRDBTN.AutoSize = true;
            defaultRDBTN.Location = new System.Drawing.Point(2, 17);
            defaultRDBTN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            defaultRDBTN.Name = "defaultRDBTN";
            defaultRDBTN.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            defaultRDBTN.Size = new System.Drawing.Size(63, 34);
            defaultRDBTN.TabIndex = 2;
            defaultRDBTN.TabStop = true;
            defaultRDBTN.Text = "Default";
            defaultRDBTN.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            defaultRDBTN.UseVisualStyleBackColor = true;
            defaultRDBTN.Click += defaultRDBTN_Click;
            // 
            // eyeZZZRDBTN
            // 
            eyeZZZRDBTN.AutoSize = true;
            eyeZZZRDBTN.Location = new System.Drawing.Point(2, 83);
            eyeZZZRDBTN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            eyeZZZRDBTN.Name = "eyeZZZRDBTN";
            eyeZZZRDBTN.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            eyeZZZRDBTN.Size = new System.Drawing.Size(91, 34);
            eyeZZZRDBTN.TabIndex = 3;
            eyeZZZRDBTN.TabStop = true;
            eyeZZZRDBTN.Text = "Eye with zZZ";
            eyeZZZRDBTN.UseVisualStyleBackColor = true;
            eyeZZZRDBTN.Click += eyeZZZRDBTN_Click;
            // 
            // mugRDBTN
            // 
            mugRDBTN.AutoSize = true;
            mugRDBTN.Location = new System.Drawing.Point(2, 149);
            mugRDBTN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            mugRDBTN.Name = "mugRDBTN";
            mugRDBTN.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            mugRDBTN.Size = new System.Drawing.Size(50, 34);
            mugRDBTN.TabIndex = 4;
            mugRDBTN.TabStop = true;
            mugRDBTN.Text = "Mug";
            mugRDBTN.UseVisualStyleBackColor = true;
            mugRDBTN.Click += mugRDBTN_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.SleepEye_Black;
            pictureBox2.Location = new System.Drawing.Point(112, 21);
            pictureBox2.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(56, 48);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Caffeine_Black_512;
            pictureBox3.Location = new System.Drawing.Point(182, 21);
            pictureBox3.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new System.Drawing.Size(56, 48);
            pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.Eye_zzz_Sleep_Black;
            pictureBox4.Location = new System.Drawing.Point(112, 87);
            pictureBox4.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new System.Drawing.Size(56, 48);
            pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 7;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.Eye_zzz_Active_Black;
            pictureBox5.Location = new System.Drawing.Point(182, 87);
            pictureBox5.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new System.Drawing.Size(56, 48);
            pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 8;
            pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (System.Drawing.Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new System.Drawing.Point(112, 153);
            pictureBox6.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            pictureBox6.Size = new System.Drawing.Size(56, 48);
            pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 9;
            pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = (System.Drawing.Image)resources.GetObject("pictureBox7.Image");
            pictureBox7.Location = new System.Drawing.Point(182, 153);
            pictureBox7.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new System.Drawing.Size(56, 48);
            pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 10;
            pictureBox7.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(CustomDurationLBL, 0, 0);
            tableLayoutPanel4.Controls.Add(durationEntryFlowPanel, 1, 0);
            tableLayoutPanel4.Controls.Add(addCustomDurationBTN, 1, 1);
            tableLayoutPanel4.Location = new System.Drawing.Point(2, 219);
            tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel4.Size = new System.Drawing.Size(274, 50);
            tableLayoutPanel4.TabIndex = 15;
            // 
            // CustomDurationLBL
            // 
            CustomDurationLBL.AutoSize = true;
            CustomDurationLBL.Dock = System.Windows.Forms.DockStyle.Top;
            CustomDurationLBL.Location = new System.Drawing.Point(2, 0);
            CustomDurationLBL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            CustomDurationLBL.Name = "CustomDurationLBL";
            tableLayoutPanel4.SetRowSpan(CustomDurationLBL, 2);
            CustomDurationLBL.Size = new System.Drawing.Size(136, 15);
            CustomDurationLBL.TabIndex = 0;
            CustomDurationLBL.Text = "Custom duration:";
            // 
            // durationEntryFlowPanel
            // 
            durationEntryFlowPanel.AutoSize = true;
            durationEntryFlowPanel.Controls.Add(hoursTextBox);
            durationEntryFlowPanel.Controls.Add(hoursLabel);
            durationEntryFlowPanel.Controls.Add(minutesTextBox);
            durationEntryFlowPanel.Controls.Add(minutesLabel);
            durationEntryFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            durationEntryFlowPanel.Location = new System.Drawing.Point(142, 2);
            durationEntryFlowPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            durationEntryFlowPanel.Name = "durationEntryFlowPanel";
            durationEntryFlowPanel.Size = new System.Drawing.Size(130, 27);
            durationEntryFlowPanel.TabIndex = 1;
            durationEntryFlowPanel.WrapContents = false;
            // 
            // hoursTextBox
            // 
            hoursTextBox.Location = new System.Drawing.Point(0, 2);
            hoursTextBox.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            hoursTextBox.Name = "hoursTextBox";
            hoursTextBox.Size = new System.Drawing.Size(34, 23);
            hoursTextBox.TabIndex = 0;
            hoursTextBox.Text = "0";
            // 
            // hoursLabel
            // 
            hoursLabel.AutoSize = true;
            hoursLabel.Location = new System.Drawing.Point(36, 0);
            hoursLabel.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            hoursLabel.Name = "hoursLabel";
            hoursLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            hoursLabel.Size = new System.Drawing.Size(14, 20);
            hoursLabel.TabIndex = 1;
            hoursLabel.Text = "h";
            // 
            // minutesTextBox
            // 
            minutesTextBox.Location = new System.Drawing.Point(56, 2);
            minutesTextBox.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            minutesTextBox.Name = "minutesTextBox";
            minutesTextBox.Size = new System.Drawing.Size(34, 23);
            minutesTextBox.TabIndex = 2;
            minutesTextBox.Text = "0";
            // 
            // minutesLabel
            // 
            minutesLabel.AutoSize = true;
            minutesLabel.Location = new System.Drawing.Point(92, 0);
            minutesLabel.Margin = new System.Windows.Forms.Padding(0);
            minutesLabel.Name = "minutesLabel";
            minutesLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            minutesLabel.Size = new System.Drawing.Size(28, 20);
            minutesLabel.TabIndex = 3;
            minutesLabel.Text = "min";
            // 
            // addCustomDurationBTN
            // 
            addCustomDurationBTN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            addCustomDurationBTN.Location = new System.Drawing.Point(142, 33);
            addCustomDurationBTN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            addCustomDurationBTN.Name = "addCustomDurationBTN";
            addCustomDurationBTN.Size = new System.Drawing.Size(46, 19);
            addCustomDurationBTN.TabIndex = 2;
            addCustomDurationBTN.Text = "Add";
            addCustomDurationBTN.UseVisualStyleBackColor = true;
            addCustomDurationBTN.Click += addCustomDurationBTN_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel5.Controls.Add(tooltipFormatLBL, 0, 0);
            tableLayoutPanel5.Controls.Add(generalTooltipRDBTN, 1, 0);
            tableLayoutPanel5.Controls.Add(specificTooltipRDBTN, 2, 0);
            tableLayoutPanel5.Location = new System.Drawing.Point(2, 77);
            tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel5.Size = new System.Drawing.Size(274, 26);
            tableLayoutPanel5.TabIndex = 16;
            // 
            // tooltipFormatLBL
            // 
            tooltipFormatLBL.AutoSize = true;
            tooltipFormatLBL.Location = new System.Drawing.Point(2, 0);
            tooltipFormatLBL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            tooltipFormatLBL.Name = "tooltipFormatLBL";
            tooltipFormatLBL.Size = new System.Drawing.Size(86, 15);
            tooltipFormatLBL.TabIndex = 0;
            tooltipFormatLBL.Text = "Tooltip format:";
            // 
            // generalTooltipRDBTN
            // 
            generalTooltipRDBTN.AutoSize = true;
            generalTooltipRDBTN.Location = new System.Drawing.Point(107, 2);
            generalTooltipRDBTN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            generalTooltipRDBTN.Name = "generalTooltipRDBTN";
            generalTooltipRDBTN.Size = new System.Drawing.Size(65, 19);
            generalTooltipRDBTN.TabIndex = 1;
            generalTooltipRDBTN.TabStop = true;
            generalTooltipRDBTN.Text = "General";
            tooltipProvider.SetToolTip(generalTooltipRDBTN, "Example: \"No sleep for about 3 hours\"");
            generalTooltipRDBTN.UseVisualStyleBackColor = true;
            generalTooltipRDBTN.Click += generalTooltipRDBTN_Click;
            // 
            // specificTooltipRDBTN
            // 
            specificTooltipRDBTN.AutoSize = true;
            specificTooltipRDBTN.Location = new System.Drawing.Point(176, 2);
            specificTooltipRDBTN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            specificTooltipRDBTN.Name = "specificTooltipRDBTN";
            specificTooltipRDBTN.Size = new System.Drawing.Size(66, 19);
            specificTooltipRDBTN.TabIndex = 2;
            specificTooltipRDBTN.TabStop = true;
            specificTooltipRDBTN.Text = "Specific";
            tooltipProvider.SetToolTip(specificTooltipRDBTN, "Example: \"No sleep for 2 hours and 47 minutes\"");
            specificTooltipRDBTN.UseVisualStyleBackColor = true;
            specificTooltipRDBTN.Click += specificTooltipRDBTN_Click;
            // 
            // okBtn
            // 
            okBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            okBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            okBtn.Location = new System.Drawing.Point(351, 721);
            okBtn.Name = "okBtn";
            okBtn.Size = new System.Drawing.Size(87, 27);
            okBtn.TabIndex = 8;
            okBtn.Text = "Ok";
            okBtn.UseVisualStyleBackColor = false;
            okBtn.Click += okBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            cancelBtn.Location = new System.Drawing.Point(444, 721);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new System.Drawing.Size(87, 27);
            cancelBtn.TabIndex = 9;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = false;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // SettingsForm
            // 
            AcceptButton = okBtn;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelBtn;
            ClientSize = new System.Drawing.Size(545, 756);
            Controls.Add(cancelBtn);
            Controls.Add(okBtn);
            Controls.Add(scrollablePanel);
            ForeColor = System.Drawing.SystemColors.ControlText;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(386, 257);
            Name = "SettingsForm";
            Text = "Welcome to Caffeinated";
            scrollablePanel.ResumeLayout(false);
            scrollablePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            durationEntryFlowPanel.ResumeLayout(false);
            durationEntryFlowPanel.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel scrollablePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView DurationsListView;
        private System.Windows.Forms.CheckBox SettingsAtLaunchChkBox;
        private System.Windows.Forms.CheckBox ActivateChkBox;
        private System.Windows.Forms.CheckBox StartupChkBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label OffIconLbl;
        private System.Windows.Forms.Label OnIconLbl;
        private System.Windows.Forms.RadioButton defaultRDBTN;
        private System.Windows.Forms.RadioButton eyeZZZRDBTN;
        private System.Windows.Forms.RadioButton mugRDBTN;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label CustomDurationLBL;
        private System.Windows.Forms.FlowLayoutPanel durationEntryFlowPanel;
        private System.Windows.Forms.TextBox hoursTextBox;
        private System.Windows.Forms.Label hoursLabel;
        private System.Windows.Forms.TextBox minutesTextBox;
        private System.Windows.Forms.Label minutesLabel;
        private System.Windows.Forms.Button addCustomDurationBTN;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label tooltipFormatLBL;
        private System.Windows.Forms.RadioButton generalTooltipRDBTN;
        private System.Windows.Forms.RadioButton specificTooltipRDBTN;
        private System.Windows.Forms.ToolTip tooltipProvider;
    }
}
