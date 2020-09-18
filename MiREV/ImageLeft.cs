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


        public ImageLeft(Main main)
        {
            InitializeComponent();
            _main = main;
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
        }

        private void ImageLeft_ResizeBegin(object sender, EventArgs e)
        {
            isMoving = true;
        }

        private void ImageLeft_Move(object sender, EventArgs e)
        {
            if (isMoving)
                this.Opacity = 0.7;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
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


        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isZoomLeftOn)
            {
                _main.activatePbZoom(e.X, e.Y, pictureBox.Image);
            }
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (isZoomLeftOn)
            {
                _main.deactivatePbZoom();
                isZoomLeftOn = !isZoomLeftOn;
            }
        }
    }
}
