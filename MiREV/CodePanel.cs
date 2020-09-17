using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PropertyGridExtensions;

namespace MiREV
{
    public partial class CodePanel : Form
    {
        public CodeSettings1 codeList1 = new CodeSettings1();
        public CodeSettings2 codeList2 = new CodeSettings2();
        public CodeSettings3 codeList3 = new CodeSettings3();
        private readonly Main _main;
       
        public CodePanel(Main main)
        {
            InitializeComponent();
            _main = main;

            this.Location = new Point((int)(Properties.Settings.Default.CodeLocation.X * _main.screenRatio), (int)(Properties.Settings.Default.CodeLocation.Y * _main.screenRatio));
            this.Size = new Size((int)(this.Width * _main.screenRatio), (int)(this.Height * _main.screenRatio));

            propertyGrid1.Size = new Size((int)(propertyGrid1.Size.Width * _main.screenRatio), (int)(propertyGrid1.Size.Height * _main.screenRatio));
            propertyGrid1.Location = new Point((int)(propertyGrid1.Location.X * _main.screenRatio), (int)(propertyGrid1.Location.Y * _main.screenRatio));
            //propertyGrid1.Font = new Font("Microsoft Sans Serif", (float)(10.0 * _main.screenRatio));
            propertyGrid1.Font = new Font("Microsoft Sans Serif", (float)(8 + 2 * _main.screenRatio));
            propertyGrid1.SelectedObject = codeList1;

            propertyGrid2.Size = new Size((int)(propertyGrid2.Size.Width * _main.screenRatio), (int)(propertyGrid2.Size.Height * _main.screenRatio));
            propertyGrid2.Location = new Point((int)(propertyGrid2.Location.X * _main.screenRatio), (int)(propertyGrid2.Location.Y * _main.screenRatio));
            //propertyGrid2.Font = new Font("Microsoft Sans Serif", (float)(10.0 * _main.screenRatio));
            propertyGrid2.Font = new Font("Microsoft Sans Serif", (float)(8 + 2 * _main.screenRatio ));
            propertyGrid2.SelectedObject = codeList2;

            propertyGrid3.Size = new Size((int)(propertyGrid3.Size.Width * _main.screenRatio), (int)(propertyGrid3.Size.Height * _main.screenRatio));
            propertyGrid3.Location = new Point((int)(propertyGrid3.Location.X * _main.screenRatio), (int)(propertyGrid3.Location.Y * _main.screenRatio));
            //propertyGrid3.Font = new Font("Microsoft Sans Serif", (float)(10.0 * _main.screenRatio));
            propertyGrid3.Font = new Font("Microsoft Sans Serif", (float)(8 + 2 * _main.screenRatio));
            propertyGrid3.SelectedObject = codeList3;
        }

        public void refreshPropertyGrid()
        {
            propertyGrid1.Refresh();
            propertyGrid2.Refresh();
            propertyGrid3.Refresh();
        }

        public Boolean isPropertyGridChanged()
        {
            return false;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (_main.selectedID > 0)
            {
                using (SqlConnection con = new SqlConnection(_main.conStrng))
                {
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "UPDATE Vida SET " + e.ChangedItem.PropertyDescriptor.Description + "=@UpdatedValue WHERE Id=@Index";

                        if (e.ChangedItem.PropertyDescriptor.Description.Equals("[Coding_date]") || 
                            e.ChangedItem.PropertyDescriptor.Description.Equals("[Road_survey_date]"))
                        {
                            com.Parameters.AddWithValue("@UpdatedValue", ((DateTime)e.ChangedItem.Value).ToShortDateString());
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@UpdatedValue", e.ChangedItem.Value.ToString());
                        }
                            
                        com.Parameters.AddWithValue("@Index", _main.selectedID);

                        con.Open();
                        com.ExecuteNonQuery();

                        //Debug.WriteLine(com.CommandText + " Value=" + e.ChangedItem.Value + " Index=" + _main.selectedID);
                    }
                }
            }
            else
            {
                Debug.WriteLine("Invalid database index.");
            }
        }

        private void propertyGrid2_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (_main.selectedID > 0)
            {
                using (SqlConnection con = new SqlConnection(_main.conStrng))
                {
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "UPDATE Vida SET " + e.ChangedItem.PropertyDescriptor.Description + "=@UpdatedValue WHERE Id=@Index";

                        if (e.ChangedItem.PropertyDescriptor.Description.Equals("[Coding_date]") ||
                            e.ChangedItem.PropertyDescriptor.Description.Equals("[Road_survey_date]"))
                        {
                            com.Parameters.AddWithValue("@UpdatedValue", ((DateTime)e.ChangedItem.Value).ToShortDateString());
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@UpdatedValue", e.ChangedItem.Value);
                        }

                        com.Parameters.AddWithValue("@Index", _main.selectedID);

                        con.Open();
                        com.ExecuteNonQuery();

                        //Debug.WriteLine(com.CommandText + " Value=" + e.ChangedItem.Value + " Index=" + _main.selectedID);
                    }
                }
            }
            else
            {
                Debug.WriteLine("Invalid database index.");
            }
        }

        private void propertyGrid3_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (_main.selectedID > 0)
            {
                using (SqlConnection con = new SqlConnection(_main.conStrng))
                {
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "UPDATE Vida SET " + e.ChangedItem.PropertyDescriptor.Description + "=@UpdatedValue WHERE Id=@Index";

                        if (e.ChangedItem.PropertyDescriptor.Description.Equals("[Coding_date]") ||
                            e.ChangedItem.PropertyDescriptor.Description.Equals("[Road_survey_date]"))
                        {
                            com.Parameters.AddWithValue("@UpdatedValue", ((DateTime)e.ChangedItem.Value).ToShortDateString());
                        }
                        else
                        {
                            /*Console.WriteLine(e.ChangedItem.Value);

                            String[] value = e.ChangedItem.Value.ToString().Split('-');

                            Console.WriteLine(value[0]);*/
                            com.Parameters.AddWithValue("@UpdatedValue", e.ChangedItem.Value.ToString());
                        }

                        com.Parameters.AddWithValue("@Index", _main.selectedID);

                        con.Open();
                        com.ExecuteNonQuery();

                        //Debug.WriteLine(com.CommandText + " Value=" + e.ChangedItem.Value + " Index=" + _main.selectedID);
                    }
                }
            }
            else
            {
                Debug.WriteLine("Invalid database index.");
            }
        }

        private void CodePanel_Load(object sender, EventArgs e)
        {
            //Location = Properties.Settings.Default.CodeLocation;
            //Size = Properties.Settings.Default.CodeSize;

            int splitterPosition = this.propertyGrid1.GetInternalLabelWidth();
            this.propertyGrid1.MoveSplitterTo(splitterPosition + (int)(100 * _main.screenRatio));

            int splitterPosition2 = this.propertyGrid2.GetInternalLabelWidth();
            this.propertyGrid2.MoveSplitterTo(splitterPosition2 + (int)(100 * _main.screenRatio));

            int splitterPosition3 = this.propertyGrid3.GetInternalLabelWidth();
            this.propertyGrid3.MoveSplitterTo(splitterPosition3 + (int)(100 * _main.screenRatio));
        }
    }
}
