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
    public partial class ImageLeft : Form
    {

        private readonly Main _main;
        private Boolean isMoving = false;

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
