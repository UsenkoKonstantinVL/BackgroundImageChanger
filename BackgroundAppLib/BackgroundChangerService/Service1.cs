using BackgroundAppLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundChangerService
{
    public partial class Service1 : ServiceBase
    {
        private Thread thread;
        private System.Diagnostics.EventLog eventLog1;

        public Service1()
        {
            InitializeComponent();
            this.ServiceName = "BackgroundImageService";
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart");
            thread = new Thread(new ThreadStart(MainProcess));
            thread.Start();
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
            if (thread != null)
            {
                thread.Abort();
            }
        }

        private void MainProcess()
        {
            eventLog1.WriteEntry("Start proccess");
            //MessageBox.Show(String.Format("Служба запущена!"));
            NetworkProcess network = new NetworkProcess();
            WallpapeSetter wSetter = new WallpapeSetter();
            while (true)
            {
                DateTime date1 = new DateTime();

                bool needToChange = false;
                String dateFormat;
                if ((dateFormat = SettingsUtil.GetInstance().GetLastTimeUpdate()) != "")
                {
                    DateTime dt = DateTime.ParseExact(dateFormat, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture).AddDays(1);
                    DateTime curDate = DateTime.Now;

                    if (curDate.Date >= dt.Date && curDate.Hour > 10)
                    {
                        needToChange = true;
                        SettingsUtil.GetInstance().SetLastTimeToUpdate(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    }
                }
                else
                {
                    needToChange = true;
                    SettingsUtil.GetInstance().SetLastTimeToUpdate(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                }

                if (needToChange)
                {
                    try
                    {
                        String imgType = (SettingsUtil.GetInstance()).GetImageType();
                        String size = SettingsUtil.GetInstance()
                            .GetScreenResolution();
                        String wPath = network.Load(imgType, size);//"1920x1080");
                        wSetter.SetWallpaper(wPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        Console.WriteLine(ex.Message);
                    }
                }
                Thread.Sleep(1000 * 60 * 60);
            }

        }
    }
}
