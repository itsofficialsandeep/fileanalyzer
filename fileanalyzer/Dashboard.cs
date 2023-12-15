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
        string csvDiskInfoFilePath = @"C:\Program Files (x86)\Sandeep Kumar\Sandy's Windows Cleaner\edisk_information.txt";

        // this variable will be used to access the line number for edisk_information.tx file
        int DiskInfoLine = 0;

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
                //DriveInfo drive = new DriveInfo(@"G:\");
                Panel drivePanel = new Panel();
                drivePanel.Dock = DockStyle.Left | DockStyle.Top;
                drivePanel.Anchor = AnchorStyles.Left;

                EnsurePanelVisible(drivePanel);

                // Set panel properties
                drivePanel.BackColor = Color.LightGray; // Set panel background color

                drivePanel.Size = new Size(250, 350); // Increased size for additional labels
                drivePanel.Location = new Point(x, y);
                drivePanel.BorderStyle = BorderStyle.FixedSingle;
                drivePanel.BackColor = Color.White;

                Label totalSizeLabel = new Label();
                totalSizeLabel.Text = ConvertBytes(drive.TotalSize);
                totalSizeLabel.Location = new Point(110, 35);
                totalSizeLabel.Font = new Font("Roboto", 17, FontStyle.Bold);
                totalSizeLabel.BackColor = Color.Transparent;
                totalSizeLabel.ForeColor = Color.DarkBlue;
                totalSizeLabel.Height = 30;
                totalSizeLabel.Width = 200;
                drivePanel.Controls.Add(totalSizeLabel);

                Label driveLabel = new Label();
                driveLabel.Text = drive.Name;
                driveLabel.Location = new Point(10, 10); // new Point(15, 110);
                driveLabel.Font = new Font("Roboto", 45, FontStyle.Bold);
                driveLabel.Height = 60;
                drivePanel.Controls.Add(driveLabel);

                Label usedSpaceLabel = new Label();
                usedSpaceLabel.Text = ConvertBytes(drive.TotalSize - drive.TotalFreeSpace);
                usedSpaceLabel.Location = new Point(20, 90);
                usedSpaceLabel.Font = new Font("Roboto", 10, FontStyle.Bold);
                usedSpaceLabel.BackColor = Color.Transparent;
                usedSpaceLabel.ForeColor = Color.FromArgb(0x42, 0x43, 0x4e);
                drivePanel.Controls.Add(usedSpaceLabel);

                Label freeSpaceLabel = new Label();
                freeSpaceLabel.Text = ConvertBytes(drive.TotalFreeSpace);
                freeSpaceLabel.Location = new Point(150, 90);
                freeSpaceLabel.Font = new Font("Roboto", 10, FontStyle.Bold);
                freeSpaceLabel.BackColor = Color.Transparent;
                freeSpaceLabel.Padding = new Padding(0);
                freeSpaceLabel.ForeColor = Color.FromArgb(0x42, 0x43, 0x4e);
                drivePanel.Controls.Add(freeSpaceLabel);

                // Use System.Windows.Forms.ProgressBar
                System.Windows.Forms.ProgressBar spaceProgressBar = new System.Windows.Forms.ProgressBar();
                spaceProgressBar.Maximum = 100;
                spaceProgressBar.Minimum = 0;
                spaceProgressBar.Value = (int)((drive.TotalSize - drive.AvailableFreeSpace) * 100 / drive.TotalSize);
                spaceProgressBar.Location = new Point(20, 120);
                spaceProgressBar.Width = 205;
                spaceProgressBar.Height = 6;
                spaceProgressBar.Style = ProgressBarStyle.Continuous;
                spaceProgressBar.MarqueeAnimationSpeed = 0;
                drivePanel.Controls.Add(spaceProgressBar);

                // Find your GroupBox (replace "groupBox1" with the actual name of your GroupBox)
                Panel myBackPanel = backpanel;

                // Assuming you have a panel named panel1
                // backpanel.AutoScroll = true;
                myBackPanel.HorizontalScroll.Visible = true;

                // Add the panel to the GroupBox
                myBackPanel.Controls.Add(drivePanel);

                // NOTE: In case of C: drive, search only specific directories like Picture, Music, AppData not all the directories..

                if (File.Exists(csvDiskInfoFilePath))
                {
                   // diskInfo(drive.Name);
                    displayDisks(ReadDiskInformationFromCSV(csvDiskInfoFilePath, drive.Name), drivePanel);
                }

                //                Thread thread = new Thread(() => diskInfo(drivePanel, @"E:\SANDEEP_KUMAR", drive));
                //                thread.Start();

                x += 280;
            }
        }

        private void diskInfo(string disk)
        {
            try
            {
                long fileCount = 0;
                long imageCount = 0;
                long videoCount = 0;
                long docCount = 0;
                long totalImageSize = 0;
                long totalVideoSize = 0;
                long totalDocSize = 0;

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
                MessageBox.Show(ex.Message + "---" + ex.StackTrace);
               // justLoggging("Dashboard.cs - 195:" + ex.Message + "---" + ex.StackTrace);
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

        public List<DiskInformation> ReadDiskInformationFromCSV(string filePath, string disk)
        {
            List<DiskInformation> diskInfoList = new List<DiskInformation>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        // Parsing values from CSV
                        DiskInformation diskInfo = new DiskInformation
                        {
                            Disk = values[0],
                            FileCount = long.Parse(values[1]),
                            ImageCount = long.Parse(values[2]),
                            VideoCount = long.Parse(values[3]),
                            DocCount = long.Parse(values[4]),
                            TotalImageSize = long.Parse(values[5]),
                            TotalVideoSize = long.Parse(values[6]),
                            TotalDocSize = long.Parse(values[7])
                        };

                        diskInfoList.Add(diskInfo);
                    }                                        
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading CSV: " + ex.Message);
            }

            return diskInfoList;
        }

        public void displayDisks(List<DiskInformation> diskInfoList, Control control)
        {
            try
            {
                DriveInfo driveInfo = new DriveInfo(diskInfoList[DiskInfoLine].Disk);

                // DETAILS OF SPACE USED BY IMAGES
                PictureBox imagespictureBox = new PictureBox();
                imagespictureBox.Location = new Point(15, 150);
                imagespictureBox.Height = 30;
                imagespictureBox.Width = 30;
                imagespictureBox.Image = iconForDrives.Images[0]; ;
                control.Controls.Add(imagespictureBox);

                Label imagesLabel = new Label();
                imagesLabel.Text = "Images";
                imagesLabel.Location = new Point(50, 150);
                imagesLabel.Font = new Font("RobotoRegular", 12);
                imagesLabel.BackColor = Color.Transparent;
                imagesLabel.ForeColor = Color.FromArgb(0x42, 0x43, 0x4e);
                imagesLabel.Height = 20;
                imagesLabel.Width = 90;
                control.Controls.Add(imagesLabel);

                Label imagesCount = new Label();
                imagesCount.Text = diskInfoList[DiskInfoLine].ImageCount + " files";
                imagesCount.Location = new Point(52, 173);
                imagesCount.Font = new Font("Roboto", 9, FontStyle.Bold);
                imagesCount.BackColor = Color.Transparent;
                imagesCount.ForeColor = Color.FromArgb(0x8d, 0x94, 0xbc);
                imagesCount.Height = 13;
                imagesCount.Width = 80;
                control.Controls.Add(imagesCount);

                Label imagesSpaceTaken = new Label();
                imagesSpaceTaken.Text = ConvertBytes(diskInfoList[DiskInfoLine].TotalImageSize) + "";
                imagesSpaceTaken.Location = new Point(140, 160);
                imagesSpaceTaken.Font = new Font("Roboto", 12, FontStyle.Bold);
                imagesSpaceTaken.BackColor = Color.Transparent;
                imagesSpaceTaken.ForeColor = Color.DarkBlue;
                imagesSpaceTaken.Height = 22;
                imagesSpaceTaken.Width = 100;
                imagesSpaceTaken.Anchor = AnchorStyles.Right;
                control.Controls.Add(imagesSpaceTaken);

                System.Windows.Forms.ProgressBar ImagespaceProgressBar = new System.Windows.Forms.ProgressBar();
                ImagespaceProgressBar.Maximum = 100;
                ImagespaceProgressBar.Minimum = 0;
                ImagespaceProgressBar.Value = (int)(diskInfoList[DiskInfoLine].TotalImageSize * 100 / (driveInfo.TotalSize - driveInfo.AvailableFreeSpace));
                ImagespaceProgressBar.Location = new Point(20, 190);
                ImagespaceProgressBar.Width = 205;
                ImagespaceProgressBar.Height = 1;
                ImagespaceProgressBar.Style = ProgressBarStyle.Continuous;
                ImagespaceProgressBar.MarqueeAnimationSpeed = 0;
                control.Controls.Add(ImagespaceProgressBar);

                // DETAILS OF SPACE USED BY VIDEOS
                PictureBox videospictureBox = new PictureBox();
                videospictureBox.Location = new Point(15, 210);
                videospictureBox.Height = 30;
                videospictureBox.Width = 30;
                videospictureBox.Image = iconForDrives.Images[4];
                control.Controls.Add(videospictureBox);

                Label videosLabel = new Label();
                videosLabel.Text = "Videos";
                videosLabel.Location = new Point(50, 210);
                videosLabel.Font = new Font("RobotoRegular", 12);
                videosLabel.BackColor = Color.Transparent;
                videosLabel.ForeColor = Color.FromArgb(0x42, 0x43, 0x4e);
                videosLabel.Height = 20;
                videosLabel.Width = 90;
                control.Controls.Add(videosLabel);

                Label videosCount = new Label();
                videosCount.Text = diskInfoList[DiskInfoLine].VideoCount + " files";
                videosCount.Location = new Point(52, 233);
                videosCount.Font = new Font("Roboto", 9, FontStyle.Bold);
                videosCount.BackColor = Color.Transparent;
                videosCount.ForeColor = Color.FromArgb(0x8d, 0x94, 0xbc);
                videosCount.Height = 13;
                videosCount.Width = 80;
                control.Controls.Add(videosCount);

                Label videosSpaceTaken = new Label();
                videosSpaceTaken.Text = ConvertBytes(diskInfoList[DiskInfoLine].TotalVideoSize) + "";
                videosSpaceTaken.Location = new Point(140, 220);
                videosSpaceTaken.Font = new Font("Roboto", 12, FontStyle.Bold);
                videosSpaceTaken.BackColor = Color.Transparent;
                videosSpaceTaken.ForeColor = Color.DarkBlue;
                videosSpaceTaken.Height = 22;
                videosSpaceTaken.Width = 100;
                videosSpaceTaken.Anchor = AnchorStyles.Right;
                control.Controls.Add(videosSpaceTaken);

                System.Windows.Forms.ProgressBar videospaceProgressBar = new System.Windows.Forms.ProgressBar();
                videospaceProgressBar.Maximum = 100;
                videospaceProgressBar.Minimum = 0;
                videospaceProgressBar.Value = (int)(diskInfoList[DiskInfoLine].TotalVideoSize * 100 / (driveInfo.TotalSize - driveInfo.TotalFreeSpace));
                videospaceProgressBar.Location = new Point(20, 250);
                videospaceProgressBar.Width = 205;
                videospaceProgressBar.Height = 1;
                videospaceProgressBar.Style = ProgressBarStyle.Continuous;
                videospaceProgressBar.MarqueeAnimationSpeed = 0;
                control.Controls.Add(videospaceProgressBar);

                // DETAILS OF SPACE USED BY docs
                PictureBox docspictureBox = new PictureBox();
                docspictureBox.Location = new Point(15, 270);
                docspictureBox.Height = 30;
                docspictureBox.Width = 30;
                docspictureBox.Image = iconForDrives.Images[1];
                control.Controls.Add(docspictureBox);

                Label docsLabel = new Label();
                docsLabel.Text = "Docs";
                docsLabel.Location = new Point(50, 270);
                docsLabel.Font = new Font("RobotoRegular", 12);
                docsLabel.BackColor = Color.Transparent;
                docsLabel.ForeColor = Color.FromArgb(0x42, 0x43, 0x4e);
                docsLabel.Height = 20;
                docsLabel.Width = 90;
                control.Controls.Add(docsLabel);

                Label docsCount = new Label();
                docsCount.Text = diskInfoList[DiskInfoLine].DocCount + " files";
                docsCount.Location = new Point(52, 293);
                docsCount.Font = new Font("Roboto", 9, FontStyle.Bold);
                docsCount.BackColor = Color.Transparent;
                docsCount.ForeColor = Color.FromArgb(0x8d, 0x94, 0xbc);
                docsCount.Height = 13;
                docsCount.Width = 80;
                control.Controls.Add(docsCount);

                Label docsSpaceTaken = new Label();
                docsSpaceTaken.Text = ConvertBytes(diskInfoList[DiskInfoLine].TotalDocSize) + "";
                docsSpaceTaken.Location = new Point(140, 280);
                docsSpaceTaken.Font = new Font("Roboto", 12, FontStyle.Bold);
                docsSpaceTaken.BackColor = Color.Transparent;
                docsSpaceTaken.ForeColor = Color.DarkBlue;
                docsSpaceTaken.Height = 22;
                docsSpaceTaken.Width = 100;
                docsSpaceTaken.Anchor = AnchorStyles.Right;
                control.Controls.Add(docsSpaceTaken);

                System.Windows.Forms.ProgressBar docspaceProgressBar = new System.Windows.Forms.ProgressBar();
                docspaceProgressBar.Maximum = 100;
                docspaceProgressBar.Minimum = 0;
                docspaceProgressBar.Value = (int)(diskInfoList[DiskInfoLine].TotalDocSize * 100 / (driveInfo.TotalSize - driveInfo.TotalFreeSpace));
                docspaceProgressBar.Location = new Point(20, 310);
                docspaceProgressBar.Width = 205;
                docspaceProgressBar.Height = 1;
                docspaceProgressBar.Style = ProgressBarStyle.Continuous;
                docspaceProgressBar.MarqueeAnimationSpeed = 0;
                control.Controls.Add(docspaceProgressBar);

                // increment so that it can display information of next Drive by fetching the next line from edisk_information.txt
                DiskInfoLine++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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

        static string ConvertBytes(long bytes)
        {
            const long kilobyte = 1024;
            const long megabyte = kilobyte * 1024;
            const long gigabyte = megabyte * 1024;

            if (bytes < kilobyte)
            {
                return $"{bytes} Bytes";
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


        private void EnsurePanelVisible(Panel panel)
        {
            // Get the form's client rectangle to determine visible area
            Rectangle screenRect = this.ClientRectangle;

            // Check if the panel's location is out of bounds
            if (!screenRect.Contains(panel.Bounds))
            {
                // Adjust panel position to keep it within the visible area
                int newX = Math.Min(Math.Max(panel.Left, screenRect.Left), screenRect.Right - panel.Width);
                int newY = Math.Min(Math.Max(panel.Top, screenRect.Top), screenRect.Bottom - panel.Height);

                panel.Location = new Point(newX, newY);
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
                    writer.WriteLine("<h2>Latest Exception: " + DateTime.Now.ToString() + "</h2>");
                    writer.WriteLine("<p><strong>Message:</strong> " + "Now " + ":" + line + "</p>");
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
