using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace fileanalyzer
{
    public partial class Form3 : Form
    {

        // function to make corner round
        private void makeCornerRound(Control control, int borderRadius)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, borderRadius * 2, borderRadius * 2, 180, 90); // Top-left corner
                path.AddArc(control.Width - borderRadius * 2, 0, borderRadius * 2, borderRadius * 2, 270, 90); // Top-right corner
                path.AddArc(control.Width - borderRadius * 2, control.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90); // Bottom-right corner
                path.AddArc(0, control.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90); // Bottom-left corner
                path.CloseFigure();

                control.Region = new Region(path);
            }
        }
        private void GenerateDriveCards()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            int x = 20, y = 25;

            foreach (DriveInfo drive in allDrives)
            {
                Panel drivePanel = new Panel();
                // Set panel properties
                drivePanel.BackColor = Color.LightGray; // Set panel background color

                drivePanel.Size = new Size(250, 350); // Increased size for additional labels
                drivePanel.Location = new Point(x, y);
                drivePanel.BorderStyle = BorderStyle.FixedSingle;
                drivePanel.BackColor = Color.White;
               // drivePanel.ForeColor = Color.White;

              //  makeCornerRound(drivePanel, 5);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = new Point(20, 30);
                pictureBox.Height = 65;
                pictureBox.Width = 65;
                pictureBox.ImageLocation = "E:\\SANDEEP_KUMAR\\PROJECT\\desktop\\fileanalyzer\\icon.jpg";
                drivePanel.Controls.Add(pictureBox);

                Label totalSizeLabel = new Label();
                totalSizeLabel.Text = ConvertBytes(drive.TotalSize);
                totalSizeLabel.Location = new Point(110, 50);
                totalSizeLabel.Font = new Font(FontFamily.GenericSansSerif, 17, FontStyle.Bold);
                totalSizeLabel.BackColor = Color.Transparent;
                totalSizeLabel.ForeColor = Color.DarkBlue;
                totalSizeLabel.Height = 60;
                totalSizeLabel.Width = 200;
                drivePanel.Controls.Add(totalSizeLabel);


                Label driveLabel = new Label();
                driveLabel.Text = drive.Name + " Drive";
                driveLabel.Location = new Point(15, 110);
                driveLabel.Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold);
                drivePanel.Controls.Add(driveLabel);

                Label usedSpaceLabel = new Label();
                usedSpaceLabel.Text = ConvertBytes(drive.TotalSize - drive.TotalFreeSpace);
                usedSpaceLabel.Location = new Point(20, 165);
                usedSpaceLabel.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                usedSpaceLabel.BackColor = Color.Transparent;
                usedSpaceLabel.ForeColor = Color.DarkGray;
                drivePanel.Controls.Add(usedSpaceLabel);

                Label freeSpaceLabel = new Label();
                freeSpaceLabel.Text = ConvertBytes(drive.TotalFreeSpace);
                freeSpaceLabel.Location = new Point(150, 165);
                freeSpaceLabel.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                freeSpaceLabel.BackColor = Color.Transparent;
                freeSpaceLabel.Padding = new Padding(0);
                freeSpaceLabel.ForeColor = Color.DarkGray;
                drivePanel.Controls.Add(freeSpaceLabel);

                // Use System.Windows.Forms.ProgressBar
                System.Windows.Forms.ProgressBar spaceProgressBar = new System.Windows.Forms.ProgressBar();
                spaceProgressBar.Maximum = 100;
                spaceProgressBar.Minimum = 0;
                spaceProgressBar.Value = (int)((drive.TotalSize - drive.AvailableFreeSpace) * 100 / drive.TotalSize);
                spaceProgressBar.Location = new Point(20, 190);
                spaceProgressBar.Width = 205;
                spaceProgressBar.Height = 6;
                spaceProgressBar.Style = ProgressBarStyle.Continuous;
                spaceProgressBar.MarqueeAnimationSpeed = 0;
                drivePanel.Controls.Add(spaceProgressBar);

                // Find your GroupBox (replace "groupBox1" with the actual name of your GroupBox)
                Panel myBackPanel = backpanel;

                // Add the panel to the GroupBox
                myBackPanel.Controls.Add(drivePanel);

                
                x += 280;
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

        static string ConvertBytes(long bytes)
        {
            const long kilobyte = 1024;
            const long megabyte = kilobyte * 1024;
            const long gigabyte = megabyte * 1024;

            if (bytes < kilobyte)
            {
                return $"{bytes} B";
            }
            else if (bytes < megabyte)
            {
                return $"{bytes / (double)kilobyte:F2} KB";
            }
            else if (bytes < gigabyte)
            {
                return $"{bytes / (double)megabyte:F2} MB";
            }
            else
            {
                return $"{bytes / (double)gigabyte:F2} GB";
            }
        }



    }
}
