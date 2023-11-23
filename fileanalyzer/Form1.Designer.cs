namespace fileanalyzer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listView1 = new System.Windows.Forms.ListView();
            this.results = new System.Windows.Forms.Label();
            this.deleteSelected = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.filesFound = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.smallFilesRadioButton = new System.Windows.Forms.RadioButton();
            this.largeFilesRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.appFilesCheckbox = new System.Windows.Forms.CheckBox();
            this.zipCheckbox = new System.Windows.Forms.CheckBox();
            this.appCheckbox = new System.Windows.Forms.CheckBox();
            this.documentCheckbox = new System.Windows.Forms.CheckBox();
            this.audioCheckbox = new System.Windows.Forms.CheckBox();
            this.allCheckbox = new System.Windows.Forms.CheckBox();
            this.otherCheckBox = new System.Windows.Forms.CheckBox();
            this.videoCheckBox = new System.Windows.Forms.CheckBox();
            this.imageCheckBox = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.analyze = new System.Windows.Forms.Button();
            this.findDuplicate = new System.Windows.Forms.Button();
            this.deleteDuplicate = new System.Windows.Forms.Button();
            this.clearRecycleBinButton = new System.Windows.Forms.Button();
            this.clearTempFolder1 = new System.Windows.Forms.Button();
            this.clearTempFolder2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.findOlderFilesButton = new System.Windows.Forms.Button();
            this.deleteOlderFilesButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.extension = new System.Windows.Forms.TextBox();
            this.deleteFilesWithExtension = new System.Windows.Forms.Button();
            this.findFilesWithExtension = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.evenOddButton = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("listView1.BackgroundImage")));
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(15, 303);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1895, 371);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // results
            // 
            this.results.AutoSize = true;
            this.results.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.results.Location = new System.Drawing.Point(15, 284);
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(59, 17);
            this.results.TabIndex = 6;
            this.results.Text = "Results:";
            // 
            // deleteSelected
            // 
            this.deleteSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteSelected.BackColor = System.Drawing.Color.White;
            this.deleteSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteSelected.Location = new System.Drawing.Point(887, 692);
            this.deleteSelected.Name = "deleteSelected";
            this.deleteSelected.Size = new System.Drawing.Size(212, 35);
            this.deleteSelected.TabIndex = 1;
            this.deleteSelected.Text = "Delete";
            this.deleteSelected.UseVisualStyleBackColor = false;
            this.deleteSelected.Click += new System.EventHandler(this.deleteSelected_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.filesFound);
            this.groupBox4.Location = new System.Drawing.Point(21, 95);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(129, 75);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Total files found: ";
            // 
            // filesFound
            // 
            this.filesFound.AutoSize = true;
            this.filesFound.Location = new System.Drawing.Point(27, 33);
            this.filesFound.Name = "filesFound";
            this.filesFound.Size = new System.Drawing.Size(68, 17);
            this.filesFound.TabIndex = 4;
            this.filesFound.Text = "------------";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.smallFilesRadioButton);
            this.groupBox2.Controls.Add(this.largeFilesRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(156, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 75);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sort by";
            // 
            // smallFilesRadioButton
            // 
            this.smallFilesRadioButton.AutoSize = true;
            this.smallFilesRadioButton.Checked = true;
            this.smallFilesRadioButton.Location = new System.Drawing.Point(202, 34);
            this.smallFilesRadioButton.Name = "smallFilesRadioButton";
            this.smallFilesRadioButton.Size = new System.Drawing.Size(93, 21);
            this.smallFilesRadioButton.TabIndex = 1;
            this.smallFilesRadioButton.TabStop = true;
            this.smallFilesRadioButton.Text = "Small Files";
            this.smallFilesRadioButton.UseVisualStyleBackColor = true;
            // 
            // largeFilesRadioButton
            // 
            this.largeFilesRadioButton.AutoSize = true;
            this.largeFilesRadioButton.Location = new System.Drawing.Point(45, 34);
            this.largeFilesRadioButton.Name = "largeFilesRadioButton";
            this.largeFilesRadioButton.Size = new System.Drawing.Size(96, 21);
            this.largeFilesRadioButton.TabIndex = 0;
            this.largeFilesRadioButton.Text = "Large Files";
            this.largeFilesRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.appFilesCheckbox);
            this.groupBox3.Controls.Add(this.zipCheckbox);
            this.groupBox3.Controls.Add(this.appCheckbox);
            this.groupBox3.Controls.Add(this.documentCheckbox);
            this.groupBox3.Controls.Add(this.audioCheckbox);
            this.groupBox3.Controls.Add(this.allCheckbox);
            this.groupBox3.Controls.Add(this.otherCheckBox);
            this.groupBox3.Controls.Add(this.videoCheckBox);
            this.groupBox3.Controls.Add(this.imageCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(534, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 141);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File Types";
            // 
            // appFilesCheckbox
            // 
            this.appFilesCheckbox.AutoSize = true;
            this.appFilesCheckbox.Location = new System.Drawing.Point(118, 71);
            this.appFilesCheckbox.Name = "appFilesCheckbox";
            this.appFilesCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.appFilesCheckbox.Size = new System.Drawing.Size(81, 21);
            this.appFilesCheckbox.TabIndex = 2;
            this.appFilesCheckbox.Text = "App files";
            this.appFilesCheckbox.UseVisualStyleBackColor = true;
            // 
            // zipCheckbox
            // 
            this.zipCheckbox.AutoSize = true;
            this.zipCheckbox.Location = new System.Drawing.Point(16, 108);
            this.zipCheckbox.Name = "zipCheckbox";
            this.zipCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.zipCheckbox.Size = new System.Drawing.Size(47, 21);
            this.zipCheckbox.TabIndex = 2;
            this.zipCheckbox.Text = "Zip";
            this.zipCheckbox.UseVisualStyleBackColor = true;
            // 
            // appCheckbox
            // 
            this.appCheckbox.AutoSize = true;
            this.appCheckbox.Location = new System.Drawing.Point(212, 71);
            this.appCheckbox.Name = "appCheckbox";
            this.appCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.appCheckbox.Size = new System.Drawing.Size(59, 21);
            this.appCheckbox.TabIndex = 2;
            this.appCheckbox.Text = "Apps";
            this.appCheckbox.UseVisualStyleBackColor = true;
            // 
            // documentCheckbox
            // 
            this.documentCheckbox.AutoSize = true;
            this.documentCheckbox.Location = new System.Drawing.Point(16, 71);
            this.documentCheckbox.Name = "documentCheckbox";
            this.documentCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.documentCheckbox.Size = new System.Drawing.Size(98, 21);
            this.documentCheckbox.TabIndex = 2;
            this.documentCheckbox.Text = "Documents";
            this.documentCheckbox.UseVisualStyleBackColor = true;
            // 
            // audioCheckbox
            // 
            this.audioCheckbox.AutoSize = true;
            this.audioCheckbox.Location = new System.Drawing.Point(212, 34);
            this.audioCheckbox.Name = "audioCheckbox";
            this.audioCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.audioCheckbox.Size = new System.Drawing.Size(63, 21);
            this.audioCheckbox.TabIndex = 2;
            this.audioCheckbox.Text = "Audio";
            this.audioCheckbox.UseVisualStyleBackColor = true;
            // 
            // allCheckbox
            // 
            this.allCheckbox.AutoSize = true;
            this.allCheckbox.Location = new System.Drawing.Point(212, 108);
            this.allCheckbox.Name = "allCheckbox";
            this.allCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.allCheckbox.Size = new System.Drawing.Size(42, 21);
            this.allCheckbox.TabIndex = 2;
            this.allCheckbox.Text = "All";
            this.allCheckbox.UseVisualStyleBackColor = true;
            this.allCheckbox.Click += new System.EventHandler(this.checkAllFilters);
            // 
            // otherCheckBox
            // 
            this.otherCheckBox.AutoSize = true;
            this.otherCheckBox.Location = new System.Drawing.Point(118, 110);
            this.otherCheckBox.Name = "otherCheckBox";
            this.otherCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.otherCheckBox.Size = new System.Drawing.Size(63, 21);
            this.otherCheckBox.TabIndex = 2;
            this.otherCheckBox.Text = "Other";
            this.otherCheckBox.UseVisualStyleBackColor = true;
            // 
            // videoCheckBox
            // 
            this.videoCheckBox.AutoSize = true;
            this.videoCheckBox.Location = new System.Drawing.Point(118, 34);
            this.videoCheckBox.Name = "videoCheckBox";
            this.videoCheckBox.Size = new System.Drawing.Size(63, 21);
            this.videoCheckBox.TabIndex = 1;
            this.videoCheckBox.Text = "Video";
            this.videoCheckBox.UseVisualStyleBackColor = true;
            // 
            // imageCheckBox
            // 
            this.imageCheckBox.AutoSize = true;
            this.imageCheckBox.Checked = true;
            this.imageCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.imageCheckBox.Location = new System.Drawing.Point(16, 34);
            this.imageCheckBox.Name = "imageCheckBox";
            this.imageCheckBox.Size = new System.Drawing.Size(65, 21);
            this.imageCheckBox.TabIndex = 0;
            this.imageCheckBox.Text = "Image";
            this.imageCheckBox.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(134, 63);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(675, 23);
            this.progressBar.TabIndex = 2;
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(134, 27);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(715, 23);
            this.pathTextBox.TabIndex = 2;
            this.pathTextBox.Text = "C:\\Users\\SANDEEP\\Desktop\\sandeep";
            this.pathTextBox.Click += new System.EventHandler(this.openFolderBrowser);
            // 
            // analyze
            // 
            this.analyze.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.analyze.BackColor = System.Drawing.Color.Gold;
            this.analyze.Font = new System.Drawing.Font("Roboto Black", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analyze.Location = new System.Drawing.Point(875, 22);
            this.analyze.Name = "analyze";
            this.analyze.Size = new System.Drawing.Size(212, 42);
            this.analyze.TabIndex = 1;
            this.analyze.Text = "Analyze";
            this.analyze.UseVisualStyleBackColor = false;
            this.analyze.Click += new System.EventHandler(this.analyze_click);
            // 
            // findDuplicate
            // 
            this.findDuplicate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.findDuplicate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.findDuplicate.Font = new System.Drawing.Font("Roboto Black", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findDuplicate.Location = new System.Drawing.Point(875, 68);
            this.findDuplicate.Name = "findDuplicate";
            this.findDuplicate.Size = new System.Drawing.Size(212, 42);
            this.findDuplicate.TabIndex = 1;
            this.findDuplicate.Text = "Find Duplicate Files";
            this.findDuplicate.UseVisualStyleBackColor = false;
            this.findDuplicate.Click += new System.EventHandler(this.DisplayDuplicatesInListView);
            // 
            // deleteDuplicate
            // 
            this.deleteDuplicate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.deleteDuplicate.BackColor = System.Drawing.Color.Coral;
            this.deleteDuplicate.Font = new System.Drawing.Font("Roboto Black", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteDuplicate.Location = new System.Drawing.Point(875, 116);
            this.deleteDuplicate.Name = "deleteDuplicate";
            this.deleteDuplicate.Size = new System.Drawing.Size(212, 42);
            this.deleteDuplicate.TabIndex = 1;
            this.deleteDuplicate.Text = "Delete Duplicate Files";
            this.deleteDuplicate.UseVisualStyleBackColor = false;
            this.deleteDuplicate.Click += new System.EventHandler(this.deleteDuplicateFiles);
            // 
            // clearRecycleBinButton
            // 
            this.clearRecycleBinButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.clearRecycleBinButton.BackColor = System.Drawing.Color.Aquamarine;
            this.clearRecycleBinButton.Font = new System.Drawing.Font("Roboto Black", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearRecycleBinButton.Location = new System.Drawing.Point(1644, 27);
            this.clearRecycleBinButton.Name = "clearRecycleBinButton";
            this.clearRecycleBinButton.Size = new System.Drawing.Size(212, 42);
            this.clearRecycleBinButton.TabIndex = 1;
            this.clearRecycleBinButton.Text = "Clear Recycle Bin";
            this.clearRecycleBinButton.UseVisualStyleBackColor = false;
            this.clearRecycleBinButton.Click += new System.EventHandler(this.clearRecycleBin);
            // 
            // clearTempFolder1
            // 
            this.clearTempFolder1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.clearTempFolder1.BackColor = System.Drawing.Color.Aquamarine;
            this.clearTempFolder1.Font = new System.Drawing.Font("Roboto Black", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearTempFolder1.Location = new System.Drawing.Point(1644, 140);
            this.clearTempFolder1.Name = "clearTempFolder1";
            this.clearTempFolder1.Size = new System.Drawing.Size(212, 42);
            this.clearTempFolder1.TabIndex = 1;
            this.clearTempFolder1.Text = "Clear %Temp% Folder";
            this.clearTempFolder1.UseVisualStyleBackColor = false;
            this.clearTempFolder1.Click += new System.EventHandler(this.analyze_click);
            // 
            // clearTempFolder2
            // 
            this.clearTempFolder2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.clearTempFolder2.BackColor = System.Drawing.Color.Aquamarine;
            this.clearTempFolder2.Font = new System.Drawing.Font("Roboto Black", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearTempFolder2.Location = new System.Drawing.Point(1644, 84);
            this.clearTempFolder2.Name = "clearTempFolder2";
            this.clearTempFolder2.Size = new System.Drawing.Size(212, 42);
            this.clearTempFolder2.TabIndex = 1;
            this.clearTempFolder2.Text = "Clear Temp Folder";
            this.clearTempFolder2.UseVisualStyleBackColor = false;
            this.clearTempFolder2.Click += new System.EventHandler(this.EmptyTempFolderButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Progress:";
            // 
            // progressLabel
            // 
            this.progressLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(815, 66);
            this.progressLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(28, 17);
            this.progressLabel.TabIndex = 6;
            this.progressLabel.Text = "0%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Path to analyze:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dateTimePicker1);
            this.groupBox5.Controls.Add(this.findOlderFilesButton);
            this.groupBox5.Controls.Add(this.deleteOlderFilesButton);
            this.groupBox5.Location = new System.Drawing.Point(1110, 22);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(219, 136);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Files older than:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(9, 39);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(197, 23);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // findOlderFilesButton
            // 
            this.findOlderFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findOlderFilesButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.findOlderFilesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.findOlderFilesButton.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findOlderFilesButton.Location = new System.Drawing.Point(9, 85);
            this.findOlderFilesButton.Name = "findOlderFilesButton";
            this.findOlderFilesButton.Size = new System.Drawing.Size(90, 34);
            this.findOlderFilesButton.TabIndex = 1;
            this.findOlderFilesButton.Text = "Find";
            this.findOlderFilesButton.UseVisualStyleBackColor = false;
            this.findOlderFilesButton.Click += new System.EventHandler(this.findOldFiles);
            // 
            // deleteOlderFilesButton
            // 
            this.deleteOlderFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteOlderFilesButton.BackColor = System.Drawing.Color.Coral;
            this.deleteOlderFilesButton.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteOlderFilesButton.Location = new System.Drawing.Point(116, 85);
            this.deleteOlderFilesButton.Name = "deleteOlderFilesButton";
            this.deleteOlderFilesButton.Size = new System.Drawing.Size(90, 34);
            this.deleteOlderFilesButton.TabIndex = 1;
            this.deleteOlderFilesButton.Text = "Delete";
            this.deleteOlderFilesButton.UseVisualStyleBackColor = false;
            this.deleteOlderFilesButton.Click += new System.EventHandler(this.deleteOldFiles);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.clearTempFolder1);
            this.groupBox1.Controls.Add(this.clearTempFolder2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.progressLabel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.clearRecycleBinButton);
            this.groupBox1.Controls.Add(this.deleteDuplicate);
            this.groupBox1.Controls.Add(this.findDuplicate);
            this.groupBox1.Controls.Add(this.analyze);
            this.groupBox1.Controls.Add(this.pathTextBox);
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1895, 253);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Analyze the folder easily";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.extension);
            this.groupBox8.Controls.Add(this.deleteFilesWithExtension);
            this.groupBox8.Controls.Add(this.findFilesWithExtension);
            this.groupBox8.Location = new System.Drawing.Point(875, 169);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(454, 70);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Files with extentions:";
            // 
            // extension
            // 
            this.extension.Location = new System.Drawing.Point(17, 30);
            this.extension.Name = "extension";
            this.extension.Size = new System.Drawing.Size(141, 23);
            this.extension.TabIndex = 0;
            // 
            // deleteFilesWithExtension
            // 
            this.deleteFilesWithExtension.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteFilesWithExtension.BackColor = System.Drawing.Color.Coral;
            this.deleteFilesWithExtension.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteFilesWithExtension.Location = new System.Drawing.Point(316, 24);
            this.deleteFilesWithExtension.Name = "deleteFilesWithExtension";
            this.deleteFilesWithExtension.Size = new System.Drawing.Size(120, 34);
            this.deleteFilesWithExtension.TabIndex = 1;
            this.deleteFilesWithExtension.Text = "Delete";
            this.deleteFilesWithExtension.UseVisualStyleBackColor = false;
            this.deleteFilesWithExtension.Click += new System.EventHandler(this.deleteWithExtension);
            // 
            // findFilesWithExtension
            // 
            this.findFilesWithExtension.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findFilesWithExtension.BackColor = System.Drawing.Color.LightSkyBlue;
            this.findFilesWithExtension.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findFilesWithExtension.Location = new System.Drawing.Point(174, 24);
            this.findFilesWithExtension.Name = "findFilesWithExtension";
            this.findFilesWithExtension.Size = new System.Drawing.Size(124, 34);
            this.findFilesWithExtension.TabIndex = 1;
            this.findFilesWithExtension.Text = "Find";
            this.findFilesWithExtension.UseVisualStyleBackColor = false;
            this.findFilesWithExtension.Click += new System.EventHandler(this.findWithExtension);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Font = new System.Drawing.Font("Roboto Black", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(1644, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(212, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.analyze_click);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAllButton.BackColor = System.Drawing.Color.White;
            this.selectAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectAllButton.Location = new System.Drawing.Point(15, 692);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(128, 35);
            this.selectAllButton.TabIndex = 1;
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.UseVisualStyleBackColor = false;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // evenOddButton
            // 
            this.evenOddButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.evenOddButton.BackColor = System.Drawing.Color.White;
            this.evenOddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.evenOddButton.Location = new System.Drawing.Point(162, 692);
            this.evenOddButton.Name = "evenOddButton";
            this.evenOddButton.Size = new System.Drawing.Size(128, 35);
            this.evenOddButton.TabIndex = 1;
            this.evenOddButton.Text = "Select Even";
            this.evenOddButton.UseVisualStyleBackColor = false;
            this.evenOddButton.Click += new System.EventHandler(this.evenOddButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1924, 739);
            this.Controls.Add(this.results);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.evenOddButton);
            this.Controls.Add(this.selectAllButton);
            this.Controls.Add(this.deleteSelected);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Folder Analyzer";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label results;
        private System.Windows.Forms.Button deleteSelected;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label filesFound;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton smallFilesRadioButton;
        private System.Windows.Forms.RadioButton largeFilesRadioButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox otherCheckBox;
        private System.Windows.Forms.CheckBox videoCheckBox;
        private System.Windows.Forms.CheckBox imageCheckBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button analyze;
        private System.Windows.Forms.Button findDuplicate;
        private System.Windows.Forms.Button deleteDuplicate;
        private System.Windows.Forms.Button clearRecycleBinButton;
        private System.Windows.Forms.Button clearTempFolder1;
        private System.Windows.Forms.Button clearTempFolder2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button deleteOlderFilesButton;
        private System.Windows.Forms.Button findOlderFiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox extension;
        private System.Windows.Forms.Button deleteFilesWithExtension;
        private System.Windows.Forms.Button findFilesWithExtension;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button evenOddButton;
        private System.Windows.Forms.CheckBox appFilesCheckbox;
        private System.Windows.Forms.CheckBox appCheckbox;
        private System.Windows.Forms.CheckBox documentCheckbox;
        private System.Windows.Forms.CheckBox audioCheckbox;
        private System.Windows.Forms.CheckBox zipCheckbox;
        private System.Windows.Forms.CheckBox allCheckbox;
        private System.Windows.Forms.Button findOlderFilesButton;
    }
}

