using System;
using Cartelux1.Models;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;

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

        public static void AddUserLog(string message, string object_ID, string userID_str, string username)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            using (CarteluxDB context = new CarteluxDB())
            {
                //log new_log = new log();
                //new_log.Fecha = DateTime.Now;
                //new_log.Usuario = username;
                //new_log.Descripcion = message;
                //new_log.Dato = object_ID;

                //int userID = 0;
                //if (!int.TryParse(userID_str, out userID))
                //{
                //    userID = 0;
                //    AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, userID_str);
                //}

                //new_log.Usuario_ID = userID;
                //context.logs.Add(new_log);
                //context.SaveChanges();
            }
        }


    }
}
