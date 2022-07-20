using Bliss.Component;
using Bliss.Services;
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Elevation.Request;
using GoogleApi.Entities.Maps.Elevation.Response;


namespace Bliss
{
    public partial class NewDash : Form
    {

        private PilotService pilot;
        private CompassService compass;
        //private SimulationService pilot = new();

        private System.Windows.Forms.Timer GetLocation = new System.Windows.Forms.Timer();

        private string AlarmText = "";

        private readonly DbService dbs;

        GoogleMaps api;
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

        public NewDash(CompassService _compass, PilotService _pilot, DbService _db, GoogleMaps _api)
        {
           

            InitializeComponent();

            blissMap1.ApiKey = AppSettings.Default.GoogleMapApiKey;
            blissMap1.DBFile = AppSettings.Default.DBLocation;

            compass = _compass;
            pilot = _pilot;
            dbs = _db;
            api = _api;
            //pilot.OnJoysticCommand("Stop");

            ChangeTheme(this.Controls);

            //Timer
            GetLocation.Tick += DashboardUpdate;
            GetLocation.Interval = AppSettings.Default.DashBoardUpdateInterval * 1000;
            GetLocation.Start();

            pilot.OnInterfaceData += Joystick_OnJoystickData;
            //pilot.OnMotorData += Pilot_OnMotordata;

            //RunTest();
        }

        //private async Task RunTest()
        //{
        //    await Test.RunTest();
        //}
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
                    if (component.Name.Contains("progress")) continue;
                    if (component.Name.Contains("list")) continue;
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
            //pictureCompass.BackColor = Color.White;
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
        private void DashboardUpdate(object? sender, EventArgs e)
        {
            SpeedLbl.Text = Info.Speed.ToString();

            compassMag.Bearing = Info.CompassBearing;
            compassNav.Bearing = Info.Bearing.ToString();
            
            if (Info.HasPosition)
            {
                blissMap1.MainMap_LocationUpdate();
                if (string.IsNullOrEmpty(lblDepth.Text) || lblDepth.Text == "0")
                {
                    GetElevation();
                }
                lblDepth.Text = Math.Round(Info.Depth, 2).ToString();
            }
            //Todo:incomment
            //listPorts.DataSource = pilot.ActivePorts;

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
        private async Task GetElevation()
        {
            try
            {
                var request = new ElevationRequest
                {
                    Key = AppSettings.Default.GoogleMapApiKey,
                    Locations = new[]
                    {
                        new Coordinate(Info.CurrentLocation.Lat, Info.CurrentLocation.Lng)
                    }
                };
                ElevationResponse response = await GoogleMaps.Elevation.QueryAsync(request);
                Info.Depth = response.Results.First().Elevation;
            }
            catch (Exception ex)
            {
                State.Alarms.Enqueue(ex.Message);
            }
        }

        #region Joystick        
        private void Joystick_OnJoystickData(object? sender, EventArgs e)
        {
            if (!Info.JoystickCommands.Any()) return;
            ProcessJoystickCommand(Info.JoystickCommands.Peek());
        }

        private void ProcessJoystickCommand(string input)
        {
            btnLeft.BackColor = input.Contains("Left") ? Color.Red : Color.Transparent;
            btnRight.BackColor = input.Contains("Right") ? Color.Red : Color.Transparent;
            btnSpeedUp.BackColor = input.Contains("Forward") ? Color.Red : Color.Transparent;
            btnSpeedDown.BackColor = input.Contains("Backward") ? Color.Red : Color.Transparent;
            //btnAlarm.BackColor = input.Alarm ? Color.Red : Color.Transparent;
            btnStop.BackColor = input.Contains("Stop") ? Color.Red : Color.Transparent;
            btnCancel.BackColor = input.Contains("Cancel") ? Color.Red : Color.Transparent;
            //pilot.OnJoysticCommand(input);
        }
        //private void JoystickInputTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Joystick.IsConnected)
        //        {
        //            Joystick.Update();
        //        }
        //        else if (XJoystick.IsConnected)
        //        {
        //            XJoystick.Update();
        //        }
        //        ProcessPilotCommands();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (JoystickInputTimer.Enabled)
        //        {
        //            State.Alarms.Enqueue("Joystick unplugged |");
        //        }
        //        ScanJoysticks();
        //    }
        //}
        #endregion

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


        private void Dispose(object sender, FormClosingEventArgs e)
        {
            compass.Dispose();
            pilot.Dispose();
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            if (State.Alarm)//If there are alarms dequeue one
            {
                State.Alarms.Dequeue();
                btnAlarm.ForeColor = ColorScheme.FG;
            }
        }
    }
}