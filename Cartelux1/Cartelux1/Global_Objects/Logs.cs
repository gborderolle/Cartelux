using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Cartelux1.Global_Objects
{
    public static class Logs
    {
        public static void AddErrorLog(string message, string className, string methodName, string obj, [CallerLineNumber] int numberNumber = 0)
        {
            try
            {
                string Path_Data = @"Logs\";
                if (ConfigurationManager.AppSettings != null)
                {
                    Path_Data = ConfigurationManager.AppSettings["Path_Log"].ToString();
                }

                string File_ErrorLog = "error_log.txt";
                if (ConfigurationManager.AppSettings != null)
                {
                    File_ErrorLog = ConfigurationManager.AppSettings["File_ErrorLog"].ToString();
                }

                string path_complete = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path_Data);
                if (!Directory.Exists(Path.GetDirectoryName(path_complete)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path_complete));
                }

                using (StreamWriter writer = new StreamWriter(path_complete + "/" + File_ErrorLog, true))
                {
                    string text = DateTime.Now.ToString() + ": [ln:" + numberNumber + "] " + className + ": " + methodName + "() - " + message + " " + obj + ".";
                    writer.WriteLine(text);
                }
            }
            catch (Exception) { }
        }

    }
}
