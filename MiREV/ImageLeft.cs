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
using Emgu.CV;

using Emgu.CV.Util;

namespace MiREV
{
    public partial class ImageLeft : Form
    {

        private readonly Main _main;
        private Boolean isMoving = false;
        private double[,] LeftPoint = new double[2, 2];
        private double[,] RightPoint = new double[2, 2];
        List<VectorOfPointF> LeftPoints = new List<VectorOfPointF>(2) { };
        List<VectorOfPointF> RightPoints = new List<VectorOfPointF>(2) { };
        private Boolean isZoomLeftOn = false;

        private System.Drawing.Drawing2D.GraphicsPath speedPath;
        private System.Drawing.Drawing2D.GraphicsPath distancePath;

        private System.Drawing.Drawing2D.GraphicsPath vehiclePath;
        private System.Drawing.Drawing2D.GraphicsPath mousePath;
        private System.Drawing.Drawing2D.GraphicsPath homoPlane;
        private System.Drawing.Pen redPen;
        private System.Drawing.Pen cynPen;
        public Point startPoint = new Point();
        public Point endedPoint = new Point();

        private Boolean startDrawing = false;
        private Boolean enableDrawing = false;

        private Boolean enableHomography = true;

        private float pref = 0;
        private float rref = 0;

        //Font font = new Font()

        public ImageLeft(Main main)
        {
            InitializeComponent();
            _main = main;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            speedPath = new System.Drawing.Drawing2D.GraphicsPath();
            distancePath = new System.Drawing.Drawing2D.GraphicsPath();

            vehiclePath = new System.Drawing.Drawing2D.GraphicsPath();

            mousePath = new System.Drawing.Drawing2D.GraphicsPath();
            homoPlane = new System.Drawing.Drawing2D.GraphicsPath();

            redPen = new System.Drawing.Pen(Color.Red, (float)(2.0 * _main.screenRatio));
            cynPen = new System.Drawing.Pen(Color.Cyan, (float)(2.0 * _main.screenRatio));
        }

