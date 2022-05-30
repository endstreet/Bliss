using Bliss.Component;
using Bliss.Services;
using SharpDX.DirectInput;
using System.Data;

namespace Bliss
{
    public partial class Dashboard : Form
    {

        private PilotService pilot = new ();
        //private SimulationService pilot = new();
        private JoystickService? Joystick;
        private System.Windows.Forms.Timer GetLocation = new System.Windows.Forms.Timer();

        private string AlarmText = "";
        private string ScrollText = "";

        private readonly DbService dbs;

        public Dashboard(DbService db)
        {
            InitializeComponent();

            blissMap1.ApiKey = AppSettings.Default.GoogleMapApiKey;
            blissMap1.DBFile = AppSettings.Default.DBLocation;

            dbs = db;

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

            if(State.Alarm)
            {
                if (AlarmText != State.ToString())
                {
                    AlarmText = State.ToString();
                    ScrollText = AlarmText.PadRight(130);
                    btnAlarm.ForeColor = ColorScheme.Busy;
                }
                ScrollText = ScrollText.Substring(1, 129) + ScrollText.Substring(0, 1);
                labelAlarms.Text = ScrollText;
            }
            else
            {
                AlarmText = "";
                ScrollText = "";
                labelAlarms.Text = ScrollText;
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
                ///Device unplugged?
            //    joystickActive.BackColor = ColorScheme.BG;
            //}
            //if (joystickActive.BackColor == ColorScheme.BG)
            //{
                //Re-connect
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
            switch(button.Name)
            {
                case "btnAlarm":
                    if (State.Alarm)
                    {
                        State.Alarms.Dequeue();
                        AlarmText = State.ToString();
                        ScrollText = AlarmText.PadRight(130);
                        button.Tag = null;
                        button.ForeColor = ColorScheme.FG;
                    }
                    return;
            }
            button.ForeColor = ColorScheme.Busy;

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void buttonMapO_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Text = button.Text == "map orientation bearing" ? "map orientation north" : "map orientation bearing";
            Info.MapShowBearing = button.Text == "map orientation bearing";
        }
    }
}
