namespace DirectoryWin
{
    partial class Dashboard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblFolderPath = new Label();
            btnSelectFolder = new Button();
            btnSearch = new Button();
            lstResults = new ListBox();
            txtKeyword = new TextBox();
            lblTextBox = new Label();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // lblFolderPath
            // 
            lblFolderPath.AutoSize = true;
            lblFolderPath.ForeColor = Color.Red;
            lblFolderPath.Location = new Point(128, 122);
            lblFolderPath.Name = "lblFolderPath";
            lblFolderPath.Size = new Size(0, 15);
            lblFolderPath.TabIndex = 0;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(126, 89);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(122, 23);
            btnSelectFolder.TabIndex = 1;
            btnSelectFolder.Text = "Select location ";
            btnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(126, 151);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // lstResults
            // 
            lstResults.FormattingEnabled = true;
            lstResults.ItemHeight = 15;
            lstResults.Location = new Point(360, 53);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(283, 244);
            lstResults.TabIndex = 3;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(127, 60);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(193, 23);
            txtKeyword.TabIndex = 4;
            // 
            // lblTextBox
            // 
            lblTextBox.AutoSize = true;
            lblTextBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTextBox.Location = new Point(20, 64);
            lblTextBox.Name = "lblTextBox";
            lblTextBox.Size = new Size(85, 15);
            lblTextBox.TabIndex = 5;
            lblTextBox.Text = "Target Name :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(20, 121);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 6;
            label1.Text = "Target Location :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.Location = new Point(357, 9);
            label2.Name = "label2";
            label2.Size = new Size(106, 21);
            label2.TabIndex = 7;
            label2.Text = "Directory AI ";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblTextBox);
            Controls.Add(txtKeyword);
            Controls.Add(lstResults);
            Controls.Add(btnSearch);
            Controls.Add(btnSelectFolder);
            Controls.Add(lblFolderPath);
            Name = "Dashboard";
            Text = "Directory AI";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFolderPath;
        private Button btnSelectFolder;
        private Button btnSearch;
        private ListBox lstResults;
        private TextBox txtKeyword;
        private Label lblTextBox;
        private Label label1;
        private Label label2;
    }
}
