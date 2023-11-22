using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// ... (existing code)

namespace fileanalyzer
{
    public partial class Form1 : Form
    {

        // Declare a variable to hold the total number of files found
        private int totalFilesFound;
        private ListViewColumnSorter lvwColumnSorter;
        private string pathToSearch;
        public Form1()
        {
            InitializeComponent();
            analyze.Click += analyze_click; // Wiring up the Click event to the analyze_click method

            // progressBar.Value += ProgressBar_ValueChanged;

        }

        private void openFolderBrowser(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pathToSearch = dialog.SelectedPath;
                pathTextBox.Text = pathToSearch;

            }
        }

        private void analyze_click(object sender, EventArgs e)
        {

            if (Directory.Exists(pathToSearch))
            {
                var selectedTypes = new List<string>();
                if (imageCheckBox.Checked)
                    selectedTypes.Add("*.jpeg"); // Add other image file extensions as needed
                                                 //   selectedTypes.Add("*.jpg"); // Add other image file extensions as needed
                                                 //   selectedTypes.Add("*.png"); // Add other image file extensions as needed

                if (videoCheckBox.Checked)
                    selectedTypes.Add("*.mp4"); // Add other video file extensions as needed
                                                // Add more file types based on checkboxes

                // var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                //                       .Where(file => selectedTypes.Any(type => file.EndsWith(type, StringComparison.OrdinalIgnoreCase)))
                //                      .ToList();

                //  string[] selectedTypesArray = { ".jpg", ".png" }; // Define the specific file types you want to search for
                var files = Directory.GetFiles(pathToSearch, "*.*", SearchOption.AllDirectories)
                                     //.Where(file => selectedTypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                                     .ToList();

                int totalFiles = files.Count;

                if (totalFiles > 1)
                {
                    filesFound.Text = "" + totalFiles;
                }
                else { MessageBox.Show("Nothing Found"); }

                if (largeFilesRadioButton.Checked)
                {
                    files = files.Where(file => new FileInfo(file).Length > 250 * 1024 * 1024).ToList();
                }
                else if (smallFilesRadioButton.Checked)
                {
                    files = files.Where(file => new FileInfo(file).Length <= 250 * 1024 * 1024).ToList();
                }

                DisplayFilesInListView(files);
            }
            else
            {
                MessageBox.Show("The specified path does not exist.");
            }
        }

        public Dictionary<string, List<string>> FindDuplicates(string directoryPath)
        {
            var filesByHash = new Dictionary<string, List<string>>();

            if (Directory.Exists(directoryPath))
            {
                foreach (var filePath in Directory.GetFiles(directoryPath))
                {
                    string fileHash = CalculateMD5(filePath);

                    if (!filesByHash.ContainsKey(fileHash))
                    {
                        filesByHash[fileHash] = new List<string>();
                    }

                    filesByHash[fileHash].Add(filePath);
                }
            }
            else
            {
                Console.WriteLine("Directory not found.");
            }

            return FilterDuplicates(filesByHash);
        }

