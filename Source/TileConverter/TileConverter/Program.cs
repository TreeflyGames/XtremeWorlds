using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.InteropServices;

class Program
{ 
    static void Main(string[] args)
    {
        var cd = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/tiles/";

        if (!Directory.Exists(cd))
            Directory.CreateDirectory(cd);

        if (!Directory.Exists(path: cd + "Unpacked"))
            Directory.CreateDirectory(path: cd + "Unpacked");

        // file tracking
        var cur_index = 0; // new tilesheet
        var cur_file = 0;  // old tile image

        // container info
        var t_size = 32;
        var t_len = 480 / t_size;

        // render properties
        var dRect = new Rectangle(x: 0, y: 0, width: 0, height: 0);
        var sRect = new Rectangle(x: 0, y: 0, width: 0, height: 0);

        while (true)
        {
            var sheet = new Bitmap(width: 512, height: 512);

            int y;
            for (y = 0; y <= t_len; y++)
            {
                int x;
                for (x = 0; x <= t_len; x++)
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

            // save and increment our tilesheet index
            sheet.Save(cd + "Unpacked/" + cur_index.ToString() + ".bmp");
            cur_index += 1;
        }

        DONE_FILING:
        return;
    }
}