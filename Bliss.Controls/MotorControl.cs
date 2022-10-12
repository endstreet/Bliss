
using Bliss.Services;
using System.ComponentModel;

namespace Bliss.Controls
{
    public partial class MotorControl : UserControl
    {
        public bool Reverse { get; set; }
        [Description("Motor ID"), Category("Motor")]
        public string MotorId { get; set; }


        public MotorControl()
        {
            InitializeComponent();

            timer1.Interval = 50;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object? sender, EventArgs e)
        {
            switch (MotorId)
            {
                case "MOTOR01":
                    labelDirection.Text = Info.LeftReverseState ? "Reverse" : "Forward";
                    labelRPMValue.Text = (Info.PowerLeftState * 32).ToString();
                    progresstPower.Value = Info.PowerLeftState;
                    break;
                case "MOTOR02":
                    labelDirection.Text = Info.RightReverseState ? "Reverse" : "Forward";
                    labelRPMValue.Text = (Info.PowerRightState * 32).ToString();
                    progresstPower.Value = Info.PowerRightState;
                    break;

            }
        }

    }
}
