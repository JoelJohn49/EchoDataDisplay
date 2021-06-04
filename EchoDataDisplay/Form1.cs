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
using System.Diagnostics;

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
            bool noErrors = true;

            if (!File.Exists(textBox1.Text) || !File.Exists(textBox2.Text))
            {
                MessageBox.Show(new Form { TopMost = true }, "Missing Input Sensor Files", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                noErrors = false;
            }
            else if (textBox1.Text.Equals(textBox2.Text))
            {
                MessageBox.Show(new Form { TopMost = true }, "The Same File Is Selected Twice", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                noErrors = false;
            }
            else if (positionFileCheck.Checked)
            {
                if (!File.Exists(posFileTextBox.Text))
                {
                    MessageBox.Show(new Form { TopMost = true }, "Missing Input Position File", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    noErrors = false;
                }
                else if (posFileTextBox.Text.Equals(textBox1.Text) || posFileTextBox.Text.Equals(textBox2.Text))
                {
                    MessageBox.Show(new Form { TopMost = true }, "The Position File Is Also Selected As a Sensor File",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    noErrors = false;
                }
            }
            if (noErrors)
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
                        //Check if file can be written to (might be open by something else)
                        try
                        {
                            writeOutput(textBox1.Text,
                                        textBox2.Text,
                                        saveFilePath,
                                        positionFileCheck.Checked,
                                        posFileTextBox.Text);

                            new Process { StartInfo = new ProcessStartInfo(saveFilePath) { UseShellExecute = true } }.Start();
                            //MessageBox.Show("Output File Created", "Save Successful");
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(new Form { TopMost = true }, ex.Message,
                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            //MessageBox.Show(new Form { TopMost = true }, "The file is unavailable because it is:"
                            //                + Environment.NewLine + "still being written to"
                            //                + Environment.NewLine + "or being processed by another thread",
                            //                "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
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
                MessageBox.Show(new Form { TopMost = true }, "Missing Folder Path", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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
                        writeOutput(files200kList[i],
                                    files450kList[i],
                                    Path.Combine(textBox3.Text, saveFileName),
                                    positionFileCheck.Checked, temp);
                    }
                }
                else
                {
                    //TO-DO update error message boxes
                    MessageBox.Show(new Form { TopMost = true }, "Input Sensor Files Don't Match", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
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
            //Read Files
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

            //List<string> posDateTimeStr = new List<string>();

            //TO-DO sanity check each case, check that there are enough characters, enough values in splitLine
            //TO-DO Check what is being added to lists contain correct formatted strings "0.000f, 0.000M" for depth values
            foreach (string line in lines1)
            {
                string[] checksumRemoved = line.Split(new[] { '*' }, 2);
                string[] splitLine = checksumRemoved[0].Split(new[] { ',' });

                switch (splitLine[0])
                {
                    case "$SDZDA":
                        {
                            //TO-DO Check each value is in the correct format.
                            string hours = splitLine[1].Substring(0, 2);
                            string minutes = splitLine[1].Substring(2, 2);
                            string seconds = splitLine[1].Substring(4, 5);

                            string jointLine = hours + ":" + minutes + ":" + seconds + ","
                                               + splitLine[2] + "/" + splitLine[3] + "/" + splitLine[4];
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
                        //TO-DO change parse to tryparse
                        var posDateTime = DateTimeOffset.ParseExact(posTimeStamp, "yyyy/MM/dd HH:mm:ss.fff 'UTC' zzz",
                                                                    CultureInfo.InvariantCulture);
                        posDateTimeStamps.Add(posDateTime);

                        //Add values to heightValues
                        heightValues.Add(splitLine[3]);
                    }
                }

                for (int i = 0; i < timeStampList.Count; i++)
                {
                    string timeStamp = timeStampList[i] + " UTC +0000";
                    //TO-DO change parse to tryparse
                    var sonarDateTime = DateTimeOffset.ParseExact(timeStamp, "HH:mm:ss.ff,dd/MM/yyyy 'UTC' zzz",
                                                                    CultureInfo.InvariantCulture);

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

                    //TO-DO change parse to tryparse
                    //double heightDatum = double.Parse(RemoveSpecialCharacters(heightValues[closestIndex]));
                    double heightDatum;
                    bool heightDatumTried = double.TryParse(RemoveSpecialCharacters(heightValues[closestIndex]), out heightDatum);
                    if (!heightDatumTried)
                    {
                        string errorMessage = "Error: Can not be converted into double." + Environment.NewLine +
                                                "Height Value: \"" + heightValues[closestIndex] + "\" in The Position File";
                        MessageBox.Show(new Form { TopMost = true }, errorMessage, "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    //posDateTimeStr.Add(posDateTimeStamps[closestIndex].ToString("HH:mm:ss.fff",
                    //                                                          CultureInfo.InvariantCulture));

                    string sonarDepth1 = waterDepth1List[i].Split(new[] { ',' }, 2)[1];
                    sonarDepth1 = sonarDepth1.Remove(sonarDepth1.Length - 1);

                    string sonarDepth2 = waterDepth2List[i].Split(new[] { ',' }, 2)[1];
                    sonarDepth2 = sonarDepth2.Remove(sonarDepth2.Length - 1);

                    double sonarDepth1_Val;
                    bool sonarDepth1_ValTried = double.TryParse(RemoveSpecialCharacters(sonarDepth1), out sonarDepth1_Val);
                    if (!sonarDepth1_ValTried)
                    {
                        string errorMessage = "Error: Can not be converted into double." + Environment.NewLine +
                                                "Depth Value: \"" + sonarDepth1 + "\" in The First Sonar File";
                        MessageBox.Show(new Form { TopMost = true }, errorMessage, "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    double sonarDepth2_Val;
                    bool sonarDepth1_Va2Tried = double.TryParse(RemoveSpecialCharacters(sonarDepth2), out sonarDepth2_Val);
                    if (!sonarDepth1_Va2Tried)
                    {
                        string errorMessage = "Error: Can not be converted into double." + Environment.NewLine +
                                                "Depth Value: \"" + sonarDepth2 + "\" in The Second Sonar File";
                        MessageBox.Show(new Form { TopMost = true }, errorMessage, "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    double adjustedDepthVal1 = heightDatum - sonarDepth1_Val;
                    string depth1str = String.Format("{0:0.0000}", Math.Round(adjustedDepthVal1, 4));
                    adjustedDepth1.Add(depth1str);

                    double adjustedDepthVal2 = heightDatum - sonarDepth2_Val;
                    string depth2str = String.Format("{0:0.0000}", Math.Round(adjustedDepthVal2, 4));
                    adjustedDepth2.Add(depth2str);
                }
            }

            //Write the string array to a new file.
            FileStream fileStream = new FileStream(outputfile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using (StreamWriter outputFile = new StreamWriter(fileStream))
            {
                string heading = "Time (HH:mm:ss.00),Date,Water Temp (C),Latitude,Longitude," +
                    "Depth 1 (ft),Depth 1 (m),Depth 2 (ft),Depth 2 (m)";

                if (adjHeight)
                {
                    heading += ",Adjusted Depth 1,Adjusted Depth 2";
                }
                outputFile.WriteLine(heading);
                for (int i = 0; i < timeStampList.Count; i++)
                {
                    string row = timeStampList[i] + "," + waterTempList[i] + "," + latLongList[i] + "," +
                                waterDepth1List[i] + "," + waterDepth2List[i];

                    if (adjHeight)
                    {
                        row += "," + adjustedDepth1[i] + "," + adjustedDepth2[i];
                    }

                    outputFile.WriteLine(row);
                }
            }
            fileStream.Close();
        }

        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

    }
}
