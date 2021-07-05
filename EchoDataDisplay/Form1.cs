/*
 * author: Joel John for Scout Aerial
 * email: joeljohn49@gmail.com
 * last modified: 16/06/2021
*/

using System;
using System.Collections.Generic;
using System.Data;
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

        private async void createOutput_Click(object sender, EventArgs e)
        {
            //Make Buttons and text fields un-interactable so the user cannot change anyhting while the program is
            //running
            createOutput.Enabled = false;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            posFileTextBox.ReadOnly = true;
            openFile1.Enabled = false;
            openFile2.Enabled = false;
            positionFileCheck.Enabled = false;
            openPosFile.Enabled = false;

            this.UseWaitCursor = true;

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
                            await Task.Run(() => writeOutput(textBox1.Text,
                                        textBox2.Text,
                                        saveFilePath,
                                        positionFileCheck.Checked,
                                        posFileTextBox.Text,
                                        pairThresholdInput.Text));

                            //Open the csv file at completion.
                            new Process
                            {
                                StartInfo = new ProcessStartInfo(saveFilePath){ UseShellExecute = true }
                            }.Start();
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
            createOutput.Enabled = true;
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            posFileTextBox.ReadOnly = false;
            openFile1.Enabled = true;
            openFile2.Enabled = true;
            positionFileCheck.Enabled = true;
            openPosFile.Enabled = positionFileCheck.Checked;
            this.UseWaitCursor = false;
        }

        private void openFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string directoryPath = folderBrowserDialog1.SelectedPath;
                textBox3.Text = directoryPath;
            }
        }

        private async void createFolderOutput_Click(object sender, EventArgs e)
        {
            //Make Buttons and text fields un-interactable so the user cannot change anyhting while the program is
            //running
            createFolderOutput.Enabled = false;
            textBox3.ReadOnly = true;
            openFolder.Enabled = false;
            this.UseWaitCursor = true;

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
                files450kList.Sort();

                //Remove the sensor frequency from the name of the file so that paired files will have the same name
                //and can be compared.
                List<string> files200kList_compare = files200kList.Select(s => s.Replace("200kHz_", "")).ToList();
                List<string> files450kList_compare = files450kList.Select(s => s.Replace("450kHz_", "")).ToList();

                //Check that both arrays have the same size and each element has a pair in the other array.
                if (files200kList_compare.SequenceEqual(files450kList_compare))
                {
                    for (int i = 0; i < files200k.Count(); i++)
                    {
                        try
                        {
                            string saveFileName = files200kList_compare[i].Split(new[] { '.' }, 2)[0] + ".csv";
                            string noPosFile = "noPosFile";
                            await Task.Run(() => writeOutput(files200kList[i],
                                        files450kList[i],
                                        Path.Combine(textBox3.Text, saveFileName),
                                        false, noPosFile, noPosFile));
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
                else
                {
                    MessageBox.Show(new Form { TopMost = true }, "Input Sensor Files Don't Match", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            Process.Start("explorer.exe",textBox3.Text);
            createFolderOutput.Enabled = true;
            textBox3.ReadOnly = false;
            openFolder.Enabled = true;
            this.UseWaitCursor = false;
        }

        private void positionFileCheck_CheckedChanged(object sender, EventArgs e)
        {
            openPosFile.Enabled = positionFileCheck.Checked;
            posFileTextBox.Enabled = positionFileCheck.Checked;
            pairThresholdInput.Enabled = positionFileCheck.Checked;
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

        void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
                tabControl1.SelectedTab.Controls.Add(pictureBox1);
        }

        private async Task writeOutput(string file1,
                                        string file2,
                                        string outputfile,
                                        bool adjHeight,
                                        string file3,
                                        string timeSpanInput)
        {
            //Read Files
            string[] lines1 = System.IO.File.ReadAllLines(file1);
            string[] lines2 = System.IO.File.ReadAllLines(file2);

            //Empty Lists that the parsed data wil be stored in befor written to a csv
            var timeStampList = new List<string>();
            var timeStampList2 = new List<string>();
            var waterTempList = new List<string>();
            var waterDepth1List = new List<string>();
            var waterDepth2List = new List<string>();
            var latLongList = new List<string>();

            var heightValues = new List<string>();
            var adjustedDepth1 = new List<string>();
            var adjustedDepth2 = new List<string>();

            List<DateTimeOffset> posDateTimeStamps = new List<DateTimeOffset>();

            List<string> posDateTimeStr = new List<string>();

            //Iterate through lines of the file.
            for (int lineNum = 0; lineNum < lines1.GetLength(0); lineNum++)
            {
                //Parse the line
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
                                throw new InputDataFormatException(splitLine[0].Substring(3),
                                    (lineNum + 1).ToString(), file1);
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
                                throw new InputDataFormatException(splitLine[0].Substring(3),
                                    (lineNum + 1).ToString(), file1);
                            }
                            //Adds the unit 'C' for celcius to the temperature value, remove the following line and
                            //change it so it just adds spltiLine[1] if the unit isn't needed.
                            //string jointLine = splitLine[1] + splitLine[2];
                            waterTempList.Add(splitLine[1]);
                            break;
                        }
                    case "$SDDBT":
                        {
                            //If the sentence is a sonar depth sensor water depth reading then it runs this block.

                            //Use regex to check if the NMEA Sentence follows the standard formatting and
                            //throw an exception if it doesn't.
                            string depthRGX = @"^\$SDDBT(,(\d+(\.\d+)*){0,1},[a-zA-Z]*){2}";
                            if (!Regex.IsMatch(line, depthRGX))
                            {
                                throw new InputDataFormatException(splitLine[0].Substring(3),
                                    (lineNum + 1).ToString(), file1);
                            }
                            //Adds depth in feet + f (unit of feet) + , + depth in meters + M (unit of meters)
                            //string jointLine = splitLine[1] + splitLine[2] + "," + splitLine[3] + splitLine[4];
                            string jointLine = splitLine[3];
                            waterDepth1List.Add(jointLine);
                            break;
                        }
                    case "$GNGGA":
                        {
                            //If the sentence is a GPS' Global Positioning System Fix Data reading then
                            //it runs this block.

                            //Use regex to check if the NMEA Sentence follows the standard formatting and
                            //throw an exception if it doesn't.
                            string gpsRGX = @"^\$GNGGA,[^,]*,-*\d+.\d+,[a-zA-Z]*,-*\d+.\d+,[a-zA-Z]*,";
                            if (!Regex.IsMatch(line, gpsRGX))
                            {
                                throw new InputDataFormatException(splitLine[0].Substring(3),
                                    (lineNum + 1).ToString(), file1);
                            }

                            string jointLine = "";

                            //Adds a "-" if the Latitude is South
                            if (splitLine[3].Equals("S", StringComparison.OrdinalIgnoreCase))
                            {
                                jointLine += "-";
                            }

                            //Adds the Latitude and a comma delimeter
                            //jointLine += splitLine[2] + ",";
                            //jointLine += splitLine[2].Substring(0, 2) + " " + splitLine[2][2..] + ",";
                            
                            jointLine += DecimalDegreesConvertion(file1, splitLine[2], 2) + ",";


                            //Adds a "-" if the Longitude is West
                            if (splitLine[5].Equals("W", StringComparison.OrdinalIgnoreCase))
                            {
                                jointLine += "-";
                            }

                            //Adds the Longitude
                            //jointLine += splitLine[4];
                            //jointLine += splitLine[4].Substring(0, 3) + " " + splitLine[4][3..];

                            jointLine += DecimalDegreesConvertion(file1, splitLine[4], 3);

                            //Adds Latitude + North or South + , + Longitude + East or West
                            //string jointLine = splitLine[2] + splitLine[3] + "," + splitLine[4] + splitLine[5];
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
                string[] checksumRemoved = line.Split(new[] { '*' }, 2);
                string[] splitLine = checksumRemoved[0].Split(new[] { ',' });

                switch (splitLine[0])
                {
                    case "$SDZDA":
                        {
                            string dateTimeRGX = @"^\$SDZDA,\d{6}\.\d{2},\d{2},\d{2},\d{4},\d{2},\d{2}";
                            if (!Regex.IsMatch(line, dateTimeRGX))
                            {
                                throw new InputDataFormatException(splitLine[0].Substring(3),
                                    (lineNum + 1).ToString(), file2);
                            }
                            //Constructs a string that can be later Parsed into a dateTimeOffset Type.
                            string hours = splitLine[1].Substring(0, 2);
                            string minutes = splitLine[1].Substring(2, 2);
                            string seconds = splitLine[1].Substring(4, 5);

                            string jointLine = hours + ":" + minutes + ":" + seconds + ","
                                               + splitLine[2] + "/" + splitLine[3] + "/" + splitLine[4];
                            timeStampList2.Add(jointLine);
                            break;
                        }
                    case "$SDDBT":
                        {
                            string depthRGX = @"^\$SDDBT(,(\d+(\.\d+)*){0,1},[a-zA-Z]*){2}";
                            if (!Regex.IsMatch(line, depthRGX))
                            {
                                throw new InputDataFormatException(splitLine[0].Substring(3), (lineNum + 1).ToString(), file2);
                            }
                            string jointLine = splitLine[3];
                            waterDepth2List.Add(jointLine);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            if (adjHeight)
            {
                string[] lines3 = System.IO.File.ReadAllLines(file3);

                for (int lineNum = 0; lineNum < lines3.GetLength(0); lineNum++)
                {
                    string line = lines3[lineNum];
                    if (!line.StartsWith("%"))
                    {
                        string[] splitLine = line.Split(new[] { ',' });

                        if (splitLine.Count() < 4)
                        {
                            throw new InputDataFormatException((lineNum + 1).ToString(), file1);
                        }

                        string posTimeStamp = splitLine[0];

                        //Convert the string values from the position file into DateTimeOffset
                        DateTimeOffset posDateTime;
                        string posDateTimeFormat = "yyyy/MM/dd HH:mm:ss.fff";
                        bool posDateTimeTried = DateTimeOffset.TryParseExact(posTimeStamp + " UTC +0000",
                                                posDateTimeFormat + " 'UTC' zzz", CultureInfo.InvariantCulture,
                                                DateTimeStyles.AllowWhiteSpaces, out posDateTime);

                        if (!posDateTimeTried)
                        {
                            throw new DateTimeConversionException(posTimeStamp, file3, posDateTimeFormat);
                        }

                        //Add DateTimeOffsets to a list of DateTimeOffsets from the pos file.
                        posDateTimeStamps.Add(posDateTime);
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
                                                CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces,
                                                out sonarDateTime);

                    if (!sonarDateTimeTried)
                    {
                        throw new DateTimeConversionException(timeStamp, file1, sonarDateTimeFormat);
                    }

                    TimeSpan minSpan;
                    bool minSpanTried = TimeSpan.TryParseExact(timeSpanInput, @"mm\:ss\.ff",
                                                CultureInfo.InvariantCulture, out minSpan);
                    if (!minSpanTried)
                    {
                        throw new DateTimeConversionException("Time Stamp Pairing Threshold Does Not Follow the Format"
                                                                + ": mm:ss.ff. Input recieved was: " + timeSpanInput
                                                                + Environment.NewLine + "Ensure only numbers have " +
                                                                "been entered and the minutes and seconds are in the "+
                                                                "range 00-60");
                    }
                    //TimeSpan minSpan = new TimeSpan(0, 0, 5, 0, 0);
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
                        bool heightDatumTried = double.TryParse(RemoveSpecialCharacters(heightValues[closestIndex]),
                                                out heightDatum);
                        if (!heightDatumTried)
                        {
                            throw new DoubleConversionException(heightValues[closestIndex], file3);
                        }

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
                        posDateTimeStr.Add("");
                    }
                    
                }
            }

            //Check the length of each list to make sure that they all have the same size.
            if (timeStampList.Count != waterTempList.Count)
            {
                throw new InputDataFormatException("The number of Sonar Mean Water Temperature readings: "
                                                    + waterTempList.Count.ToString() + " do not match "
                                                    + "the number of Sonar Time readings: "
                                                    + timeStampList.Count.ToString() + " in file: " + file1);
            }
            if (timeStampList.Count != latLongList.Count)
            {
                throw new InputDataFormatException("The number of Latitude and Longitude readings: "
                                                    + latLongList.Count.ToString() + " do not match "
                                                    + "the number of Sonar Time readings: "
                                                    + timeStampList.Count.ToString() + " in file: " + file1);
            }
            if (timeStampList.Count != waterDepth1List.Count)
            {
                throw new InputDataFormatException("The number of Sonar Depth readings: "
                                                    + waterDepth1List.Count.ToString() + " do not match "
                                                    + "the number of Sonar Time readings: "
                                                    + timeStampList.Count.ToString() + " in file: " + file1);
            }
            if (Math.Abs(timeStampList.Count - waterDepth2List.Count) > 10)
            {
                throw new InputDataFormatException("The number of Sonar Depth readings: "
                                                    + waterDepth2List.Count.ToString() + " do not match "
                                                    + "the number of Sonar Time readings: "
                                                    + timeStampList.Count.ToString() + " in file: " + file2);
            }

            //Write the string array to a new file.
            FileStream fileStream = new FileStream(outputfile, FileMode.Create, FileAccess.ReadWrite);
            using (StreamWriter outputFile = new StreamWriter(fileStream))
            {
                string heading = "Latitude,Longitude,Depth 1 (m),Depth 2 (m),Sonar 1 Time (HH:mm:ss.00),Sonar 1 Date," + 
                    "Sonar 2 Time (HH:mm:ss.00),Sonar 2 Date,Water Temp (C),";

                if (adjHeight)
                {
                    heading += ",Adjusted Depth 1,Adjusted Depth 2,Position Time Stamp";
                }
                outputFile.WriteLine(heading);
                for (int i = 0; i < timeStampList.Count; i++)
                {
                    string row = latLongList[i] + "," + waterDepth1List[i] + "," + waterDepth2List[i] + "," +
                        timeStampList[i] + "," + timeStampList2[i] + "," + waterTempList[i];

                    if (adjHeight)
                    {
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

        public string HeightAdjustedDepth(string file, string sonarDepth, double heightDatum)
        {
            //Take the depth in metres from the depth reading
            //string sonarDepth = depth.Split(new[] { ',' }, 2)[1];
            //sonarDepth = sonarDepth.Remove(sonarDepth.Length - 1);

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

        public string DecimalDegreesConvertion(string file, string coordinate, int degreeChars)
        {
            string latDegString = coordinate.Substring(0, degreeChars);
            string latMinString = coordinate[degreeChars..];

            double latDeg;
            bool latDegTried = double.TryParse(RemoveSpecialCharacters(latDegString), out latDeg);
            if (!latDegTried)
            {
                throw new DoubleConversionException(coordinate, file);
            }

            double latMin;
            bool latMinTried = double.TryParse(RemoveSpecialCharacters(latMinString), out latMin);
            if (!latMinTried)
            {
                throw new DoubleConversionException(coordinate, file);
            }

            double LatDD = latDeg + latMin / 60;
            string LatDDString = String.Format("{0:0.000000000}", Math.Round(LatDD, 9));
            return LatDDString;
        }

    }
}
