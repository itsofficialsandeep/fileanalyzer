using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;

using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static fileanalyzer.Form3;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using File = System.IO.File;
using System.Net.Http;

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


        // variable to save searched files folders list so that we dont need to serch all the directories again
        public List<string> SearchedFolders { get; set; }
        public List<string> SearchedFiles { get; set; }
        public int TotalItems { get; set; }

        string recentFilePath = @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\recentFiles.txt";

        public Form3()
        {
            InitializeComponent();

            analyze.Click += analyze_click; // Wiring up the Click event to the analyze_click method
          //  clearRecycleBinButton.Click += clearRecycleBin;

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

            SearchedFolders = new List<string>();
            SearchedFiles = new List<string>();
            TotalItems = 0;

            GenerateDriveCards();
        }

        private void openFolderBrowser(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = dialog.SelectedPath;
                pathToSearch = pathTextBox.Text;

            }
        }

        private void analyze_click(object sender, EventArgs e)
        {

           // await Task.Run( () =>
            {
                if (Directory.Exists(pathTextBox.Text))
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

                        try
                        {
                            files = Directory.GetFiles(pathToSearch, searchPattern, SearchOption.AllDirectories)
                             .Where(file => !selectedTypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                             .ToList();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            files = Directory.GetFiles(pathToSearch, searchPattern, SearchOption.AllDirectories)
                            .Where(file => selectedTypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                            .ToList();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

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
                using (var stream = System.IO.File.OpenRead(filePath))
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

        private async void duplicatesInListView(Dictionary<string, List<string>> duplicates)
        {
            await Task.Run(() => {
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
            });

        }

        private async void DisplayDuplicatesInListView(object sender, EventArgs e)
        {
            await Task.Run(() => {
                // Call the FindDuplicates method to get the duplicates dictionary
                Dictionary<string, List<string>> duplicates = FindDuplicates(pathToSearch);

                // Display the duplicates in the ListView using DisplayDuplicatesInListView
                duplicatesInListView(duplicates);
            });


        }

        private void SetupListView()
        {
            try {
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void DisplayFilesInListView(List<string> files)
        {
            int totalFiles = 0;
            //await Task.Run(() => {
                try {
                    // Ensure the ListView is set up correctly
                    SetupListView();

                    ImageList imageList = new ImageList();
                    imageList.ImageSize = new System.Drawing.Size(64, 64);

                    // Calculate the total number of files
                    totalFiles = files.Count;
                    long totalFileSize = 0;

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

                        totalFileSize += fileInfo.Length;

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

                        // Ensure the value doesn't exceed the maximum
                        if (progressBar.Value >= progressBar.Maximum)
                        {
                            progressBar.Value = progressBar.Maximum - 1;
                        }

                        if (progressBar.Value >= progressBar.Minimum)
                        {
                            progressLabel.Text = progressBar.Value.ToString();
                        }

                        // Update progress bar value incrementally
                        progressBar.Value++;
                    }
                    progressBar.Value = totalFiles;

                    progressLabel.Text = "Done";
                    listView1.LargeImageList = imageList;
                    totalFileSizeLabel.Text = ConvertBytes(totalFileSize) + "";
                }
                catch(Exception ex) {
                    progressBar.Value = totalFiles;
                    WriteExceptionToHTMLFile(ex,"exceptionLog.html"); 
                
                }

           // });
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


        private async void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            await Task.Run(() => {
                // Check if an item is selected and the click is a left click
                if (e.Button == MouseButtons.Left && listView1.SelectedItems.Count > 0)
                {
                    // Get the selected item
                    ListViewItem selectedItem = listView1.SelectedItems[0];

                    // Access the file path from the first subitem
                    string filePath = selectedItem.Text;

                    // Open the file using the default associated program
                    if (System.IO.File.Exists(filePath))
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
                    else
                    {
                        MessageBox.Show("File not found or path is invalid.");
                        // Or handle the case when the file is not found as needed
                    }
                }

            });

        }

        private async void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            await Task.Run(() => {
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
            });

        }

        private void DisplaySelectedFilesSize(object sender, ItemCheckEventArgs e)
        {
            DisplaySelectedFilesSize();
        }

        private void DisplaySelectedFilesSize()
        {
            long totalSize = 0;

            foreach (ListViewItem selectedItem in listView1.SelectedItems)
            {
                // Assuming the column index for file sizes is 1 (adjust as per your ListView)
                string fileSizeString = selectedItem.SubItems[1].Text.Replace('M',' ').Replace('K',' ').Replace('G', ' ').Replace('B', ' ').Replace('K', ' ').Replace('y', ' ').Replace('t', ' ').Replace('e', ' ').Replace('s', ' ');
                fileSizeString = fileSizeString.Trim();
                if (long.TryParse(fileSizeString, out long fileSize))
                {
                    totalSize += fileSize;
                }
            }

            // Convert totalSize to appropriate units (KB, MB, GB, etc.) for display
            string totalSizeFormatted = ConvertBytes(totalSize);

            // Display the total size somewhere, for example, in a label or messagebox
            selectedFileSize.Text = totalSizeFormatted;
        }

        private async void deleteSelected_Click(object sender, EventArgs e)
        {

            await Task.Run(() => {
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
            });            

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

            DisplaySelectedFilesSize();

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

            DisplaySelectedFilesSize();
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

            DisplaySelectedFilesSize();
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

            DisplaySelectedFilesSize();
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

            DisplaySelectedFilesSize();

            deleteSelected_Click(sender, e);

        }

        // FIND FILES WITH EXTENSIONS
        private async void findWithExtension(object sender, EventArgs e)
        {
            await Task.Run(() => {
                if (Directory.Exists(pathToSearch))
                {

                    string extensions = extension.Text;

                    // Split the input string by commas and remove spaces
                    string[] words = extensions.Split(',');

                    var selectedTypes = new List<string>();
                    var files = new List<string>();

                    if (fileNameRadio.Checked)
                    {
                        foreach (string word in words)
                        {
                            string cleanedWord = word.Trim(); // Remove leading/trailing spaces
                            selectedTypes.Add(cleanedWord);
                        }

                        files = Directory.GetFiles(pathToSearch, "*.*", SearchOption.AllDirectories)
                         .Where(file => selectedTypes.Contains(Path.GetFileName(file), StringComparer.OrdinalIgnoreCase))
                         .ToList();

                    }

                    if (extensionRadio.Checked)
                    {
                        foreach (string word in words)
                        {
                            string cleanedWord = "." + word.Trim(); // Remove leading/trailing spaces
                            selectedTypes.Add(cleanedWord);
                        }

                        files = Directory.GetFiles(pathToSearch, "*.*", SearchOption.AllDirectories)
                         .Where(file => selectedTypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                         .ToList();
                    }

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
            });

        }

        private async void deleteWithExtension(object sender, EventArgs e)
        {
            await Task.Run(() => {
                if (Directory.Exists(pathToSearch))
                {

                    string extensions = extension.Text;

                    // Split the input string by commas and remove spaces
                    string[] words = extensions.Split(',');

                    var selectedTypes = new List<string>();
                    var files = new List<string>();

                    if (fileNameRadio.Checked)
                    {
                        foreach (string word in words)
                        {
                            string cleanedWord = word.Trim(); // Remove leading/trailing spaces
                            selectedTypes.Add(cleanedWord);
                        }

                        files = Directory.GetFiles(pathToSearch, "*.*", SearchOption.AllDirectories)
                         .Where(file => selectedTypes.Contains(Path.GetFileName(file), StringComparer.OrdinalIgnoreCase))
                         .ToList();

                    }

                    if (extensionRadio.Checked)
                    {
                        foreach (string word in words)
                        {
                            string cleanedWord = "." + word.Trim(); // Remove leading/trailing spaces
                            selectedTypes.Add(cleanedWord);
                        }

                        files = Directory.GetFiles(pathToSearch, "*.*", SearchOption.AllDirectories)
                         .Where(file => selectedTypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                         .ToList();
                    }

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

                SelectAllFiles();
                selectAllButton.Text = "Deselect All";

                deleteSelected_Click(sender, e);

            });
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

        private async void findOldFiles(object sender, EventArgs e)
        {
            await Task.Run(() => {
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
            });


        }

        private async void deleteOldFiles(object sender, EventArgs e)
        {
            await Task.Run(() => {
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
            });


        }

        private async void clearRecycleBin(object sender, EventArgs e)
        {
            await Task.Run(() => {
                int result = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOCONFIRMATION);
                if (result == 0)
                {
                    MessageBox.Show("Recycle bin emptied successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to empty the recycle bin.");
                }
            });
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

        private async void YourForm_Load(object sender, EventArgs e)
        {
            await Task.Run(() => {
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
            });

        }

        private async void Form3_Load(object sender, EventArgs e)
        {
            await Task.Run(() => {
                loadLargeFilesListview();
            });

            await Task.Run(() => {
                largeSizeFolders();
            });


            await Task.Run(() => {
                displayRecentFilesListview(DeserializeRecentFilesCSVToList());
            });

            // [Start] code for Apps tab
            await Task.Run(() =>
            {
                DisplayInstalledAppsInListView(GetInstalledApps());
                // [END] code for Apps tab });
            });

            await Task.Run(() => {

                         pictureSize.Text = ConvertBytes(GetFolderSize(@"C:\Users\" + Environment.UserName + @"\Pictures", totalPictures));
                         videoSize.Text = ConvertBytes(GetFolderSize(@"C:\Users\" + Environment.UserName + @"\Videos", totalVideos));
                         audioSize.Text = ConvertBytes(GetFolderSize(@"C:\Users\" + Environment.UserName + @"\Musics", totalAudios));
                         docSize.Text = ConvertBytes(GetFolderSize(@"C:\Users\" + Environment.UserName + @"\Documents", totalDoc));
                         downloadSize.Text = ConvertBytes(GetFolderSize(@"C:\Users\" + Environment.UserName + @"\Download", totalDownload));
                         desktopSize.Text = ConvertBytes(GetFolderSize(@"C:\Users\" + Environment.UserName + @"\Desktop", totalDesktop));
                         screenshotSize.Text = ConvertBytes(GetFolderSize(@"C:\Users\" + Environment.UserName + @"\Pictures\Screenshots",totalScreenshots));
                         tempSize.Text = ConvertBytes(GetFolderSize(@"C:\Windows\Temp\",totalTemp));
                         temp2Size.Text = ConvertBytes(GetFolderSize(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp",totalTemp2));
                         recyclebinSize.Text = ConvertBytes(GetFolderSize($@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\Recycle Bin",totalRecyclebin)); // Path to Recycle Bin
                                                         
            });


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

        static long GetFolderSize(string folderPath, Control control)
        {
            long size = 0;
            int totalFiles = 0;

            try
            {
                // Check if the folder exists
                if (Directory.Exists(folderPath))
                {
                    // Get the files in the directory and sum up their sizes
                    string[] files = Directory.GetFiles(folderPath);
                    foreach (string file in files)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        size += fileInfo.Length;
                        totalFiles++;
                    }

                    // Get subdirectories and recursively calculate their sizes
                    string[] subDirectories = Directory.GetDirectories(folderPath);
                    foreach (string subDir in subDirectories)
                    {
                        size += GetFolderSize(subDir, control);
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.ToString());
                // MessageBox.Show(ex.Message);
            }

            control.Text = totalFiles + " Files";
            return size;
        }

        private void oneClickClear(object sender, EventArgs e)
        {

        }

        static async void DeleteFolderContents(string folderPath)
        {
            await Task.Run(() => {
                try
                {
                    foreach (string file in Directory.GetFiles(folderPath))
                    {
                        try
                        {
                            if (File.Exists(file))
                            {
                                File.Delete(file);
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            // File is inaccessible; skip deletion
                            // Log or handle the inaccessible file scenario as needed
                        }
                    }

                    foreach (string subDir in Directory.GetDirectories(folderPath))
                    {
                        DeleteFolderContents(subDir);
                        if (Directory.GetFiles(subDir).Length == 0 && Directory.GetDirectories(subDir).Length == 0)
                        {
                            Directory.Delete(subDir);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }
            });

        }

        private async void clearPictureFolder(object sender, EventArgs e)
        {
            await Task.Run(() => {
                string confirmationMessage = $"Are you sure you want to clear Picture folder ?";

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Pictures");
                }
            });

        }

        private async void clearVideosFolder(object sender, EventArgs e)
        {
            await Task.Run(() => {
                string confirmationMessage = $"Are you sure you want to clear Videos folder ?";

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Videos");

                }
            });

        }

        private async void clearMusicsFolder(object sender, EventArgs e)
        {
            await Task.Run(() => {
                string confirmationMessage = $"Are you sure you want to clear music folder ?";

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Musics");
                }
            });

        }

        private async void clearDocumentsFolder(object sender, EventArgs e)
        {
            await Task.Run(() => {

            });
            string confirmationMessage = $"Are you sure you want to clear Document folder ?";

            // Show confirmation dialog
            DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Documents");
            }

        }

        private async void clearDownloadFolder(object sender, EventArgs e)
        {
            await Task.Run(() => {
                string confirmationMessage = $"Are you sure you want to clear download folder ?";

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Downloads");
                }
            });


        }

        private async void clearDesktopFolder(object sender, EventArgs e)
        {
            await Task.Run(() => {
                string confirmationMessage = $"Are you sure you want to clear desktop folder ?";

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Desktop");
                }

            });

        }

        private async void clearScreenshotsFolder(object sender, EventArgs e)
        {
            await Task.Run(() => {
                string confirmationMessage = $"Are you sure you want to clear screenshot folder ?";

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\Pictures\Screenshots");
                }
            });

        }

        private async void clearTempFolder(object sender, EventArgs e)
        {
            await Task.Run(() => {
                string confirmationMessage = $"Are you sure you want to clear Temporary folder ?";

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteFolderContents(@"C:\Windows\Temp\");
                }

            });

        }

        private async void clearTemp2Folder(object sender, EventArgs e)
        {
            await Task.Run(() => {
                string confirmationMessage = $"Are you sure you want to clear Temporary folder ?";

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteFolderContents(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp");
                }
            });
        }

        private void analyzePictureFolder(object sender, MouseEventArgs e)
        {
            OpenSpecificTabWithData(@"C:\Users\" + Environment.UserName + @"\Pictures", sender, e);
        }
        private void analyzeVideoFolder(object sender, MouseEventArgs e)
        {
            OpenSpecificTabWithData(@"C:\Users\" + Environment.UserName + @"\Videos", sender, e);
        }
        private void analyzeMusicFolder(object sender, MouseEventArgs e)
        {
            OpenSpecificTabWithData(@"C:\Users\" + Environment.UserName + @"\Musics", sender, e);
        }
        private void analyzeDocumentFolder(object sender, MouseEventArgs e)
        {
            OpenSpecificTabWithData(@"C:\Users\" + Environment.UserName + @"\Documents", sender, e);
        }
        private void analyzeDownloadFolder(object sender, MouseEventArgs e)
        {
            OpenSpecificTabWithData(@"C:\Users\" + Environment.UserName + @"\Downloads", sender, e);
        }
        private void analyzeDesktopFolder(object sender, MouseEventArgs e)
        {
            OpenSpecificTabWithData(@"C:\Users\" + Environment.UserName + @"\Desktop", sender, e);
        }
        private void analyzeScreenshotFolder(object sender, MouseEventArgs e)
        {
            OpenSpecificTabWithData(@"C:\Users\" + Environment.UserName + @"\Pictures\Screenshots", sender, e);
        }
        private async void oneClickClearButton_Click(object sender, EventArgs e)
        {

            long totalAvailableSpaceBerforeButtonClick = 0;
            long totalAvialableSpaceAfterProcessingFiles = 0;
            long totalSpaceCleared = 0;

            long totalNumberOfFilesProcessed = 0;
            long totalNumberOfFilesDeleted = 0;
            long totalSizeOfFiles = 0;

            await Task.Run(() =>
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                // record the space avaiable in machine before clearing the file
                foreach (DriveInfo drive in allDrives)
                {
                    if (drive.IsReady)
                    {
                        totalAvailableSpaceBerforeButtonClick += drive.AvailableFreeSpace;
                    }
                }

                //  DELETE THE TEMP FOLDERS
                string[] folders = { @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp", @"C:\Windows\Temp\" };
                foreach (string folder in folders)
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))
                        {
                            try
                            {
                                if (File.Exists(file))
                                {
                                    try { File.Delete(file); }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Custom:==>" + ex.Message);
                                    }

                                    oneClickDeletingFilename.Text = file;

                                    FileInfo fileInfo = new FileInfo(file);
                                    spaceClearedLabel.Text = ConvertBytes(fileInfo.Length);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Custom2:==>" + ex.Message);
                            }
                        }

                        foreach (string subDir in Directory.GetDirectories(folder))
                        {
                            DeleteFolderContents(subDir);
                            if (Directory.GetFiles(subDir).Length == 0 && Directory.GetDirectories(subDir).Length == 0)
                            {
                                Directory.Delete(subDir, true);
                                oneClickDeletingFilename.Text = subDir;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Custom3:==>" + ex.Message);
                    }
                }

                // DELETE DULICATE FILES
                var filesByHash = new Dictionary<string, List<string>>();

                // process all the avaiable drives for redundant files
                //  foreach (DriveInfo d in allDrives)    
                {
                    // LOOP THROUGH ALL FILES IN THE DRIVE
                    foreach (var filePath in Directory.GetFiles(@"C:\Users\SANDEEP\Desktop\sandeep"))
                    {
                        FileInfo fileinfo = new FileInfo(filePath);

                        string[] specificExtensions = { ".tmp", ".cache", ".log", ".old", ".temp", ".dump", "thumbs.db", ".DS_Store", ".partial", ".crdownload" };

                        // Loop through files with specific extensions in the directory
                        if (filePath.EndsWith(specificExtensions[0]) || filePath.EndsWith(specificExtensions[1])
                            || filePath.EndsWith(specificExtensions[2]) || filePath.EndsWith(specificExtensions[3])
                            || filePath.EndsWith(specificExtensions[4]) || filePath.EndsWith(specificExtensions[5])
                            || filePath.EndsWith(specificExtensions[6]) || filePath.EndsWith(specificExtensions[7])
                            || filePath.EndsWith(specificExtensions[8]) || filePath.EndsWith(specificExtensions[9])
                            || fileinfo.Length < 1024
                            )
                        {

                            try
                            {
                                // DELETE THE FILES THAT MATCHES THE CONDITIONS
                                File.Delete(filePath);
                                oneClickDeletingFilename.Text = filePath;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("custom4 :==>" + ex.Message);
                            }
                        }
                        else
                        {
                            // CREATE A DICTIONARIES OF SAME FILES
                            string fileHash = CalculateMD5(filePath);

                            if (!filesByHash.ContainsKey(fileHash))
                            {
                                filesByHash[fileHash] = new List<string>();
                            }

                            filesByHash[fileHash].Add(filePath);

                            var duplicates = new Dictionary<string, List<string>>();

                            foreach (var entry in filesByHash)
                            {
                                if (entry.Value.Count > 1)
                                {
                                    duplicates[entry.Key] = entry.Value;
                                }
                            }

                            foreach (var duplicateGroup in duplicates.Values)
                            {
                                // Keep the first file and delete the rest
                                for (int i = 1; i < duplicateGroup.Count; i++)
                                {
                                    try
                                    {
                                        File.Delete(duplicateGroup[i]);
                                        oneClickDeletingFilename.Text = duplicateGroup[i];

                                        FileInfo fileInfo = new FileInfo(duplicateGroup[i]);
                                        spaceClearedLabel.Text = ConvertBytes(fileInfo.Length);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                    }

                                }
                            }
                        }
                    }

                }

                // calculate the space after processing (deleting) files
                foreach (DriveInfo drive in allDrives)
                {
                    if (drive.IsReady)
                    {
                        totalAvialableSpaceAfterProcessingFiles += drive.AvailableFreeSpace;
                    }
                }

                // set the label

                totalSpaceCleared = totalAvialableSpaceAfterProcessingFiles - totalAvailableSpaceBerforeButtonClick;
                spaceClearedLabel.Text = ConvertBytes(totalSpaceCleared);
                oneClickDeletingFilename.Text = "Done";

            });

        }

        //[START] belongs to Largest file listview
        [Serializable]
        public class LargestFileData
        {
            public string LargestFilePath { get; set; }
            public long LargestFileSize { get; set; }
        }

        public async void loadLargeFilesListview()
        {
            await Task.Run(() => {
                List<LargestFileData> largestFiles;

                if (File.Exists(@"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\FileList.txt"))
                {
                    // Load the list from the file if it exists
                    largestFiles = DeserializeCSVToList();
                }
                else
                {

                    //   MessageBox.Show("not Found for files..");

                    // Create the list by recursively getting all files in the drive
                    List<string> drives = Environment.GetLogicalDrives().ToList();
                    largestFiles = new List<LargestFileData>();

                    foreach (string drive in drives.Where(d => !d.Equals("C:\\", StringComparison.OrdinalIgnoreCase)))
                    {
                        try
                        {
                            List<string> files = Directory.GetFiles(drive, "*", SearchOption.AllDirectories).ToList();

                            List<LargestFileData> driveFiles = files.Select(file =>
                            {
                                FileInfo fileInfo = new FileInfo(file);
                                return new LargestFileData { LargestFilePath = file, LargestFileSize = fileInfo.Length };
                            }).OrderByDescending(file => file.LargestFileSize).ToList();

                            largestFiles = largestFiles.Concat(driveFiles)
                                .OrderByDescending(file => file.LargestFileSize)
                                .Take(100)
                                .ToList();
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            Console.WriteLine($"Access denied: {ex.Message}");
                        }
                        catch (DirectoryNotFoundException ex)
                        {
                            Console.WriteLine($"Directory not found: {ex.Message}");
                        }
                    }

                    // Store the list in the file for future use
                    //SerializeListToFile(largestFiles);
                    SerializeListToCSV(largestFiles);
                }

                largeSizeFileListview.View = View.Details;
                largeSizeFileListview.Columns.Add("File Name", 200);
                largeSizeFileListview.Columns.Add("File Path", 400);
                largeSizeFileListview.Columns.Add("File Size", 80);
                largeSizeFileListview.Columns.Add("Created At", 150);

                foreach (LargestFileData file in largestFiles)
                {
                    FileInfo fileInfo = new FileInfo(file.LargestFilePath);

                    ListViewItem item = new ListViewItem(fileInfo.Name);
                    item.SubItems.Add(file.LargestFilePath);
                    item.SubItems.Add(ConvertBytes(file.LargestFileSize));
                    item.SubItems.Add(fileInfo.CreationTime + "");


                    largeSizeFileListview.Items.Add(item);
                }
            });

        }

        public void SerializeListToCSV(List<LargestFileData> list)
        {
            try
            {
                //   justLoggging("Serialize using csv format");

                // Create a StringBuilder to build the CSV content
                StringBuilder csvContent = new StringBuilder();

                // Append CSV headers (assuming LargestFileData has properties: FilePath and FileSize)
                csvContent.AppendLine("FilePath,FileSize");

                // Append data from the list to the CSV content
                foreach (var fileData in list)
                {
                    // Format each line in CSV with FilePath and FileSize separated by a comma
                    csvContent.AppendLine($"{fileData.LargestFilePath},{fileData.LargestFileSize}");
                }

                string filePath = @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\FileList.txt";

                // Write the CSV content to the specified file
                File.WriteAllText(filePath, csvContent.ToString());

                Console.WriteLine("List serialized to CSV successfully.");
            }
            catch (Exception ex)
            {
                //   justLoggging("Serialize exception occured: " + ex.Message + ex.StackTrace);
                Console.WriteLine($"Error serializing list to CSV: {ex.Message}");
            }
        }

        public List<LargestFileData> DeserializeCSVToList()
        {
            string filePath = @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\FileList.txt";

            List<LargestFileData> deserializedList = new List<LargestFileData>();

            try
            {
                //   justLoggging("csv Deserialization start");

                string[] lines = File.ReadAllLines(filePath);

                // Assuming the first line contains headers
                string[] headers = lines[0].Split(',');

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] data = lines[i].Split(',');

                    if (data.Length == headers.Length)
                    {
                        LargestFileData fileData = new LargestFileData();

                        for (int j = 0; j < headers.Length; j++)
                        {
                            // Adjust property assignment based on your CSV structure
                            if (headers[j] == "FilePath")
                            {
                                fileData.LargestFilePath = data[j];
                            }
                            else if (headers[j] == "FileSize")
                            {
                                if (long.TryParse(data[j], out long fileSize))
                                {
                                    fileData.LargestFileSize = fileSize;
                                }
                            }
                            // Add other properties as needed...
                        }

                        deserializedList.Add(fileData);
                    }
                }

                Console.WriteLine("CSV file deserialized successfully.");
            }
            catch (Exception ex)
            {
                // justLoggging("Deserialization exception occured: " + ex.Message + ex.StackTrace);

                Console.WriteLine($"Error deserializing CSV to list: {ex.Message}");
            }

            return deserializedList;
        }

        static async void SerializeListToFile(List<LargestFileData> list)
        {
            await Task.Run(() => {
                try
                {
                    using (Stream stream = File.Open(@"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\FileList.txt", FileMode.Create))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, list);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error serializing list: {ex.Message}");
                }
            });
        }

        static List<LargestFileData> DeserializeListFromFile()
        {
            try
            {
                using (Stream stream = File.Open(@"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\FileList.txt", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (List<LargestFileData>)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("line:1609 "+ex.Message );
                Console.WriteLine($"Error deserializing list: {ex.Message}");
                return new List<LargestFileData>();
            }
        }
        //[END] belongs to Largest file listview

        /// <summary>
        /// ////////////////////////////////////////////////////////////////
        /// </summary>
        // SORTING LARGE SIZE FOLDERS

        public async void largeSizeFolders()
        {
            await Task.Run(() => {
                largeSizeFoldersListview.View = View.Details;
                largeSizeFoldersListview.Columns.Add("Name", 200);
                largeSizeFoldersListview.Columns.Add("Folder Size", 100);
                largeSizeFoldersListview.Columns.Add("Folder Path", 400);

                var folders = GetFoldersWithLargestSize(50);

                foreach (var folder in folders)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(folder.FolderPath);
                    ListViewItem item = new ListViewItem(directoryInfo.Name);
                    item.SubItems.Add(ConvertBytes(folder.FolderSize));
                    item.SubItems.Add(directoryInfo.FullName+"");

                    largeSizeFoldersListview.Items.Add(item);
                }

                // Add the ListView to a form or display it in your application
            });

        }

        static List<FolderData> GetFoldersWithLargestSize(int count)
        {
            List<FolderData> folders = new List<FolderData>();

            // Check if the file exists, if it does, deserialize the list from the file
            string path = @"C:\Program Files (x86)\SANDEEP\largestFolderList.txt2";
            if (File.Exists(path))
            {
                folders = DeserializeListFromFile(path);
              //  MessageBox.Show("Found for folder..");
            }
            else
            {
              //  MessageBox.Show("Not Found for folder..");
                DriveInfo[] drives = DriveInfo.GetDrives();

                foreach (var drive in drives.Where(d => d.IsReady && d.DriveType == DriveType.Fixed && !d.Name.Equals("C:\\", StringComparison.OrdinalIgnoreCase)))
                {
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(drive.RootDirectory.FullName);

                        foreach (var dir in di.EnumerateDirectories("*", SearchOption.AllDirectories))
                        {
                            long folderSize = GetDirectorySize(dir.FullName);

                            folders.Add(new FolderData { FolderPath = dir.FullName, FolderSize = folderSize });
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine($"Access denied: {ex.Message}");
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        Console.WriteLine($"Directory not found: {ex.Message}");
                    }

                }

                // Store the list in the file for future use
                SerializeListToFile(path, folders);
            }


            return folders.OrderByDescending(f => f.FolderSize).Take(count).ToList();
        }

        static long GetDirectorySize(string directoryPath)
        {
            long size = 0;

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

                if (directoryInfo.Exists)
                {
                    foreach (var file in directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories))
                    {
                        size += file.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("1506:"+ex.Message);
                Debug.WriteLine(ex.Message);
            }

            return size;
        }

        static async void SerializeListToFile(string filePath, List<FolderData> list)
        {
            await Task.Run(() => {
                try
                {
                    using (Stream stream = File.Open(filePath, FileMode.Create))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, list);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    //  MessageBox.Show("1524:"+ex.Message);
                }
            });
        }

        static List<FolderData> DeserializeListFromFile(string filePath)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (List<FolderData>)formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //  MessageBox.Show("1504:" + ex.Message);
                return new List<FolderData>();
            }
        }

        public void SerializeListToCSV(string filePath, List<FolderData> list)
        {
            try
            {
             //   justLoggging("Serialize using csv format");

                // Create a StringBuilder to build the CSV content
                StringBuilder csvContent = new StringBuilder();

                // Append CSV headers (assuming LargestFileData has properties: FilePath and FileSize)
                csvContent.AppendLine("FilePath,FileSize");

                // Append data from the list to the CSV content
                foreach (var fileData in list)
                {
                    // Format each line in CSV with FilePath and FileSize separated by a comma
                    csvContent.AppendLine($"{fileData.FolderPath},{fileData.FolderSize}");
                }

                // Write the CSV content to the specified file
                File.WriteAllText(filePath, csvContent.ToString());

                Console.WriteLine("List serialized to CSV successfully.");
            }
            catch (Exception ex)
            {
             //   justLoggging("Serialize exception occured: " + ex.Message + ex.StackTrace);
                Console.WriteLine($"Error serializing list to CSV: {ex.Message}");
            }
        }

        public List<LargestFileData> DeserializeCSVToList(string filePath)
        {
            List<LargestFileData> deserializedList = new List<LargestFileData>();

            try
            {
             //   justLoggging("csv Deserialization start");

                string[] lines = File.ReadAllLines(filePath);

                // Assuming the first line contains headers
                string[] headers = lines[0].Split(',');

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] data = lines[i].Split(',');

                    if (data.Length == headers.Length)
                    {
                        LargestFileData fileData = new LargestFileData();

                        for (int j = 0; j < headers.Length; j++)
                        {
                            // Adjust property assignment based on your CSV structure
                            if (headers[j] == "FilePath")
                            {
                                fileData.LargestFilePath = data[j];
                            }
                            else if (headers[j] == "FileSize")
                            {
                                if (long.TryParse(data[j], out long fileSize))
                                {
                                    fileData.LargestFileSize = fileSize;
                                }
                            }
                            // Add other properties as needed...
                        }

                        deserializedList.Add(fileData);
                    }
                }

                Console.WriteLine("CSV file deserialized successfully.");
            }
            catch (Exception ex)
            {
               // justLoggging("Deserialization exception occured: " + ex.Message + ex.StackTrace);

                Console.WriteLine($"Error deserializing CSV to list: {ex.Message}");
            }

            return deserializedList;
        }

        [Serializable]
        public class FolderData
        {
            public string FolderPath { get; set; }
            public long FolderSize { get; set; }
        }

        //////////////////////////////////////////////////////////////////////////////////
        // ADDING RIGHT CLICK ON "LARGE SIZE FOLDER" LISTVIEW 

        private  void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            
                if (e.Button == MouseButtons.Right)
                {
                    var item = largeSizeFoldersListview.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {

                        var menu = new ContextMenu();
                        var analyzeMenuItem = new MenuItem("Analyze");
                        var openInExplorer = new MenuItem("Open in Explorer");
                        var deleteFolder = new MenuItem("Delete");

                        analyzeMenuItem.Click += (s, args) =>
                        {

                            // Get the data associated with the clicked item
                            string folderPath = item.Text; // Replace with data extraction logic

                            // Open a specific tab in the TabControl and pass the data
                            OpenSpecificTabWithData(folderPath, sender, e);
                        };

                        openInExplorer.Click += (s, args) =>
                        {
                            // Get the data associated with the clicked item
                            string folderPath = item.Text; // Replace with data extraction logic

                            // Open the folder using the default file explorer
                            Process.Start("explorer.exe", folderPath);
                        };

                        deleteFolder.Click += (s, args) =>
                        {
                            // Get the data associated with the clicked item
                            string folderPath = item.Text; // Replace with data extraction logic

                            string confirmationMessage = $"Are you sure you want to delete {folderPath} ?";

                            // Show confirmation dialog
                            DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (result == DialogResult.Yes)
                            {
                                // Open the folder using the default file explorer
                                Directory.Delete(folderPath);
                            }

                        };


                        menu.MenuItems.Add(analyzeMenuItem);
                        menu.MenuItems.Add(deleteFolder);
                        menu.MenuItems.Add(openInExplorer);

                        // Display the context menu at the clicked position
                        menu.Show(largeSizeFoldersListview, e.Location);
                    }
                }            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///  menu item for large file listview
        private void largeFilesListviewMenu(object sender, MouseEventArgs e)
        {
                if (e.Button == MouseButtons.Right)
                {
                    var item = largeSizeFoldersListview.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {
                        var menu = new ContextMenu();
                        var openInExplorer = new MenuItem("Open in Explorer");
                        var deleteFile = new MenuItem("Delete");

                        openInExplorer.Click += (s, args) =>
                        {
                            // Get the data associated with the clicked item
                            string filePath = item.Text; // Replace with data extraction logic

                            // Open the folder using the default file explorer
                            Process.Start("explorer.exe", filePath);
                        };

                        deleteFile.Click += (s, args) =>
                        {
                            // Get the data associated with the clicked item
                            string filePath = item.Text; // Replace with data extraction logic

                            string confirmationMessage = $"Are you sure you want to delete {filePath} ?";

                            // Show confirmation dialog
                            DialogResult result = MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (result == DialogResult.Yes)
                            {
                                // Open the folder using the default file explorer
                                File.Delete(filePath);
                            }

                        };


                        menu.MenuItems.Add(deleteFile);
                        menu.MenuItems.Add(openInExplorer);

                        // Display the context menu at the clicked position
                        menu.Show(largeSizeFoldersListview, e.Location);
                    }
                }

        }

        private void OpenSpecificTabWithData(string folderPath, object sender, MouseEventArgs e)
        {
            // Logic to open a specific tab in the TabControl with the provided data
            // For example:
            mainTabControl.SelectedIndex = 2; // Change 1 to the index of the desired tab

            // Perform actions with the provided data
            pathTextBox.Text = folderPath;
            pathToSearch = folderPath;

            imageCheckBox.Checked = true;
            videoCheckBox.Checked = true;
            audioCheckbox.Checked = true;
            appCheckbox.Checked = true;
            appFilesCheckbox.Checked = true;
            documentCheckbox.Checked = true;
            zipCheckbox.Checked = true;
            otherCheckBox.Checked = true;
            allCheckbox.Checked = true;

            analyze_click(sender, e);
        }

        private void folderPanelClick(object sender, MouseEventArgs e)
        {
            if (sender is Panel clickedPanel)
            {
                string panel = clickedPanel.Name;

                switch (panel)
                {
                    case "picturePanel":
                        // Code to handle picturePanel click
                        Process.Start("explorer.exe", @"C:\Users\" + Environment.UserName + @"\Pictures");

                        break;
                    case "musicPanel":
                        // Code to handle audioPanel click
                        Process.Start("explorer.exe", @"C:\Users\" + Environment.UserName + @"\Music");

                        break;
                    case "videoPanel":
                        // Code to handle videoPanel click
                        Process.Start("explorer.exe", @"C:\Users\" + Environment.UserName + @"\Videos");

                        break;
                    case "documentPanel":
                        // Code to handle documentPanel click
                        Process.Start("explorer.exe", @"C:\Users\" + Environment.UserName + @"\Documents");

                        break;
                    case "downloadPanel":
                        // Code to handle folderPanel click
                        Process.Start("explorer.exe", @"C:\Users\" + Environment.UserName + @"\Downloads");

                        break;
                    case "desktopPanel":
                        // Code to handle settingsPanel click
                        Process.Start("explorer.exe", @"C:\Users\" + Environment.UserName + @"\Desktop");

                        break;
                    case "screenshotPanel":
                        // Code to handle toolsPanel click
                        Process.Start("explorer.exe", @"C:\Users\" + Environment.UserName + @"\Pictures\Screenshots");

                        break;
                    case "tempPanel":
                        // Code to handle helpPanel click
                        Process.Start("explorer.exe", @"C:\Windows\Temp\");

                        break;
                    case "temp2Panel":
                        // Code to handle aboutPanel click
                        Process.Start("explorer.exe", @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp");

                        break;
                    default:
                        // Default case if the panel doesn't match any known name
                        Process.Start("explorer.exe", "shell:RecycleBinFolder");
                        break;
                }

            }
        }

        //[START] Code for Apps TAB
        // Define an ImageList to store app icons
        private ImageList appIconsImageList;

        private List<InstalledAppInfo> GetInstalledApps()
        {
            List<InstalledAppInfo> installedApps = new List<InstalledAppInfo>();

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                foreach (string subKeyName in key.GetSubKeyNames())
                {
                    using (RegistryKey subKey = key.OpenSubKey(subKeyName))
                    {
                        try {
                            string name = subKey.GetValue("DisplayName") as string;
                            string version = subKey.GetValue("DisplayVersion") as string;
                            string publisher = subKey.GetValue("Publisher") as string;
                            string installLocation = subKey.GetValue("InstallLocation") as string;
                            string installDate = subKey.GetValue("InstallDate") as string;
                            string estimatedSize = subKey.GetValue("EstimatedSize") as string;
                            string displayIcon = subKey.GetValue("DisplayIcon") as string;
                            string UninstallString = subKey.GetValue("UninstallString") as string;
                            string QuietUninstallString = subKey.GetValue("QuietUninstallString") as string;
                            string URLInfoAbout = subKey.GetValue("URLInfoAbout") as string;
                            string HelpLink = subKey.GetValue("HelpLink") as string;

                            if (!string.IsNullOrEmpty(name))
                            {
                                installedApps.Add(new InstalledAppInfo
                                {
                                    Name = name,
                                    Version = version,
                                    Publisher = publisher,
                                    InstallLocation = installLocation,
                                    InstallDate = installDate,
                                    EstimatedSize = estimatedSize,
                                    DisplayIcon = displayIcon,
                                    UninstallString = UninstallString,
                                    URLInfoAbout = URLInfoAbout,
                                    HelpLink = HelpLink,
                                    QuietUninstallString=QuietUninstallString,
                                });
                            }

                        }
                        catch(Exception ex) {
                           // MessageBox.Show(ex.Message);
                        }

                    }
                }
            }

            return installedApps;
        }

        // [START] display listview of recent files
        public class RecentFiles
        {
            public string RecentFilePath { get; set; }
            public long RecentFileSize { get; set; }
            public DateTime RecentFileCreationTime { get; set; }
            public string fileName { get; set; }

        }

        public List<RecentFiles> DeserializeRecentFilesCSVToList()
        {
            List<RecentFiles> deserializedList = new List<RecentFiles>();

            try
            {
                if (File.Exists(recentFilePath))
                {
                    string[] lines = File.ReadAllLines(recentFilePath);

                    // Assuming the first line contains headers
                    string[] headers = lines[0].Split(',');

                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] data = lines[i].Split(',');

                        if (data.Length == headers.Length)
                        {
                            RecentFiles fileData = new RecentFiles();

                            for (int j = 0; j < headers.Length; j++)
                            {
                                // Adjust property assignment based on your CSV structure
                                if (headers[j] == "FilePath")
                                {
                                    fileData.RecentFilePath = data[j];
                                }
                                else if (headers[j] == "FileSize")
                                {
                                    if (long.TryParse(data[j], out long fileSize))
                                    {
                                        fileData.RecentFileSize = fileSize;
                                    }
                                }
                                // Add other properties as needed...
                            }

                            deserializedList.Add(fileData);
                        }
                    }

                    Console.WriteLine("CSV file deserialized successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing CSV to list: {ex.Message}");
            }

            return deserializedList;
        }

        private async void displayRecentFilesListview(List<RecentFiles> recentFiles)
        {
            await Task.Run(() => {
                recentFilesListview.View = View.Details;
                recentFilesListview.Columns.Add("Name", 450);
                recentFilesListview.Columns.Add("Size", 125);
                recentFilesListview.Columns.Add("Created At", 250);
                recentFilesListview.Columns.Add("Full Path", 750);

                recentFiles = DeserializeRecentFilesCSVToList();

                foreach (var file in recentFiles)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file.RecentFilePath);

                        ListViewItem item = new ListViewItem(fileInfo.Name);

                        //MessageBox.Show("ICON Address: " + appInfo.DisplayIcon);
                        item.SubItems.Add(ConvertBytes(fileInfo.Length));
                        item.SubItems.Add( fileInfo.CreationTime+"");
                        item.SubItems.Add(fileInfo.FullName);

                        // Allow the user to rearrange columns.
                        recentFilesListview.AllowColumnReorder = true;

                        // Select the item and subitems when selection is made.
                        recentFilesListview.FullRowSelect = true;

                        recentFilesListview.Items.Add(item);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            });
        }

        // [END] display listview of recent files

        private async void DisplayInstalledAppsInListView(List<InstalledAppInfo> installedApps)
        {
            await Task.Run(() => {
                InstalledAppsListview.View = View.Details;
                //     InstalledAppsListview.Columns.Add("Icon", 100);
                InstalledAppsListview.Columns.Add("Name", 350);
                InstalledAppsListview.Columns.Add("Version", 125);
                InstalledAppsListview.Columns.Add("Publisher", 150);
                InstalledAppsListview.Columns.Add("Install Location", 450);
                InstalledAppsListview.Columns.Add("Install Date", 100);
                InstalledAppsListview.Columns.Add("Estimated Size", 120);

                foreach (var appInfo in installedApps)
                {
                    try
                    {


                        ImageList imageList = new ImageList();
                        imageList.ImageSize = new System.Drawing.Size(64, 64);

                        ListViewItem item = new ListViewItem(appInfo.Name);

                        if (!string.IsNullOrEmpty(appInfo.DisplayIcon) && File.Exists(appInfo.DisplayIcon))
                        {
                            // Extract thumbnails or use default icons
                            Icon icon = Icon.ExtractAssociatedIcon(appInfo.DisplayIcon); // Extract the icon from the file
                                                                                         //             imageList.Images.Add(icon.ToBitmap()); // Add the icon as a bitmap to the image list
                                                                                         //             item.ImageIndex = imageList.Images.Count+1; // Set the appropriate image index

                            InstalledAppsListview.SmallImageList = new ImageList();
                            InstalledAppsListview.SmallImageList.Images.Add(icon.ToBitmap());
                            item.ImageIndex = 0; // Set the image index to the only image in the list

                        }

                        //MessageBox.Show("ICON Address: " + appInfo.DisplayIcon);
                        item.SubItems.Add(appInfo.Version);
                        item.SubItems.Add(appInfo.Publisher);
                        item.SubItems.Add(appInfo.InstallLocation);
                        item.SubItems.Add(appInfo.InstallDate);
                        item.SubItems.Add(appInfo.EstimatedSize);

                        // Allow the user to rearrange columns.
                        InstalledAppsListview.AllowColumnReorder = true;

                        // Select the item and subitems when selection is made.
                        InstalledAppsListview.FullRowSelect = true;

                        // Sort the items in the list in ascending order.
                        InstalledAppsListview.Sorting = SortOrder.Ascending;

                        InstalledAppsListview.Items.Add(item);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            });
        }

        private void appInfoRightClick(object sender, MouseEventArgs e)
        {
            if (InstalledAppsListview.SelectedItems.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    var item = largeSizeFoldersListview.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {

                        var menu = new ContextMenu();
                        var uninstall = new MenuItem("Uninstall");
                        var quietUninstall = new MenuItem("Quiet Uninstall");
                        var appInfo = new MenuItem("App Information");
                        var appHelp = new MenuItem("App Help");

                        // Assuming the first column contains the application name
                        string selectedAppName = InstalledAppsListview.SelectedItems[0].Text;

                        // Search for the application in the installedApps list
                        InstalledAppInfo selectedApp = GetInstalledApps().FirstOrDefault(app => app.Name == selectedAppName);


                        uninstall.Click += (s, args) =>
                        {

                            if (selectedApp != null)
                            {
                                // Access UninstallString for the selected app
                                string uninstallString = selectedApp.UninstallString;

                                // Perform actions with the UninstallString
                                // For instance, initiate the uninstallation process
                                if (!string.IsNullOrEmpty(uninstallString))
                                {
                                    // Example: Start a process to run the uninstall command
                                    ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + uninstallString);
                                    Process.Start(psi);
                                }
                                else
                                {
                                    MessageBox.Show("Uninstall information not found for this application.");
                                }
                            }
                        };

                        quietUninstall.Click += (s, args) =>
                        {
                            if (selectedApp != null)
                            {
                                // Access UninstallString for the selected app
                                string quietUninstallString = selectedApp.QuietUninstallString;

                                // Perform actions with the UninstallString
                                // For instance, initiate the uninstallation process
                                if (!string.IsNullOrEmpty(quietUninstallString))
                                {
                                    // Example: Start a process to run the uninstall command
                                    ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + quietUninstallString);
                                    Process.Start(psi);
                                }else
                                {
                                    MessageBox.Show("Uninstall information not found for this application.");
                                }
                            }
                        };

                        appInfo.Click += (s, args) =>
                        {
                            if (selectedApp != null)
                            {
                                // Access UninstallString for the selected app
                                string URLInfoAbout = selectedApp.URLInfoAbout;

                                // Perform actions with the UninstallString
                                // For instance, initiate the uninstallation process
                                if (!string.IsNullOrEmpty(URLInfoAbout))
                                {
                                    Process.Start(URLInfoAbout);
                                }
                                else
                                {
                                    MessageBox.Show("App information link not found for this application.");
                                }
                            }

                        };

                        appHelp.Click += (s, args) =>
                        {
                            if (selectedApp != null)
                            {
                                // Access UninstallString for the selected app
                                string HelpLink = selectedApp.HelpLink;

                                // Perform actions with the UninstallString
                                // For instance, initiate the uninstallation process
                                if (!string.IsNullOrEmpty(HelpLink))
                                {
                                    Process.Start(HelpLink);
                                }
                                else
                                {
                                    MessageBox.Show("App help link not found for this application.");
                                }
                            }

                        };


                        menu.MenuItems.Add(uninstall);
                        menu.MenuItems.Add(quietUninstall);
                        menu.MenuItems.Add(appInfo);
                        menu.MenuItems.Add(appHelp);

                        // Display the context menu at the clicked position
                        menu.Show(InstalledAppsListview, e.Location);
                    }
                }

            }
        }

        private void smallFilesRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WriteExceptionToHTMLFile(Exception ex, string filePath)
        {
            try
            {
                // Create or append to an HTML file
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("<html><head><title>Exception Log</title></head><body>");
                    writer.WriteLine("<h1>Latest Exception:</h1>");
                    writer.WriteLine("<p><strong>Date/Time:</strong> " + DateTime.Now.ToString() + "</p>");
                    writer.WriteLine("<p><strong>Message:</strong> " + ex.Message + "</p>");
                    writer.WriteLine("<p><strong>Stack Trace:</strong><br/>" + ex.StackTrace + "</p>");
                    writer.WriteLine("<hr/>"); // Separator between exceptions
                    writer.WriteLine("</body></html>");
                }

                Console.WriteLine("Exception written to " + filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing exception to file: " + e.Message);
            }
        }

        private void largeSizeFileListview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            displayRecentFilesListview(DeserializeRecentFilesCSVToList());
        }

        // [END] Code for Apps Tab
    }

    // [START] Code for Apps Tab
    class InstalledAppInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
        public string InstallLocation { get; set; }
        public string InstallDate { get; set; }
        public string EstimatedSize { get; set; }
        public string DisplayIcon {  get; set; }
        public string HelpLink {  get; set; }
        public string URLInfoAbout {  get; set; }
        public string QuietUninstallString {  get; set; }
        public string UninstallString {  get; set; }

    }

    // [END] Code for Apps Tab


}