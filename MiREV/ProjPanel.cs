using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using MiREV.Properties;

namespace MiREV
{
    public partial class ProjPanel : Form
    {
        enum camera : int           // Camera Index
        {
            left = 0,
            cent = 1,
            rght = 2
        }

        enum gpslogger : int        // GPS Logger Column Index (tag, latitude, longitude, bearing, timestamp)
        {
            tag = 0,
            lat = 1,
            lng = 2,
            bea = 3,
            tim = 4,                 
            acx = 5,
            acy = 6,
            cmt = 7
        }

        public class locationManager
        {
            public double lat { get; set; }
            public double lng { get; set; }
            public double bea { get; set; }
            public double dst { get; set; }
            
            public double acx { get; set; }
            public double acy { get; set; }

            public locationManager(double lat, double lng, double bea, double acx, double acy)
            {
                this.lat = lat;
                this.lng = lng;
                this.bea = bea;
                               
                this.acx = acx;
                this.acy = acy;
            }
        }

        public class timeStamp
        {
            public int tag { get; set; }
            public int sec { get; set; }        // GPS Location TimeStamp (second)
            public int mil { get; set; }        // GPS Location TimeStamp (millisecond)
            
            public int lft { get; set; }        // Left Image TimeStamp
            public int cen { get; set; }        // Cent Image TimeStamp
            public int rgt { get; set; }        // Rght Image TImeStamp

            public locationManager location { get; set; }
            public Boolean is100m { get; set; }

            public timeStamp(int tag, int sec, int mil, locationManager loc, Boolean is100m)
            {
                this.tag = tag;
                this.sec = sec;
                this.mil = mil;

                this.location = loc;

                this.is100m = is100m;
            }

            public void addImageTag(int tag, int cam) {
                switch (cam)
                {
                    case (int)camera.left:
                        this.lft = tag;
                        break;
                    case (int)camera.cent:
                        this.cen = tag;
                        break;
                    case (int)camera.rght:
                        this.rgt = tag;
                        break;
                }
            }
        }

        private const int totalCamera = 3;
        private DateTime dateSurvey;
        
        List<timeStamp> listGPS = new List<timeStamp>();

        private String[] camera_ipAddress = {"192.168.0.96", "192.168.0.97", "192.168.0.95"};
        
        private int completedTask = 0;

        private readonly Main _main;
        private String filename;

        private const double EARTH_RADIUS = 6371000;

        private locationManager strt_loc = new locationManager(0, 0, 0, 0, 0);
        private locationManager prev_loc = new locationManager(0, 0, 0, 0, 0);

        private double dist_50m = 50;

