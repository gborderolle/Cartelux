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
                string form_ID_str = Request.QueryString["ID"];
                if (!string.IsNullOrWhiteSpace(form_ID_str))
                {
                    BindData(form_ID_str);
                }
            }
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
                        txbNombre.Value = form.Nombre_completo;
                        txbEdad.Value = form.Edad.ToString();
                        txbTel.Value = form.Telefono;
                        //txbMotivo = form.Motivo_ID;

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
            if(value)
            {
                txbNombre.Attributes.Add("readonly", "readonly");
            txbEdad.Attributes.Add("readonly", "readonly");
            txbTel.Attributes.Add("readonly", "readonly");
            txbMotivo.Attributes.Add("readonly", "readonly");
            txbDetalles.Attributes.Add("readonly", "readonly");
            msj_result.Visible = true;
                btnConfirmar.Disabled = true;
            }
            else
            {
                txbNombre.Attributes.Add("readonly", "false");
                txbEdad.Attributes.Add("readonly", "false");
                txbTel.Attributes.Add("readonly", "false");
                txbMotivo.Attributes.Add("readonly", "false");
                txbDetalles.Attributes.Add("readonly", "false");
                btnConfirmar.Disabled = false;
            }
        }

        protected void btnConfirmar_ServerClick(object sender, EventArgs e)
        {
            string form_ID_str = Request.QueryString["ID"];
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
                    form.Nombre_completo = txbNombre.Value;
                    if (!string.IsNullOrWhiteSpace(txbNombre.Value))
                    {
                        int edad_int = 0;
                        if (!int.TryParse(txbEdad.Value, out edad_int))
                        {
                            edad_int = 0;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, txbEdad.Value);
                        }
                        form.Edad = edad_int;
                    }
                    form.Telefono = txbTel.Value;
                    form.Datetime = DateTime.Now;
                    //
                    form.Prospecto_ID = 0;
                    form.Codigo = 0;
                    form.Motivo_ID = 0;

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