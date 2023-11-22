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
            this.analyze = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressLabel = new System.Windows.Forms.Label();
            this.filesFound = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.otherCheckBox = new System.Windows.Forms.CheckBox();
            this.videoCheckBox = new System.Windows.Forms.CheckBox();
            this.imageCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.smallFilesRadioButton = new System.Windows.Forms.RadioButton();
            this.largeFilesRadioButton = new System.Windows.Forms.RadioButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.results = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // analyze
            // 
            this.analyze.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analyze.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analyze.Location = new System.Drawing.Point(339, 516);
            this.analyze.Name = "analyze";
            this.analyze.Size = new System.Drawing.Size(552, 34);
            this.analyze.TabIndex = 1;
            this.analyze.Text = "Analyze";
            this.analyze.UseVisualStyleBackColor = true;
            this.analyze.Click += new System.EventHandler(this.analyze_click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBox.Location = new System.Drawing.Point(134, 47);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(957, 23);
            this.pathTextBox.TabIndex = 2;
            this.pathTextBox.Text = "E:\\SANDEEP_KUMAR\\PIC\\";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Path to analyze:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.progressLabel);
            this.groupBox1.Controls.Add(this.filesFound);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pathTextBox);
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1097, 235);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Analyze the folder easily";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(21, 82);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(69, 17);
            this.progressLabel.TabIndex = 5;
            this.progressLabel.Text = "Progress:";
            // 
            // filesFound
            // 
            this.filesFound.AutoSize = true;
            this.filesFound.Location = new System.Drawing.Point(131, 118);
            this.filesFound.Name = "filesFound";
            this.filesFound.Size = new System.Drawing.Size(68, 17);
            this.filesFound.TabIndex = 4;
            this.filesFound.Text = "------------";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Total files found: ";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(134, 82);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(957, 23);
            this.progressBar.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.otherCheckBox);
            this.groupBox3.Controls.Add(this.videoCheckBox);
            this.groupBox3.Controls.Add(this.imageCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(661, 154);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 68);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.smallFilesRadioButton);
            this.groupBox2.Controls.Add(this.largeFilesRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(134, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 68);
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
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(15, 285);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1097, 225);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // results
            // 
            this.results.AutoSize = true;
            this.results.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.results.Location = new System.Drawing.Point(15, 266);
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(59, 17);
            this.results.TabIndex = 6;
            this.results.Text = "Results:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1126, 575);
            this.Controls.Add(this.results);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.analyze);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Folder Analyzer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button analyze;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox otherCheckBox;
        private System.Windows.Forms.CheckBox videoCheckBox;
        private System.Windows.Forms.CheckBox imageCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton smallFilesRadioButton;
        private System.Windows.Forms.RadioButton largeFilesRadioButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label filesFound;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label results;
    }
}

