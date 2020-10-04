using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace ImageMerger {
    internal class ImageData {
        public const int ImageSize = 24;

        public static ImageData LoadImage(string imagePath) {
            string imageName = Path.GetFileNameWithoutExtension(imagePath);
            Match match = Regex.Match(imageName, @"^Image\.(?'r'\d)\.(?'c'\d)");

            int rowIndex = match.Groups["r"].Value.ToDefaultInt() - 1;
            int columnIndex = match.Groups["c"].Value.ToDefaultInt() - 1;

            return new ImageData() { ImagePath = imagePath, RowIndex = rowIndex, ColumnIndex = columnIndex };
        }

        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

        public string ImagePath { get; set; }

        public Point GetImageLocation() {
            int x = ColumnIndex * ImageSize;
            int y = RowIndex * ImageSize;

            return new Point(x, y);
        }

        public Size GetImageSize() {
            return new Size(ImageSize, ImageSize);
        }

        public Rectangle GetImageRectangle() {
            return new Rectangle(GetImageLocation(), GetImageSize());
        }

        public override string ToString() {
            return $"Row = {RowIndex}, Column = {ColumnIndex}";
        }
    }
}