        public ProjPanel(Main main)
        {
            InitializeComponent();
            _main = main;

            this.Size = new Size((int)(this.Width * _main.screenRatio),(int)(this.Height * _main.screenRatio));
            this.Padding = new Padding((int)(10.0 * _main.screenRatio));

            btnCreate.Size = new Size((int)(btnCreate.Size.Width * _main.screenRatio), (int)(btnCreate.Size.Height * _main.screenRatio));
            btnCreate.Location = new Point((int)(btnCreate.Location.X * _main.screenRatio), (int)(btnCreate.Location.Y * _main.screenRatio));
            btnCreate.Image = new Bitmap(Resources.create, new Size((int)(48.0 * _main.screenRatio), (int)(48.0 * _main.screenRatio)));
            
            btnCancel.Size = new Size((int)(btnCancel.Size.Width * _main.screenRatio), (int)(btnCancel.Size.Height * _main.screenRatio));
            btnCancel.Location = new Point((int)(btnCancel.Location.X * _main.screenRatio), (int)(btnCancel.Location.Y * _main.screenRatio));
            btnCancel.Image = new Bitmap(Resources.cancel, new Size((int)(48.0 * _main.screenRatio), (int)(48.0 * _main.screenRatio)));

            pictureName.Size = new Size((int)(pictureName.Size.Width * _main.screenRatio), (int)(pictureName.Size.Height * _main.screenRatio));
            pictureName.Location = new Point((int)(pictureName.Location.X * _main.screenRatio), (int)(pictureName.Location.Y * _main.screenRatio));
            pictureName.Image = new Bitmap(Resources.person, new Size((int)(48.0 * _main.screenRatio), (int)(48.0 * _main.screenRatio)));

            pictureBrowse.Size = new Size((int)(pictureBrowse.Size.Width * _main.screenRatio), (int)(pictureBrowse.Size.Height * _main.screenRatio));
            pictureBrowse.Location = new Point((int)(pictureBrowse.Location.X * _main.screenRatio), (int)(pictureBrowse.Location.Y * _main.screenRatio));
            pictureBrowse.Image = new Bitmap(Resources.browse, new Size((int)(48.0 * _main.screenRatio), (int)(48.0 * _main.screenRatio)));

            txtCoderName.Size = new Size((int)(txtCoderName.Size.Width * _main.screenRatio), (int)(txtCoderName.Size.Height * _main.screenRatio));
            txtCoderName.Location = new Point((int)(txtCoderName.Location.X * _main.screenRatio), (int)(txtCoderName.Location.Y * _main.screenRatio));
            //txtCoderName.Font = new Font("Microsoft Sans Serif", (float)(12.0 * _main.screenRatio));
            txtCoderName.Font = new Font("Microsoft Sans Serif", (float)(8 + 4 * _main.screenRatio));

            txtBrowse.Size = new Size((int)(txtBrowse.Size.Width * _main.screenRatio), (int)(txtBrowse.Size.Height * _main.screenRatio));
            txtBrowse.Location = new Point((int)(txtBrowse.Location.X * _main.screenRatio), (int)(txtBrowse.Location.Y * _main.screenRatio));
            //txtBrowse.Font = new Font("Microsoft Sans Serif", (float)(12.0 * _main.screenRatio));
            txtBrowse.Font = new Font("Microsoft Sans Serif", (float)(8 + 4 * _main.screenRatio));

            txtStatus.Size = new Size((int)(txtStatus.Size.Width * _main.screenRatio), (int)(txtStatus.Size.Height * _main.screenRatio));
            txtStatus.Location = new Point((int)(txtStatus.Location.X * _main.screenRatio), (int)(txtStatus.Location.Y * _main.screenRatio));
            //txtStatus.Font = new Font("Microsoft Sans Serif", (float)(10.0 * _main.screenRatio));
            txtStatus.Font = new Font("Microsoft Sans Serif", (float)(6 + 4 * _main.screenRatio));

            pictureBusy.Size = new Size((int)(pictureBusy.Size.Width * _main.screenRatio), (int)(pictureBusy.Size.Height * _main.screenRatio));
            pictureBusy.Location = new Point((int)(pictureBusy.Location.X * _main.screenRatio), (int)(pictureBusy.Location.Y * _main.screenRatio));
            //pictureBusy.Image = new Bitmap(Resources.busy, new Size((int)(48.0 * _main.screenRatio), (int)(48.0 * _main.screenRatio)));
        }

        private void ProjPanel_Load(object sender, EventArgs e)
        {
            pictureBusy.Visible = false;

            ToolTip toolTip = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 2000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(this.btnCreate, "Create Project");
            toolTip.SetToolTip(this.btnCancel, "Cancel");
        }

