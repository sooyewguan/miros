namespace MiREV
{
    partial class MapViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gMapControl = new GMap.NET.WindowsForms.GMapControl();
            this.pbZoom = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // gMapControl
            // 
            this.gMapControl.Bearing = 0F;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl.GrayScaleMode = false;
            this.gMapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl.LevelsKeepInMemmory = 5;
            this.gMapControl.Location = new System.Drawing.Point(0, 0);
            this.gMapControl.Margin = new System.Windows.Forms.Padding(0);
            this.gMapControl.MarkersEnabled = true;
            this.gMapControl.MaxZoom = 20;
            this.gMapControl.MinZoom = 10;
            this.gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl.Name = "gMapControl";
            this.gMapControl.NegativeMode = false;
            this.gMapControl.PolygonsEnabled = true;
            this.gMapControl.RetryLoadTile = 0;
            this.gMapControl.RoutesEnabled = true;
            this.gMapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl.ShowTileGridLines = false;
            this.gMapControl.Size = new System.Drawing.Size(1258, 1053);
            this.gMapControl.TabIndex = 0;
            this.gMapControl.Zoom = 15D;
            this.gMapControl.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.MapViewer_OnMarkerEnter);
            this.gMapControl.Load += new System.EventHandler(this.gMapControl_Load);
            this.gMapControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gMapControl_KeyDown);
            this.gMapControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gMapControl_KeyPress);
            this.gMapControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gMapControl_KeyUp);
            this.gMapControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapViewer_MouseDown);
            this.gMapControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapViewer_MouseUp);
            // 
            // pbZoom
            // 
            this.pbZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbZoom.Enabled = false;
            this.pbZoom.Location = new System.Drawing.Point(0, 0);
            this.pbZoom.Name = "pbZoom";
            this.pbZoom.Size = new System.Drawing.Size(1258, 1053);
            this.pbZoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbZoom.TabIndex = 1;
            this.pbZoom.TabStop = false;
            this.pbZoom.Visible = false;
            // 
            // MapViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1258, 1053);
            this.ControlBox = false;
            this.Controls.Add(this.pbZoom);
            this.Controls.Add(this.gMapControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.Location = new System.Drawing.Point(0, 760);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MapViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MapViewer";
            this.Load += new System.EventHandler(this.MapViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapControl;
        private System.Windows.Forms.PictureBox pbZoom;
    }
}