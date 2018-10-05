using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace BackgroundAppLib
{
    public class SettingsUtil
    {
        private const String SECTION = "section";
        private const String KEY_IMAGE_TYPE = "image_type";
        private const String KEY_LAST_TIME = "last_time";
        private static SettingsUtil instance = null;

        private static Object instanceObj = new Object();
        Object obj = new Object();

        private String folderName = "BackgroundApp";
        private String fileName = "Settings.txt";

        private IniSettings iniSettings = null;

        private SettingsUtil()
        {

        }

        public static SettingsUtil GetInstance()
        {
            lock(instanceObj)
            {
                if(instance == null)
                {
                    instance = new SettingsUtil();
                }
                return instance;
            }    
        }

        public String GetImageType()
        {
            lock(obj)
            {
                // Проверка на существование файла
                CheckFileExistingAndCreateIfNot();
                // Считывание второй строки из файла
                return ReturnValueOrSetDefault(KEY_IMAGE_TYPE, Util.IMAGE_DEFAULT);
            }
        }

        public string GetLastTimeUpdate()
        {
            lock (obj)
            {
                // Проверка на существование файла
                CheckFileExistingAndCreateIfNot();
                // Считывание первой строки
                return ReturnValueOrSetDefault(KEY_LAST_TIME, "");
            }
        }

        private String ReturnValueOrSetDefault(String key, String defaultValue)
        {
            if (iniSettings.KeyExists(key, SECTION))
            {
                return iniSettings.ReadINI(SECTION, key);
            }
            else
            {
                iniSettings.Write(SECTION, key, defaultValue);
                return defaultValue;
            }
        }

        public void SetLastTimeToUpdate(String lastTime)
        {
            lock (obj)
            {
                // Проверка на существование файла
                CheckFileExistingAndCreateIfNot();
                // Обновление данных
                iniSettings.Write(SECTION, KEY_LAST_TIME, lastTime);
            }
        }

        public void SetImageType(String imageType)
        {
            lock (obj)
            {
                // Проверка на существование файла
                CheckFileExistingAndCreateIfNot();
                // Обновление данных
                iniSettings.Write(SECTION, KEY_IMAGE_TYPE, imageType);
            }
        }

        public String GetScreenResolution()
        {
            var resolution = Screen.PrimaryScreen.Bounds.Size;

            return String.Format("{0}x{1}",
                resolution.Width,
                resolution.Height
                );
        }

        public void CheckFileExistingAndCreateIfNot()
        {
            string path = "";
            try
            {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folderName);
                if(!System.IO.Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    path = Path.Combine(path, fileName);
                    File.WriteAllText(path, "");
                }
                else
                {
                    path = Path.Combine(path, fileName);
                    if (!File.Exists(path))
                    {
                        File.WriteAllText(path, "");
                    }
                }
                if(iniSettings == null)
                {
                    iniSettings = new IniSettings(path);
                }
            }
            catch(Exception e)
            {
                iniSettings = null;
            }
        }
    }
}
