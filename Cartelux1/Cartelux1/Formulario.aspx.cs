using Cartelux1.Global_Objects;
using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Cartelux1
{
    public partial class Formulario : System.Web.UI.Page
    {
        #region Events 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string serie_str = GetURLParam_Decrypted("ID");
                string tel_str = GetURLParam("TEL");
                if (!string.IsNullOrWhiteSpace(serie_str) && !string.IsNullOrWhiteSpace(tel_str))
                {
                    BindData(serie_str, tel_str);
                }
                else
                {
                    SetFieldsReadOnly(true);
                }
            }
            else
            {

            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SetFieldsReadOnly(false);
        }

        protected void btnConfirmar_ServerClick(object sender, EventArgs e)
        {
            string form_ID_str = GetURLParam_Decrypted("ID");
            if (!string.IsNullOrWhiteSpace(form_ID_str))
            {
                SaveData(form_ID_str);
            }
        }


        #endregion

        #region General Methods 

        private string GetURLParam_Decrypted(string param_request)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(param_request))
            {
                // Logger variables
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                string serie_str = Request.QueryString[param_request];
                if (!string.IsNullOrWhiteSpace(serie_str))
                {
                    try
                    {
                        serie_str = Utilities.Decrypt(serie_str);
                        if (!string.IsNullOrWhiteSpace(serie_str))
                        {
                            result = serie_str;
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

        private string GetURLParam(string param_request)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(param_request))
            {
                // Logger variables
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                result = Request.QueryString[param_request];
            }
            return result;
        }


        protected void BindData(string serie_str, string tel_str)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            if (!string.IsNullOrWhiteSpace(serie_str) && !string.IsNullOrWhiteSpace(tel_str))
            {
                using (carteluxdbEntities context = new carteluxdbEntities())
                {
                    formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Serie.Equals(serie_str));
                    if (_formulario != null)
                    {
                        // Cliente
                        clientes _cliente = Get_Client(context, _formulario.Cliente_ID);
                        if (_cliente != null)
                        {
                            txbNombre.Value = _cliente.Nombre;
                            txbTel.Value = _cliente.Telefono;
                        }                        

                        // Pedidos
                        List<pedidos> lista_pedidos = (List<pedidos>)context.pedidos.Where(v => v.Formulario_ID == _formulario.Formulario_ID).ToList();
                        if (lista_pedidos != null && lista_pedidos.Count > 0)
                        {
                            foreach (pedidos _pedido in lista_pedidos)
                            {
                                // GET Diseño
                                pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(v => v.Pedido_Diseno_ID.Equals(_pedido.Pedido_Diseno_ID));
                                if (_pedido_diseno != null)
                                {
                                    txbTexto1.Text = _pedido_diseno.Texto;
                                }

                                // GET Entrega
                                pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(v => v.Pedido_Entrega_ID.Equals(_pedido.Pedido_Entrega_ID));
                                if (_pedido_entrega != null)
                                {
                                    txbDireccion.Value = _pedido_entrega.Direccion;
                                    txbFecha.Value = _pedido_entrega.Fecha_entrega;
                                    txbCiudad.Value = _pedido_entrega.Ciudad;

                                    if (_pedido_entrega.Entrega_Tipo_ID > 0)
                                    {
                                        ddlTipoEntrega.DataBind();
                                        ddlTipoEntrega.SelectedIndex = _pedido_entrega.Entrega_Tipo_ID;
                                    }
                                }

                                #region Tipo
                                if (_pedido.Pedido_Tipo_ID > 0)
                                {
                                    //ddlMotivo.DataBind();
                                    //ddlMotivo.SelectedIndex = cartel.Tematica_ID;
                                }
                                #endregion

                                #region Material = Pintado / Impreso
                                if (_pedido.Pedido_Material_ID > 0)
                                {
                                    //ddlMotivo.DataBind();
                                    //ddlMotivo.SelectedIndex = cartel.Tematica_ID;
                                }
                                #endregion

                                #region Tamaño
                                if (_pedido.Pedido_Tamano_ID > 0)
                                {
                                    ddlTamano.DataBind();
                                    ddlTamano.SelectedIndex = _pedido.Pedido_Tamano_ID;
                                }
                                #endregion

                                #region Temática
                                if (_pedido.Pedido_Tematica_ID > 0)
                                {
                                    //ddlMotivo.DataBind();
                                    //ddlMotivo.SelectedIndex = cartel.Tematica_ID;
                                }
                                #endregion

                            }
                        }

                        // Última actualización del pedido
                        lblLastUpdate.InnerText = " " + _formulario.Fecha_update.ToString();

                        SetFieldsReadOnly(true);
                    }
                    else
                    {
                        txbTel.Value = tel_str;
                    }
                }
            }
            else
            {
                Logs.AddErrorLog("Error. FormID no válido. ERROR:", className, methodName, serie_str);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
            }
        }

        private clientes Get_Client(carteluxdbEntities context, int id)
        {
            return (clientes)context.clientes.FirstOrDefault(v => v.Cliente_ID.Equals(id));
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
                txbCiudad.Attributes.Add("readonly", "readonly");
                txbFecha.Attributes.Add("readonly", "readonly");

                // Cartel
                txbTexto1.Attributes.Add("readonly", "readonly");
                //txbDetalles.Attributes.Add("readonly", "readonly");

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
                txbCiudad.Attributes.Add("readonly", "false");
                txbFecha.Attributes.Add("readonly", "false");

                // Cartel
                txbTexto1.Attributes.Add("readonly", "false");
                //txbDetalles.Attributes.Add("readonly", "false");

                btnConfirmar.Disabled = false;
            }
        }

        private void SaveData(string serie_str)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            if (!string.IsNullOrWhiteSpace(serie_str))
            {
                using (carteluxdbEntities context = new carteluxdbEntities())
                {
                    formularios formulario = (formularios)context.formularios.FirstOrDefault(v => v.Serie.Equals(serie_str));
                    formulario = formulario != null ? formulario : new formularios();
                    formulario.Fecha_update = DateTime.Now;

                    // Cliente
                    formulario.Cliente_ID = NEW_Cliente(context);

                    // Nuevo
                    if (string.IsNullOrWhiteSpace(formulario.Serie))
                    {
                        string url_complete = HttpContext.Current.Request.Url.AbsoluteUri;
                        if (!string.IsNullOrWhiteSpace(url_complete))
                        {
                            formulario.URL_completa = url_complete;
                            formulario.URL_short = url_complete; //
                        }

                        formulario.Formulario_ID = 0;
                        formulario.Serie = serie_str;
                        formulario.Fecha_creado = DateTime.Now;
                        context.formularios.Add(formulario);
                    }

                    Guardar_Contexto(context);

                    // Pedidos
                    int formulario_ID = Get_NextFormularioID(context);
                    if (formulario_ID > 0)
                    {
                        NEW_Pedido(context, formulario_ID);
                    }

                    Guardar_Contexto(context);
                }
            }
            else
            {
                Logs.AddErrorLog("Error. FormID no válido. ERROR:", className, methodName, serie_str);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
            }
        }

        private void Guardar_Contexto(carteluxdbEntities context)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            try
            {
                context.SaveChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>confirmacionPedido(); </script>", false);
            }
            catch (Exception e)
            {
                Logs.AddErrorLog("Excepcion. Guardando en la base de datos. ERROR:", className, methodName, e.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
            }
        }

        private void NEW_Pedido(carteluxdbEntities context, int formulario_ID)
        {
            if (formulario_ID > 0)
            {
                pedidos _pedido = new pedidos();
                _pedido.Pedido_ID = 0;

                #region Tipo
                //if (ddlTamano.SelectedIndex > 0)
                //{
                //    pedido.Pedido_Tipo_ID = ddlTamano.SelectedIndex;
                //}
                #endregion

                #region Material = Pintado / Impreso
                //if (ddlTamano.SelectedIndex > 0)
                //{
                //    pedido.Pedido_Material_ID = ddlTamano.SelectedIndex;
                //}
                #endregion

                #region Tamaño
                //if (ddlTamano.SelectedIndex > 0)
                //{
                //    //pedido.Pedido_Tematica_ID = ddlTamano.SelectedIndex;
                //}
                #endregion

                #region Temática
                //if (ddlTamano.SelectedIndex > 0)
                //{
                //    pedido.Pedido_Tamano_ID = ddlTamano.SelectedIndex;
                //}
                #endregion


                #region NEW Diseño
                pedido_disenos _pedido_diseno = new pedido_disenos();
                _pedido_diseno.Pedido_Diseno_ID = 0;

                _pedido_diseno.Texto = txbTexto1.Text;

                context.pedido_disenos.Add(_pedido_diseno);
                #endregion

                #region NEW Entrega
                pedido_entregas _pedido_entrega = new pedido_entregas();
                _pedido_entrega.Pedido_Entrega_ID = 0;

                if (ddlTipoEntrega.SelectedIndex > 0)
                {
                    _pedido_entrega.Entrega_Tipo_ID = ddlTipoEntrega.SelectedIndex;
                }

                _pedido_entrega.Direccion = txbDireccion.Value;
                _pedido_entrega.Fecha_entrega = txbFecha.Value;


                context.pedido_entregas.Add(_pedido_entrega);
                #endregion

                _pedido.Formulario_ID = formulario_ID;

                context.pedidos.Add(_pedido);
            }
        }

        private int NEW_Cliente(carteluxdbEntities context)
        {
            int ret_ID = 0;
            string client_tel = txbTel.Value;
            string client_name = txbNombre.Value;
            if (context != null && !string.IsNullOrWhiteSpace(client_tel) && !string.IsNullOrWhiteSpace(client_name))
            {
                clientes cliente = (clientes)context.clientes.FirstOrDefault(v => v.Telefono.Equals(client_tel));
                cliente = cliente != null ? cliente : new clientes();
                cliente.Fecha_update = DateTime.Now;
                ret_ID = cliente.Cliente_ID;

                if (string.IsNullOrWhiteSpace(cliente.Nombre))
                {
                    // Cliente nuevo 
                    cliente.Cliente_ID = 0;
                    cliente.Nombre = string.Empty;
                    cliente.Telefono = client_tel;
                    cliente.Fecha_creado = DateTime.Now;

                    context.clientes.Add(cliente);
                    Guardar_Contexto(context);

                    ret_ID = Get_NextClientID(context);
                }
                cliente.Nombre = client_name;
            }
            return ret_ID;
        }

        private int Get_NextClientID(carteluxdbEntities context)
        {
            int id = 1;
            clientes cliente = (clientes)context.clientes.OrderByDescending(p => p.Cliente_ID).FirstOrDefault();
            if (cliente != null)
            {
                id = cliente.Cliente_ID;
            }
            return id;
        }

        private int Get_NextFormularioID(carteluxdbEntities context)
        {
            int id = 1;
            pedidos pedido = (pedidos)context.pedidos.OrderByDescending(p => p.Pedido_ID).FirstOrDefault();
            if (pedido != null)
            {
                id = pedido.Pedido_ID;
            }
            return id;
        }

        #endregion

        #region Static Methods 

        #endregion

    }
}