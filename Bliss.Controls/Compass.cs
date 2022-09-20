using Bliss.Component;
using System.ComponentModel;

namespace Bliss.Controls
{
    
    public partial class Compass : UserControl
    {

        [Description("Title"), Category("Data")]
        public string? Title { get; set; }
        //public int Bearing { get; set; }
        public Compass()
        {
            InitializeComponent();
        }

        public void Update(int Bearing)
        {
            pictureCompass.BackgroundImage = CompassImage.RotateImage(Bearing * -1);
            labelHeading.Text = $"{Title} {Bearing}";
            //pictureCompass.BackgroundImageLayout = ImageLayout.None;
            this.Refresh();
        }
    }
}
