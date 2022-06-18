using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Svg;

namespace Bliss.Component
{
    public class Compass
    {
        //public static Bitmap DrawCompassX(double degree, double pitch, double maxpitch, double tilt, double maxtilt, Size s)
        //{


        //    double maxRadius = s.Width > s.Height ? s.Height / 2 : s.Width / 2;

        //    double sizeMultiplier = maxRadius / 180;
        //    double relativepitch = pitch / maxpitch;
        //    double relativetilt = tilt / maxtilt;

        //    Bitmap result = null;
        //    SolidBrush drawBrushWhite = new SolidBrush(Color.White);
        //    SolidBrush drawBrushRed = new SolidBrush(Color.Red);
        //    SolidBrush drawBrushOrange = new SolidBrush(Color.Red);
        //    SolidBrush drawBrushBlue = new SolidBrush(Color.DodgerBlue);
        //    SolidBrush drawBrushWhiteGrey = new SolidBrush(Color.DodgerBlue);
        //    double outerradius = (((maxRadius - sizeMultiplier * 60) / maxRadius) * maxRadius);
        //    double innerradius = (((maxRadius - sizeMultiplier * 90) / maxRadius) * maxRadius);
        //    double degreeRadius = outerradius + 37 * sizeMultiplier;
        //    double dirRadius = innerradius - 30 * sizeMultiplier;
        //    double TriRadius = outerradius + 20 * sizeMultiplier;
        //    double PitchTiltRadius = innerradius * 0.55;
        //    if (s.Width * s.Height > 0)
        //    {
        //        result = new Bitmap(s.Width, s.Height);
        //        using (Font font2 = new Font("Copperplate Gothic Bold", (float)(17 * sizeMultiplier)))
        //        {
        //            using (Font font1 = new Font("Copperplate Gothic Bold", (float)(14 * sizeMultiplier)))
        //            {
        //                using (Pen penblue = new Pen(Color.DodgerBlue, ((int)(sizeMultiplier) < 4 ? 4 : (int)(sizeMultiplier))))
        //                {
        //                    using (Pen penorange = new Pen(Color.Red, ((int)(sizeMultiplier) < 1 ? 1 : (int)(sizeMultiplier))))
        //                    {
        //                        using (Pen penred = new Pen(Color.Red, ((int)(sizeMultiplier) < 1 ? 1 : (int)(sizeMultiplier))))
        //                        {

        //                            using (Pen pen1 = new Pen(Color.FromArgb(255, 255, 255), (int)(sizeMultiplier * 4)))
        //                            {

        //                                using (Pen pen2 = new Pen(Color.FromArgb(255, 255, 255), ((int)(sizeMultiplier) < 1 ? 1 : (int)(sizeMultiplier))))
        //                                {
        //                                    using (Pen pen3 = new Pen(Color.FromArgb(0, 255, 255, 255), ((int)(sizeMultiplier) < 1 ? 1 : (int)(sizeMultiplier))))
        //                                    {
        //                                        using (Graphics g = Graphics.FromImage(result))
        //                                        {


        //                                            // Calculate some image information.
        //                                            double sourcewidth = s.Width;
        //                                            double sourceheight = s.Height;

        //                                            int xcenterpoint = (int)(s.Width / 2);
        //                                            int ycenterpoint = (int)((s.Height / 2));// maxRadius; //TODO: 

        //                                            Point pA1 = new Point(xcenterpoint, ycenterpoint - (int)(sizeMultiplier * 45));
        //                                            Point pB1 = new Point(xcenterpoint - (int)(sizeMultiplier * 7), ycenterpoint - (int)(sizeMultiplier * 45));
        //                                            Point pC1 = new Point(xcenterpoint, ycenterpoint - (int)(sizeMultiplier * 90));
        //                                            Point pB2 = new Point(xcenterpoint + (int)(sizeMultiplier * 7), ycenterpoint - (int)(sizeMultiplier * 45));

        //                                            Point[] a2 = new Point[] { pA1, pB1, pC1 };
        //                                            Point[] a3 = new Point[] { pA1, pB2, pC1 };


        //                                            g.DrawPolygon(penred, a2);
        //                                            g.FillPolygon(drawBrushRed, a2);
        //                                            g.DrawPolygon(penred, a3);
        //                                            g.FillPolygon(drawBrushWhite, a3);


