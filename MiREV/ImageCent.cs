using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MiREV.Properties;

namespace MiREV
{
    public partial class ImageCent : Form
    {
        private System.Drawing.Drawing2D.GraphicsPath mousePath;
        private System.Drawing.Drawing2D.GraphicsPath homoPlane;
        private System.Drawing.Pen redPen;
        private System.Drawing.Pen cynPen;

        public Point startPoint = new Point();
        public Point endedPoint = new Point();

        private Boolean isZoomOn = false;

        private Boolean startDrawing = false;
        private Boolean enableHomography = true;

        private readonly Main _main;
        private FormWindowState LastWindowState = FormWindowState.Normal;
        private Point location_prev = new Point();
        private Boolean modifiedKey = false;

        private double road_width = 0;
        private double object_height = 0;
        private Boolean measureWidth = true;

        public ImageCent(Main main)
        {
            InitializeComponent();
            _main = main;
            
            this.Location = new Point((int)(Properties.Settings.Default.ImgCentLocation.X * _main.screenRatio), (int)(Properties.Settings.Default.ImgCentLocation.Y * _main.screenRatio));
            this.Size = new Size((int)(Properties.Settings.Default.ImgCentSize.Width * _main.screenRatio), (int)(Properties.Settings.Default.ImgCentSize.Height * _main.screenRatio));
            
            btnSave.Size = new Size((int)(btnSave.Size.Width * _main.screenRatio), (int)(btnSave.Size.Height * _main.screenRatio));
            btnSave.Location = new Point((int)(btnSave.Location.X * _main.screenRatio), (int)(btnSave.Location.Y * _main.screenRatio));
            btnSave.Image = new Bitmap(Resources.save_w, new Size((int)(24.0 * _main.screenRatio), (int)(24.0 * _main.screenRatio)));
           
            mousePath = new System.Drawing.Drawing2D.GraphicsPath();
            homoPlane = new System.Drawing.Drawing2D.GraphicsPath();

            redPen = new System.Drawing.Pen(Color.Red, (float)(2.0 * _main.screenRatio));
            cynPen = new System.Drawing.Pen(Color.Cyan, (float)(2.0 * _main.screenRatio));

            lblDistance.Parent = pictureBox;
            //lblDistance.Font = new Font("Microsoft Sans Serif", (float)(14.0 * _main.screenRatio));
            lblDistance.Font = new Font("Microsoft Sans Serif", (float)(8 + 6 * _main.screenRatio));
        }


        private void ImageCent_Load(object sender, EventArgs e)
        {
            location_prev = Location;
        }

        public void LoadImage(String s)
        {            
            //try
            //{
                if (pictureBox.Image != null)
                    pictureBox.Image.Dispose();

                //pictureBox.Image = new Bitmap(s);
                pictureBox.Image = Image.FromFile(s);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("Cent Image:" + s + ". Error:" + ex.Message);
            //}

            

            //Debug.WriteLine(pictureBox.Image.Size.Width + " " + pictureBox.Image.Size.Height + " " + pictureBox.Size);

        }

        private void ImageCent_ResizeBegin(object sender, EventArgs e)
        {
            showMeasurement(false);
            showHomography(false);
        }

        private void ImageCent_ResizeEnd(object sender, EventArgs e)
        {
            showMeasurement(true);
            showHomography(true);

            _main.resizeImagePanel(this.Width, this.Height);
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawPath(cynPen, homoPlane);
            e.Graphics.DrawPath(redPen, mousePath);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (startDrawing)
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    mousePath.Reset();
                    mousePath.AddLine(startPoint.X, startPoint.Y, startPoint.X, e.Y);
                    measureWidth = false;
                    //lblDistance.Text = roadWidth(rescalePoint(startPoint), rescalePoint(new Point(e.X, e.Y))).ToString("#0.00") + "m";
                    //lblDistance.Location = new Point(e.X + 5, e.Y - 14);

                    pictureBox.Invalidate();

                }
                else
                {
                    mousePath.Reset();
                    mousePath.AddLine(startPoint.X, startPoint.Y, e.X, startPoint.Y);
                    measureWidth = true;
                    //lblDistance.Text = roadWidth(rescalePoint(startPoint), rescalePoint(new Point(e.X, startPoint.Y))).ToString("#0.00") + "m";
                    //lblDistance.Location = new Point(e.X + 5, startPoint.Y - 14);

                    pictureBox.Invalidate();
                }

            }

