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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageLeft));
            this.btnMeasure = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.pbPitch = new System.Windows.Forms.PictureBox();
            this.txtRoll = new System.Windows.Forms.TextBox();
            this.pbRoll = new System.Windows.Forms.PictureBox();
            this.txtPitch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoll)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMeasure
            // 
            this.btnMeasure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMeasure.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMeasure.BackColor = System.Drawing.Color.Transparent;
            this.btnMeasure.FlatAppearance.BorderSize = 0;
            this.btnMeasure.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMeasure.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMeasure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMeasure.Image = global::MiREV.Properties.Resources.ruler;
            this.btnMeasure.Location = new System.Drawing.Point(668, 0);
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMeasure.Size = new System.Drawing.Size(36, 36);
            this.btnMeasure.TabIndex = 11;
            this.btnMeasure.UseVisualStyleBackColor = false;
            this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(704, 720);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pbPitch
            // 
            this.pbPitch.BackColor = System.Drawing.Color.Transparent;
            this.pbPitch.Enabled = false;
            this.pbPitch.Image = ((System.Drawing.Image)(resources.GetObject("pbPitch.Image")));
            this.pbPitch.Location = new System.Drawing.Point(0, -9);
            this.pbPitch.Margin = new System.Windows.Forms.Padding(1);
            this.pbPitch.Name = "pbPitch";
            this.pbPitch.Size = new System.Drawing.Size(64, 64);
            this.pbPitch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPitch.TabIndex = 51;
            this.pbPitch.TabStop = false;
            // 
            // txtRoll
            // 
            this.txtRoll.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtRoll.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRoll.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtRoll.Enabled = false;
            this.txtRoll.Font = new System.Drawing.Font("Segoe UI Symbol", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoll.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRoll.Location = new System.Drawing.Point(0, 37);
            this.txtRoll.Margin = new System.Windows.Forms.Padding(1);
            this.txtRoll.Name = "txtRoll";
            this.txtRoll.ReadOnly = true;
            this.txtRoll.Size = new System.Drawing.Size(64, 18);
            this.txtRoll.TabIndex = 53;
            this.txtRoll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pbRoll
            // 
            this.pbRoll.BackColor = System.Drawing.Color.Transparent;
            this.pbRoll.Image = ((System.Drawing.Image)(resources.GetObject("pbRoll.Image")));
            this.pbRoll.Location = new System.Drawing.Point(64, -9);
            this.pbRoll.Margin = new System.Windows.Forms.Padding(1);
            this.pbRoll.Name = "pbRoll";
            this.pbRoll.Size = new System.Drawing.Size(64, 64);
            this.pbRoll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRoll.TabIndex = 54;
            this.pbRoll.TabStop = false;
            // 
            // txtPitch
            // 
            this.txtPitch.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtPitch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPitch.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtPitch.Enabled = false;
            this.txtPitch.Font = new System.Drawing.Font("Segoe UI Symbol", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPitch.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPitch.Location = new System.Drawing.Point(64, 37);
            this.txtPitch.Margin = new System.Windows.Forms.Padding(1);
            this.txtPitch.Name = "txtPitch";
            this.txtPitch.ReadOnly = true;
            this.txtPitch.Size = new System.Drawing.Size(64, 18);
            this.txtPitch.TabIndex = 55;
            this.txtPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ImageLeft
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(704, 720);
            this.ControlBox = false;
            this.Controls.Add(this.txtPitch);
            this.Controls.Add(this.pbRoll);
            this.Controls.Add(this.txtRoll);
            this.Controls.Add(this.pbPitch);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbPitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoll)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnMeasure;
        private System.Windows.Forms.PictureBox pbPitch;
        private System.Windows.Forms.TextBox txtRoll;
        private System.Windows.Forms.PictureBox pbRoll;
        private System.Windows.Forms.TextBox txtPitch;
    }
}