        //                                            double[] Cos = new double[360];
        //                                            double[] Sin = new double[360];

        //                                            ////draw centercross
        //                                            //g.DrawLine(pen2, new Point(((int)(xcenterpoint - (PitchTiltRadius - sizeMultiplier * 50))), ycenterpoint), new Point(((int)(xcenterpoint + (PitchTiltRadius - sizeMultiplier * 50))), ycenterpoint));
        //                                            //g.DrawLine(pen2, new Point(xcenterpoint, (int)(ycenterpoint - (PitchTiltRadius - sizeMultiplier * 50))), new Point(xcenterpoint, ((int)(ycenterpoint + (PitchTiltRadius - sizeMultiplier * 50)))));


        //                                            //draw pitchtiltcross
        //                                            //Point PitchTiltCenter = new Point((int)(xcenterpoint + PitchTiltRadius * relativetilt), (int)(ycenterpoint - PitchTiltRadius * relativepitch));
        //                                            //int rad = (int)(sizeMultiplier * 8);
        //                                            //int rad2 = (int)(sizeMultiplier * 25);

        //                                            //Rectangle r = new Rectangle((int)(PitchTiltCenter.X - rad2), (int)(PitchTiltCenter.Y - rad2), (int)(rad2 * 2), (int)(rad2 * 2));
        //                                            //g.DrawEllipse(pen3, r);
        //                                            //g.FillEllipse(drawBrushWhiteGrey, r);
        //                                            //g.DrawLine(penorange, PitchTiltCenter.X - rad, PitchTiltCenter.Y, PitchTiltCenter.X + rad, PitchTiltCenter.Y);
        //                                            //g.DrawLine(penorange, PitchTiltCenter.X, PitchTiltCenter.Y - rad, PitchTiltCenter.X, PitchTiltCenter.Y + rad);


        //                                            //prep here because need before and after for red triangle.
        //                                            for (int d = 0; d < 360; d++)
        //                                            {
        //                                                //   map[y] = new long[src.Width];
        //                                                double angleInRadians = ((((double)d) + 270d) - degree) / 180F * Math.PI;
        //                                                Cos[d] = Math.Cos(angleInRadians);
        //                                                Sin[d] = Math.Sin(angleInRadians);
        //                                            }


        //                                            for (int d = 0; d < 360; d++)
        //                                            {



        //                                                Point p1 = new Point((int)(outerradius * Cos[d]) + xcenterpoint, (int)(outerradius * Sin[d]) + ycenterpoint);
        //                                                Point p2 = new Point((int)(innerradius * Cos[d]) + xcenterpoint, (int)(innerradius * Sin[d]) + ycenterpoint);

        //                                                //Draw Degree labels
        //                                                if (d % 30 == 0)
        //                                                {
        //                                                    //line every 30 deg
        //                                                    g.DrawLine(penblue, p1, p2);

        //                                                    //Point p3 = new Point((int)(degreeRadius * Cos[d]) + xcenterpoint, (int)(degreeRadius * Sin[d]) + ycenterpoint);
        //                                                    //SizeF s1 = g.MeasureString(d.ToString(), font1);
        //                                                    //p3.X = p3.X - (int)(s1.Width / 2);
        //                                                    //p3.Y = p3.Y - (int)(s1.Height / 2);

        //                                                    //g.DrawString(d.ToString(), font1, drawBrushWhite, p3);
        //                                                    //Point pA = new Point((int)(TriRadius * Cos[d]) + xcenterpoint, (int)(TriRadius * Sin[d]) + ycenterpoint);

        //                                                    int width = (int)(sizeMultiplier * 3);
        //                                                    int dp = d + width > 359 ? d + width - 360 : d + width;
        //                                                    int dm = d - width < 0 ? d - width + 360 : d - width;

        //                                                    Point pB = new Point((int)((TriRadius - (15 * sizeMultiplier)) * Cos[dm]) + xcenterpoint, (int)((TriRadius - (15 * sizeMultiplier)) * Sin[dm]) + ycenterpoint);
        //                                                    Point pC = new Point((int)((TriRadius - (15 * sizeMultiplier)) * Cos[dp]) + xcenterpoint, (int)((TriRadius - (15 * sizeMultiplier)) * Sin[dp]) + ycenterpoint);

