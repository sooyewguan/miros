using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiREV
{
    public partial class IncGauges : Form
    {
        private Boolean doneRotate = true;
        private Boolean doneRotate2 = true;
        private float pref, rref;
        //Image roll = Image.FromFile("C:\\Users\\MyREV\\Desktop\\MiREV\\MiREV\\Resources\\roll.png");
		//Image pitch = Image.FromFile("C:\\Users\\MyREV\\Desktop\\MiREV\\MiREV\\Resources\\pitch.png");
        Image roll = GetImageByName("cancel.png");
        Image pitch = GetImageByName("create.png");


        private readonly Main _main;

        public IncGauges(Main main)
        {
            InitializeComponent();
            _main = main;

        }

        public static Bitmap GetImageByName(string imageName)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);
            return (Bitmap)rm.GetObject(imageName);

        }

        private void Gauges_Load(object sender, EventArgs e)
        {
            this.Location = new Point((int)(Properties.Settings.Default.IncGaugLocation.X * _main.screenRatio), (int)(Properties.Settings.Default.IncGaugLocation.Y * _main.screenRatio));
            this.Size = new Size((int)(Properties.Settings.Default.IncGaugSize.Width * _main.screenRatio), (int)(Properties.Settings.Default.IncGaugSize.Height * _main.screenRatio));

            pbPitch.Size = new Size((int)(pbPitch.Size.Width * _main.screenRatio), (int)(pbPitch.Size.Height * _main.screenRatio));
            pbRoll.Size = new Size((int)(pbRoll.Size.Width * _main.screenRatio), (int)(pbRoll.Size.Height * _main.screenRatio));
            pbSensorSignal.Size = new Size((int)(pbSensorSignal.Size.Width * _main.screenRatio), (int)(pbSensorSignal.Size.Height * _main.screenRatio));
            pictureBox1.Size = new Size((int)(pictureBox1.Size.Width * _main.screenRatio), (int)(pictureBox1.Size.Height * _main.screenRatio));
            txtRoll.Size = new Size((int)(txtRoll.Size.Width * _main.screenRatio), (int)(txtRoll.Size.Height * _main.screenRatio));
            txtPitch.Size = new Size((int)(txtPitch.Size.Width * _main.screenRatio), (int)(txtPitch.Size.Height * _main.screenRatio));
            txtRoll.Location = new Point((int)(txtRoll.Location.X * _main.screenRatio), (int)(txtRoll.Location.Y * _main.screenRatio));
            txtPitch.Location = new Point((int)(txtPitch.Location.X * _main.screenRatio), (int)(txtPitch.Location.Y * _main.screenRatio));
            pbPitch.Location = new Point((int)(pbPitch.Location.X * _main.screenRatio), (int)(pbPitch.Location.Y * _main.screenRatio));
            pbRoll.Location = new Point((int)(pbRoll.Location.X * _main.screenRatio), (int)(pbRoll.Location.Y * _main.screenRatio));
            pbSensorSignal.Location = new Point((int)(pbSensorSignal.Location.X * _main.screenRatio), (int)(pbSensorSignal.Location.Y * _main.screenRatio));
            pictureBox1.Location = new Point((int)(pictureBox1.Location.X * _main.screenRatio), (int)(pictureBox1.Location.Y * _main.screenRatio));
            
            //pbPitch
            System.Drawing.Drawing2D.GraphicsPath gp3 = new System.Drawing.Drawing2D.GraphicsPath();
            gp3.AddEllipse(0, 0, pbPitch.Width, pbPitch.Height);
            System.Drawing.Region rg3 = new System.Drawing.Region(gp3);
            pbPitch.Region = rg3;
            //pbRoll button
            System.Drawing.Drawing2D.GraphicsPath gp5 = new System.Drawing.Drawing2D.GraphicsPath();
            gp5.AddEllipse(0, 0, pbRoll.Width, pbRoll.Height);
            System.Drawing.Region rg5 = new System.Drawing.Region(gp5);
            pbRoll.Region = rg5;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown
                || e.CloseReason == CloseReason.ApplicationExitCall
                || e.CloseReason == CloseReason.TaskManagerClosing)
            {
                return;
            }
            e.Cancel = true;
            //assuming you want the close-button to only hide the form, 
            //and are overriding the form's OnFormClosing method:
            this.WindowState = FormWindowState.Minimized;
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

            if (doneRotate)
				Invoke(new Action<float>(RotateImage), rref*(-1));
			if (doneRotate2)
			    Invoke(new Action<float>(RotateImage2), pref);
        }

        void RotateImage2(float rotationAngle)
        {
            try
            {
                doneRotate2 = false;
                /*int a = 250;
                Bitmap bmp = new Bitmap(a, a);
                Graphics gfx = Graphics.FromImage(bmp);
                gfx.TranslateTransform((float)a / 2, (float)a / 2);
                gfx.RotateTransform(rotationAngle);
                gfx.TranslateTransform(-(float)a / 2, -(float)a / 2);
                gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfx.DrawImage(roll, new System.Drawing.Point(0, 0));
                gfx.Dispose();
                pbRoll.Image = bmp;*/
                txtRoll.Text = Math.Round(rotationAngle, 3).ToString();
                doneRotate2 = true;
            }
            catch(InvalidOperationException ex)
            { }
        }

        void RotateImage(float rotationAngle)
        {
            try
            {
                doneRotate = false;
                /*int a = 500;
                Bitmap bmp = new Bitmap(a, a);
                Graphics gfx = Graphics.FromImage(bmp);
                gfx.TranslateTransform((float)a / 2, (float)a / 2);
                gfx.RotateTransform(rotationAngle);
                gfx.TranslateTransform(-(float)a / 2, -(float)a / 2);
                gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gfx.DrawImage(pitch, new System.Drawing.Point(0, 0));
                gfx.Dispose();
                pbPitch.Image = bmp;*/
                txtPitch.Text = Math.Round(rotationAngle, 3).ToString();
                doneRotate = true;
            }
            catch(InvalidOperationException ex)
            { }
        }
    }
}
