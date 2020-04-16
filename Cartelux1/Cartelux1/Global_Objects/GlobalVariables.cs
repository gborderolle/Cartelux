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
    }
}