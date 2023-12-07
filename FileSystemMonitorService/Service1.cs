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
            // Load existing largest folders data
            LoadLargestFolders();

            // Create a FileSystemWatcher instance
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = @"C:\"; // Change this to the root path you want to monitor
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            // Subscribe to the Created event
            watcher.Created += OnFileCreated;

            Console.WriteLine("Monitoring file system changes. Press any key to exit.");
            Console.ReadKey();

            // Save the updated largest folders data before exiting
            SaveLargestFolders();
        }

        static void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            // File created event handler
            string directory = Path.GetDirectoryName(e.FullPath);

            // Update the largest folders dictionary
            UpdateLargestFolders(directory);
        }

        static void UpdateLargestFolders(string folderPath)
        {
            long folderSize = GetDirectorySize(folderPath);

            // Update the largest folders dictionary
            largestFolders[folderPath] = folderSize;

            // Save the updated largest folders data
            SaveLargestFolders();
        }
 
        static void LoadLargestFolders()
        {
            if (File.Exists(largestFoldersFile))
            {
                // Load largest folders data from file
                string[] lines = File.ReadAllLines(largestFoldersFile);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && long.TryParse(parts[1], out long size))
                    {
                        largestFolders[parts[0]] = size;
                    }
                }
            }
        }

        static void SaveLargestFolders()
        {
            // Save largest folders data to file
            List<string> lines = new List<string>();
            foreach (var kvp in largestFolders)
            {
                lines.Add($"{kvp.Key},{kvp.Value}");
            }
            File.WriteAllLines(largestFoldersFile, lines);
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
                Console.WriteLine("Error calculating folder size: " + ex.Message);
            }

            return size;
        }

        protected override void OnStop()
        {
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
                SerializeListToFile(largestFiles);
            });
        }

        static async void SerializeListToFile(List<LargestFileData> list)
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
                    Console.WriteLine($"Error serializing list: {ex.Message}");
                }
            });
        }

        // [END] Getting Large size files               /////////////////////////////////////////////////////


        // Getting LARGE SIZE FOLDERS [START]           /////////////////////////////////////////////////////

        static List<FolderData> GetFoldersWithLargestSize(int count)
        {
            List<FolderData> folders = new List<FolderData>();

            // Check if the file exists, if it does, deserialize the list from the file
            string path = @"C:\Program Files (x86)\SANDEEP\largestFolderList.txt2";

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

            return folders.OrderByDescending(f => f.FolderSize).Take(count).ToList();
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

        [Serializable]
        public class FolderData
        {
            public string FolderPath { get; set; }
            public long FolderSize { get; set; }
        }

        // GETTING LARGE SIZE FOLDERS [END]            /////////////////////////////////////////////////
    }
}
