using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundAppLib
{
    public class SiteConstructor
    {
        private String sitePattern1 = "https://source.unsplash.com/{0}/daily";
        private String sitePattern2 = "https://source.unsplash.com/{0}/daily?{1}";
        private String sitePattern3 = "https://source.unsplash.com/{0}/";

        private String wStyle = "";
        private String wSize = "";

        private delegate String MyConstructor(String wallpaperStyle, String wallpaperSize);
        private MyConstructor myConstractor = null;

        public SiteConstructor(String wallpaperStyle, String wallpaperSize)
        {
            wStyle = wallpaperStyle;
            wSize = wallpaperSize;

            if(wStyle == Util.IMAGE_RANDOM)
            {
                myConstractor = ConstructDaily;
            }
            else if(wStyle == Util.IMAGE_ALWAYS_RANDOM)
            {
                myConstractor = ConstructRandom;
            }
            else
            {
                myConstractor = ConstructTyped;
            }
        }

        public String Construct()
        {
            return myConstractor(wStyle, wSize);
        }

        private String ConstructDaily(String wallpaperStyle, String wallpaperSize)
        {
            return String.Format(sitePattern1, wallpaperSize);
        }

        private String ConstructTyped(String wallpaperStyle, String wallpaperSize)
        {
            return String.Format(sitePattern2, wallpaperSize, wallpaperStyle);
        }

        private String ConstructRandom(String wallpaperStyle, String wallpaperSize)
        {
            return String.Format(sitePattern3, wallpaperSize);
        }
    }
}
