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
using System.Globalization;

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
            else if (textBox1.Text.Equals(textBox2.Text))
            {
                MessageBox.Show("The Same File Is Selected Twice", "Error");
            }
            else
            {
                saveFileDialog1.Title = "Choose File Destination";
                saveFileDialog1.DefaultExt = "csv";
                saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string saveFilePath = saveFileDialog1.FileName;
                    if (!String.IsNullOrEmpty(saveFilePath))
                    {
                        writeOutput(textBox1.Text, textBox2.Text, saveFilePath, positionFileCheck.Checked, posFileTextBox.Text);
                        MessageBox.Show("Output File Created", "Save Successful");
                    }
                }
            }
        }

        private void openFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string directoryPath = folderBrowserDialog1.SelectedPath;
                textBox3.Text = directoryPath;
            }
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
                        string temp = "temp";
                        writeOutput(files200kList[i], files450kList[i], Path.Combine(textBox3.Text, saveFileName), positionFileCheck.Checked, temp);
                    }
                }
                else
                {
                    MessageBox.Show("Input Sensor Files Don't Match", "Error");
                }
            }
        }

        private void timeZoneText_TextChanged(object sender, EventArgs e)
        {
            //TO-DO Check that the text follows the required format and open an error window if it doesn't.
        }

        private void positionFileCheck_CheckedChanged(object sender, EventArgs e)
        {
            openPosFile.Enabled = positionFileCheck.Checked;
            posFileTextBox.Enabled = positionFileCheck.Checked;
        }

        private void openPosFile_Click(object sender, EventArgs e)
        {
            openFileDialog3.Title = "Open Optional Postion File";
            openFileDialog3.Filter = "All files (*.*)|*.*";
            openFileDialog3.FilterIndex = 0;
            openFileDialog3.ShowDialog();

            string openPosFilePath = openFileDialog3.FileName;

            posFileTextBox.Text = openPosFilePath;
        }

        private void writeOutput(string file1, string file2, string outputfile, bool adjHeight, string file3)
        {
            string[] lines1 = System.IO.File.ReadAllLines(file1);
            string[] lines2 = System.IO.File.ReadAllLines(file2);

            var timeStampList = new List<string>();
            var waterTempList = new List<string>();
            var waterDepth1List = new List<string>();
            var waterDepth2List = new List<string>();
            var latLongList = new List<string>();

            //var posTimeStampList = new List<string>();
            var heightValues = new List<string>();
            var adjustedDepth1 = new List<string>();
            var adjustedDepth2 = new List<string>();

            List<DateTimeOffset> posDateTimeStamps = new List<DateTimeOffset>();


            foreach (string line in lines1)
            {
                string[] checksumRemoved = line.Split(new[] { '*' }, 2);
                string[] splitLine = checksumRemoved[0].Split(new[] { ',' });

                switch (splitLine[0])
                {
                    case "$SDZDA":
                        {
                            /*
                            int year = Int32.Parse(splitLine[4]);
                            int month = Int32.Parse(splitLine[3]);
                            int date = Int32.Parse(splitLine[2]);
                            int hours = Int32.Parse(splitLine[1].Substring(0, 2));
                            int minutes = Int32.Parse(splitLine[1].Substring(2, 2));
                            int seconds = Int32.Parse(splitLine[1].Substring(4, 2));
                            int millisecond = Int32.Parse(splitLine[1].Substring(7, 2))*10;

                            DateTime dateTime = new DateTime(year, month, date, hours, minutes, seconds, millisecond);
                            DateTimeOffset targetTime;

                            // Instantiate a DateTimeOffset value from a UTC time
                            DateTime utcTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                            targetTime = new DateTimeOffset(utcTime);

                            */

                            /*
                            var value = DateTimeOffset.ParseExact(text,
                                      "yyyy'-'MM'-'dd'T'HH':'mm':'sszzz",
                                      CultureInfo.InvariantCulture);
                            */

                            //string jointLine = year.ToString("D4") + "/" + month.ToString("D2") + "/" + date.ToString("D2");

                            string hours = splitLine[1].Substring(0, 2);
                            string minutes = splitLine[1].Substring(2, 2);
                            string seconds = splitLine[1].Substring(4, 5);

                            string jointLine = hours + ":" + minutes + ":" + seconds + "," + splitLine[2] + "/" + splitLine[3] + "/" + splitLine[4];
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

            if (adjHeight)
            {
                string[] lines3 = System.IO.File.ReadAllLines(file3);

                foreach (string line in lines3)
                {
                    if (!line.StartsWith("%"))
                    {
                        string[] splitLine = line.Split(new[] { ',' });

                        //Reformat and add values to posDateTimeStamps
                        string posTimeStamp = splitLine[0];
                        posTimeStamp += " UTC +0000";
                        var posDateTime = DateTimeOffset.ParseExact(posTimeStamp, "yyyy/MM/dd HH:mm:ss.fff 'UTC' zzz", CultureInfo.InvariantCulture);
                        posDateTimeStamps.Add(posDateTime);

                        //Add values to heightValues
                        heightValues.Add(splitLine[3]);
                    }
                }

                for (int i = 0; i < timeStampList.Count; i++)
                {
                    string timeStamp = timeStampList[i] + " UTC +0000";
                    var sonarDateTime = DateTimeOffset.ParseExact(timeStamp, "HH:mm:ss.ff,dd/MM/yyyy 'UTC' zzz", CultureInfo.InvariantCulture);

                    TimeSpan minSpan = new TimeSpan(99, 0, 0, 0, 0);
                    int closestIndex = 0;

                    //TO-DO can be made more efficient as time stamps should be in order
                    for (int j = 0; j < posDateTimeStamps.Count; j++)
                    {
                        TimeSpan difference = sonarDateTime.Subtract(posDateTimeStamps[j]).Duration();

                        int comp = TimeSpan.Compare(difference, minSpan);

                        if (comp < 0)
                        {
                            minSpan = difference;
                            closestIndex = j;
                        }
                    }

                    //TO-DO int32 Parse not correct.
                    string sonarDepth1 = waterDepth1List[i].Split(new[] { ',' }, 2)[1];
                    sonarDepth1 = sonarDepth1.Remove(sonarDepth1.Length - 1);
                    int adjustedDepthVal1 = Int32.Parse(heightValues[closestIndex]) - Int32.Parse(sonarDepth1);
                    adjustedDepth1.Add(adjustedDepthVal1.ToString());

                    string sonarDepth2 = waterDepth2List[i].Split(new[] { ',' }, 2)[1];
                    sonarDepth2 = sonarDepth2.Remove(sonarDepth2.Length - 1);
                    int adjustedDepthVal2 = Int32.Parse(heightValues[closestIndex]) - Int32.Parse(sonarDepth2);
                    adjustedDepth2.Add(adjustedDepthVal2.ToString());
                }
            }

            // Write the string array to a new file.
            using (StreamWriter outputFile = new StreamWriter(outputfile))
            {
                string heading = "Time (hh:mm:ss.ss),Date,Water Temp (C),Latitude,Longitude,Depth 1 (ft),Depth 1 (m),Depth 2 (ft),Depth 2 (m)";
                if (adjHeight)
                {
                    heading += ",Adjusted Depth 1,Adjusted Depth 2";
                }
                outputFile.WriteLine(heading);
                for (int i = 0; i < timeStampList.Count; i++)
                {
                    string row;
                    if (adjHeight)
                    {
                        row = timeStampList[i] + "," + waterTempList[i] + "," + latLongList[i] + "," + waterDepth1List[i] + "," + waterDepth2List[i] + "," + adjustedDepth1[i] + "," + adjustedDepth2[i];
                    }
                    else
                    {
                        row = timeStampList[i] + "," + waterTempList[i] + "," + latLongList[i] + "," + waterDepth1List[i] + "," + waterDepth2List[i];
                    }
                    outputFile.WriteLine(row);
                }
            }
        }

    }
}