        //                                                    Pen p = penblue;
        //                                                    Brush b = drawBrushBlue;
        //                                                    if (d == 0)
        //                                                    {
        //                                                        p = penred;
        //                                                        b = drawBrushRed;
        //                                                    }
        //                                                    //Point[] a = new Point[] { pA, pB, pC };

        //                                                    //g.DrawPolygon(p, a);
        //                                                    //g.FillPolygon(b, a);
        //                                                }
        //                                                //else if (d % 2 == 0)
        //                                                //    g.DrawLine(pen2, p1, p2);

        //                                                //draw N,E,S,W
        //                                                if (d % 90 == 0)
        //                                                {
        //                                                    string dir = (d == 0 ? "N" : (d == 90 ? "E" : (d == 180 ? "S" : "W")));
        //                                                    Point p4 = new Point((int)(dirRadius * Cos[d]) + xcenterpoint, (int)(dirRadius * Sin[d]) + ycenterpoint);
        //                                                    SizeF s2 = g.MeasureString(dir, font1);
        //                                                    p4.X = p4.X - (int)(s2.Width / 2);
        //                                                    p4.Y = p4.Y - (int)(s2.Height / 2);


        //                                                    g.DrawString(dir, font1, d == 0 ? drawBrushRed : drawBrushBlue, p4);

        //                                                    //}
        //                                                    ////Draw red triangle at 0 degrees 
        //                                                    //if (d == 0)
        //                                                    //{


        //                                                }

        //                                            }
        //                                            //draw course

        //                                            //g.DrawLine(pen1, new Point(xcenterpoint, ycenterpoint - (int)innerradius), new Point(xcenterpoint, ycenterpoint - ((int)outerradius + (int)(sizeMultiplier * 50))));
        //                                            String deg = Math.Round(degree, 1).ToString("0.0") + "°";
        //                                            SizeF s3 = g.MeasureString(deg, font1);
        //                                            //Draw degrees in center
        //                                            g.DrawString(deg, font2, drawBrushOrange, new Point(xcenterpoint - (int)(s3.Width / 2), ycenterpoint - (int)(sizeMultiplier * 20)));// 

        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}


        static string background = System.IO.File.ReadAllText(@"D:\FollowTheSun\AutoPilot\Bliss.Shared\Component\CompassBack.svg");

        static string lastbearing;
        static Bitmap bitmap;
        public static Bitmap Needle;
        //.Replace("[Bearing]", bearing.ToString());
        public Compass()
        {

        }
        //transform="translate(-2.4000511,-0.26726913) rotate([Bearing])">
        //transform="translate(-4.3544497,-2.2826445)">
        //Rotate
        //The rotate(<a> [<x> <y>]) transform function specifies a rotation by a degrees about a given point.If optional parameters x and y are not supplied, the rotation is about the origin of the current user coordinate system.If optional parameters x and y are supplied, the rotation is about the point (x, y).
        //transform= "rotate(100)"
        public static Bitmap DrawCompass(string bearing)
        {
            if (bearing == lastbearing && bitmap is not null) return bitmap;
            lastbearing = bearing;
            //var newcontent = background.Replace("[Bearing]", $"{bearing} 31 28.5");
            //var byteArray = Encoding.ASCII.GetBytes(background.Replace("[Bearing]", $"{bearing} 31 28.5"));
            using (Stream stream = new MemoryStream(Encoding.ASCII.GetBytes(background.Replace("[Bearing]", $"{bearing} 31 28.5"))))
            {
                var svgDocument = SvgDocument.Open<SvgDocument>(stream);
                bitmap = svgDocument.Draw();
            }
            //if(Needle is null)
            //{
            //    byteArray = Encoding.ASCII.GetBytes(foreground);
            //    using (Stream stream = new MemoryStream(byteArray))
            //    {
            //        var svgDocument = SvgDocument.Open<SvgDocument>(stream);
            //        Needle = svgDocument.Draw();
            //         //.Save(path, ImageFormat.Png);
            //    }
            //}
            return bitmap;
        }
    }
}
