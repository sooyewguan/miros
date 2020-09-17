using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiREV
{
    public partial class CsvSelector : Form
    {
        private readonly Main _main;
        private int minVal, maxVal;
        public CsvSelector(Main main)
        {
            InitializeComponent();
            _main = main;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            String folderName = "Output";

            try
            {
                if (!Directory.Exists(_main.filepath + "\\" + folderName))
                    Directory.CreateDirectory(_main.filepath + "\\" + folderName);

                TableDumper tb = new TableDumper();

                using (SqlConnection con = new SqlConnection(_main.conStrng))
                {
                    con.Open();
                    tb.DumpTableToFile(con, "Vida", _main.filepath + "\\" + folderName + "\\vida_" + minVal.ToString() + "_to_" + maxVal + ".csv", minVal/2, maxVal/2);
                    //int test = maxVal / 2;
                }
                MessageBox.Show("CSV file exported successfully.\n" + _main.filepath + "\\" + folderName + "\\vida_" + minVal.ToString() + "_to_" + maxVal + ".csv");
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to create the CSV file.\n" + ex.Message);
            }

        }

        private void CsvSelector_Load(object sender, EventArgs e)
        {
            trackBar1.Minimum = _main.trackBar.Minimum;
            trackBar1.Maximum = _main.trackBar.Maximum;
            trackBar2.Minimum = _main.trackBar.Minimum;
            trackBar2.Maximum = _main.trackBar.Maximum;
            trackBar2.Value = trackBar2.Maximum;
            minVal = _main.trackBar.Minimum;
            maxVal = _main.trackBar.Maximum;
            txtMin.Text = _main.trackBar.Minimum.ToString();
            txtMax.Text = _main.trackBar.Maximum.ToString();
            radAll.Checked = true;
            radAll.Enabled = true;
            radCust.Enabled = true;
            trackBar1.Enabled = false;
            trackBar2.Enabled = false;
            txtMin.Enabled = false;
            txtMax.Enabled = false;
        }

        private void radCust_CheckedChanged(object sender, EventArgs e)
        {
            if (radCust.Checked)
            {
                trackBar1.Enabled = true;
                trackBar2.Enabled = true;
                txtMin.Enabled = true;
                txtMax.Enabled = true;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            txtMin.Text = trackBar1.Value.ToString();
            minVal = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            txtMax.Text = trackBar2.Value.ToString();
            maxVal = trackBar2.Value;
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radAll.Checked)
            {
                trackBar1.Enabled = false;
                trackBar2.Enabled = false;
                txtMin.Enabled = false;
                txtMax.Enabled = false;
                minVal = _main.trackBar.Minimum;
                maxVal = _main.trackBar.Maximum;
            }
        }

        private void txtMin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(txtMin.Text) < _main.trackBar.Minimum)
                {
                    minVal = _main.trackBar.Minimum;
                    trackBar1.Value = _main.trackBar.Minimum;
                    txtMin.Text = _main.trackBar.Minimum.ToString();
                }
                else if (Convert.ToInt16(txtMin.Text) > _main.trackBar.Maximum)
                {
                    minVal = _main.trackBar.Maximum;
                    trackBar1.Value = _main.trackBar.Maximum;
                    txtMin.Text = _main.trackBar.Maximum.ToString();
                }
                else
                {
                    minVal = Convert.ToInt16(txtMin.Text);
                    trackBar1.Value = minVal;
                }
            }
            catch (FormatException ex)
            {
                txtMin.Text = _main.trackBar.Minimum.ToString();
                minVal = _main.trackBar.Minimum;
                trackBar1.Value = _main.trackBar.Minimum;
            }
        }

        private void txtMax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(txtMax.Text) > _main.trackBar.Maximum)
                {
                    maxVal = _main.trackBar.Maximum;
                    trackBar2.Value = _main.trackBar.Maximum;
                    txtMax.Text = _main.trackBar.Maximum.ToString();
                }
                else if (Convert.ToInt16(txtMin.Text) < _main.trackBar.Minimum)
                {
                    maxVal = _main.trackBar.Minimum;
                    trackBar2.Value = _main.trackBar.Minimum;
                    txtMax.Text = _main.trackBar.Minimum.ToString();
                }
                else
                {
                    maxVal = Convert.ToInt16(txtMax.Text);
                    trackBar2.Value = maxVal;
                }
            }
            catch (FormatException ex)
            {
                txtMax.Text = _main.trackBar.Maximum.ToString();
                maxVal = _main.trackBar.Maximum;
                trackBar2.Value = _main.trackBar.Maximum;
            }
        }

    }
}
