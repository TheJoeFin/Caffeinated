namespace Caffeinated {
    partial class ExceptionLogForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionLogForm));
            exceptionListView = new System.Windows.Forms.ListView();
            detailsTextBox = new System.Windows.Forms.TextBox();
            clearButton = new System.Windows.Forms.Button();
            closeButton = new System.Windows.Forms.Button();
            refreshButton = new System.Windows.Forms.Button();
            splitContainer = new System.Windows.Forms.SplitContainer();
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // exceptionListView
            // 
            exceptionListView.Dock = System.Windows.Forms.DockStyle.Fill;
            exceptionListView.FullRowSelect = true;
            exceptionListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            exceptionListView.Location = new System.Drawing.Point(0, 0);
            exceptionListView.MultiSelect = false;
            exceptionListView.Name = "exceptionListView";
            exceptionListView.Size = new System.Drawing.Size(784, 200);
            exceptionListView.TabIndex = 0;
            exceptionListView.UseCompatibleStateImageBehavior = false;
            exceptionListView.View = System.Windows.Forms.View.List;
            exceptionListView.SelectedIndexChanged += exceptionListView_SelectedIndexChanged;
            // 
            // detailsTextBox
            // 
            detailsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            detailsTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            detailsTextBox.Location = new System.Drawing.Point(0, 0);
            detailsTextBox.Multiline = true;
            detailsTextBox.Name = "detailsTextBox";
            detailsTextBox.ReadOnly = true;
            detailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            detailsTextBox.Size = new System.Drawing.Size(784, 265);
            detailsTextBox.TabIndex = 1;
            detailsTextBox.WordWrap = false;
            // 
            // clearButton
            // 
            clearButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            clearButton.Location = new System.Drawing.Point(12, 481);
            clearButton.Name = "clearButton";
            clearButton.Size = new System.Drawing.Size(87, 27);
            clearButton.TabIndex = 2;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            closeButton.Location = new System.Drawing.Point(697, 481);
            closeButton.Name = "closeButton";
            closeButton.Size = new System.Drawing.Size(87, 27);
            closeButton.TabIndex = 3;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            // 
            // refreshButton
            // 
            refreshButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            refreshButton.Location = new System.Drawing.Point(105, 481);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new System.Drawing.Size(87, 27);
            refreshButton.TabIndex = 4;
            refreshButton.Text = "Refresh";
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshButton_Click;
            // 
            // splitContainer
            // 
            splitContainer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer.Location = new System.Drawing.Point(0, 0);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(panel1);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(panel2);
            splitContainer.Size = new System.Drawing.Size(784, 469);
            splitContainer.SplitterDistance = 200;
            splitContainer.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Controls.Add(exceptionListView);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(784, 200);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(detailsTextBox);
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(784, 265);
            panel2.TabIndex = 0;
            // 
            // ExceptionLogForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 520);
            Controls.Add(splitContainer);
            Controls.Add(refreshButton);
            Controls.Add(closeButton);
            Controls.Add(clearButton);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(600, 400);
            Name = "ExceptionLogForm";
            Text = "Exception Log - Caffeinated";
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView exceptionListView;
        private System.Windows.Forms.TextBox detailsTextBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
