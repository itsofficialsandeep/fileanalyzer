using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    }
}
