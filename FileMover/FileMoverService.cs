using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FileMover
{
    public partial class FileMoverService : ServiceBase
    {

        public static Dictionary<string, string> extensionMap = new Dictionary<string, string>()
        {
            {".pdf", @"C:\Users\mfreedm\Documents"},
            {".mp3", @"C:\Users\mfreedm\Music"},
            {".doc", @"C:\Users\mfreedm\Documents"},
            {".docx", @"C:\Users\mfreedm\Documents"},
            {".xlsx", @"C:\Users\mfreedm\Documents\Excel"},
            {".xls", @"C:\Users\mfreedm\Documents\Excel"},
            {".csv", @"C:\Users\mfreedm\Documents\Excel"},
            {".png", @"C:\Users\mfreedm\Pictures"},
            {".exe", @"C:\Users\mfreedm\Downloads\Applications"},
            {".dll", @"C:\Users\mfreedm\Downloads\Applications"},
           


        };

        private static string otherPath = @"C:\Users\mfreedm\Downloads\Junk";

        public FileMoverService()
        {
            InitializeComponent();
        }
        private static System.Timers.Timer myTimer;

        protected override void OnStart(string[] args)
        {
            //Run every 60 minutes
            myTimer = new System.Timers.Timer(1000 * 60 * 60);

            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            myTimer.Enabled = true;

        }

        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            MikesUtils.Logging.Log("Timer Elapsed. Process Files", "FileMover");
            ProcessFiles();
            
        }

        private static void ProcessFiles()
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\mfreedm\Downloads");

            FileInfo[] allFiles = di.GetFiles();

            foreach (FileInfo file in allFiles)
            {
                string targetPath = "";
                extensionMap.TryGetValue(file.Extension, out targetPath);

                if (targetPath != "" && targetPath != null)
                {
                    MoveFile(file, targetPath);
                }
                else
                {
                    if(DateTime.Now.Subtract(file.CreationTime).Days > 30)
                    {
                        MoveFile(file, otherPath);
                    }

                }
             }

        }

        private static void MoveFile(FileInfo sourceFile, string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            string targetFilePath = targetPath + @"\" + sourceFile.Name;
            if (!File.Exists(targetFilePath))
            {
                try
                {
                    File.Move(sourceFile.FullName, targetPath + @"\" + sourceFile.Name);
                }
                catch (Exception e)
                {
                    MikesUtils.Logging.Log(e.Message.ToString(), "FileMover");
                    //log here but dont crash

                }
            }
            else
            {
                File.Delete(sourceFile.FullName);
            }

        }

        protected override void OnStop()
        {
        }
    }
}
