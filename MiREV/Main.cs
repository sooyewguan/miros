using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using GMap.NET;
using System.Runtime.InteropServices;

using System.Data.SqlClient;
using System.Net.Http;
using System.Xml;
using System.Threading;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Timers;
using MiREV.Properties;
using System.Net;
using System.Net.Sockets;
using Emgu.CV;

namespace MiREV
{
    public partial class Main : Form
    {
        enum Vida : int        // Column Index for the Vida Database
        {
            Id = 0,
            Coder_name = 1,
            Coding_date = 2,
            Road_survey_date = 3,
            Image_reference = 4,
            Road_name = 5,
            Section = 6,
            Distance = 7,
            Length = 8,
            Latitude = 9,
            Longitude = 10,
            Landmark = 11,
            Comments = 12,
            Carriageway_label = 13,
            Upgrade_cost = 14,
            Motorcycle_observed_flow = 15,
            Bicycle_observed_flow = 16,
            Pedestrian_observed_flow_across_the_road = 17,
            Pedestrian_observed_flow_along_the_road_driver_side = 18,
            Pedestrian_observed_flow_along_the_road_passenger_side = 19,
            Land_use_driver_side = 20,
            Land_use_passenger_side = 21,
            Area_type = 22,
            Speed_limit = 23,
            Motorcycle_speed_limit = 24,
            Truck_speed_limit = 25,
            Differential_speed_limits = 26,
            Median_type = 27,
            Centreline_rumble_strips = 28,
            Roadside_severity_driver_side_distance = 29,
            Roadside_severity_driver_side_object = 30,
            Roadside_severity_passenger_side_distance = 31,
            Roadside_severity_passenger_side_object = 32,
            Shoulder_rumble_strips = 33,
            Paved_shoulder_driver_side = 34,
            Paved_shoulder_passenger_side = 35,
            Intersection_type = 36,
            Intersection_channelisation = 37,
            Intersecting_road_volume = 38,
            Intersection_quality = 39,
            Property_access_points = 40,
            Number_of_lanes = 41,
            Lane_width = 42,
            Curvature = 43,
            Quality_of_curve = 44,
            Grade = 45,
            Road_condition = 46,
            Skid_resistance = 47,
            Delineation = 48,
            Street_lighting = 49,
            Pedestrian_crossing_facilities_inspected_road = 50,
            Pedestrian_crossing_quality = 51,
            Pedestrian_crossing_facilities_intersecting_road = 52,
            Pedestrian_fencing = 53,
            Speed_management_traffic_calming = 54,
            Vehicle_parking = 55,
            Sidewalk_driver_side = 56,
            Sidewalk_passenger_side = 57,
            Service_road = 58,
            Facilities_for_motorised_two_wheelers = 59,
            Facilities_for_bicycles = 60,
            Roadworks = 61,
            Sight_distance = 62,
            Vehicle_flow_AADT = 63,
            Motorcycle_PERCENT = 64,
            Pedestrian_peak_hour_flow_across_the_road = 65,
            Pedestrian_peak_hour_flow_along_the_road_driver_side = 66,
            Pedestrian_peak_hour_flow_along_the_road_passenger_side = 67,
            Bicycle_peak_hour_flow = 68,
            Operating_Speed_85th_percentile = 69,
            Operating_Speed_mean = 70,
            Roads_that_cars_can_read = 71,
            Vehicle_Occupant_Star_Rating_Policy_Target = 72,
            Motorcycle_Star_Rating_Policy_Target = 73,
            Pedestrian_Star_Rating_Policy_Target = 74,
            Bicycle_Star_Rating_Policy_Target = 75,
            Annual_Fatality_Growth_Multiplier = 76,
            School_zone_warning = 77,
            School_zone_crossing_supervisor = 78
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

        public class imageInfo
        {
            public int tag { get; set; }

            public locationManager location { get; set; }

            public string img_left { get; set; }
            public string img_cent { get; set; }
            public string img_rght { get; set; }

            public Boolean is100m { get; set; }
            public int vida_id { get; set; }

            public imageInfo(int tag, locationManager loc, string lft, string cnt, string rgt, Boolean is100m, int vida_id)
            {
                this.tag = tag;
                this.location = loc;

                this.img_left = lft;
                this.img_cent = cnt;
                this.img_rght = rgt;

                this.is100m = is100m;
                this.vida_id = vida_id;
            }
        }

        MapViewer mapViewer;
        ImageLeft imageLeft;
        ImageCent imageCent;
        ImageRght imageRght;
        CodePanel codePanel;
        ProjPanel projPanel;
        IncGauges incGauges;
        Radius radius;

        public Matrix<double> cam_left_intr = new Matrix<double>(new double[3, 3] { { 624.58764006,0,707.4767552 }, { 0,619.76097575,430.72793602 }, { 0,0, 1,} });
        public Matrix<double> cam_right_intr = new Matrix<double>(new double[3, 3] { { 627.50791955,0,670.36155163 }, { 0,628.15595222,410.00911493 }, { 0, 0,1,} });
        public Matrix<double> cam_left_dist = new Matrix<double>(new double[] { -0.33529885,0.12410999,0,0,-0.02183655 });
        public Matrix<double> cam_right_dist = new Matrix<double>(new double[] { -0.31407998,0.09706118,0, 0,-0.01319174 });
        public Matrix<double> R = new Matrix<double>(new double[3, 3] { { 0.9992885,0.0376054,-0.00288683 }, { -0.03769141,0.99847203,-0.04040994 }, { 0.00136279,0.04049,0.99917901 } });
        public Matrix<double> T = new Matrix<double>(new double[] { -0.31407998, 0.09706118, 0, 0, -0.01319174 });
        public Matrix<double> P1 = new Matrix<double>(new double[3, 4] { { 623.95846399,0,747.533741,0}, { 0,623.95846399,417.59704208,0 }, { 0,0,1,0} });
        public Matrix<double> P2 = new Matrix<double>(new double[3, 4] { { 623.95846399, 0,747.533741,- 14891.32038499 }, { 0,623.95846399,417.59704208, 0 }, { 0,0,1,0} });

        private Boolean isPanelShown = false;

        public int interval = 2;

        PointLatLng start;

        public String conStrng = null;
        public String filepath = null;
        public String filename = null;
        public int selectedID = 0;
        public int trackValue = 0;

        public int fullWidth, fullHeight, halfWidth, halfHeight;

        public String hvalue_filename = "homography.calib";
        public double[,] hValue = new double[3, 3];

        public String zvalue_filename = "height.calib";
        public double[] zValue = new double[12];

        public String hplane_filename = "homography.box";
        public Point[] hPlane = new Point[4];

        public Boolean enableHomography = false;

        public String[] image_foldername = { "ImageLeft", "ImageCent", "ImageRght" };

        List<imageInfo> imageList = new List<imageInfo>();

        List<int> left_id = new List<int>();
        List<int> cent_id = new List<int>();
        List<int> rght_id = new List<int>();
        private int[] curr_im = new int[3];

        private int strt_index = 0;
        private int curr_index = 0;
        private int last_index = 0;
        private int fps = 1000 / 20;

        public Boolean isVideoPlaying = false;
        public Boolean isInfoLocked = false;
        public Boolean shouldUpdateTrackbar = true;

        public double screenRatio = 1;


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        private const int WM_NCLBUTTONDBLCLK = 0x00A3;

        // [SOO 20200831] Socket Server for 360
        private Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private Socket clientSocket;
        private EndPoint epLocal, epRemote;
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 4567;
        private byte[] buffer = new byte[BUFFER_SIZE];

        private Process process;

