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
using System.Text.RegularExpressions;

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
                        //Read from the files and write to the save file
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
                            //Let the user know that the save file is open or can't be written to
                            MessageBox.Show(new Form { TopMost = true }, ex.Message,
                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch (DoubleConversionException ex)
                        {
                            MessageBox.Show(new Form { TopMost = true }, ex.Message,
                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch (DateTimeConversionException ex)
                        {
                            MessageBox.Show(new Form { TopMost = true }, ex.Message,
                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch (InputDataFormatException ex)
                        {
                            MessageBox.Show(new Form { TopMost = true }, ex.Message,
                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                //TO-DO Check that both arrays have the same size and each element has a pair in the other array.
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

            var heightValues = new List<string>();
            var adjustedDepth1 = new List<string>();
            var adjustedDepth2 = new List<string>();

            List<DateTimeOffset> posDateTimeStamps = new List<DateTimeOffset>();

            //TO-DO remove posDateTimeStr
            List<string> posDateTimeStr = new List<string>();

            //TO-DO sanity check each case, check that there are enough characters, enough values in splitLine
            //TO-DO Check what is being added to lists contain correct formatted strings "0.000f, 0.000M" for depth values

            //foreach (string line in lines1)
            for (int lineNum = 0; lineNum < lines1.GetLength(0); lineNum++)
            {
                string line = lines1[lineNum];
                string[] checksumRemoved = line.Split(new[] { '*' }, 2);
                string[] splitLine = checksumRemoved[0].Split(new[] { ',' });

                switch (splitLine[0])
                {
                    case "$SDZDA":
                        {
                            //If the sentence is a sonar depth sensor time reading then it runs this block.

                            //Use regex to check if the NMEA Sentence follows the standard formatting and
                            //throw an exception if it doesn't.
                            string dateTimeRGX = @"^\$SDZDA,\d{6}\.\d{2},\d{2},\d{2},\d{4},\d{2},\d{2}";
                            if (!Regex.IsMatch(line, dateTimeRGX))
                            {
                                throw new InputDataFormatException(splitLine[0].Substring(3), (lineNum + 1).ToString(), file1);
                            }

                            //Constructs a string that can be later Parsed into a dateTimeOffset Type.
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
                            //If the sentence is a sonar depth sensor water temperature reading
                            //then it runs this block.

                            //Use regex to check if the NMEA Sentence follows the standard formatting and
                            //throw an exception if it doesn't.
                            string tempRGX = @"^\$SDMTW,-*\d+(\.\d+)*,";
                            if (!Regex.IsMatch(line, tempRGX))
                            {
                                throw new InputDataFormatException(splitLine[0].Substring(3), (lineNum + 1).ToString(), file1);
                            }
                            //Adds the unit 'C' for celcius to the temperature value, remove the following line and
                            //change it so it just adds spltiLine[1] if the unit isn't needed.
                            string jointLine = splitLine[1] + splitLine[2];
                            waterTempList.Add(jointLine);
                            break;
                        }
                    case "$SDDBT":
                        {
                            //If the sentence is a sonar depth sensor water depth reading then it runs this block.

                            //Use regex to check if the NMEA Sentence follows the standard formatting and
                            //throw an exception if it doesn't.
                            string depthRGX = @"^\$SDDBT,(\d+(\.\d+)*){0,1},[a-zA-Z]*,(\d+(\.\d+)*){0,1},[a-zA-Z]*,(\d+(\.\d+)*){0,1},[a-zA-Z]*";
                            if (!Regex.IsMatch(line, depthRGX))
                            {
                                throw new InputDataFormatException(splitLine[0].Substring(3), (lineNum + 1).ToString(), file1);
                            }
                            //Adds depth in feet + f (unit of feet) + , + depth in meters + M (unit of meters)
                            string jointLine = splitLine[1] + splitLine[2] + "," + splitLine[3] + splitLine[4];
                            waterDepth1List.Add(jointLine);
                            break;
                        }
                    case "$GNGGA":
                        {
                            //If the sentence is a GPS' Global Positioning System Fix Data reading then it runs this block.

                            //Use regex to check if the NMEA Sentence follows the standard formatting and
                            //throw an exception if it doesn't.
                            string gpsRGX = @"^\$GNGGA,[^,]*,-*\d+.\d+,[a-zA-Z]*,-*\d+.\d+,[a-zA-Z]*,";
                            if (!Regex.IsMatch(line, gpsRGX))
                            {
                                throw new InputDataFormatException(splitLine[0].Substring(3), (lineNum + 1).ToString(), file1);
                            }

                            //Adds Latitude + North or South + , + Longitude + East or West
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

            for (int lineNum = 0; lineNum < lines2.GetLength(0); lineNum++)
            {
                string line = lines2[lineNum];
                if (line.StartsWith("$SDDBT"))
                {
                    //If the sentence is a sonar depth sensor water depth reading then it runs this block.

                    string[] checksumRemoved = line.Split(new[] { '*' }, 2);
                    string[] splitLine = checksumRemoved[0].Split(new[] { ',' });

                    //Use regex to check if the NMEA Sentence follows the standard formatting and
                    //throw an exception if it doesn't.
                    string depthRGX = @"^\$SDDBT,(\d+(\.\d+)*){0,1},[a-zA-Z]*,(\d+(\.\d+)*){0,1},[a-zA-Z]*,(\d+(\.\d+)*){0,1},[a-zA-Z]*";
                    if (!Regex.IsMatch(line, depthRGX))
                    {
                        throw new InputDataFormatException(splitLine[0].Substring(3), (lineNum + 1).ToString(), file1);
                    }

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
                        string posTimeStamp = splitLine[0];

                        //Convert the string values from the position file into DateTimeOffset
                        DateTimeOffset posDateTime;
                        string posDateTimeFormat = "yyyy/MM/dd HH:mm:ss.fff";
                        bool posDateTimeTried = DateTimeOffset.TryParseExact(posTimeStamp + " UTC +0000", posDateTimeFormat + " 'UTC' zzz",
                                                                    CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out posDateTime);

                        if (!posDateTimeTried)
                        {
                            throw new DateTimeConversionException(posTimeStamp, file3, posDateTimeFormat);
                        }

                        posDateTimeStamps.Add(posDateTime);

                        //TO-DO check if height needs to be checked
                        //Add values to heightValues
                        heightValues.Add(splitLine[3]);
                    }
                }

                //Loop through each DateTime from the first sensor file
                for (int i = 0; i < timeStampList.Count; i++)
                {
                    string timeStamp = timeStampList[i] + " UTC +0000";

                    //Convert the string values from the first sonar file into DateTimeOffset
                    DateTimeOffset sonarDateTime;
                    string sonarDateTimeFormat = "HH:mm:ss.ff,dd/MM/yyyy 'UTC' zzz";
                    bool sonarDateTimeTried = DateTimeOffset.TryParseExact(timeStamp, sonarDateTimeFormat,
                                                                    CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out sonarDateTime);

                    if (!sonarDateTimeTried)
                    {
                        throw new DateTimeConversionException(timeStamp, file1, sonarDateTimeFormat);
                    }

                    TimeSpan minSpan = new TimeSpan(0, 0, 5, 0, 0);
                    int closestIndex = 0;
                    bool minSpanChanged = false;

                    //TO-DO can be made more efficient as time stamps should be in order
                    //Loop through each DateTime from the position file.
                    for (int j = 0; j < posDateTimeStamps.Count; j++)
                    {
                        TimeSpan difference = sonarDateTime.Subtract(posDateTimeStamps[j]).Duration();

                        int comp = TimeSpan.Compare(difference, minSpan);
                        if (comp < 0)
                        {
                            minSpan = difference;
                            closestIndex = j;
                            minSpanChanged = true;
                        }
                    }

                    //Check if a DateTime suitable pair from the pos file was found or if it's using the defualt value
                    if (minSpanChanged)
                    {
                        double heightDatum;
                        bool heightDatumTried = double.TryParse(RemoveSpecialCharacters(heightValues[closestIndex]), out heightDatum);
                        if (!heightDatumTried)
                        {
                            throw new DoubleConversionException(heightValues[closestIndex], file3);
                        }

                        //TO-DO remove posDateTimeStr
                        posDateTimeStr.Add(posDateTimeStamps[closestIndex].ToString("HH:mm:ss.fff",
                                                                                  CultureInfo.InvariantCulture));

                        //Calculates the height adjusted depth and adds it to the its list
                        adjustedDepth1.Add(HeightAdjustedDepth(file1, waterDepth1List[i], heightDatum));
                        adjustedDepth2.Add(HeightAdjustedDepth(file2, waterDepth2List[i], heightDatum));
                    }
                    else
                    {
                        adjustedDepth1.Add("");
                        adjustedDepth2.Add("");
                        //TO-DO remove posDateTimeStr
                        posDateTimeStr.Add("");
                    }
                    
                }
            }

            //Write the string array to a new file.
            FileStream fileStream = new FileStream(outputfile, FileMode.Create, FileAccess.ReadWrite);
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
                        //TO-DO remove posDateTimeStr
                        row += "," + adjustedDepth1[i] + "," + adjustedDepth2[i] + "," + posDateTimeStr[i];
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

        public string HeightAdjustedDepth(string file, string depth, double heightDatum)
        {
            string sonarDepth = depth.Split(new[] { ',' }, 2)[1];
            sonarDepth = sonarDepth.Remove(sonarDepth.Length - 1);

            double sonarDepth_Val;
            bool sonarDepth_ValTried = double.TryParse(RemoveSpecialCharacters(sonarDepth), out sonarDepth_Val);
            if (!sonarDepth_ValTried)
            {
                throw new DoubleConversionException(sonarDepth, file);
            }

            double adjustedDepthVal = heightDatum - sonarDepth_Val;
            string adjustedDepthString = String.Format("{0:0.0000}", Math.Round(adjustedDepthVal, 4));
            return adjustedDepthString;
        }
    }
}
