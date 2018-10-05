using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundAppLib
{
    public class NetworkProcess
    {
       
        public String Load(String wallpaperStyle, String wallpaperSize)
        {
            String pathToImage = "";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            //client.Encoding = Encoding.UTF8;
            String url = (new SiteConstructor(wallpaperStyle, wallpaperSize)).Construct();

            byte[] data = client.DownloadData(url);
            using (MemoryStream mem = new MemoryStream(data))
            {
                using (var yourImage = Image.FromStream(mem))
                {
                    // If you want it as Jpeg
                    yourImage.Save("img.jpg", ImageFormat.Jpeg);
                    pathToImage = "img.jpg";
                }
            }

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathToImage);
        }
    }
}