        public Main()
        {
            /*using (StreamReader r = new StreamReader("camParams.json"))
            {
                string json = r.ReadToEnd();
                dynamic items=Newtonsoft.Json.Linq.JObject.Parse(json);
                //cam_left_intr = items["CameraParameters1"]["IntrinsicMatrix"].to;
                //cam_right_intr = items["CameraParameters2"]["IntrinsicMatrix"];
                //cam_left_dist = items["CameraParameters1"]["RadialDistortion"];
                //cam_right_dist = items["CameraParameters2"]["RadialDistortion"];
                //R = items["RotationOfCamera2"];
               // T = items["TranslationOfCamera2"];
                //P1 = items["P1"];
                //P2 = items["P2"].ToObject(typeof(Matrix<double>));
            }*/

            if (Environment.OSVersion.Version.Major >= 6)                   // Ignore HDPI Windwos Scaling
                SetProcessDPIAware();

            System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.Bounds;

            screenRatio = workingRectangle.Width / 3840.0;

            Console.WriteLine(workingRectangle + " ratio:" + screenRatio + " " + screenRatio);

            InitializeComponent();

            this.Location = new Point((int)(Properties.Settings.Default.MainLocation.X * screenRatio), (int)(Properties.Settings.Default.MainLocation.Y * screenRatio));
            //this.Location = new Point((int)(Properties.Settings.Default.MainLocation.X * 0), (int)(Properties.Settings.Default.MainLocation.Y *0));
            this.Size = new Size((int)(this.Width * screenRatio), (int)(this.Height * screenRatio));
            this.Padding = new Padding((int)(10.0 * screenRatio));

            Debug.WriteLine("start " + Location.X + "," + Location.Y + " " + Size.Width);

            btnNew.Size = new Size((int)(btnNew.Size.Width * screenRatio), (int)(btnNew.Size.Height * screenRatio));
            btnNew.Location = new Point((int)(btnNew.Location.X * screenRatio), (int)(btnNew.Location.Y * screenRatio));
            btnNew.Image = new Bitmap(Resources.new_project, new Size((int)(64.0 * screenRatio), (int)(64.0 * screenRatio)));

            btnOpen.Size = new Size((int)(btnOpen.Size.Width * screenRatio), (int)(btnOpen.Size.Height * screenRatio));
            btnOpen.Location = new Point((int)(btnOpen.Location.X * screenRatio), (int)(btnOpen.Location.Y * screenRatio));
            btnOpen.Image = new Bitmap(Resources.open_project, new Size((int)(64.0 * screenRatio), (int)(64.0 * screenRatio)));

            btnExport.Size = new Size((int)(btnExport.Size.Width * screenRatio), (int)(btnExport.Size.Height * screenRatio));
            btnExport.Location = new Point((int)(btnExport.Location.X * screenRatio), (int)(btnExport.Location.Y * screenRatio));
            btnExport.Image = new Bitmap(Resources.export_project, new Size((int)(64.0 * screenRatio), (int)(64.0 * screenRatio)));

            btnPlay.Size = new Size((int)(btnPlay.Width * screenRatio), (int)(btnPlay.Height * screenRatio));
            btnPlay.Location = new Point((int)(btnPlay.Location.X * screenRatio), (int)(btnPlay.Location.Y * screenRatio));
            btnPlay.Image = new Bitmap(Resources.play, new Size((int)(48.0 * screenRatio), (int)(48.0 * screenRatio)));

            btnProjector.Size = new Size((int)(btnProjector.Width * screenRatio), (int)(btnProjector.Height * screenRatio));
            btnProjector.Location = new Point((int)(btnProjector.Location.X * screenRatio), (int)(btnProjector.Location.Y * screenRatio));
            btnProjector.Image = new Bitmap(Resources.show, new Size((int)(48.0 * screenRatio), (int)(48.0 * screenRatio)));

            btnLock.Size = new Size((int)(btnLock.Width * screenRatio), (int)(btnLock.Height * screenRatio));
            btnLock.Location = new Point((int)(btnLock.Location.X * screenRatio), (int)(btnLock.Location.Y * screenRatio));
            btnLock.Image = new Bitmap(Resources.unlocked, new Size((int)(48.0 * screenRatio), (int)(48.0 * screenRatio)));

            trackBar.Size = new Size((int)(trackBar.Width * screenRatio), (int)(trackBar.Height * screenRatio));
            trackBar.Location = new Point((int)(trackBar.Location.X * screenRatio), (int)(trackBar.Location.Y * screenRatio));

            btnNext.Size = new Size((int)(btnNext.Size.Width * screenRatio), (int)(btnNext.Size.Height * screenRatio));
            btnNext.Location = new Point((int)(btnNext.Location.X * screenRatio), (int)(btnNext.Location.Y * screenRatio));

            btnPrev.Size = new Size((int)(btnPrev.Size.Width * screenRatio), (int)(btnPrev.Size.Height * screenRatio));
            btnPrev.Location = new Point((int)(btnPrev.Location.X * screenRatio), (int)(btnPrev.Location.Y * screenRatio));

            txtTAG.Size = new Size((int)(txtTAG.Size.Width * screenRatio), (int)(txtTAG.Size.Height * screenRatio));
            txtTAG.Location = new Point((int)(txtTAG.Location.X * screenRatio), (int)(txtTAG.Location.Y * screenRatio));
            txtTAG.Font = new Font("Microsoft Sans Serif", (float)(8 + 4 * screenRatio));
            //txtTAG.Font = new Font("Microsoft Sans Serif", (float)(12.0 * screenRatio));

            this.KeyPreview = true;

            SetupServer();

            try
            {
                process = new Process();
                process.StartInfo.FileName = "360.exe";
                //process.StartInfo.Arguments = "-parentHWND " + panel1.Handle.ToInt32() + " " + Environment.CommandLine;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.CreateNoWindow = true;

                //process.Start();
                //process.WaitForInputIdle();
                // Doesn't work for some reason ?!
                //unityHWND = process.MainWindowHandle;
                //EnumChildWindows(panel1.Handle, WindowEnum, IntPtr.Zero);

                //unityHWNDLabel.Text = "Unity HWND: 0x" + unityHWND.ToString("X8");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ".\nCheck if Container.exe is placed next to UnityGame.exe.");
            }

        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            MessageBox.Show("Closing called");

        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                mapViewer.BringToFront();
                imageLeft.BringToFront();
                imageRght.BringToFront();
                imageCent.BringToFront();
                codePanel.BringToFront();
                incGauges.BringToFront();
                radius.BringToFront();
                this.Focus();
            }

            base.WndProc(ref m);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //Location = Properties.Settings.Default.MainLocation;

            mapViewer = new MapViewer(this);
            imageLeft = new ImageLeft(this);
            imageRght = new ImageRght(this);
            imageCent = new ImageCent(this);
            codePanel = new CodePanel(this);
            projPanel = new ProjPanel(this);
            incGauges = new IncGauges(this);
            radius = new Radius(this);

            ToolTip toolTip = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 2000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(this.btnNew, "New Project");
            toolTip.SetToolTip(this.btnOpen, "Open Project");
            toolTip.SetToolTip(this.btnExport, "Export CSV");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            projPanel.ShowDialog();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "C:\\Users\\Valued User\\Desktop\\r1";
            openFileDialog.Filter = "Project file (*.mdf)|*.mdf|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ValidateNames = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog.FileName;
                filepath = Path.GetDirectoryName(filename);

                Debug.WriteLine(filename);

                //conStrng = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=" + filename + ";Integrated Security=true";
                conStrng = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + filename + ";Integrated Security=true";
                loadProject();

            }
        }

