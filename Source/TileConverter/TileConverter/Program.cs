using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.InteropServices;

class Program
{ 
    static void Main(string[] args)
    {
        var cd = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\tiles\\";

        if (!Directory.Exists(cd))
            Directory.CreateDirectory(cd);

        if (!Directory.Exists(path: cd + "Unpacked"))
            Directory.CreateDirectory(path: cd + "Unpacked");

        // file tracking
        var cur_index = 1; // new tilesheet
        var cur_file = 0;  // old tile image

        // container info
        var t_size = 32;
        var t_height = 512 / t_size;
        var t_width = 256 / t_size;

        // render properties
        var dRect = new Rectangle(x: 0, y: 0, width: 0, height: 0);
        var sRect = new Rectangle(x: 0, y: 0, width: 0, height: 0);

        while (true)
        {
            var sheet = new Bitmap(width: t_width * t_size, height: t_height * t_size);
            for (var y = 0; y < t_height; y++)
            {
                for (var x = 0; x < t_width; x++)
                {
                    Console.WriteLine(value: "Ripping Current File: " + cur_file.ToString());

                    // escape when no more files
                    if (!File.Exists(path: cd + cur_file.ToString() + ".bmp"))
                        goto DONE_FILING;

                    // parse the image into our sheet

                    var img = new Bitmap(filename: cd + cur_file.ToString() + ".bmp");
                    using (var g = Graphics.FromImage(image: sheet))
                    {
                        dRect = new Rectangle(x: x * t_size, y: y * t_size, width: t_size, height: t_size);
                        sRect = new Rectangle(x: 0, y: 0, width: t_size, height: t_size);
                        g.DrawImage(image: img, destRect: dRect, srcRect: sRect, srcUnit: GraphicsUnit.Pixel);
                    }

                    // increment our tile index
                    cur_file += 1;
                }
            }

            string imagePath = cd + "Unpacked/" + cur_index.ToString() + ".png";

            // save and increment our tilesheet index
            sheet.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);

            cur_index += 1;
        }

        DONE_FILING:
        return;
    }
}