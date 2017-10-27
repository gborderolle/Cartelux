using Cartelux1.Global_Objects;
using System;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Services;

namespace Cartelux1
{
    public partial class GeneradorURL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Ejemplo: 20170918014644x644
        /// Datetime=20170918014644 + x + Random=644
        /// </summary>
        /// <param name="dummy"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GenerarURL(string dummy)
        {
            string resultado = string.Empty;
            string url = "http://cartelux.uy?ID=";
            if (ConfigurationManager.AppSettings != null)
            {
                url = ConfigurationManager.AppSettings["URL_formulario"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                DateTime date = DateTime.Now;
                resultado += date.ToString("yyyy-MM-dd-hh-mm-ss", CultureInfo.InvariantCulture).Replace("-", "");
                Random ran = new Random();
                resultado += ran.Next(0, 9).ToString();
                
                string resultado_parcial = Utilities.Encrypt(resultado);
                if (!string.IsNullOrWhiteSpace(resultado_parcial))
                {
                    resultado = resultado_parcial;
                }
                resultado = url + resultado;

                int isLocal = HttpContext.Current.Request.IsLocal ? 1 : 0;
                resultado += "|" + isLocal;
            }
            return resultado;
        }        

    }
}