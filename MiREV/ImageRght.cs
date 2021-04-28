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

namespace MiREV
{
    public partial class ImageRght : Form
    {

        private readonly Main _main;
        private Boolean isMoving = false;

        private Boolean isZoomRightOn = false;
        private System.Drawing.Drawing2D.GraphicsPath distancePath;
        private System.Drawing.Drawing2D.GraphicsPath vehiclePath;
        private System.Drawing.Drawing2D.GraphicsPath mousePath;
        private System.Drawing.Drawing2D.GraphicsPath homoPlane;
        private System.Drawing.Pen redPen;
        private System.Drawing.Pen cynPen;
        public Point startPoint = new Point();
        public Point endedPoint = new Point();

        private Boolean startDrawing = false;

        public ImageRght(Main main)
        {
            InitializeComponent();
            _main = main;

            distancePath = new System.Drawing.Drawing2D.GraphicsPath();

            vehiclePath = new System.Drawing.Drawing2D.GraphicsPath();

            mousePath = new System.Drawing.Drawing2D.GraphicsPath();
            homoPlane = new System.Drawing.Drawing2D.GraphicsPath();

            redPen = new System.Drawing.Pen(Color.Red, (float)(2.0 * _main.screenRatio));
            cynPen = new System.Drawing.Pen(Color.Cyan, (float)(2.0 * _main.screenRatio));

        }


        public void ClearRect()
        {
            distancePath.Reset();

            vehiclePath.Reset();
            pictureBox.Invalidate();
        }

        public void LoadImage(String s)
        {
            //Debug.WriteLine("R Image Loaded" + s);

            //try
            //{            
                if (pictureBox.Image != null)
                    pictureBox.Image.Dispose();

                //pictureBox.Image = new Bitmap(s);
                pictureBox.Image = Image.FromFile(s);

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("Rght Image:" + s + ". Error:" + ex.Message);
            //}
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(0xFF, 0x00, 0x00)), distancePath);

            e.Graphics.DrawPath(redPen, vehiclePath);
            e.Graphics.DrawPath(redPen, mousePath);
        }

        private void ImageRght_Load(object sender, EventArgs e)
        {
            //Location = Properties.Settings.Default.ImgRghtLocation;
            this.Location = new Point((int)(Properties.Settings.Default.ImgRghtLocation.X * _main.screenRatio), (int)(Properties.Settings.Default.ImgRghtLocation.Y * _main.screenRatio));
            this.Size = new Size((int)(Properties.Settings.Default.ImgRghtSize.Width * _main.screenRatio), (int)(Properties.Settings.Default.ImgRghtSize.Height * _main.screenRatio));
            
        }

        private void ImageRght_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
            isMoving = false;
        }

        private void ImageRght_ResizeBegin(object sender, EventArgs e)
        {
            isMoving = true;
        }

        private void ImageRght_Move(object sender, EventArgs e)
        {
            if (isMoving)
                this.Opacity = 0.7;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (startDrawing)
                {
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


                    //endedPoint = new Point((int)mousePath.PathPoints[1].X, (int)mousePath.PathPoints[1].Y);

                    //lblDistance.Visible = true;
                    //lblDistance.Location = new Point(endedPoint.X + 5, endedPoint.Y - lblDistance.Height / 2);
                    
                    var distance = _main.AddRightPoints(rescalePoint(startPoint), rescalePoint(endedPoint));

                    distancePath.AddString(distance.ToString(), System.Drawing.FontFamily.GenericSansSerif, 0, 18, new Point(endedPoint.X, endedPoint.Y - 18), StringFormat.GenericDefault);
                    pictureBox.Invalidate();

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
                if (!isZoomRightOn)
                {
                    _main.activatePbZoom(e.X, e.Y, pictureBox.Image);
                    isZoomRightOn = !isZoomRightOn;
                }
                else
                {
                    _main.deactivatePbZoom();
                    isZoomRightOn = !isZoomRightOn;
                }
            }
        }

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

            if (isZoomRightOn)
            {
                _main.activatePbZoom(e.X, e.Y, pictureBox.Image);
            }
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (isZoomRightOn)
            {
                _main.deactivatePbZoom();
                isZoomRightOn = !isZoomRightOn;
            }
        }

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
            return Tuple.Create(realX, realY);
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
    }
}