        private void trackBar_ValueChanged(object sender, System.EventArgs e)
        {
            if (shouldUpdateTrackbar)
            {
                if (trackBar.Value < imageList.Count)
                {
                    if (trackBar.Value < trackValue)
                    {
                        Debug.WriteLine("UnLock");
                        toggleLock(true);
                    }
                    trackValue = trackBar.Value;

                    if (changeImag(trackBar.Value))
                    {
                        updateInfo(trackBar.Value);
                        // [SOO 20200831] Disable Inclinometer
                        // updateInMeter(trackBar.Value);
                        // updateRadius(trackBar.Value);
                        mapViewer.MarkerImageSwitch(trackBar.Value);
                        txtTAG.Text = (trackBar.Value).ToString();
                    }
                    //else
                    //{
                    //    MessageBox.Show("Image file could not be found in the directory.");
                    //}
                }
                else
                {
                    Debug.WriteLine("Invalid Trackbar Value:" + trackBar.Value + " with ImageList Size:" + imageList.Count);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            updateTrackbar(getNext100m(trackBar.Value, interval));
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            updateTrackbar(getPrev100m(trackBar.Value, interval));
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                btnNext.Text = ">";
                btnPrev.Text = "<";

                interval = 1;
                trackBar.SmallChange = interval;
                trackBar.LargeChange = interval;
            }
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            if (trackBar.Enabled)
            {
                switch (e.KeyCode)
                {
                    case Keys.ControlKey:
                        Debug.WriteLine("Ctrl");
                        btnNext.Text = ">>";
                        btnPrev.Text = "<<";
                        interval = 2;
                        trackBar.SmallChange = interval;
                        trackBar.LargeChange = interval;
                        break;

                    case Keys.Left:
                        if (!trackBar.Focused)
                        {
                            updateTrackbar(getPrev100m(trackBar.Value, interval));
                        }
                        break;
                    case Keys.A:
                        updateTrackbar(getPrev100m(trackBar.Value, interval));

                        break;
                    case Keys.Right:
                        if (!trackBar.Focused)
                        {
                            updateTrackbar(getNext100m(trackBar.Value, interval));
                        }
                        break;
                    case Keys.D:
                        updateTrackbar(getNext100m(trackBar.Value, interval));

                        break;
                }
            }

        }

        public void loadProject()
        {
            if (addImageInfo())
            {
                // [SOO 20200831] disable width measurement
                // enableHomography = updateHomoValue() & updateHomoPlane() & updateHeightValue();
                enableHomography = false;

                if (enableHomography)
                {
                    imageCent.showHomography(true);
                }
            }
        }

        public void moveImagePanel(Point offset)
        {
            imageLeft.Location = new Point(imageLeft.Location.X + offset.X, imageLeft.Location.Y + offset.Y);
            imageRght.Location = new Point(imageRght.Location.X + offset.X, imageRght.Location.Y + offset.Y);
        }

        public void resizeImagePanel(int w, int h)
        {
            imageLeft.Width = w;
            imageLeft.Height = h;

            imageRght.Width = w;
            imageRght.Height = h;
        }

        public int getNext100m(int current, int offset)
        {
            if (offset > 1)
            {
                //offset = 2 - (current + offset) % 2;
                while (current < trackBar.Maximum)
                {
                    current++;
                    if (imageList[current].is100m)
                        break;
                }
            }
            else
            {
                current++;
            }

            if (current > trackBar.Maximum)
                current = trackBar.Maximum;

            return current;
        }

        public int getPrev100m(int current, int offset)
        {
            if (offset > 1)
            {
                while (current > trackBar.Minimum)
                {
                    current--;

                    if (imageList[current].is100m)
                        break;
                }
            }
            else
            {
                current--;
            }


            if (current < trackBar.Minimum)
                current = trackBar.Minimum;

            return current;
        }

        public void updateTrackbar(int n)
        {
            //if (trackBar.Enabled)
            //{
            if (n > trackBar.Maximum)
                trackBar.Value = trackBar.Maximum;
            else if (n < trackBar.Minimum)
                trackBar.Value = trackBar.Minimum;
            else
                trackBar.Value = n;
            //}
        }

        public void toggleControls(Boolean b)
        {
            btnExport.Enabled = b;
            trackBar.Enabled = b;
            btnPrev.Enabled = b;
            btnNext.Enabled = b;
            btnPlay.Enabled = b;
            btnProjector.Enabled = b;
            btnLock.Enabled = b;

            txtTAG.Enabled = b;

            imageLeft.Enabled = b;
            imageCent.Enabled = b;
            imageRght.Enabled = b;
            codePanel.Enabled = b;
            mapViewer.Enabled = b;
            incGauges.Enabled = b;
            radius.Enabled = b;
        }

        public void toggleImageHolder(Boolean b)
        {
            if (b)
            {
                //process.Start();
                //process.WaitForInputIdle();

                imageLeft.Show();
                //imageRght.Show();
                //imageCent.Show();
                mapViewer.Show();
                codePanel.Show();
                incGauges.Show();
                radius.Show();
                isPanelShown = true;


            }
            else
            {
                //process.Kill();

                imageLeft.Hide();
                imageRght.Hide();
                imageCent.Hide();
                mapViewer.Hide();
                codePanel.Hide();
                incGauges.Hide();
                radius.Hide();

            }
        }

        public Boolean addImageInfo()
        {
            Debug.WriteLine(conStrng);

            Boolean success = true;

            try
            {
                mapViewer.remMarker();
                mapViewer.remRoutes();
                imageList.Clear();

                using (SqlConnection con = new SqlConnection(conStrng))
                {
                    using (SqlCommand com = con.CreateCommand())
                    {
                        /*com.CommandText = "SELECT * FROM Vida";
                        con.Open();
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mapViewer.addMarker(imageList.Count, reader.GetDouble((int)Vida.Latitude), reader.GetDouble((int)Vida.Longitude));

                                imageInfo info = new imageInfo(reader.GetInt32(0), reader.GetDouble(9), reader.GetDouble(10));
                                imageList.Add(info);

                                Debug.WriteLine("Add: " + reader.GetInt32(0) + " " + reader.GetDouble(9) + " " + reader.GetDouble(10));
                            }
                        } */

                        com.CommandText = "SELECT * FROM Images";
                        con.Open();
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // [SOO 20200831] update Radis V2 data structure loading
                                mapViewer.addMarker(imageList.Count, double.Parse(reader["Latitude"].ToString()), double.Parse(reader["Longitude"].ToString()));

                                locationManager location = new locationManager(double.Parse(reader["Latitude"].ToString()), double.Parse(reader["Longitude"].ToString()), double.Parse(reader["Longitude"].ToString()), double.Parse(reader["Longitude"].ToString()), double.Parse(reader["Longitude"].ToString()));


                                imageInfo info = new imageInfo(int.Parse(reader["Id"].ToString()), location, reader["Image_left"].ToString(), reader["Image_cent"].ToString(), reader["Image_rght"].ToString(), Convert.ToBoolean(int.Parse(reader["Is100m"].ToString())), int.Parse(reader["Vida_Id"].ToString()));
                                imageList.Add(info);

                                //Debug.WriteLine("Add: " + reader.GetInt32(0) + " " + reader.GetDouble(1) + " " + reader.GetDouble(2) + " " + reader.GetString(6) + " " + reader.GetString(7) + " " + reader.GetString(8) + " " + reader.GetBoolean(9) + " TAG:" + reader.GetInt32(10));
                                Debug.WriteLine("Add: " + reader["Image_left"].ToString() + " " + reader["Image_cent"].ToString() + " " + reader["Image_rght"].ToString());

                            }
                        }
                    }
                }

                if (imageList.Count > 0)
                {
                    toggleControls(true);
                    toggleImageHolder(true);

                    trackBar.Minimum = 0;
                    trackBar.Maximum = imageList.Count - 1;
                    trackBar.SmallChange = interval;
                    trackBar.Value = 0;
                    txtTAG.Text = "0";
                    mapViewer.MarkerImageSwitch(trackBar.Value);

                    start = new PointLatLng(imageList[0].location.lat, imageList[0].location.lng);
                    mapViewer.setPosition(start);
                    mapViewer.addOverlay();

                    if (changeImag(0))
                    {
                        updateInfo(0);
                    }
                    else
                    {
                        success = false;
                        MessageBox.Show("Unable to find the image files at this GPS location.");
                    }
                }
                else
                {
                    success = false;
                    MessageBox.Show("Error loading the database. Total data entry: " + imageList.Count);
                }

            }
            catch (Exception ex)
            {
                success = false;
                toggleControls(false);
                toggleImageHolder(false);
                MessageBox.Show(ex.Message);
            }

