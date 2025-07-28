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

            string keyword = txtKeyword.Text;
            var files = Directory.GetFiles(selectedFolder, "*.*", SearchOption.AllDirectories)
                                 .Where(f => f.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)
                                          || f.EndsWith(".docx", StringComparison.OrdinalIgnoreCase)
                                          || f.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                                 .ToList();

            if (files.Count == 0)
            {
                MessageBox.Show("No supported files found in the selected folder.");
                return;
            }

            lstResults.Items.Clear();

            string pythonScriptPath = "semantic_search.py";

            // Save the list of files into a temp file
            string tempFileListPath = Path.GetTempFileName();
            File.WriteAllLines(tempFileListPath, files);

            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{pythonScriptPath}\" \"{keyword}\" \"{tempFileListPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,  // <-- add this
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using var process = System.Diagnostics.Process.Start(psi);

                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();  // <-- read errors
                process.WaitForExit();

                //if (!string.IsNullOrEmpty(errors))
                //{
                //    MessageBox.Show("Python errors: " + errors);
                //    return;
                //}

                if (string.IsNullOrWhiteSpace(output))
                {
                    lstResults.Items.Add("No semantic matches found.");
                    return;
                }

                var lines = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var parts = line.Split("|||");
                    if (parts.Length == 2)
                    {
                        lstResults.Items.Add($"{parts[0]} (score: {double.Parse(parts[1]):0.00})");
                    }
                }

                if (lstResults.Items.Count == 0)
                {
                    lstResults.Items.Add("No files found with that keyword.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Semantic search failed: " + ex.Message);
            }
        }


        //private string ReadContent(string filePath)
        //{
        //    if (filePath.EndsWith(".txt"))
        //    {
        //        return File.ReadAllText(filePath);
        //    }
        //    else if (filePath.EndsWith(".docx"))
        //    {
        //        using var doc = DocX.Load(filePath);
        //        return doc.Text;
        //    }
        //    else if (filePath.EndsWith(".pdf"))
        //    {
        //        using var pdf = PdfDocument.Open(filePath);
        //        return string.Join(" ", pdf.GetPages().Select(p => p.Text));
        //    }
        //    return null;
        //}

        private void lstResults_DoubleClick(object sender, EventArgs e)
        {
            if (lstResults.SelectedItem == null) return;

            string selectedItem = lstResults.SelectedItem.ToString();
            string filePath = selectedItem.Split(" (score:")[0]; // Extract path

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
