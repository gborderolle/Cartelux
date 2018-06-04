using Cartelux1.Global_Objects;
using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Cartelux1.Helpers;
using System.IO;
using System.Web.Hosting;

namespace Cartelux1
{
    public partial class UniteGallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<string> GetFileNames(string location)
        {
            string[] files = null;
            List<string> files_name = new List<string>();
            if (!string.IsNullOrWhiteSpace(location))
            {
                files = Directory.GetFiles(HostingEnvironment.MapPath(location));
                if (files != null && files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        files_name.Add(Path.GetFileName(file));
                    }
                }
            }
            return files_name;
        }
    }
}