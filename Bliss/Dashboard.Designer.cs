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
            this.blissMap1 = new Bliss.Controls.BlissMap();
            this.InstrumentPanel = new System.Windows.Forms.Panel();
            this.joystickActive = new System.Windows.Forms.Panel();
            this.spare2 = new System.Windows.Forms.PictureBox();
            this.spare1 = new System.Windows.Forms.PictureBox();
            this.Stop = new System.Windows.Forms.PictureBox();
            this.Left = new System.Windows.Forms.PictureBox();
            this.Aux = new System.Windows.Forms.PictureBox();
            this.Right = new System.Windows.Forms.PictureBox();
            this.Reverse = new System.Windows.Forms.PictureBox();
            this.Up = new System.Windows.Forms.PictureBox();
            this.Down = new System.Windows.Forms.PictureBox();
            this.button9 = new System.Windows.Forms.Button();
            this.groupPilot = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.autopilot = new System.Windows.Forms.Button();
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
            this.joystickActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spare2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spare1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Aux)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reverse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Up)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Down)).BeginInit();
            this.groupPilot.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.blissMap1);
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
            // blissMap1
            // 
            this.blissMap1.ApiKey = "";
            this.blissMap1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.blissMap1.DBFile = "Bliss";
            this.blissMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blissMap1.Location = new System.Drawing.Point(0, 0);
            this.blissMap1.Name = "blissMap1";
            this.blissMap1.Size = new System.Drawing.Size(440, 610);
            this.blissMap1.TabIndex = 0;
            // 
            // InstrumentPanel
            // 
            this.InstrumentPanel.AutoSize = true;
            this.InstrumentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.InstrumentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.InstrumentPanel.Controls.Add(this.joystickActive);
            this.InstrumentPanel.Controls.Add(this.button9);
            this.InstrumentPanel.Controls.Add(this.groupPilot);
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
            // joystickActive
            // 
            this.joystickActive.Controls.Add(this.spare2);
            this.joystickActive.Controls.Add(this.spare1);
            this.joystickActive.Controls.Add(this.Stop);
            this.joystickActive.Controls.Add(this.Left);
            this.joystickActive.Controls.Add(this.Aux);
            this.joystickActive.Controls.Add(this.Right);
            this.joystickActive.Controls.Add(this.Reverse);
            this.joystickActive.Controls.Add(this.Up);
            this.joystickActive.Controls.Add(this.Down);
            this.joystickActive.Location = new System.Drawing.Point(362, 38);
            this.joystickActive.Name = "joystickActive";
            this.joystickActive.Size = new System.Drawing.Size(246, 178);
            this.joystickActive.TabIndex = 38;
            // 
            // spare2
            // 
            this.spare2.BackColor = System.Drawing.Color.Transparent;
            this.spare2.Location = new System.Drawing.Point(11, 123);
            this.spare2.Name = "spare2";
            this.spare2.Size = new System.Drawing.Size(71, 47);
            this.spare2.TabIndex = 36;
            this.spare2.TabStop = false;
            // 
            // spare1
            // 
            this.spare1.Location = new System.Drawing.Point(11, 9);
            this.spare1.Name = "spare1";
            this.spare1.Size = new System.Drawing.Size(71, 47);
            this.spare1.TabIndex = 35;
            this.spare1.TabStop = false;
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(88, 65);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(71, 47);
            this.Stop.TabIndex = 32;
            this.Stop.TabStop = false;
            // 
            // Left
            // 
            this.Left.Location = new System.Drawing.Point(11, 65);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(71, 47);
            this.Left.TabIndex = 28;
            this.Left.TabStop = false;
            // 
            // Aux
            // 
            this.Aux.Location = new System.Drawing.Point(165, 123);
            this.Aux.Name = "Aux";
            this.Aux.Size = new System.Drawing.Size(71, 47);
            this.Aux.TabIndex = 34;
            this.Aux.TabStop = false;
            // 
            // Right
            // 
            this.Right.Location = new System.Drawing.Point(165, 65);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(71, 47);
            this.Right.TabIndex = 29;
            this.Right.TabStop = false;
            // 
            // Reverse
            // 
            this.Reverse.Location = new System.Drawing.Point(165, 9);
            this.Reverse.Name = "Reverse";
            this.Reverse.Size = new System.Drawing.Size(71, 47);
            this.Reverse.TabIndex = 33;
            this.Reverse.TabStop = false;
            // 
            // Up
            // 
            this.Up.Location = new System.Drawing.Point(88, 9);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(71, 47);
            this.Up.TabIndex = 30;
            this.Up.TabStop = false;
            // 
            // Down
            // 
            this.Down.Location = new System.Drawing.Point(88, 122);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(71, 47);
            this.Down.TabIndex = 31;
            this.Down.TabStop = false;
            // 
            // button9
            // 
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.ForeColor = System.Drawing.SystemColors.Control;
            this.button9.Location = new System.Drawing.Point(182, 238);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(96, 27);
            this.button9.TabIndex = 37;
            this.button9.Text = "...";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // groupPilot
            // 
            this.groupPilot.Controls.Add(this.button6);
            this.groupPilot.Controls.Add(this.button7);
            this.groupPilot.Controls.Add(this.button8);
            this.groupPilot.Controls.Add(this.button3);
            this.groupPilot.Controls.Add(this.button4);
            this.groupPilot.Controls.Add(this.button5);
            this.groupPilot.Controls.Add(this.button2);
            this.groupPilot.Controls.Add(this.button1);
            this.groupPilot.Controls.Add(this.autopilot);
            this.groupPilot.Font = new System.Drawing.Font("Copperplate Gothic Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupPilot.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.groupPilot.Location = new System.Drawing.Point(16, 290);
            this.groupPilot.Name = "groupPilot";
            this.groupPilot.Size = new System.Drawing.Size(355, 127);
            this.groupPilot.TabIndex = 27;
            this.groupPilot.TabStop = false;
            this.groupPilot.Text = "pilot commands";
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.SystemColors.Control;
            this.button6.Location = new System.Drawing.Point(241, 86);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(108, 27);
            this.button6.TabIndex = 45;
            this.button6.Text = "cancel";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.MouseClickButton);
            this.button6.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.button6.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // button7
            // 
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.SystemColors.Control;
            this.button7.Location = new System.Drawing.Point(126, 86);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(108, 27);
            this.button7.TabIndex = 44;
            this.button7.Text = "slow down";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.MouseClickButton);
            this.button7.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.button7.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // button8
            // 
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.SystemColors.Control;
            this.button8.Location = new System.Drawing.Point(12, 86);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(108, 27);
            this.button8.TabIndex = 43;
            this.button8.Text = "alarm";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.MouseClickButton);
            this.button8.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.button8.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.SystemColors.Control;
            this.button3.Location = new System.Drawing.Point(241, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 27);
            this.button3.TabIndex = 42;
            this.button3.Text = "right";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.MouseClickButton);
            this.button3.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.button3.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.SystemColors.Control;
            this.button4.Location = new System.Drawing.Point(126, 53);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(108, 27);
            this.button4.TabIndex = 41;
            this.button4.Text = "stop";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.MouseClickButton);
            this.button4.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.button4.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.SystemColors.Control;
            this.button5.Location = new System.Drawing.Point(12, 53);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(108, 27);
            this.button5.TabIndex = 40;
            this.button5.Text = "left";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.MouseClickButton);
            this.button5.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.button5.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(241, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 27);
            this.button2.TabIndex = 39;
            this.button2.Text = "max speed";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.MouseClickButton);
            this.button2.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.button2.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(126, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 27);
            this.button1.TabIndex = 38;
            this.button1.Text = "speed up";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.MouseClickButton);
            this.button1.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.button1.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // autopilot
            // 
            this.autopilot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.autopilot.ForeColor = System.Drawing.SystemColors.Control;
            this.autopilot.Location = new System.Drawing.Point(12, 20);
            this.autopilot.Name = "autopilot";
            this.autopilot.Size = new System.Drawing.Size(108, 27);
            this.autopilot.TabIndex = 37;
            this.autopilot.Text = "autopilot";
            this.autopilot.UseVisualStyleBackColor = true;
            this.autopilot.Click += new System.EventHandler(this.MouseClickButton);
            this.autopilot.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.autopilot.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // pictureCompass
            // 
            this.pictureCompass.Location = new System.Drawing.Point(16, 12);
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
            this.SpeedLbl.Location = new System.Drawing.Point(104, 244);
            this.SpeedLbl.Name = "SpeedLbl";
            this.SpeedLbl.Size = new System.Drawing.Size(16, 15);
            this.SpeedLbl.TabIndex = 3;
            this.SpeedLbl.Text = "...";
            this.SpeedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BearingLbl
            // 
            this.BearingLbl.AutoSize = true;
            this.BearingLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BearingLbl.ForeColor = System.Drawing.Color.White;
            this.BearingLbl.Location = new System.Drawing.Point(104, 220);
            this.BearingLbl.Name = "BearingLbl";
            this.BearingLbl.Size = new System.Drawing.Size(16, 15);
            this.BearingLbl.TabIndex = 2;
            this.BearingLbl.Text = "...";
            this.BearingLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(22, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Speed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 220);
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
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.InstrumentPanel.ResumeLayout(false);
            this.InstrumentPanel.PerformLayout();
            this.joystickActive.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spare2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spare1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Aux)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reverse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Up)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Down)).EndInit();
            this.groupPilot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCompass)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private SplitContainer splitContainer1;
        private System.Windows.Forms.Timer JoystickInputTimer;
        private Panel InstrumentPanel;
        private PictureBox pictureCompass;
        private Label SpeedLbl;
        private Label BearingLbl;
        private Label label2;
        private Label label1;
        private GroupBox groupPilot;
        private Controls.BlissMap blissMap1;
        private Button button9;
        private Panel joystickActive;
        private PictureBox spare2;
        private PictureBox spare1;
        private PictureBox Stop;
        private PictureBox Left;
        private PictureBox Aux;
        private PictureBox Right;
        private PictureBox Reverse;
        private PictureBox Up;
        private PictureBox Down;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button2;
        private Button button1;
        private Button autopilot;
    }
}