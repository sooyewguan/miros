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
    public partial class Radius : Form
    {
        private float rad0_50, rad50_100, rad0_100;
        private readonly Main _main;

        public Radius(Main main)
        {
            InitializeComponent();
            _main = main;
        }
        
        public void CalcRadius(double bea1, double bea2, double bea3)
        {
            rad0_50 = 873 / (float)(bea2 - bea1);
            rad50_100 = 873 / (float)(bea3 - bea2);
            rad0_100 = 1746 / (float)(bea3 - bea1);

            if (rad0_50 < 0)
            {
                rad0_50 = rad0_50 * -1;
            }
            if (rad50_100 < 0)
            {
                rad50_100 = rad50_100 * -1;
            }
            if (rad0_100 < 0)
            {
                rad0_100 = rad0_100 * -1;
            }

            if (rad0_100 > 800.0)
            {
                _main.updateCurvature("1 - Straight or gently curving");
            }
            else
            {
                if (rad0_100 >= 400.0)
                {
                    _main.updateCurvature("2 - Moderate");
                }
                else
                {
                    if (rad0_100 >= 200)
                    {
                        _main.updateCurvature("3 - Sharp");
                    }
                    else
                    {
                        _main.updateCurvature("4 - Very sharp");
                    }
                }
            }

            txtHea1.Text = bea1.ToString();
            txtHea2.Text = bea2.ToString();
            txtHea3.Text = bea3.ToString();

            txtRad1.Text = rad0_50.ToString();
            txtRad2.Text = rad50_100.ToString();
            txtRad3.Text = rad0_100.ToString();
        }

        private void Radius_Load(object sender, EventArgs e)
        {
            this.Location = new Point((int)(Properties.Settings.Default.RadiusLocation.X * _main.screenRatio), (int)(Properties.Settings.Default.RadiusLocation.Y * _main.screenRatio));

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
    }
}
