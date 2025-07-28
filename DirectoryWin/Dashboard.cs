using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Xceed.Words.NET;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace DirectoryWin
{
    public partial class Dashboard : Form
    {
        private string selectedFolder = "";

        public Dashboard()
        {
            InitializeComponent();

            btnSelectFolder.Text = "Select Folder";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click; //  Connect event

            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click; //  Connect event

            lstResults.DoubleClick += lstResults_DoubleClick;


        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedFolder = dialog.SelectedPath;
                lblFolderPath.Text = selectedFolder;
                lblTargetLocationSelct.Visible = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedFolder) || string.IsNullOrWhiteSpace(txtKeyword.Text))
            {
                MessageBox.Show("Select a folder and enter a keyword.");
                return;
            }

            string keyword = txtKeyword.Text.ToLower();
            var files = Directory.GetFiles(selectedFolder, "*.*", SearchOption.AllDirectories)
                                 .Where(f => f.EndsWith(".pdf") || f.EndsWith(".docx") || f.EndsWith(".txt"))
                                 .ToList();

            lstResults.Items.Clear();

            foreach (var file in files)
            {
                try
                {
                    string content = ReadContent(file);
                    if (content != null && content.ToLower().Contains(keyword))
                    {
                        lstResults.Items.Add(file);
                    }
                }
                catch (Exception ex)
                {
                    // You can log the error
                    Console.WriteLine($"Error reading {file}: {ex.Message}");
                }
            }

            if (lstResults.Items.Count == 0)
            {
                lstResults.Items.Add("No files found with that keyword.");
            }
        }

        private string ReadContent(string filePath)
        {
            if (filePath.EndsWith(".txt"))
            {
                return File.ReadAllText(filePath);
            }
            else if (filePath.EndsWith(".docx"))
            {
                using var doc = DocX.Load(filePath);
                return doc.Text;
            }
            else if (filePath.EndsWith(".pdf"))
            {
                using var pdf = PdfDocument.Open(filePath);
                return string.Join(" ", pdf.GetPages().Select(p => p.Text));
            }
            return null;
        }

        private void lstResults_DoubleClick(object sender, EventArgs e)
        {
            if (lstResults.SelectedItem == null) return;

            string filePath = lstResults.SelectedItem.ToString();

            if (File.Exists(filePath))
            {
                try
                {
                    var process = new System.Diagnostics.Process();
                    process.StartInfo = new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    };
                    process.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not open the file: " + ex.Message);
                }
            }
        }

    }
}
