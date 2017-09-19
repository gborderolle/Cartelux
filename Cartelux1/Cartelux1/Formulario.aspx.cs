using Cartelux1.Global_Objects;
using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cartelux1
{
    public partial class Formulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string form_ID_str = GetURLParam_Decrypted("ID");
                if (!string.IsNullOrWhiteSpace(form_ID_str))
                {
                    BindData(form_ID_str);
                }
                else
                {
                    SetFieldsReadOnly(true);
                }
            }
        }

        private string GetURLParam_Decrypted(string param_request)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(param_request))
            {
                string form_ID_str = Request.QueryString[param_request];
                if (!string.IsNullOrWhiteSpace(form_ID_str))
                {
                    try
                    {
                        form_ID_str = Utilities.Decrypt(form_ID_str);
                        if (!string.IsNullOrWhiteSpace(form_ID_str))
                        {
                            result = form_ID_str;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logs.AddErrorLog("Excepcion. Desencriptando ID del parámetro URL. ERROR:", className, methodName, ex.Message);
                    }
                }
            }
            return result;
        }

        protected void BindData(string form_ID_str)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            if (!string.IsNullOrWhiteSpace(form_ID_str))
            {
                using (carteluxdbEntities context = new carteluxdbEntities())
                {
                    formularios form = (formularios)context.formularios.FirstOrDefault(v => v.Formulario_ID.Equals(form_ID_str));
                    if (form != null)
                    {
                        // Contacto
                        txbNombre.Value = form.Nombre_completo;
                        txbTel.Value = form.Telefono;

                        // Entrega
                        txbDireccion.Value = form.Direccion;
                        txbBarrio.Value = form.Barrio;
                        txbFecha.Value = form.Fecha;
                        //rad.Value = form.Motivo_cod;

                        // Cartel
                        //txbTel.Value = form.Entrega_cod;
                        txbTexto.Value = form.Texto;
                        txbDetalles.Value = form.Detalles;

                        SetFieldsReadOnly(true);
                    }
                }
            }
            else
            {
                Logs.AddErrorLog("Error. FormID no válido. ERROR:", className, methodName, form_ID_str);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
            }
        }

        private void SetFieldsReadOnly(bool value)
        {
            if (value)
            {
                // Contacto
                txbNombre.Attributes.Add("readonly", "readonly");
                txbTel.Attributes.Add("readonly", "readonly");

                // Entrega
                txbDireccion.Attributes.Add("readonly", "readonly");
                txbBarrio.Attributes.Add("readonly", "readonly");
                txbFecha.Attributes.Add("readonly", "readonly");

                // Cartel
                txbTexto.Attributes.Add("readonly", "readonly");
                txbDetalles.Attributes.Add("readonly", "readonly");

                msj_result.Visible = true;
                btnConfirmar.Disabled = true;
            }
            else
            {
                // Contacto
                txbNombre.Attributes.Add("readonly", "false");
                txbTel.Attributes.Add("readonly", "false");

                // Entrega
                txbDireccion.Attributes.Add("readonly", "false");
                txbBarrio.Attributes.Add("readonly", "false");
                txbFecha.Attributes.Add("readonly", "false");

                // Cartel
                txbTexto.Attributes.Add("readonly", "false");
                txbDetalles.Attributes.Add("readonly", "false");

                btnConfirmar.Disabled = false;
            }
        }

        protected void btnConfirmar_ServerClick(object sender, EventArgs e)
        {
            string form_ID_str = GetURLParam_Decrypted("ID");
            if (!string.IsNullOrWhiteSpace(form_ID_str))
            {
                SaveData(form_ID_str);
            }
        }

        private void SaveData(string form_ID_str)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            if (!string.IsNullOrWhiteSpace(form_ID_str))
            {
                using (carteluxdbEntities context = new carteluxdbEntities())
                {
                    formularios form = (formularios)context.formularios.FirstOrDefault(v => v.Formulario_ID.Equals(form_ID_str));
                    form = form != null ? form : new formularios();
                    form.Datetime = DateTime.Now;

                    form.Prospecto_ID = 0;
                    form.Codigo = 0;
                    form.Motivo_ID = 0;

                    // Contacto
                    form.Nombre_completo = txbNombre.Value;
                    form.Telefono = txbTel.Value;

                    // Entrega
                    form.Direccion = txbDireccion.Value;
                    form.Barrio = txbBarrio.Value;
                    form.Fecha = txbFecha.Value;
                    //rad.Value = form.Motivo_cod;

                    // Cartel
                    form.Texto = txbTexto.Value;
                    form.Detalles = txbDetalles.Value;

                    if (string.IsNullOrWhiteSpace(form.Formulario_ID))
                    {
                        form.Formulario_ID = form_ID_str;
                        context.formularios.Add(form);
                        context.Entry(form).State = EntityState.Added;
                    }
                    else
                    {
                        context.Entry(form).State = EntityState.Modified;
                    }

                    try
                    {
                        context.SaveChanges();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('¡Gracias por elegir Cartelux! \nNos comunicaremos a la brevedad.'); </script>", false);
                    }
                    catch (Exception e)
                    {
                        Logs.AddErrorLog("Excepcion. Guardando en la base de datos. ERROR:", className, methodName, e.Message);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
                    }
                }
            }
            else
            {
                Logs.AddErrorLog("Error. FormID no válido. ERROR:", className, methodName, form_ID_str);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SetFieldsReadOnly(false);
        }
    }
}