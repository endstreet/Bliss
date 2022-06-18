namespace Bliss.Controls
{
    partial class MotorControl
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
            this.progresstPower = new System.Windows.Forms.ProgressBar();
            this.labelRPM = new System.Windows.Forms.Label();
            this.labelRPMValue = new System.Windows.Forms.Label();
            this.labelDirection = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progresstPower
            // 
            this.progresstPower.BackColor = System.Drawing.Color.Black;
            this.progresstPower.Location = new System.Drawing.Point(0, 0);
            this.progresstPower.Name = "progresstPower";
            this.progresstPower.Size = new System.Drawing.Size(108, 27);
            this.progresstPower.TabIndex = 45;
            this.progresstPower.Value = 50;
            // 
            // labelRPM
            // 
            this.labelRPM.AutoSize = true;
            this.labelRPM.Location = new System.Drawing.Point(0, 35);
            this.labelRPM.Name = "labelRPM";
            this.labelRPM.Size = new System.Drawing.Size(32, 15);
            this.labelRPM.TabIndex = 46;
            this.labelRPM.Text = "RPM";
            // 
            // labelRPMValue
            // 
            this.labelRPMValue.AutoSize = true;
            this.labelRPMValue.Location = new System.Drawing.Point(60, 35);
            this.labelRPMValue.MinimumSize = new System.Drawing.Size(50, 0);
            this.labelRPMValue.Name = "labelRPMValue";
            this.labelRPMValue.Size = new System.Drawing.Size(50, 15);
            this.labelRPMValue.TabIndex = 47;
            this.labelRPMValue.Text = "0";
            this.labelRPMValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelDirection
            // 
            this.labelDirection.AutoSize = true;
            this.labelDirection.Location = new System.Drawing.Point(0, 50);
            this.labelDirection.MinimumSize = new System.Drawing.Size(50, 0);
            this.labelDirection.Name = "labelDirection";
            this.labelDirection.Size = new System.Drawing.Size(58, 15);
            this.labelDirection.TabIndex = 48;
            this.labelDirection.Text = "backward";
            // 
            // MotorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelDirection);
            this.Controls.Add(this.labelRPMValue);
            this.Controls.Add(this.labelRPM);
            this.Controls.Add(this.progresstPower);
            this.Name = "MotorControl";
            this.Size = new System.Drawing.Size(110, 72);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar progresstPower;
        private Label labelRPM;
        private Label labelRPMValue;
        private Label labelDirection;
        private System.Windows.Forms.Timer timer1;
    }
}