        private string CalculateMD5(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private Dictionary<string, List<string>> FilterDuplicates(Dictionary<string, List<string>> filesByHash)
        {
            var duplicates = new Dictionary<string, List<string>>();

            foreach (var entry in filesByHash)
            {
                if (entry.Value.Count > 1)
                {
                    duplicates[entry.Key] = entry.Value;
                }
            }

            return duplicates;
        }

        private void duplicatesInListView(Dictionary<string, List<string>> duplicates)
        {
            // Ensure the ListView is set up correctly
            SetupListView();

            ImageList imageList = new ImageList();
            imageList.ImageSize = new System.Drawing.Size(64, 64);

            foreach (var duplicateGroup in duplicates.Values)
            {
                foreach (string file in duplicateGroup)
                {
                    // Extract thumbnails or use default icons
                    Icon icon = Icon.ExtractAssociatedIcon(file); // Extract the icon from the file
                    imageList.Images.Add(icon.ToBitmap()); // Add the icon as a bitmap to the image list

                    // Get file information for metadata
                    FileInfo fileInfo = new FileInfo(file);

                    string fileName = Path.GetFileName(file);
                    string fileSize = GetFormattedSize(fileInfo.Length); // Get formatted file size
                    string lastModified = fileInfo.LastWriteTime.ToString(); // Last modified date/time
                    string filePath = fileInfo.DirectoryName; // Get file path

                    ListViewItem item = new ListViewItem(fileName);
                    item.ImageIndex = imageList.Images.Count - 1; // Set the appropriate image index

                    // Add additional metadata as subitems
                    item.SubItems.Add(fileSize); // File size
                    item.SubItems.Add(lastModified); // Last modified date/time
                    item.SubItems.Add(filePath); // File path
                    item.Checked = false;

                    listView1.CheckBoxes = true;
                    listView1.Items.Add(item);
                }
            }

            listView1.LargeImageList = imageList;
        }

        private void DisplayDuplicatesInListView(object sender, EventArgs e) {
            // Call the FindDuplicates method to get the duplicates dictionary
            Dictionary<string, List<string>> duplicates = FindDuplicates(pathToSearch);

            // Display the duplicates in the ListView using DisplayDuplicatesInListView
            duplicatesInListView(duplicates);

        }
        private void SetupListView()
        {
            // Clear existing columns and items
            listView1.Columns.Clear();
            listView1.Items.Clear();

            // Set the view mode to Details
            listView1.View = View.Details;

            // Add columns for file name, size, last modified, and file path
            listView1.Columns.Add("File Name", 200); // Adjust column widths as needed
            listView1.Columns.Add("Size", 100);
            listView1.Columns.Add("Last Modified", 150);
            listView1.Columns.Add("File Path", 400); // Adjust column widths as needed

            // Allow columns to be sortable
            listView1.Columns[0].Tag = "string";
            listView1.Columns[1].Tag = "numeric";
            listView1.Columns[2].Tag = "datetime";
            listView1.Columns[3].Tag = "string";
            listView1.ListViewItemSorter = new ListViewColumnSorter();

            lvwColumnSorter = new ListViewColumnSorter();
            listView1.ListViewItemSorter = lvwColumnSorter;

            // Handle the ColumnClick event
            listView1.ColumnClick += ListView_ColumnClick;
        }

        private void DisplayFilesInListView(List<string> files)
        {
            // Ensure the ListView is set up correctly
            SetupListView();

            ImageList imageList = new ImageList();
            imageList.ImageSize = new System.Drawing.Size(64, 64);

            // Calculate the total number of files
            int totalFiles = files.Count;

            // Set the progress bar range and reset its value
            progressBar.Minimum = 0;
            progressBar.Maximum = totalFiles;
            progressBar.Value = 0;

            foreach (string file in files)
            {
                // Extract thumbnails or use default icons
                Icon icon = Icon.ExtractAssociatedIcon(file); // Extract the icon from the file
                imageList.Images.Add(icon.ToBitmap()); // Add the icon as a bitmap to the image list

                // Get file information for metadata
                FileInfo fileInfo = new FileInfo(file);

                string fileName = Path.GetFileName(file);
                string fileSize = GetFormattedSize(fileInfo.Length); // Get formatted file size
                string lastModified = fileInfo.LastWriteTime.ToString(); // Last modified date/time
                string filePath = fileInfo.DirectoryName; // Get file path

                ListViewItem item = new ListViewItem(fileName);
                item.ImageIndex = imageList.Images.Count - 1; // Set the appropriate image index

                // Add additional metadata as subitems
                item.SubItems.Add(fileSize); // File size
                item.SubItems.Add(lastModified); // Last modified date/time
                item.SubItems.Add(filePath); // File path

                item.Checked = false;


                // Allow the user to rearrange columns.
                listView1.AllowColumnReorder = true;
                // Display check boxes.
                listView1.CheckBoxes = true;
                // Select the item and subitems when selection is made.
                listView1.FullRowSelect = true;
                // Display grid lines.
                listView1.GridLines = true;
                // Sort the items in the list in ascending order.
                listView1.Sorting = SortOrder.Ascending;

                listView1.Items.Add(item);

                // Update progress bar value incrementally
                progressBar.Value++;

                // Ensure the value doesn't exceed the maximum
                if (progressBar.Value >= progressBar.Maximum)
                {
                    progressBar.Value = progressBar.Maximum - 1;
                }

                if (progressBar.Value >= progressBar.Minimum)
                {
                    progressLabel.Text = progressBar.Value.ToString();
                }
            }
            listView1.LargeImageList = imageList;
        }

        // Method to format file size in MB or GB
        private string GetFormattedSize(long sizeInBytes)
        {
            const long megaByte = 1024 * 1024;
            const long gigaByte = 1024 * 1024 * 1024;

            if (sizeInBytes >= gigaByte)
            {
                double sizeInGB = (double)sizeInBytes / gigaByte;
                return $"{sizeInGB:F2} GB";
            }
            else
            {
                double sizeInMB = (double)sizeInBytes / megaByte;
                return $"{sizeInMB:F2} MB";
            }
        }


        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            // Check if an item is selected and the click is a left click
            if (e.Button == MouseButtons.Left && listView1.SelectedItems.Count > 0)
            {
                // Get the selected item
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // Access the file path from the first subitem
                string filePath = selectedItem.Text;

                // Open the file using the default associated program
                if (File.Exists(filePath))
                {
                    System.Diagnostics.Process.Start(filePath);
                }
                else
                {
                    MessageBox.Show("File not found or path is invalid.");
                    // Or handle the case when the file is not found as needed
                }
            }

        }

        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                lvwColumnSorter.SortColumn = e.Column;
                listView1.Sort();
            }
            else
            {
                lvwColumnSorter.SortColumn = e.Column;
                listView1.Sorting = SortOrder.Ascending;
                listView1.Sort();
            }
        }



        private void deleteSelected_Click(object sender, EventArgs e)
        {
            int filesToDeleteCount = 0;

            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Checked)
                {
                    filesToDeleteCount++;
                }
            }

            if (filesToDeleteCount == 0)
            {
                MessageBox.Show("No files selected for deletion.");
                return;
            }

            string confirmationMessage = $"Are you sure you want to delete {filesToDeleteCount} file(s)?";

            // Show confirmation dialog
            DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                for (int i = listView1.Items.Count - 1; i >= 0; i--)
                {
                    if (listView1.Items[i].Checked)
                    {
                        string filePath = string.Empty;
                        if (listView1.Items[i].SubItems.Count > 0) // Check if the index is valid
                        {
                            filePath = listView1.Items[i].SubItems[3].Text + "\\" + listView1.Items[i].Text; // Adjust index according to your setup
                        }

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                            listView1.Items[i].Remove();
                        }
                        else
                        {
                            MessageBox.Show("Path not found: " + filePath);
                        }
                    }
                }
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {
            // Handle the Enter event for groupBox6 here
            // Add your logic or functionality for this event
        }


    }
}