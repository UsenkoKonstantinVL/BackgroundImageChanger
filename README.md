# BackgroundImageChanger
Проект для разработке программы по смене обоев на рабочем столе. This is learning project to create program to change wallpaper on desktop.
# About project
This is learning project. Main goal of developing is to explore mechanism changing desctop wallpaper in windows and developing Windows services.    

# Main components
This project contains four sub project:
* BackgroundAppLib - main library that contains changing wallpaper functions and classes
* BackgroundChangerService - windows service that change wallpaper
* SettingsApp - form app that choose wallpaper type 
* TestApp - terminal app that test BackgroundChangerService algorithm
# Wallpaper changing in windows
Next code change desctop wallpaper.

```c#
 
[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

public static extern int SystemParametersInfo(int uAction, int uParam, IntPtr lpvParam, int fuWinIni);
public const int SPI_SETDESKWALLPAPER = 20;
public const int SPIF_UPDATEINIFILE = 0x1;
public const int SPIF_SENDWININICHANGE = 0x2;

// Where pathToWallpaper is path to image
public void SetWallpaper(String pathToWallpaper)
{
      SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, Marshal.StringToBSTR(pathToWallpaper), SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
}
```
# Wallpaper gallery
I use  [unsplash.com](https://unsplash.com/) as wallpaper gallery. It has simple API for getting image of different sizes and types. I download image used unsplash API and save image in special folder. Then I set a new wallpaper.
# To do
- Explore how to launch service in Windows
