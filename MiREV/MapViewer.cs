using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;

using System.Diagnostics;

namespace MiREV
{

    public partial class MapViewer : Form
    {
        GMapOverlay markerOverlay = new GMapOverlay("markers");
        GMapOverlay routesOverlay  = new GMapOverlay("routes");
        GMarkerImage currentMarker = null;
        GMarkerImage previouMarker = null;
        private bool markerClicked = false;

        private readonly Main _main;

        public MapViewer(Main main)
        {
            InitializeComponent();
            _main = main;
                        
            this.Location = new Point((int)(Properties.Settings.Default.MapLocation.X * _main.screenRatio), (int)(Properties.Settings.Default.MapLocation.Y * _main.screenRatio));
            this.Size = new Size((int)(Properties.Settings.Default.MapSize.Width * _main.screenRatio), (int)(Properties.Settings.Default.MapSize.Height * _main.screenRatio));
        }

        public Boolean pbZoomProp
        {
            get { return pbZoom.Visible; }
            set { 
                pbZoom.Enabled = value;
                pbZoom.Visible = value;

                gMapControl.Enabled = !value;
                gMapControl.Visible = !value;
            }
        }
        public int pbZoomWidth
        {
            get { return gMapControl.Width; }
            set { gMapControl.Width = value;}
        }
        public int pbZoomHeight
        {
            get { return gMapControl.Height; }
            set { gMapControl.Height = value;}
        }

        public System.Drawing.Image pbZoomPic
        {
            get { return pbZoom.Image; }
            set { pbZoom.Image = value;}
        }

        private void MapViewer_Load(object sender, EventArgs e)
        {
            //Location = Properties.Settings.Default.MapLocation;
            //Size = Properties.Settings.Default.MapSize;

            gMapControl.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl.SetPositionByKeywords("Kuala Lumpur, Malaysia");
            gMapControl.ShowCenter = false;
        }

        private void MapViewer_OnMarkerEnter(GMapMarker item)
        {
            if (!markerClicked)
            {                
                currentMarker = (GMarkerImage)item;
            }
        }

        private void MapViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (currentMarker != null && currentMarker.IsMouseOver)
                {
                    markerClicked = true;

                    MarkerImageSwitch(currentMarker);
                    _main.updateTrackbar((int)currentMarker.Tag);

                    Debug.WriteLine("Marker " + currentMarker.Tag);
                }
            }
        }

        private void MapViewer_MouseUp(object sender, MouseEventArgs e)
        {
            currentMarker = null;
            markerClicked = false;
        }

        public void MarkerImageSwitch(GMarkerImage marker)
        {
            if (previouMarker != null)
            {
                previouMarker.markerChange(MiREV.Properties.Resources.marker, 0);
            }
            previouMarker = marker;
            marker.markerChange(MiREV.Properties.Resources.image, -15);

            gMapControl.Position = new PointLatLng(marker.Position.Lat, marker.Position.Lng);
        }

        public void MarkerImageSwitch(int n)
        {
            MarkerImageSwitch((GMarkerImage)markerOverlay.Markers[n]);
        }

        public void addMarker(int tag, double lat, double lng)
        {
            GMarkerImage marker = new GMarkerImage(new PointLatLng(lat, lng), MiREV.Properties.Resources.marker);
            marker.Tag = tag;
            markerOverlay.Markers.Add(marker);
        }

        public void addMarker(int tag, PointLatLng point, int markerType)
        {
            GMarkerImage marker; 
            switch (markerType) {
                case 0:
                    marker = new GMarkerImage(point, MiREV.Properties.Resources.marker);
                    marker.Tag = tag;
                    markerOverlay.Markers.Add(marker);
                    break;
                case 1:
                    marker = new GMarkerImage(point, MiREV.Properties.Resources.image);
                    marker.Tag = tag;
                    markerOverlay.Markers.Add(marker);
                    break;
            }

            marker = new GMarkerImage(point, MiREV.Properties.Resources.marker);
            marker.Tag = tag;
            markerOverlay.Markers.Add(marker);
        }

        public void setPosition(PointLatLng p)
        {
            gMapControl.Position = p;
        }

        public void addOverlay()
        {
            gMapControl.Overlays.Add(markerOverlay);
            gMapControl.Overlays.Add(routesOverlay);
            gMapControl.Zoom = 18;
        }

        public void remMarker()
        {
            markerOverlay.Markers.Clear();
        }

        public void remRoutes()
        {
            routesOverlay.Routes.Clear();
        }

        private void gMapControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'a':
                    _main.updateTrackbar(_main.trackBar.Value - _main.interval);
                    break;
                case 'A':
                    _main.updateTrackbar(_main.trackBar.Value - _main.interval);
                    break;
                case 's':
                    _main.updateTrackbar(_main.trackBar.Value + _main.interval);
                    break;
                case 'S':
                    _main.updateTrackbar(_main.trackBar.Value + _main.interval);
                    break;
                default:
                    break;
            }
        }

        private void gMapControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                _main.btnNext.Text = ">";
                _main.btnPrev.Text = "<";

                _main.interval = 1;
                _main.trackBar.SmallChange = _main.interval;
            }
        }

        private void gMapControl_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    Debug.WriteLine("Ctrl");
                    _main.btnNext.Text = ">>";
                    _main.btnPrev.Text = "<<";
                    _main.interval = 5;
                    _main.trackBar.SmallChange = _main.interval;

                    break;
                case Keys.Left:
                    if (!_main.trackBar.Focused)
                    {
                        Debug.WriteLine("Left");
                        _main.updateTrackbar(_main.trackBar.Value - _main.interval);
                    }
                    break;
                case Keys.Right:
                    if (!_main.trackBar.Focused)
                    {
                        Debug.WriteLine("Rght");
                        _main.updateTrackbar(_main.trackBar.Value + _main.interval);
                    }
                    break;
            }
        }

        private void gMapControl_Load(object sender, EventArgs e)
        {

        }

    }
}
