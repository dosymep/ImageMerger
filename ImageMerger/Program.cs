using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMerger {
    internal class Program {
        public static void Main(string[] args) {
            string directory = GetArgumentValue(args, "-d:");
            if(string.IsNullOrEmpty(directory)) {
                throw new ArgumentException("Directory name couldn't be null or empty.");
            }

            ImageData[] images = Directory.EnumerateFiles(directory, "*.jpg")
                .Select(ImageData.LoadImage)
                .ToArray();

            int maxRowCount = images.Select(item => item.RowIndex).Distinct().Count();
            int maxColumnCount = images.Select(item => item.ColumnIndex).Distinct().Count();

            Bitmap bitmap = new Bitmap(maxColumnCount * ImageData.ImageSize, maxRowCount * ImageData.ImageSize);
            using(Graphics graphics = Graphics.FromImage(bitmap)) {
                graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);

                foreach(ImageData imageData in images) {
                    Image image = Image.FromFile(imageData.ImagePath);
                    graphics.DrawImage(image, imageData.GetImageRectangle());
                }
            }

            bitmap.Save("bitmap.bmp");
            Process.Start("bitmap.bmp");
        }

        private static string GetArgumentValue(string[] args, string argName) {
            return args
                .FirstOrDefault(item => item.StartsWith(argName))
                ?.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault();
        }
    }
}
