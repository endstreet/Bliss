namespace Bliss.Controls
{
    partial class DepthWidget
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
            this.listDepths = new System.Windows.Forms.TextBox();
            this.pictureDepthChart = new System.Windows.Forms.PictureBox();
            this.checkAutoScan = new System.Windows.Forms.CheckBox();
            this.labelDepth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDepthChart)).BeginInit();
            this.SuspendLayout();
            // 
            // listDepths
            // 
            this.listDepths.AcceptsReturn = true;
            this.listDepths.BackColor = System.Drawing.Color.Black;
            this.listDepths.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listDepths.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.listDepths.ForeColor = System.Drawing.Color.White;
            this.listDepths.Location = new System.Drawing.Point(10, 11);
            this.listDepths.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.listDepths.MaximumSize = new System.Drawing.Size(40, 150);
            this.listDepths.Multiline = true;
            this.listDepths.Name = "listDepths";
            this.listDepths.Size = new System.Drawing.Size(40, 150);
            this.listDepths.TabIndex = 8;
            this.listDepths.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureDepthChart
            // 
            this.pictureDepthChart.Location = new System.Drawing.Point(62, 11);
            this.pictureDepthChart.Margin = new System.Windows.Forms.Padding(0);
            this.pictureDepthChart.MaximumSize = new System.Drawing.Size(178, 150);
            this.pictureDepthChart.Name = "pictureDepthChart";
            this.pictureDepthChart.Size = new System.Drawing.Size(178, 150);
            this.pictureDepthChart.TabIndex = 6;
            this.pictureDepthChart.TabStop = false;
            // 
            // checkAutoScan
            // 
            this.checkAutoScan.AutoSize = true;
            this.checkAutoScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkAutoScan.ForeColor = System.Drawing.Color.DodgerBlue;
            this.checkAutoScan.Location = new System.Drawing.Point(14, 195);
            this.checkAutoScan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkAutoScan.Name = "checkAutoScan";
            this.checkAutoScan.Size = new System.Drawing.Size(55, 17);
            this.checkAutoScan.TabIndex = 9;
            this.checkAutoScan.Text = "Scan";
            this.checkAutoScan.UseVisualStyleBackColor = true;
            // 
            // labelDepth
            // 
            this.labelDepth.AutoSize = true;
            this.labelDepth.BackColor = System.Drawing.Color.Transparent;
            this.labelDepth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelDepth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelDepth.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelDepth.Location = new System.Drawing.Point(62, 170);
            this.labelDepth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDepth.Name = "labelDepth";
            this.labelDepth.Size = new System.Drawing.Size(0, 13);
            this.labelDepth.TabIndex = 10;
            // 
            // DepthWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.labelDepth);
            this.Controls.Add(this.checkAutoScan);
            this.Controls.Add(this.listDepths);
            this.Controls.Add(this.pictureDepthChart);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DepthWidget";
            this.Size = new System.Drawing.Size(257, 249);
            ((System.ComponentModel.ISupportInitialize)(this.pictureDepthChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox listDepths;
        private System.Windows.Forms.PictureBox pictureDepthChart;
        public System.Windows.Forms.CheckBox checkAutoScan;
        private System.Windows.Forms.Label labelDepth;
    }
}
