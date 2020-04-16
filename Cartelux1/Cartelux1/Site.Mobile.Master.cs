using System;
using System.IO;
using System.Reflection;

namespace Cartelux1
{
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {
        private string build_date;
        public string Build_date { get { return build_date; } }

        private string _lblUserName;
        public string LblUserName { get { return _lblUserName; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null && Session["UserName"] != null)
            {
                if (!IsPostBack)
                {
                    build_date = GetLinkerTime(Assembly.GetExecutingAssembly()).ToString();
                    _lblUserName = Session["UserName"].ToString();
                }
            }
            else
            {
                Response.Redirect("/Acceso.aspx");
            }
        }

        public static DateTime GetLinkerTime(Assembly assembly, TimeZoneInfo target = null)
        {
            var filePath = assembly.Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;

            var buffer = new byte[2048];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(buffer, 0, 2048);

            var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

            var tz = target ?? TimeZoneInfo.Local;
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

            return localTime;
        }
    }
}