using Cartelux1.Global_Objects;
using Cartelux1.Models;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace Cartelux1
{
    public partial class GeneradorURL : System.Web.UI.Page
    {
        public static string _Serie
        {
            get { return (string)GetCurrentPageViewState()["_Serie"]; }
            set { GetCurrentPageViewState()["_Serie"] = value; }
        }

        public static string _ClientTEL
        {
            get { return (string)GetCurrentPageViewState()["_ClientTEL"]; }
            set { GetCurrentPageViewState()["_ClientTEL"] = value; }
        }

        public static System.Web.UI.StateBag GetCurrentPageViewState()
        {
            Page page = HttpContext.Current.Handler as Page;
            var viewStateProp = page?.GetType().GetProperty("ViewState", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic);
            return (System.Web.UI.StateBag)viewStateProp?.GetValue(page);
        }

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
        public static string GenerarURL(string txbContactPhone)
        {
            string resultado = string.Empty;
            if (!string.IsNullOrWhiteSpace(txbContactPhone))
            {
                string url = "http://cartelux.uy?ID=";
                if (ConfigurationManager.AppSettings != null)
                {
                    url = ConfigurationManager.AppSettings["URL_formulario"].ToString();
                }

                if (!string.IsNullOrWhiteSpace(url))
                {
                    DateTime date = DateTime.Now;
                    string serie = date.ToString("yyyy-MM-dd-hh-mm-ss", CultureInfo.InvariantCulture).Replace("-", "");
                    _Serie = serie;

                    resultado += serie;

                    string resultado_parcial = Utilities.Encrypt(resultado);
                    if (!string.IsNullOrWhiteSpace(resultado_parcial))
                    {
                        resultado = resultado_parcial;
                    }
                    resultado = url + resultado;

                    // Agrega el número de contacto del cliente al final para usar en el propio form
                    // Si el número NO empieza con 0 lo agrega
                    string first = txbContactPhone.Substring(0, 1);
                    if (!string.IsNullOrWhiteSpace(first) && !first.Equals("0"))
                    {
                        txbContactPhone = "0" + txbContactPhone;
                    }
                    resultado += "&TEL=" + txbContactPhone;
                    _ClientTEL = txbContactPhone;

                    int isLocal = HttpContext.Current.Request.IsLocal ? 1 : 0;
                    resultado += "|" + isLocal;
                }
            }
            return resultado;
        }

        #region Static Methods 

        [WebMethod]
        public static bool CheckFormStatus_1(string txbContactPhone)
        {
            bool ok = false;
            if (!string.IsNullOrWhiteSpace(txbContactPhone))
            {
                ok = CheckFormStatus_2(txbContactPhone); 
            }
            return ok;
        }

        private static bool CheckFormStatus_2(string tel_str)
        {
            bool ok = false;
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            if (!string.IsNullOrWhiteSpace(tel_str))
            {
                using (CarteluxDB context = new CarteluxDB())
                {
                    clientes _cliente = (clientes)context.clientes.FirstOrDefault(v => v.Telefono.Equals(tel_str));
                    if (_cliente != null)
                    {
                        ok = true;
                    }
                }
            }
            return ok;
        }

        #endregion

    }
}