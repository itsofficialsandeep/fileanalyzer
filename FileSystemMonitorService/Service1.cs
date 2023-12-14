using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileSystemMonitorService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        static Dictionary<string, long> largestFolders = new Dictionary<string, long>();
        const string largestFoldersFile = @"C:\Program Files (x86)\Sandeep Kumar\Sandy's Windows Cleaner\largestFoldersList.dat";
        string filePath = @"C:\Program Files (x86)\Sandeep Kumar\Sandy's Windows Cleaner\listFiles.txt";
        string recentFilePath = @"C:\Program Files (x86)\Sandeep Kumar\Sandy's Windows Cleaner\recentFiles.txt";
        string csvDiskInfoFilePath = @"C:\Program Files (x86)\Sandeep Kumar\Sandy's Windows Cleaner\edisk_information.txt";
        private string LargestFolderListPath = @"C:\Program Files (x86)\Sandeep Kumar\Sandy's Windows Cleaner\largestFolderList.txt"; private readonly string listFilePath = @"C:\Program Files (x86)\Sandeep Kumar\Sandy's Windows Cleaner\FileList.txt";
        private List<LargestFileData> largestFiles;
        private List<RecentFiles> recentFiles;

        long fileCount = 0;
        long imageCount = 0;
        long videoCount = 0;
        long docCount = 0;
        long totalImageSize = 0;
        long totalVideoSize = 0;
        long totalDocSize = 0;

        List<DiskInformation> diskInfoList = new List<DiskInformation>();

        protected override void OnStart(string[] args)
        {
            justLoggging("27");

            try {
              largestFiles =  DeserializeCSVToList(filePath);
             //   DeserializeListFromFileForLargestFiles();
                StartMonitoringDrives();


                if (!File.Exists(csvDiskInfoFilePath))
                {
                    DriveInfo[] allDrives = DriveInfo.GetDrives();

                    foreach (DriveInfo driveInfo in allDrives)
                    {
                        justLoggging("gathering info for drive::" + driveInfo.Name);

                        diskInfo(driveInfo.Name);
                    }

                }

                // start monitoring drives for changes
                SetupFileSystemWatchersForAllDrives();
            }
            catch (Exception ex)
            {
                justLoggging(ex.StackTrace);

                WriteExceptionToHTMLFile(ex, @"E:\ServiceExceptionLog.html");
            }


        }        


        public long GetDirectorySize(string directoryPath)
        {
            justLoggging("getDirectorySize");
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
                justLoggging("getDirectorySize: " + ex.Message);
                WriteExceptionToHTMLFile(ex, @"E:\ServiceExceptionLog.html");
                Console.WriteLine("Error calculating folder size: " + ex.Message);
            }

            return size;
        }

        protected override void OnStop()
        {
            justLoggging("67");
            try
            {

            }
            catch (Exception ex)
            {
                WriteExceptionToHTMLFile(ex, @"E:\ServiceExceptionLog.html");
            }

        }

        // [START] Getting largest files           /////////////////////////////////////////////////////
        [Serializable]
        public class LargestFileData
        {
            public string LargestFilePath { get; set; }
            public long LargestFileSize { get; set; }
        }

        public class RecentFiles
        {
            public string RecentFilePath { get; set; }
            public long RecentFileSize { get; set; }
            public DateTime RecentFileCreationTime { get; set; }

            public string fileName { get; set; }

        }


        public async void getFilesWithLargestSize()
        {
            justLoggging("getting file With Large Size");

            try
            {
                await Task.Run(() => {
                    List<LargestFileData> largestFiles;

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
                        catch (UnauthorizedAccessException ex) {
                            justLoggging("getfileEithLargestSize: " + ex.Message);
                            WriteExceptionToHTMLFile(ex, @"E:\ServiceExceptionLog.html");
                            Console.WriteLine($"Access denied: {ex.Message}");
                        }
                        catch (DirectoryNotFoundException ex)
                        {
                            justLoggging("getfileEithLargestSize: " + ex.Message);
                            WriteExceptionToHTMLFile(ex, "ServiceExceptionLog.html");
                            Console.WriteLine($"Directory not found: {ex.Message}");
                        }
                    }

                    // Store the list in the file for future use
                    //SerializeListToFile(largestFiles);
                    SerializeListToCSV(largestFiles,listFilePath);
                });
            }
            catch (Exception ex)
            {
                WriteExceptionToHTMLFile(ex, @"E:\ServiceExceptionLog.html");
            }
        }

        public void SerializeListToCSV(List<LargestFileData> largestFiles, string listFilePath)
        {
            try
            {
                justLoggging("Serialize using csv format");

                // Create a StringBuilder to build the CSV content
                StringBuilder csvContent = new StringBuilder();

                // Append CSV headers (assuming LargestFileData has properties: FilePath and FileSize)
                csvContent.AppendLine("FilePath,FileSize");

                // Append data from the list to the CSV content
                foreach (var fileData in largestFiles)
                {
                    // Format each line in CSV with FilePath and FileSize separated by a comma
                    csvContent.AppendLine($"{fileData.LargestFilePath},{fileData.LargestFileSize}");
                }

                // Write the CSV content to the specified file
                File.WriteAllText(listFilePath, csvContent.ToString());

                Console.WriteLine("List serialized to CSV successfully.");
            }
            catch (Exception ex)
            {
                justLoggging("Serialize exception occured: " + ex.Message + ex.StackTrace);
                Console.WriteLine($"Error serializing list to CSV: {ex.Message}");
            }
        }

        // [END] Getting Large size files               /////////////////////////////////////////////////////

        // [START] Serializing recent files             /////////////////////////////////////////////////////

        public void SerializeRecentFilesListToCSV(List<RecentFiles> largestFiles, string listFilePath)
        {
            try
            {
                justLoggging("Serialize using csv format");

                // Create a StringBuilder to build the CSV content
                StringBuilder csvContent = new StringBuilder();

                // Append CSV headers (assuming LargestFileData has properties: FilePath and FileSize)
                csvContent.AppendLine("FilePath,FileSize");

                // Append data from the list to the CSV content
                foreach (var fileData in largestFiles)
                {
                    // Format each line in CSV with FilePath and FileSize separated by a comma
                    csvContent.AppendLine($"{fileData.RecentFilePath},{fileData.RecentFileSize}");
                }

                // Write the CSV content to the specified file
                File.WriteAllText(listFilePath, csvContent.ToString());

                Console.WriteLine("List serialized to CSV successfully.");
            }
            catch (Exception ex)
            {
                justLoggging("Serialize exception occured: " + ex.Message + ex.StackTrace);
                Console.WriteLine($"Error serializing list to CSV: {ex.Message}");
            }
        }

        // [END] serializing recent files               /////////////////////////////////////////////////////

        // Getting LARGE SIZE FOLDERS [START]           /////////////////////////////////////////////////////

        public List<FolderData> GetFoldersWithLargestSize(int count)
        {
            justLoggging("178");

            List<FolderData> folders = new List<FolderData>();

            // Check if the file exists, if it does, deserialize the list from the file
            string path = LargestFolderListPath;

            //  MessageBox.Show("Not Found for folder..");
            DriveInfo[] drives = DriveInfo.GetDrives();

            try
            {
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
                        WriteExceptionToHTMLFile(ex, @"E:\ServiceExceptionLog.html");
                        Console.WriteLine($"Access denied: {ex.Message}");
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        WriteExceptionToHTMLFile(ex, @"E:\ServiceExceptionLog.html");
                        Console.WriteLine($"Directory not found: {ex.Message}");
                    }

                }
            }
            catch (Exception ex)
            {
                WriteExceptionToHTMLFile(ex, "ServiceExceptionLog.html");
            }

            // Store the list in the file for future use
            SerializeListToFile(path, folders);

            return folders.OrderByDescending(f => f.FolderSize).Take(count).ToList();
        }

        public async void SerializeListToFile(string filePath, List<FolderData> list)
        {
            justLoggging("serializing list to file");

            try
            {
                await Task.Run(() => {
                    try
                    {
               /**         using (Stream stream = File.Open(filePath, FileMode.Create))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(stream, list);
                        }   **/

                        SerializeListToCSV(filePath);
                    }
                    catch (Exception ex)
                    {
                        justLoggging("an exception occured in serializeListToFile");
                        justLoggging(ex.Message);
                        WriteExceptionToHTMLFile(ex, "ServiceExceptionLog.html");
                        Debug.WriteLine(ex.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                justLoggging("SerializeListToFile(string filePath, List<FolderData> list): " + ex.Message);
                WriteExceptionToHTMLFile(ex, "ServiceExceptionLog.html");
            }

        }


        [Serializable]
        public class FolderData
        {
            public string FolderPath { get; set; }
            public long FolderSize { get; set; }
        }

        // GETTING LARGE SIZE FOLDERS [END]            /////////////////////////////////////////////////


        // FILE WATCHER      ///////////////////////////////////////////////////////////////////////////
        // Below are functions that will run to refresh the list of Largest Files Avaialable in the drives

        public void SerializeListToCSV(string listFilePath)
        {
            try
            {
                justLoggging("Serializing using csv format");

                // Create a StringBuilder to build the CSV content
                StringBuilder csvContent = new StringBuilder();

                // Append CSV headers (assuming LargestFileData has properties: FilePath and FileSize)
                csvContent.AppendLine("FilePath,FileSize");

                // Append data from the list to the CSV content
                foreach (var fileData in largestFiles)
                {
                    // Format each line in CSV with FilePath and FileSize separated by a comma
                    csvContent.AppendLine($"{fileData.LargestFilePath},{fileData.LargestFileSize}");
                }

                // Write the CSV content to the specified file
                File.WriteAllText(recentFilePath, csvContent.ToString());

                Console.WriteLine("List serialized to CSV successfully.");
            }
            catch (Exception ex)
            {
                justLoggging("Serialize exception occured: "+ex.Message + ex.StackTrace);
                Console.WriteLine($"Error serializing list to CSV: {ex.Message}");
            }
        }

        public List<LargestFileData> DeserializeCSVToList( string listFilePath)
        {
            List<LargestFileData> deserializedList = new List<LargestFileData>();

            try
            {
                justLoggging("csv Deserialization start");

                if (File.Exists(listFilePath)){
                    string[] lines = File.ReadAllLines(listFilePath);

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
                } else {
                    getFilesWithLargestSize();
                }

            }
            catch (Exception ex)
            {
                justLoggging("Deserialization exception occured: " + ex.Message + ex.StackTrace);

                Console.WriteLine($"Error deserializing CSV to list: {ex.Message}");
            }

            return deserializedList;
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


        private void StartMonitoringDrives()
        {
            justLoggging("started monitoring");
            try
            {
                foreach (string drive in Environment.GetLogicalDrives().Where(d => !d.Equals("C:\\", StringComparison.OrdinalIgnoreCase)))
                {
                    FileSystemWatcher watcher = new FileSystemWatcher(drive);
                    watcher.Created += OnFileCreated;                    
                    watcher.IncludeSubdirectories = true;
                    watcher.EnableRaisingEvents = true;
                }
            }
            catch (Exception ex)
            {
                WriteExceptionToHTMLFile(ex, "ServiceExceptionLog.html");
            }
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            justLoggging("Something was created");

            try
            {
                // file info of latest created file
                FileInfo fileInfo = new FileInfo(e.FullPath);

                justLoggging("file was "+e.FullPath);

                // this is for Largest Files
                if (largestFiles.Count >= 100)
                {
                    LargestFileData smallestFile = largestFiles.OrderBy(file => file.LargestFileSize).FirstOrDefault();
                    if (smallestFile?.LargestFileSize < fileInfo.Length)
                    {
                        largestFiles.Remove(smallestFile);
                    }
                    else
                    {
                        return; // Skip if the new file is smaller than the smallest in the list
                    }
                }

                LargestFileData newFile = new LargestFileData { LargestFilePath = e.FullPath, LargestFileSize = fileInfo.Length };
                largestFiles.Add(newFile);
                largestFiles = largestFiles.OrderByDescending(file => file.LargestFileSize).ToList();
                
                SerializeListToCSV(largestFiles, listFilePath);


                // code for "RECENT FILES"

                // Define a list of common media file extensions
                List<string> mediaExtensions = new List<string> {
                    ".mp3", ".mp4", ".avi", ".mkv", ".mov", ".wav", ".flv", ".wmv", ".m4a", /* Add more as needed */ 
                    ".exe", ".msi", ".app", ".bat", ".sh",".txt", ".doc", ".docx", ".rtf", ".odt",".xls", ".xlsx", ".ods", ".csv", ".ppt", ".pptx", ".odp", ".rar", ".zip", ".txt", ".pdf",".xml"
                };

                // Assuming fileInfo is created earlier with FileInfo fileInfo = new FileInfo(e.FullPath);

                recentFiles = DeserializeRecentFilesCSVToList();

                if (!fileInfo.FullName.Contains("$RECYCLE.BIN"))
                {
                    if (mediaExtensions.Contains(fileInfo.Extension, StringComparer.OrdinalIgnoreCase))
                    {
                        // Adds recent files (for third tab on main app)
                        if (recentFiles.Count >= 100)
                        {
                            // Remove the oldest file from the list
                            recentFiles.RemoveAt(recentFiles.Count - 1);

                            justLoggging("matched" + fileInfo.Name);
                        }

                        RecentFiles newRecentFile = new RecentFiles { RecentFilePath = e.FullPath, RecentFileCreationTime = fileInfo.CreationTime, RecentFileSize = fileInfo.Length, fileName = fileInfo.Name };
                        recentFiles.Insert(0, newRecentFile); // Insert at the beginning of the list
                        SerializeRecentFilesListToCSV(recentFiles, recentFilePath);
                    }
                }

            }
            catch (Exception ex)
            {
                justLoggging("exception at onFileCreated" + ex.ToString());
            }
        }

        // Logging Functions      ////////////////////////////////////////////////////////////////////////////////////
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

        public void justLoggging(string line)
        {
            string filePath = @"E:\File.html"; // Specify your file path

            try
            {
                // Create or append to an HTML file
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("<html><head><title>Exception Log</title></head><body>");
                    writer.WriteLine("<h1>Latest Exception:</h1>");
                    writer.WriteLine("<p><strong>Date/Time:</strong> " + DateTime.Now.ToString() + "</p>");
                    writer.WriteLine("<p><strong>Message:</strong> " + "Now " + ":"+ line + "</p>");
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



        // CODE FOR DISK INFORMATION TAB   ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void diskInfo(string disk)
        {
            try
            {
                justLoggging("line 656");

                TraverseTreeParallelForEach(disk, (f) =>
                {
                    try
                    {
                        // Count all files
                        Interlocked.Increment(ref fileCount);

                        string extension = Path.GetExtension(f)?.ToLower();
                        if (!string.IsNullOrEmpty(extension))
                        {
                            var fileInfo = new FileInfo(f);
                            long fileSize = fileInfo.Length;

                            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                            {
                                // Image file
                                Interlocked.Increment(ref imageCount);
                                Interlocked.Add(ref totalImageSize, fileSize);
                            }
                            else if (extension == ".mp4" || extension == ".avi" || extension == ".mkv")
                            {
                                // Video file
                                Interlocked.Increment(ref videoCount);
                                Interlocked.Add(ref totalVideoSize, fileSize);
                            }
                            else if (extension == ".doc" || extension == ".docx" || extension == ".pdf" || extension == ".txt")
                            {
                                // Document file
                                Interlocked.Increment(ref docCount);
                                Interlocked.Add(ref totalDocSize, fileSize);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception Message: " + ex.Message);
                        Debug.WriteLine("Stack Trace: " + ex.StackTrace);
                        Debug.WriteLine("Source: " + ex.Source);
                        Debug.WriteLine("Target Site: " + ex.TargetSite);
                    }

                });

                // Create or append to a CSV file with the collected information
                string csvContent = $"{disk},{fileCount},{imageCount},{videoCount},{docCount},{totalImageSize},{totalVideoSize},{totalDocSize}\n";

                File.AppendAllText(csvDiskInfoFilePath, csvContent);

            }
            catch (Exception ex)
            {
                justLoggging("Exception line--> 707"+ex.Message);
            }
        }

        private void SetupFileSystemWatchersForAllDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady && drive.DriveType == DriveType.Fixed)
                {
                    string rootPath = drive.RootDirectory.FullName;
                    SetupFileSystemWatcher(rootPath);
                }
            }
        }

        // File System Watcher to track file creation and deletion
        private void SetupFileSystemWatcher(string disk)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = disk;
            watcher.IncludeSubdirectories = true;

            // Watch for changes in files and directories
            watcher.Created += (sender, e) => fileWasCreated(disk,e.FullPath);
            watcher.Deleted += (sender, e) => fileWasDeleted(disk,e.FullPath);

            // Enable the watcher
            watcher.EnableRaisingEvents = true;
        }

        private void fileWasCreated(string disk,string path)
        {

            // TO-DO: Also add code to show new drive space

            FileInfo fileInfo = new FileInfo(path);
            string extension = Path.GetExtension(path)?.ToLower();

            // since a file has been created in the drive

            fileCount++;

            foreach(DiskInformation diskInformation in diskInfoList)
            {
                if (diskInformation != null)
                {
                    if(diskInformation.Disk == disk)
                    {
                        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                        {
                            diskInformation.ImageCount++;

                            diskInformation.TotalImageSize = diskInformation.TotalImageSize + fileInfo.Length;
                        }
                        else if (extension == ".mp4" || extension == ".avi" || extension == ".mkv")
                        {
                            diskInformation.VideoCount++;

                            diskInformation.TotalVideoSize = diskInformation.TotalVideoSize + fileInfo.Length;
                        }
                        else if (extension == ".doc" || extension == ".docx" || extension == ".pdf" || extension == ".txt")
                        {
                            diskInformation.DocCount++;

                            diskInformation.TotalDocSize = diskInformation.TotalDocSize + fileInfo.Length;
                        }
                    }
                }

                //     Create or append to a CSV file with the collected information
               string csvContent = $"{disk},{fileCount},{diskInformation.ImageCount},{diskInformation.VideoCount},{diskInformation.DocCount},{diskInformation.TotalImageSize},{diskInformation.TotalVideoSize},{diskInformation.TotalDocSize}\n";

                File.AppendAllText(csvDiskInfoFilePath, csvContent);
            }
            
        }

        private void fileWasDeleted(string disk, string path)
        {

            // TO-DO: Also add code to show new drive space

            FileInfo fileInfo = new FileInfo(path);
            string extension = Path.GetExtension(path)?.ToLower();

            // since a file has been created in the drive

            fileCount--;

            foreach (DiskInformation diskInformation in diskInfoList)
            {
                if (diskInformation != null)
                {
                    if (diskInformation.Disk == disk)
                    {
                        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                        {
                            diskInformation.ImageCount--;

                            diskInformation.TotalImageSize = diskInformation.TotalImageSize - fileInfo.Length;
                        }
                        else if (extension == ".mp4" || extension == ".avi" || extension == ".mkv")
                        {
                            diskInformation.VideoCount--;

                            diskInformation.TotalVideoSize = diskInformation.TotalVideoSize - fileInfo.Length;
                        }
                        else if (extension == ".doc" || extension == ".docx" || extension == ".pdf" || extension == ".txt")
                        {
                            diskInformation.DocCount--;

                            diskInformation.TotalDocSize = diskInformation.TotalDocSize + fileInfo.Length;
                        }
                    }
                }

                //     Create or append to a CSV file with the collected information
                string csvContent = $"{disk},{fileCount},{diskInformation.ImageCount},{diskInformation.VideoCount},{diskInformation.DocCount},{diskInformation.TotalImageSize},{diskInformation.TotalVideoSize},{diskInformation.TotalDocSize}\n";

                File.AppendAllText(csvDiskInfoFilePath, csvContent);
            }

        }

        public class DiskInformation
        {
            public string Disk { get; set; }
            public long FileCount { get; set; }
            public long ImageCount { get; set; }
            public long VideoCount { get; set; }
            public long DocCount { get; set; }
            public long TotalImageSize { get; set; }
            public long TotalVideoSize { get; set; }
            public long TotalDocSize { get; set; }
        }

        public static void TraverseTreeParallelForEach(string root, Action<string> action)
        {
            // Count of files traversed and timer for diagnostic output
            int fileCount = 0;
            var sw = Stopwatch.StartNew();

            // Determine whether to parallelize file processing on each folder based on processor count.
            int procCount = Environment.ProcessorCount;

            // Data structure to hold names of subfolders to be examined for files.
            Stack<string> dirs = new Stack<string>();

            if (!Directory.Exists(root))
            {
                throw new ArgumentException("The given root directory doesn't exist.", nameof(root));
            }
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs = { };
                string[] files = { };

                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }
                // Thrown if we do not have discovery permission on the directory.
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                // Thrown if another process has deleted the directory after we retrieved its name.
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                try
                {
                    files = Directory.GetFiles(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // Execute in parallel if there are enough files in the directory.
                // Otherwise, execute sequentially. Files are opened and processed
                // synchronously but this could be modified to perform async I/O.
                try
                {
                    if (files.Length < procCount)
                    {
                        foreach (var file in files)
                        {
                            action(file);
                            Interlocked.Increment(ref fileCount);
                        }
                    }
                    else
                    {
                        Parallel.ForEach(files, () => 0,
                            (file, loopState, localCount) =>
                            {
                                action(file);
                                return (int)++localCount;
                            },
                            (c) =>
                            {
                                Interlocked.Add(ref fileCount, c);
                            });
                    }
                }
                catch (AggregateException ae)
                {
                    ae.Handle((ex) =>
                    {
                        if (ex is UnauthorizedAccessException)
                        {
                            // Here we just output a message and go on.
                            Console.WriteLine(ex.Message);
                            return true;
                        }
                        // Handle other exceptions here if necessary...

                        return false;
                    });
                }

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs)
                    dirs.Push(str);
            }

            // For diagnostic purposes.
            Console.WriteLine("Processed {0} files in {1} milliseconds", fileCount, sw.ElapsedMilliseconds);
        }
    }
}
