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
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Cartelux1.Global_Objects.GlobalVariables;

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
                LoadAttributes();

                //string serie_str = GetURLParam_Decrypted("ID");
                string serie_str = string.Empty;
                if (System.Web.HttpContext.Current.Session["form_serie"] != null)
                {
                    serie_str = System.Web.HttpContext.Current.Session["form_serie"].ToString();
                }
                BindData(serie_str); // Existe el formulario con la serie?

                //string tel_str = GetURLParam("TEL");
                //if (!string.IsNullOrWhiteSpace(serie_str) && !string.IsNullOrWhiteSpace(tel_str))
                //if (!string.IsNullOrWhiteSpace(serie_str))
                //{
                //    BindData(serie_str);
                //}
                //if (string.IsNullOrWhiteSpace(serie_str))
                //{
                // Si no tiene ID como parámetro, tomar como si fuese un ingreso nuestro. Mostrar botón de limpiar campos 
                // (usar js función emptyFields_all_tabs) y verificar que guarde OK
                //}
            }
            else if (Request.QueryString.Count == 0)
            {
                //Response.Redirect("/Acceso.aspx");
            }
        }

        private void LoadAttributes()
        {
            radImpreso1.Attributes.Add("data-radiocharm-label", "Impreso");
            radImpreso2.Attributes.Add("data-radiocharm-label", "Pintado");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //SetFieldsReadOnly(false);
        }

        protected void btnConfirmar_ServerClick(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void btnConfirmar_ServerClick_tab2(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void btnLimpiar_ServerClick(object sender, EventArgs e)
        {
            Limpiar_Datos();
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

                // DDL Tamaños - Pasacalle
                dt1 = new DataTable();
                dt1 = Extras.ToDataTable(context.lista_pedido_tamanos.Where(v => v.Agrupacion == Agrupacion.Todas.ToString() || v.Agrupacion == Agrupacion.Pasacalle.ToString()).OrderBy(e => e.Nombre).ToList());
                ddlTamano1.DataSource = dt1;
                ddlTamano1.DataTextField = "Nombre";
                ddlTamano1.DataValueField = "Codigo";
                ddlTamano1.DataBind();
                ddlTamano1.Items.Insert(0, new ListItem("Tamaño", "0"));

                // DDL Entregas - Pasacalle
                dt1 = new DataTable();
                dt1 = Extras.ToDataTable(context.lista_entregas_tipos.Where(v => v.Agrupacion == Agrupacion.Todas.ToString() || v.Agrupacion == Agrupacion.Pasacalle.ToString()).OrderBy(e => e.Nombre).ToList());
                ddlTipoEntrega1.DataSource = dt1;
                ddlTipoEntrega1.DataTextField = "Nombre";
                ddlTipoEntrega1.DataValueField = "Codigo";
                ddlTipoEntrega1.DataBind();
                ddlTipoEntrega1.Items.Insert(0, new ListItem("Tipo de entrega", "0"));

                // DDL Temáticas - Pasacalle
                dt1 = new DataTable();
                dt1 = Extras.ToDataTable(context.lista_pedido_tematicas.ToList());
                ddlTematica.DataSource = dt1;
                ddlTematica.DataTextField = "Nombre";
                ddlTematica.DataValueField = "Codigo";
                ddlTematica.DataBind();
                ddlTematica.Items.Insert(0, new ListItem("Temática", "0"));

                // DDL Tamaños - Roll up
                dt1 = new DataTable();
                dt1 = Extras.ToDataTable(context.lista_pedido_tamanos.Where(v => v.Agrupacion == Agrupacion.Todas.ToString() || v.Agrupacion == Agrupacion.Rollup.ToString()).OrderBy(e => e.Nombre).ToList());
                ddlTamano1_tab2.DataSource = dt1;
                ddlTamano1_tab2.DataTextField = "Nombre";
                ddlTamano1_tab2.DataValueField = "Codigo";
                ddlTamano1_tab2.DataBind();
                ddlTamano1_tab2.Items.Insert(0, new ListItem("Tamaño", "0"));

                // DDL Entregas - Roll up
                dt1 = new DataTable();
                dt1 = Extras.ToDataTable(context.lista_entregas_tipos.Where(v => v.Agrupacion == Agrupacion.Todas.ToString() || v.Agrupacion == Agrupacion.Rollup.ToString()).OrderBy(e => e.Nombre).ToList());
                ddlTipoEntrega1_tab2.DataSource = dt1;
                ddlTipoEntrega1_tab2.DataTextField = "Nombre";
                ddlTipoEntrega1_tab2.DataValueField = "Codigo";
                ddlTipoEntrega1_tab2.DataBind();
                ddlTipoEntrega1_tab2.Items.Insert(0, new ListItem("Tipo de entrega", "0"));

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

        protected void BindData(string serie_str)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            //if (!string.IsNullOrWhiteSpace(serie_str) && !string.IsNullOrWhiteSpace(tel_str))
            {
                using (CarteluxDB context = new CarteluxDB())
                {
                    if (!string.IsNullOrWhiteSpace(serie_str))
                    {
                        formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Serie.Equals(serie_str));
                        if (_formulario != null)
                        {
                            // Cliente
                            clientes _cliente = Get_Client(context, _formulario.Cliente_ID);
                            if (_cliente != null)
                            {
                                txbNombre.Value = _cliente.Nombre;
                                txbTelefono.Value = _cliente.Telefono;
                                txbEmail.Value = _cliente.Email;
                                //txbDocumento.Value = _cliente.NroDocumento.ToString();

                                //if (_cliente.NroDocumento.ToString().Length > 8)
                                //{
                                //    radDoc1.Checked = false;
                                //    radDoc2.Checked = true;
                                //}
                                //else
                                //{
                                //    radDoc1.Checked = true;
                                //    radDoc2.Checked = false;
                                //}
                            } // end cliente

                            // Pedidos
                            List<pedidos> lista_pedidos = (List<pedidos>)context.pedidos.Where(v => v.Formulario_ID == _formulario.Formulario_ID).ToList();
                            if (lista_pedidos != null && lista_pedidos.Count > 0)
                            {
                                foreach (pedidos _pedido in lista_pedidos)
                                {
                                    hdnPedidoCantidad.Value = _pedido.Cantidad.ToString();

                                    #region ¿Is Pasacalle OR Roll up?

                                    bool isPasacalle = true;
                                    int tipoPedidoID = _pedido.Pedido_Tipo_ID;
                                    lista_pedido_tipos _pedido_tipo = (lista_pedido_tipos)context.lista_pedido_tipos.FirstOrDefault(v => v.Pedido_Tipo_ID.Equals(tipoPedidoID));
                                    if (_pedido_tipo != null)
                                    {
                                        // Si es código 4 ==> Roll up
                                        isPasacalle = _pedido_tipo.Codigo == 4 ? false : true;
                                    }

                                    #endregion ¿Is Pasacalle OR Roll up?

                                    #region IS PASACALLE

                                    if (isPasacalle)
                                    {
                                        #region GET Material

                                        int material = 1;
                                        lista_pedido_materiales _pedido_material = (lista_pedido_materiales)context.lista_pedido_materiales.FirstOrDefault(v => v.Pedido_Material_ID.Equals(_pedido.Pedido_Material_ID));
                                        if (_pedido_material != null)
                                        {
                                            material = _pedido_material.Codigo;
                                        }

                                        switch (material)
                                        {
                                            case 1:
                                                {
                                                    // Impreso
                                                    radImpreso1.Checked = true;
                                                    radImpreso2.Checked = false;
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    // Pintado
                                                    radImpreso1.Checked = false;
                                                    radImpreso2.Checked = true;
                                                    break;
                                                }
                                        }

                                        #endregion GET Material

                                        #region GET Diseño

                                        pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(v => v.Pedido_Diseno_ID.Equals(_pedido.Pedido_Diseno_ID));
                                        if (_pedido_diseno != null)
                                        {
                                            txbTexto1.Text = _pedido_diseno.Texto;
                                            chbBosquejo.Checked = string.IsNullOrWhiteSpace(_pedido_diseno.Boceto_nombre) ? false : true;

                                            string Diseno_referido = !string.IsNullOrWhiteSpace(_pedido_diseno.Diseno_referido) ? _pedido_diseno.Diseno_referido : "Elija un diseño aquí por favor";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Set Diseno_referido", "<script type='text/javascript'>$('#aLinkToUnitegallery').val('" + Diseno_referido + "') </script>", false);
                                        }

                                        #endregion GET Diseño

                                        #region GET Temática
                                        if (_pedido.Pedido_Tematica_ID > 0)
                                        {
                                            lista_pedido_tematicas _lista_pedido_tematica = (lista_pedido_tematicas)context.lista_pedido_tematicas.FirstOrDefault(v => v.Pedido_Tematica_ID.Equals(_pedido.Pedido_Tematica_ID));
                                            if (_lista_pedido_tematica != null)
                                            {
                                                ddlTematica.SelectedValue = _lista_pedido_tematica.Codigo.ToString();
                                            }
                                        }
                                        #endregion GET Temática

                                        #region GET Entrega

                                        pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(v => v.Pedido_Entrega_ID.Equals(_pedido.Pedido_Entrega_ID));
                                        if (_pedido_entrega != null)
                                        {
                                            txbDireccion_calle.Value = _pedido_entrega.Direccion_calle;
                                            txbDireccion_numero.Value = _pedido_entrega.Direccion_numero;
                                            txbDireccion_apto.Value = _pedido_entrega.Direccion_apto;
                                            txbDireccion_esquina.Value = _pedido_entrega.Direccion_esquina;

                                            string sqlFormattedDate = _pedido_entrega.Fecha_entrega.HasValue ? _pedido_entrega.Fecha_entrega.Value.ToString(GlobalVariables.ShortDateTime_format1) : "null";
                                            txbFecha.Value = sqlFormattedDate;

                                            txbCiudad.Value = _pedido_entrega.Ciudad;

                                            /*
                                            hdnCurrentLAT.Value = _pedido_entrega.Coordenadas_X;
                                            hdnCurrentLNG.Value = _pedido_entrega.Coordenadas_Y;
                                            hdnCurrentLocationURL.Value = _pedido_entrega.Google_maps_URL;
                                            */

                                            if (_pedido_entrega.Entrega_Tipo_ID > 0)
                                            {
                                                lista_entregas_tipos _lista_entregas_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(v => v.Entrega_Tipo_ID.Equals(_pedido_entrega.Entrega_Tipo_ID));
                                                if (_lista_entregas_tipo != null)
                                                {
                                                    ddlTipoEntrega1.SelectedValue = _lista_entregas_tipo.Codigo.ToString();
                                                    int codigo = _lista_entregas_tipo.Codigo;
                                                    /*
                                                     * Codigo = 1: Colocación
                                                     * Codigo = 2: Envío a domicilio
                                                     * Codigo = 3: Envío al interior
                                                     * Codigo = 4: Retiro en el taller
                                                     * */
                                                    if (codigo == 1 || codigo == 2)
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>showControl_withDelay('dir_group', true);</script>", false);
                                                    }
                                                    else if (codigo == 3)
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>showControl_withDelay('txbCiudad', true);</script>", false);
                                                    }
                                                }
                                            }
                                        }

                                        #endregion GET Entrega

                                        #region Tamaño
                                        if (_pedido.Pedido_Tamano_ID > 0)
                                        {
                                            lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(v => v.Pedido_Tamano_ID.Equals(_pedido.Pedido_Tamano_ID));
                                            if (_lista_pedido_tamano != null)
                                            {
                                                ddlTamano1.SelectedValue = _lista_pedido_tamano.Codigo.ToString();
                                            }
                                        }
                                        #endregion

                                    } // if isPasacalle

                                    #endregion IS PASACALLE

                                    #region IS ROLL UP

                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Set ROLLUP", "<script type='text/javascript'>$('#aTabsPedidos_2').click();</script>", false);

                                        #region GET Diseño

                                        pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(v => v.Pedido_Diseno_ID.Equals(_pedido.Pedido_Diseno_ID));
                                        if (_pedido_diseno != null)
                                        {
                                            txbTexto1_tab2.Text = _pedido_diseno.Texto;
                                            chbBosquejo_tab2.Checked = string.IsNullOrWhiteSpace(_pedido_diseno.Boceto_nombre) ? false : true;
                                        }

                                        #endregion GET Diseño

                                        #region GET Entrega

                                        pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(v => v.Pedido_Entrega_ID.Equals(_pedido.Pedido_Entrega_ID));
                                        if (_pedido_entrega != null)
                                        {
                                            txbDireccion_calle_tab2.Value = _pedido_entrega.Direccion_calle;
                                            txbDireccion_numero_tab2.Value = _pedido_entrega.Direccion_numero;
                                            txbDireccion_apto_tab2.Value = _pedido_entrega.Direccion_apto;
                                            txbDireccion_esquina_tab2.Value = _pedido_entrega.Direccion_esquina;

                                            string sqlFormattedDate = _pedido_entrega.Fecha_entrega.HasValue ? _pedido_entrega.Fecha_entrega.Value.ToString(GlobalVariables.ShortDateTime_format1) : "null";
                                            txbFecha_tab2.Value = sqlFormattedDate;

                                            txbCiudad_tab2.Value = _pedido_entrega.Ciudad;

                                            if (_pedido_entrega.Entrega_Tipo_ID > 0)
                                            {
                                                lista_entregas_tipos _lista_entregas_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(v => v.Entrega_Tipo_ID.Equals(_pedido_entrega.Entrega_Tipo_ID));
                                                if (_lista_entregas_tipo != null)
                                                {
                                                    ddlTipoEntrega1_tab2.SelectedValue = _lista_entregas_tipo.Codigo.ToString();
                                                    int codigo = _lista_entregas_tipo.Codigo;
                                                    /*
                                                     * Codigo = 1: Colocación
                                                     * Codigo = 2: Envío a domicilio
                                                     * Codigo = 3: Envío al interior
                                                     * Codigo = 4: Retiro en el taller
                                                     * */
                                                    if (codigo == 1 || codigo == 2)
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>showControl_withDelay('dir_group_tab2', true);</script>", false);
                                                    }
                                                    else if (codigo == 3)
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>showControl_withDelay('txbCiudad_tab2', true);</script>", false);
                                                    }
                                                }
                                            }
                                        }

                                        #endregion GET Entrega

                                        #region Tamaño
                                        if (_pedido.Pedido_Tamano_ID > 0)
                                        {
                                            lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(v => v.Pedido_Tamano_ID.Equals(_pedido.Pedido_Tamano_ID));
                                            if (_lista_pedido_tamano != null)
                                            {
                                                ddlTamano1_tab2.SelectedValue = _lista_pedido_tamano.Codigo.ToString();
                                            }
                                        }
                                        #endregion
                                    }

                                    #endregion IS ROLL UP
                                }
                            } // end pedido

                            // Última actualización del pedido
                            lblLastUpdate.InnerText = " " + _formulario.Fecha_update.ToString(GlobalVariables.ShortDateTime_format1_long);
                        }
                    }
                    else
                    {
                        //txbTelefono.Value = tel_str;
                        //txbTelefono_tab2.Value = tel_str;
                    }
                }
            }
            //else
            //{
            //    Logs.AddErrorLog("Error. FormID no válido. ERROR:", className, methodName, serie_str);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
            //}
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
                txbTelefono.Attributes.Add("readonly", "readonly");
                txbEmail.Attributes.Add("readonly", "readonly");

                // Entrega
                txbDireccion_calle.Attributes.Add("readonly", "readonly");
                txbDireccion_numero.Attributes.Add("readonly", "readonly");
                txbDireccion_apto.Attributes.Add("readonly", "readonly");
                txbDireccion_esquina.Attributes.Add("readonly", "readonly");
                txbCiudad.Attributes.Add("readonly", "readonly");
                txbFecha.Attributes.Add("readonly", "readonly");

                // Cartel
                txbTexto1.Attributes.Add("readonly", "readonly");

                msj_result.Visible = true;
                btnConfirmar1.Enabled = false;
            }
            else
            {
                // Contacto
                txbNombre.Attributes.Add("readonly", "false");
                txbTelefono.Attributes.Add("readonly", "false");
                txbEmail.Attributes.Add("readonly", "false");                

                // Entrega
                txbDireccion_calle.Attributes.Add("readonly", "false");
                txbDireccion_numero.Attributes.Add("readonly", "false");
                txbDireccion_apto.Attributes.Add("readonly", "false");
                txbDireccion_esquina.Attributes.Add("readonly", "false");
                txbCiudad.Attributes.Add("readonly", "false");
                txbFecha.Attributes.Add("readonly", "false");

                // Cartel
                txbTexto1.Attributes.Add("readonly", "false");

                btnConfirmar1.Enabled = true;
            }
        }

        private void SaveData(bool isPasacalle = true)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            bool save_ok = true;

            bool existe_form = false;
            //string serie_str = GetURLParam_Decrypted("ID"); 
            // Existe el formulario con la serie?
            string serie_str = string.Empty;
            if (System.Web.HttpContext.Current.Session["form_serie"] != null)
            {
                serie_str = System.Web.HttpContext.Current.Session["form_serie"].ToString();
            }
            using (CarteluxDB context = new CarteluxDB())
            {
                formularios _formulario = null;
                if (!string.IsNullOrWhiteSpace(serie_str))
                {
                    //string serie_str = Extras
                    _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Serie.Equals(serie_str));
                    if (_formulario != null)
                    {
                        existe_form = true;
                    }
                }
                if (!existe_form)
                {
                    _formulario = new formularios();
                }

                _formulario.Fecha_update = GetCurrentTime();

                // Cliente
                _formulario.Cliente_ID = Cliente_Nuevo(context);

                // Nuevo formulario
                if (!existe_form)
                {
                    // Genero la nueva Serie para el formulario
                    serie_str = Extras.Generar_Serie();
                    _formulario.Serie = serie_str;

                    // Guardo la Serie en la sesión
                    System.Web.HttpContext.Current.Session["form_serie"] = serie_str;

                    string url_complete = HttpContext.Current.Request.Url.AbsoluteUri;
                    if (!string.IsNullOrWhiteSpace(url_complete))
                    {
                        _formulario.URL_completa = url_complete;
                        _formulario.URL_short = url_complete; //
                    }

                    _formulario.Formulario_ID = 0;
                    _formulario.Fecha_creado = GetCurrentTime();
                    context.formularios.Add(_formulario);

                    save_ok = Guardar_Contexto(context);

                    // Nuevo pedido
                    int formulario_ID = Get_NextFormularioID(context);
                    if (formulario_ID > 0)
                    {
                        Pedido_Nuevo(context, formulario_ID, isPasacalle, _formulario);
                    }

                    // Guardada final - Pedido nuevo
                    save_ok = Guardar_Contexto(context);
                    if (save_ok)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>confirmacionPedido(); apply_savedStyle(); </script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
                    }
                }
                else
                {
                    #region Pedido ya existe

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

                        /* Código tipo pedido
                         * 1 - Pasacalle
                         * 2 - Pancarta
                         * 3 - Lona informativa / decorativa
                         * 4 - Roll up
                         * */

                        int tipo_codigo = 1; // Pasacalle
                        if (!isPasacalle)
                        {
                            tipo_codigo = 4; // Roll up
                        }

                        _pedido.Pedido_Tipo_ID = 2; // ID Pasacalle
                        lista_pedido_tipos _pedido_tipo = (lista_pedido_tipos)context.lista_pedido_tipos.FirstOrDefault(v => v.Codigo.Equals(tipo_codigo));
                        if (_pedido_tipo != null)
                        {
                            _pedido.Pedido_Tipo_ID = _pedido_tipo.Pedido_Tipo_ID;
                        }

                        #endregion

                        #region Material = Pintado / Impreso
                        int material = 1; // Impreso
                        if (!radImpreso1.Checked)
                        {
                            material = 2;
                        }

                        int materialID = _pedido.Pedido_Material_ID;
                        lista_pedido_materiales _pedido_material = (lista_pedido_materiales)context.lista_pedido_materiales.FirstOrDefault(v => v.Codigo.Equals(material));
                        if (_pedido_material != null)
                        {
                            materialID = _pedido_material.Pedido_Material_ID;
                        }
                        _pedido.Pedido_Material_ID = materialID;
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

                        #region UPDATE Diseño
                        pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(v => v.Pedido_Diseno_ID.Equals(_pedido.Pedido_Diseno_ID));
                        if (_pedido_diseno != null)
                        {
                            _pedido_diseno.Texto = txbTexto1.Text;
                            if (!string.IsNullOrWhiteSpace(hdnDisenoSeleccionado.Value))
                            {
                                _pedido_diseno.Diseno_referido = hdnDisenoSeleccionado.Value;
                            }
                        }
                        #endregion

                        #region UPDATE Temática
                        if (ddlTematica.SelectedIndex > 0)
                        {
                            int codigo = 1;
                            if (!int.TryParse(ddlTematica.SelectedValue, out codigo))
                            {
                                codigo = 1;
                                Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTematica.SelectedValue);
                            }
                            int tipoID = 1;
                            lista_pedido_tematicas _lista_pedido_tematica = (lista_pedido_tematicas)context.lista_pedido_tematicas.FirstOrDefault(v => v.Codigo.Equals(codigo));
                            if (_lista_pedido_tematica != null)
                            {
                                tipoID = _lista_pedido_tematica.Pedido_Tematica_ID;
                            }
                            _pedido.Pedido_Tematica_ID = tipoID;
                        }
                        #endregion Temática

                        #region UPDATE Entrega
                        pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(v => v.Pedido_Entrega_ID.Equals(_pedido.Pedido_Entrega_ID));
                        if (_pedido_entrega != null)
                        {
                            _pedido_entrega.Fecha_entrega = GetDatetimeFormated(txbFecha.Value);

                            if (ddlTipoEntrega1.SelectedIndex > 0)
                            {
                                int codigo = 1;
                                if (!int.TryParse(ddlTipoEntrega1.SelectedValue, out codigo))
                                {
                                    codigo = 1;
                                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTipoEntrega1.SelectedValue);
                                }

                                int tipoID = codigo;
                                lista_entregas_tipos _pedido_entrega_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(v => v.Codigo.Equals(codigo));
                                if (_pedido_entrega_tipo != null)
                                {
                                    tipoID = _pedido_entrega_tipo.Entrega_Tipo_ID;
                                }
                                _pedido_entrega.Entrega_Tipo_ID = tipoID;

                                /*
                                 * Codigo = 1: Colocación
                                 * Codigo = 2: Envío a domicilio
                                 * Codigo = 3: Envío al interior
                                 * Codigo = 4: Retiro en el taller
                                 * */
                                if (codigo == 1 || codigo == 2)
                                {
                                    _pedido_entrega.Direccion_calle = txbDireccion_calle.Value;
                                    _pedido_entrega.Direccion_numero = txbDireccion_numero.Value;
                                    _pedido_entrega.Direccion_apto = txbDireccion_apto.Value;
                                    _pedido_entrega.Direccion_esquina = txbDireccion_esquina.Value;
                                }
                                else if (codigo == 3)
                                {
                                    _pedido_entrega.Ciudad = txbCiudad.Value;

                                    // Guarda la ciudad del Interior en Comentarios del Cliente
                                    if (_formulario != null)
                                    {
                                        int cliente_ID = _formulario.Cliente_ID;
                                        clientes _cliente = (clientes)context.clientes.FirstOrDefault(v => v.Cliente_ID.Equals(cliente_ID));
                                        if (_cliente != null)
                                        {
                                            _cliente.Comentarios = txbCiudad.Value;
                                        }
                                    }
                                }

                                string sqlFormattedDate = _pedido_entrega.Fecha_entrega.HasValue ? _pedido_entrega.Fecha_entrega.Value.ToString(GlobalVariables.ShortDateTime_format1) : "null";
                                txbFecha.Value = sqlFormattedDate;
                            }

                            string gmaps_url = Get_GMaps_URL();
                            if (!string.IsNullOrWhiteSpace(gmaps_url))
                            {
                                _pedido_entrega.Google_maps_URL = gmaps_url;
                            }

                            /*
                            // Save current LAT and LNG
                            if (!string.IsNullOrWhiteSpace(hdnCurrentLAT.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLNG.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLocationURL.Value))
                            {
                                _pedido_entrega.Coordenadas_X = hdnCurrentLAT.Value;
                                _pedido_entrega.Coordenadas_Y = hdnCurrentLNG.Value;
                                _pedido_entrega.Google_maps_URL = hdnCurrentLocationURL.Value;
                            }
                            */
                        }
                        #endregion
                    }

                    // Guardada final - Pedido existente
                    save_ok = Guardar_Contexto(context);
                    if (save_ok)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>actualizacionPedido(); apply_savedStyle(); </script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
                    }

                    #endregion END Pedido ya existe
                }

                //save_ok = Guardar_Contexto(context);
                //if (save_ok)
                //{
                //    //if (MyFileUpload.PostedFile != null && !string.IsNullOrWhiteSpace(MyFileUpload.PostedFile.FileName))
                //    //{
                //    //    //save_ok = UploadMegaAPI(_formulario, tel_str);
                //    //}
                //}                
            }
            //}
            //else
            //{
            //    Logs.AddErrorLog("Error. FormID no válido. ERROR:", className, methodName, serie_str);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "<script type='text/javascript'>alert('Error interno. \nComunícate con el equipo de Cartelux por favor.'); </script>", false);
            //}
        }

        private string Get_GMaps_URL(bool isPasacalle = true)
        {
            string gmaps_url = "https://www.google.com/maps/search/?api=1&query=";
            if (ConfigurationManager.AppSettings != null)
            {
                gmaps_url = ConfigurationManager.AppSettings["GMap_URL"].ToString();
            }
            if (isPasacalle)
            {
                gmaps_url += txbDireccion_calle.Value + " " + txbDireccion_numero.Value;
            }
            else
            {
                gmaps_url += txbDireccion_calle_tab2.Value + " " + txbDireccion_numero_tab2.Value;
            }
            return gmaps_url;
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

        private void Pedido_Nuevo(CarteluxDB context, int formulario_ID, bool isPasacalle, formularios _formulario)
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

                bool isColocacion_entrega = false;

                #region IS PASACALLE

                if (isPasacalle)
                {
                    #region Tamaño

                    /* Código tamaño cartel
                    * 1 - 150x80
                    * 2 - 300x80
                    * 3 - 500x80
                    * 4 - Pancarta otra medida
                    * 5 - 150x80
                    * 6 - 200x80
                    * 7 - Banner 80x90
                    * 8 - Banner otra medida
                    * */
                    bool isBanner = false;
                    _pedido.Pedido_Tamano_ID = 2;
                    if (ddlTamano1.SelectedIndex > 0)
                    {
                        int tamano_codigo = 1;
                        if (!int.TryParse(ddlTamano1.SelectedValue, out tamano_codigo))
                        {
                            tamano_codigo = 1;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTamano1.SelectedValue);
                        }
                        lista_pedido_tamanos _pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(v => v.Codigo.Equals(tamano_codigo));
                        if (_pedido_tamano != null)
                        {
                            _pedido.Pedido_Tamano_ID = _pedido_tamano.Pedido_Tamano_ID;
                        }

                        if (tamano_codigo == 7 || tamano_codigo == 8)
                        {
                            isBanner = true;
                        }
                    }
                    #endregion

                    #region Tipo cartel = Pasacalle / Roll up

                    /* Código tipo pedido
                     * 1 - Pasacalle
                     * 2 - Pancarta
                     * 3 - Banner
                     * 4 - Roll up
                     * */

                    int tipo_codigo = 1; // Pasacalle
                    _pedido.Pedido_Tipo_ID = 2; // ID Pasacalle (por si falla)
                    if (isBanner)
                    {
                        tipo_codigo = 3; // Banner
                    }

                    lista_pedido_tipos _pedido_tipo = (lista_pedido_tipos)context.lista_pedido_tipos.FirstOrDefault(v => v.Codigo.Equals(tipo_codigo));
                    if (_pedido_tipo != null)
                    {
                        _pedido.Pedido_Tipo_ID = _pedido_tipo.Pedido_Tipo_ID;
                    }

                    #endregion

                    #region Material = Pintado / Impreso
                    int material = 1; // Impreso
                    if (!radImpreso1.Checked)
                    {
                        material = 2;
                    }

                    int materialID = 2;
                    lista_pedido_materiales _pedido_material = (lista_pedido_materiales)context.lista_pedido_materiales.FirstOrDefault(v => v.Codigo.Equals(material));
                    if (_pedido_material != null)
                    {
                        materialID = _pedido_material.Pedido_Material_ID;
                    }
                    _pedido.Pedido_Material_ID = materialID;
                    #endregion

                    #region Temática
                    _pedido.Pedido_Tematica_ID = 0;
                    if (ddlTematica.SelectedIndex > 0)
                    {
                        int codigo = 1;
                        if (!int.TryParse(ddlTematica.SelectedValue, out codigo))
                        {
                            codigo = 1;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTematica.SelectedValue);
                        }
                        int tematicaID = 1;
                        lista_pedido_tematicas _pedido_tematica = (lista_pedido_tematicas)context.lista_pedido_tematicas.FirstOrDefault(v => v.Codigo.Equals(codigo));
                        if (_pedido_tematica != null)
                        {
                            tematicaID = _pedido_tematica.Pedido_Tematica_ID;
                        }
                        _pedido.Pedido_Tematica_ID = tematicaID;
                    }

                    #endregion

                    #region NEW Diseño
                    pedido_disenos _pedido_diseno = new pedido_disenos();
                    _pedido_diseno.Pedido_Diseno_ID = 0;

                    _pedido_diseno.Texto = txbTexto1.Text;
                    if (!string.IsNullOrWhiteSpace(hdnDisenoSeleccionado.Value))
                    {
                        _pedido_diseno.Diseno_referido = hdnDisenoSeleccionado.Value;
                    }

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

                    _pedido_entrega.Fecha_entrega = GetDatetimeFormated(txbFecha.Value);

                    _pedido_entrega.Entrega_Tipo_ID = 0;
                    if (ddlTipoEntrega1.SelectedIndex > 0)
                    {
                        int codigo = 1;
                        if (!int.TryParse(ddlTipoEntrega1.SelectedValue, out codigo))
                        {
                            codigo = 1;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTipoEntrega1.SelectedValue);
                        }

                        int tipoID = codigo;
                        lista_entregas_tipos _pedido_entrega_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(v => v.Codigo.Equals(codigo));
                        if (_pedido_entrega_tipo != null)
                        {
                            tipoID = _pedido_entrega_tipo.Entrega_Tipo_ID;
                        }
                        _pedido_entrega.Entrega_Tipo_ID = tipoID;

                        /*
                        * Codigo = 1: Colocación
                        * Codigo = 2: Envío a domicilio
                        * Codigo = 3: Envío al interior
                        * Codigo = 4: Retiro en el taller
                        * */
                        if (codigo == 1 || codigo == 2)
                        {
                            _pedido_entrega.Direccion_calle = txbDireccion_calle.Value;
                            _pedido_entrega.Direccion_numero = txbDireccion_numero.Value;
                            _pedido_entrega.Direccion_apto = txbDireccion_apto.Value;
                            _pedido_entrega.Direccion_esquina = txbDireccion_esquina.Value;
                            isColocacion_entrega = true;
                        }
                        else if (codigo == 3)
                        {
                            _pedido_entrega.Ciudad = txbCiudad.Value;
                            isColocacion_entrega = true;

                            // Guarda la ciudad del Interior en Comentarios del Cliente
                            if (_formulario != null)
                            {
                                int cliente_ID = _formulario.Cliente_ID;
                                clientes _cliente = (clientes)context.clientes.FirstOrDefault(v => v.Cliente_ID.Equals(cliente_ID));
                                if (_cliente != null)
                                {
                                    _cliente.Comentarios = txbCiudad.Value;
                                }
                            }
                        }
                    }

                    string gmaps_url = Get_GMaps_URL();
                    if (!string.IsNullOrWhiteSpace(gmaps_url))
                    {
                        _pedido_entrega.Google_maps_URL = gmaps_url;
                    }

                    /*
                    // Save current LAT and LNG
                    if (!string.IsNullOrWhiteSpace(hdnCurrentLAT.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLNG.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLocationURL.Value))
                    {
                        _pedido_entrega.Coordenadas_X = hdnCurrentLAT.Value;
                        _pedido_entrega.Coordenadas_Y = hdnCurrentLNG.Value;
                        _pedido_entrega.Google_maps_URL = hdnCurrentLocationURL.Value;
                    }
                    */

                    context.pedido_entregas.Add(_pedido_entrega);
                    Guardar_Contexto(context);
                    int pedido_entrega_ID = Get_NextPedido_EntregaID(context);
                    if (pedido_entrega_ID > 0)
                    {
                        _pedido.Pedido_Entrega_ID = pedido_entrega_ID;
                    }
                    #endregion


                } // if isPasacalle

                #endregion IS PASACALLE

                #region IS ROLL UP

                else
                {
                    #region Tipo cartel = Pasacalle / Roll up

                    /* Código tipo pedido
                     * 1 - Pasacalle
                     * 2 - Pancarta
                     * 3 - Lona informativa / decorativa
                     * 4 - Roll up
                     * */

                    int tipo_codigo = 4; // Roll up
                    _pedido.Pedido_Tipo_ID = 5; // ID Pasacalle (por si falla)
                    lista_pedido_tipos _pedido_tipo = (lista_pedido_tipos)context.lista_pedido_tipos.FirstOrDefault(v => v.Codigo.Equals(tipo_codigo));
                    if (_pedido_tipo != null)
                    {
                        _pedido.Pedido_Tipo_ID = _pedido_tipo.Pedido_Tipo_ID;
                    }

                    #endregion

                    #region Material = Pintado / Impreso
                    _pedido.Pedido_Material_ID = 1; // 1 Impreso
                    #endregion

                    #region Tamaño
                    _pedido.Pedido_Tamano_ID = 0;
                    if (ddlTamano1_tab2.SelectedIndex > 0)
                    {
                        int tamano_codigo = 1;
                        if (!int.TryParse(ddlTamano1_tab2.SelectedValue, out tamano_codigo))
                        {
                            tamano_codigo = 1;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTamano1_tab2.SelectedValue);
                        }
                        lista_pedido_tamanos _pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(v => v.Codigo.Equals(tamano_codigo));
                        if (_pedido_tamano != null)
                        {
                            _pedido.Pedido_Tamano_ID = _pedido_tamano.Pedido_Tamano_ID;
                        }
                    }
                    #endregion

                    #region Temática
                    _pedido.Pedido_Tematica_ID = 0;
                    //if (ddlTematica.SelectedIndex > 0)
                    //{
                    //    int codigo = 1;
                    //    if (!int.TryParse(ddlTematica.SelectedValue, out codigo))
                    //    {
                    //        codigo = 1;
                    //        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTematica.SelectedValue);
                    //    }
                    //    _pedido.Pedido_Tematica_ID = codigo;
                    //}

                    #endregion

                    #region NEW Diseño
                    pedido_disenos _pedido_diseno = new pedido_disenos();
                    _pedido_diseno.Pedido_Diseno_ID = 0;

                    _pedido_diseno.Texto = txbTexto1_tab2.Text;

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

                    _pedido_entrega.Fecha_entrega = GetDatetimeFormated(txbFecha_tab2.Value);

                    _pedido_entrega.Entrega_Tipo_ID = 0;
                    if (ddlTipoEntrega1_tab2.SelectedIndex > 0)
                    {
                        int codigo = 1;
                        if (!int.TryParse(ddlTipoEntrega1_tab2.SelectedValue, out codigo))
                        {
                            codigo = 1;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlTipoEntrega1.SelectedValue);
                        }

                        int tipoID = codigo;
                        lista_entregas_tipos _pedido_entrega_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(v => v.Codigo.Equals(codigo));
                        if (_pedido_entrega_tipo != null)
                        {
                            tipoID = _pedido_entrega_tipo.Entrega_Tipo_ID;
                        }
                        _pedido_entrega.Entrega_Tipo_ID = tipoID;

                        /*
                        * Codigo = 1: Colocación
                        * Codigo = 2: Envío a domicilio
                        * Codigo = 3: Envío al interior
                        * Codigo = 4: Retiro en el taller
                        * */
                        if (codigo == 1 || codigo == 2)
                        {
                            _pedido_entrega.Direccion_calle = txbDireccion_calle_tab2.Value;
                            _pedido_entrega.Direccion_numero = txbDireccion_numero_tab2.Value;
                            _pedido_entrega.Direccion_apto = txbDireccion_apto_tab2.Value;
                            _pedido_entrega.Direccion_esquina = txbDireccion_esquina_tab2.Value;
                            isColocacion_entrega = true;
                        }
                        else if (codigo == 3)
                        {
                            _pedido_entrega.Ciudad = txbCiudad_tab2.Value;
                            isColocacion_entrega = true;

                            // Guarda la ciudad del Interior en Comentarios del Cliente
                            if (_formulario != null)
                            {
                                int cliente_ID = _formulario.Cliente_ID;
                                clientes _cliente = (clientes)context.clientes.FirstOrDefault(v => v.Cliente_ID.Equals(cliente_ID));
                                if (_cliente != null)
                                {
                                    _cliente.Comentarios = txbCiudad.Value;
                                }
                            }
                        }
                    }

                    string gmaps_url = Get_GMaps_URL(false);
                    if (!string.IsNullOrWhiteSpace(gmaps_url))
                    {
                        _pedido_entrega.Google_maps_URL = gmaps_url;
                    }

                    /*
                    // Save current LAT and LNG
                    if (!string.IsNullOrWhiteSpace(hdnCurrentLAT.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLNG.Value) && !string.IsNullOrWhiteSpace(hdnCurrentLocationURL.Value))
                    {
                        _pedido_entrega.Coordenadas_X = hdnCurrentLAT.Value;
                        _pedido_entrega.Coordenadas_Y = hdnCurrentLNG.Value;
                        _pedido_entrega.Google_maps_URL = hdnCurrentLocationURL.Value;
                    }
                    */

                    context.pedido_entregas.Add(_pedido_entrega);
                    Guardar_Contexto(context);
                    int pedido_entrega_ID = Get_NextPedido_EntregaID(context);
                    if (pedido_entrega_ID > 0)
                    {
                        _pedido.Pedido_Entrega_ID = pedido_entrega_ID;
                    }
                    #endregion
                }

                #endregion IS ROLL UP

                _pedido.Formulario_ID = formulario_ID;

                context.pedidos.Add(_pedido);

                // Enviar notificación al equipo por EMAIL
                if (!HttpContext.Current.Request.IsLocal)
                {
                    SendNotification_Email_2(isColocacion_entrega);
                }
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

        private int Cliente_Nuevo(CarteluxDB context)
        {
            int ret_ID = 0;
            if (context != null) {
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                string client_tel = txbTelefono.Value;
                string client_name = txbNombre.Value;
                string client_email = txbEmail.Value;
                
                //int txbDocumento_int = 0;
                //if (!string.IsNullOrWhiteSpace(txbDocumento.Value))
                //{
                //    if (!int.TryParse(txbDocumento.Value, out txbDocumento_int))
                //    {
                //        txbDocumento_int = 0;
                //        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, txbDocumento.Value);
                //    }
                //}

                if (!string.IsNullOrWhiteSpace(client_tel) && !string.IsNullOrWhiteSpace(client_name))
                {
                    clientes cliente = (clientes)context.clientes.FirstOrDefault(v => v.Telefono.Equals(client_tel));
                    cliente = cliente != null ? cliente : new clientes();
                    cliente.Fecha_update = GetCurrentTime();
                    ret_ID = cliente.Cliente_ID;

                    if (string.IsNullOrWhiteSpace(cliente.Nombre))
                    {
                        // Cliente nuevo 
                        cliente.Cliente_ID = 0;
                        cliente.Nombre = client_name;
                        cliente.Telefono = client_tel;
                        cliente.Fecha_creado = GetCurrentTime();
                        //cliente.NroDocumento = txbDocumento_int;

                        context.clientes.Add(cliente);
                        Guardar_Contexto(context);

                        ret_ID = Get_NextClientID(context);
                    }
                    cliente.Nombre = client_name;
                }
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

        private bool UploadBOXAPI(formularios _formulario, string tel_str)
        {
            bool uploadBOX_ok = true;
            if (_formulario != null)
            {
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                // Source: https://github.com/box/box-windows-sdk-v2

                #region Get_Params

                //BOX User. 
                string BOX_User = "gborderolle2@gmail.com";
                if (ConfigurationManager.AppSettings != null)
                {
                    BOX_User = ConfigurationManager.AppSettings["BOX_User"].ToString();
                }

                //BOX Password. 
                string BOX_Password = "154060gb";
                if (ConfigurationManager.AppSettings != null)
                {
                    BOX_Password = ConfigurationManager.AppSettings["BOX_Password"].ToString();
                }

                //BOX Path 1. 
                string BOX_Path1 = "Cartelux";
                if (ConfigurationManager.AppSettings != null)
                {
                    BOX_Path1 = ConfigurationManager.AppSettings["BOX_Path1"].ToString();
                }

                //BOX Path 2. 
                string BOX_Path2 = "Workspace";
                if (ConfigurationManager.AppSettings != null)
                {
                    BOX_Path2 = ConfigurationManager.AppSettings["BOX_Path2"].ToString();
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
                                    //var boxConfig = new BoxConfig(BOX_User, BOX_Password, < Enterprise_Id >, < Private_Key >, < JWT_Private_Key_Password >, < JWT_Public_Key_Id >);
                                    //var boxJWT = new BoxJWTAuth(boxConfig);

                                    // Instancia de un cliente para conectar con mega.
                                    MegaApiClient cliente = new MegaApiClient();
                                    // Inicio de sesión con el cliente, pasando el correo y la contraseña de la cuenta mega a la que se sube el archivo.
                                    cliente.Login(BOX_User, BOX_Password);

                                    // Obtenemos los nodos (directorios/archivos) de la cuenta dentro de una variable.
                                    var nodos = cliente.GetNodes();

                                    // Comprobar si existe algún nodo (directorio) que se llame "Facturas" (en mi caso quiero subir el archivo a dicha carpeta).

                                    // Crear dos nodos.
                                    INode root;
                                    INode carpeta;

                                    string folder = string.Empty;

                                    #region Carpeta TRABAJOS
                                    folder = BOX_Path1;
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
                                    folder = BOX_Path2;
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
                                    uploadBOX_ok = Guardar_Contexto(context);

                                    #endregion
                                }
                                catch (Exception e)
                                {
                                    Logs.AddErrorLog("Excepcion. Copiando archivo al server y guardando en BD. ERROR:", className, methodName, e.Message);
                                    Logs.AddErrorLog("Excepcion. URL:", className, methodName, complete_URL);
                                    uploadBOX_ok = false;
                                }

                            }
                        }
                    }
                }
            }
            return uploadBOX_ok;
        }

        private void SendNotification_Email()
        {
            SmtpClient smtpClient = new SmtpClient("mail.cartelux.uy", 25);

            smtpClient.Credentials = new System.Net.NetworkCredential("proyectos@cartelux.uy", "Cartelux1234$");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("proyectos@cartelux.uy", "Cartelux");
            mail.To.Add(new MailAddress("gborderolle@gmail.com"));
            //mail.To.Add(new MailAddress("maxi.cartelux@gmail.com"));

            smtpClient.Send(mail);
        }

        private void SendNotification_Email_2(bool isColocacion_entrega = false)
        {
            // SOURCE: https://www.smarterasp.net/support/kb/a179/how-to-send-email-in-asp_net.aspx
            string nombre = txbNombre.Value;
            string telefono = txbTelefono.Value;
            string email1 = txbEmail.Value;            

            string material = "Impreso";
            if (!radImpreso1.Checked)
            {
                material = "Pintado";
            }
            string fecha_entrega = GetDatetimeFormated(txbFecha.Value).ToString(GlobalVariables.ShortDateTime_format1);
            string day_name = GetDatetimeFormated(txbFecha.Value).ToString("ddd", new CultureInfo("es-UY"));
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (!string.IsNullOrWhiteSpace(nombre) || !string.IsNullOrWhiteSpace(telefono) || !string.IsNullOrWhiteSpace(fecha_entrega) || !string.IsNullOrWhiteSpace(url))
            {
                //create the mail message 
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;

                string email_emisor = "notificaciones@cartelux.uy";
                List<string> email_receptor = new List<string>();
                List<string> email_receptor_entrega = new List<string>();
                using (CarteluxDB context = new CarteluxDB())
                {
                    List<confi_emails> lista_confi_emails = (List<confi_emails>)context.confi_emails.ToList();
                    if (lista_confi_emails != null && lista_confi_emails.Count > 0)
                    {
                        foreach (confi_emails _confi_emails in lista_confi_emails)
                        {
                            if (_confi_emails.EsEmisor)
                            {
                                email_emisor = _confi_emails.Email;
                            }
                            else
                            {
                                if (_confi_emails.EsEntrega)
                                {
                                    email_receptor_entrega.Add(_confi_emails.Email);
                                }
                                else
                                {
                                    email_receptor.Add(_confi_emails.Email);
                                }
                            }
                        }
                    }
                }

                //set the addresses 
                mail.From = new MailAddress(email_emisor); //IMPORTANT: This must be same as your smtp authentication address.
                foreach (string email in email_receptor)
                {
                    int index = email_receptor.IndexOf(email);
                    if (index == 0)
                    {
                        mail.To.Add(email);
                    }
                    else
                    {
                        mail.CC.Add(email);
                    }
                }

                if (isColocacion_entrega)
                {
                    foreach (string email in email_receptor_entrega)
                    {
                        mail.CC.Add(email); 
                    }
                }

                string telefono_aux = telefono;
                if (telefono[0].Equals('0'))
                {
                    telefono_aux = telefono.Substring(1);
                }
                string wpp_url = "https://api.whatsapp.com/send?phone=598" + telefono_aux;

                //set the content 
                mail.Subject = "CX-AVISO: " + nombre + " > " + day_name.Substring(0, 3) + " " + fecha_entrega;
                mail.Body = "<div><strong>Información básica del pedido nuevo.</strong></div>";
                mail.Body += "<br/><div><strong>Nombre:</strong> " + nombre + "</div>";
                mail.Body += "<div><strong>Teléfono:</strong> " + telefono + "</div>";
                mail.Body += "<div><strong>Material:</strong> " + material + "</div>";
                mail.Body += "<div><strong>Entrega:</strong> " + fecha_entrega + "</div>";
                mail.Body += "<br/><br/>";
                mail.Body += "<div><font size='2'><strong><span style='color:#e15211'>------------------------------<wbr>------------------------------<wbr>-------------</span></strong></font></div>";
                mail.Body += "<div><strong><span style='font-size:12pt;color:#e15211'>Accesos</span></strong></div>";
                mail.Body += "<div><strong><a href='" + url + "' title='' target='_blank'>Formulario</a></strong></div>";
                mail.Body += "<div><strong><a href='www.pedidos.cartelux.uy/Dashboard' title='' target='_blank'>Dashboard</a></strong></div>";
                mail.Body += "<div><strong><a href='" + wpp_url + "' title='' target='_blank'>WhatsApp URL</a></strong></div>";
                mail.Body += "<br/><br/>";
                mail.Body += "<div>Este es un email auto-generado, por favor no lo responda.</div>";
                mail.Body += "<div>Fecha creación: " + GetCurrentTime().ToString(GlobalVariables.ShortDateTime_format1_long) + "</div>";

                string sign_1 = "<br/><div><font size='2'><strong><span style='color:#e15211'>------------------------------<wbr>------------------------------<wbr>-------------</span></strong></font></div>";
                string sign_2 = "<div><strong><span style='font-size:8pt;color:#e15211'>Cartelux Publicidad 2018</span></strong><strong><span style='color:#4a442a'> | </span></strong><strong><span style='FONT-SIZE:8pt;'>www.cartelux.uy</span></strong></div>";
                string sign_3 = "<div><font size='2'><strong><span style='color:#e15211'>------------------------------<wbr>------------------------------<wbr>-------------</span></strong></font></div>";
                mail.Body += sign_1 + sign_2 + sign_3;

                //send the message 
                SmtpClient smtp = new SmtpClient("mail.cartelux.uy");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("notificaciones@cartelux.uy", "Cartelux1234$");
                smtp.Credentials = Credentials;
                smtp.Send(mail);
            }
        }

        private void Limpiar_Datos()
        {
            System.Web.HttpContext.Current.Session["form_serie"] = null;

            txbNombre.Value = string.Empty;
            txbTelefono.Value = string.Empty;
            txbEmail.Value = string.Empty;
            
            //txbDocumento.Value = string.Empty;
            txbDireccion_calle.Value = string.Empty;
            txbDireccion_numero.Value = string.Empty;
            txbDireccion_apto.Value = string.Empty;
            txbDireccion_esquina.Value = string.Empty;
            txbFecha.Value = string.Empty;
            txbCiudad.Value = string.Empty;

            ddlTamano1.SelectedIndex = 0;
            ddlTematica.SelectedIndex = 0;
            ddlTipoEntrega1.SelectedIndex = 0;

            radDoc1.Checked = true;
            radDoc2.Checked = false;
            radImpreso1.Checked = true;
            radImpreso2.Checked = false;
        }

        #endregion

        #region Static Methods 

        public static DateTime GetCurrentTime()
        {
            DateTime serverTime = DateTime.Now;
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, "Montevideo Standard Time");
            return _localTime;
        }

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

        #endregion

    }
}