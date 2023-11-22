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
            this.otherCheckBox = new System.Windows.Forms.CheckBox();
            this.videoCheckBox = new System.Windows.Forms.CheckBox();
            this.imageCheckBox = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.analyze = new System.Windows.Forms.Button();
            this.findDuplicate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.clearRecycleBin = new System.Windows.Forms.Button();
            this.clearTempFolder1 = new System.Windows.Forms.Button();
            this.clearTempFolder2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.deleteOlderFiles = new System.Windows.Forms.Button();
            this.findOlderFiles = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.largeFileValue = new System.Windows.Forms.NumericUpDown();
            this.deleteLargerFiles = new System.Windows.Forms.Button();
            this.findLargerFiles = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.deleteFilesWithExtension = new System.Windows.Forms.Button();
            this.findFilesWithExtension = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.smallFilesValue = new System.Windows.Forms.NumericUpDown();
            this.deleteSmallerFiles = new System.Windows.Forms.Button();
            this.findSmallerFiles = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.evenOddButton = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.largeFileValue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smallFilesValue)).BeginInit();
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
            this.groupBox3.Controls.Add(this.otherCheckBox);
            this.groupBox3.Controls.Add(this.videoCheckBox);
            this.groupBox3.Controls.Add(this.imageCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(534, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 75);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File Types";
            // 
            // otherCheckBox
            // 
            this.otherCheckBox.AutoSize = true;
            this.otherCheckBox.Location = new System.Drawing.Point(220, 34);
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
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button2.BackColor = System.Drawing.Color.Coral;
            this.button2.Font = new System.Drawing.Font("Roboto Black", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(875, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(212, 42);
            this.button2.TabIndex = 1;
            this.button2.Text = "Delete Duplicate Files";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.analyze_click);
            // 
            // clearRecycleBin
            // 
            this.clearRecycleBin.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.clearRecycleBin.BackColor = System.Drawing.Color.Aquamarine;
            this.clearRecycleBin.Font = new System.Drawing.Font("Roboto Black", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearRecycleBin.Location = new System.Drawing.Point(1644, 27);
            this.clearRecycleBin.Name = "clearRecycleBin";
            this.clearRecycleBin.Size = new System.Drawing.Size(212, 42);
            this.clearRecycleBin.TabIndex = 1;
            this.clearRecycleBin.Text = "Clear Recycle Bin";
            this.clearRecycleBin.UseVisualStyleBackColor = false;
            this.clearRecycleBin.Click += new System.EventHandler(this.analyze_click);
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
            this.clearTempFolder1.Text = "Clear Temp Files";
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
            this.clearTempFolder2.Text = "Clear Temp Files";
            this.clearTempFolder2.UseVisualStyleBackColor = false;
            this.clearTempFolder2.Click += new System.EventHandler(this.analyze_click);
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
            this.progressLabel.Location = new System.Drawing.Point(815, 70);
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
            this.groupBox5.Controls.Add(this.deleteOlderFiles);
            this.groupBox5.Controls.Add(this.findOlderFiles);
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
            // deleteOlderFiles
            // 
            this.deleteOlderFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteOlderFiles.BackColor = System.Drawing.Color.Coral;
            this.deleteOlderFiles.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteOlderFiles.Location = new System.Drawing.Point(123, 85);
            this.deleteOlderFiles.Name = "deleteOlderFiles";
            this.deleteOlderFiles.Size = new System.Drawing.Size(90, 34);
            this.deleteOlderFiles.TabIndex = 1;
            this.deleteOlderFiles.Text = "Delete";
            this.deleteOlderFiles.UseVisualStyleBackColor = false;
            this.deleteOlderFiles.Click += new System.EventHandler(this.analyze_click);
            // 
            // findOlderFiles
            // 
            this.findOlderFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findOlderFiles.BackColor = System.Drawing.Color.LightSkyBlue;
            this.findOlderFiles.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findOlderFiles.Location = new System.Drawing.Point(9, 85);
            this.findOlderFiles.Name = "findOlderFiles";
            this.findOlderFiles.Size = new System.Drawing.Size(104, 34);
            this.findOlderFiles.TabIndex = 1;
            this.findOlderFiles.Text = "Find";
            this.findOlderFiles.UseVisualStyleBackColor = false;
            this.findOlderFiles.Click += new System.EventHandler(this.analyze_click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.largeFileValue);
            this.groupBox6.Controls.Add(this.deleteLargerFiles);
            this.groupBox6.Controls.Add(this.findLargerFiles);
            this.groupBox6.Location = new System.Drawing.Point(1356, 22);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(258, 100);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Files larger than:";
            this.groupBox6.Enter += new System.EventHandler(this.groupBox6_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(186, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "MB";
            // 
            // largeFileValue
            // 
            this.largeFileValue.Location = new System.Drawing.Point(24, 26);
            this.largeFileValue.Name = "largeFileValue";
            this.largeFileValue.Size = new System.Drawing.Size(156, 23);
            this.largeFileValue.TabIndex = 2;
            // 
            // deleteLargerFiles
            // 
            this.deleteLargerFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteLargerFiles.BackColor = System.Drawing.Color.Coral;
            this.deleteLargerFiles.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteLargerFiles.Location = new System.Drawing.Point(133, 59);
            this.deleteLargerFiles.Name = "deleteLargerFiles";
            this.deleteLargerFiles.Size = new System.Drawing.Size(91, 34);
            this.deleteLargerFiles.TabIndex = 1;
            this.deleteLargerFiles.Text = "Delete";
            this.deleteLargerFiles.UseVisualStyleBackColor = false;
            this.deleteLargerFiles.Click += new System.EventHandler(this.analyze_click);
            // 
            // findLargerFiles
            // 
            this.findLargerFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findLargerFiles.BackColor = System.Drawing.Color.LightSkyBlue;
            this.findLargerFiles.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findLargerFiles.Location = new System.Drawing.Point(24, 59);
            this.findLargerFiles.Name = "findLargerFiles";
            this.findLargerFiles.Size = new System.Drawing.Size(103, 34);
            this.findLargerFiles.TabIndex = 1;
            this.findLargerFiles.Text = "Find";
            this.findLargerFiles.UseVisualStyleBackColor = false;
            this.findLargerFiles.Click += new System.EventHandler(this.analyze_click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.clearTempFolder1);
            this.groupBox1.Controls.Add(this.clearTempFolder2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.progressLabel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.clearRecycleBin);
            this.groupBox1.Controls.Add(this.button2);
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
            this.groupBox8.Controls.Add(this.textBox1);
            this.groupBox8.Controls.Add(this.deleteFilesWithExtension);
            this.groupBox8.Controls.Add(this.findFilesWithExtension);
            this.groupBox8.Location = new System.Drawing.Point(875, 169);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(454, 70);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Files with extentions:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(141, 23);
            this.textBox1.TabIndex = 0;
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
            this.deleteFilesWithExtension.Click += new System.EventHandler(this.analyze_click);
            // 
            // findFilesWithExtension
            // 
            this.findFilesWithExtension.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findFilesWithExtension.BackColor = System.Drawing.Color.LightSkyBlue;
            this.findFilesWithExtension.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findFilesWithExtension.Location = new System.Drawing.Point(177, 24);
            this.findFilesWithExtension.Name = "findFilesWithExtension";
            this.findFilesWithExtension.Size = new System.Drawing.Size(124, 34);
            this.findFilesWithExtension.TabIndex = 1;
            this.findFilesWithExtension.Text = "Find";
            this.findFilesWithExtension.UseVisualStyleBackColor = false;
            this.findFilesWithExtension.Click += new System.EventHandler(this.analyze_click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.smallFilesValue);
            this.groupBox7.Controls.Add(this.deleteSmallerFiles);
            this.groupBox7.Controls.Add(this.findSmallerFiles);
            this.groupBox7.Location = new System.Drawing.Point(1356, 140);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(258, 99);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Files smaller than:";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox6_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "MB";
            // 
            // smallFilesValue
            // 
            this.smallFilesValue.Location = new System.Drawing.Point(24, 26);
            this.smallFilesValue.Name = "smallFilesValue";
            this.smallFilesValue.Size = new System.Drawing.Size(156, 23);
            this.smallFilesValue.TabIndex = 2;
            // 
            // deleteSmallerFiles
            // 
            this.deleteSmallerFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteSmallerFiles.BackColor = System.Drawing.Color.Coral;
            this.deleteSmallerFiles.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteSmallerFiles.Location = new System.Drawing.Point(133, 58);
            this.deleteSmallerFiles.Name = "deleteSmallerFiles";
            this.deleteSmallerFiles.Size = new System.Drawing.Size(91, 34);
            this.deleteSmallerFiles.TabIndex = 1;
            this.deleteSmallerFiles.Text = "Delete";
            this.deleteSmallerFiles.UseVisualStyleBackColor = false;
            this.deleteSmallerFiles.Click += new System.EventHandler(this.analyze_click);
            // 
            // findSmallerFiles
            // 
            this.findSmallerFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findSmallerFiles.BackColor = System.Drawing.Color.LightSkyBlue;
            this.findSmallerFiles.Font = new System.Drawing.Font("Roboto", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findSmallerFiles.Location = new System.Drawing.Point(24, 58);
            this.findSmallerFiles.Name = "findSmallerFiles";
            this.findSmallerFiles.Size = new System.Drawing.Size(103, 34);
            this.findSmallerFiles.TabIndex = 1;
            this.findSmallerFiles.Text = "Find";
            this.findSmallerFiles.UseVisualStyleBackColor = false;
            this.findSmallerFiles.Click += new System.EventHandler(this.analyze_click);
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
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.largeFileValue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smallFilesValue)).EndInit();
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button clearRecycleBin;
        private System.Windows.Forms.Button clearTempFolder1;
        private System.Windows.Forms.Button clearTempFolder2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button deleteOlderFiles;
        private System.Windows.Forms.Button findOlderFiles;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown largeFileValue;
        private System.Windows.Forms.Button deleteLargerFiles;
        private System.Windows.Forms.Button findLargerFiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown smallFilesValue;
        private System.Windows.Forms.Button deleteSmallerFiles;
        private System.Windows.Forms.Button findSmallerFiles;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button deleteFilesWithExtension;
        private System.Windows.Forms.Button findFilesWithExtension;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button evenOddButton;
    }
}

