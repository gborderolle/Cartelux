using Cartelux1.Global_Objects;
using Cartelux1.Helpers;
using Cartelux1.Models;
using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cartelux1
{
    public partial class Formulario : System.Web.UI.Page
    {
        #region Events 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind_DataConfig();

                string serie_str = GetURLParam_Decrypted("ID");
                string tel_str = GetURLParam("TEL");
                if (!string.IsNullOrWhiteSpace(serie_str) && !string.IsNullOrWhiteSpace(tel_str))
                {
                    BindData(serie_str, tel_str);
                }
                else
                {
                    //SetFieldsReadOnly(true);
                }
            }
            else
            {

            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //SetFieldsReadOnly(false);
        }

        protected void btnConfirmar_ServerClick(object sender, EventArgs e)
        {
            string form_ID_str = GetURLParam_Decrypted("ID");
            string tel_str = GetURLParam("TEL");
            if (!string.IsNullOrWhiteSpace(form_ID_str) && !string.IsNullOrWhiteSpace(tel_str))
            {
                SaveData(form_ID_str, tel_str);
            }
        }

        #endregion

        #region General Methods 

        private void Bind_DataConfig()
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            using (CarteluxDB context = new CarteluxDB())
            {
                // DDL Types
                DataTable dt1 = new DataTable();
                //dt1 = Extras.ToDataTable(context.lista_pedido_tipos.OrderBy(e => e.Nombre).ToList());
                //ddlTipoCartel.DataSource = dt1;
                //ddlTipoCartel.DataTextField = "Nombre";
                //ddlTipoCartel.DataValueField = "Codigo";
                //ddlTipoCartel.DataBind();
                //ddlTipoCartel.Items.Insert(0, new ListItem("Tipo de producto", "0"));

                // DDL Sizes
                dt1 = new DataTable();
                dt1 = Extras.ToDataTable(context.lista_pedido_tamanos.OrderBy(e => e.Nombre).ToList());
                ddlTamano1.DataSource = dt1;
                ddlTamano1.DataTextField = "Nombre";
                ddlTamano1.DataValueField = "Codigo";
                ddlTamano1.DataBind();
                ddlTamano1.Items.Insert(0, new ListItem("Tamaño", "0"));

                // DDL Deliveries
                dt1 = new DataTable();
                dt1 = Extras.ToDataTable(context.lista_entregas_tipos.OrderBy(e => e.Nombre).ToList());
                ddlTipoEntrega1.DataSource = dt1;
                ddlTipoEntrega1.DataTextField = "Nombre";
                ddlTipoEntrega1.DataValueField = "Codigo";
                ddlTipoEntrega1.DataBind();
                ddlTipoEntrega1.Items.Insert(0, new ListItem("Tipo de entrega", "0"));
            }
        }

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

                string param_request_str = Request.QueryString[param_request];
                if (!string.IsNullOrWhiteSpace(param_request_str))
                {
                    result = GetQueryStringDecrypt(param_request_str);
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
                using (CarteluxDB context = new CarteluxDB())
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
                                hdnPedidoCantidad.Value = _pedido.Cantidad.ToString();

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
                                    txbCX_dir.Value = _pedido_entrega.Direccion;
                                    txbDireccion.Value = _pedido_entrega.Direccion;

                                    string sqlFormattedDate = _pedido_entrega.Fecha_entrega.HasValue ? _pedido_entrega.Fecha_entrega.Value.ToString(GlobalVariables.ShortDateTime_format1) : "null";
                                    txbFecha.Value = sqlFormattedDate;

                                    txbCiudad.Value = _pedido_entrega.Ciudad;

                                    hdnCurrentLAT.Value = _pedido_entrega.Coordenadas_X;
                                    hdnCurrentLNG.Value = _pedido_entrega.Coordenadas_Y;
                                    hdnCurrentLocationURL.Value = _pedido_entrega.Google_maps_URL;

                                    if (_pedido_entrega.Entrega_Tipo_ID > 0)
                                    {
                                        lista_entregas_tipos _lista_entregas_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(v => v.Codigo.Equals(_pedido_entrega.Entrega_Tipo_ID));
                                        if (_lista_entregas_tipo != null)
                                        {
                                            ddlTipoEntrega1.SelectedValue = _lista_entregas_tipo.Codigo.ToString();
                                        }
                                    }
                                }

                                #region Tipo
                                //if (_pedido.Pedido_Tipo_ID > 0)
                                //{
                                //    lista_pedido_tipos _lista_pedido_tipo = (lista_pedido_tipos)context.lista_pedido_tipos.FirstOrDefault(v => v.Codigo.Equals(_pedido.Pedido_Tipo_ID));
                                //    if (_lista_pedido_tipo != null)
                                //    {
                                //        ddlTipoCartel.SelectedValue = _lista_pedido_tipo.Codigo.ToString();
                                //    }
                                //}
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
                                    lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(v => v.Codigo.Equals(_pedido.Pedido_Tamano_ID));
                                    if (_lista_pedido_tamano != null)
                                    {
                                        ddlTamano1.SelectedValue = _lista_pedido_tamano.Codigo.ToString();
                                    }
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

                        //SetFieldsReadOnly(true);
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

        private clientes Get_Client(CarteluxDB context, int id)
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
                btnConfirmar1.Enabled = false;
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

                //btnConfirmar.Disabled = false;
                btnConfirmar1.Enabled = true;
            }
        }

        private void SaveData(string serie_str, string tel_str)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            bool save_ok = true;
            if (!string.IsNullOrWhiteSpace(serie_str) && !string.IsNullOrWhiteSpace(tel_str))
            {
                using (CarteluxDB context = new CarteluxDB())
                {
                    formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Serie.Equals(serie_str));
                    _formulario = _formulario != null ? _formulario : new formularios();
                    _formulario.Fecha_update = DateTime.Now;

                    // Cliente
                    _formulario.Cliente_ID = NEW_Cliente(context);

                    // Nuevo formulario
                    if (string.IsNullOrWhiteSpace(_formulario.Serie))
                    {
                        string url_complete = HttpContext.Current.Request.Url.AbsoluteUri;
                        if (!string.IsNullOrWhiteSpace(url_complete))
                        {
                            _formulario.URL_completa = url_complete;
                            _formulario.URL_short = url_complete; //
                        }

                        _formulario.Formulario_ID = 0;
                        _formulario.Serie = serie_str;
                        _formulario.Fecha_creado = DateTime.Now;
                        context.formularios.Add(_formulario);

                        save_ok = Guardar_Contexto(context);

                        // Nuevo pedido
                        int formulario_ID = Get_NextFormularioID(context);
                        if (formulario_ID > 0)
                        {
                            NEW_Pedido(context, formulario_ID);
                        }
                    }
                    else
                    {
                        // Pedido ya existe
                        // ISSUE a resolver cuando efectivamente los Formularios tengan muchos Pedidos
                        pedidos _pedido = (pedidos)context.pedidos.FirstOrDefault(v => v.Formulario_ID.Equals(_formulario.Formulario_ID));
                        if (_pedido != null)
                        {
                            // Update cantidad de Pedidos
                            int cantidad = _pedido.Cantidad;
                            if (!string.IsNullOrWhiteSpace(hdnPedidoCantidad.Value))
                            {
                                if (!int.TryParse(hdnPedidoCantidad.Value, out cantidad))
                                {
                                    cantidad = _pedido.Cantidad;
                                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, hdnPedidoCantidad.Value);
                                }
                            }
                            _pedido.Cantidad = cantidad;

                            // Updates

                            #region Tipo = Pasacalle / Roll up / etc
                            //if (ddlTipoCartel.SelectedIndex > 0)
                            //{
                            //    int selected = 1;
                            //    if (!int.TryParse(ddlTipoCartel.SelectedValue, out selected))
                            //    {
                            //        selected = 1;
                            //        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTipoCartel.SelectedValue);
                            //    }
                            //    _pedido.Pedido_Tipo_ID = selected;
                            //}
                            _pedido.Pedido_Tipo_ID = 1;
                            #endregion

                            #region Material = Pintado / Impreso
                            //if (ddlTamano.SelectedIndex > 0)
                            //{
                            //    pedido.Pedido_Material_ID = ddlTamano.SelectedIndex;
                            //}
                            #endregion

                            #region UPDATE Tamaño
                            if (ddlTamano1.SelectedIndex > 0)
                            {
                                int selected = 1;
                                if (!int.TryParse(ddlTamano1.SelectedValue, out selected))
                                {
                                    selected = 1;
                                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTamano1.SelectedValue);
                                }
                                _pedido.Pedido_Tamano_ID = selected;
                            }
                            #endregion

                            #region Temática
                            //if (ddlTamano.SelectedIndex > 0)
                            //{
                            //    pedido.Pedido_Tamano_ID = ddlTamano.SelectedIndex;
                            //}
                            #endregion

                            #region UPDATE Diseño
                            pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(v => v.Pedido_Diseno_ID.Equals(_pedido.Pedido_Diseno_ID));
                            if (_pedido_diseno != null)
                            {
                                _pedido_diseno.Texto = txbTexto1.Text;
                            }
                            #endregion

                            #region UPDATE Entrega
                            pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(v => v.Pedido_Entrega_ID.Equals(_pedido.Pedido_Entrega_ID));
                            if (_pedido_entrega != null)
                            {
                                _pedido_entrega.Direccion = txbDireccion.Value;

                                _pedido_entrega.Fecha_entrega = GetDatetimeFormated(txbFecha.Value);

                                if (ddlTipoEntrega1.SelectedIndex > 0)
                                {
                                    int selected = 1;
                                    if (!int.TryParse(ddlTipoEntrega1.SelectedValue, out selected))
                                    {
                                        selected = 1;
                                        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTipoEntrega1.SelectedValue);
                                    }
                                    _pedido_entrega.Entrega_Tipo_ID = selected;
                                }

                                // Save current LAT and LNG
                                if (!string.IsNullOrWhiteSpace(hdnCurrentLAT.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLNG.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLocationURL.Value))
                                {
                                    _pedido_entrega.Coordenadas_X = hdnCurrentLAT.Value;
                                    _pedido_entrega.Coordenadas_Y = hdnCurrentLNG.Value;
                                    _pedido_entrega.Google_maps_URL = hdnCurrentLocationURL.Value;
                                }
                            }
                            #endregion
                        }

                    }

                    save_ok = Guardar_Contexto(context);
                    if (save_ok)
                    {
                        if (MyFileUpload.PostedFile != null && !string.IsNullOrWhiteSpace(MyFileUpload.PostedFile.FileName))
                        {
                            save_ok = UploadMegaAPI(_formulario, tel_str);
                        }
                    }

                    if (save_ok)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>confirmacionPedido(); apply_savedStyle(); </script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
                    }
                }
            }
            else
            {
                Logs.AddErrorLog("Error. FormID no válido. ERROR:", className, methodName, serie_str);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
            }
        }

        private DateTime GetDatetimeFormated(string fecha_str)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            DateTime date = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(fecha_str))
            {
                if (!DateTime.TryParseExact(fecha_str, GlobalVariables.ShortDateTime_format1, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    date = DateTime.MinValue;
                    Logs.AddErrorLog("Excepcion. Convirtiendo datetime. ERROR:", className, methodName, fecha_str);
                }
            }
            return date;
        }

        private bool Guardar_Contexto(CarteluxDB context)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            bool save_ok = true;
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Logs.AddErrorLog("Excepcion. Guardando en la base de datos. ERROR:", className, methodName, e.Message);
                save_ok = false;
            }
            return save_ok;
        }

        private void NEW_Pedido(CarteluxDB context, int formulario_ID)
        {
            if (formulario_ID > 0)
            {
                // Logger variables
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                pedidos _pedido = new pedidos();
                _pedido.Pedido_ID = 0;

                int cantidad = 1;
                if (!string.IsNullOrWhiteSpace(hdnPedidoCantidad.Value))
                {
                    if (!int.TryParse(hdnPedidoCantidad.Value, out cantidad))
                    {
                        cantidad = 1;
                        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, hdnPedidoCantidad.Value);
                    }
                }
                _pedido.Cantidad = cantidad;

                #region Tipo cartel = Pasacalle / Roll up
                _pedido.Pedido_Tipo_ID = 1;
                //if (ddlTipoCartel.SelectedIndex > 0)
                //{
                //    int selected = 1;
                //    if (!int.TryParse(ddlTipoCartel.SelectedValue, out selected))
                //    {
                //        selected = 1;
                //        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTipoCartel.SelectedValue);
                //    }
                //    _pedido.Pedido_Tipo_ID = selected;
                //}
                #endregion

                #region Material = Pintado / Impreso
                //if (ddlTamano.SelectedIndex > 0)
                //{
                //    pedido.Pedido_Material_ID = ddlTamano.SelectedIndex;
                //}
                _pedido.Pedido_Material_ID = 0;
                #endregion

                #region Tamaño
                _pedido.Pedido_Tamano_ID = 0;
                if (ddlTamano1.SelectedIndex > 0)
                {
                    int selected = 1;
                    if (!int.TryParse(ddlTamano1.SelectedValue, out selected))
                    {
                        selected = 1;
                        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTamano1.SelectedValue);
                    }
                    _pedido.Pedido_Tamano_ID = selected;
                }
                #endregion

                #region Temática
                //if (ddlTamano.SelectedIndex > 0)
                //{
                //    pedido.Pedido_Tamano_ID = ddlTamano.SelectedIndex;
                //}
                _pedido.Pedido_Tematica_ID = 0;
                #endregion

                #region NEW Diseño
                pedido_disenos _pedido_diseno = new pedido_disenos();
                _pedido_diseno.Pedido_Diseno_ID = 0;

                _pedido_diseno.Texto = txbTexto1.Text;

                context.pedido_disenos.Add(_pedido_diseno);
                Guardar_Contexto(context);

                int pedido_diseno_ID = Get_NextPedido_DisenoID(context);
                if (pedido_diseno_ID > 0)
                {
                    _pedido.Pedido_Diseno_ID = pedido_diseno_ID;
                }
                #endregion

                #region NEW Entrega
                pedido_entregas _pedido_entrega = new pedido_entregas();
                _pedido_entrega.Pedido_Entrega_ID = 0;

                _pedido_entrega.Entrega_Tipo_ID = 0;
                if (ddlTipoEntrega1.SelectedIndex > 0)
                {
                    int selected = 1;
                    if (!int.TryParse(ddlTipoEntrega1.SelectedValue, out selected))
                    {
                        selected = 1;
                        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTipoEntrega1.SelectedValue);
                    }
                    _pedido_entrega.Entrega_Tipo_ID = selected;
                }

                _pedido_entrega.Direccion = txbDireccion.Value;
                _pedido_entrega.Fecha_entrega = GetDatetimeFormated(txbFecha.Value);

                // Save current LAT and LNG
                if (!string.IsNullOrWhiteSpace(hdnCurrentLAT.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLNG.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLocationURL.Value))
                {
                    _pedido_entrega.Coordenadas_X = hdnCurrentLAT.Value;
                    _pedido_entrega.Coordenadas_Y = hdnCurrentLNG.Value;
                    _pedido_entrega.Google_maps_URL = hdnCurrentLocationURL.Value;
                }

                context.pedido_entregas.Add(_pedido_entrega);
                Guardar_Contexto(context);
                int pedido_entrega_ID = Get_NextPedido_EntregaID(context);
                if (pedido_entrega_ID > 0)
                {
                    _pedido.Pedido_Entrega_ID = pedido_entrega_ID;
                }
                #endregion

                _pedido.Formulario_ID = formulario_ID;

                context.pedidos.Add(_pedido);
            }
        }

        private int Get_NextPedido_EntregaID(CarteluxDB context)
        {
            int id = 1;
            pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.OrderByDescending(p => p.Pedido_Entrega_ID).FirstOrDefault();
            if (_pedido_entrega != null)
            {
                id = _pedido_entrega.Pedido_Entrega_ID;
            }
            return id;
        }

        private int Get_NextPedido_DisenoID(CarteluxDB context)
        {
            int id = 1;
            pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.OrderByDescending(p => p.Pedido_Diseno_ID).FirstOrDefault();
            if (_pedido_diseno != null)
            {
                id = _pedido_diseno.Pedido_Diseno_ID;
            }
            return id;
        }

        private int NEW_Cliente(CarteluxDB context)
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
                    cliente.Nombre = client_name;
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

        private int Get_NextClientID(CarteluxDB context)
        {
            int id = 1;
            clientes cliente = (clientes)context.clientes.OrderByDescending(p => p.Cliente_ID).FirstOrDefault();
            if (cliente != null)
            {
                id = cliente.Cliente_ID;
            }
            return id;
        }

        private int Get_NextFormularioID(CarteluxDB context)
        {
            int id = 1;
            formularios _formulario = (formularios)context.formularios.OrderByDescending(p => p.Formulario_ID).FirstOrDefault();
            if (_formulario != null)
            {
                id = _formulario.Formulario_ID;
            }
            return id;
        }

        private void UploadFile(string serie_str)
        {
            if (!string.IsNullOrWhiteSpace(serie_str))
            {
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                // Source: http://stackoverflow.com/questions/1998452/accessing-input-type-file-at-server-side-in-asp-net

                /* ******** Get file extension ******** */
                string fileName = MyFileUpload.PostedFile.FileName;
                string file_extension = string.Empty;

                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    file_extension = fileName.Substring(fileName.LastIndexOf('.'));
                }

                /* ******** Global variables ******** */
                string repoFilename = "", fullLocalPath = "", relativeLocalPath = "";

                using (CarteluxDB context = new CarteluxDB())
                {
                    formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Serie.Equals(serie_str));
                    if (_formulario != null)
                    {
                        // Pedidos
                        List<pedidos> lista_pedidos = (List<pedidos>)context.pedidos.Where(v => v.Formulario_ID == _formulario.Formulario_ID).ToList();
                        if (lista_pedidos != null && lista_pedidos.Count > 0)
                        {
                            foreach (pedidos _pedido in lista_pedidos)
                            {
                                int ID = _pedido.Pedido_ID;

                                // GET Diseño
                                pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(v => v.Pedido_Diseno_ID.Equals(_pedido.Pedido_Diseno_ID));
                                if (_pedido_diseno != null)
                                {
                                    if (MyFileUpload != null && MyFileUpload.PostedFile != null && !string.IsNullOrWhiteSpace(MyFileUpload.PostedFile.FileName))
                                    {
                                        /* ******** Configuration variables ******** */

                                        string original_filePath = MyFileUpload.PostedFile.FileName;

                                        // Repository path
                                        string localRepoPath = string.Empty;

                                        int isLocal = HttpContext.Current.Request.IsLocal ? 1 : 0;
                                        if (isLocal == 1)
                                        {
                                            if (ConfigurationManager.AppSettings != null)
                                            {
                                                localRepoPath = ConfigurationManager.AppSettings["LocalRepoPath"].ToString();
                                            }
                                        }
                                        else
                                        {
                                            if (ConfigurationManager.AppSettings != null)
                                            {
                                                localRepoPath = ConfigurationManager.AppSettings["ServerRepoPath"].ToString();
                                            }
                                        }

                                        repoFilename = ID + file_extension;

                                        // Repository relative path
                                        relativeLocalPath = DateTime.Now.Year.ToString("D4") + "\\" + DateTime.Now.Month.ToString("D2") + "\\";

                                        fullLocalPath = localRepoPath + relativeLocalPath; // REAL
                                    }

                                    /* ******** Save in DB ******** */

                                    string real_fileName = string.Empty;
                                    if (!string.IsNullOrWhiteSpace(fileName))
                                    {
                                        real_fileName = Path.GetFileName(fileName);
                                    }

                                    if (!string.IsNullOrWhiteSpace(real_fileName)
                                        && !string.IsNullOrWhiteSpace(relativeLocalPath) && !string.IsNullOrWhiteSpace(repoFilename)
                                        && !string.IsNullOrWhiteSpace(fullLocalPath) && MyFileUpload != null && MyFileUpload.PostedFile != null)
                                    {
                                        try
                                        {
                                            /*************** Finally save the file in server ***************/

                                            // Check if directory exists, if not creates it
                                            if (!Directory.Exists(Path.GetDirectoryName(fullLocalPath)))
                                            {
                                                Directory.CreateDirectory(Path.GetDirectoryName(fullLocalPath));
                                            }

                                            MyFileUpload.PostedFile.SaveAs(fullLocalPath + repoFilename);

                                            /*************** END ***************/

                                            /*************** Save in DB ***************/

                                            string bd_path = relativeLocalPath.Replace("\\", "/") + repoFilename;
                                            _pedido_diseno.Boceto_nombre = repoFilename;
                                            _pedido_diseno.Boceto_extension = file_extension;
                                            _pedido_diseno.Boceto_PATH = fullLocalPath; //bd_path ?
                                            Guardar_Contexto(context);

                                            /*************** END ***************/
                                        }
                                        catch (Exception e)
                                        {
                                            Logs.AddErrorLog("Excepcion. Copiando archivo al server y guardando en BD. ERROR:", className, methodName, e.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void FTPUpload(string serie_str)
        {
            if (!string.IsNullOrWhiteSpace(serie_str))
            {
                // https://www.aspsnippets.com/Articles/Uploading-Files-to-FTP-Server-programmatically-in-ASPNet-using-C-and-VBNet.aspx

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                //FTP Server URL.
                string ftp = "ftp://ftp.site4now.net/";
                if (ConfigurationManager.AppSettings != null)
                {
                    ftp = ConfigurationManager.AppSettings["FTP_Server"].ToString();
                }

                //FTP Folder name. Leave blank if you want to upload to root folder.
                string ftpFolder = "Cartelux/cxpedidos/Resources/Bocetos/";
                if (ConfigurationManager.AppSettings != null)
                {
                    ftpFolder = ConfigurationManager.AppSettings["FTP_Path"].ToString();
                }

                //FTP User. 
                string ftpUser = "madelux-001";
                if (ConfigurationManager.AppSettings != null)
                {
                    ftpUser = ConfigurationManager.AppSettings["FTP_User"].ToString();
                }

                //FTP Password. 
                string ftpPassword = "madelux1234";
                if (ConfigurationManager.AppSettings != null)
                {
                    ftpPassword = ConfigurationManager.AppSettings["FTP_Password"].ToString();
                }

                byte[] fileBytes = null;

                //Read the FileName and convert it to Byte array.
                string fileName = MyFileUpload.PostedFile.FileName;
                string file_extension = string.Empty;
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    file_extension = fileName.Substring(fileName.LastIndexOf('.'));
                }

                if (!string.IsNullOrWhiteSpace(file_extension))
                {
                    using (CarteluxDB context = new CarteluxDB())
                    {
                        formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Serie.Equals(serie_str));
                        if (_formulario != null)
                        {
                            // Pedidos
                            List<pedidos> lista_pedidos = (List<pedidos>)context.pedidos.Where(v => v.Formulario_ID == _formulario.Formulario_ID).ToList();
                            if (lista_pedidos != null && lista_pedidos.Count > 0)
                            {
                                foreach (pedidos _pedido in lista_pedidos)
                                {
                                    int ID = _pedido.Pedido_ID;
                                    string url = string.Empty;

                                    // GET Diseño
                                    pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(v => v.Pedido_Diseno_ID.Equals(_pedido.Pedido_Diseno_ID));
                                    if (_pedido_diseno != null)
                                    {
                                        string repoFilename = ID + file_extension;

                                        // Repository relative path
                                        string relativePath = DateTime.Now.Year.ToString("D4") + "/" + DateTime.Now.Month.ToString("D2") + "/";

                                        #region Upload file FTP
                                        string fileName1 = Path.GetFileName(MyFileUpload.FileName);
                                        using (StreamReader fileStream = new StreamReader(MyFileUpload.PostedFile.InputStream))
                                        {
                                            fileBytes = Encoding.UTF8.GetBytes(fileStream.ReadToEnd());
                                            fileStream.Close();
                                        }

                                        try
                                        {
                                            //url = ftpFolder + relativePath + repoFilename; 
                                            //url = ftp + ftpFolder + repoFilename; 
                                            url = ftp + ftpFolder + fileName1;

                                            //Create FTP Request.
                                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                                            request.Method = WebRequestMethods.Ftp.UploadFile;

                                            //Enter FTP Server credentials.
                                            request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                                            request.ContentLength = fileBytes.Length;
                                            request.UsePassive = true;
                                            request.UseBinary = true;
                                            request.ServicePoint.ConnectionLimit = fileBytes.Length;
                                            request.EnableSsl = false;

                                            using (Stream requestStream = request.GetRequestStream())
                                            {
                                                requestStream.Write(fileBytes, 0, fileBytes.Length);
                                                requestStream.Close();
                                            }

                                            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                                            //lblMessage.Text += fileName + " uploaded.<br />";
                                            response.Close();

                                            // Save in DB
                                            _pedido_diseno.Boceto_nombre = repoFilename;
                                            _pedido_diseno.Boceto_extension = file_extension;
                                            _pedido_diseno.Boceto_PATH = url; //bd_path ?
                                            Guardar_Contexto(context);
                                        }
                                        catch (Exception e)
                                        {
                                            Logs.AddErrorLog("Excepcion. Cargando archivo de boceto por FTP. ERROR:", className, methodName, e.Message);
                                            Logs.AddErrorLog("Excepcion. URL:", className, methodName, url);
                                        }

                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void FTPUpload_2(string serie_str)
        {
            if (!string.IsNullOrWhiteSpace(serie_str))
            {
                // https://gpailler.github.io/MegaApiClient/articles/samples.html

                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                #region Get_Params

                //MEGA User. 
                string MEGA_User = "madelux-001";
                if (ConfigurationManager.AppSettings != null)
                {
                    MEGA_User = ConfigurationManager.AppSettings["MEGA_User"].ToString();
                }

                //MEGA Password. 
                string MEGA_Password = "madelux1234";
                if (ConfigurationManager.AppSettings != null)
                {
                    MEGA_Password = ConfigurationManager.AppSettings["MEGA_Password"].ToString();
                }

                //MEGA Path. 
                string MEGA_Path = "madelux1234";
                if (ConfigurationManager.AppSettings != null)
                {
                    MEGA_Path = ConfigurationManager.AppSettings["MEGA_Path"].ToString();
                }

                #endregion

                //Read the FileName and convert it to Byte array.
                string fileName = MyFileUpload.PostedFile.FileName;
                string file_extension = string.Empty;
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    file_extension = fileName.Substring(fileName.LastIndexOf('.'));
                }

                if (!string.IsNullOrWhiteSpace(file_extension))
                {
                    using (CarteluxDB context = new CarteluxDB())
                    {
                        formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Serie.Equals(serie_str));
                        if (_formulario != null)
                        {
                            // Pedidos
                            List<pedidos> lista_pedidos = (List<pedidos>)context.pedidos.Where(v => v.Formulario_ID == _formulario.Formulario_ID).ToList();
                            if (lista_pedidos != null && lista_pedidos.Count > 0)
                            {
                                foreach (pedidos _pedido in lista_pedidos)
                                {
                                    int ID = _pedido.Pedido_ID;
                                    string url = string.Empty;

                                    // GET Diseño
                                    pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(v => v.Pedido_Diseno_ID.Equals(_pedido.Pedido_Diseno_ID));
                                    if (_pedido_diseno != null)
                                    {
                                        string repoFilename = ID + file_extension;

                                        // Repository relative path
                                        string relativePath = DateTime.Now.Year.ToString("D4") + "/" + DateTime.Now.Month.ToString("D2") + "/";

                                        #region Upload file FTP
                                        string fileName2 = MyFileUpload.PostedFile.FileName;
                                        string fileName1 = Path.GetFileName(MyFileUpload.FileName);

                                        //using (StreamReader fileStream = new StreamReader(MyFileUpload.PostedFile.InputStream))
                                        //{
                                        //    fileBytes = Encoding.UTF8.GetBytes(fileStream.ReadToEnd());
                                        //    fileStream.Close();
                                        //}

                                        try
                                        {
                                            //url = ftpFolder + relativePath + repoFilename; 
                                            //url = ftp + ftpFolder + repoFilename; 
                                            //url = ftp + ftpFolder + fileName1;

                                            #region MEGA

                                            var client = new MegaApiClient();
                                            client.Login(MEGA_User, MEGA_Password);

                                            IEnumerable<INode> nodes = client.GetNodes();

                                            INode root = nodes.Single(x => x.Type == NodeType.Root);
                                            INode myFolder = client.CreateFolder(MEGA_Path + relativePath, root);

                                            INode myFile = client.UploadFile(fileName2, myFolder);//
                                            Uri downloadLink = client.GetDownloadLink(myFile);
                                            Console.WriteLine(downloadLink);

                                            client.Logout();

                                            #endregion

                                            #region Save in DB

                                            _pedido_diseno.Boceto_nombre = repoFilename;
                                            _pedido_diseno.Boceto_extension = file_extension;
                                            _pedido_diseno.Boceto_PATH = url; //bd_path ?
                                            Guardar_Contexto(context);

                                            #endregion
                                        }
                                        catch (Exception e)
                                        {
                                            Logs.AddErrorLog("Excepcion. Cargando archivo de boceto por FTP. ERROR:", className, methodName, e.Message);
                                            Logs.AddErrorLog("Excepcion. URL:", className, methodName, url);
                                        }

                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool UploadMegaAPI(formularios _formulario, string tel_str)
        {
            bool uploadMega_ok = true;
            if (_formulario != null)
            {
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                // Source: https://github.com/gpailler/MegaApiClient
                // Source: https://codigoscript.com/2015/05/07/c-utilizar-api-mega-para-subir-archivos-a-la-nube/

                #region Get_Params

                //MEGA User. 
                string MEGA_User = "ventas@cartelux.uy";
                if (ConfigurationManager.AppSettings != null)
                {
                    MEGA_User = ConfigurationManager.AppSettings["MEGA_User"].ToString();
                }

                //MEGA Password. 
                string MEGA_Password = "cartelux1234";
                if (ConfigurationManager.AppSettings != null)
                {
                    MEGA_Password = ConfigurationManager.AppSettings["MEGA_Password"].ToString();
                }

                //MEGA Path 1. 
                string MEGA_Path1 = "TRABAJOS";
                if (ConfigurationManager.AppSettings != null)
                {
                    MEGA_Path1 = ConfigurationManager.AppSettings["MEGA_Path1"].ToString();
                }

                //MEGA Path 2. 
                string MEGA_Path2 = "BOSQUEJOS";
                if (ConfigurationManager.AppSettings != null)
                {
                    MEGA_Path2 = ConfigurationManager.AppSettings["MEGA_Path2"].ToString();
                }

                #endregion

                /* ******** Get file extension ******** */
                string fileName = MyFileUpload.PostedFile.FileName;
                string file_extension = string.Empty;
                string complete_URL = string.Empty;

                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    file_extension = fileName.Substring(fileName.LastIndexOf('.'));
                }

                /* ******** Global variables ******** */

                using (CarteluxDB context = new CarteluxDB())
                {
                    // Pedidos
                    pedidos _pedido = (pedidos)context.pedidos.FirstOrDefault(v => v.Formulario_ID == _formulario.Formulario_ID);
                    if (_pedido != null) // CHECK THIS ***********************
                    {
                        int ID = _pedido.Pedido_ID;

                        // GET Diseño
                        pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(v => v.Pedido_Diseno_ID.Equals(_pedido.Pedido_Diseno_ID));
                        if (_pedido_diseno != null)
                        {
                            if (MyFileUpload != null && MyFileUpload.PostedFile != null && !string.IsNullOrWhiteSpace(MyFileUpload.PostedFile.FileName))
                            {
                                try
                                {
                                    // Instancia de un cliente para conectar con mega.
                                    MegaApiClient cliente = new MegaApiClient();
                                    // Inicio de sesión con el cliente, pasando el correo y la contraseña de la cuenta mega a la que se sube el archivo.
                                    cliente.Login(MEGA_User, MEGA_Password);

                                    // Obtenemos los nodos (directorios/archivos) de la cuenta dentro de una variable.
                                    var nodos = cliente.GetNodes();

                                    // Comprobar si existe algún nodo (directorio) que se llame "Facturas" (en mi caso quiero subir el archivo a dicha carpeta).

                                    // Crear dos nodos.
                                    INode root;
                                    INode carpeta;

                                    string folder = string.Empty;

                                    #region Carpeta TRABAJOS
                                    folder = MEGA_Path1;
                                    complete_URL = folder + "/";
                                    bool existe = cliente.GetNodes().Any(n => n.Name == folder);

                                    // Si el directorio facturas existe, se obtiene. Si no existe, se crea.
                                    if (existe == true)
                                    {
                                        // Obtenemos el directorio.
                                        carpeta = nodos.FirstOrDefault(n => n.Name == folder);
                                    }
                                    else
                                    {
                                        // Obtenemos el nodo raíz.
                                        root = nodos.Single(n => n.Type == NodeType.Root);
                                        carpeta = cliente.CreateFolder(folder, root);
                                    }
                                    #endregion

                                    #region Carpeta Mes-Año 01-2018

                                    string year = DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Year.ToString("D4");

                                    folder = year;
                                    complete_URL += folder + "/";
                                    existe = cliente.GetNodes(carpeta).Any(n => n.Name == folder);

                                    // Si el directorio facturas existe, se obtiene. Si no existe, se crea.
                                    if (existe == true)
                                    {
                                        // Obtenemos el directorio.
                                        carpeta = nodos.FirstOrDefault(n => n.Name == folder);
                                    }
                                    else
                                    {
                                        // Obtenemos el nodo raíz.
                                        carpeta = cliente.CreateFolder(folder, carpeta);
                                    }
                                    #endregion

                                    #region Carpeta Bocetos
                                    folder = MEGA_Path2;
                                    complete_URL += folder + "/";
                                    existe = cliente.GetNodes(carpeta).Any(n => n.Name == folder);

                                    // Si el directorio facturas existe, se obtiene. Si no existe, se crea.
                                    if (existe == true)
                                    {
                                        // Obtenemos el directorio.
                                        carpeta = nodos.FirstOrDefault(n => n.Name == folder);
                                    }
                                    else
                                    {
                                        // Obtenemos el nodo raíz.
                                        carpeta = cliente.CreateFolder(folder, carpeta);
                                    }
                                    #endregion

                                    //string fileName1 = Path.GetFileName(MyFileUpload.FileName);
                                    string fileName1 = tel_str + file_extension;

                                    using (Stream stream = MyFileUpload.PostedFile.InputStream)
                                    {
                                        cliente.Upload(stream, fileName1, carpeta);
                                        cliente.Logout();
                                    }

                                    #region Save in DB

                                    _pedido_diseno.Boceto_nombre = fileName1;
                                    _pedido_diseno.Boceto_extension = file_extension;
                                    _pedido_diseno.Boceto_PATH = complete_URL;
                                    uploadMega_ok = Guardar_Contexto(context);

                                    #endregion
                                }
                                catch (Exception e)
                                {
                                    Logs.AddErrorLog("Excepcion. Copiando archivo al server y guardando en BD. ERROR:", className, methodName, e.Message);
                                    Logs.AddErrorLog("Excepcion. URL:", className, methodName, complete_URL);
                                    uploadMega_ok = false;
                                }

                            }
                        }
                    }
                }
            }
            return uploadMega_ok;
        }

        #endregion

        #region Static Methods 

        private static string GetQueryStringDecrypt(string serie_str)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(serie_str))
            {
                // Logger variables
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

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
            return result;
        }


        [WebMethod]
        public static bool SaveForm(string form_ID_param, string tel_param)
        {
            bool ret = false;
            if (!string.IsNullOrWhiteSpace(form_ID_param) && !string.IsNullOrWhiteSpace(tel_param))
            {
                string form_ID_str = GetQueryStringDecrypt(form_ID_param);
                string tel_str = GetQueryStringDecrypt(tel_param);
                if (!string.IsNullOrWhiteSpace(form_ID_str) && !string.IsNullOrWhiteSpace(tel_str))
                {
                    SaveData_static(form_ID_str, tel_str);
                }
                ret = true;
            }
            return ret;
        }

        private static void SaveData_static(string serie_str, string tel_str)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            if (!string.IsNullOrWhiteSpace(serie_str) && !string.IsNullOrWhiteSpace(tel_str))
            {
                using (CarteluxDB context = new CarteluxDB())
                {
                    formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Serie.Equals(serie_str));
                    _formulario = _formulario != null ? _formulario : new formularios();
                    _formulario.Fecha_update = DateTime.Now;

                    // Cliente
                    //_formulario.Cliente_ID = NEW_Cliente(context); // FIX

                    // Nuevo formulario
                    if (string.IsNullOrWhiteSpace(_formulario.Serie))
                    {
                        string url_complete = HttpContext.Current.Request.Url.AbsoluteUri;
                        if (!string.IsNullOrWhiteSpace(url_complete))
                        {
                            _formulario.URL_completa = url_complete;
                            _formulario.URL_short = url_complete; //
                        }

                        _formulario.Formulario_ID = 0;
                        _formulario.Serie = serie_str;
                        _formulario.Fecha_creado = DateTime.Now;
                        context.formularios.Add(_formulario);

                        //Guardar_Contexto(context); // FIX

                        // Nuevo pedido
                        //int formulario_ID = Get_NextFormularioID(context); // FIX
                        //if (formulario_ID > 0)
                        //{
                        //    NEW_Pedido(context, formulario_ID);
                        //}
                    }
                    else
                    {
                        // Pedido ya existe
                        // ISSUE a resolver cuando efectivamente los Formularios tengan muchos Pedidos
                        pedidos _pedido = (pedidos)context.pedidos.FirstOrDefault(v => v.Formulario_ID.Equals(_formulario.Formulario_ID));
                        if (_pedido != null)
                        {
                            // Update cantidad de Pedidos
                            int cantidad = _pedido.Cantidad;
                            //if (!string.IsNullOrWhiteSpace(hdnPedidoCantidad.Value)) // FIX
                            //{
                            //    if (!int.TryParse(hdnPedidoCantidad.Value, out cantidad))
                            //    {
                            //        cantidad = _pedido.Cantidad;
                            //        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, hdnPedidoCantidad.Value);
                            //    }
                            //}
                            _pedido.Cantidad = cantidad;

                            // Updates

                            #region Tipo = Pasacalle / Roll up / etc
                            //if (ddlTipoCartel.SelectedIndex > 0)
                            //{
                            //    int selected = 1;
                            //    if (!int.TryParse(ddlTipoCartel.SelectedValue, out selected))
                            //    {
                            //        selected = 1;
                            //        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTipoCartel.SelectedValue);
                            //    }
                            //    _pedido.Pedido_Tipo_ID = selected;
                            //}
                            _pedido.Pedido_Tipo_ID = 1;
                            #endregion

                            #region Material = Pintado / Impreso
                            //if (ddlTamano.SelectedIndex > 0)
                            //{
                            //    pedido.Pedido_Material_ID = ddlTamano.SelectedIndex;
                            //}
                            #endregion

                            #region UPDATE Tamaño
                            //if (ddlTamano1.SelectedIndex > 0) // FIX
                            //{
                            //    int selected = 1;
                            //    if (!int.TryParse(ddlTamano1.SelectedValue, out selected))
                            //    {
                            //        selected = 1;
                            //        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTamano1.SelectedValue);
                            //    }
                            //    _pedido.Pedido_Tamano_ID = selected;
                            //}
                            #endregion

                            #region Temática
                            //if (ddlTamano.SelectedIndex > 0)
                            //{
                            //    pedido.Pedido_Tamano_ID = ddlTamano.SelectedIndex;
                            //}
                            #endregion

                            #region UPDATE Diseño
                            pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(v => v.Pedido_Diseno_ID.Equals(_pedido.Pedido_Diseno_ID));
                            if (_pedido_diseno != null)
                            {
                                //_pedido_diseno.Texto = txbTexto1.Text; // FIX
                            }
                            #endregion

                            #region UPDATE Entrega
                            pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(v => v.Pedido_Entrega_ID.Equals(_pedido.Pedido_Entrega_ID));
                            if (_pedido_entrega != null)
                            {
                                //_pedido_entrega.Direccion = txbDireccion.Value; // FIX

                                //_pedido_entrega.Fecha_entrega = GetDatetimeFormated(txbFecha.Value);

                                //if (ddlTipoEntrega1.SelectedIndex > 0)
                                //{
                                //    int selected = 1;
                                //    if (!int.TryParse(ddlTipoEntrega1.SelectedValue, out selected))
                                //    {
                                //        selected = 1;
                                //        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTipoEntrega1.SelectedValue);
                                //    }
                                //    _pedido_entrega.Entrega_Tipo_ID = selected;
                                //}

                                // Save current LAT and LNG
                                //if (!string.IsNullOrWhiteSpace(hdnCurrentLAT.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLNG.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLocationURL.Value))
                                //{
                                //    _pedido_entrega.Coordenadas_X = hdnCurrentLAT.Value;
                                //    _pedido_entrega.Coordenadas_Y = hdnCurrentLNG.Value;
                                //    _pedido_entrega.Google_maps_URL = hdnCurrentLocationURL.Value;
                                //}
                            }
                            #endregion
                        }

                    }

                    // Guardar_Contexto(context); // FIX

                    //if (MyFileUpload.PostedFile != null && !string.IsNullOrWhiteSpace(MyFileUpload.PostedFile.FileName)) // FIX
                    //{
                    //    UploadMegaAPI(_formulario, tel_str);
                    //}
                }
            }
            else
            {
                Logs.AddErrorLog("Error. FormID no válido. ERROR:", className, methodName, serie_str);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false); // FIX
            }
        }

        #endregion

    }
}