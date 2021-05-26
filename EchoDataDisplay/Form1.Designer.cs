
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFile1
            // 
            this.openFile1.Location = new System.Drawing.Point(45, 82);
            this.openFile1.Name = "openFile1";
            this.openFile1.Size = new System.Drawing.Size(75, 24);
            this.openFile1.TabIndex = 0;
            this.openFile1.Text = "Open File";
            this.openFile1.UseVisualStyleBackColor = true;
            this.openFile1.Click += new System.EventHandler(this.openFile1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(45, 112);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(551, 23);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // openFile2
            // 
            this.openFile2.Location = new System.Drawing.Point(45, 185);
            this.openFile2.Name = "openFile2";
            this.openFile2.Size = new System.Drawing.Size(75, 24);
            this.openFile2.TabIndex = 2;
            this.openFile2.Text = "Open File";
            this.openFile2.UseVisualStyleBackColor = true;
            this.openFile2.Click += new System.EventHandler(this.openFile2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(45, 213);
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
            this.createOutput.Location = new System.Drawing.Point(290, 283);
            this.createOutput.Name = "createOutput";
            this.createOutput.Size = new System.Drawing.Size(75, 23);
            this.createOutput.TabIndex = 4;
            this.createOutput.Text = "Create File";
            this.createOutput.UseVisualStyleBackColor = true;
            this.createOutput.Click += new System.EventHandler(this.createOutput_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select the First Sensor Text File\r\n";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select the Second Sensor Text File\r\n";
            // 
            // openFolder
            // 
            this.openFolder.Location = new System.Drawing.Point(45, 58);
            this.openFolder.Name = "openFolder";
            this.openFolder.Size = new System.Drawing.Size(85, 23);
            this.openFolder.TabIndex = 7;
            this.openFolder.Text = "Open Folder";
            this.openFolder.UseVisualStyleBackColor = true;
            this.openFolder.Click += new System.EventHandler(this.openFolder_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(45, 119);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(551, 23);
            this.textBox3.TabIndex = 8;
            // 
            // createFolderOutput
            // 
            this.createFolderOutput.Location = new System.Drawing.Point(290, 177);
            this.createFolderOutput.Name = "createFolderOutput";
            this.createFolderOutput.Size = new System.Drawing.Size(75, 23);
            this.createFolderOutput.TabIndex = 9;
            this.createFolderOutput.Text = "Create Files";
            this.createFolderOutput.UseVisualStyleBackColor = true;
            this.createFolderOutput.Click += new System.EventHandler(this.createFolderOutput_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(640, 450);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.createOutput);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.openFile2);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.openFile1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(632, 422);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Page 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.createFolderOutput);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.openFolder);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(632, 422);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Page 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 451);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Echo Sounder Data Merger";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
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
    }
}