        public void LoadImage(String s)
        {

            //Debug.WriteLine("L Image Loaded" + s);
            //try
            //{
                if (pictureBox.Image != null)
                    pictureBox.Image.Dispose();

                //pictureBox.Image = new Bitmap(s);
                pictureBox.Image = Image.FromFile(s);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("Left Image:" + s + ". Error:" + ex.Message);
            //}
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(0xFF, 0x00, 0x00)), speedPath);
            e.Graphics.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(0xFF, 0x00, 0x00)), distancePath);

            e.Graphics.DrawPath(redPen, vehiclePath);
            e.Graphics.DrawPath(redPen, mousePath);
        }

        public void DrawRect(int[] data)
        {
            Debug.WriteLine(data);

            Point min = scaledPoint(new Point(data[0], data[1]));
            Point max = scaledPoint(new Point(data[2], data[3]));

            Debug.WriteLine(min);
            Debug.WriteLine(max);

            Rectangle rect = new Rectangle(min.X, min.Y, max.X - min.X, max.Y - min.Y);

            vehiclePath.Reset();
            vehiclePath.AddRectangle(rect);
            speedPath.AddString(data[4].ToString() + " kmh", System.Drawing.FontFamily.GenericSansSerif, 0, 18, new Point(min.X, min.Y - 18), StringFormat.GenericDefault);
            
            pictureBox.Invalidate();
        }

        public void ClearRect()
        {
            speedPath.Reset();
            distancePath.Reset();

            vehiclePath.Reset();
            pictureBox.Invalidate();
        }

        private void ImageLeft_Load(object sender, EventArgs e)
        {
            
            //Location = Properties.Settings.Default.ImgLeftLocation;
            this.Location = new Point((int)(Properties.Settings.Default.ImgLeftLocation.X * _main.screenRatio), (int)(Properties.Settings.Default.ImgLeftLocation.Y * _main.screenRatio));
            this.Size = new Size((int)(Properties.Settings.Default.ImgLeftSize.Width * _main.screenRatio), (int)(Properties.Settings.Default.ImgLeftSize.Height * _main.screenRatio));
        }

        private void ImageLeft_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
            isMoving = false;

            //pictureBox.Width;
        }

        private void ImageLeft_ResizeBegin(object sender, EventArgs e)
        {
            isMoving = true;
            ClearRect();
        }

        private void ImageLeft_Move(object sender, EventArgs e)
        {
            if (isMoving)
                this.Opacity = 0.7;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_main.isVideoPlaying)
            {
                //if (e.Button == MouseButtons.Right)
                //{
                //    enableHomography = !enableHomography;
                //    showHomography(enableHomography);
                //}

                if (e.Button == MouseButtons.Left && enableDrawing)
                {
                    if (startDrawing)
                    {
                        enableDrawing = false;
                        startDrawing = false;
                        pictureBox.Cursor = Cursors.Default;

                        var pixCoords = Box2PixelCoords(e.X, e.Y); //Convert pictureBox coordinates to pixel coordinates
                        var X = pixCoords.Item1;
                        var Y = pixCoords.Item2;

                        endedPoint = new Point((int)mousePath.PathPoints[1].X, (int)mousePath.PathPoints[1].Y);

                        Debug.WriteLine(pixCoords);
                        Debug.WriteLine(Y.ToString(), X.ToString());
                        Debug.WriteLine(endedPoint);
                        Debug.WriteLine(rescalePoint(endedPoint));

                                             

                        //lblDistance.Visible = true;
                        //lblDistance.Location = new Point(endedPoint.X + 5, endedPoint.Y - lblDistance.Height / 2);

                        //startPoint = rescalePoint(startPoint);
                        //endedPoint = rescalePoint(endedPoint);

                        var distance = _main.AddLeftPoints(rescalePoint(startPoint), rescalePoint(endedPoint));

                        //distancePath.AddString(distance.ToString(), System.Drawing.FontFamily.GenericSansSerif, 0, 18, new Point(endedPoint.X, endedPoint.Y - 18), StringFormat.GenericDefault);
                        //pictureBox.Invalidate();

                        /*if (measureWidth)
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
                        }*/

                    }
                    else
                    {
                        if (_main.enableHomography)
                        {
                            //lblDistance.Visible = true;
                            startPoint = new Point(e.X, e.Y);
                            mousePath.StartFigure();

                            pictureBox.Cursor = Cursors.Cross;
                            //lblDistance.Visible = false;
                            //btnSave.Visible = false;
                            startDrawing = true;

                            distancePath.Reset();
                            pictureBox.Invalidate();
                        }
                        else
                        {
                            //lblDistance.Text = "Could not open or load calibration files.";
                            //lblDistance.Visible = true;
                            //lblDistance.Location = e.Location;

                            //btnSave.Visible = true;
                            //btnSave.Location = new Point(lblDistance.Location.X + lblDistance.Width, lblDistance.Location.Y);
                        }
                    }
                }

                if (e.Button == MouseButtons.Middle)
                {
                    if (!isZoomLeftOn)
                    {
                        _main.activatePbZoom(e.X, e.Y, pictureBox.Image);
                        isZoomLeftOn = !isZoomLeftOn;
                    }
                    else
                    {
                        _main.deactivatePbZoom();
                        isZoomLeftOn = !isZoomLeftOn;
                    }
                }
            }
        }
        /*private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (!isZoomLeftOn)
                {
                    _main.activatePbZoom(e.X, e.Y, pictureBox.Image);
                    isZoomLeftOn = !isZoomLeftOn;
                }
                else
                {
                    _main.deactivatePbZoom();
                    isZoomLeftOn = !isZoomLeftOn;
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                var pixCoords = Box2PixelCoords(e.X, e.Y); //Convert pictureBox coordinates to pixel coordinates
                var X = pixCoords.Item1;
                var Y = pixCoords.Item2;

                Debug.WriteLine(Y.ToString(),X.ToString());
               
                if (X< 1280) // from left image
                {
                    if (LeftPoints.Count == 2)
                    {
                        LeftPoints.Clear();
                    }
                   
                    var distorted_point = new VectorOfPointF(new[] { new PointF(X,Y) }); 
                    var undistorted_point = new VectorOfPointF(new[] { new PointF(-1, -1) });
                    //undistor the pionts
                    CvInvoke.UndistortPoints(distorted_point, undistorted_point, _main.cam_left_intr, _main.cam_left_dist, null, _main.P1);
                    
                    LeftPoints.Add(undistorted_point);
                    
                }
                if (X > 1280) // from right image
                {
                    if (RightPoints.Count == 2)
                    {
                        RightPoints.Clear();
                    }

                    var distorted_point = new VectorOfPointF(new[] { new PointF(X-1280, Y) });
                    var undistorted_point = new VectorOfPointF(new[] { new PointF(-1, -1) });
                    CvInvoke.UndistortPoints(distorted_point, undistorted_point, _main.cam_right_intr, _main.cam_right_dist, null, _main.P2);
                    RightPoints.Add(undistorted_point);
                }
                if(RightPoints.Count == 2 && LeftPoints.Count == 2)
                {
                    Matrix<float> Tt1 = new Matrix<float>(4, 1);
                    Matrix<float> Tt2 = new Matrix<float>(4, 1);
                    CvInvoke.TriangulatePoints(_main.P1, _main.P2, LeftPoints[0], RightPoints[0], Tt1);
                    CvInvoke.TriangulatePoints(_main.P1, _main.P2, LeftPoints[1], RightPoints[1], Tt2);
                    var Tt13D = Tt1.Mul(1 / Tt1[3, 0]); // Convert from homogeneous coordinates [X Y Z W] to Euclidean space [X Y Z 1]
                    var Tt23D = Tt2.Mul(1 / Tt2[3, 0]); // Convert from homogeneous coordinates [X Y Z W] to Euclidean space [X Y Z 1]
                    Console.WriteLine(Distance(Tt13D, Tt23D)/100); // Euclidean distance
                }

            }
        }*/

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (startDrawing)
            {
                //mousePath.Reset();
                //mousePath.AddLine(startPoint.X, startPoint.Y, e.X, e.Y);
                //pictureBox.Invalidate();

                if (Control.ModifierKeys == Keys.Control)
                {
                    mousePath.Reset();
                    mousePath.AddLine(startPoint.X, startPoint.Y, startPoint.X, e.Y);
                    //measureWidth = false;
                    //lblDistance.Text = roadWidth(rescalePoint(startPoint), rescalePoint(new Point(e.X, e.Y))).ToString("#0.00") + "m";
                    //lblDistance.Location = new Point(e.X + 5, e.Y - 14);

                    pictureBox.Invalidate();

                }
                else
                {
                    mousePath.Reset();
                    mousePath.AddLine(startPoint.X, startPoint.Y, e.X, startPoint.Y);
                    //measureWidth = true;
                    //lblDistance.Text = roadWidth(rescalePoint(startPoint), rescalePoint(new Point(e.X, startPoint.Y))).ToString("#0.00") + "m";
                    //lblDistance.Location = new Point(e.X + 5, startPoint.Y - 14);

                    pictureBox.Invalidate();
                }

            }

                if (isZoomLeftOn)
            {
                _main.activatePbZoom(e.X, e.Y, pictureBox.Image);
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

        public static double Distance(Matrix<float> T1, Matrix<float> T2) => Math.Sqrt(Math.Pow(T1[0, 0] - T2[0, 0], 2) + Math.Pow(T1[1, 0] - T2[1, 0], 2)
            + Math.Pow(T1[2, 0] - T2[2, 0], 2) + Math.Pow(T1[3, 0] - T2[3, 0], 2));

        //Function to convert pictureBox coordinates to pixel coordinates 
        private Tuple<Int32, Int32> Box2PixelCoords(Int32 mouseX, Int32 mouseY)
        {
            Int32 realW = pictureBox.Image.Width;
            Int32 realH = pictureBox.Image.Height;
            Int32 currentW = pictureBox.ClientRectangle.Width;
            Int32 currentH = pictureBox.ClientRectangle.Height;
            Double zoomW = (currentW / (Double)realW);
            Double zoomH = (currentH / (Double)realH);
            Double zoomActual = Math.Min(zoomW, zoomH);
            Double padX = zoomActual == zoomW ? 0 : (currentW - (zoomActual * realW)) / 2;
            Double padY = zoomActual == zoomH ? 0 : (currentH - (zoomActual * realH)) / 2;
            Int32 realX = (Int32)((mouseX - padX) / zoomActual);
            Int32 realY = (Int32)((mouseY - padY) / zoomActual);
            return Tuple.Create(realX,realY);
        }



        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (isZoomLeftOn)
            {
                _main.deactivatePbZoom();
                isZoomLeftOn = !isZoomLeftOn;
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

        private void btnMeasure_Click(object sender, EventArgs e)
        {
            if(enableDrawing)
            {
                enableDrawing = false;
                pictureBox.Cursor = Cursors.Default;
            } else
            {
                enableDrawing = true;
                pictureBox.Cursor = Cursors.Cross;
            }
            
        }

        public void animation(double acx1, double acx2, double acx3, double acy1, double acy2, double acy3)
        {
            rref = (float)(acx1 + acx2 + acx3) / 3;
            pref = (float)(acy1 + acy2 + acy3) / 3;

            if (pref >= 10.0)
            {
                _main.updateGrade("5 - >= 10%");
            }
            else
            {
                if (pref >= 7.5)
                {
                    _main.updateGrade("4 - >= 7.5% to <10%");
                }
                else
                {
                    _main.updateGrade("1 - >= 0% to <7.5%");
                }
            }

            //if (doneRotate)
            //    Invoke(new Action<float>(RotateImage), rref * (-1));
            //if (doneRotate2)
            //    Invoke(new Action<float>(RotateImage2), pref);

            txtRoll.Text = Math.Round(rref * (-1), 3).ToString();
            txtPitch.Text = Math.Round(pref, 3).ToString();
        }

    }
}
