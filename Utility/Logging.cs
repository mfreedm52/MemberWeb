using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Utility
{
    public static class Logging
    {

        public static void Log(string message, string Application)
        {
            string date = string.Format("{0:MM_dd_yyyy}", DateTime.Now);

            string directory = @"C:\Logs\" + Application;
            string filePath = directory + @"\" + date + "_log.txt";

            message = DateTime.Now.ToLongTimeString() + " : " + message;

            System.IO.StreamWriter sw;
            if (File.Exists(filePath))
            {
                using (sw = System.IO.File.AppendText(filePath))
                {
                    sw.WriteLine(message);
                }

            }
            else
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (sw = System.IO.File.AppendText(filePath))
                {
                    sw.WriteLine(message);
                }

            }

        }

    }
}
