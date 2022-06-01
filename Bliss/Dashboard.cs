using Bliss.Component;
using Bliss.Services;
using SharpDX.DirectInput;
using System.Data;

namespace Bliss
{
    public partial class Dashboard : Form
    {

        private PilotService pilot;
        //private SimulationService pilot = new();
        private JoystickService? Joystick;
        private System.Windows.Forms.Timer GetLocation = new System.Windows.Forms.Timer();

        private string AlarmText = "";

        private readonly DbService dbs;

        private string ScrollText
        {
            get
            {
                return labelAlarms.Text.Substring(1, 124) + labelAlarms.Text.Substring(0, 1); ; 
            }
            set
            {
                labelAlarms.Text = value;
            }
        }

        public Dashboard(PilotService _pilot,DbService _db)
        {
            InitializeComponent();

            blissMap1.ApiKey = AppSettings.Default.GoogleMapApiKey;
            blissMap1.DBFile = AppSettings.Default.DBLocation;

            pilot = _pilot;
            dbs = _db;

            ChangeTheme(splitContainer1.Panel2.Controls);

            //Timer
            GetLocation.Tick += MainMap_LocationUpdate;
            GetLocation.Interval = AppSettings.Default.DashBoardUpdateInterval * 1000;
            GetLocation.Start();


            //Joystick
            ScanJoysticks();
        }
        public void ChangeTheme(Control.ControlCollection components)
        {
            foreach (Control component in components)
            {
                if (component is Panel)
                {

                    component.BackColor = ColorScheme.BG;
                    component.ForeColor = ColorScheme.FG;
                    component.Font = ColorScheme.LargeFont;
                }
                else //(component is Button)
                {
                    component.BackColor = ColorScheme.BG;
                    component.ForeColor = ColorScheme.FG;
                    component.Font = ColorScheme.SmallFont;
                }
                if (component.HasChildren)
                {
                    ChangeTheme(component.Controls);
                }
            }
            labelAlarms.ForeColor = Color.Red;
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            //trackBarZoom.Value = (int)MainMap.Zoom * 100;
            Activate();
            TopMost = true;
            TopMost = false;
        }

        /// <summary>
        ///  On Positionchanged
        /// </summary>
        private void MainMap_LocationUpdate(object? sender, EventArgs e)
        {
            BearingLbl.Text = Info.Bearing.ToString();
            SpeedLbl.Text = Info.Speed.ToString();
            pictureCompass.Image = Compass.DrawCompass((int)Math.Round(Info.Bearing, 0), 0, 80, 0, 80, pictureCompass.Size);
            pictureCompassM.Image = Compass.DrawCompass((int)Math.Round(Info.Bearing, 0), 0, 80, 0, 80, pictureCompass.Size);
            blissMap1.MainMap_LocationUpdate();
            progressLeftPower.Value = Info.PowerLeft > 0 ? Info.PowerLeft : Info.PowerLeft * -1;
            progressRightPower.Value = Info.PowerRight > 0 ? Info.PowerRight : Info.PowerRight * -1;
            btnLeftReverse.ForeColor = Info.LeftReverse ? ColorScheme.Busy : ColorScheme.FG;
            btnRightReverse.ForeColor = Info.RightReverse ? ColorScheme.Busy : ColorScheme.FG;

            if (State.Alarm)
            {
                if (AlarmText != State.ToString())//New alarm
                {
                    AlarmText = State.ToString();
                    labelAlarms.Text = AlarmText.PadRight(125);
                }
                btnAlarm.ForeColor = ColorScheme.Busy;
                labelAlarms.Text = ScrollText;
            }
            else
            {
                AlarmText = "";
                labelAlarms.Text = "";
                btnAlarm.ForeColor = ColorScheme.FG;
            }
        }
        //private void OnAlarm(object? sender, EventArgs e)
        //{
        //    ScrollText = ScrollText.Substring(1, 129) + ScrollText.Substring(0, 1);
        //    labelAlarms.Text = ScrollText;
        //}
        #region Joystick
        private void ScanJoysticks()
        {
            JoystickInputTimer.Enabled = false;
            DirectInput _directInput = new DirectInput();

            if (_directInput.GetDevices().Where(d => d.ProductName == "USB Game Controllers").Any())
            {
                DeviceInstance firstJoystickInstance = _directInput.GetDevices().Where(d => d.ProductName == "USB Game Controllers").First();
                Joystick = new JoystickService(_directInput, firstJoystickInstance);
                Joystick.Acquire();
                JoystickInputTimer.Enabled = true;
                //joystickActive.BackColor = ColorScheme.Active;
            }
        }
        private void ProcessPilotCommands()
        {
            if(State.Alarm)
            {

            }
            if (!Info.PilotCommands.Any()) return;
            ProcessPilotCommand(Info.PilotCommands.Dequeue());
        }
        private void ProcessPilotCommand(PilotCommand input)
        {
            btnLeft.BackColor = input.TurnLeft ? Color.Red : Color.Transparent;
            btnRight.BackColor = input.TurnRight ? Color.Red : Color.Transparent;
            btnSpeedUp.BackColor = input.SpeedUp ? Color.Red : Color.Transparent;
            btnSpeedDown.BackColor = input.SpeedDown ? Color.Red : Color.Transparent;
            btnAlarm.BackColor = input.Alarm ? Color.Red : Color.Transparent;
            btnStop.BackColor = input.Stop ? Color.Red : Color.Transparent;
            btnCancel.BackColor = input.Cancel ? Color.Red : Color.Transparent;

            pilot.OnPilotCommand(input);


        }
        private void JoystickInputTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Joystick.Update();
                ProcessPilotCommands();
            }
            catch (Exception ex)
            {
                if (JoystickInputTimer.Enabled)
                {
                    State.Alarms.Enqueue("Joystick unplugged |");
                }
                ScanJoysticks();
            }
        }
        #endregion
        private void Right_Click(object sender, EventArgs e)
        {

        }

        private void MouseOverButton(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = ColorScheme.Active;
        }

        private void MouseLeaveButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = ColorScheme.BG;
            if (button.Tag != null) return;
            button.ForeColor = ColorScheme.FG;
        }

        private void MouseClickButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Tag = button.Tag == null ? "Active" : null;
            if (button.Tag == null) return;
            button.ForeColor = ColorScheme.Busy;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMapO_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Text = button.Text == "Map Orientation BEARING" ? "Map Orientation NORTH" : "Map Orientation BEARING";
            Info.MapShowBearing = button.Text == "Map Orientation BEARING";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAlarm_Click(object sender, EventArgs e)
        {
            if (State.Alarm)//If there are alarms dequeue one
            {
                State.Alarms.Dequeue();
                btnAlarm.ForeColor = ColorScheme.FG;
            }
        }

    }
}
