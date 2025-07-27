namespace DirectoryWin
{
    partial class Form1
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
            SuspendLayout();
            // 
            // lblFolderPath
            // 
            lblFolderPath.AutoSize = true;
            lblFolderPath.Location = new Point(106, 150);
            lblFolderPath.Name = "lblFolderPath";
            lblFolderPath.Size = new Size(38, 15);
            lblFolderPath.TabIndex = 0;
            lblFolderPath.Text = "label1";
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(106, 180);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(75, 23);
            btnSelectFolder.TabIndex = 1;
            btnSelectFolder.Text = "btnSelectFolder";
            btnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(224, 180);
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
            lstResults.Location = new Point(360, 48);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(370, 244);
            lstResults.TabIndex = 3;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(106, 124);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(193, 23);
            txtKeyword.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtKeyword);
            Controls.Add(lstResults);
            Controls.Add(btnSearch);
            Controls.Add(btnSelectFolder);
            Controls.Add(lblFolderPath);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFolderPath;
        private Button btnSelectFolder;
        private Button btnSearch;
        private ListBox lstResults;
        private TextBox txtKeyword;
    }
}