            return success;
        }

        public String[] getImageName(int index)
        {
            if (index >= imageList.Count())
            {
                index = imageList.Count() - 1;
            }

            String[] name = new String[3];

            name[0] = imageList[index].img_left;
            name[1] = imageList[index].img_cent;
            name[2] = imageList[index].img_rght;

            /*using (SqlConnection con = new SqlConnection(conStrng))
            {
                using (SqlCommand com = con.CreateCommand())
                {
                    com.CommandText = "SELECT TOP 1 * FROM Images WHERE (Id=@id)";
                    com.Parameters.AddWithValue("@id", imageList[index].tag);
                    con.Open();

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())      // Table:Images {Id, Image_left, Image_cent, Image_rght}
                        {
                            name[0] = reader.GetString(1);
                            name[1] = reader.GetString(2);
                            name[2] = reader.GetString(3);

                            //Debug.WriteLine(reader.GetString(2));
                        }
                        else
                        {
                            MessageBox.Show("No record found on this GPS Coordinates.");
                            selectedID = 0;
                        }
                    }
                }
            }*/

            return name;
        }

        public Boolean changeImag(int index)
        {
            imageCent.showMeasurement(false);
            imageCent.startPoint = new Point();
            imageCent.endedPoint = new Point();

            Boolean validImage = true;

            String[] image_name = getImageName(index);

            image_name[0] = filepath + "\\" + image_foldername[0] + "\\" + image_name[0];
            image_name[1] = filepath + "\\" + image_foldername[1] + "\\" + image_name[1];
            image_name[2] = filepath + "\\" + image_foldername[2] + "\\" + image_name[2];

            if (File.Exists(image_name[0]))
                imageLeft.LoadImage(image_name[0]);
            else
                validImage = false;

            if (File.Exists(image_name[1]))
            {
                try
                {
                    if(clientSocket != null && clientSocket.Connected)
                        clientSocket.Send(Encoding.ASCII.GetBytes(image_name[1]));
                }
                catch (SocketException)
                {
                    Console.WriteLine("Client forcefully disconnected");
                    clientSocket.Close();
                    clientSocket = null;
                }
            } else
                validImage = false;

            //if (File.Exists(image_name[1]))
            //    imageCent.LoadImage(image_name[1]);
            //else
            //    validImage = false;

            //if (File.Exists(image_name[2]))
            //    imageRght.LoadImage(image_name[2]);
            //else
            //    validImage = false;

            //incGauges.animation(locationManager);

            return validImage;
        }

        public void updateInMeter(int index)
        {
            try
            {
                incGauges.animation(imageList[index].location.acx, imageList[index + 1].location.acx, imageList[index + 2].location.acx, imageList[index].location.acy, imageList[index + 1].location.acy, imageList[index + 2].location.acy);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void updateRadius(int index)
        {
            try
            {
                radius.CalcRadius(imageList[index].location.bea, imageList[index + 1].location.bea, imageList[index + 2].location.bea);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void updateInfo(int index)
        {
            using (SqlConnection con = new SqlConnection(conStrng))
            {
                using (SqlCommand com = con.CreateCommand())
                {
                    /*com.CommandText = "SELECT TOP 1 * FROM Vida WHERE (Latitude=@latitude AND Longitude=@lngitude)";
                    com.Parameters.AddWithValue("@latitude", imageList[index].lat);
                    com.Parameters.AddWithValue("@lngitude", imageList[index].lng);
                    con.Open();*/

                    selectedID = imageList[index].vida_id;

                    if (selectedID > 0)
                    {
                        if (isInfoLocked)
                        {
                            infoCopy(selectedID);
                        }
                    }
                    Console.WriteLine("Tag:" + imageList[index].tag + " VivaId:" + selectedID);

                    com.CommandText = "SELECT TOP 1 * FROM Vida WHERE (Id=@id)";
                    com.Parameters.AddWithValue("@id", selectedID);
                    con.Open();

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine(reader.GetString((int)Vida.Coding_date));
                            Console.WriteLine(DateTime.Now.Date);

                            codePanel.codeList1._1_01_Coder_name = reader.IsDBNull((int)Vida.Coder_name) ? null : reader.GetString((int)Vida.Coder_name);
                            //[SOO 20200831] Invalid Date format from Data Import
                            //codePanel.codeList1._1_02_Coding_date = reader.IsDBNull((int)Vida.Coding_date) ? DateTime.Now.Date : DateTime.Parse(reader.GetString((int)Vida.Coding_date));
                            //codePanel.codeList1._1_03_Road_survey_date = reader.IsDBNull((int)Vida.Road_survey_date) ? default(DateTime).Date : DateTime.Parse(reader.GetString((int)Vida.Road_survey_date));
                            codePanel.codeList1._1_02_Coding_date = DateTime.Now.Date;
                            codePanel.codeList1._1_03_Road_survey_date = DateTime.Now.Date;

                            codePanel.codeList1._1_04_Image_reference = reader.IsDBNull((int)Vida.Image_reference) ? null : reader.GetString((int)Vida.Image_reference);

                            if (reader.IsDBNull((int)Vida.Road_name))
                                updateRoadName(imageList[index].location.lat, imageList[index].location.lng);
                            else
                                codePanel.codeList1._1_05_Road_name = reader.GetString((int)Vida.Road_name);

                            /*if (!reader.IsDBNull(5))
                            {
                                Thread thread = new Thread(delegate()
                                {
                                    codePanel.codeList._1_05_Road_name = updateRoadName(imageList[index].lat, imageList[index].lng);
                                    this.Invoke(new Action(codePanel.refreshPropertyGrid));
                                });
                                thread.Start();
                            }
                            else
                            {
                                codePanel.codeList._1_05_Road_name = reader.GetString(5);
                            }*/

                            codePanel.codeList1._1_06_Section = reader.IsDBNull((int)Vida.Section) ? null : reader.GetString((int)Vida.Section);
                            codePanel.codeList1._1_07_Distance = reader.IsDBNull((int)Vida.Distance) ? null : reader.GetString((int)Vida.Distance);
                            codePanel.codeList1._1_08_Length = reader.IsDBNull((int)Vida.Length) ? null : reader.GetString((int)Vida.Length);

                            //codePanel.codeList1._1_09_Latitude = imageList[index].lat.ToString(); 
                            //codePanel.codeList1._1_10_Longitude = imageList[index].lng.ToString();

                            codePanel.codeList1._1_09_Latitude = reader.IsDBNull((int)Vida.Latitude) ? null : reader.GetDouble((int)Vida.Latitude).ToString();
                            codePanel.codeList1._1_10_Longitude = reader.IsDBNull((int)Vida.Longitude) ? null : reader.GetDouble((int)Vida.Longitude).ToString();

                            codePanel.codeList1._1_11_Landmark = reader.IsDBNull((int)Vida.Landmark) ? null : reader.GetString((int)Vida.Landmark);
                            codePanel.codeList1._1_12_Comments = reader.IsDBNull((int)Vida.Comments) ? null : reader.GetString((int)Vida.Comments);

                            codePanel.codeList1._1_13_Upgrade_cost = reader.IsDBNull((int)Vida.Upgrade_cost) ? null : reader.GetString((int)Vida.Upgrade_cost);
                            codePanel.codeList1._1_14_Roadworks = reader.IsDBNull((int)Vida.Roadworks) ? null : reader.GetString((int)Vida.Roadworks);

                            codePanel.codeList1._2_01_Annual_Fatality_Growth_Multiplier = reader.IsDBNull((int)Vida.Annual_Fatality_Growth_Multiplier) ? null : reader.GetString((int)Vida.Annual_Fatality_Growth_Multiplier);
                            codePanel.codeList1._2_02_Vehicle_occupant_star_rating_policy_target = reader.IsDBNull((int)Vida.Vehicle_Occupant_Star_Rating_Policy_Target) ? null : reader.GetString((int)Vida.Vehicle_Occupant_Star_Rating_Policy_Target);
                            codePanel.codeList1._2_03_Motorcycle_star_rating_policy_target = reader.IsDBNull((int)Vida.Motorcycle_Star_Rating_Policy_Target) ? null : reader.GetString((int)Vida.Motorcycle_Star_Rating_Policy_Target);
                            codePanel.codeList1._2_04_Pedestrian_star_rating_policy_target = reader.IsDBNull((int)Vida.Pedestrian_Star_Rating_Policy_Target) ? null : reader.GetString((int)Vida.Pedestrian_Star_Rating_Policy_Target);
                            codePanel.codeList1._2_05_Bicycle_star_rating_policy_target = reader.IsDBNull((int)Vida.Bicycle_Star_Rating_Policy_Target) ? null : reader.GetString((int)Vida.Bicycle_Star_Rating_Policy_Target);
                            //codePanel.codeList._2_06_Road_survey_date = reader.IsDBNull((int)Vida.Road_survey_date) ? null : reader.GetString((int)Vida.Road_survey_date);

                            codePanel.codeList1._3_01_Area_type = reader.IsDBNull((int)Vida.Area_type) ? null : reader.GetString((int)Vida.Area_type);
                            codePanel.codeList1._3_02_Land_use_driver_side = reader.IsDBNull((int)Vida.Land_use_driver_side) ? null : reader.GetString((int)Vida.Land_use_driver_side);
                            codePanel.codeList1._3_03_Land_use_passenger_side = reader.IsDBNull((int)Vida.Land_use_passenger_side) ? null : reader.GetString((int)Vida.Land_use_passenger_side);

                            codePanel.codeList1._4_01_Speed_limit = reader.IsDBNull((int)Vida.Speed_limit) ? null : reader.GetString((int)Vida.Speed_limit);
                            codePanel.codeList1._4_02_Motorcycle_speed_limit = reader.IsDBNull((int)Vida.Motorcycle_speed_limit) ? null : reader.GetString((int)Vida.Motorcycle_speed_limit);
                            codePanel.codeList1._4_03_Truck_speed_limit = reader.IsDBNull((int)Vida.Truck_speed_limit) ? null : reader.GetString((int)Vida.Truck_speed_limit);
                            codePanel.codeList1._4_04_Differential_speed_limits = reader.IsDBNull((int)Vida.Differential_speed_limits) ? null : reader.GetString((int)Vida.Differential_speed_limits);
                            codePanel.codeList1._4_05_Speed_management_traffic_calming = reader.IsDBNull((int)Vida.Speed_management_traffic_calming) ? null : reader.GetString((int)Vida.Speed_management_traffic_calming);
                            codePanel.codeList1._4_06_School_zone_warning = reader.IsDBNull((int)Vida.School_zone_warning) ? null : reader.GetString((int)Vida.School_zone_warning);

                            codePanel.codeList2._5_01_Vehicle_flow_AADT = reader.IsDBNull((int)Vida.Vehicle_flow_AADT) ? null : reader.GetString((int)Vida.Vehicle_flow_AADT);
                            codePanel.codeList2._5_02_Motorcycle_PERCENT = reader.IsDBNull((int)Vida.Motorcycle_PERCENT) ? null : reader.GetString((int)Vida.Motorcycle_PERCENT);
                            codePanel.codeList2._5_03_Motorcycle_observed_flow = reader.IsDBNull((int)Vida.Motorcycle_observed_flow) ? null : reader.GetString((int)Vida.Motorcycle_observed_flow);
                            codePanel.codeList2._5_04_Bicycle_observed_flow = reader.IsDBNull((int)Vida.Bicycle_observed_flow) ? null : reader.GetString((int)Vida.Bicycle_observed_flow);
                            codePanel.codeList2._5_05_Bicycle_peak_hour_flow = reader.IsDBNull((int)Vida.Bicycle_peak_hour_flow) ? null : reader.GetString((int)Vida.Bicycle_peak_hour_flow);
                            codePanel.codeList2._5_06_Pedestrian_observed_flow_across_the_road = reader.IsDBNull((int)Vida.Pedestrian_observed_flow_across_the_road) ? null : reader.GetString((int)Vida.Pedestrian_observed_flow_across_the_road);
                            codePanel.codeList2._5_07_Pedestrian_observed_flow_along_the_road_driver_side = reader.IsDBNull((int)Vida.Pedestrian_observed_flow_along_the_road_driver_side) ? null : reader.GetString((int)Vida.Pedestrian_observed_flow_along_the_road_driver_side);
                            codePanel.codeList2._5_08_Pedestrian_observed_flow_along_the_road_passenger_side = reader.IsDBNull((int)Vida.Pedestrian_observed_flow_along_the_road_passenger_side) ? null : reader.GetString((int)Vida.Pedestrian_observed_flow_along_the_road_passenger_side);
                            codePanel.codeList2._5_09_Pedestrian_peak_hour_flow_across_the_road = reader.IsDBNull((int)Vida.Pedestrian_peak_hour_flow_across_the_road) ? null : reader.GetString((int)Vida.Pedestrian_peak_hour_flow_across_the_road);
                            codePanel.codeList2._5_10_Pedestrian_peak_hour_flow_along_the_road_driver_side = reader.IsDBNull((int)Vida.Pedestrian_peak_hour_flow_along_the_road_driver_side) ? null : reader.GetString((int)Vida.Pedestrian_peak_hour_flow_along_the_road_driver_side);
                            codePanel.codeList2._5_11_Pedestrian_peak_hour_flow_along_the_road_passenger_side = reader.IsDBNull((int)Vida.Pedestrian_peak_hour_flow_along_the_road_passenger_side) ? null : reader.GetString((int)Vida.Pedestrian_peak_hour_flow_along_the_road_passenger_side);
                            codePanel.codeList2._5_12_Operating_speed_85th_percentile = reader.IsDBNull((int)Vida.Operating_Speed_85th_percentile) ? null : reader.GetString((int)Vida.Operating_Speed_85th_percentile);
                            codePanel.codeList2._5_13_Operating_speed_mean = reader.IsDBNull((int)Vida.Operating_Speed_mean) ? null : reader.GetString((int)Vida.Operating_Speed_mean);

                            codePanel.codeList2._6_01_Curvature = reader.IsDBNull((int)Vida.Curvature) ? null : reader.GetString((int)Vida.Curvature);
                            codePanel.codeList2._6_02_Quality_of_curve = reader.IsDBNull((int)Vida.Quality_of_curve) ? null : reader.GetString((int)Vida.Quality_of_curve);
                            codePanel.codeList2._6_03_Grade = reader.IsDBNull((int)Vida.Grade) ? null : reader.GetString((int)Vida.Grade);
                            codePanel.codeList2._6_04_Sight_distance = reader.IsDBNull((int)Vida.Sight_distance) ? null : reader.GetString((int)Vida.Sight_distance);
                            codePanel.codeList2._6_05_Roads_that_cars_can_read = reader.IsDBNull((int)Vida.Roads_that_cars_can_read) ? null : reader.GetString((int)Vida.Roads_that_cars_can_read);

                            codePanel.codeList2._7_01_Carriageway_label = reader.IsDBNull((int)Vida.Carriageway_label) ? null : reader.GetString((int)Vida.Carriageway_label);
                            codePanel.codeList2._7_02_Median_type = reader.IsDBNull((int)Vida.Median_type) ? null : reader.GetString((int)Vida.Median_type);
                            codePanel.codeList2._7_03_Number_of_lanes = reader.IsDBNull((int)Vida.Number_of_lanes) ? null : reader.GetString((int)Vida.Number_of_lanes);
                            codePanel.codeList2._7_04_Lane_width = reader.IsDBNull((int)Vida.Lane_width) ? null : reader.GetString((int)Vida.Lane_width);
                            codePanel.codeList2._7_05_Paved_shoulder_driver_side = reader.IsDBNull((int)Vida.Paved_shoulder_driver_side) ? null : reader.GetString((int)Vida.Paved_shoulder_driver_side);
                            codePanel.codeList2._7_06_Paved_shoulder_passenger_side = reader.IsDBNull((int)Vida.Paved_shoulder_passenger_side) ? null : reader.GetString((int)Vida.Paved_shoulder_passenger_side);

                            codePanel.codeList2._8_01_Intersection_type = reader.IsDBNull((int)Vida.Intersection_type) ? null : reader.GetString((int)Vida.Intersection_type);
                            codePanel.codeList2._8_02_Intersection_channelisation = reader.IsDBNull((int)Vida.Intersection_channelisation) ? null : reader.GetString((int)Vida.Intersection_channelisation);
                            codePanel.codeList2._8_03_Intersecting_road_volume = reader.IsDBNull((int)Vida.Intersecting_road_volume) ? null : reader.GetString((int)Vida.Intersecting_road_volume);
                            codePanel.codeList2._8_04_Intersection_quality = reader.IsDBNull((int)Vida.Intersection_quality) ? null : reader.GetString((int)Vida.Intersection_quality);
                            codePanel.codeList2._8_05_Property_access_points = reader.IsDBNull((int)Vida.Property_access_points) ? null : reader.GetString((int)Vida.Property_access_points);

                            codePanel.codeList3._9_01_Roadside_severity_driver_side_object = reader.IsDBNull((int)Vida.Roadside_severity_driver_side_object) ? null : reader.GetString((int)Vida.Roadside_severity_driver_side_object);
                            codePanel.codeList3._9_02_Roadside_severity_driver_side_distance = reader.IsDBNull((int)Vida.Roadside_severity_driver_side_distance) ? null : reader.GetString((int)Vida.Roadside_severity_driver_side_distance);
                            codePanel.codeList3._9_03_Roadside_severity_passenger_side_object = reader.IsDBNull((int)Vida.Roadside_severity_passenger_side_object) ? null : reader.GetString((int)Vida.Roadside_severity_passenger_side_object);
                            codePanel.codeList3._9_04_Roadside_severity_passenger_side_distance = reader.IsDBNull((int)Vida.Roadside_severity_passenger_side_distance) ? null : reader.GetString((int)Vida.Roadside_severity_passenger_side_distance);
                            codePanel.codeList3._9_05_Vehicle_parking = reader.IsDBNull((int)Vida.Vehicle_parking) ? null : reader.GetString((int)Vida.Vehicle_parking);
                            codePanel.codeList3._9_06_Service_road = reader.IsDBNull((int)Vida.Service_road) ? null : reader.GetString((int)Vida.Service_road);

                            codePanel.codeList3._10_01_Street_lighting = reader.IsDBNull((int)Vida.Street_lighting) ? null : reader.GetString((int)Vida.Street_lighting);
                            codePanel.codeList3._10_02_Delineation = reader.IsDBNull((int)Vida.Delineation) ? null : reader.GetString((int)Vida.Delineation);
                            codePanel.codeList3._10_03_Centreline_rumble_strips = reader.IsDBNull((int)Vida.Centreline_rumble_strips) ? null : reader.GetString((int)Vida.Centreline_rumble_strips);
                            codePanel.codeList3._10_04_Shoulder_rumble_strips = reader.IsDBNull((int)Vida.Shoulder_rumble_strips) ? null : reader.GetString((int)Vida.Shoulder_rumble_strips);

                            codePanel.codeList3._11_01_Road_condition = reader.IsDBNull((int)Vida.Road_condition) ? null : reader.GetString((int)Vida.Road_condition);
                            codePanel.codeList3._11_02_Skid_resistance = reader.IsDBNull((int)Vida.Skid_resistance) ? null : reader.GetString((int)Vida.Skid_resistance);

                            codePanel.codeList3._12_01_Pedestrian_crossing_facilities_inspected_road = reader.IsDBNull((int)Vida.Pedestrian_crossing_facilities_inspected_road) ? null : reader.GetString((int)Vida.Pedestrian_crossing_facilities_inspected_road);
                            codePanel.codeList3._12_02_Pedestrian_crossing_quality = reader.IsDBNull((int)Vida.Pedestrian_crossing_quality) ? null : reader.GetString((int)Vida.Pedestrian_crossing_quality);
                            codePanel.codeList3._12_03_School_zone_crossing_supervisor = reader.IsDBNull((int)Vida.School_zone_crossing_supervisor) ? null : reader.GetString((int)Vida.School_zone_crossing_supervisor);
                            codePanel.codeList3._12_04_Pedestrian_crossing_facilities_intersecting_road = reader.IsDBNull((int)Vida.Pedestrian_crossing_facilities_intersecting_road) ? null : reader.GetString((int)Vida.Pedestrian_crossing_facilities_intersecting_road);
                            codePanel.codeList3._12_05_Sidewalk_driver_side = reader.IsDBNull((int)Vida.Sidewalk_driver_side) ? null : reader.GetString((int)Vida.Sidewalk_driver_side);
                            codePanel.codeList3._12_06_Sidewalk_passenger_side = reader.IsDBNull((int)Vida.Sidewalk_passenger_side) ? null : reader.GetString((int)Vida.Sidewalk_passenger_side);
                            codePanel.codeList3._12_07_Pedestrian_fencing = reader.IsDBNull((int)Vida.Pedestrian_fencing) ? null : reader.GetString((int)Vida.Pedestrian_fencing);
                            codePanel.codeList3._12_08_Facilities_for_bicycles = reader.IsDBNull((int)Vida.Facilities_for_bicycles) ? null : reader.GetString((int)Vida.Facilities_for_bicycles);
                            codePanel.codeList3._12_09_Facilities_for_motorised_two_wheelers = reader.IsDBNull((int)Vida.Facilities_for_motorised_two_wheelers) ? null : reader.GetString((int)Vida.Facilities_for_motorised_two_wheelers);

                            codePanel.refreshPropertyGrid();


                        }
                        else
                        {
                            MessageBox.Show("No record found on this GPS Coordinates.");
                            selectedID = 0;
                        }
                    }
                }
            }
        }

        public void updateLaneWidth(String s)
        {
            codePanel.codeList2._7_04_Lane_width = s;
            codePanel.refreshPropertyGrid();

            using (SqlConnection con = new SqlConnection(conStrng))
            {
                using (SqlCommand com = con.CreateCommand())
                {
                    com.CommandText = "UPDATE Vida SET Lane_width=@UpdatedValue WHERE Id=@Index";

                    com.Parameters.AddWithValue("@UpdatedValue", s);
                    com.Parameters.AddWithValue("@Index", selectedID);

                    con.Open();
                    com.ExecuteNonQuery();

                    //Debug.WriteLine(com.CommandText + " Value=" + e.ChangedItem.Value + " Index=" + _main.selectedID);
                }
            }
        }

        public void updateCurvature(String s)
        {
            codePanel.codeList2._6_01_Curvature = s;
            codePanel.refreshPropertyGrid();

            using (SqlConnection con = new SqlConnection(conStrng))
            {
                using (SqlCommand com = con.CreateCommand())
                {
                    com.CommandText = "UPDATE Vida SET Curvature=@UpdatedValue WHERE Id=@Index";

                    com.Parameters.AddWithValue("@UpdatedValue", s);
                    com.Parameters.AddWithValue("@Index", selectedID);

                    con.Open();
                    com.ExecuteNonQuery();

                    //Debug.WriteLine(com.CommandText + " Value=" + e.ChangedItem.Value + " Index=" + _main.selectedID);
                }
            }
        }

        public void updateGrade(String s)
        {
            codePanel.codeList2._6_03_Grade = s;
            codePanel.refreshPropertyGrid();

            using (SqlConnection con = new SqlConnection(conStrng))
            {
                using (SqlCommand com = con.CreateCommand())
                {
                    com.CommandText = "UPDATE Vida SET Grade=@UpdatedValue WHERE Id=@Index";

                    com.Parameters.AddWithValue("@UpdatedValue", s);
                    com.Parameters.AddWithValue("@Index", selectedID);

                    con.Open();
                    com.ExecuteNonQuery();

                    //Debug.WriteLine(com.CommandText + " Value=" + e.ChangedItem.Value + " Index=" + _main.selectedID);
                }
            }
        }

        /*public String updateRoadName(object lat, object lng)
        {
            String s = null;
            String url = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + lng + "&sensor=false";
            Debug.WriteLine(url);


            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(url);

            s = xDoc.SelectSingleNode("/GeocodeResponse/result/address_component/long_name").InnerText;

            using (SqlConnection con = new SqlConnection(conStrng))
            {
                using (SqlCommand com = con.CreateCommand())
                {
                    com.CommandText = "UPDATE Vida SET Road_name=@UpdatedValue WHERE Id=@Index";

                    com.Parameters.AddWithValue("@UpdatedValue", s);
                    com.Parameters.AddWithValue("@Index", selectedID);

                    con.Open();
                    com.ExecuteNonQuery();
                }
            }
            Debug.WriteLine(s);
            return s;
        }*/

        public async void updateRoadName(double lat, double lng)
        {
            String s = null;
            String url = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + lng + "&sensor=false";

            try
            {
                HttpClient client = new HttpClient();
                string xmlContent = await client.GetStringAsync(url);

                if (!String.IsNullOrEmpty(xmlContent))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(xmlContent);

                    s = xDoc.SelectSingleNode("/GeocodeResponse/status").InnerText;

                    if (s.CompareTo("OK") == 0)
                    {
                        s = xDoc.SelectSingleNode("/GeocodeResponse/result/address_component/long_name").InnerText;

                        if (!String.IsNullOrEmpty(s))
                        {
                            using (SqlConnection con = new SqlConnection(conStrng))
                            {
                                using (SqlCommand com = con.CreateCommand())
                                {
                                    com.CommandText = "UPDATE Vida SET Road_name=@UpdatedValue WHERE Id=@Index";

                                    com.Parameters.AddWithValue("@UpdatedValue", s);
                                    com.Parameters.AddWithValue("@Index", selectedID);

                                    con.Open();
                                    com.ExecuteNonQuery();
                                }
                            }
                            codePanel.codeList1._1_05_Road_name = s;
                            codePanel.refreshPropertyGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\nPlease verify your internet connection.");
            }

        }

        /* 
         *      Loading the Homography Matrix from the Configuration File (homography.calib)
         */
        private Boolean updateHomoValue()
        {
            Boolean isCalibrated = true;

            String hFile = filepath + "\\" + hvalue_filename;
            int row = 0;

            try
            {
                using (StreamReader sr = File.OpenText(hFile))
                {
                    string line = "";

                    while (((line = sr.ReadLine()) != null) && row < 3)
                    {
                        //Debug.WriteLine(line);

                        string[] coln = line.Split('\t');       // Split line into arrray every column

                        for (int n = 0; n < 3; n++)
                        {
                            hValue[row, n] = double.Parse(coln[n]);
                        }
                        row++;
                    }
                }

                if (row < 3)
                {
                    isCalibrated = false;
                    MessageBox.Show("Invalid homography calibration file.");
                }

            }
            catch (Exception ex)
            {
                isCalibrated = false;
                MessageBox.Show(hvalue_filename + ": " + ex.Message);
            }

            return isCalibrated;
        }

        /* 
         *      Loading the Homography Region from the Configuration File (homography.box)
         */
        private Boolean updateHomoPlane()
        {
            Boolean isCalibrated = true;

            String hFile = filepath + "\\" + hplane_filename;
            int row = 0;

            try
            {
                using (StreamReader sr = File.OpenText(hFile))
                {
                    string line = "";

                    while (((line = sr.ReadLine()) != null) && row < 4)
                    {
                        string[] coln = line.Split(',');       // Split line into arrray every column
                        hPlane[row] = new Point(int.Parse(coln[0]), int.Parse(coln[1]));

                        row++;
                    }
                }

                if (row < 4)
                {
                    isCalibrated = false;
                    MessageBox.Show("Invalid homography plane file.");
                }

            }
            catch (Exception ex)
            {
                isCalibrated = false;
                MessageBox.Show(hplane_filename + ": " + ex.Message);
            }

            return isCalibrated;
        }

        /* 
         *      Loading the Height Measurement Variable from the Configuration File (height.calib)
         */
        private Boolean updateHeightValue()
        {
            Boolean isCalibrated = true;

            String hFile = filepath + "\\" + zvalue_filename;
            int row = 0;

            try
            {
                using (StreamReader sr = File.OpenText(hFile))
                {
                    string line = "";

                    while (((line = sr.ReadLine()) != null) && row < 12)
                    {
                        //Debug.WriteLine(line);                    
                        zValue[row] = double.Parse(line);
                        row++;
                    }
                }

                if (row < 12)
                {
                    isCalibrated = false;
                    MessageBox.Show("Invalid homography calibration file.");
                }
            }
            catch (Exception ex)
            {
                isCalibrated = false;
                MessageBox.Show(zvalue_filename + ": " + ex.Message);
            }

            return isCalibrated;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            CsvSelector csvSel = new CsvSelector(this);
            csvSel.Show();

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Point location = Location;

            if (WindowState != FormWindowState.Normal)
            {
                Properties.Settings.Default.MainLocation = new Point((int)(RestoreBounds.Location.X / screenRatio), (int)(RestoreBounds.Location.Y / screenRatio));

                if (isPanelShown)
                {
                    Properties.Settings.Default.ImgLeftSize = new Size((int)(imageLeft.RestoreBounds.Size.Width / screenRatio), (int)(imageLeft.RestoreBounds.Size.Height / screenRatio));
                    Properties.Settings.Default.ImgCentSize = new Size((int)(imageCent.RestoreBounds.Size.Width / screenRatio), (int)(imageCent.RestoreBounds.Size.Height / screenRatio));
                    Properties.Settings.Default.ImgRghtSize = new Size((int)(imageRght.RestoreBounds.Size.Width / screenRatio), (int)(imageRght.RestoreBounds.Size.Height / screenRatio));

                    Properties.Settings.Default.ImgLeftLocation = new Point((int)(imageLeft.RestoreBounds.Location.X / screenRatio), (int)(imageLeft.RestoreBounds.Location.Y / screenRatio));
                    Properties.Settings.Default.ImgCentLocation = new Point((int)(imageCent.RestoreBounds.Location.X / screenRatio), (int)(imageCent.RestoreBounds.Location.Y / screenRatio));
                    Properties.Settings.Default.ImgRghtLocation = new Point((int)(imageRght.RestoreBounds.Location.X / screenRatio), (int)(imageRght.RestoreBounds.Location.Y / screenRatio));

                    Properties.Settings.Default.MapSize = new Size((int)(mapViewer.RestoreBounds.Size.Width / screenRatio), (int)(mapViewer.RestoreBounds.Size.Height / screenRatio));
                    Properties.Settings.Default.MapLocation = new Point((int)(mapViewer.RestoreBounds.Location.X / screenRatio), (int)(mapViewer.RestoreBounds.Location.Y / screenRatio));

                    //Properties.Settings.Default.CodeSize = new Size((int)(codePanel.RestoreBounds.Size.Width / screenRatio), (int)(codePanel.RestoreBounds.Size.Height / screenRatio));
                    Properties.Settings.Default.CodeLocation = new Point((int)(codePanel.RestoreBounds.Location.X / screenRatio), (int)(codePanel.RestoreBounds.Location.Y / screenRatio));

                    Properties.Settings.Default.IncGaugLocation = new Point((int)(incGauges.RestoreBounds.Location.X / screenRatio), (int)(incGauges.RestoreBounds.Location.Y / screenRatio));
                    Properties.Settings.Default.IncGaugSize = new Size((int)(incGauges.RestoreBounds.Size.Width / screenRatio), (int)(incGauges.RestoreBounds.Size.Height / screenRatio));
                    Properties.Settings.Default.RadiusLocation = new Point((int)(radius.RestoreBounds.Location.X / screenRatio), (int)(radius.RestoreBounds.Location.Y / screenRatio));
                    Properties.Settings.Default.RadiusSize = new Size((int)(radius.RestoreBounds.Size.Width / screenRatio), (int)(radius.RestoreBounds.Size.Height / screenRatio));
                }
            }
            else
            {
                Properties.Settings.Default.MainLocation = new Point((int)(Location.X / screenRatio), (int)(Location.Y / screenRatio));
                Debug.WriteLine(Location.X + "," + Location.Y);
                if (isPanelShown)
                {
                    Properties.Settings.Default.ImgLeftSize = new Size((int)(imageLeft.Size.Width / screenRatio), (int)(imageLeft.Size.Height / screenRatio));
                    Properties.Settings.Default.ImgCentSize = new Size((int)(imageCent.Size.Width / screenRatio), (int)(imageCent.Size.Height / screenRatio));
                    Properties.Settings.Default.ImgRghtSize = new Size((int)(imageRght.Size.Width / screenRatio), (int)(imageRght.Size.Height / screenRatio));

                    Properties.Settings.Default.ImgLeftLocation = new Point((int)(imageLeft.Location.X / screenRatio), (int)(imageLeft.Location.Y / screenRatio));
                    Properties.Settings.Default.ImgCentLocation = new Point((int)(imageCent.Location.X / screenRatio), (int)(imageCent.Location.Y / screenRatio));
                    Properties.Settings.Default.ImgRghtLocation = new Point((int)(imageRght.Location.X / screenRatio), (int)(imageRght.Location.Y / screenRatio));

                    Properties.Settings.Default.MapSize = new Size((int)(mapViewer.Size.Width / screenRatio), (int)(mapViewer.Size.Height / screenRatio));
                    Properties.Settings.Default.MapLocation = new Point((int)(mapViewer.Location.X / screenRatio), (int)(mapViewer.Location.Y / screenRatio));

                    //Properties.Settings.Default.CodeSize = new Size((int)(codePanel.Size.Width / screenRatio), (int)(codePanel.Size.Height / screenRatio));
                    Properties.Settings.Default.CodeLocation = new Point((int)(codePanel.Location.X / screenRatio), (int)(codePanel.Location.Y / screenRatio));

                    Properties.Settings.Default.IncGaugLocation = new Point((int)(incGauges.Location.X / screenRatio), (int)(incGauges.Location.Y / screenRatio));
                    Properties.Settings.Default.IncGaugSize = new Size((int)(incGauges.Size.Width / screenRatio), (int)(incGauges.Size.Height / screenRatio));
                    Properties.Settings.Default.RadiusLocation = new Point((int)(radius.Location.X / screenRatio), (int)(radius.Location.Y / screenRatio));
                    Properties.Settings.Default.RadiusSize = new Size((int)(radius.Size.Width / screenRatio), (int)(radius.Size.Height / screenRatio));
                }
            }

            Properties.Settings.Default.Save();
        }

        private void txtTAG_TextChanged(object sender, EventArgs e)
        {
            updateTrackbar(int.Parse(txtTAG.Text));
        }

        private void txtTAG_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        public void activatePbZoom(int x, int y, Image img)
        {
            fullWidth = mapViewer.pbZoomWidth / 2;
            fullHeight = mapViewer.pbZoomHeight / 2;

            if (x < fullWidth / 4)
                x = fullWidth / 4;

            if (x > img.Width - fullWidth / 4)
                x = img.Width - fullWidth / 4;

            if (y < fullHeight / 4)
                y = fullHeight / 4;

            if (y > img.Height - fullHeight / 4)
                y = img.Height - fullHeight / 4;

            mapViewer.pbZoomProp = true;

            var dest = new Bitmap(fullWidth, fullHeight, PixelFormat.Format32bppPArgb);
            using (var gr = Graphics.FromImage(dest))
            {
                gr.DrawImage(img,
                                new Rectangle(0, 0, fullWidth, fullHeight), //dest
                                new Rectangle(x - fullWidth / 4, y - fullHeight / 4, fullWidth / 2, fullHeight / 2), //src
                                GraphicsUnit.Pixel);
                gr.DrawLine(Pens.Red, halfWidth + 1, halfHeight - 4, halfWidth + 1, halfHeight - 1);
                gr.DrawLine(Pens.Red, halfWidth + 1, halfHeight + 6, halfWidth + 1, halfHeight + 3);
                gr.DrawLine(Pens.Red, halfWidth - 4, halfHeight + 1, halfWidth - 1, halfHeight + 1);
                gr.DrawLine(Pens.Red, halfWidth + 6, halfHeight + 1, halfWidth + 3, halfHeight + 1);
            }
            if (mapViewer.pbZoomPic != null)
                mapViewer.pbZoomPic.Dispose();

            mapViewer.pbZoomPic = dest;
        }

        public void deactivatePbZoom()
        {
            mapViewer.pbZoomProp = false;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (isVideoPlaying)
            {
                isVideoPlaying = false;
            }
            else
            {
                toggleLock(true);

                if (trackBar.Value < trackBar.Maximum)
                {
                    strt_index = trackBar.Value;
                    last_index = getNext100m(strt_index, interval);

                    Console.WriteLine("Curr:" + curr_index + " last:" + last_index);

                    left_id.Clear();
                    cent_id.Clear();
                    rght_id.Clear();

                    for (int i = 0; i < last_index - strt_index + 1; i++)
                    {
                        String[] image_name = getImageName(strt_index + i);

                        left_id.Add(Int32.Parse(Regex.Match(image_name[0], @"\d+").Value));

                        // [SOO 202200831] file name too long
                        cent_id.Add(Int32.Parse(Regex.Match(image_name[1].Substring(0,10), @"\d+").Value));
                        rght_id.Add(Int32.Parse(Regex.Match(image_name[2], @"\d+").Value));

                        Console.WriteLine("Id:" + left_id[i] + " " + cent_id[i] + " " + rght_id[i]);
                    }

                    curr_index = 0;
                    curr_im[0] = left_id[0];
                    curr_im[1] = cent_id[0];
                    curr_im[2] = rght_id[0];

                    toggleControls(false);
                    isVideoPlaying = true;

                    btnPlay.Enabled = true;
                    btnPlay.Image = new Bitmap(Resources.stop, new Size((int)(48.0 * screenRatio), (int)(48.0 * screenRatio)));
                    new Thread(new ThreadStart(SildeShow_Thread)).Start();
                }
            }
        }

        private void btnProjector_Click(object sender, EventArgs e)
        {
            if (isVideoPlaying)
            {
                isVideoPlaying = false;
            }
            else
            {
                toggleLock(true);

                if (trackBar.Value < trackBar.Maximum)
                {
                    strt_index = trackBar.Value;
                    last_index = trackBar.Maximum;

                    left_id.Clear();
                    cent_id.Clear();
                    rght_id.Clear();

                    Console.WriteLine("Curr:" + curr_index + " last:" + last_index);

                    for (int i = 0; i < last_index - strt_index + 1; i++)
                    {
                        String[] image_name = getImageName(strt_index + i);

                        Console.WriteLine("Id:" + image_name[0] + " " + image_name[1] + " " + image_name[2]);

                        left_id.Add(Int32.Parse(Regex.Match(image_name[0], @"\d+").Value));

                        // [SOO 202200831] file name too long
                        cent_id.Add(Int32.Parse(Regex.Match(image_name[1].Substring(0, 10), @"\d+").Value));
                        rght_id.Add(Int32.Parse(Regex.Match(image_name[2], @"\d+").Value));

                        Console.WriteLine("Id:" + left_id[i] + " " + cent_id[i] + " " + rght_id[i]);
                    }

                    curr_index = 0;
                    curr_im[0] = left_id[0];
                    curr_im[1] = cent_id[0];
                    curr_im[2] = rght_id[0];

                    toggleControls(false);
                    isVideoPlaying = true;

                    btnProjector.Enabled = true;
                    btnProjector.Image = new Bitmap(Resources.stop, new Size((int)(48.0 * screenRatio), (int)(48.0 * screenRatio)));
                    new Thread(new ThreadStart(SildeShow_Thread)).Start();
                }
            }
        }

        public void SildeShow_Thread()
        {
            while (isVideoPlaying)
            {
                String[] image_name = new String[3];

                // [SOO 20200831] fixed image file name
                //image_name[0] = filepath + "\\" + image_foldername[0] + "\\" + String.Format("image-{0:000000000}.jpg", curr_im[0]);
                //image_name[1] = filepath + "\\" + image_foldername[1] + "\\" + String.Format("image-{0:000000000}.jpg", curr_im[1]);
                //image_name[2] = filepath + "\\" + image_foldername[2] + "\\" + String.Format("image-{0:000000000}.jpg", curr_im[2]);
                image_name[0] = filepath + "\\" + image_foldername[0] + "\\" + curr_im[0] + ".jpg";
                image_name[1] = filepath + "\\" + image_foldername[1] + "\\" + curr_im[1] + ".jpg";
                image_name[2] = filepath + "\\" + image_foldername[2] + "\\" + curr_im[2] + ".jpg";

                this.Invoke(new Action<String, String, string>(SlideShow_Image), image_name[0], image_name[1], image_name[2]);

                curr_im[0]++;
                curr_im[1]++;
                curr_im[2]++;

                if (curr_im[0] > left_id[curr_index + 1] || curr_im[1] > cent_id[curr_index + 1] || curr_im[2] > rght_id[curr_index + 1])
                {
                    strt_index++;

                    if (strt_index >= last_index)
                    {
                        isVideoPlaying = false;
                        this.Invoke(new Action<int>(updateTrackbar), strt_index);
                        //Console.WriteLine("Last:" + curr_im[0] + " " + curr_im[1] + " " + curr_im[2]);

                    }
                    else
                    {
                        curr_index++;
                        curr_im[0] = left_id[curr_index];
                        curr_im[1] = cent_id[curr_index];
                        curr_im[2] = rght_id[curr_index];
                        this.Invoke(new Action<int>(updateTrackbar), strt_index);

                        //Console.WriteLine("Index:" + curr_index + " " + curr_im[0] + " " + curr_im[1] + " " + curr_im[2]);
                    }
                }

                Thread.Sleep(fps);
            }

            this.Invoke(new Action<Boolean>(toggleControls), true);
            btnPlay.Image = new Bitmap(Resources.play, new Size((int)(48.0 * screenRatio), (int)(48.0 * screenRatio)));
            btnProjector.Image = new Bitmap(Resources.show, new Size((int)(48.0 * screenRatio), (int)(48.0 * screenRatio)));
        }

        public void SlideShow_Image(String left, String cent, String rght)
        {
            if (File.Exists(left))
                imageLeft.LoadImage(left);

            if (File.Exists(cent))
                imageCent.LoadImage(cent);

            if (File.Exists(rght))
                imageRght.LoadImage(rght);
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            toggleLock(isInfoLocked);
        }

        public void toggleLock(Boolean b)
        {
            if (b)
            {
                isInfoLocked = false;
                //btnLock.Image = Resources.unlocked;
                btnLock.Image = new Bitmap(Resources.unlocked, new Size((int)(48.0 * screenRatio), (int)(48.0 * screenRatio)));
            }
            else
            {
                isInfoLocked = true;
                //btnLock.Image = Resources.locked;
                btnLock.Image = new Bitmap(Resources.locked, new Size((int)(48.0 * screenRatio), (int)(48.0 * screenRatio)));
            }
        }

        public void infoCopy(int index)
        {
            using (SqlConnection con = new SqlConnection(conStrng))
            {
                using (SqlCommand com = con.CreateCommand())
                {
                    //String sz = "UPDATE Vida SET [Carriageway_label]=@carriageway_label, [Upgrade_cost]=@upgrade_cost, [Motorcycle_observed_flow]=@motorcycle_observed_flow, [Bicycle_observed_flow]=@bicycle_observed_flow WHERE Id = 2;";
                    String s = @"UPDATE n SET 
                                    n.[Carriageway_label] = p.[Carriageway_label], 
                                    n.[Upgrade_cost] = p.[Upgrade_cost], 
                                    n.[Motorcycle_observed_flow] = p.[Motorcycle_observed_flow],
                                    n.[Bicycle_observed_flow] = p.[Bicycle_observed_flow],
                                    n.[Pedestrian_observed_flow_across_the_road] = p.[Pedestrian_observed_flow_across_the_road],
                                    n.[Pedestrian_observed_flow_along_the_road_driver_side] = p.[Pedestrian_observed_flow_along_the_road_driver_side],
                                    n.[Pedestrian_observed_flow_along_the_road_passenger_side] = p.[Pedestrian_observed_flow_along_the_road_passenger_side],
                                    n.[Land_use_driver_side] = p.[Land_use_driver_side],
                                    n.[Land_use_passenger_side] = p.[Land_use_passenger_side],
                                    n.[Area_type] = p.[Area_type],
                                    n.[Speed_limit]  = p.[Speed_limit],
                                    n.[Motorcycle_speed_limit] = p.[Motorcycle_speed_limit],
                                    n.[Truck_speed_limit] = p.[Truck_speed_limit],
                                    n.[Differential_speed_limits] = p.[Differential_speed_limits],
                                    n.[Median_type] = p.[Median_type],
                                    n.[Centreline_rumble_strips] = p.[Centreline_rumble_strips],
                                    n.[Roadside_severity_driver_side_distance] = p.[Roadside_severity_driver_side_distance],
                                    n.[Roadside_severity_driver_side_object] = p.[Roadside_severity_driver_side_object],
                                    n.[Roadside_severity_passenger_side_distance] = p.[Roadside_severity_passenger_side_distance],
                                    n.[Roadside_severity_passenger_side_object] = p.[Roadside_severity_passenger_side_object],
                                    n.[Shoulder_rumble_strips] = p.[Shoulder_rumble_strips],
                                    n.[Paved_shoulder_driver_side] = p.[Paved_shoulder_driver_side],
                                    n.[Paved_shoulder_passenger_side] = p.[Paved_shoulder_passenger_side],
                                    n.[Intersection_type] = p.[Intersection_type],
                                    n.[Intersection_channelisation] = p.[Intersection_channelisation],    
                                    n.[Intersecting_road_volume] = p.[Intersecting_road_volume],
                                    n.[Intersection_quality] = p.[Intersection_quality],
                                    n.[Property_access_points] = p.[Property_access_points],
                                    n.[Number_of_lanes] = p.[Number_of_lanes],
                                    n.[Lane_width] = p.[Lane_width],
                                    n.[Curvature] = p.[Curvature],
                                    n.[Quality_of_curve] = p.[Quality_of_curve],
                                    n.[Grade] = p.[Grade],
                                    n.[Road_condition] = p.[Road_condition],
                                    n.[Skid_resistance] = p.[Skid_resistance],
                                    n.[Delineation] = p.[Delineation],
                                    n.[Street_lighting] = p.[Street_lighting],
                                    n.[Pedestrian_crossing_facilities_inspected_road] = p.[Pedestrian_crossing_facilities_inspected_road],
                                    n.[Pedestrian_crossing_quality] = p.[Pedestrian_crossing_quality],
                                    n.[Pedestrian_crossing_facilities_intersecting_road] = p.[Pedestrian_crossing_facilities_intersecting_road],
                                    n.[Pedestrian_fencing] = p.[Pedestrian_fencing],
                                    n.[Speed_management_traffic_calming] = p.[Speed_management_traffic_calming],
                                    n.[Vehicle_parking] = p.[Vehicle_parking],
                                    n.[Sidewalk_driver_side] = p.[Sidewalk_driver_side],
                                    n.[Sidewalk_passenger_side] = p.[Sidewalk_passenger_side],
                                    n.[Service_road] = p.[Service_road],
                                    n.[Facilities_for_motorised_two_wheelers] = p.[Facilities_for_motorised_two_wheelers],
                                    n.[Facilities_for_bicycles] = p.[Facilities_for_bicycles],     
                                    n.[Sight_distance] = p.[Sight_distance],
                                    n.[Vehicle_flow_AADT] = p.[Vehicle_flow_AADT],
                                    n.[Motorcycle_%] = p.[Motorcycle_%],
                                    n.[Pedestrian_peak_hour_flow_across_the_road] = p.[Pedestrian_peak_hour_flow_across_the_road],
                                    n.[Pedestrian_peak_hour_flow_along_the_road_driver_side] = p.[Pedestrian_peak_hour_flow_along_the_road_driver_side],
                                    n.[Pedestrian_peak_hour_flow_along_the_road_passenger_side] = p.[Pedestrian_peak_hour_flow_along_the_road_passenger_side],
                                    n.[Bicycle_peak_hour_flow] = p.[Bicycle_peak_hour_flow],
                                    n.[Operating_speed_85th_percentile] = p.[Operating_speed_85th_percentile],
                                    n.[Operating_speed_mean] = p.[Operating_speed_mean],
                                    n.[Roads_that_cars_can_read] = p.[Roads_that_cars_can_read],
                                    n.[School_zone_warning]  = p.[School_zone_warning],                  
                                    n.[School_zone_crossing_supervisor] = p.[School_zone_crossing_supervisor]
                                  FROM [Vida] n inner join [Vida] p ON n.Id = @Id_Dest AND p.Id = @Id_Orig";

                    com.CommandText = s;

                    com.Parameters.AddWithValue("@Id_Dest", index);
                    com.Parameters.AddWithValue("@Id_Orig", index - 1);

                    con.Open();
                    com.ExecuteNonQuery();


                    //Debug.WriteLine(com.CommandText + " Value=" + e.ChangedItem.Value + " Index=" + _main.selectedID);
                }
            }
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            shouldUpdateTrackbar = true;
            //Debug.WriteLine("MouseDn" + trackBar.Value);
            trackBar_ValueChanged(sender, e);
        }

        private void trackBar_MouseDown(object sender, MouseEventArgs e)
        {
            //Debug.WriteLine("MouseUp" + trackBar.Value);
            shouldUpdateTrackbar = false;
        }

        // [SOO 20200831] Socket Server for 360
        private void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I can not seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            clientSocket = socket;
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("{0}", socket.RemoteEndPoint + " connected...");
            /*foreach (object obj in clientSockets)
            {
                Socket socket2 = (Socket)obj;
                string message = socket2.RemoteEndPoint.ToString();
                byte[] data = Encoding.ASCII.GetBytes(message);
                socket2.Send(data);
            }*/
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                clientSocket = null;
                return;
            }
        }
    }
}