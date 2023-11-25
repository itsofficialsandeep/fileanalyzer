using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

// ... (existing code)

namespace fileanalyzer
{
    public partial class Form3 : Form
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        [Flags]
        public enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001, // No confirmation
            SHERB_NOPROGRESSUI = 0x00000001,   // No progress tracking window
            SHERB_NOSOUND = 0x00000004         // No sound is played when the operation is complete
        }

        // Declare a variable to hold the total number of files found
        private ListViewColumnSorter lvwColumnSorter;
        private string pathToSearch;
        string searchPattern = "*.*";

        public Form3()
        {
            InitializeComponent();

            analyze.Click += analyze_click; // Wiring up the Click event to the analyze_click method
            clearRecycleBinButton.Click += clearRecycleBin;

            // progressBar.Value += ProgressBar_ValueChanged;

            // adding icons to tab of tab control
            // initialize the imagelist
            ImageList imageList1 = new ImageList();
            imageList1.Images.Add("key1", Image.FromFile(@"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\icon.jpg"));
            imageList1.Images.Add("key2", Image.FromFile(@"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\icon.jpg"));

            //initialize the tab control
            // TabControl tabControl1 = new TabControl();
            //    mainTabControl.Dock = DockStyle.Fill;
            //    mainTabControl.ImageList = imageList1;
            //    mainTabControl.TabPages.Add("tabKey1", "TabText1", "key1"); // icon using ImageKey
            //   mainTabControl.TabPages.Add("tabKey2", "TabText2", "key2");      // icon using ImageIndex
            //    this.Controls.Add(mainTabControl);

            GenerateDriveCards();
            
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
                string imageExtensions = "";
                string videoExtensions = "";
                string audioExtensions = "";
                string zipExtensions = "";
                string docExtensions = "";
                string otherExtensions = "";
                string appExtensions = "";
                string appFilesExtensions = "";

                string extensions = "";

                var selectedTypes = new List<string>();
                if (imageCheckBox.Checked)
                {
                    imageExtensions = @".png, .jpeg, .jpg, .tiff, .tif, .bmp, .gif, .tga, .webp, .svg, 
                                               .eps, .ai, .xcf, .ico, .psd, .raw, .yuv, .ppm, .pgm, .pbm, .pnm, 
                                                .hdr, .exr, .bpg, .jxr, .heic, .heif";
                }

                if (videoCheckBox.Checked)
                {
                    videoExtensions = ".mp4, .mov, .avi, .mkv, .wmv, .flv, .webm, .m4v, .mpeg, .mpg, .3gp, .vob, .ogv, .swf, .asf, .rm, .rmvb, .m2ts, .ts, .m2v, .mts, .f4v, .divx, .xvid, .mxf, .dv, .mp2, .m1v, .gxf, .roq, .m2p, .mpeg4, .mpg2, .mpeg1, .ssif, .r3d, .bik, .smk, .m4e, .nsv, .nut, .wtv, .trp, .ifo, .wtv, .dvr-ms, .dav, .ogm, .drc, .yuv, .vc1, .avs, .mts";

                }

                if (audioCheckbox.Checked)
                {
                    audioExtensions = ".mp3, .wav, .ogg, .flac, .aac, .wma, .m4a, .opus, .alac, .aiff, .ape";

                }

                if (documentCheckbox.Checked)
                {
                    docExtensions = ".docx, .xlsx, .pptx, .pdf, .odt, .ods, .odp, .txt, .rtf, .csv";

                }

                if (zipCheckbox.Checked)
                {
                    zipExtensions = ".zip, .rar, .7z, .tar, .gz, .iso, .bz2, .xz, .pkg, .tgz";

                }

                if (appCheckbox.Checked)
                {
                    appExtensions = ".exe, .apk, .app, .deb, .msi, .dmg, .jar, .bat, .sh, .com, .cmd, .vb";

                }

                if (appFilesCheckbox.Checked)
                {
                    appFilesExtensions = ".cfg, .ini, .conf, .plist, .properties, .db, .sqlite, .mdb, .accdb, .sql, .bak, .dbf, .ttf, .otf, .woff, .woff2, .eot, .dll, .sys, .bin, .dat, .key, .mdf, .log";

                }

                if (otherCheckBox.Checked)
                {
                    otherExtensions = ".png, .jpeg, .jpg, .tiff, .tif, .bmp, .gif, .tga, .webp, .svg, .eps, .ai, .xcf, .ico, .psd, .raw, .yuv, .ppm, .pgm, .pbm, .pnm, .hdr, .exr, .bpg, .jxr, .heic, .heif, " +
                        ".mp4, .mov, .avi, .mkv, .wmv, .flv, .webm, .m4v, .mpeg, .mpg, .3gp, .vob, .ogv, .swf, .asf, .rm, .rmvb, .m2ts, .ts, .m2v, .mts, .f4v, .divx, .xvid, .mxf, .dv, .mp2, .m1v, .gxf, .roq, .m2p, .mpeg4, .mpg2, .mpeg1, .ssif, .r3d, .bik, .smk, .m4e, .nsv, .nut, .wtv, .trp, .ifo, .wtv, .dvr-ms, .dav, .ogm, .drc, .yuv, .vc1, .avs, .mts " +
                        ".mp3, .wav, .ogg, .flac, .aac, .wma, .m4a, .opus, .alac, .aiff, .ape " +
                        ".docx, .xlsx, .pptx, .pdf, .odt, .ods, .odp, .txt, .rtf, .csv " +
                        ".zip, .rar, .7z, .tar, .gz, .iso, .bz2, .xz, .pkg, .tgz " +
                        ".exe, .apk, .app, .deb, .msi, .dmg, .jar, .bat, .sh, .com, .cmd, .vb " +
                        ".cfg, .ini, .conf, .plist, .properties, .db, .sqlite, .mdb, .accdb, .sql, .bak, .dbf, .ttf, .otf, .woff, .woff2, .eot, .dll, .sys, .bin, .dat, .key, .mdf, .log ";
                }

                if (allCheckbox.Checked)
                {
                    videoCheckBox.Checked = true;
                    audioCheckbox.Checked = true;
                    appFilesCheckbox.Checked = true;
                    //otherCheckBox.Checked = true;
                    appCheckbox.Checked = true;
                    imageCheckBox.Checked = true;
                    zipCheckbox.Checked = true;
                    documentCheckbox.Checked = true;

                    searchPattern = "*.*";
                }

                extensions = imageExtensions + " " + videoExtensions + " " + audioExtensions + " " + docExtensions + " " + zipExtensions + " " + appExtensions + " " + appFilesExtensions;

                string[] words = extensions.Split(',');
                foreach (string word in words)
                {
                    string cleanedWord = word.Trim(); // Remove leading/trailing spaces
                    selectedTypes.Add(cleanedWord);
                }

                var files = new List<string>();

                if (otherCheckBox.Checked)
                {

                    extensions = otherExtensions;
                    words = extensions.Split(',');
                    foreach (string word in words)
                    {
                        string cleanedWord = word.Trim(); // Remove leading/trailing spaces
                        selectedTypes.Add(cleanedWord);
                    }

                    files = Directory.GetFiles(pathToSearch, searchPattern, SearchOption.AllDirectories)
                     .Where(file => !selectedTypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                     .ToList();
                }
                else
                {
                    files = Directory.GetFiles(pathToSearch, searchPattern, SearchOption.AllDirectories)
                     .Where(file => selectedTypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                     .ToList();
                }

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

        private void DisplayDuplicatesInListView(object sender, EventArgs e)
        {
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

        private void SelectAllFiles()
        {

            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                listView1.Items[i].Checked = true;
            }


        }

        private void DeselectAllFiles()
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                listView1.Items[i].Checked = false;
            }
        }

        private void SelectFilesInEvenOrder()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (i % 2 == 0)
                {
                    listView1.Items[i].Checked = true;
                }
            }
        }

        private void SelectFilesInOddOrder()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (i % 2 != 0)
                {
                    listView1.Items[i].Checked = true;
                }
            }
        }


        private void selectEvenOrderButton_Click(object sender, EventArgs e)
        {
            SelectFilesInEvenOrder();
        }

        private void selectOddOrderButton_Click(object sender, EventArgs e)
        {
            SelectFilesInOddOrder();
        }

        private void ToggleSelectAllFiles()
        {
            bool anySelected = listView1.CheckedItems.Count > 0;

            if (anySelected)
            {
                DeselectAllFiles();
                selectAllButton.Text = "Select All";
            }
            else
            {
                SelectAllFiles();
                selectAllButton.Text = "Deselect All";
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            ToggleSelectAllFiles();
        }


        private bool selectEven = true; // Flag to track even/odd selection

        private void evenOddButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (i % 2 == 0 && selectEven) // Select even index files
                {
                    listView1.Items[i].Checked = true;
                }
                else if (i % 2 != 0 && !selectEven) // Select odd index files
                {
                    listView1.Items[i].Checked = true;
                }
                else
                {
                    listView1.Items[i].Checked = false; // Deselect other files
                }
            }

            // Toggle the flag for the next click
            selectEven = !selectEven;
            evenOddButton.Text = selectEven ? "Select Even" : "Select Odd";
        }

        // DELETE DUPLICATE FILES
        private void deleteDuplicateFiles(object sender, EventArgs e)
        {
            // Call the FindDuplicates method to get the duplicates dictionary
            Dictionary<string, List<string>> duplicates = FindDuplicates(pathToSearch);

            // Display the duplicates in the ListView using DisplayDuplicatesInListView
            duplicatesInListView(duplicates);

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (i % 2 == 0 && selectEven) // Select even index files
                {
                    listView1.Items[i].Checked = true;
                }
                else if (i % 2 != 0 && !selectEven) // Select odd index files
                {
                    listView1.Items[i].Checked = true;
                }
                else
                {
                    listView1.Items[i].Checked = false; // Deselect other files
                }
            }

            deleteSelected_Click(sender, e);

        }

        // FIND FILES WITH EXTENSIONS
        private void findWithExtension(object sender, EventArgs e)
        {
            if (Directory.Exists(pathToSearch))
            {
                string extensions = extension.Text;

                var selectedTypes = new List<string>();

                // Split the input string by commas and remove spaces
                string[] words = extensions.Split(',');
                foreach (string word in words)
                {
                    string cleanedWord = "." + word.Trim(); // Remove leading/trailing spaces
                    selectedTypes.Add(cleanedWord);
                }

                var files = Directory.GetFiles(pathToSearch, "*.*", SearchOption.AllDirectories)
                                     .Where(file => selectedTypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                                     .ToList();

                int totalFiles = files.Count;

                if (totalFiles > 1)
                {
                    filesFound.Text = "" + totalFiles;
                }
                else { MessageBox.Show("Nothing Found"); }

                DisplayFilesInListView(files);
            }
            else
            {
                MessageBox.Show("The specified path does not exist.");
            }
        }

        private void deleteWithExtension(object sender, EventArgs e)
        {
            if (Directory.Exists(pathToSearch))
            {
                string extensions = extension.Text;

                var selectedTypes = new List<string>();

                // Split the input string by commas and remove spaces
                string[] words = extensions.Split(',');
                foreach (string word in words)
                {
                    string cleanedWord = "." + word.Trim(); // Remove leading/trailing spaces
                    selectedTypes.Add(cleanedWord);
                }

                var files = Directory.GetFiles(pathToSearch, "*.*", SearchOption.AllDirectories)
                                     .Where(file => selectedTypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                                     .ToList();

                int totalFiles = files.Count;

                if (totalFiles > 1)
                {
                    filesFound.Text = "" + totalFiles;
                }
                else { MessageBox.Show("Nothing Found"); }

                DisplayFilesInListView(files);
            }

            SelectAllFiles();
            selectAllButton.Text = "Deselect All";

            deleteSelected_Click(sender, e);

        }

        private void checkAllFilters(object sender, EventArgs e)
        {
            if (allCheckbox.Checked)
            {
                videoCheckBox.Checked = true;
                audioCheckbox.Checked = true;
                appFilesCheckbox.Checked = true;
                appCheckbox.Checked = true;
                imageCheckBox.Checked = true;
                zipCheckbox.Checked = true;
                documentCheckbox.Checked = true;

                searchPattern = "*.*";
            }
        }

        private void findOldFiles(object sender, EventArgs e)
        {
            // Assuming dateTimePicker is the name of your DateTimePicker control
            DateTime selectedDate = dateTimePicker1.Value;

            if (Directory.Exists(pathToSearch))
            {

                var files = Directory.GetFiles(pathToSearch, "*.*", SearchOption.AllDirectories)
                                     .Where(file => {
                                         FileInfo fileInfo = new FileInfo(file);
                                         // Change the condition based on whether you want to filter by creation or last modified date
                                         return fileInfo.LastWriteTime < selectedDate; // For last modified date
                                                                                       // return fileInfo.CreationTime < selectedDate; // For creation date
                                     })
                                     .ToList();

                int totalFiles = files.Count;

                if (totalFiles > 1)
                {
                    filesFound.Text = "" + totalFiles;
                }
                else { MessageBox.Show("Nothing Found.."); }

                DisplayFilesInListView(files);
            }
            else
            {
                MessageBox.Show("The specified path does not exist.");
            }
        }

        private void deleteOldFiles(object sender, EventArgs e)
        {
            // Assuming dateTimePicker is the name of your DateTimePicker control
            DateTime selectedDate = dateTimePicker1.Value;

            if (Directory.Exists(pathToSearch))
            {

                var files = Directory.GetFiles(pathToSearch, "*.*", SearchOption.AllDirectories)
                                     .Where(file => {
                                         FileInfo fileInfo = new FileInfo(file);
                                         // Change the condition based on whether you want to filter by creation or last modified date
                                         return fileInfo.LastWriteTime < selectedDate; // For last modified date
                                                                                       // return fileInfo.CreationTime < selectedDate; // For creation date
                                     })
                                     .ToList();

                int totalFiles = files.Count;

                if (totalFiles > 1)
                {
                    filesFound.Text = "" + totalFiles;
                }
                else { MessageBox.Show("Nothing Found.."); }

                DisplayFilesInListView(files);
            }

            SelectAllFiles();
            selectAllButton.Text = "Deselect All";

            deleteSelected_Click(sender, e);
        }

        private void clearRecycleBin(object sender, EventArgs e)
        {
            int result = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOCONFIRMATION);
            if (result == 0)
            {
                MessageBox.Show("Recycle bin emptied successfully.");
            }
            else
            {
                MessageBox.Show("Failed to empty the recycle bin.");
            }
        }

        private void EmptyFolder(string folderPath)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(folderPath);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    subDirectory.Delete(true);
                }

                MessageBox.Show("Folder emptied successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to empty the folder: " + ex.Message);
            }
        }

        // Call this method on a button click or as needed
        private void EmptyTempFolderButton_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Windows\Temp";
            EmptyFolder(folderPath);
        }

        private void YourForm_Load(object sender, EventArgs e)
        {
            // Set properties for groupBox1 if needed

            // Create a Panel within the current TabPage
            Panel panel1 = new Panel();
            panel1.Dock = DockStyle.Fill; // Dock the panel within the TabPage

            // Add the Panel to the TabPage
            dashboardPanel.Controls.Add(panel1);

            // Add the GroupBox to the Panel
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(groupBoxForResuts);

            // Adjust the position and size of the GroupBox within the Panel
            groupBox1.Location = new Point(0, 0); // Set the location within the panel
            groupBoxForResuts.Location = new Point(0, 255);
            //groupBox1.Size = new Size(200, 150); // Set the size of the groupBox1
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            makeCornerRound(bannerFlowPanel, 7);
            makeCornerRound(picturePanel, 7);
            makeCornerRound(musicPanel, 7);
            makeCornerRound(videoPanel, 7);
            makeCornerRound(documentPanel, 7);
            makeCornerRound(downloadPanel, 7);
            makeCornerRound(desktopPanel, 7);
            makeCornerRound(screenshotPanel, 7);
            makeCornerRound(tempPanel, 7);
            makeCornerRound(temp2Panel, 7);
            makeCornerRound(recyclebinPanel, 7);
            makeCornerRound(bigsizefileGroupBox, 7);
        }

        static void DeleteFolderContents(string folderPath)
        {
            try
            {
                foreach (string file in Directory.GetFiles(folderPath))
                {
                    File.Delete(file);
                }

                foreach (string subDir in Directory.GetDirectories(folderPath))
                {
                    DeleteFolderContents(subDir);
                    Directory.Delete(subDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void clearPictureFolder(object sender, EventArgs e)
        {
            DeleteFolderContents(@"C:\Users\"+ Environment.UserName + @"\Pictures");
        }

        private void clearVideosFolder(object sender, EventArgs e)
        {
            DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Videos");
        }

        private void clearMusicsFolder(object sender, EventArgs e)
        {
            DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Musics");
        }

        private void clearDocumentsFolder(object sender, EventArgs e)
        {
            DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Documents");
        }

        private void clearDownloadFolder(object sender, EventArgs e)
        {
            DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Downloads");
        }

        private void clearDesktopFolder(object sender, EventArgs e)
        {
            DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Destop");
        }

        private void clearScreenshotsFolder(object sender, EventArgs e)
        {
            DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Pictures\Screenshots");
        }

        private void clearTempFolder(object sender, EventArgs e)
        {
            DeleteFolderContents(@"\C:\Windows\Temp");
        }

        private void clearTemp2Folder(object sender, EventArgs e)
        {
            DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp");
        }
    }
}