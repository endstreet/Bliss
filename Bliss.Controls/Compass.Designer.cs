namespace Bliss.Controls
{
    partial class Compass
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Compass));
            this.pictureCompass = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCompass)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureCompass
            // 
            this.pictureCompass.BackColor = System.Drawing.Color.Transparent;
            this.pictureCompass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureCompass.BackgroundImage")));
            this.pictureCompass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureCompass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureCompass.Image = ((System.Drawing.Image)(resources.GetObject("pictureCompass.Image")));
            this.pictureCompass.Location = new System.Drawing.Point(0, 0);
            this.pictureCompass.Margin = new System.Windows.Forms.Padding(0);
            this.pictureCompass.MaximumSize = new System.Drawing.Size(220, 220);
            this.pictureCompass.MinimumSize = new System.Drawing.Size(220, 220);
            this.pictureCompass.Name = "pictureCompass";
            this.pictureCompass.Size = new System.Drawing.Size(220, 220);
            this.pictureCompass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureCompass.TabIndex = 5;
            this.pictureCompass.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // Compass
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.pictureCompass);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(220, 220);
            this.MinimumSize = new System.Drawing.Size(220, 220);
            this.Name = "Compass";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(220, 220);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCompass)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureCompass;
        private System.Windows.Forms.Timer timer1;
    }
}
