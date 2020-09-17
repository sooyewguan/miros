namespace MiREV
{
    partial class ProjPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjPanel));
            this.txtCoderName = new System.Windows.Forms.TextBox();
            this.txtBrowse = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.pictureBrowse = new System.Windows.Forms.PictureBox();
            this.pictureName = new System.Windows.Forms.PictureBox();
            this.pictureBusy = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBrowse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBusy)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCoderName
            // 
            this.txtCoderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoderName.Location = new System.Drawing.Point(64, 48);
            this.txtCoderName.Name = "txtCoderName";
            this.txtCoderName.Size = new System.Drawing.Size(502, 35);
            this.txtCoderName.TabIndex = 1;
            this.txtCoderName.Text = "Coder Name";
            // 
            // txtBrowse
            // 
            this.txtBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBrowse.Location = new System.Drawing.Point(64, 119);
            this.txtBrowse.Name = "txtBrowse";
            this.txtBrowse.Size = new System.Drawing.Size(502, 35);
            this.txtBrowse.TabIndex = 2;
            this.txtBrowse.Text = "Browse ...";
            this.txtBrowse.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtBrowse_MouseClick);
            // 
            // txtStatus
            // 
            this.txtStatus.Enabled = false;
            this.txtStatus.Location = new System.Drawing.Point(79, 194);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(310, 60);
            this.txtStatus.TabIndex = 7;
            // 
            // pictureBrowse
            // 
            this.pictureBrowse.Image = ((System.Drawing.Image)(resources.GetObject("pictureBrowse.Image")));
            this.pictureBrowse.Location = new System.Drawing.Point(13, 119);
            this.pictureBrowse.Name = "pictureBrowse";
            this.pictureBrowse.Size = new System.Drawing.Size(45, 35);
            this.pictureBrowse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBrowse.TabIndex = 6;
            this.pictureBrowse.TabStop = false;
            // 
            // pictureName
            // 
            this.pictureName.Image = ((System.Drawing.Image)(resources.GetObject("pictureName.Image")));
            this.pictureName.Location = new System.Drawing.Point(13, 48);
            this.pictureName.Name = "pictureName";
            this.pictureName.Size = new System.Drawing.Size(45, 35);
            this.pictureName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureName.TabIndex = 5;
            this.pictureName.TabStop = false;
            // 
            // pictureBusy
            // 
            this.pictureBusy.BackColor = System.Drawing.Color.Transparent;
            this.pictureBusy.Image = global::MiREV.Properties.Resources.busy;
            this.pictureBusy.Location = new System.Drawing.Point(13, 194);
            this.pictureBusy.Name = "pictureBusy";
            this.pictureBusy.Size = new System.Drawing.Size(60, 60);
            this.pictureBusy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBusy.TabIndex = 4;
            this.pictureBusy.TabStop = false;
            this.pictureBusy.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(496, 184);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 70);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Image = ((System.Drawing.Image)(resources.GetObject("btnCreate.Image")));
            this.btnCreate.Location = new System.Drawing.Point(420, 184);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(70, 70);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // ProjPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(578, 269);
            this.ControlBox = false;
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.pictureBrowse);
            this.Controls.Add(this.pictureName);
            this.Controls.Add(this.pictureBusy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtBrowse);
            this.Controls.Add(this.txtCoderName);
            this.Controls.Add(this.btnCreate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Import";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProjPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBrowse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBusy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtCoderName;
        private System.Windows.Forms.TextBox txtBrowse;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pictureBusy;
        private System.Windows.Forms.PictureBox pictureName;
        private System.Windows.Forms.PictureBox pictureBrowse;
        private System.Windows.Forms.TextBox txtStatus;
    }
}