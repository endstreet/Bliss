using Bliss.Component;
using System.ComponentModel;

namespace Bliss.Controls
{
    
    public partial class Compass : UserControl
    {

        [Description("Bearing"), Category("Data")]
        public string Bearing { get; set; }
        public Compass()
        {
            InitializeComponent();
            Bearing = "0";
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }
        
        private void timer1_Tick(object? sender, EventArgs e)
        {
            pictureCompass.BackgroundImage = CompassImage.RotateImage(((int)double.Parse(Bearing)) * -1);
            //pictureCompass.BackgroundImageLayout = ImageLayout.None;
            this.Refresh();
        }
    }
}
