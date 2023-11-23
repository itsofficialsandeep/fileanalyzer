using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fileanalyzer
{
    public partial class Form3 : Form
    {

        private void GenerateDriveCards()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            int x = 20, y = 20;

            foreach (DriveInfo drive in allDrives)
            {
                Panel drivePanel = new Panel();
                drivePanel.Size = new Size(200, 200); // Increased size for additional labels
                drivePanel.Location = new Point(x, y);
                drivePanel.BorderStyle = BorderStyle.FixedSingle;

                Label driveLabel = new Label();
                driveLabel.Text = drive.Name;
                driveLabel.Location = new Point(10, 10);
                driveLabel.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
                drivePanel.Controls.Add(driveLabel);

                // Labels for total and available space
                // ...

                ProgressBar spaceProgressBar = new ProgressBar();
                spaceProgressBar.Maximum = 100;
                spaceProgressBar.Minimum = 0;
                spaceProgressBar.Value = (int)((drive.TotalSize - drive.AvailableFreeSpace) * 100 / drive.TotalSize);
                spaceProgressBar.Location = new Point(10, 80);
                spaceProgressBar.Width = 170;
                drivePanel.Controls.Add(spaceProgressBar);

                int imageCount = 10;
                int documentCount = 10;

                // LOGIC HERE PLEASE

                Label imageCountLabel = new Label();
                imageCountLabel.Text = $"Images: {imageCount}";
                imageCountLabel.Location = new Point(10, 120);
                drivePanel.Controls.Add(imageCountLabel);

                Label documentCountLabel = new Label();
                documentCountLabel.Text = $"Documents: {documentCount}";
                documentCountLabel.Location = new Point(10, 150);
                drivePanel.Controls.Add(documentCountLabel);

                this.Controls.Add(drivePanel);
                x += 220;
            }
        }

        private void DiskAnalyzeButton_Click(object sender, EventArgs e)
        {
            try
            {
                int fileCount = 0;
                int imageCount = 0;
                int videoCount = 0;
                int docCount = 0;
                int otherCount = 0;

                TraverseTreeParallelForEach(@"E:\SANDEEP_KUMAR\PIC", (f) =>
                {
                    // Exceptions are no-ops.
                    try
                    {
                        // Count all files
                        Interlocked.Increment(ref fileCount);

                        string extension = Path.GetExtension(f)?.ToLower();
                        if (!string.IsNullOrEmpty(extension))
                        {
                            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                            {
                                // Image file
                                Interlocked.Increment(ref imageCount);
                            }
                            else if (extension == ".mp4" || extension == ".avi" || extension == ".mkv")
                            {
                                // Video file
                                Interlocked.Increment(ref videoCount);
                            }
                            else if (extension == ".doc" || extension == ".docx" || extension == ".pdf" || extension == ".txt")
                            {
                                // Document file
                                Interlocked.Increment(ref docCount);
                            }
                            else
                            {
                                // Other files
                                Interlocked.Increment(ref otherCount);
                            }
                        }
                    }
                    catch (FileNotFoundException) { }
                    catch (IOException) { }
                    catch (UnauthorizedAccessException) { }
                    catch (System.Security.SecurityException) { }
                });

                // Display counts or use them as needed
                MessageBox.Show($"Total Files: {fileCount}\nImage Files: {imageCount}\nVideo Files: {videoCount}\nDocument Files: {docCount}\nOther Files: {otherCount}");
            }
            catch (ArgumentException)
            {
                MessageBox.Show(@"The directory 'C:\Program Files' does not exist.");
            }
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
                throw new ArgumentException(
                    "The given root directory doesn't exist.", nameof(root));
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
