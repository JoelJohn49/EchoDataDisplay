using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EchoDataDisplay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFile1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open First Sensor log File";
            openFileDialog1.Filter = "log files (*.log)|*.log|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.ShowDialog();

            string file1Path = openFileDialog1.FileName;

            textBox1.Text = file1Path;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFile2_Click(object sender, EventArgs e)
        {
            openFileDialog2.Title = "Open Second Sensor log File";
            openFileDialog2.Filter = "log files (*.log)|*.log|All files (*.*)|*.*";
            openFileDialog2.FilterIndex = 1;
            openFileDialog2.ShowDialog();

            string file2Path = openFileDialog2.FileName;

            textBox2.Text = file2Path;
        }

        private void createOutput_Click(object sender, EventArgs e)
        {

            if (!File.Exists(textBox1.Text) || !File.Exists(textBox2.Text))
            {
                MessageBox.Show("Missing Input Sensor Files", "Error");
            }
            else
            {
                saveFileDialog1.Title = "Choose File Destination";
                saveFileDialog1.DefaultExt = "csv";
                saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;

                saveFileDialog1.ShowDialog();

                string saveFilePath = saveFileDialog1.FileName;

                writeOutput(textBox1.Text, textBox2.Text, saveFilePath);

                MessageBox.Show("Output File Created", "Save Successful");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void openFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            string directoryPath = folderBrowserDialog1.SelectedPath;

            textBox3.Text = directoryPath;
        }

        private void createFolderOutput_Click(object sender, EventArgs e)
        {
            //string[] files = Directory.GetFiles(textBox3.Text, "*.log");

            if (!Directory.Exists(textBox3.Text))
            {
                MessageBox.Show("Missing Folder Path", "Error");
            }
            else
            {
                var files200k = Directory.EnumerateFiles(textBox3.Text, "*.log", SearchOption.AllDirectories)
            .Where(s => s.Contains("Dual_200kHz"));
                List<string> files200kList = files200k.ToList();
                var files450k = Directory.EnumerateFiles(textBox3.Text, "*.log", SearchOption.AllDirectories)
                .Where(s => s.Contains("Dual_450kHz"));
                List<string> files450kList = files450k.ToList();

                files200kList.Sort();
                List<string> files200kList_compare = files200kList.Select(s => s.Replace("200kHz_", "")).ToList();
                files450kList.Sort();
                List<string> files450kList_compare = files450kList.Select(s => s.Replace("450kHz_", "")).ToList();

                //var newList = metricList.Select(s => s.Replace("XX", "1")).ToList();

                //TODO Check that both arrays have the same size and each element has a pair in the other array.
                if (files200kList_compare.SequenceEqual(files450kList_compare))
                {
                    for (int i = 0; i < files200k.Count(); i++)
                    {
                        string saveFileName = files200kList_compare[i].Split(new[] { '.' }, 2)[0] + ".csv";
                        writeOutput(files200kList[i], files450kList[i], Path.Combine(textBox3.Text, saveFileName));
                    }
                }
                else
                {
                    MessageBox.Show("Input Sensor Files Don't Match", "Error");
                }
            }
        }

        private void writeOutput(string file1, string file2, string outputfile)
        {
            string[] lines1 = System.IO.File.ReadAllLines(file1);
            string[] lines2 = System.IO.File.ReadAllLines(file2);

            var timeStampList = new List<string>();
            var waterTempList = new List<string>();
            var waterDepth1List = new List<string>();
            var waterDepth2List = new List<string>();
            var latLongList = new List<string>();

            foreach (string line in lines1)
            {
                string[] checksumRemoved = line.Split(new[] { '*' }, 2);
                string[] splitLine = checksumRemoved[0].Split(new[] { ',' });

                switch (splitLine[0])
                {
                    case "$SDZDA":
                        {
                            string jointLine = splitLine[1] + "," + splitLine[2] + "," + splitLine[3] + "," + splitLine[4];
                            timeStampList.Add(jointLine);
                            break;
                        }
                    case "$SDMTW":
                        {
                            string jointLine = splitLine[1] + splitLine[2];
                            waterTempList.Add(jointLine);
                            break;
                        }
                    case "$SDDBT":
                        {
                            string jointLine = splitLine[1] + splitLine[2] + "," + splitLine[3] + splitLine[4];
                            waterDepth1List.Add(jointLine);
                            break;
                        }
                    case "$GNGGA":
                        {
                            string jointLine = splitLine[2] + splitLine[3] + "," + splitLine[4] + splitLine[5];
                            latLongList.Add(jointLine);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            foreach (string line in lines2)
            {
                if (line.StartsWith("$SDDBT"))
                {
                    string[] checksumRemoved = line.Split(new[] { '*' }, 2);
                    string[] splitLine = checksumRemoved[0].Split(new[] { ',' });
                    string jointLine = splitLine[1] + splitLine[2] + "," + splitLine[3] + splitLine[4];
                    waterDepth2List.Add(jointLine);
                }
            }

            // Write the string array to a new file.
            using (StreamWriter outputFile = new StreamWriter(outputfile))
            {
                outputFile.WriteLine("Time (hhmmss.ss),Day,Month,Year,Water Temp (C),Depth 1 (ft),Depth 1 (m),Depth 2 (ft),Depth 2 (m),Latitude,Longitude");
                for (int i = 0; i < timeStampList.Count; i++)
                {
                    string row = timeStampList[i] + "," + waterTempList[i] + "," + waterDepth1List[i] + "," + waterDepth2List[i] + "," + latLongList[i];
                    outputFile.WriteLine(row);
                }
            }
        }

    }
}
