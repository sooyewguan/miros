namespace MiREV
{
    partial class IncGauges
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncGauges));
            this.txtRoll = new System.Windows.Forms.TextBox();
            this.txtPitch = new System.Windows.Forms.TextBox();
            this.pbRoll = new System.Windows.Forms.PictureBox();
            this.pbPitch = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbSensorSignal = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensorSignal)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRoll
            // 
            this.txtRoll.BackColor = System.Drawing.SystemColors.Window;
            this.txtRoll.Enabled = false;
            this.txtRoll.Font = new System.Drawing.Font("Segoe UI Symbol", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoll.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRoll.Location = new System.Drawing.Point(103, 27);
            this.txtRoll.Name = "txtRoll";
            this.txtRoll.ReadOnly = true;
            this.txtRoll.Size = new System.Drawing.Size(157, 51);
            this.txtRoll.TabIndex = 52;
            this.txtRoll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPitch
            // 
            this.txtPitch.BackColor = System.Drawing.SystemColors.Window;
            this.txtPitch.Enabled = false;
            this.txtPitch.Font = new System.Drawing.Font("Segoe UI Symbol", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPitch.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPitch.Location = new System.Drawing.Point(106, 433);
            this.txtPitch.Name = "txtPitch";
            this.txtPitch.ReadOnly = true;
            this.txtPitch.Size = new System.Drawing.Size(157, 51);
            this.txtPitch.TabIndex = 51;
            this.txtPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pbRoll
            // 
            this.pbRoll.BackColor = System.Drawing.Color.Transparent;
            this.pbRoll.Image = ((System.Drawing.Image)(resources.GetObject("pbRoll.Image")));
            this.pbRoll.Location = new System.Drawing.Point(57, 531);
            this.pbRoll.Name = "pbRoll";
            this.pbRoll.Size = new System.Drawing.Size(250, 250);
            this.pbRoll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRoll.TabIndex = 50;
            this.pbRoll.TabStop = false;
            // 
            // pbPitch
            // 
            this.pbPitch.BackColor = System.Drawing.Color.Transparent;
            this.pbPitch.Image = ((System.Drawing.Image)(resources.GetObject("pbPitch.Image")));
            this.pbPitch.Location = new System.Drawing.Point(58, 125);
            this.pbPitch.Name = "pbPitch";
            this.pbPitch.Size = new System.Drawing.Size(250, 250);
            this.pbPitch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPitch.TabIndex = 49;
            this.pbPitch.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(24, 90);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 320);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            // 
            // pbSensorSignal
            // 
            this.pbSensorSignal.BackColor = System.Drawing.Color.Transparent;
            this.pbSensorSignal.Image = ((System.Drawing.Image)(resources.GetObject("pbSensorSignal.Image")));
            this.pbSensorSignal.Location = new System.Drawing.Point(23, 496);
            this.pbSensorSignal.Name = "pbSensorSignal";
            this.pbSensorSignal.Size = new System.Drawing.Size(320, 320);
            this.pbSensorSignal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSensorSignal.TabIndex = 47;
            this.pbSensorSignal.TabStop = false;
            // 
            // IncGauges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(366, 830);
            this.Controls.Add(this.txtRoll);
            this.Controls.Add(this.txtPitch);
            this.Controls.Add(this.pbRoll);
            this.Controls.Add(this.pbPitch);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbSensorSignal);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IncGauges";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Inclinometer";
            this.Load += new System.EventHandler(this.Gauges_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbRoll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensorSignal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRoll;
        private System.Windows.Forms.TextBox txtPitch;
        private System.Windows.Forms.PictureBox pbRoll;
        private System.Windows.Forms.PictureBox pbPitch;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbSensorSignal;
    }
}