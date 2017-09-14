using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cartelux1.Pages
{
    public partial class GeneradorLink : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GenerarLink(string dummy)
        {
            string resultado = string.Empty;

            string url = "http://cartelux.uy/Pages/Formulario?ID=";
            if (ConfigurationManager.AppSettings != null)
            {
                url = ConfigurationManager.AppSettings["URL_formulario"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                DateTime date = DateTime.Now;
                Random ran = new Random();
                int number = 0;
                for (int i = 0; i < 3; i++)
                {
                    number = ran.Next(0, 9);
                    resultado += number.ToString();
                }
                resultado += date.ToString("yyyy-MM-dd-hh-mm-ss", CultureInfo.InvariantCulture).Replace("-","");
                for (int i = 0; i < 3; i++)
                {
                    number = ran.Next(0, 9);
                    resultado += number.ToString();
                }
                resultado = url + resultado;
            }
            return resultado;
        }

    }
}