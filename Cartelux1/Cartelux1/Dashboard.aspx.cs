using Cartelux1.Global_Objects;
using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Cartelux1.Helpers;

namespace Cartelux1
{
    public partial class Dashboard : System.Web.UI.Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_DataConfig();
                Bind_GridFormularios();
                Bind_GridProyectos();
            }

            gridFormularios.UseAccessibleHeader = true;
            gridFormularios.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridFormularios.FooterRow.TableSection = TableRowSection.TableFooter;

            gridProyectos.UseAccessibleHeader = true;
            gridProyectos.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridProyectos.FooterRow.TableSection = TableRowSection.TableFooter;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            string client_ID_str = hdn_clientID.Value;
            if (!string.IsNullOrWhiteSpace(client_ID_str))
            {
                int cliente_ID = 0;
                if (!int.TryParse(client_ID_str, out cliente_ID))
                {
                    cliente_ID = 0;
                    Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, client_ID_str);
                }

                if (cliente_ID > 0)
                {
                    //string date1 = txbFiltro1.Value;
                    //string date2 = txbFiltro2.Value;
                    //BindGridViajesImprimir(cliente_ID, date1, date2);
                }
            }
        }

        protected void btnSearch_Click_saldos(object sender, EventArgs e)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            string client_ID_str = hdn_clientID.Value;
            if (!string.IsNullOrWhiteSpace(client_ID_str))
            {
                int cliente_ID = 0;
                if (!int.TryParse(client_ID_str, out cliente_ID))
                {
                    cliente_ID = 0;
                    Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, client_ID_str);
                }

                if (cliente_ID > 0)
                {

                }
            }
        }

        protected void gridFormularios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {
                if (!string.IsNullOrWhiteSpace(e.CommandArgument.ToString()) && !string.IsNullOrWhiteSpace(e.CommandName))
                {
                    if (e.CommandName.Equals("View"))
                    {
                        string[] values = e.CommandArgument.ToString().Split(new char[] { ',' });
                        if (values.Length > 1)
                        {
                            string tabla = values[0];
                            string dato = values[1];
                            if (!string.IsNullOrWhiteSpace(tabla) && !string.IsNullOrWhiteSpace(dato))
                            {
                                Response.Redirect("Listados.aspx?tabla=" + tabla + "&dato=" + dato);
                            }
                        }
                    }
                }
            }
        }

        protected void gridProyectos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {
                if (!string.IsNullOrWhiteSpace(e.CommandArgument.ToString()) && !string.IsNullOrWhiteSpace(e.CommandName))
                {
                    if (e.CommandName.Equals("View"))
                    {

                    }
                }
            }
        }

        protected void gridFormularios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            #region Labels

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                using (CarteluxDB context = new CarteluxDB())
                {
                    formularios _formulario = (formularios)(e.Row.DataItem);
                    if (_formulario != null)
                    {
                        clientes _cliente = (clientes)context.clientes.FirstOrDefault(c => c.Cliente_ID == _formulario.Cliente_ID);
                        pedidos _pedido = (pedidos)context.pedidos.FirstOrDefault(c => c.Formulario_ID == _formulario.Cliente_ID);
                        if (_cliente != null && _pedido != null)
                        {
                            Label lbl1 = e.Row.FindControl("lblNumber") as Label;
                            if (lbl1 != null)
                            {
                                lbl1.Text = e.Row.RowIndex.ToString();
                            }

                            lbl1 = e.Row.FindControl("lblCantidad") as Label;
                            if (lbl1 != null)
                            {
                                lbl1.Text = _pedido.Cantidad.ToString();
                            }

                            lbl1 = e.Row.FindControl("lblFechaEntrega") as Label;
                            if (lbl1 != null)
                            {
                                pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
                                if (_pedido_entrega != null)
                                {
                                    string nombre = _pedido_entrega.Fecha_entrega.Value.ToString(GlobalVariables.ShortDateTime_format1);
                                    if (_pedido_entrega.Fecha_entrega == DateTime.MinValue)
                                    {
                                        nombre = string.Empty;
                                    }
                                    lbl1.Text = nombre;
                                }
                            }

                            LinkButton lbtn = e.Row.FindControl("lblTelefono") as LinkButton;
                            if (lbtn != null)
                            {
                                string nombre = string.Empty;
                                if (!string.IsNullOrWhiteSpace(_cliente.Telefono))
                                {
                                    nombre = _cliente.Telefono;
                                }
                                lbtn.Text = nombre;
                                lbtn.CommandArgument = "clientes," + nombre;
                            }

                            lbl1 = e.Row.FindControl("lblNombre") as Label;
                            if (lbl1 != null)
                            {
                                string nombre = string.Empty;
                                if (!string.IsNullOrWhiteSpace(_cliente.Nombre))
                                {
                                    nombre = _cliente.Nombre;
                                }
                                lbl1.Text = nombre;
                            }

                            lbl1 = e.Row.FindControl("lblTipoEntrega") as Label;
                            if (lbl1 != null)
                            {
                                pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
                                if (_pedido_entrega != null)
                                {
                                    lista_entregas_tipos _lista_entregas_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(c => c.Entrega_Tipo_ID == _pedido_entrega.Entrega_Tipo_ID);
                                    if (_lista_entregas_tipo != null)
                                    {
                                        string nombre = string.Empty;
                                        if (!string.IsNullOrWhiteSpace(_lista_entregas_tipo.Nombre))
                                        {
                                            nombre = _lista_entregas_tipo.Nombre;
                                        }
                                        lbl1.Text = nombre;
                                    }
                                }
                            }

                            lbl1 = e.Row.FindControl("lblTamano") as Label;
                            if (lbl1 != null)
                            {
                                /* Código tamaño cartel
                                * 1 - 150x80 Pasacalle ID:2
                                * 2 - 300x80 Pasacalle ID:3
                                * 3 - 500x80 Pasacalle ID:4
                                * 4 - Pancarta otra medida ID:5
                                * 5 - 150x80 Roll up ID:6
                                * 6 - 200x80 Roll up ID:7
                                * 7 - Banner 80x90 ID:8
                                * 8 - Banner otra medida ID:9
                                * 9 - 200x80 Pasacalle ID:10
                                * 
                                * ---- ATENCIÓN: Desde 22 Julio 2019
                                * Códigos nuevos:
                                1 - Pasacalle	
                                2 - Roll up	
                                3 - Banner	
                                4 - Cartelería	

                                5 - total_lonas
                                6 - total_vinilos	
                                7 - total_banderas	
                                8 - total_tercializaciones	
                                9 - total_otros	
                                * */

                                string tamano_tipo_ID_str = _pedido.Pedido_Tamano_ID.ToString(); // Se usa ID, no código
                                if (_formulario.Fecha_update <= GlobalVariables.GetDatetimeFormated("22-07-2019")) //dd-MM-yyyy
                                {
                                    switch (tamano_tipo_ID_str)
                                    {
                                        case "2":
                                        case "3":
                                        case "4":
                                        case "5":
                                        case "10":
                                            {
                                                // Pasacalles
                                                tamano_tipo_ID_str = "2"; // ID, no código
                                                break;
                                            }
                                        case "6":
                                        case "7":
                                            {
                                                // Roll ups
                                                tamano_tipo_ID_str = "3";
                                                break;
                                            }
                                        case "8":
                                        case "9":
                                            {
                                                // Banners
                                                tamano_tipo_ID_str = "4";
                                                break;
                                            }
                                    }
                                }
                                int _tamano_tipo_ID = 0;
                                if (!int.TryParse(tamano_tipo_ID_str, out _tamano_tipo_ID))
                                {
                                    _tamano_tipo_ID = _pedido.Pedido_Tamano_ID;
                                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, tamano_tipo_ID_str);
                                }
                                lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(c => c.Pedido_Tamano_ID == _tamano_tipo_ID);

                                //lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(c => c.Pedido_Tamano_ID == _pedido.Pedido_Tamano_ID);
                                if (_lista_pedido_tamano != null)
                                {
                                    string nombre = string.Empty;
                                    if (!string.IsNullOrWhiteSpace(_lista_pedido_tamano.Nombre))
                                    {
                                        nombre = _lista_pedido_tamano.Nombre;
                                    }
                                    lbl1.Text = nombre;
                                }
                            }

                            lbl1 = e.Row.FindControl("lblTamanoReal") as Label;
                            if (lbl1 != null)
                            {
                                string tamanoReal = "N/D";
                                if (!string.IsNullOrWhiteSpace(_pedido.Tamano_real))
                                {
                                    tamanoReal = _pedido.Tamano_real;
                                }
                                lbl1.Text = tamanoReal;
                            }

                            //lbl1 = e.Row.FindControl("lblTipo") as Label;
                            //if (lbl1 != null)
                            //{
                            //    lista_productos _lista_pedido_tipo = (lista_productos)context.lista_productos.FirstOrDefault(c => c.Pedido_Tipo_ID == _pedido.Pedido_Tipo_ID);
                            //    if (_lista_pedido_tipo != null)
                            //    {
                            //        string nombre = string.Empty;
                            //        if (!string.IsNullOrWhiteSpace(_lista_pedido_tipo.Nombre))
                            //        {
                            //            nombre = _lista_pedido_tipo.Nombre;
                            //        }
                            //        lbl1.Text = nombre;
                            //    }
                            //}

                            lbl1 = e.Row.FindControl("lblTematica") as Label;
                            if (lbl1 != null)
                            {
                                lista_pedido_tematicas _lista_pedido_tematica = (lista_pedido_tematicas)context.lista_pedido_tematicas.FirstOrDefault(c => c.Pedido_Tematica_ID == _pedido.Pedido_Tematica_ID);
                                if (_lista_pedido_tematica != null)
                                {
                                    lbl1.Text = _lista_pedido_tematica.Nombre;
                                }
                            }

                            lbl1 = e.Row.FindControl("lblMedioP") as Label;
                            if (lbl1 != null)
                            {
                                lista_pedido_mediosDePago _lista_pedido_mediosDePago = (lista_pedido_mediosDePago)context.lista_pedido_mediosDePago.FirstOrDefault(c => c.Pedido_mediosDePago_ID == _pedido.Pedido_MedioDePago_ID);
                                if (_lista_pedido_mediosDePago != null)
                                {
                                    lbl1.Text = _lista_pedido_mediosDePago.Nombre;
                                }
                            }

                            lbl1 = e.Row.FindControl("lblMonto") as Label;
                            if (lbl1 != null)
                            {
                                lbl1.Text = _formulario.Monto.ToString();
                            }

                            lbl1 = e.Row.FindControl("lblUsuario") as Label;
                            if (lbl1 != null)
                            {
                                usuarios _usuario = (usuarios)context.usuarios.FirstOrDefault(c => c.Usuario_ID == _pedido.Pedido_Estado_UltimaModificacion_Usuario_ID);
                                if (_usuario != null)
                                {
                                    string nombre = string.Empty;
                                    if (!string.IsNullOrWhiteSpace(_usuario.Usuario))
                                    {
                                        nombre = _usuario.Usuario;
                                    }
                                    lbl1.Text = nombre;
                                }
                            }

                            //lbl1 = e.Row.FindControl("lblZona") as Label;
                            //if (lbl1 != null)
                            //{
                            //    pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
                            //    if (_pedido_entrega != null)
                            //    {
                            //        string nombre = string.Empty;
                            //        if (!string.IsNullOrWhiteSpace(_pedido_entrega.Barrio))
                            //        {
                            //            nombre = _pedido_entrega.Barrio;
                            //        }
                            //        lbl1.Text = nombre;
                            //    }
                            //}

                            //LinkButton btnGMaps = e.Row.FindControl("btnGMaps") as LinkButton;
                            //if (btnGMaps != null)
                            //{
                            //    btnGMaps.Text = string.Empty;
                            //    pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
                            //    if (_pedido_entrega != null)
                            //    {
                            //        string value = "#";
                            //        if (!string.IsNullOrWhiteSpace(_pedido_entrega.Google_maps_URL) && _pedido_entrega.Google_maps_URL != "0")
                            //        {
                            //            value = _pedido_entrega.Google_maps_URL;
                            //        }
                            //        btnGMaps.Text = value;
                            //    }
                            //}

                        }
                    }
                }

                #endregion Labels

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridFormularios, "Select$" + e.Row.RowIndex);
                }
            }
        }

        protected void gridProyectos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region Labels

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                using (CarteluxDB context = new CarteluxDB())
                {
                    proyectos _proyecto = (proyectos)(e.Row.DataItem);
                    if (_proyecto != null)
                    {
                        Label lbl1 = e.Row.FindControl("lblNumber_proy") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = e.Row.RowIndex.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblNombre_proy") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _proyecto.Nombre.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblDescripcion_proy") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _proyecto.Descripcion.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblFechaEstimada") as Label;
                        if (lbl1 != null)
                        {
                            string fecha = _proyecto.Fecha_estimada.Value.ToString(GlobalVariables.ShortDateTime_format1);
                            if (_proyecto.Fecha_estimada.Value == DateTime.MinValue)
                            {
                                fecha = string.Empty;
                            }
                            lbl1.Text = fecha;
                        }

                        lbl1 = e.Row.FindControl("lblContacto1") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _proyecto.Contacto_1_nombre.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblTelefono1") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _proyecto.Contacto_1_telefono.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblEmail1") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _proyecto.Contacto_1_email.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblContacto2") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _proyecto.Contacto_2_nombre.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblTelefono2") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _proyecto.Contacto_2_telefono.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblEmail2") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _proyecto.Contacto_2_email.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblComentarios_proy") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _proyecto.Comentarios.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblEstado_proy") as Label;
                        if (lbl1 != null)
                        {
                            string nombre = string.Empty;
                            lista_proyecto_estados _proyectoEstado = (lista_proyecto_estados)context.lista_proyecto_estados.FirstOrDefault(c => c.Proyecto_Estado_ID == _proyecto.Proyecto_estado_ID);
                            if (_proyectoEstado != null)
                            {
                                nombre = _proyectoEstado.Nombre;
                            }
                            lbl1.Text = nombre.ToString();
                        }

                    }
                }

                #endregion Labels

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridFormularios, "Select$" + e.Row.RowIndex);
                }
            }
        }

        protected void gridFormularios_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Source: http://www.codeproject.com/Tips/622720/Fire-GridView-SelectedIndexChanged-Event-without-S

            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            foreach (GridViewRow row in gridFormularios.Rows)
            {
                if (row.RowIndex == gridFormularios.SelectedIndex)
                {
                    string Formulario_ID_str = gridFormularios.SelectedRow.Cells[0].Text;
                    if (!string.IsNullOrWhiteSpace(Formulario_ID_str))
                    {
                        int Formulario_ID = 0;
                        if (!int.TryParse(Formulario_ID_str, out Formulario_ID))
                        {
                            Formulario_ID = 0;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, Formulario_ID_str);
                        }

                        if (Formulario_ID > 0)
                        {
                            using (CarteluxDB context = new CarteluxDB())
                            {
                                formularios _formulario = (formularios)context.formularios.FirstOrDefault(c => c.Formulario_ID == Formulario_ID);
                                if (_formulario != null)
                                {
                                    //lblClientName_1.Text = lblClientName_2.Text = _formulario.Nombre;

                                    //BindGridViajes(cliente_ID);

                                    // Filtrar por fechas del mes corriente por defecto
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "gridFormularios_OnSelectedIndexChanged", "<script type='text/javascript'></script>", false);

                                    hdn_FormularioID.Value = Formulario_ID_str;
                                }
                            }
                        }
                    }
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    }
                }
            }
        }

        protected void gridProyectos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Source: http://www.codeproject.com/Tips/622720/Fire-GridView-SelectedIndexChanged-Event-without-S

            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            foreach (GridViewRow row in gridProyectos.Rows)
            {
                if (row.RowIndex == gridProyectos.SelectedIndex)
                {
                    string Proyecto_ID_str = gridProyectos.SelectedRow.Cells[0].Text;
                    if (!string.IsNullOrWhiteSpace(Proyecto_ID_str))
                    {
                        int Proyecto_ID = 0;
                        if (!int.TryParse(Proyecto_ID_str, out Proyecto_ID))
                        {
                            Proyecto_ID = 0;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, Proyecto_ID_str);
                        }

                        if (Proyecto_ID > 0)
                        {
                            using (CarteluxDB context = new CarteluxDB())
                            {
                                proyectos _proyecto = (proyectos)context.proyectos.FirstOrDefault(c => c.Proyecto_ID == Proyecto_ID);
                                if (_proyecto != null)
                                {
                                    // Filtrar por fechas del mes corriente por defecto
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "gridProyectos_OnSelectedIndexChanged", "<script type='text/javascript'></script>", false);

                                    hdn_proyectoID.Value = Proyecto_ID_str;
                                }
                            }
                        }
                    }
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    }
                }
            }
        }


        #endregion Events

        #region Methods

        private void Bind_DataConfig()
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            using (CarteluxDB context = new CarteluxDB())
            {
                // DDL Years
                DataTable dt1 = new DataTable();
                dt1 = Extras.ToDataTable(context.config_formularios_anos.OrderBy(e => e.Nombre).ToList());
                ddl_year.DataSource = dt1;
                ddl_year.DataTextField = "Nombre";
                ddl_year.DataValueField = "Config_formularios_ano_ID";
                ddl_year.DataBind();
                ddl_year.Items.Insert(0, new ListItem("Elegir", "0"));
                ddl_year.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        private void Bind_GridFormularios(string year = "", string month = "", bool solo_vigentes = true)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            int current_year = DateTime.Now.Year;
            int current_month = DateTime.Now.Month;

            // Current datetime
            DateTime datetime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(year) && !string.IsNullOrWhiteSpace(month))
            {
                int year_int = 0;
                if (!int.TryParse(year, out year_int))
                {
                    year_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, year);
                }

                int month_int = 0;
                if (!int.TryParse(month, out month_int))
                {
                    month_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, month);
                }

                if (year_int > 0 && month_int > 0)
                {
                    datetime = new DateTime(year_int, month_int, 1);
                }

                current_year = year_int;
                current_month = month_int;
            }

            // If is future month
            if (current_year >= DateTime.Now.Year && current_month > DateTime.Now.Month)
            {
                solo_vigentes = false;
            }

            List<formularios> formularios_elements = GetFormularios_pre(solo_vigentes, current_year, current_month);

            // Reportes - Todos los formularios
            //List<formularios> formularios_elements_reportes = GetFormularios_pre(false, current_year, current_month);

            #region Fill reports data

            //if (formularios_elements_reportes.Count() > 0)
            //{
            //    int totalPedidos = formularios_elements_reportes.Count();
            //    pnl_rpt_totalPedidos.Controls.Clear();
            //    pnl_rpt_totalPedidos.Controls.Add(new LiteralControl(totalPedidos.ToString()));
            //    //lbl_rpt_totalPedidos.Text = totalPedidos.ToString();
            //}

            #endregion Fill reports data


            #region Fill gridFormularios

            if (formularios_elements.Count() > 0)
            {
                gridFormularios.DataSource = formularios_elements;
                gridFormularios.DataBind();

                gridFormularios.UseAccessibleHeader = true;
                gridFormularios.HeaderRow.TableSection = TableRowSection.TableHeader;

                lblgridFormulariosCount.Text = "# " + formularios_elements.Count();
            }
            else
            {
                var obj = new List<formularios>();
                formularios _formulario1 = new formularios();
                _formulario1.Formulario_ID = 1;
                _formulario1.Cliente_ID = 1;
                _formulario1.URL_short = string.Empty;
                _formulario1.URL_completa = string.Empty;
                _formulario1.Fecha_creado = DateTime.MinValue;
                _formulario1.Fecha_update = DateTime.MinValue;
                _formulario1.Comentarios = string.Empty;
                _formulario1.Monto = 0;
                _formulario1.Serie = string.Empty;

                obj.Add(_formulario1);

                // Bind the DataTable which contain a blank row to the GridView
                gridFormularios.DataSource = obj;
                gridFormularios.DataBind();
                int columnsCount = gridFormularios.Columns.Count;
                gridFormularios.Rows[0].Cells.Clear();// clear all the cells in the row
                gridFormularios.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                gridFormularios.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

                //You can set the styles here
                gridFormularios.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                gridFormularios.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                gridFormularios.Rows[0].Cells[0].Font.Bold = true;

                //set No Results found to the new added cell
                gridFormularios.Rows[0].Cells[0].Text = "No hay registros";
            }

            #endregion
        }

        private void Bind_GridProyectos(string year = "", string month = "")
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            int current_year = DateTime.Now.Year;
            int current_month = DateTime.Now.Month;

            // Current datetime
            DateTime datetime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(year) && !string.IsNullOrWhiteSpace(month))
            {
                int year_int = 0;
                if (!int.TryParse(year, out year_int))
                {
                    year_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, year);
                }

                int month_int = 0;
                if (!int.TryParse(month, out month_int))
                {
                    month_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, month);
                }

                if (year_int > 0 && month_int > 0)
                {
                    datetime = new DateTime(year_int, month_int, 1);
                }

                current_year = year_int;
                current_month = month_int;
            }

            List<proyectos> proyectos_elements = GetProyectos_pre(current_year, current_month);

            // Reportes - Todos los formularios
            //List<formularios> formularios_elements_reportes = GetFormularios_pre(false, current_year, current_month);

            #region Fill gridProyectos

            if (proyectos_elements.Count() > 0)
            {
                gridProyectos.DataSource = proyectos_elements;
                gridProyectos.DataBind();

                gridProyectos.UseAccessibleHeader = true;
                gridProyectos.HeaderRow.TableSection = TableRowSection.TableHeader;

                lblgridProyectosCount.Text = "# " + proyectos_elements.Count();
            }
            else
            {
                var obj = new List<proyectos>();
                proyectos _proyecto1 = new proyectos();
                _proyecto1.Proyecto_ID = 1;
                _proyecto1.Nombre = string.Empty;
                _proyecto1.Descripcion = string.Empty;
                _proyecto1.Fecha_estimada = DateTime.MinValue;
                _proyecto1.Fecha_creado = DateTime.MinValue;
                _proyecto1.Fecha_update = DateTime.MinValue;
                _proyecto1.Contacto_1_nombre = string.Empty;
                _proyecto1.Contacto_1_telefono = string.Empty;
                _proyecto1.Contacto_1_email = string.Empty;
                _proyecto1.Contacto_2_nombre = string.Empty;
                _proyecto1.Contacto_2_telefono = string.Empty;
                _proyecto1.Contacto_2_email = string.Empty;
                _proyecto1.Comentarios = string.Empty;
                _proyecto1.Proyecto_estado_ID = 1;

                obj.Add(_proyecto1);

                // Bind the DataTable which contain a blank row to the GridView
                gridProyectos.DataSource = obj;
                gridProyectos.DataBind();
                int columnsCount = gridProyectos.Columns.Count;
                gridProyectos.Rows[0].Cells.Clear();// clear all the cells in the row
                gridProyectos.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                gridProyectos.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

                //You can set the styles here
                gridProyectos.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                gridProyectos.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                gridProyectos.Rows[0].Cells[0].Font.Bold = true;

                //set No Results found to the new added cell
                gridProyectos.Rows[0].Cells[0].Text = "No hay registros";
            }

            #endregion
        }


        private List<formularios> GetFormularios_pre(bool solo_vigentes, int current_year, int current_month)
        {
            using (CarteluxDB context = new CarteluxDB())
            {
                int day_value = 1;
                if (solo_vigentes)
                {
                    day_value = DateTime.Now.Day;
                }

                DateTime date1 = new DateTime(current_year, current_month, 1);
                int last_day = DateTime.DaysInMonth(current_year, current_month);
                DateTime date2 = new DateTime(current_year, current_month, last_day);

                return GetFormularios_PorMes(context, date1, date2);
            }
        }

        public static List<formularios> GetFormularios_PorMes(CarteluxDB context, DateTime date1, DateTime date2, bool incluye_cancelados = false, bool chbSoloArteRefinado_value = false)
        {
            List<formularios> formularios_elements = new List<formularios>();
            if (context != null)
            {
                // Get Entregas
                var pedidos_entregas_element = context.pedido_entregas.Where(v => v.Fecha_entrega >= date1 && v.Fecha_entrega <= date2).OrderBy(e => e.Fecha_entrega).ToList();
                foreach (pedido_entregas _pedido_entrega in pedidos_entregas_element)
                {
                    // Get Pedidos
                    var pedidos_elements = context.pedidos.Where(v => v.Pedido_Entrega_ID == _pedido_entrega.Pedido_Entrega_ID).ToList();
                    foreach (pedidos _pedido in pedidos_elements)
                    {
                        // Filtro NO incluye cancelados
                        if (!incluye_cancelados)
                        {
                            lista_pedido_estados _pedidoEstado = (lista_pedido_estados)context.lista_pedido_estados.FirstOrDefault(c => c.Pedido_Estado_ID == _pedido.Pedido_Estado_ID);
                            if (_pedidoEstado != null)
                            {
                                // Estado 2 = Cancelado || Estado 3 = Borrado 
                                //if ((_pedido.Pedido_Estado_ID == _pedidoEstado.Pedido_Estado_ID && (_pedidoEstado.Codigo == 2 || _pedidoEstado.Codigo == 3)))
                                if (_pedidoEstado.Codigo == 2 || _pedidoEstado.Codigo == 3)
                                {
                                    // Si es Cancelado o Borrado descarta este pedido
                                    continue;
                                }
                            }
                        }

                        // Si NO es ARTE REFINADO descarta este pedido
                        if (chbSoloArteRefinado_value)
                        {
                            // Tamaño 10 = ARTE REFINADO
                            lista_pedido_tamanos _pedidoTamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(c => c.Pedido_Tamano_ID == _pedido.Pedido_Tamano_ID);
                            if (_pedidoTamano != null)
                            {
                                if (_pedidoTamano.Codigo == 10)
                                {
                                    // Es AR, agrega el Form
                                    int pedido_entrega_ID = _pedido.Pedido_Entrega_ID;
                                    formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Formulario_ID == _pedido.Formulario_ID);
                                    if (_formulario != null)
                                    {
                                        // Add Formulario
                                        formularios_elements.Add(_formulario);
                                    }
                                }
                            }
                        }
                        else
                        {
                            int pedido_entrega_ID = _pedido.Pedido_Entrega_ID;
                            formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Formulario_ID == _pedido.Formulario_ID);
                            if (_formulario != null)
                            {
                                // Add Formulario
                                formularios_elements.Add(_formulario);
                            }
                        }

                    } // foreach pedidos
                } // foreach pedido_entregas


            }
            return formularios_elements;
        }

        public static int? GetFormularios_PorMes_GetMonto(CarteluxDB context, DateTime date1, DateTime date2)
        {
            int? monto = 0;
            if (context != null)
            {
                List<formularios> formularios_elements = new List<formularios>();
                formularios_elements = GetFormularios_PorMes(context, date1, date2);
                if (formularios_elements != null && formularios_elements.Count > 0)
                {
                    monto = formularios_elements.Sum(v => v.Monto);
                }
            }
            return monto;
        }

        private List<proyectos> GetProyectos_pre(int current_year, int current_month)
        {
            using (CarteluxDB context = new CarteluxDB())
            {
                int day_value = DateTime.Now.Day;
                DateTime date1 = new DateTime(current_year, current_month, 1);
                int last_day = DateTime.DaysInMonth(current_year, current_month);
                DateTime date2 = new DateTime(current_year, current_month, last_day);

                return context.proyectos.Where(v => v.Fecha_creado >= date1 && v.Fecha_creado <= date2).OrderBy(e => e.Fecha_creado).ToList();
            }
        }

        #endregion

        #region Static Methods

        [WebMethod]
        public static _GridFormularios[] GetData_BindGridFormularios(string year_value, string month_value, bool soloVigentes_value, bool soloEntrCol_value, bool incluirCancelados_value, bool chbSoloArteRefinado_value)
        {
            List<_GridFormularios> _GridFormularios_list = new List<_GridFormularios>();
            if (!string.IsNullOrWhiteSpace(year_value) && !string.IsNullOrWhiteSpace(month_value))
            {
                // Logger variables
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                int year_int = 0;
                if (!int.TryParse(year_value, out year_int))
                {
                    year_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, year_value);
                }

                int month_int = 0;
                if (!int.TryParse(month_value, out month_int))
                {
                    month_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, month_value);
                }

                // Source: https://www.codeproject.com/Tips/775585/Bind-Gridview-using-AJAX
                if (year_int > 0 && month_int > 0)
                {
                    using (CarteluxDB context = new CarteluxDB())
                    {
                        // Si es mes futuro
                        if (year_int >= DateTime.Now.Year && month_int > DateTime.Now.Month)
                        {
                            soloVigentes_value = false;
                        }

                        // Si es mes pasado
                        if (year_int <= DateTime.Now.Year && month_int < DateTime.Now.Month)
                        {
                            soloVigentes_value = false;
                        }

                        int day_value = 1;
                        if (soloVigentes_value)
                        {
                            day_value = DateTime.Now.Day;
                        }

                        DateTime date1 = new DateTime(year_int, month_int, day_value);
                        int last_day = DateTime.DaysInMonth(year_int, month_int);
                        DateTime date2 = new DateTime(year_int, month_int, last_day);

                        int? importeAcumuladoMes = 0;
                        int number = 1;
                        List<formularios> formularios_elements = GetFormularios_PorMes(context, date1, date2, incluirCancelados_value, chbSoloArteRefinado_value);
                        foreach (formularios _formulario in formularios_elements)
                        {
                            if (_formulario != null)
                            {
                                _GridFormularios _GridFormulario1 = new Cartelux1.Dashboard._GridFormularios();
                                _GridFormulario1.Formulario_ID = _formulario.Formulario_ID.ToString();
                                _GridFormulario1.URL_short = _formulario.URL_short;
                                _GridFormulario1.EstadoNro = 0;

                                _GridFormulario1.lblComentarios = _formulario.Comentarios;

                                /*
                                 * Estados de pedidos:
                                 * 0: Vigente
                                 * 1: Concluído
                                 * 2: Cancelado
                                 * 3: Eliminado
                                 * 4: Diseño OK, pronto para imprimir
                                 * */
                                bool es_cancelado = false;

                                clientes _cliente = (clientes)context.clientes.FirstOrDefault(c => c.Cliente_ID == _formulario.Cliente_ID);
                                pedidos _pedido = (pedidos)context.pedidos.FirstOrDefault(c => c.Formulario_ID == _formulario.Formulario_ID);
                                if (_cliente != null && _pedido != null)
                                {
                                    _GridFormulario1.lblTelefono = _cliente.Telefono;
                                    _GridFormulario1.lblNombre = _cliente.Nombre;
                                    _GridFormulario1.lblNumero = number.ToString();

                                    lista_pedido_estados _pedidoEstado = (lista_pedido_estados)context.lista_pedido_estados.FirstOrDefault(c => c.Pedido_Estado_ID == _pedido.Pedido_Estado_ID);
                                    if (_pedidoEstado != null)
                                    {
                                        _GridFormulario1.EstadoNro = _pedidoEstado.Codigo;
                                        if (_pedidoEstado.Codigo == 2 || _pedidoEstado.Codigo == 3) // Si Cancelado o Eliminado, no concluye después
                                        {
                                            es_cancelado = true;
                                        }
                                    }

                                    #region Pedido Entrega ---------------------------------------------------------------------------------------------------------
                                    pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
                                    if (_pedido_entrega != null)
                                    {
                                        _GridFormulario1.lblFechaEntrega = _pedido_entrega.Fecha_entrega.Value.ToString(GlobalVariables.ShortDateTime_format1);
                                        _GridFormulario1.lblZona = _pedido_entrega.Barrio;

                                        string value = "#";
                                        if (!string.IsNullOrWhiteSpace(_pedido_entrega.Google_maps_URL) && _pedido_entrega.Google_maps_URL != "0")
                                        {
                                            value = _pedido_entrega.Google_maps_URL;
                                        }
                                        _GridFormulario1.URL_gmaps = value;

                                        if (!es_cancelado)
                                        {
                                            // Check vencimiento
                                            if (_pedido_entrega.Fecha_entrega.Value.Month < DateTime.Now.Month ||
                                                (_pedido_entrega.Fecha_entrega.Value.Month == DateTime.Now.Month && _pedido_entrega.Fecha_entrega.Value.Day < DateTime.Now.Day))
                                            {
                                                _GridFormulario1.EstadoNro = 1; // Concluídos
                                            }
                                        }
                                        // Filtro Entregas o Colocaciones
                                        /*
                                        * Codigo = 1: Colocación
                                        * Codigo = 2: Envío a domicilio
                                        * Codigo = 3: Envío al interior
                                        * Codigo = 4: Retiro en el taller
                                        * */
                                        lista_entregas_tipos _lista_entregas_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(c => c.Entrega_Tipo_ID == _pedido_entrega.Entrega_Tipo_ID);
                                        if (_lista_entregas_tipo != null)
                                        {
                                            _GridFormulario1.lblTipoEntrega = _lista_entregas_tipo.Nombre;
                                            _GridFormulario1.lblTipoCodigo = _lista_entregas_tipo.Codigo.ToString();

                                            if (soloEntrCol_value && (_lista_entregas_tipo.Codigo != 1 && _lista_entregas_tipo.Codigo != 2 && _lista_entregas_tipo.Codigo != 3))
                                            {
                                                continue;
                                            }
                                        }

                                    }
                                    #endregion END Pedido Entrega

                                    #region Pedido Diseño ---------------------------------------------------------------------------------------------------------
                                    pedido_disenos _pedido_diseno = (pedido_disenos)context.pedido_disenos.FirstOrDefault(c => c.Pedido_Diseno_ID == _pedido.Pedido_Diseno_ID);
                                    if (_pedido_diseno != null)
                                    {
                                        _GridFormulario1.chbTieneBosquejo = string.IsNullOrWhiteSpace(_pedido_diseno.Boceto_nombre) ? false : true;
                                        _GridFormulario1.lblDisenoReferido = _pedido_diseno.Diseno_referido;
                                    }
                                    #endregion END Pedido Diseño

                                    #region Pedido Tamaño ---------------------------------------------------------------------------------------------------------

                                    /* Código tamaño cartel
                                    * 1 - 150x80 Pasacalle ID:2
                                    * 2 - 300x80 Pasacalle ID:3
                                    * 3 - 500x80 Pasacalle ID:4
                                    * 4 - Pancarta otra medida ID:5
                                    * 5 - 150x80 Roll up ID:6
                                    * 6 - 200x80 Roll up ID:7
                                    * 7 - Banner 80x90 ID:8
                                    * 8 - Banner otra medida ID:9
                                    * 9 - 200x80 Pasacalle ID:10
                                    * 
                                    * ---- ATENCIÓN: Desde 22 Julio 2019
                                    * Códigos nuevos:
                                    1 - Pasacalle	
                                    2 - Roll up	
                                    3 - Banner	
                                    4 - Cartelería	
                                    * */

                                    string tamano_tipo_ID_str = _pedido.Pedido_Tamano_ID.ToString(); // Se usa ID, no código
                                    if (_formulario.Fecha_update <= GlobalVariables.GetDatetimeFormated("22-07-2019")) //dd-MM-yyyy
                                    {
                                        switch (tamano_tipo_ID_str)
                                        {
                                            case "2":
                                            case "3":
                                            case "4":
                                            case "5":
                                            case "10":
                                                {
                                                    // Pasacalles
                                                    tamano_tipo_ID_str = "2"; // ID, no código
                                                    break;
                                                }
                                            case "6":
                                            case "7":
                                                {
                                                    // Roll ups
                                                    tamano_tipo_ID_str = "3";
                                                    break;
                                                }
                                            case "8":
                                            case "9":
                                                {
                                                    // Banners
                                                    tamano_tipo_ID_str = "4";
                                                    break;
                                                }
                                        }
                                    }

                                    int _tamano_tipo_ID = 0;
                                    if (!int.TryParse(tamano_tipo_ID_str, out _tamano_tipo_ID))
                                    {
                                        _tamano_tipo_ID = _pedido.Pedido_Tamano_ID;
                                        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, tamano_tipo_ID_str);
                                    }
                                    
                                    //lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(c => c.Pedido_Tamano_ID == _pedido.Pedido_Tamano_ID);
                                    lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(c => c.Pedido_Tamano_ID == _tamano_tipo_ID);
                                    if (_lista_pedido_tamano != null)
                                    {
                                        _GridFormulario1.lblTipoCartelCodigo = _lista_pedido_tamano.Codigo.ToString();
                                        _GridFormulario1.lblTamano = _lista_pedido_tamano.Nombre;
                                        _GridFormulario1.lblTamano_largo_cm = _lista_pedido_tamano.Descripcion;
                                    }
                                    string _tamano_real = _pedido.Tamano_real;
                                    if (string.IsNullOrWhiteSpace(_pedido.Tamano_real))
                                    {
                                        _tamano_real = "N/D";
                                    }
                                    _GridFormulario1.lblTamanoReal = _tamano_real;
                                    #endregion END Pedido Tamaño

                                    #region Pedido Tipo ---------------------------------------------------------------------------------------------------------
                                    lista_productos _lista_pedido_tipo = (lista_productos)context.lista_productos.FirstOrDefault(c => c.Producto_ID == _pedido.Pedido_Tipo_ID);
                                    if (_lista_pedido_tipo != null)
                                    {
                                        if (!string.IsNullOrWhiteSpace(_lista_pedido_tipo.Nombre))
                                        {
                                            _GridFormulario1.lblTipo = _lista_pedido_tipo.Nombre;
                                        }
                                    }
                                    // WORKAROUND
                                    if (string.IsNullOrWhiteSpace(_GridFormulario1.lblTipo))
                                    {
                                        _GridFormulario1.lblTipo = "Pasacalle";
                                    }

                                    #endregion END Pedido Tipo

                                    #region Pedido Medio de pago ---------------------------------------------------------------------------------------------------------
                                    _GridFormulario1.lblMedioP = "N/D";
                                    lista_pedido_mediosDePago _lista_pedido_mediosDePago = (lista_pedido_mediosDePago)context.lista_pedido_mediosDePago.FirstOrDefault(c => c.Pedido_mediosDePago_ID == _pedido.Pedido_MedioDePago_ID);
                                    if (_lista_pedido_mediosDePago != null && !string.IsNullOrWhiteSpace(_lista_pedido_mediosDePago.Nombre))
                                    {
                                        _GridFormulario1.lblMedioP = _lista_pedido_mediosDePago.Nombre;
                                    }
                                    #endregion END Pedido Medio de pago

                                    #region Pedido Monto ---------------------------------------------------------------------------------------------------------
                                    _GridFormulario1.lblMonto = _formulario.Monto;                                    
                                    #endregion END Monto

                                    #region Pedido Temática ---------------------------------------------------------------------------------------------------------
                                    _GridFormulario1.lblTematica = "N/D";
                                    lista_pedido_tematicas _lista_pedido_tematica = (lista_pedido_tematicas)context.lista_pedido_tematicas.FirstOrDefault(c => c.Pedido_Tematica_ID == _pedido.Pedido_Tematica_ID);
                                    if (_lista_pedido_tematica != null && !string.IsNullOrWhiteSpace(_lista_pedido_tematica.Nombre))
                                    {
                                        _GridFormulario1.lblTematica = _lista_pedido_tematica.Nombre;
                                    }
                                    #endregion END Pedido Temática

                                    #region Pedido Usuario que modifica ---------------------------------------------------------------------------------------------------------
                                    _GridFormulario1.lblUsuario = "N/D";
                                    usuarios _usuario = (usuarios)context.usuarios.FirstOrDefault(c => c.Usuario_ID == _pedido.Pedido_Estado_UltimaModificacion_Usuario_ID);
                                    if (_usuario != null && !string.IsNullOrWhiteSpace(_usuario.Usuario))
                                    {
                                        _GridFormulario1.lblUsuario = _usuario.Usuario;
                                    }
                                    #endregion END Pedido Usuario que modifica

                                }
                                importeAcumuladoMes += _GridFormulario1.lblMonto;
                                _GridFormulario1.importeAcumuladoMes = importeAcumuladoMes;
                                _GridFormularios_list.Add(_GridFormulario1);
                            }
                            number++;
                        } // foreach
                    }
                }
            }
            return _GridFormularios_list.ToArray();
        }

        [WebMethod]
        public static int? GetData_BindGridFormularios_MesAnterior(string year_value, string month_value)
        {
            int? monto_mesAnterior = 0;
            if (!string.IsNullOrWhiteSpace(year_value) && !string.IsNullOrWhiteSpace(month_value))
            {
                // Logger variables
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                int year_int = 0;
                if (!int.TryParse(year_value, out year_int))
                {
                    year_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, year_value);
                }

                int month_int = 0;
                if (!int.TryParse(month_value, out month_int))
                {
                    month_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, month_value);
                }

                if (year_int > 0 && month_int > 0)
                {
                    using (CarteluxDB context = new CarteluxDB())
                    {

                        int day_value = 1;

                        DateTime date1 = new DateTime(year_int, month_int, day_value);
                        int last_day = DateTime.DaysInMonth(year_int, month_int);
                        DateTime date2 = new DateTime(year_int, month_int, last_day);

                        monto_mesAnterior = GetFormularios_PorMes_GetMonto(context, date1, date2);
                    }
                }
            }
            return monto_mesAnterior;
        }

        public class _GridFormularios
        {
            public string lblNumero { get; set; }
            public string lblTelefono { get; set; }
            public string lblNombre { get; set; }
            public string lblFechaEntrega { get; set; }
            public string lblTipoEntrega { get; set; }
            public string lblTipoCodigo { get; set; }
            public string lblTamano { get; set; }
            public string lblTamanoReal { get; set; }
            public string lblTamano_largo_cm { get; set; }
            public string lblTipo { get; set; }
            public string lblMedioP { get; set; }
            public int? lblMonto { get; set; }
            public int? lblMonto_mesAnterior { get; set; }
            public string lblTematica { get; set; }
            public string lblUsuario { get; set; }
            public int lblCantidad { get; set; }
            public string lblZona { get; set; }
            public string Formulario_ID { get; set; }
            public string URL_short { get; set; }
            public string URL_gmaps { get; set; }
            public bool chbTieneBosquejo { get; set; }
            public string lblDisenoReferido { get; set; }
            public int EstadoNro { get; set; }
            public string lblTipoCartelCodigo { get; set; }
            public string lblComentarios { get; set; }
            public int? importeAcumuladoMes { get; set; }
        }

        public class _GridProyectos
        {
            public string Proyecto_ID { get; set; }
            public string lblNumber_proy { get; set; }
            public string lblNombre_proy { get; set; }
            public string lblDescripcion_proy { get; set; }
            public string lblFechaEstimada { get; set; }
            public string lblContacto1 { get; set; }
            public string lblTelefono1 { get; set; }
            public string lblEmail1 { get; set; }
            public string lblContacto2 { get; set; }
            public string lblTelefono2 { get; set; }
            public string lblEmail2 { get; set; }
            public string lblComentarios_proy { get; set; }
            public string lblEstado_proy { get; set; }
        }

        [WebMethod]
        public static _GridProyectos[] GetData_BindGridProyectos(string year_value, string month_value)
        {
            List<_GridProyectos> _GridProyectos_list = new List<_GridProyectos>();
            if (!string.IsNullOrWhiteSpace(year_value) && !string.IsNullOrWhiteSpace(month_value))
            {
                // Logger variables
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                int year_int = 0;
                if (!int.TryParse(year_value, out year_int))
                {
                    year_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, year_value);
                }

                int month_int = 0;
                if (!int.TryParse(month_value, out month_int))
                {
                    month_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, month_value);
                }

                // Source: https://www.codeproject.com/Tips/775585/Bind-Gridview-using-AJAX
                if (year_int > 0 && month_int > 0)
                {
                    using (CarteluxDB context = new CarteluxDB())
                    {
                        DateTime date1 = new DateTime(year_int, month_int, 1);
                        int last_day = DateTime.DaysInMonth(year_int, month_int);
                        DateTime date2 = new DateTime(year_int, month_int, last_day);

                        int number = 1;
                        List<proyectos> proyectos_elements = context.proyectos.Where(v => v.Fecha_creado >= date1 && v.Fecha_creado <= date2).OrderBy(e => e.Fecha_creado).ToList();
                        foreach (proyectos _proyecto in proyectos_elements)
                        {
                            if (_proyecto != null)
                            {
                                _GridProyectos _GridProyecto1 = new Cartelux1.Dashboard._GridProyectos();
                                _GridProyecto1.Proyecto_ID = _proyecto.Proyecto_ID.ToString();

                                _GridProyecto1.lblNumber_proy = number.ToString();
                                _GridProyecto1.lblNombre_proy = _proyecto.Nombre.ToString();
                                _GridProyecto1.lblDescripcion_proy = _proyecto.Descripcion.ToString();
                                _GridProyecto1.lblFechaEstimada = _proyecto.Fecha_estimada.ToString();
                                _GridProyecto1.lblContacto1 = _proyecto.Contacto_1_nombre.ToString();
                                _GridProyecto1.lblTelefono1 = _proyecto.Contacto_1_telefono.ToString();
                                _GridProyecto1.lblEmail1 = _proyecto.Contacto_1_email.ToString();
                                _GridProyecto1.lblContacto2 = _proyecto.Contacto_2_nombre.ToString();
                                _GridProyecto1.lblTelefono2 = _proyecto.Contacto_2_telefono.ToString();
                                _GridProyecto1.lblEmail2 = _proyecto.Contacto_2_email.ToString();
                                _GridProyecto1.lblComentarios_proy = _proyecto.Proyecto_ID.ToString();

                                string estado = string.Empty;
                                lista_proyecto_estados _proyecto_estado = (lista_proyecto_estados)context.lista_proyecto_estados.FirstOrDefault(c => c.Proyecto_Estado_ID == _proyecto.Proyecto_estado_ID);
                                if (_proyecto_estado != null)
                                {
                                    estado = _proyecto_estado.Nombre;
                                }
                                _GridProyecto1.lblEstado_proy = estado.ToString();

                                _GridProyectos_list.Add(_GridProyecto1);
                            }
                            number++;
                        } // foreach
                    }
                }
            }
            return _GridProyectos_list.ToArray();
        }

        [WebMethod]
        public static bool PedidosUpdateState(int actionID, string formID, string userID)
        {
            bool ret = false;
            if (!string.IsNullOrWhiteSpace(formID))
            {
                // Logger variables
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                using (CarteluxDB context = new CarteluxDB())
                {
                    int formID_int = 0;
                    if (!int.TryParse(formID, out formID_int))
                    {
                        formID_int = 0;
                        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, formID);
                    }
                    int userID_int = 0;
                    if (!int.TryParse(userID, out userID_int))
                    {
                        userID_int = 0;
                        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, userID);
                    }
                    if (formID_int > 0 && userID_int > 0)
                    {
                        pedidos _pedido = (pedidos)context.pedidos.FirstOrDefault(v => v.Formulario_ID.Equals(formID_int));
                        if (_pedido != null)
                        {
                            lista_pedido_estados _pedidoEstado = (lista_pedido_estados)context.lista_pedido_estados.FirstOrDefault(v => v.Codigo.Equals(actionID));
                            if (_pedidoEstado != null)
                            {
                                //Logs.AddUserLog("(%s) (%s) -- Info WebMethod. Parametros recibidos: " + tapeID.ToString() + ", " + isExtra.ToString(), className, methodName);
                                //Logs.AddUserLog("Acceso al sistema", obj.GetType().Name + ": " + obj.GetType().Name + ": " + obj.Viaje_ID, userID1, username);

                                int estadoID = _pedidoEstado.Pedido_Estado_ID;
                                _pedido.Pedido_Estado_ID = estadoID;
                                _pedido.Pedido_Estado_UltimaModificacion_Usuario_ID = userID_int;
                                ret = true;

                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
            return ret;
        }
        #endregion
    }
}

