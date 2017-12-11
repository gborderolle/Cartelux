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
                if (!string.IsNullOrWhiteSpace(serie_str))
                {
                    BindData(serie_str);
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

        protected void BindData(string serie_str)
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
                    pedidos pedido = (pedidos)context.pedidos.FirstOrDefault(v => v.Serie.Equals(serie_str));
                    if (pedido != null)
                    {
                        // Cliente
                        clientes cliente = Get_Client(context, pedido.Cliente_ID);
                        if (cliente != null)
                        {
                            txbNombre.Value = cliente.Nombre_completo;
                            txbTel.Value = cliente.Telefono;
                        }

                        // Cartel
                        List<carteles> lista_carteles = (List<carteles>)context.carteles.Where(v => v.Pedido_ID == pedido.Pedido_ID).ToList();
                        if (lista_carteles != null && lista_carteles.Count > 0)
                        {
                            foreach (carteles cartel in lista_carteles)
                            {
                                txbTexto1.Text = cartel.Texto;
                                //txbDetalles.Value = cartel.Detalles;
                                txbDireccion.Value = cartel.Colocacion_direccion;
                                txbCiudad.Value = cartel.CiudadInterior;
                                txbFecha.Value = cartel.Colocacion_fecha;

                                if (cartel.Tamano_ID > 0)
                                {
                                    ddlTamano.DataBind();
                                    ddlTamano.SelectedIndex = cartel.Tamano_ID;
                                }
                                if (cartel.Tematica_ID > 0)
                                {
                                    //ddlMotivo.DataBind();
                                    //ddlMotivo.SelectedIndex = cartel.Tematica_ID;
                                }
                                if (cartel.Entregas_Tipo_ID > 0)
                                {
                                    ddlTipoEntrega.DataBind();
                                    ddlTipoEntrega.SelectedIndex = cartel.Entregas_Tipo_ID;
                                }
                                if (cartel.Entregas_Lugar_ID > 0)
                                {
                                    //ddlLugarEntrega.DataBind();
                                    //ddlLugarEntrega.SelectedIndex = cartel.Entregas_Lugar_ID;
                                }
                            }
                        }

                        // Última actualización del pedido
                        lblLastUpdate.InnerText = " " + pedido.LastUpdate.ToString();

                        SetFieldsReadOnly(true);
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
                    pedidos pedido = (pedidos)context.pedidos.FirstOrDefault(v => v.Serie.Equals(serie_str));
                    pedido = pedido != null ? pedido : new pedidos();
                    pedido.LastUpdate = DateTime.Now;

                    // Cliente
                    pedido.Cliente_ID = Guardar_Cliente(context);                                    

                    // Nuevo
                    if (string.IsNullOrWhiteSpace(pedido.Serie))
                    {
                        string url_complete = HttpContext.Current.Request.Url.AbsoluteUri;
                        if (!string.IsNullOrWhiteSpace(url_complete))
                        {
                            pedido.URL_completa = url_complete;
                        }

                        pedido.Pedido_ID = 0;
                        pedido.Serie = serie_str;
                        pedido.DateCreated = DateTime.Now;
                        context.pedidos.Add(pedido);
                    }

                    Guardar_Contexto(context);

                    // Cartel
                    int pedido_ID = Get_NextPedidoID(context);
                    if (pedido_ID > 0)
                    {
                        Guardar_Cartel(context, pedido_ID);
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

        private void Guardar_Cartel(carteluxdbEntities context, int pedido_ID)
        {
            if (pedido_ID > 0)
            {
                carteles cartel = new carteles();
                cartel.Cartel_ID = 0;
                cartel.Texto = txbTexto1.Text;
                //cartel.Detalles = txbDetalles.Value;
                cartel.Colocacion_fecha = txbFecha.Value;
                cartel.Colocacion_direccion = txbDireccion.Value;
                cartel.CiudadInterior = txbCiudad.Value;

                // Tamaño
                if (ddlTamano.SelectedIndex > 0)
                {
                    cartel.Tamano_ID = ddlTamano.SelectedIndex;
                }

                // Temática
                //if (ddlMotivo.SelectedIndex > 0)
                //{
                //    cartel.Tematica_ID = ddlMotivo.SelectedIndex;
                //}
                cartel.Tematica_ID = 0;

                // Entrega lugar
                //if (ddlLugarEntrega.SelectedIndex > 0)
                //{
                //    cartel.Entregas_Lugar_ID = ddlLugarEntrega.SelectedIndex;
                //}
                cartel.Entregas_Lugar_ID = 0;

                // Entrega tipo
                if (ddlTipoEntrega.SelectedIndex > 0)
                {
                    cartel.Entregas_Tipo_ID = ddlTipoEntrega.SelectedIndex;
                }

                cartel.DateCreated = DateTime.Now;
                cartel.LastUpdate = DateTime.Now;
                cartel.Pedido_ID = pedido_ID;

                context.carteles.Add(cartel);
            }
        }

        private int Guardar_Cliente(carteluxdbEntities context)
        {
            int ret_ID = 0;
            string client_tel = txbTel.Value;
            string client_name = txbNombre.Value;
            if (context != null && !string.IsNullOrWhiteSpace(client_tel) && !string.IsNullOrWhiteSpace(client_name))
            {
                clientes cliente = (clientes)context.clientes.FirstOrDefault(v => v.Telefono.Equals(client_tel));
                cliente = cliente != null ? cliente : new clientes();
                cliente.LastUpdate = DateTime.Now;
                ret_ID = cliente.Cliente_ID;

                if (string.IsNullOrWhiteSpace(cliente.Nombre_completo))
                {
                    // Cliente nuevo 
                    cliente.Cliente_ID = 0;
                    cliente.Nombre_completo = string.Empty;
                    cliente.Telefono = client_tel;
                    cliente.DateCreated = DateTime.Now;

                    context.clientes.Add(cliente);
                    Guardar_Contexto(context);

                    ret_ID = Get_NextClientID(context);
                }
                cliente.Nombre_completo = client_name;
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

        private int Get_NextPedidoID(carteluxdbEntities context)
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