        private void txtBrowse_MouseClick(object sender, MouseEventArgs e)
        {
            txtBrowse.ForeColor = Color.Black;

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "GPS files (*.gps)|*.gps|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ValidateNames = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filename = openFileDialog.FileName;
                    _main.filepath = Path.GetDirectoryName(filename);

                    txtBrowse.Text = filename;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtBrowse.Text))
            {                
                pictureBusy.Visible = true;
                listGPS.Clear();
                using (StreamReader sr = File.OpenText(filename))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        UpdateTexts(line);                        
                    }
                }
                
                //dateSurvey = FromUnixTime(long.Parse(time[0]));
                //Debug.WriteLine("Total GPS Location: " + listGPS.Count + ". SurveyDate: " + dateSurvey);
                txtStatus.Text = "Total GPS Location: " + listGPS.Count + ". SurveyDate: " + dateSurvey;

                Boolean filesExists = true;
                for (int n = 0; n < 3; n++) 
                {
                    if (!File.Exists(_main.filepath + "\\" + camera_ipAddress[n] + ".mjpeg") || !File.Exists(_main.filepath + "\\" + camera_ipAddress[n] + ".time"))
                    {
                        filesExists = false;
                        break;
                    }
                }

                if (filesExists)
                {
                    if (createDatabase())
                    {
                        completedTask = 0;
                        Thread threadLeft = new Thread(new ParameterizedThreadStart(ExtractImages));
                        Thread threadCent = new Thread(new ParameterizedThreadStart(ExtractImages));
                        Thread threadRght = new Thread(new ParameterizedThreadStart(ExtractImages));

                        threadLeft.Start(camera.left);
                        threadCent.Start(camera.cent);
                        threadRght.Start(camera.rght);
                    }
                }
                else
                {
                    pictureBusy.Visible = false;
                    MessageBox.Show("Unable to locate the video files in" + _main.filepath + ".");
                }
                
            }
            else
            {
                txtBrowse.ForeColor = Color.Red;
            }         
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tasksCompleted (int nCamera)
        {
            completedTask++;
            Debug.WriteLine("Thread for camera:" + nCamera + " done processing the images. Total Completed:" + completedTask);

            if (completedTask >= totalCamera)
            {
                Debug.WriteLine("All Task completed! Insert into database.");

                int Vida_Id = 1;

                using (SqlConnection con = new SqlConnection(_main.conStrng))
                {
                    con.Open();

                    /*using (SqlCommand com = new SqlCommand("INSERT INTO Vida (Coder_name, Coding_date, Road_survey_date, Latitude, Longitude) VALUES (@Coder_name,  @Coding_date, @Road_survey_date, @Latitude, @Longitude)", con))
                    {
                        com.Parameters.AddWithValue("@Coder_name", txtCoderName.Text);
                        com.Parameters.AddWithValue("@Coding_date", DateTime.Today.ToShortDateString());
                        com.Parameters.AddWithValue("@Road_survey_date", dateSurvey.ToShortDateString());
                        com.Parameters.AddWithValue("@Latitude", listGPS[0].location.lat);
                        com.Parameters.AddWithValue("@Longitude", listGPS[0].location.lng);

                        //Debug.WriteLine(com.CommandText);
                        com.ExecuteNonQuery();
                    }*/

                    for (int n = 0; n < listGPS.Count; n++)
                    {
                        if (listGPS[n].is100m)
                        {
                            if (n > 0)
                                Vida_Id++;

                            using (SqlCommand com = new SqlCommand("INSERT INTO Vida (Coder_name, Coding_date, Road_survey_date, Latitude, Longitude) VALUES (@Coder_name,  @Coding_date, @Road_survey_date, @Latitude, @Longitude)", con))
                            {
                                com.Parameters.AddWithValue("@Coder_name", txtCoderName.Text);
                                com.Parameters.AddWithValue("@Coding_date", DateTime.Today.ToShortDateString());
                                com.Parameters.AddWithValue("@Road_survey_date", dateSurvey.ToShortDateString());
                                com.Parameters.AddWithValue("@Latitude", listGPS[n].location.lat);
                                com.Parameters.AddWithValue("@Longitude", listGPS[n].location.lng);

                                //Debug.WriteLine(com.CommandText);
                                com.ExecuteNonQuery();
                            }
                        }

                        using (SqlCommand com = new SqlCommand("INSERT INTO Images (Latitude, Longitude, Bearing, InclinoX, InclinoY, Image_left, Image_cent, Image_rght, Is100m, Vida_Id) VALUES (@Latitude, @Longitude, @Bearing, @InclinoX, @InclinoY, @Image_left,  @Image_cent,  @Image_rght, @Is100m, @Vida_Id)", con))
                        {
                            com.Parameters.AddWithValue("@Latitude",  listGPS[n].location.lat);
                            com.Parameters.AddWithValue("@Longitude", listGPS[n].location.lng);
                            com.Parameters.AddWithValue("@Bearing",   listGPS[n].location.bea);

                            com.Parameters.AddWithValue("@InclinoX", listGPS[n].location.acx);
                            com.Parameters.AddWithValue("@InclinoY", listGPS[n].location.acy);

                            com.Parameters.AddWithValue("@Image_left", String.Format("image-{0:000000000}.jpg", listGPS[n].lft));
                            com.Parameters.AddWithValue("@Image_cent", String.Format("image-{0:000000000}.jpg", listGPS[n].cen));
                            com.Parameters.AddWithValue("@Image_rght", String.Format("image-{0:000000000}.jpg", listGPS[n].rgt));

                            com.Parameters.AddWithValue("@Is100m",  listGPS[n].is100m);
                            com.Parameters.AddWithValue("@Vida_Id", Vida_Id);

                            Debug.WriteLine(n + " " + listGPS[n].is100m);
                            com.ExecuteNonQuery();
                        }

                        
                    }                         
                }
                Debug.WriteLine("Project created successfully.");
                this.Close();
                _main.loadProject();
                
            }   
        }

        /*
         * Extracting Images from MJEG
         */ 
        private void ExtractImages(object n)
        {
            int nCamera = (int)n;

            Debug.WriteLine("Start extracting Images for camera:" + nCamera);

            if (!Directory.Exists(_main.filepath + "\\" + _main.image_foldername[nCamera]))
                Directory.CreateDirectory(_main.filepath + "\\" + _main.image_foldername[nCamera]);

            string cmdString = "/C ffmpeg.exe -i \"" + _main.filepath + "\\" + camera_ipAddress[nCamera] + ".mjpeg\" -r 25 -qscale:v 2 -f image2 \"" + _main.filepath + "\\" + _main.image_foldername[nCamera] + "\\image-%9d.jpg\"";
            Debug.WriteLine(cmdString);

            Process process = new Process();                                // Execute ffmpeg arguments with command prompt
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = cmdString;
            
            process.StartInfo = startInfo;
            process.EnableRaisingEvents = true;

            process.Exited += (sender, eventArgs) => {                      // Event triggered once all images are extracted
                Debug.WriteLine("All images from camera:" + nCamera + " are extracted to " + _main.image_foldername[nCamera]);
                updateImageToGPS(n);
            };

            process.Start();
        }

        private void updateImageToGPS(object camera)
        {
            int nCamera = (int)camera;

            try
            {
                Debug.WriteLine("Pairing Images to GPS location for camera:" + nCamera);

                ArrayList listCAM = new ArrayList();

                using (StreamReader sr = File.OpenText(_main.filepath + "\\" + camera_ipAddress[nCamera] + ".time"))
                {
                    String line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        listCAM.Add(line);
                    }
                }
                Debug.WriteLine(listCAM.Count + " timeStamp read from camera:" + nCamera);

                int indexGPS = 0;
                int indexCAM = 0;

                for (indexGPS = 0; indexGPS < listGPS.Count; indexGPS++)
                {
                    indexCAM = firstTimeStamp(listCAM, indexCAM, listGPS[indexGPS].sec);

                    if (indexCAM < listCAM.Count)
                        break;
                    else
                        indexCAM = 0;
                }
                Debug.WriteLine("First Match" + indexGPS);
                
                if (indexGPS < listGPS.Count)
                {
                    for (int index = indexGPS; index < listGPS.Count; index++)
                    {   
                        Boolean isIndexFound = false;
                        int n = indexCAM;

                        while (n < listCAM.Count)
                        {
                            String[] time = listCAM[n].ToString().Split('\t');

                            if (int.Parse(time[0]) == listGPS[index].sec)
                            {
                                if (int.Parse(time[1]) >= listGPS[index].mil)
                                {
                                    listGPS[index].addImageTag(n, nCamera);

                                    Debug.WriteLine("Matched <Image at:" + n + " to GPS at:" + index + "> Time:" + listGPS[index].sec + " for camera:" + nCamera + " 100m?" + listGPS[index].is100m);
                                    isIndexFound = true;
                                    break;
                                }
                            }
                            n++;
                        }

                        if (isIndexFound)
                            indexCAM = n;                       
                    }
                }
                else
                {
                    Debug.WriteLine("No timestamp match found for camera:" + nCamera);
                }
                this.Invoke(new Action<int>(tasksCompleted), nCamera);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        public int firstTimeStamp(ArrayList list, int start_from, int compare_sec)
        {
            int match = 0;

            for (match = start_from; match < list.Count; match++)
            {
                String[] time = list[match].ToString().Split('\t');

                if (int.Parse(time[0]) == compare_sec)
                {
                    Debug.WriteLine("TimeStamp start at:" + start_from + " match at:" + match + " to second:" + compare_sec);
                    return match;
                }
            }
            return match;
        }

        public DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        private Boolean createDatabase()
        {
            Boolean bSuccess = false;

            String database_nam = _main.filepath + "\\Database.mdf";
            String database_log = _main.filepath + "\\Database_log.ldf";

            try
            {
                if (!File.Exists(database_nam))
                {
                    File.Copy("Database.mdf", database_nam);
                    bSuccess = true;

                    Debug.WriteLine("New database created: " + database_nam);
                }
                else
                {
                    DialogResult res = MessageBox.Show("The database already exists. \r\nDo you want to replace it?", "Confirm Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (res == DialogResult.Yes)
                    {
                        File.Delete(database_nam);
                        File.Delete(database_log);

                        File.Copy("Database.mdf", database_nam);

                        bSuccess = true;
                        Debug.WriteLine("Overwrite existing database.");
                    }
                }

                _main.conStrng = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=" + database_nam + ";Integrated Security=True";
                Debug.WriteLine("Opening database:" + _main.conStrng);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bSuccess;
        }

        /*private void UpdateTexts(String s)
        {            
            String[] coln = s.Split('\t');
            
            locationManager curr_loc = new locationManager(double.Parse(coln[(int)gpslogger.lat]), double.Parse(coln[(int)gpslogger.lng]), 0);          

            if (strt_loc.lat > 0 && strt_loc.lng > 0)
            {
                curr_loc.dst = GetDistance(strt_loc.lat, strt_loc.lng, curr_loc.lat, curr_loc.lng);

                if (curr_loc.dst >= dist_100)
                {
                    //Console.WriteLine("Id:" + a[0] + " strLat:" + start_lat + " strLng:" + start_lng + " curLat:" + currn_lat + " curLng:" + currn_lng + " curDis:" + currn_dst + " preDis:" + prevs_dst);
                    
                    String[] time = coln[(int)gpslogger.tim].Split('.');     // Separate the timestamp in GPS logger into second and milli

                    double euclidean = Math.Abs(curr_loc.dst - dist_100) - Math.Abs(prev_loc.dst - dist_100);
                    if (euclidean > 0)
                    {
                        timeStamp info = new timeStamp(listGPS.Count, int.Parse(time[0]), int.Parse(time[1]), prev_loc, new inclinoMeter(double.Parse(coln[(int)gpslogger.acx]), double.Parse(coln[(int)gpslogger.acy])), true);
                        listGPS.Add(info);

                        strt_loc = prev_loc;
                        strt20m_loc = prev_loc;
                        
                        dist_100 = 100 + (dist_100 - prev_loc.dst);                        

                        Console.WriteLine(">> Save Previous GPS Record. distance:" + prev_loc.dst + " next interval:" + dist_100);
                    }
                    else
                    {
                        timeStamp info = new timeStamp(listGPS.Count, int.Parse(time[0]), int.Parse(time[1]), curr_loc, new inclinoMeter(double.Parse(coln[(int)gpslogger.acx]), double.Parse(coln[(int)gpslogger.acy])), true);
                        listGPS.Add(info);

                        strt_loc = curr_loc;
                        strt20m_loc = curr_loc;

                        dist_100 = 100 + (dist_100 - curr_loc.dst);

                        Console.WriteLine(">> Save Current  GPS Record. distance:" + curr_loc.dst + " next interval:" + dist_100);
                    }

                    curr_loc.dst = GetDistance(strt_loc.lat, strt_loc.lng, curr_loc.lat, curr_loc.lng);
                }
                else
                {
                    double currn20m_dst = GetDistance(strt20m_loc.lat, strt20m_loc.lng, curr_loc.lat, curr_loc.lng);

                    if ((currn20m_dst >= dist_020) && (curr_loc.dst < 90.0))
                    {
                        String[] time = coln[(int)gpslogger.tim].Split('.');     // Separate the timestamp in GPS logger into second and milli

                        double euclidean = Math.Abs(currn20m_dst - dist_020) - Math.Abs(prev20m_loc.dst - dist_020);

                        if (euclidean > 0)
                        {
                            timeStamp info = new timeStamp(listGPS.Count, int.Parse(time[0]), int.Parse(time[1]), prev_loc, 0, 0, false);
                            listGPS.Add(info);

                            strt20m_loc = prev_loc;

                            dist_020 = 20 + (dist_020 - prev20m_loc.dst);
                            Console.WriteLine(" - Save Previous GPS Record. distance:" + prev20m_loc.dst + " next interval:" + dist_020);
                        }
                        else
                        {
                            timeStamp info = new timeStamp(listGPS.Count, int.Parse(time[0]), int.Parse(time[1]), curr_loc, 0, 0, false);
                            listGPS.Add(info);

                            strt20m_loc = curr_loc;

                            dist_020 = 20 + (dist_020 - currn20m_dst);
                            Console.WriteLine(" - Save Current  GPS Record. distance:" + currn20m_dst + " next interval:" + dist_020);
                        }
                        //Console.WriteLine(" - Id:" + coln[0] + " strLat:" + start20m_lat + " strLng:" + start20m_lng + " curLat:" + currn_lat + " curLng:" + currn_lng + " curDis:" + currn20m_dst + " preDis:" + prevs20m_dst);
                        currn20m_dst = GetDistance(strt20m_loc.lat, strt20m_loc.lng, curr_loc.lat, curr_loc.lng);
                    }

                    prev20m_loc.dst = currn20m_dst;
                }
                prev_loc = curr_loc;
            }
            else
            {
                String[] time = coln[(int)gpslogger.tim].Split('.');     // Separate the timestamp in GPS logger into second and milli
                dateSurvey = FromUnixTime(long.Parse(time[0]));

                strt_loc = curr_loc;
                prev_loc = curr_loc;
                strt_loc.dst = 0;
                prev_loc.dst = 0;

                strt20m_loc = curr_loc;
                prev20m_loc = curr_loc;
                strt20m_loc.dst = 0;
                prev20m_loc.dst = 0;
            }
        }*/

        private void UpdateTexts(String s)
        {
            String[] coln = s.Split('\t');

            locationManager curr_loc = new locationManager(double.Parse(coln[(int)gpslogger.lat]), double.Parse(coln[(int)gpslogger.lng]), double.Parse(coln[(int)gpslogger.bea]), double.Parse(coln[(int)gpslogger.acx]), double.Parse(coln[(int)gpslogger.acy]));

            if (strt_loc.lat > 0 && strt_loc.lng > 0)
            {
                curr_loc.dst = GetDistance(strt_loc.lat, strt_loc.lng, curr_loc.lat, curr_loc.lng);

                if (curr_loc.dst >= dist_50m)
                {
                    String[] time = coln[(int)gpslogger.tim].Split('.');     // Separate the timestamp in GPS logger into second and milli

                    double euclidean = Math.Abs(curr_loc.dst - dist_50m) - Math.Abs(prev_loc.dst - dist_50m);

                    if (euclidean > 0)
                    {
                        timeStamp info = new timeStamp(listGPS.Count, int.Parse(time[0]), int.Parse(time[1]), prev_loc, !Convert.ToBoolean(listGPS.Count % 2));
                        listGPS.Add(info);

                        strt_loc = prev_loc;

                        dist_50m = 50.0 + (dist_50m - prev_loc.dst);

                        Console.WriteLine(">> Save Previous GPS Record. distance:" + prev_loc.dst + " next interval:" + dist_50m + " is100m:" + Convert.ToBoolean(0 % 2) + " is100m:" + Convert.ToBoolean(1 % 2) + " is100m:" + Convert.ToBoolean(2 % 2));

                    }
                    else
                    {
                        timeStamp info = new timeStamp(listGPS.Count, int.Parse(time[0]), int.Parse(time[1]), curr_loc, !Convert.ToBoolean(listGPS.Count % 2));
                        listGPS.Add(info);

                        strt_loc = curr_loc;

                        dist_50m = 50.0 + (dist_50m - curr_loc.dst);

                        Console.WriteLine(">> Save Current  GPS Record. distance:" + curr_loc.dst + " next interval:" + dist_50m + " is100m:" + Convert.ToBoolean(listGPS.Count % 2));
                    }

                    curr_loc.dst = GetDistance(strt_loc.lat, strt_loc.lng, curr_loc.lat, curr_loc.lng);
                }

                prev_loc = curr_loc;
                
            }
            else
            {
                String[] time = coln[(int)gpslogger.tim].Split('.');     // Separate the timestamp in GPS logger into second and milli

                dateSurvey = FromUnixTime(long.Parse(time[0]));

                strt_loc = curr_loc;
                prev_loc = curr_loc;

                strt_loc.dst = 0;
                prev_loc.dst = 0;
            }
        }

        public static double angle2rad(double a)
        {
            return a * (Math.PI / 180);
        }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double dLat = angle2rad(lat1 - lat2);
            double dLon = angle2rad(lng1 - lng2);

            double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                       Math.Cos(angle2rad(lat1)) * Math.Cos(angle2rad(lat2)) *
                       Math.Pow(Math.Sin(dLon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EARTH_RADIUS * c;

            return distance;
        }

    }
}
