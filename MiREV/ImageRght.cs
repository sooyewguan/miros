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

        public ImageRght(Main main)
        {
            InitializeComponent();
            _main = main;

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
    }
}
