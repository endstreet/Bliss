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
            this.pictureCompassM = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonMapOrientation = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnMotorRight = new System.Windows.Forms.Button();
            this.btnMotorLeft = new System.Windows.Forms.Button();
            this.groupPilot = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSpeedDown = new System.Windows.Forms.Button();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnMaxSpeed = new System.Windows.Forms.Button();
            this.btnSpeedUp = new System.Windows.Forms.Button();
            this.btnAutopilot = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureCompassM)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(1099, 458);
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
            this.blissMap1.Size = new System.Drawing.Size(440, 458);
            this.blissMap1.TabIndex = 0;
            // 
            // InstrumentPanel
            // 
            this.InstrumentPanel.AutoSize = true;
            this.InstrumentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.InstrumentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.InstrumentPanel.Controls.Add(this.pictureCompassM);
            this.InstrumentPanel.Controls.Add(this.groupBox2);
            this.InstrumentPanel.Controls.Add(this.groupBox1);
            this.InstrumentPanel.Controls.Add(this.groupPilot);
            this.InstrumentPanel.Controls.Add(this.pictureCompass);
            this.InstrumentPanel.Controls.Add(this.SpeedLbl);
            this.InstrumentPanel.Controls.Add(this.BearingLbl);
            this.InstrumentPanel.Controls.Add(this.label2);
            this.InstrumentPanel.Controls.Add(this.label1);
            this.InstrumentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InstrumentPanel.Location = new System.Drawing.Point(0, 0);
            this.InstrumentPanel.Name = "InstrumentPanel";
            this.InstrumentPanel.Size = new System.Drawing.Size(655, 458);
            this.InstrumentPanel.TabIndex = 4;
            // 
            // pictureCompassM
            // 
            this.pictureCompassM.Location = new System.Drawing.Point(229, 12);
            this.pictureCompassM.Name = "pictureCompassM";
            this.pictureCompassM.Size = new System.Drawing.Size(191, 179);
            this.pictureCompassM.TabIndex = 47;
            this.pictureCompassM.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonMapOrientation);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Copperplate Gothic Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.groupBox2.Location = new System.Drawing.Point(377, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 224);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "navigation";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // buttonMapOrientation
            // 
            this.buttonMapOrientation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMapOrientation.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonMapOrientation.Location = new System.Drawing.Point(9, 170);
            this.buttonMapOrientation.Name = "buttonMapOrientation";
            this.buttonMapOrientation.Size = new System.Drawing.Size(248, 27);
            this.buttonMapOrientation.TabIndex = 38;
            this.buttonMapOrientation.Text = "map orientation north";
            this.buttonMapOrientation.UseVisualStyleBackColor = true;
            this.buttonMapOrientation.Click += new System.EventHandler(this.buttonMapO_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(174, 133);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(16, 15);
            this.label20.TabIndex = 23;
            this.label20.Text = "...";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(9, 136);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(96, 14);
            this.label21.TabIndex = 22;
            this.label21.Text = "waypoint eta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(174, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "...";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(9, 122);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(131, 14);
            this.label19.TabIndex = 20;
            this.label19.Text = "waypoint distance";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(174, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 15);
            this.label11.TabIndex = 19;
            this.label11.Text = "...";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(174, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 15);
            this.label12.TabIndex = 18;
            this.label12.Text = "...";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(9, 108);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 14);
            this.label13.TabIndex = 17;
            this.label13.Text = "waypoint time";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(9, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(106, 14);
            this.label14.TabIndex = 16;
            this.label14.Text = "total distance";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(174, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 15);
            this.label15.TabIndex = 15;
            this.label15.Text = "...";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(174, 64);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 15);
            this.label16.TabIndex = 14;
            this.label16.Text = "...";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(9, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 14);
            this.label17.TabIndex = 13;
            this.label17.Text = "Total time";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(9, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(135, 14);
            this.label18.TabIndex = 12;
            this.label18.Text = "remaining distance";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(174, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "...";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(174, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "...";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(9, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 14);
            this.label9.TabIndex = 9;
            this.label9.Text = "remaining time";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(9, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 14);
            this.label10.TabIndex = 8;
            this.label10.Text = "eta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(174, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "...";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(9, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "destination";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar2);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.btnMotorRight);
            this.groupBox1.Controls.Add(this.btnMotorLeft);
            this.groupBox1.Font = new System.Drawing.Font("Copperplate Gothic Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.groupBox1.Location = new System.Drawing.Point(16, 348);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 91);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "drive motors";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(241, 53);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(108, 27);
            this.progressBar2.TabIndex = 44;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 53);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(108, 27);
            this.progressBar1.TabIndex = 43;
            this.progressBar1.Value = 50;
            // 
            // btnMotorRight
            // 
            this.btnMotorRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMotorRight.ForeColor = System.Drawing.SystemColors.Control;
            this.btnMotorRight.Location = new System.Drawing.Point(241, 20);
            this.btnMotorRight.Name = "btnMotorRight";
            this.btnMotorRight.Size = new System.Drawing.Size(108, 27);
            this.btnMotorRight.TabIndex = 42;
            this.btnMotorRight.Text = "right";
            this.btnMotorRight.UseVisualStyleBackColor = true;
            // 
            // btnMotorLeft
            // 
            this.btnMotorLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMotorLeft.ForeColor = System.Drawing.SystemColors.Control;
            this.btnMotorLeft.Location = new System.Drawing.Point(12, 20);
            this.btnMotorLeft.Name = "btnMotorLeft";
            this.btnMotorLeft.Size = new System.Drawing.Size(108, 27);
            this.btnMotorLeft.TabIndex = 40;
            this.btnMotorLeft.Text = "left";
            this.btnMotorLeft.UseVisualStyleBackColor = true;
            // 
            // groupPilot
            // 
            this.groupPilot.Controls.Add(this.btnCancel);
            this.groupPilot.Controls.Add(this.btnSpeedDown);
            this.groupPilot.Controls.Add(this.btnAlarm);
            this.groupPilot.Controls.Add(this.btnRight);
            this.groupPilot.Controls.Add(this.btnStop);
            this.groupPilot.Controls.Add(this.btnLeft);
            this.groupPilot.Controls.Add(this.btnMaxSpeed);
            this.groupPilot.Controls.Add(this.btnSpeedUp);
            this.groupPilot.Controls.Add(this.btnAutopilot);
            this.groupPilot.Font = new System.Drawing.Font("Copperplate Gothic Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupPilot.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.groupPilot.Location = new System.Drawing.Point(16, 215);
            this.groupPilot.Name = "groupPilot";
            this.groupPilot.Size = new System.Drawing.Size(355, 127);
            this.groupPilot.TabIndex = 27;
            this.groupPilot.TabStop = false;
            this.groupPilot.Text = "pilot commands";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Location = new System.Drawing.Point(241, 86);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 27);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.MouseClickButton);
            this.btnCancel.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.btnCancel.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // btnSpeedDown
            // 
            this.btnSpeedDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpeedDown.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSpeedDown.Location = new System.Drawing.Point(126, 86);
            this.btnSpeedDown.Name = "btnSpeedDown";
            this.btnSpeedDown.Size = new System.Drawing.Size(108, 27);
            this.btnSpeedDown.TabIndex = 44;
            this.btnSpeedDown.Text = "slow down";
            this.btnSpeedDown.UseVisualStyleBackColor = true;
            this.btnSpeedDown.Click += new System.EventHandler(this.MouseClickButton);
            this.btnSpeedDown.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.btnSpeedDown.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // btnAlarm
            // 
            this.btnAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlarm.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAlarm.Location = new System.Drawing.Point(12, 86);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(108, 27);
            this.btnAlarm.TabIndex = 43;
            this.btnAlarm.Text = "alarm";
            this.btnAlarm.UseVisualStyleBackColor = true;
            this.btnAlarm.Click += new System.EventHandler(this.MouseClickButton);
            this.btnAlarm.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.btnAlarm.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // btnRight
            // 
            this.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRight.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRight.Location = new System.Drawing.Point(241, 53);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(108, 27);
            this.btnRight.TabIndex = 42;
            this.btnRight.Text = "right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.MouseClickButton);
            this.btnRight.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.btnRight.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // btnStop
            // 
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.ForeColor = System.Drawing.SystemColors.Control;
            this.btnStop.Location = new System.Drawing.Point(126, 53);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(108, 27);
            this.btnStop.TabIndex = 41;
            this.btnStop.Text = "stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.MouseClickButton);
            this.btnStop.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.btnStop.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // btnLeft
            // 
            this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLeft.ForeColor = System.Drawing.SystemColors.Control;
            this.btnLeft.Location = new System.Drawing.Point(12, 53);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(108, 27);
            this.btnLeft.TabIndex = 40;
            this.btnLeft.Text = "left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.MouseClickButton);
            this.btnLeft.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.btnLeft.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // btnMaxSpeed
            // 
            this.btnMaxSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaxSpeed.ForeColor = System.Drawing.SystemColors.Control;
            this.btnMaxSpeed.Location = new System.Drawing.Point(241, 20);
            this.btnMaxSpeed.Name = "btnMaxSpeed";
            this.btnMaxSpeed.Size = new System.Drawing.Size(108, 27);
            this.btnMaxSpeed.TabIndex = 39;
            this.btnMaxSpeed.Text = "max speed";
            this.btnMaxSpeed.UseVisualStyleBackColor = true;
            this.btnMaxSpeed.Click += new System.EventHandler(this.MouseClickButton);
            this.btnMaxSpeed.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.btnMaxSpeed.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // btnSpeedUp
            // 
            this.btnSpeedUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpeedUp.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSpeedUp.Location = new System.Drawing.Point(126, 20);
            this.btnSpeedUp.Name = "btnSpeedUp";
            this.btnSpeedUp.Size = new System.Drawing.Size(108, 27);
            this.btnSpeedUp.TabIndex = 38;
            this.btnSpeedUp.Text = "speed up";
            this.btnSpeedUp.UseVisualStyleBackColor = true;
            this.btnSpeedUp.Click += new System.EventHandler(this.MouseClickButton);
            this.btnSpeedUp.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.btnSpeedUp.MouseHover += new System.EventHandler(this.MouseOverButton);
            // 
            // btnAutopilot
            // 
            this.btnAutopilot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutopilot.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAutopilot.Location = new System.Drawing.Point(12, 20);
            this.btnAutopilot.Name = "btnAutopilot";
            this.btnAutopilot.Size = new System.Drawing.Size(108, 27);
            this.btnAutopilot.TabIndex = 37;
            this.btnAutopilot.Text = "autopilot";
            this.btnAutopilot.UseVisualStyleBackColor = true;
            this.btnAutopilot.Click += new System.EventHandler(this.MouseClickButton);
            this.btnAutopilot.MouseLeave += new System.EventHandler(this.MouseLeaveButton);
            this.btnAutopilot.MouseHover += new System.EventHandler(this.MouseOverButton);
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
            this.SpeedLbl.Location = new System.Drawing.Point(527, 36);
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
            this.BearingLbl.Location = new System.Drawing.Point(527, 12);
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
            this.label2.Location = new System.Drawing.Point(445, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Speed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(445, 12);
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
            this.ClientSize = new System.Drawing.Size(1099, 458);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureCompassM)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
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
        private Button btnCancel;
        private Button btnSpeedDown;
        private Button btnAlarm;
        private Button btnRight;
        private Button btnStop;
        private Button btnLeft;
        private Button btnMaxSpeed;
        private Button btnSpeedUp;
        private Button btnAutopilot;
        private GroupBox groupBox1;
        private ProgressBar progressBar2;
        private ProgressBar progressBar1;
        private Button btnMotorRight;
        private Button btnMotorLeft;
        private GroupBox groupBox2;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label3;
        private Label label5;
        private Label label20;
        private Label label21;
        private Label label4;
        private Label label19;
        private PictureBox pictureCompassM;
        private Button buttonMapOrientation;
    }
}