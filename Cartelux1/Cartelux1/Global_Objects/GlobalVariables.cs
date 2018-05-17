using System;
using System.Collections.Generic;
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

        public enum Agrupacion
        {
            Todas,
            Pasacalle,
            Rollup
        };
    }
}