            if (isZoomOn)
            {
                 _main.activatePbZoom(e.X, e.Y, pictureBox.Image);
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_main.isVideoPlaying)
            {
                if (e.Button == MouseButtons.Right)
                {
                    enableHomography = !enableHomography;
                    showHomography(enableHomography);
                }

                if (e.Button == MouseButtons.Left)
                {
                    if (startDrawing)
                    {
                        startDrawing = false;
                        pictureBox.Cursor = Cursors.Default;

                        endedPoint = new Point((int)mousePath.PathPoints[1].X, (int)mousePath.PathPoints[1].Y);

                        lblDistance.Visible = true;
                        lblDistance.Location = new Point(endedPoint.X + 5, endedPoint.Y - lblDistance.Height / 2);

                        startPoint = rescalePoint(startPoint);
                        endedPoint = rescalePoint(endedPoint);

                        if (measureWidth)
                        {
                            road_width = roadWidth(startPoint, endedPoint);

                            lblDistance.Text = road_width.ToString("#0.00") + "m";

                            btnSave.Visible = true;
                            btnSave.Height = lblDistance.Height;
                            btnSave.Width = lblDistance.Height;
                            btnSave.Location = new Point(lblDistance.Location.X + lblDistance.Width, lblDistance.Location.Y);

                            
                        }
                        else
                        {
                            object_height = objectHeight(startPoint, endedPoint);
                            lblDistance.Text = object_height.ToString("#0.00") + "m";

                            Debug.WriteLine(objectHeight(new Point(711, 609), new Point(721, 363)));
                        }
                        
                    }
                    else
                    {
                        if (_main.enableHomography)
                        {
                            lblDistance.Visible = true;
                            startPoint = new Point(e.X, e.Y);
                            mousePath.StartFigure();

                            pictureBox.Cursor = Cursors.Cross;
                            lblDistance.Visible = false;
                            btnSave.Visible = false;
                            startDrawing = true;
                        }
                        else
                        {
                            lblDistance.Text = "Could not open or load calibration files.";
                            lblDistance.Visible = true;
                            lblDistance.Location = e.Location;

                            btnSave.Visible = true;
                            btnSave.Location = new Point(lblDistance.Location.X + lblDistance.Width, lblDistance.Location.Y);
                        }
                    }
                }

                if (e.Button == MouseButtons.Middle)
                {
                    if (!isZoomOn)
                    {
                        _main.activatePbZoom(e.X, e.Y, pictureBox.Image);
                        isZoomOn = !isZoomOn;
                    }
                    else
                    {
                        _main.deactivatePbZoom();
                        isZoomOn = !isZoomOn;
                    }
                }
            }
        }

        public void showMeasurement(Boolean hiding)
        {
            if (hiding)
            {
                if (!startPoint.IsEmpty && !endedPoint.IsEmpty)
                {
                    mousePath.Reset();
                    mousePath.AddLine(scaledPoint(startPoint), scaledPoint(endedPoint));
                    lblDistance.Visible = true;
                    lblDistance.Location = scaledPoint(endedPoint);

                    btnSave.Visible = true;
                    btnSave.Location = new Point(lblDistance.Location.X + lblDistance.Width, lblDistance.Location.Y);

                }
            }
            else
            {
                mousePath.Reset();
                lblDistance.Visible = false;
                btnSave.Visible = false;
                startDrawing = false;
            }
        }

        public void showHomography(Boolean showing)
        {
            if (showing)
            {
                homoPlane.Reset();
                homoPlane.StartFigure();

                homoPlane.AddLine(scaledPoint(_main.hPlane[0]), scaledPoint(_main.hPlane[1]));
                homoPlane.AddLine(scaledPoint(_main.hPlane[1]), scaledPoint(_main.hPlane[2]));
                homoPlane.AddLine(scaledPoint(_main.hPlane[2]), scaledPoint(_main.hPlane[3]));
                homoPlane.AddLine(scaledPoint(_main.hPlane[3]), scaledPoint(_main.hPlane[0]));

                pictureBox.Invalidate();
            }
            else
            {
                homoPlane.Reset();
                pictureBox.Invalidate();
            }
        }

        private Point scaledPoint(Point p)
        {
            Point scaled_p = new Point();

            // image and container dimensions
            int w_i = pictureBox.Image.Width;
            int h_i = pictureBox.Image.Height;
            int w_c = pictureBox.Width;
            int h_c = pictureBox.Height;

            float imageRatio = w_i / (float)h_i; // image W:H ratio
            float contrRatio = w_c / (float)h_c; // container W:H ratio
            float scaleFactor = 0;
            float scaleDimens = 0;
            float imageFiller = 0;


            if (imageRatio >= contrRatio)
            {
                // horizontal image
                scaleFactor = w_i / (float)w_c;
                scaleDimens = h_i / scaleFactor;

                imageFiller = Math.Abs(h_c - scaleDimens) / 2;
                scaled_p.X = (int)(p.X / scaleFactor);
                scaled_p.Y = (int)((p.Y / scaleFactor) + imageFiller);
            }
            else
            {
                // vertical image
                scaleFactor = h_i / (float)h_c;
                scaleDimens = w_i / scaleFactor;

                imageFiller = Math.Abs(w_c - scaleDimens) / 2;
                scaled_p.X = (int)((p.X / scaleFactor) + imageFiller);
                scaled_p.Y = (int)(p.Y / scaleFactor);
            }
            return scaled_p;
        }


        private Point rescalePoint(Point p)
        {
            Point unscaled_p = new Point();

            // image and container dimensions
            int w_i = pictureBox.Image.Width;
            int h_i = pictureBox.Image.Height;
            int w_c = pictureBox.Width;
            int h_c = pictureBox.Height;

            float imageRatio = w_i / (float)h_i; // image W:H ratio
            float contrRatio = w_c / (float)h_c; // container W:H ratio
            float scaleFactor = 0;
            float scaleDimens = 0;
            float imageFiller = 0;

            if (imageRatio >= contrRatio)
            {
                // horizontal image
                scaleFactor = w_c / (float)w_i;
                scaleDimens = h_i * scaleFactor;

                imageFiller = Math.Abs(h_c - scaleDimens) / 2;
                unscaled_p.X = (int)(p.X / scaleFactor);
                unscaled_p.Y = (int)((p.Y - imageFiller) / scaleFactor);
            }
            else
            {
                // vertical image
                scaleFactor = h_c / (float)h_i;
                scaleDimens = w_i * scaleFactor;

                imageFiller = Math.Abs(w_c - scaleDimens) / 2;
                unscaled_p.X = (int)((p.X - imageFiller) / scaleFactor);
                unscaled_p.Y = (int)(p.Y / scaleFactor);
            }

            return unscaled_p;
        }


        private double roadWidth(Point s, Point e)
        {
            //double[,] H = { { -0.002194170, -0.002194170, 3.172770612 }, { 0.0002391322, -0.023784458, 21.32349326 }, { 0.0000368632, -0.001587104, 1 } };

            double[,] point1 = { { s.X }, { s.Y }, { 1 } };
            double[,] location1 = { { 0 }, { 0 }, { 0 } };

            location1[0, 0] = ((_main.hValue[0, 0] * point1[0, 0]) + (_main.hValue[0, 1] * point1[1, 0]) + (_main.hValue[0, 2] * point1[2, 0]));
            location1[1, 0] = ((_main.hValue[1, 0] * point1[0, 0]) + (_main.hValue[1, 1] * point1[1, 0]) + (_main.hValue[1, 2] * point1[2, 0]));
            location1[2, 0] = ((_main.hValue[2, 0] * point1[0, 0]) + (_main.hValue[2, 1] * point1[1, 0]) + (_main.hValue[2, 2] * point1[2, 0]));

            location1[0, 0] = location1[0, 0] / location1[2, 0];
            location1[1, 0] = location1[1, 0] / location1[2, 0];
            location1[2, 0] = location1[2, 0] / location1[2, 0]; ;

            double[,] point2 = { { e.X }, { e.Y }, { 1 } };
            double[,] location2 = { { 0 }, { 0 }, { 0 } };

            location2[0, 0] = ((_main.hValue[0, 0] * point2[0, 0]) + (_main.hValue[0, 1] * point2[1, 0]) + (_main.hValue[0, 2] * point2[2, 0]));
            location2[1, 0] = ((_main.hValue[1, 0] * point2[0, 0]) + (_main.hValue[1, 1] * point2[1, 0]) + (_main.hValue[1, 2] * point2[2, 0]));
            location2[2, 0] = ((_main.hValue[2, 0] * point2[0, 0]) + (_main.hValue[2, 1] * point2[1, 0]) + (_main.hValue[2, 2] * point2[2, 0]));

            location2[0, 0] = location2[0, 0] / location2[2, 0];
            location2[1, 0] = location2[1, 0] / location2[2, 0];
            location2[2, 0] = location2[2, 0] / location2[2, 0];

            return (double)Math.Sqrt((double)Math.Pow((double)location1[0, 0] - (double)location2[0, 0], 2) + ((double)Math.Pow((double)location1[1, 0] - (double)location2[1, 0], 2)));
        }

        private double objectHeight(Point s, Point e)
        {
            double l71 = ((_main.zValue[1] * 1) - (s.Y * 1)); double l72 = -((_main.zValue[0] * 1) - (s.X * 1)); double l73 = ((_main.zValue[0] * s.Y) - (s.X * _main.zValue[1]));

            double u11 = ((l72 * _main.zValue[4]) - (_main.zValue[3] * l73)); double u12 = -((l71 * _main.zValue[4]) - (_main.zValue[2] * l73)); double u13 = ((l71 * _main.zValue[3]) - (_main.zValue[2] * l72));
            u11 = u11 / u13; u12 = u12 / u13; u13 = u13 / u13;

            double t1u11 = ((e.Y * u13) - (u12 * 1)); double t1u12 = -((e.X * u13) - (u11 * 1)); double t1u13 = ((e.X * u12) - (u11 * e.Y));

            double vb21 = ((_main.zValue[6] * 1) - (_main.zValue[1] * _main.zValue[7])); double vb22 = -((_main.zValue[5] * 1) - (_main.zValue[0] * _main.zValue[7])); double vb23 = ((_main.zValue[5] * _main.zValue[1]) - (_main.zValue[0] * _main.zValue[6]));

            double tp1 = ((t1u12 * vb23) - (vb22 * t1u13)); double tp2 = -((t1u11 * vb23) - (vb21 * t1u13)); double tp3 = ((t1u11 * vb22) - (vb21 * t1u12));
            tp1 = tp1 / tp3; tp2 = tp2 / tp3; tp3 = tp3 / tp3;

            double pp11 = _main.zValue[5] - _main.zValue[9]; double pp12 = _main.zValue[6] - _main.zValue[10]; double pp13 = _main.zValue[7] - _main.zValue[11];
            double pp21 = _main.zValue[0] - tp1; double pp22 = _main.zValue[1] - tp2; double pp23 = 1 - tp3;
            double pp31 = _main.zValue[5] - tp1; double pp32 = _main.zValue[6] - tp2; double pp33 = _main.zValue[7] - tp3;
            double pp41 = _main.zValue[0] - _main.zValue[9]; double pp42 = _main.zValue[1] - _main.zValue[10]; double pp43 = 1 - _main.zValue[11];

            return ((((pp11 * pp21) + (pp12 * pp22) + (pp13 * pp23)) / ((pp31 * pp41) + (pp32 * pp42) + (pp33 * pp43))) * _main.zValue[8]);
         }

        private void ImageCent_Resize(object sender, EventArgs e)
        {
            // When window state changes
            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;

                if (WindowState == FormWindowState.Maximized)
                {
                    if (enableHomography)
                        showHomography(true);

                    showMeasurement(true);
                }

                if (WindowState == FormWindowState.Normal)
                {
                    if (enableHomography)
                        showHomography(true);

                    showMeasurement(true);
                }
            }
        }

        private void ImageCent_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("Cent closing");
            Point location = Location;
            Size size = Size;

            if (WindowState != FormWindowState.Normal)
            {
                Debug.WriteLine("Not Normal");
                location = RestoreBounds.Location;
                size = RestoreBounds.Size;
            }

            Properties.Settings.Default.ImgCentLocation = location;
            Properties.Settings.Default.ImgCentSize = size;
            Properties.Settings.Default.Save();
        }

        private void ImageCent_Move(object sender, EventArgs e)
        {
            if (!modifiedKey)
            {
                Point pMoved = new Point(Location.X - location_prev.X, Location.Y - location_prev.Y);
                _main.moveImagePanel(pMoved);
            }

            location_prev = Location;
        }

        private void ImageCent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                modifiedKey = true;
            }
        }

        private void ImageCent_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                modifiedKey = false;
            }

        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void ImageCent_Deactivate(object sender, EventArgs e)
        {
            if (startDrawing)
            {
                mousePath.Reset();
                lblDistance.Visible = false;
                btnSave.Visible = false;

                startDrawing = false;

                pictureBox.Invalidate();
            }

            if (isZoomOn)
            {
                _main.deactivatePbZoom();
                isZoomOn = !isZoomOn;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (measureWidth)
            {
                if (road_width >= 3.25)
                {
                    _main.updateLaneWidth("1 - Wide (>= 3.25m)");
                }
                else
                {
                    if (road_width >= 2.75)
                    {
                        _main.updateLaneWidth("2 - Medium (>= 2.75m to < 3.25m)");
                    }
                    else
                    {
                        _main.updateLaneWidth("3 - Narrow (>= 0m to < 2.75m)");
                    }
                }
            }            
        }
    }
}
