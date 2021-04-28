namespace MiREV
{
    partial class ImageLeft
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
            this.btnMeasure = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMeasure
            // 
            this.btnMeasure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMeasure.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMeasure.BackColor = System.Drawing.Color.Transparent;
            this.btnMeasure.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMeasure.FlatAppearance.BorderSize = 0;
            this.btnMeasure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMeasure.Image = global::MiREV.Properties.Resources.ruler;
            this.btnMeasure.Location = new System.Drawing.Point(1228, 0);
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMeasure.Size = new System.Drawing.Size(50, 50);
            this.btnMeasure.TabIndex = 11;
            this.btnMeasure.UseVisualStyleBackColor = false;
            this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1280, 720);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // ImageLeft
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.ControlBox = false;
            this.Controls.Add(this.btnMeasure);
            this.Controls.Add(this.pictureBox);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Location = new System.Drawing.Point(0, 1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImageLeft";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ImageLeft";
            this.Load += new System.EventHandler(this.ImageLeft_Load);
            this.ResizeBegin += new System.EventHandler(this.ImageLeft_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.ImageLeft_ResizeEnd);
            this.Move += new System.EventHandler(this.ImageLeft_Move);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnMeasure;
    }
}