using Bliss.Component;
using Bliss.Controls.Properties;
using GMap.NET;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace Bliss.Controls
{
    
    public partial class DepthScanner : UserControl
    {

        [Description("Title"), Category("Data")]
        public string? Title { get; set; }

        Dictionary<int, List<int>> DepthScales = new Dictionary<int, List<int>>();
        Queue<float> depths = new Queue<float>();

        public float Depth { get; set; } = 1;

        static Rectangle textRect = new Rectangle(60, 30, 80, 30);
        static Rectangle targetRect = new Rectangle(10, 98, 200, 60);
        
        Bitmap targetBitmap = Resources.DepthGuage;
        Brush bgBrush = new LinearGradientBrush(targetRect, Color.DodgerBlue, Color.Black, 90f);
        Brush myBrush = new SolidBrush(Color.Brown);
        Brush eraser = new SolidBrush(Color.Transparent);

        public DepthScanner()
        {
            InitializeComponent();

            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepthScanner));

            int scale = 1;
            for (int i = 1; i <= 4; i++)
            {
                List<int> ints = new List<int>();
                for (int j = scale; j <= 10 * scale; j = j + scale)
                {
                    ints.Add(j);
                }
                DepthScales.Add(i, ints);
                scale = scale * 10;
            }
            typeof(PictureBox).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, pictureDepth, new object[] { true });
        }
        //private void SetBackground()
        //{
        //    Rectangle textRect = new Rectangle(60, 30, 80, 30);
        //    Rectangle targetRect = new Rectangle(10, 98, 200, 60);
        //    Bitmap targetBitmap = Resources.DepthGuage;
        //    Brush bgBrush = new LinearGradientBrush(targetRect, Color.DodgerBlue, Color.Black, 90f);
        //    using (Graphics g = Graphics.FromImage(targetBitmap))
        //    {
        //        g.SmoothingMode = SmoothingMode.AntiAlias;
        //        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //        StringFormat sf = new StringFormat();
        //        sf.Alignment = StringAlignment.Center;
        //        sf.LineAlignment = StringAlignment.Center;
        //        g.DrawString($"{Depth.ToString("0.0").Replace(",", ".")} m", new System.Drawing.Font("Tahoma", 18, FontStyle.Bold), Brushes.Black, textRect, sf);

        //        g.FillRectangle(bgBrush, targetRect);
        //    }
        //    //Draw inticator

        //    pictureDepth.BackgroundImage = targetBitmap;
        //}

        private void SetScale(int scale)
        {
            //listDepths.Text = "";
            //foreach (int d in DepthScales[scale])
            //{
            //    listDepths.Text = listDepths.Text + d.ToString() + Environment.NewLine;
            //}
        }
        public new void Update()
        {
            targetBitmap = Resources.DepthGuage;
            depths.Enqueue(Depth);

            int newscale = 1;
            //Find the Scale
            foreach (var scale in DepthScales)
            {
                if (scale.Value.Max() > depths.Max())
                {
                    newscale = scale.Key;
                    break;
                }
            }
            //Set the scale factor for the Depthmap
            
            float factor = (float)targetRect.Height / (float)DepthScales[newscale].Max();
            if (depths.Count > targetRect.Width / 4) depths.Dequeue();

            using (Graphics g = Graphics.FromImage(targetBitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString($"{Depth.ToString("0.0").Replace(",", ".")} m", new System.Drawing.Font("Tahoma", 14, FontStyle.Bold), Brushes.Black, textRect, sf);

                int height = targetRect.Height;
                int postion = targetRect.X;
                //10, 98,

                foreach (var depth in depths)
                {
                    Rectangle bar = new Rectangle(postion, targetRect.Y, 4, height);
                    g.FillRectangle(eraser, bar);
                    int scaledDepth = (int)(depth * factor) + targetRect.Y;
                    Rectangle rect = new Rectangle(postion, scaledDepth, 4, (height+ targetRect.Y) - scaledDepth);
                    g.FillRectangle(myBrush, rect);
                    postion = postion + 4;
                }
            }
            pictureDepth.BackgroundImage = targetBitmap;
        }
        //private void Draw(Rectangle targetRect,float scalefactor)
        //{
        //    //Draw background
        //    int height = targetRect.Height;
        //    int postion = 0;

        //    Brush myBrush = new SolidBrush(Color.Brown);
        //    Brush eraser = new SolidBrush(Color.DodgerBlue);
        //    foreach (var depth in depths)
        //    {
        //        Rectangle bar = new Rectangle(postion, 0, 4, height);
        //        g.FillRectangle(eraser, bar);
        //        int scaledDepth = (int)(depth * scalefactor);
        //        Rectangle rect = new Rectangle(postion, scaledDepth, 4, height - scaledDepth);
        //        g.FillRectangle(myBrush, rect);
        //        postion = postion + 4;
        //    }
        //}
    }
}
