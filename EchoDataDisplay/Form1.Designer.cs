/*
 * author: Joel John for Scout Aerial
 * email: joeljohn49@gmail.com
 * last modified: 15/06/2021
*/

namespace EchoDataDisplay
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFile1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFile2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.createOutput = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFolder = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.createFolderOutput = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pairThresholdInput = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openPosFile = new System.Windows.Forms.Button();
            this.posFileTextBox = new System.Windows.Forms.TextBox();
            this.positionFileCheck = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.singleFileTextBox = new System.Windows.Forms.TextBox();
            this.openSingleFile = new System.Windows.Forms.Button();
            this.createSingleFile = new System.Windows.Forms.Button();
            this.openFileDialog4 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.singlePairThresholdInput = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.singleOpenPosFile = new System.Windows.Forms.Button();
            this.singlePosFileTextBox = new System.Windows.Forms.TextBox();
            this.singlePosCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFile1
            // 
            this.openFile1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.openFile1.Location = new System.Drawing.Point(45, 156);
            this.openFile1.Name = "openFile1";
            this.openFile1.Size = new System.Drawing.Size(75, 24);
            this.openFile1.TabIndex = 0;
            this.openFile1.Text = "Open File";
            this.toolTip1.SetToolTip(this.openFile1, "Select a Sensor File to Merge From");
            this.openFile1.UseVisualStyleBackColor = true;
            this.openFile1.Click += new System.EventHandler(this.openFile1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox1.Location = new System.Drawing.Point(45, 186);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(551, 23);
            this.textBox1.TabIndex = 1;
            // 
            // openFile2
            // 
            this.openFile2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.openFile2.Location = new System.Drawing.Point(45, 241);
            this.openFile2.Name = "openFile2";
            this.openFile2.Size = new System.Drawing.Size(75, 24);
            this.openFile2.TabIndex = 2;
            this.openFile2.Text = "Open File";
            this.toolTip1.SetToolTip(this.openFile2, "Select a Sensor File to Merge From");
            this.openFile2.UseVisualStyleBackColor = true;
            this.openFile2.Click += new System.EventHandler(this.openFile2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox2.Location = new System.Drawing.Point(45, 269);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(551, 23);
            this.textBox2.TabIndex = 3;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // createOutput
            // 
            this.createOutput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.createOutput.Location = new System.Drawing.Point(290, 411);
            this.createOutput.Name = "createOutput";
            this.createOutput.Size = new System.Drawing.Size(75, 23);
            this.createOutput.TabIndex = 4;
            this.createOutput.Text = "Create File";
            this.toolTip1.SetToolTip(this.createOutput, "Create The Output File and Open It");
            this.createOutput.UseVisualStyleBackColor = true;
            this.createOutput.Click += new System.EventHandler(this.createOutput_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select the 200k Sonar Text File\r\n";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select the 450k Sonar Text File\r\n";
            // 
            // openFolder
            // 
            this.openFolder.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.openFolder.Location = new System.Drawing.Point(45, 309);
            this.openFolder.Name = "openFolder";
            this.openFolder.Size = new System.Drawing.Size(85, 23);
            this.openFolder.TabIndex = 7;
            this.openFolder.Text = "Open Folder";
            this.toolTip1.SetToolTip(this.openFolder, "Select a Directory Containing Pairs of Sensor Files");
            this.openFolder.UseVisualStyleBackColor = true;
            this.openFolder.Click += new System.EventHandler(this.openFolder_Click);
            // 
            // textBox3
            // 
            this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox3.Location = new System.Drawing.Point(45, 338);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(551, 23);
            this.textBox3.TabIndex = 8;
            // 
            // createFolderOutput
            // 
            this.createFolderOutput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.createFolderOutput.Location = new System.Drawing.Point(290, 371);
            this.createFolderOutput.Name = "createFolderOutput";
            this.createFolderOutput.Size = new System.Drawing.Size(75, 23);
            this.createFolderOutput.TabIndex = 9;
            this.createFolderOutput.Text = "Create Files";
            this.toolTip1.SetToolTip(this.createFolderOutput, "Batch Merge The Sensor Files and Save The Merged Files To The Selected Directory");
            this.createFolderOutput.UseVisualStyleBackColor = true;
            this.createFolderOutput.Click += new System.EventHandler(this.createFolderOutput_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(644, 580);
            this.tabControl1.TabIndex = 10;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.createOutput);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.pairThresholdInput);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.openPosFile);
            this.tabPage1.Controls.Add(this.posFileTextBox);
            this.tabPage1.Controls.Add(this.positionFileCheck);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.openFile2);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.openFile1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(636, 552);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Process Sonar Pair";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(337, 411);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(299, 138);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pairThresholdInput
            // 
            this.pairThresholdInput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pairThresholdInput.Enabled = false;
            this.pairThresholdInput.Location = new System.Drawing.Point(534, 308);
            this.pairThresholdInput.Mask = "00:00.00";
            this.pairThresholdInput.Name = "pairThresholdInput";
            this.pairThresholdInput.Size = new System.Drawing.Size(51, 23);
            this.pairThresholdInput.TabIndex = 18;
            this.pairThresholdInput.Text = "050000";
            this.toolTip1.SetToolTip(this.pairThresholdInput, "The max time difference the Sonar Time Stamps and the Position Time Stamps can ha" +
        "ve.\r\nUsefull for missing data.");
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(311, 311);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(225, 15);
            this.label11.TabIndex = 15;
            this.label11.Text = "Time Stamp Pairing Threshold (mm:ss.ff):";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(547, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Adds Depth data from the second sensor file to the data from the first sensor fil" +
    "e and outputs a csv file.";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(115, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(420, 41);
            this.label3.TabIndex = 12;
            this.label3.Text = "Process Echo Sounder File Pair";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // openPosFile
            // 
            this.openPosFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.openPosFile.Enabled = false;
            this.openPosFile.Location = new System.Drawing.Point(45, 335);
            this.openPosFile.Name = "openPosFile";
            this.openPosFile.Size = new System.Drawing.Size(75, 23);
            this.openPosFile.TabIndex = 10;
            this.openPosFile.Text = "Open File";
            this.toolTip1.SetToolTip(this.openPosFile, "Select a Position File to Calculate Adjusted Height From");
            this.openPosFile.UseVisualStyleBackColor = true;
            this.openPosFile.Click += new System.EventHandler(this.openPosFile_Click);
            // 
            // posFileTextBox
            // 
            this.posFileTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.posFileTextBox.Enabled = false;
            this.posFileTextBox.Location = new System.Drawing.Point(45, 365);
            this.posFileTextBox.Name = "posFileTextBox";
            this.posFileTextBox.Size = new System.Drawing.Size(551, 23);
            this.posFileTextBox.TabIndex = 9;
            // 
            // positionFileCheck
            // 
            this.positionFileCheck.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.positionFileCheck.AutoSize = true;
            this.positionFileCheck.Location = new System.Drawing.Point(45, 309);
            this.positionFileCheck.Name = "positionFileCheck";
            this.positionFileCheck.Size = new System.Drawing.Size(173, 19);
            this.positionFileCheck.TabIndex = 8;
            this.positionFileCheck.Text = "Select Optional Position File";
            this.toolTip1.SetToolTip(this.positionFileCheck, "(Optional) Toggle Whether A Position File Will Be Selected");
            this.positionFileCheck.UseVisualStyleBackColor = true;
            this.positionFileCheck.CheckedChanged += new System.EventHandler(this.positionFileCheck_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.createFolderOutput);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.openFolder);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(636, 552);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Batch Process Pairs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 288);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(199, 15);
            this.label10.TabIndex = 17;
            this.label10.Text = "Select The Directory Containing Files";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(45, 182);
            this.label9.MaximumSize = new System.Drawing.Size(400, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(390, 60);
            this.label9.TabIndex = 16;
            this.label9.Text = resources.GetString("label9.Text");
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(45, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(214, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "Only merges files with \".log\" extension.";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(255, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "Ensure only files to be merged are in the folder.";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(64, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(528, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Merges all pairs of Echo Sounder Sensor Files within a selected folder and create" +
    "s a csv file for each.";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(30, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(593, 41);
            this.label5.TabIndex = 10;
            this.label5.Text = "Merge All Echo Sounder Files In A Directory";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox5);
            this.tabPage3.Controls.Add(this.textBox4);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(636, 552);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Help";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textBox5.Location = new System.Drawing.Point(9, 493);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(273, 51);
            this.textBox5.TabIndex = 13;
            this.textBox5.Text = "Created by Joel John with Scout Aerial for Ocebile\r\nEmail: joeljohn49@gmail.com\r\n" +
    "Last Modified: 15/06/2021";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.Location = new System.Drawing.Point(9, 23);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox4.Size = new System.Drawing.Size(619, 413);
            this.textBox4.TabIndex = 0;
            this.textBox4.Text = resources.GetString("textBox4.Text");
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.FileName = "openFileDialog3";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.singlePairThresholdInput);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.singleOpenPosFile);
            this.tabPage4.Controls.Add(this.singlePosFileTextBox);
            this.tabPage4.Controls.Add(this.singlePosCheckBox);
            this.tabPage4.Controls.Add(this.createSingleFile);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.singleFileTextBox);
            this.tabPage4.Controls.Add(this.openSingleFile);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(636, 552);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Process Sonar File ";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(203, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(230, 15);
            this.label12.TabIndex = 18;
            this.label12.Text = "Formats data from a sonar file to a csv file.";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(110, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(364, 41);
            this.label13.TabIndex = 17;
            this.label13.Text = "Process Echo Sounder File";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(40, 163);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(136, 15);
            this.label14.TabIndex = 16;
            this.label14.Text = "Select the Sonar Text File\r\n";
            // 
            // singleFileTextBox
            // 
            this.singleFileTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.singleFileTextBox.Location = new System.Drawing.Point(40, 212);
            this.singleFileTextBox.Name = "singleFileTextBox";
            this.singleFileTextBox.Size = new System.Drawing.Size(551, 23);
            this.singleFileTextBox.TabIndex = 15;
            // 
            // openSingleFile
            // 
            this.openSingleFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.openSingleFile.Location = new System.Drawing.Point(40, 182);
            this.openSingleFile.Name = "openSingleFile";
            this.openSingleFile.Size = new System.Drawing.Size(75, 24);
            this.openSingleFile.TabIndex = 14;
            this.openSingleFile.Text = "Open File";
            this.toolTip1.SetToolTip(this.openSingleFile, "Select a Sensor File to Merge From");
            this.openSingleFile.UseVisualStyleBackColor = true;
            this.openSingleFile.Click += new System.EventHandler(this.openSingleFile_Click);
            // 
            // createSingleFile
            // 
            this.createSingleFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.createSingleFile.Location = new System.Drawing.Point(290, 411);
            this.createSingleFile.Name = "createSingleFile";
            this.createSingleFile.Size = new System.Drawing.Size(75, 23);
            this.createSingleFile.TabIndex = 19;
            this.createSingleFile.Text = "Create File";
            this.toolTip1.SetToolTip(this.createSingleFile, "Create The Output File and Open It");
            this.createSingleFile.UseVisualStyleBackColor = true;
            this.createSingleFile.Click += new System.EventHandler(this.createSingleFile_Click);
            // 
            // openFileDialog4
            // 
            this.openFileDialog4.FileName = "openFileDialog4";
            // 
            // singlePairThresholdInput
            // 
            this.singlePairThresholdInput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.singlePairThresholdInput.Enabled = false;
            this.singlePairThresholdInput.Location = new System.Drawing.Point(532, 282);
            this.singlePairThresholdInput.Mask = "00:00.00";
            this.singlePairThresholdInput.Name = "singlePairThresholdInput";
            this.singlePairThresholdInput.Size = new System.Drawing.Size(51, 23);
            this.singlePairThresholdInput.TabIndex = 24;
            this.singlePairThresholdInput.Text = "050000";
            this.toolTip1.SetToolTip(this.singlePairThresholdInput, "The max time difference the Sonar Time Stamps and the Position Time Stamps can ha" +
        "ve.\r\nUsefull for missing data.");
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(309, 285);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(225, 15);
            this.label15.TabIndex = 23;
            this.label15.Text = "Time Stamp Pairing Threshold (mm:ss.ff):";
            // 
            // singleOpenPosFile
            // 
            this.singleOpenPosFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.singleOpenPosFile.Enabled = false;
            this.singleOpenPosFile.Location = new System.Drawing.Point(43, 309);
            this.singleOpenPosFile.Name = "singleOpenPosFile";
            this.singleOpenPosFile.Size = new System.Drawing.Size(75, 23);
            this.singleOpenPosFile.TabIndex = 22;
            this.singleOpenPosFile.Text = "Open File";
            this.toolTip1.SetToolTip(this.singleOpenPosFile, "Select a Position File to Calculate Adjusted Height From");
            this.singleOpenPosFile.UseVisualStyleBackColor = true;
            // 
            // singlePosFileTextBox
            // 
            this.singlePosFileTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.singlePosFileTextBox.Enabled = false;
            this.singlePosFileTextBox.Location = new System.Drawing.Point(43, 339);
            this.singlePosFileTextBox.Name = "singlePosFileTextBox";
            this.singlePosFileTextBox.Size = new System.Drawing.Size(551, 23);
            this.singlePosFileTextBox.TabIndex = 21;
            // 
            // singlePosCheckBox
            // 
            this.singlePosCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.singlePosCheckBox.AutoSize = true;
            this.singlePosCheckBox.Location = new System.Drawing.Point(43, 283);
            this.singlePosCheckBox.Name = "singlePosCheckBox";
            this.singlePosCheckBox.Size = new System.Drawing.Size(173, 19);
            this.singlePosCheckBox.TabIndex = 20;
            this.singlePosCheckBox.Text = "Select Optional Position File";
            this.toolTip1.SetToolTip(this.singlePosCheckBox, "(Optional) Toggle Whether A Position File Will Be Selected");
            this.singlePosCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 580);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(660, 574);
            this.Name = "Form1";
            this.Text = "Echo Sounder Data Processor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button openFile1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button openFile2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button createOutput;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button openFolder;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button createFolderOutput;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox posFileTextBox;
        private System.Windows.Forms.CheckBox positionFileCheck;
        private System.Windows.Forms.Button openPosFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox pairThresholdInput;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button createSingleFile;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox singleFileTextBox;
        private System.Windows.Forms.Button openSingleFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.MaskedTextBox singlePairThresholdInput;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button singleOpenPosFile;
        private System.Windows.Forms.TextBox singlePosFileTextBox;
        private System.Windows.Forms.CheckBox singlePosCheckBox;
    }
}

