namespace Bliss.Controls
{
    partial class DepthScanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepthScanner));
            this.pictureDepth = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureDepth
            // 
            this.pictureDepth.BackColor = System.Drawing.Color.Transparent;
            this.pictureDepth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureDepth.BackgroundImage")));
            this.pictureDepth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureDepth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureDepth.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureDepth.ErrorImage")));
            this.pictureDepth.Image = ((System.Drawing.Image)(resources.GetObject("pictureDepth.Image")));
            this.pictureDepth.Location = new System.Drawing.Point(0, 0);
            this.pictureDepth.Margin = new System.Windows.Forms.Padding(0);
            this.pictureDepth.MaximumSize = new System.Drawing.Size(220, 220);
            this.pictureDepth.MinimumSize = new System.Drawing.Size(220, 220);
            this.pictureDepth.Name = "pictureDepth";
            this.pictureDepth.Size = new System.Drawing.Size(220, 220);
            this.pictureDepth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureDepth.TabIndex = 5;
            this.pictureDepth.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // DepthScanner
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.pictureDepth);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(220, 250);
            this.MinimumSize = new System.Drawing.Size(220, 220);
            this.Name = "DepthScanner";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(220, 220);
            ((System.ComponentModel.ISupportInitialize)(this.pictureDepth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureDepth;
        private System.Windows.Forms.Timer timer1;
    }
}
