using System;
using Cartelux1.Models;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using Cartelux1.Global_Objects;

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

        public static void AddUserLog(string message, string object_ID, string userID_str, string username, string IP_client = "")
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            using (CarteluxDB context = new CarteluxDB())
            {
                logs _logs = new logs();
                _logs.Fecha_creado = GlobalVariables.GetCurrentTime();
                _logs.Usuario = username;
                _logs.Descripcion = message;
                _logs.Dato_afectado = object_ID;
                _logs.IP_client = IP_client;

                int userID = 0;
                if (!int.TryParse(userID_str, out userID))
                {
                    userID = 0;
                    AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, userID_str);
                }

                _logs.Usuario_ID = userID;
                context.logs.Add(_logs);
                context.SaveChanges();
            }
        }

        internal static void AddErrorLog(string v, object className, object methodName, string tamano_tipo_ID_str)
        {
            throw new NotImplementedException();
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
