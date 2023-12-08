using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceProcess;
using System.Text;
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
        const string largestFoldersFile = "largestFoldersList.dat";

        protected override void OnStart(string[] args)
        {
            justLoggging("27");

            try {
                DeserializeListFromFileForLargestFiles();
                StartMonitoringDrives();             
            }
            catch (Exception ex)
            {
                WriteExceptionToHTMLFile(ex, @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\ServiceExceptionLog.html");
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
                WriteExceptionToHTMLFile(ex, @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\ServiceExceptionLog.html");
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
                WriteExceptionToHTMLFile(ex, @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\ServiceExceptionLog.html");
            }

        }

        // [START] Getting largest files           /////////////////////////////////////////////////////
        [Serializable]
        public class LargestFileData
        {
            public string LargestFilePath { get; set; }
            public long LargestFileSize { get; set; }
        }


        public async void getFilesWithLargestSize()
        {
            justLoggging("getfileWithLargestSize");
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
                            WriteExceptionToHTMLFile(ex, @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\ServiceExceptionLog.html");
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
                    SerializeListToFile(largestFiles);
                });
            }
            catch (Exception ex)
            {
                WriteExceptionToHTMLFile(ex, @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\ServiceExceptionLog.html");
            }


        }

        public async void SerializeListToFile(List<LargestFileData> list)
        {
            justLoggging("serilaizingListToFile");
            try
            {
                await Task.Run(() => {
                    try
                    {
                        using (Stream stream = File.Open(@"C:\Program Files (x86)\SANDEEP\largestFilesList.sandeep", FileMode.Create))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(stream, list);
                        }
                    }
                    catch (Exception ex)
                    {
                        justLoggging("serializeListToFile exception");
                        justLoggging(ex.Message);
                        WriteExceptionToHTMLFile(ex, @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\ServiceExceptionLog.html");
                        Console.WriteLine($"Error serializing list: {ex.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                WriteExceptionToHTMLFile(ex, @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\ServiceExceptionLog.html");
            }

        }

        // [END] Getting Large size files               /////////////////////////////////////////////////////


        // Getting LARGE SIZE FOLDERS [START]           /////////////////////////////////////////////////////

        public List<FolderData> GetFoldersWithLargestSize(int count)
        {
            justLoggging("178");

            List<FolderData> folders = new List<FolderData>();

            // Check if the file exists, if it does, deserialize the list from the file
            string path = @"C:\Program Files (x86)\SANDEEP\largestFolderList.txt2";

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
                        WriteExceptionToHTMLFile(ex, @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\ServiceExceptionLog.html");
                        Console.WriteLine($"Access denied: {ex.Message}");
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        WriteExceptionToHTMLFile(ex, @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\ServiceExceptionLog.html");
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
                        using (Stream stream = File.Open(filePath, FileMode.Create))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(stream, list);
                        }
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

        private readonly string listFilePath = @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\FileList.txt";                                               
        private List<LargestFileData> largestFiles;


        private void DeserializeListFromFileForLargestFiles()
        {
            justLoggging("276");

            try
            {
                if (File.Exists(listFilePath))
                {
                    using (Stream stream = File.Open(listFilePath, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        largestFiles = (List<LargestFileData>)formatter.Deserialize(stream);
                    }
                }
                else
                {
                    largestFiles = new List<LargestFileData>();
                }
            }
            catch (Exception ex)
            {
                justLoggging("DeserializeListFromFileForLargestFiles:" + ex.Message);
                WriteExceptionToHTMLFile(ex, "ServiceExceptionLog.html");
                Console.WriteLine($"Error deserializing list: {ex.Message}");
            }
        }

        private void SerializeListToFileForLargestFile()
        {
            justLoggging("303");            

            try
            {
              //  string listFilePath = @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\FileList.txt";
                using (Stream stream = File.Open(listFilePath, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();                    
                    formatter.Serialize(stream, largestFiles);
                }
            }
            catch (Exception ex)
            {
                justLoggging("SerializeListToFileForLargestFile:" + ex.Message);
                WriteExceptionToHTMLFile(ex, "ServiceExceptionLog.html");
                Console.WriteLine($"Error serializing list: {ex.Message}");
            }
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
                FileInfo fileInfo = new FileInfo(e.FullPath);

                justLoggging("file was "+e.FullPath);

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
                SerializeListToFileForLargestFile();
            }
            catch (Exception ex)
            {
                WriteExceptionToHTMLFile(ex, "ServiceExceptionLog.html");
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
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

        public void justLoggging(string line)
        {
            string filePath = @"E:\SANDEEP_KUMAR\PROJECT\desktop\fileanalyzer\fileanalyzer\FileSystemMonitorService\bin\Debug\File.html"; // Specify your file path

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
    }
}
