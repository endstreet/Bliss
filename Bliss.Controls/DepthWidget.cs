using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Bliss.Controls
{
    public partial class DepthWidget : UserControl
    {
        Dictionary<int, List<int>> DepthScales = new Dictionary<int, List<int>>();
        Queue<float> depths = new Queue<float>();

        public float Depth { get; set; } = 1;
        public DepthWidget()
        {
            InitializeComponent();
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
            null, pictureDepthChart, new object[] { true });
        }

        private void SetBackground()
        {
            // see the note below about the aspect ratios of the two rectangles!!
            Rectangle targetRect = pictureDepthChart.ClientRectangle;
            Bitmap targetBitmap = new Bitmap(targetRect.Width, targetRect.Height);
            Brush bgBrush = new LinearGradientBrush(targetRect, Color.DodgerBlue, Color.Black, 90f);
            using (Graphics g = Graphics.FromImage(targetBitmap))
            {
                g.FillRectangle(bgBrush, targetRect);
            }
            pictureDepthChart.BackgroundImage = targetBitmap;
        }

        private void SetScale(int scale)
        {
            listDepths.Text = "";
            foreach (int d in DepthScales[scale])
            {
                listDepths.Text = listDepths.Text + d.ToString() + Environment.NewLine;
            }
        }
        public new void Update()
        {
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
            SetScale(newscale);
            //Set the scale factor for the Depthmap
            float factor = (float)pictureDepthChart.Height / (float)DepthScales[newscale].Max();
            if (depths.Count > pictureDepthChart.Width / 4) depths.Dequeue();
            if (pictureDepthChart.BackgroundImage == null) SetBackground();
            Draw(factor);
            labelDepth.Text = $"Depth {Depth} meters";
        }
        private void Draw(float scalefactor)
        {
            //Draw background
            int height = pictureDepthChart.Height;
            int postion = 0;
            Graphics g = pictureDepthChart.CreateGraphics();
            Brush myBrush = new SolidBrush(Color.Brown);
            Brush eraser = new SolidBrush(Color.DodgerBlue);
            foreach (var depth in depths)
            {
                Rectangle bar = new Rectangle(postion, 0, 4, height);
                g.FillRectangle(eraser, bar);
                int scaledDepth = (int)(depth * scalefactor);
                Rectangle rect = new Rectangle(postion, scaledDepth, 4, height - scaledDepth);
                g.FillRectangle(myBrush, rect);
                postion = postion + 4;
            }
            g.Dispose();
            myBrush.Dispose();
        }
    }
}
