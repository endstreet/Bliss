namespace Bliss
{
    partial class Dashboard
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MainMap = new GMap.NET.WindowsForms.GMapControl();
            this.InstrumentPanel = new System.Windows.Forms.Panel();
            this.Aux = new System.Windows.Forms.PictureBox();
            this.Reverse = new System.Windows.Forms.PictureBox();
            this.Stop = new System.Windows.Forms.PictureBox();
            this.checkConnect = new System.Windows.Forms.CheckBox();
            this.Down = new System.Windows.Forms.PictureBox();
            this.Up = new System.Windows.Forms.PictureBox();
            this.Right = new System.Windows.Forms.PictureBox();
            this.Left = new System.Windows.Forms.PictureBox();
            this.pictureCompass = new System.Windows.Forms.PictureBox();
            this.SpeedLbl = new System.Windows.Forms.Label();
            this.BearingLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.JoystickInputTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.InstrumentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Aux)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reverse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Up)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCompass)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.MainMap);
            this.splitContainer1.Panel1MinSize = 440;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.InstrumentPanel);
            this.splitContainer1.Panel2MinSize = 440;
            this.splitContainer1.Size = new System.Drawing.Size(1080, 610);
            this.splitContainer1.SplitterDistance = 440;
            this.splitContainer1.TabIndex = 6;
            // 
            // MainMap
            // 
            this.MainMap.AutoSize = true;
            this.MainMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainMap.Bearing = 0F;
            this.MainMap.CanDragMap = true;
            this.MainMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.MainMap.GrayScaleMode = false;
            this.MainMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.MainMap.LevelsKeepInMemory = 5;
            this.MainMap.Location = new System.Drawing.Point(0, 0);
            this.MainMap.MarkersEnabled = true;
            this.MainMap.MaxZoom = 2;
            this.MainMap.MinZoom = 2;
            this.MainMap.MouseWheelZoomEnabled = true;
            this.MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.MainMap.Name = "MainMap";
            this.MainMap.NegativeMode = false;
            this.MainMap.PolygonsEnabled = true;
            this.MainMap.RetryLoadTile = 0;
            this.MainMap.RoutesEnabled = true;
            this.MainMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.MainMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.MainMap.ShowTileGridLines = false;
            this.MainMap.Size = new System.Drawing.Size(440, 610);
            this.MainMap.TabIndex = 5;
            this.MainMap.Zoom = 0D;
            // 
            // InstrumentPanel
            // 
            this.InstrumentPanel.AutoSize = true;
            this.InstrumentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.InstrumentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.InstrumentPanel.Controls.Add(this.Aux);
            this.InstrumentPanel.Controls.Add(this.Reverse);
            this.InstrumentPanel.Controls.Add(this.Stop);
            this.InstrumentPanel.Controls.Add(this.checkConnect);
            this.InstrumentPanel.Controls.Add(this.Down);
            this.InstrumentPanel.Controls.Add(this.Up);
            this.InstrumentPanel.Controls.Add(this.Right);
            this.InstrumentPanel.Controls.Add(this.Left);
            this.InstrumentPanel.Controls.Add(this.pictureCompass);
            this.InstrumentPanel.Controls.Add(this.SpeedLbl);
            this.InstrumentPanel.Controls.Add(this.BearingLbl);
            this.InstrumentPanel.Controls.Add(this.label2);
            this.InstrumentPanel.Controls.Add(this.label1);
            this.InstrumentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InstrumentPanel.Location = new System.Drawing.Point(0, 0);
            this.InstrumentPanel.Name = "InstrumentPanel";
            this.InstrumentPanel.Size = new System.Drawing.Size(636, 610);
            this.InstrumentPanel.TabIndex = 4;
            // 
            // Aux
            // 
            this.Aux.Location = new System.Drawing.Point(346, 267);
            this.Aux.Name = "Aux";
            this.Aux.Size = new System.Drawing.Size(48, 47);
            this.Aux.TabIndex = 25;
            this.Aux.TabStop = false;
            // 
            // Reverse
            // 
            this.Reverse.Location = new System.Drawing.Point(346, 153);
            this.Reverse.Name = "Reverse";
            this.Reverse.Size = new System.Drawing.Size(48, 47);
            this.Reverse.TabIndex = 24;
            this.Reverse.TabStop = false;
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(291, 209);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(48, 47);
            this.Stop.TabIndex = 23;
            this.Stop.TabStop = false;
            // 
            // checkConnect
            // 
            this.checkConnect.AutoSize = true;
            this.checkConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkConnect.ForeColor = System.Drawing.Color.White;
            this.checkConnect.Location = new System.Drawing.Point(243, 128);
            this.checkConnect.Name = "checkConnect";
            this.checkConnect.Size = new System.Drawing.Size(86, 19);
            this.checkConnect.TabIndex = 22;
            this.checkConnect.Text = "Connected";
            this.checkConnect.UseVisualStyleBackColor = true;
            // 
            // Down
            // 
            this.Down.Location = new System.Drawing.Point(292, 267);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(48, 47);
            this.Down.TabIndex = 21;
            this.Down.TabStop = false;
            // 
            // Up
            // 
            this.Up.Location = new System.Drawing.Point(292, 153);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(48, 47);
            this.Up.TabIndex = 20;
            this.Up.TabStop = false;
            // 
            // Right
            // 
            this.Right.Location = new System.Drawing.Point(345, 209);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(48, 47);
            this.Right.TabIndex = 19;
            this.Right.TabStop = false;
            // 
            // Left
            // 
            this.Left.Location = new System.Drawing.Point(238, 209);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(48, 47);
            this.Left.TabIndex = 18;
            this.Left.TabStop = false;
            // 
            // pictureCompass
            // 
            this.pictureCompass.Location = new System.Drawing.Point(16, 153);
            this.pictureCompass.Name = "pictureCompass";
            this.pictureCompass.Size = new System.Drawing.Size(191, 179);
            this.pictureCompass.TabIndex = 4;
            this.pictureCompass.TabStop = false;
            // 
            // SpeedLbl
            // 
            this.SpeedLbl.AutoSize = true;
            this.SpeedLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SpeedLbl.ForeColor = System.Drawing.Color.White;
            this.SpeedLbl.Location = new System.Drawing.Point(110, 36);
            this.SpeedLbl.Name = "SpeedLbl";
            this.SpeedLbl.Size = new System.Drawing.Size(50, 15);
            this.SpeedLbl.TabIndex = 3;
            this.SpeedLbl.Text = "Bearing";
            this.SpeedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BearingLbl
            // 
            this.BearingLbl.AutoSize = true;
            this.BearingLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BearingLbl.ForeColor = System.Drawing.Color.White;
            this.BearingLbl.Location = new System.Drawing.Point(110, 12);
            this.BearingLbl.Name = "BearingLbl";
            this.BearingLbl.Size = new System.Drawing.Size(50, 15);
            this.BearingLbl.TabIndex = 2;
            this.BearingLbl.Text = "Bearing";
            this.BearingLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(28, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Speed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bearing";
            // 
            // JoystickInputTimer
            // 
            this.JoystickInputTimer.Tick += new System.EventHandler(this.JoystickInputTimer_Tick);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 610);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.InstrumentPanel.ResumeLayout(false);
            this.InstrumentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Aux)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reverse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Up)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCompass)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private SplitContainer splitContainer1;
        private System.Windows.Forms.Timer JoystickInputTimer;
        private GMap.NET.WindowsForms.GMapControl MainMap;
        private Panel InstrumentPanel;
        private PictureBox Aux;
        private PictureBox Reverse;
        private PictureBox Stop;
        private CheckBox checkConnect;
        private PictureBox Down;
        private PictureBox Up;
        private PictureBox Right;
        private PictureBox Left;
        private PictureBox pictureCompass;
        private Label SpeedLbl;
        private Label BearingLbl;
        private Label label2;
        private Label label1;
    }
}