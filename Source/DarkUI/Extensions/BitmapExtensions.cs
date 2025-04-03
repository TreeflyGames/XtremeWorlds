using System.Drawing;
using System.Drawing.Imaging;

namespace DarkUI.Extensions
{
    internal static class BitmapExtensions
    {
        /// <summary>
        /// Sets all non-transparent pixels in the bitmap to the specified color.
        /// </summary>
        /// <param name="bitmap">The source bitmap.</param>
        /// <param name="color">The color to apply to non-transparent pixels.</param>
        /// <returns>A new bitmap with the color applied.</returns>
        internal static Bitmap SetColor(this Bitmap bitmap, Color color)
        {
            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap), "Bitmap cannot be null.");

            var newBitmap = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
            switch (bitmap.PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    SetColor32bppArgb(bitmap, newBitmap, color);
                    break;
                case PixelFormat.Format24bppRgb:
                    SetColor24bppRgb(bitmap, newBitmap, color);
                    break;
                default:
                    SetColorDefault(bitmap, newBitmap, color);
                    break;
            }
            return newBitmap;
        }

        private static void SetColor32bppArgb(Bitmap original, Bitmap newBitmap, Color color)
        {
            var originalData = original.LockBits(
                new Rectangle(0, 0, original.Width, original.Height),
                ImageLockMode.ReadOnly,
                original.PixelFormat);
            var newData = newBitmap.LockBits(
                new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                ImageLockMode.WriteOnly,
                newBitmap.PixelFormat);

            int bytesPerPixel = 4; // BGRA
            int height = original.Height;
            int widthInBytes = original.Width * bytesPerPixel;

            unsafe
            {
                byte* originalPtr = (byte*)originalData.Scan0;
                byte* newPtr = (byte*)newData.Scan0;
                for (int y = 0; y < height; y++)
                {
                    byte* originalRow = originalPtr + (y * originalData.Stride);
                    byte* newRow = newPtr + (y * newData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        if (originalRow[x + 3] > 0) // Check alpha
                        {
                            newRow[x] = color.B;
                            newRow[x + 1] = color.G;
                            newRow[x + 2] = color.R;
                            newRow[x + 3] = color.A;
                        }
                    }
                }
            }

            original.UnlockBits(originalData);
            newBitmap.UnlockBits(newData);
        }

        private static void SetColor24bppRgb(Bitmap original, Bitmap newBitmap, Color color)
        {
            var newData = newBitmap.LockBits(
                new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                ImageLockMode.WriteOnly,
                newBitmap.PixelFormat);

            int bytesPerPixel = 3; // BGR
            int height = newBitmap.Height;
            int widthInBytes = newBitmap.Width * bytesPerPixel;

            unsafe
            {
                byte* newPtr = (byte*)newData.Scan0;
                for (int y = 0; y < height; y++)
                {
                    byte* newRow = newPtr + (y * newData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        newRow[x] = color.B;
                        newRow[x + 1] = color.G;
                        newRow[x + 2] = color.R;
                    }
                }
            }

            newBitmap.UnlockBits(newData);
        }

        private static void SetColorDefault(Bitmap original, Bitmap newBitmap, Color color)
        {
            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    var pixel = original.GetPixel(i, j);
                    if (pixel.A > 0)
                        newBitmap.SetPixel(i, j, color);
                }
            }
        }

        /// <summary>
        /// Changes pixels of a specific color to a new color, leaving others unchanged.
        /// </summary>
        /// <param name="bitmap">The source bitmap.</param>
        /// <param name="oldColor">The color to replace.</param>
        /// <param name="newColor">The color to apply.</param>
        /// <returns>A new bitmap with the color changed.</returns>
        internal static Bitmap ChangeColor(this Bitmap bitmap, Color oldColor, Color newColor)
        {
            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap), "Bitmap cannot be null.");

            var newBitmap = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
            switch (bitmap.PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    ChangeColor32bppArgb(bitmap, newBitmap, oldColor, newColor);
                    break;
                case PixelFormat.Format24bppRgb:
                    ChangeColor24bppRgb(bitmap, newBitmap, oldColor, newColor);
                    break;
                default:
                    ChangeColorDefault(bitmap, newBitmap, oldColor, newColor);
                    break;
            }
            return newBitmap;
        }

        private static void ChangeColor32bppArgb(Bitmap original, Bitmap newBitmap, Color oldColor, Color newColor)
        {
            var originalData = original.LockBits(
                new Rectangle(0, 0, original.Width, original.Height),
                ImageLockMode.ReadOnly,
                original.PixelFormat);
            var newData = newBitmap.LockBits(
                new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                ImageLockMode.WriteOnly,
                newBitmap.PixelFormat);

            int bytesPerPixel = 4; // BGRA
            int height = original.Height;
            int widthInBytes = original.Width * bytesPerPixel;
            byte oldB = oldColor.B, oldG = oldColor.G, oldR = oldColor.R, oldA = oldColor.A;
            byte newB = newColor.B, newG = newColor.G, newR = newColor.R, newA = newColor.A;

            unsafe
            {
                byte* originalPtr = (byte*)originalData.Scan0;
                byte* newPtr = (byte*)newData.Scan0;
                for (int y = 0; y < height; y++)
                {
                    byte* originalRow = originalPtr + (y * originalData.Stride);
                    byte* newRow = newPtr + (y * newData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        if (originalRow[x] == oldB && originalRow[x + 1] == oldG &&
                            originalRow[x + 2] == oldR && originalRow[x + 3] == oldA)
                        {
                            newRow[x] = newB;
                            newRow[x + 1] = newG;
                            newRow[x + 2] = newR;
                            newRow[x + 3] = newA;
                        }
                    }
                }
            }

            original.UnlockBits(originalData);
            newBitmap.UnlockBits(newData);
        }

        private static void ChangeColor24bppRgb(Bitmap original, Bitmap newBitmap, Color oldColor, Color newColor)
        {
            if (oldColor.A != 255)
                return; // No match possible since all pixels have implicit A=255

            var originalData = original.LockBits(
                new Rectangle(0, 0, original.Width, original.Height),
                ImageLockMode.ReadOnly,
                original.PixelFormat);
            var newData = newBitmap.LockBits(
                new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                ImageLockMode.WriteOnly,
                newBitmap.PixelFormat);

            int bytesPerPixel = 3; // BGR
            int height = original.Height;
            int widthInBytes = original.Width * bytesPerPixel;
            byte oldB = oldColor.B, oldG = oldColor.G, oldR = oldColor.R;
            byte newB = newColor.B, newG = newColor.G, newR = newColor.R;

            unsafe
            {
                byte* originalPtr = (byte*)originalData.Scan0;
                byte* newPtr = (byte*)newData.Scan0;
                for (int y = 0; y < height; y++)
                {
                    byte* originalRow = originalPtr + (y * originalData.Stride);
                    byte* newRow = newPtr + (y * newData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        if (originalRow[x] == oldB && originalRow[x + 1] == oldG && originalRow[x + 2] == oldR)
                        {
                            newRow[x] = newB;
                            newRow[x + 1] = newG;
                            newRow[x + 2] = newR;
                        }
                    }
                }
            }

            original.UnlockBits(originalData);
            newBitmap.UnlockBits(newData);
        }

        private static void ChangeColorDefault(Bitmap original, Bitmap newBitmap, Color oldColor, Color newColor)
        {
            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    var pixel = original.GetPixel(i, j);
                    if (pixel == oldColor)
                        newBitmap.SetPixel(i, j, newColor);
                }
            }
        }
    }
}
