using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cartelux1.Pages
{
    public partial class Formulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string form_ID_str = Request.QueryString["ID"];
            if (!string.IsNullOrWhiteSpace(form_ID_str))
            {
                BindData(form_ID_str);
            }
        }

        protected void BindData(string form_ID_str)
        {
            int form_ID = 0;
            if (!int.TryParse(form_ID_str, out form_ID))
            {
                form_ID = 0;
            }
            if (form_ID > 0)
            {
                using (carteluxdbEntities context = new carteluxdbEntities())
                {
                    formularios form = (formularios)context.formularios.FirstOrDefault(v => v.Formulario_ID == form_ID);
                    if (form != null)
                    {
                        txbNombre.Value = form.Nombre_completo;
                        txbEdad.Value = form.Edad.ToString();
                        txbTel.Value = form.Telefono;
                        //txbMotivo = form.Motivo_ID;
                    }
                }
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
            int form_ID = 0;
            if (!int.TryParse(form_ID_str, out form_ID))
            {
                form_ID = 0;
            }
            if (form_ID > 0)
            {
                using (carteluxdbEntities context = new carteluxdbEntities())
                {
                    formularios form = (formularios)context.formularios.FirstOrDefault(v => v.Formulario_ID == form_ID);
                    form = form != null ? form : new formularios();
                    if (form != null)
                    {
                        form.Nombre_completo = txbNombre.Value;
                        int edad_int = 0;
                        if (!int.TryParse(txbEdad.Value, out edad_int))
                        {
                            edad_int = 0;
                        }
                        form.Edad = edad_int;
                        form.Telefono = txbTel.Value;
                        form.Datetime = DateTime.Now;
                        //
                        form.Prospecto_ID = 0;
                        form.Codigo = 0;
                        form.Motivo_ID = 0;

                        if (form.Formulario_ID == 0)
                        {
                            form.Formulario_ID = form_ID;
                            context.formularios.Add(form);
                        }
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}