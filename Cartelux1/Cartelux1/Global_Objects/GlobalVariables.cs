using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Cartelux1.Global_Objects
{
    public class GlobalVariables
    {
        public static string ShortDateTime_format1 = "dd-MM-yyyy";
        public static string ShortDateTime_format2 = "dd/MM/yyyy";
        public static string ShortDateTime_format3 = "MM-dd-yyyy";
        public static string ShortDateTime_format4 = "MM/dd/yyyy";
        public static string ShortDateTime_format1_long = "dd-MM-yyyy HH:mm:ss";

        public enum Agrupacion
        {
            Todas,
            Pasacalle,
            Rollup
        };

        public static DateTime GetDatetimeFormated(string fecha_str)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            DateTime date = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(fecha_str))
            {
                if (!DateTime.TryParseExact(fecha_str, GlobalVariables.ShortDateTime_format1, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    date = DateTime.MinValue;
                    Logs.AddErrorLog("Excepcion. Convirtiendo datetime. ERROR:", className, methodName, fecha_str);
                }
            }
            return date;
        }

        public static DateTime GetCurrentTime()
        {
            DateTime serverTime = DateTime.Now;
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, "Montevideo Standard Time");
            return _localTime;
        }

        public static bool? EsTrader(string user_ID_str)
        {
            bool? ret = false;
            if (!string.IsNullOrWhiteSpace(user_ID_str))
            {
                using (CarteluxDB context = new CarteluxDB())
                {
                    // Logger variables
                    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                    string methodName = stackFrame.GetMethod().Name;

                    int user_ID = 0;
                    if (!int.TryParse(user_ID_str, out user_ID))
                    {
                        user_ID = 0;
                        Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, user_ID_str);
                    }
                    if (user_ID > 0)
                    {
                        usuarios _usuario = (usuarios)context.usuarios.FirstOrDefault(v => v.Usuario_ID == user_ID);
                        if (_usuario != null)
                        {
                            ret = _usuario.EsTrader;
                        }
                    }
                }
            } // using

            return ret;
        }

    }
}