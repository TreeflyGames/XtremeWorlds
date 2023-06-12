using System;
using System.Drawing;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string[] files = Directory.GetFiles(@"Z:\Dropbox\Pokemon Blood\Client and Editor\resources\tilesets\autotiles\", "*.png");

        int sheetIndex = 0; // To generate a new output file name for each new sheet
        int x = 0;
        int y = 0;
        int maxRowHeight = 0;

        Bitmap bitmap = new Bitmap(512, 512);
        Graphics graphics = Graphics.FromImage(bitmap);

        foreach (string file in files)
        {
            using (Image image = System.Drawing.Image.FromFile(file))
            {
                // Wrap to the next row if the image will not fit on the current row
                if (x + image.Width > bitmap.Width)
                {
                    x = 0;
                    y += maxRowHeight;
                    maxRowHeight = 0;
                }

                // Start a new sheet if the image will not fit on the current sheet
                if (y + image.Height > bitmap.Height)
                {
                    bitmap.Save($@"Z:\Dropbox\Pokemon Blood\Client and Editor\resources\tilesets\output_{sheetIndex}.png", System.Drawing.Imaging.ImageFormat.Png);

                    // Create a new sheet
                    bitmap.Dispose();
                    bitmap = new Bitmap(1024, 1024);
                    graphics.Dispose();
                    graphics = Graphics.FromImage(bitmap);

                    // Reset variables
                    x = 0;
                    y = 0;
                    maxRowHeight = 0;
                    sheetIndex++;
                }

                graphics.DrawImage(image, new Point(x, y));
                x += image.Width;
                maxRowHeight = Math.Max(maxRowHeight, image.Height);
            }
        }

        // Save the last sheet
        bitmap.Save($@"Z:\Dropbox\Pokemon Blood\Client and Editor\resources\tilesets\output_{sheetIndex}.png", System.Drawing.Imaging.ImageFormat.Png);

        graphics.Dispose();
        bitmap.Dispose();
    }
}
