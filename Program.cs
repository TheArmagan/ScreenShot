using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShot
{
    class Program
    {
        static void Main(string[] _args)
        {
            string[] args = new string[8] { "", "", "", "", "", "", "", "" };

            for (int i = 0; i < _args.Length; i++)
            {
                args[i] = _args[i];
            }

            string[] supportedList = new string[3] { "jpg", "png", "bmp" };

            if (args[0] == "listAllScreens")
            {

                for (int i = 0; i < Screen.AllScreens.Length; i++)
                {
                    Screen screen = Screen.AllScreens[i];

                    Console.WriteLine($"index={i}|/|deviceName={screen.DeviceName}|/|isPrimary={screen.Primary.ToString().ToLower()}|/|x={screen.Bounds.X}|/|y={screen.Bounds.Y}|/|width={screen.Bounds.Width}|/|height={screen.Bounds.Height}");
                }

            } else if (args[0] == "screenshot")
            {
                int deviceIndex = 0;
                if (!Int32.TryParse(args[1], out deviceIndex)) {
                    Console.WriteLine("ok=false|/|error=Invalid device index. (Invalid number.)");
                    return;
                }

                if (Int32.Parse(args[1]) > Screen.AllScreens.Length-1)
                {
                    Console.WriteLine("ok=false|/|error=Invalid device index. (Out of range.)");
                    return;
                }

                if (!supportedList.Contains(args[2]))
                {
                    Console.WriteLine("ok=false|/|error=Invalid file format expected jpg, png or bmp.");
                    return;
                };

                if (args[3].Length < 1)
                {
                    Console.WriteLine("ok=false|/|error=Invalid file path.");
                    return;
                };

                Bitmap bitmap = GetSreenshot(Screen.AllScreens[deviceIndex]);
                bitmap.Save(args[3], args[2] == "jpg" ? ImageFormat.Jpeg : (args[2] == "png" ? ImageFormat.Png : (args[2] == "bmp" ? ImageFormat.Bmp : ImageFormat.Jpeg)));
                Console.WriteLine("ok=true");
            } else
            {
                Console.WriteLine("Commands: listAllScreens, screenshot;");
                Console.WriteLine("Screenshot command example: screenshot 0 png \"myScrenshot.png\"");
            }

            Application.Exit();

            Bitmap GetSreenshot(Screen screen)
            {
                Bitmap bm = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
                Graphics g = Graphics.FromImage(bm);
                g.CopyFromScreen(0, 0, 0, 0, bm.Size);
                return bm;
            }
        }


    }
}
