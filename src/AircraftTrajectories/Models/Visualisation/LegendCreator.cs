using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace AircraftTrajectories.Models.Visualisation
{
    public class LegendCreator
    {
        public int max;
        public int min;

        public LegendCreator()
        {
            max = 80;
            min = 65;
        }

        public void OutputLegendImage()
        {
            // specify size bitmap
            Bitmap bitmap = new Bitmap(115, 320);
            // generate gradient bitmap
            GenerateGradient(bitmap);
            // fill in background transparent
            TransparentBackground(bitmap, true);
            // draw boundary values
            DrawText(bitmap, true);

            bitmap.Save("gradientImage.png", ImageFormat.Png);
        }

        public void OutputLegendTitle()
        {
            // specify size bitmap
            Bitmap bitmap = new Bitmap(200, 30);
            // fill in background transparent
            TransparentBackground(bitmap, false);
            // draw boundary values
            DrawText(bitmap, false);

            bitmap.Save("titleImage.png", ImageFormat.Png);
        }

        public void GenerateGradient(Bitmap bmp) {

            using (Graphics graphics = Graphics.FromImage(bmp))
            
            using (LinearGradientBrush brush = new LinearGradientBrush(
                new Rectangle(0, 0, 55, 320),
                Color.FromArgb(200, 255, 53, 20),
                Color.FromArgb(200, 75, 237, 100),
            LinearGradientMode.Vertical))
            {
                graphics.FillRectangle(brush, new Rectangle(0, 0, 55, 320));

            }
        }

        public void TransparentBackground(Bitmap bmp, Boolean gradient)
        {
            using (Graphics graphics2 = Graphics.FromImage(bmp))

            // generate transparent background
            using (Brush brush2 = new SolidBrush(Color.FromArgb(0, 0, 0, 0)))
            {
                Graphics g = Graphics.FromImage(bmp);
                if (gradient)
                {
                    g.FillRectangle(brush2, 75, 0, 58, 320);
                } else
                {
                    g.FillRectangle(brush2, 0, 0, 200, 30);
                }

            }
        }

         public void DrawText(Bitmap bmp, Boolean gradient)
        {
            Graphics g = Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            if (gradient)
            {
                RectangleF rectmin = new RectangleF(57, 300, 58, 50);
                RectangleF rectmax = new RectangleF(57, -5, 58, 50);

                RectangleF rect2 = new RectangleF(57, 71, 48, 50);
                RectangleF rect3 = new RectangleF(57, 148, 48, 50);
                RectangleF rect4 = new RectangleF(57, 224, 48, 50);

                g.DrawString(min.ToString(), new Font("Tahoma", 10), Brushes.White, rectmin);
                g.DrawString(max.ToString(), new Font("Tahoma", 10), Brushes.White, rectmax);

                int x1 = (int)(0.75 * (max - min) + min);
                int x2 = (int)(0.50 * (max - min) + min);
                int x3 = (int)(0.25 * (max - min) + min);

                g.DrawString(x1.ToString(), new Font("Tahoma", 10), Brushes.White, rect2);
                g.DrawString(x2.ToString(), new Font("Tahoma", 10), Brushes.White, rect3);
                g.DrawString(x3.ToString(), new Font("Tahoma", 10), Brushes.White, rect4);

            }
            else
            {
                RectangleF rectitle = new RectangleF(0, 0, 200, 100);
                g.DrawString("Geluidsniveau (dB)", new Font("Tahoma", 12), Brushes.White, rectitle);
                
            }
            g.Flush();
        }

    }
}
