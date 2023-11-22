using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

        public Form1()
        {
            InitializeComponent();
            analyze.Click += analyze_click; // Wiring up the Click event to the analyze_click method
        }

        private void analyze_click(object sender, EventArgs e)
        {
            string path = pathTextBox.Text;

            if (Directory.Exists(path))
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
                var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                     //.Where(file => selectedTypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                                     .ToList();

                int totalFiles = files.Count;

                if (totalFiles > 1)
                {
                    filesFound.Text = ""+totalFiles;
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

        private void DisplayFilesInListView(List<string> files)
        {
            listView1.Items.Clear();
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
                ListViewItem item = new ListViewItem(fileName);
                item.ImageIndex = imageList.Images.Count - 1; // Set the appropriate image index

                // Add additional metadata as subitems
                item.SubItems.Add(fileInfo.Length.ToString()); // File size
                item.SubItems.Add(fileInfo.LastWriteTime.ToString()); // Last modified date/time

                listView1.Items.Add(item);

                // Update progress bar value incrementally
                progressBar.Value++;

                // Ensure the value doesn't exceed the maximum
                if (progressBar.Value >= progressBar.Maximum)
                {
                    progressBar.Value = progressBar.Maximum - 1;
                }
            }
            listView1.LargeImageList = imageList;
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




    }
}

