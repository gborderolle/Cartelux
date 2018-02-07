using Cartelux1.Global_Objects;
using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Cartelux1
{
    public partial class Acceso : System.Web.UI.Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            Logout();
        }

        protected void btnSubmit_candidato1_ServerClick(object sender, EventArgs e)
        {
            string username = txbUser1.Value;
            string password = txbPassword1.Value;
            Perform_login(username, password, false);
        }

        #endregion Events

        #region Methods

        private void Logout()
        {
            Session["UserID"] = null;
            Session["UserName"] = null;
        }

        private void Perform_login(string username, string password, bool isPasswordInput_hashed = false)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;
            string exception_message = string.Empty;

            int resultado = 0;
            if (!string.IsNullOrWhiteSpace(username) || !string.IsNullOrWhiteSpace(password))
            {
                using (CarteluxDB context = new CarteluxDB())
                {
                    try
                    {
                        context.Database.Connection.Open();
                        usuarios _usuario = (usuarios)context.usuarios.FirstOrDefault(v => v.Usuario == username && v.Clave == password);
                        if (_usuario != null)
                        {
                            Session["UserID"] = _usuario.Usuario_ID;
                            Session["UserName"] = username;

                            Response.Redirect("Dashboard", false);
                        }
                        else
                        {
                            resultado = 2;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logs.AddErrorLog("Excepcion. Haciendo login. ERROR:", className, methodName, ex.Message);
                        exception_message = ex.Message;
                        resultado = 3;
                    }
                }
            }
            else
            {
                resultado = 1;
            }

            ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowErrorMessage", "ShowErrorMessage('" + resultado + "', '" + exception_message + "');", true);
        }

        #endregion Methods
    }
}