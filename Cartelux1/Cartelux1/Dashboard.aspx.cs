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
            }

            gridFormularios.UseAccessibleHeader = true;
            gridFormularios.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridFormularios.FooterRow.TableSection = TableRowSection.TableFooter;
            //ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnExport);
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
                }
            }
        }

        protected void gridFormularios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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

                            lbl1 = e.Row.FindControl("lblTelefono") as Label;
                            if (lbl1 != null)
                            {
                                string nombre = string.Empty;
                                if (!string.IsNullOrWhiteSpace(_cliente.Telefono))
                                {
                                    nombre = _cliente.Telefono;
                                }
                                lbl1.Text = nombre;
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
                                    lista_entregas_tipos _lista_entregas_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(c => c.Codigo == _pedido_entrega.Entrega_Tipo_ID);
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
                                lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(c => c.Codigo == _pedido.Pedido_Tamano_ID);
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

                            lbl1 = e.Row.FindControl("lblTipo") as Label;
                            if (lbl1 != null)
                            {
                                lista_pedido_tipos _lista_pedido_tipo = (lista_pedido_tipos)context.lista_pedido_tipos.FirstOrDefault(c => c.Pedido_Tipo_ID == _pedido.Pedido_Tipo_ID);
                                if (_lista_pedido_tipo != null)
                                {
                                    string nombre = string.Empty;
                                    if (!string.IsNullOrWhiteSpace(_lista_pedido_tipo.Nombre))
                                    {
                                        nombre = _lista_pedido_tipo.Nombre;
                                    }
                                    lbl1.Text = nombre;
                                }
                            }

                            lbl1 = e.Row.FindControl("lblMaterial") as Label;
                            if (lbl1 != null)
                            {
                                lista_pedido_materiales _lista_pedido_material = (lista_pedido_materiales)context.lista_pedido_materiales.FirstOrDefault(c => c.Codigo == _pedido.Pedido_Material_ID);
                                if (_lista_pedido_material != null)
                                {
                                    lbl1.Text = _lista_pedido_material.Nombre;
                                }
                            }

                            lbl1 = e.Row.FindControl("lblZona") as Label;
                            if (lbl1 != null)
                            {
                                pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
                                if (_pedido_entrega != null)
                                {
                                    string nombre = string.Empty;
                                    if (!string.IsNullOrWhiteSpace(_pedido_entrega.Barrio))
                                    {
                                        nombre = _pedido_entrega.Barrio;
                                    }
                                    lbl1.Text = nombre;
                                }
                            }

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
                dt1 = Extras.ToDataTable(context.confi_formularios_anos.OrderBy(e => e.Nombre).ToList());
                ddl_year.DataSource = dt1;
                ddl_year.DataTextField = "Nombre";
                ddl_year.DataValueField = "Confi_formularios_ano_ID";
                ddl_year.DataBind();
                ddl_year.Items.Insert(0, new ListItem("Elegir", "0"));
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

            using (CarteluxDB context = new CarteluxDB())
            {
                // If is future month
                if (current_year >= DateTime.Now.Year && current_month > DateTime.Now.Month)
                {
                    solo_vigentes = false;
                }

                int day_value = 1;
                if (solo_vigentes)
                {
                    day_value = DateTime.Now.Day;
                }

                DateTime date1 = new DateTime(current_year, current_month, 1);
                int last_day = DateTime.DaysInMonth(current_year, current_month);
                DateTime date2 = new DateTime(current_year, current_month, last_day);

                List<formularios> formularios_elements = GetFormularios_ByMonth(context, date1, date2);

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
                    obj.Add(new formularios());

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
        }

        public static List<formularios> GetFormularios_ByMonth(CarteluxDB context, DateTime date1, DateTime date2, bool incluye_cancelados = false)
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
                        // Filtro No incluye cancelados
                        if (!incluye_cancelados)
                        {
                            lista_pedido_estados _pedidoEstado = (lista_pedido_estados)context.lista_pedido_estados.FirstOrDefault(c => c.Pedido_Estado_ID == _pedido.Pedido_Estado_ID);
                            if (_pedidoEstado != null)
                            {
                                if ((_pedido.Pedido_Estado_ID == _pedidoEstado.Pedido_Estado_ID && (_pedidoEstado.Codigo == 2 || _pedidoEstado.Codigo == 3)))
                                {
                                    continue;
                                }
                            }
                        }

                        int pedido_entrega_ID = _pedido.Pedido_Entrega_ID;
                        formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Formulario_ID == _pedido.Formulario_ID);
                        if (_formulario != null)
                        {
                            // Add Formulario
                            formularios_elements.Add(_formulario);
                        }
                    } // foreach pedidos
                } // foreach pedido_entregas

            }
            return formularios_elements;
        }


        #endregion

        #region Static Methods

        [WebMethod]
        public static _GridFormularios[] GetData_BindGridFormularios(string year_value, string month_value, bool soloVigentes_value, bool soloJuanchy_value, bool incluirCancelados_value)
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
                            soloVigentes_value = true;
                        }

                        int day_value = 1;
                        if (soloVigentes_value)
                        {
                            day_value = DateTime.Now.Day;
                        }

                        DateTime date1 = new DateTime(year_int, month_int, day_value);
                        int last_day = DateTime.DaysInMonth(year_int, month_int);
                        DateTime date2 = new DateTime(year_int, month_int, last_day);

                        int number = 1;
                        List<formularios> formularios_elements = GetFormularios_ByMonth(context, date1, date2, incluirCancelados_value);
                        foreach (formularios _formulario in formularios_elements)
                        {
                            if (_formulario != null)
                            {
                                _GridFormularios _GridFormulario1 = new Cartelux1.Dashboard._GridFormularios();
                                _GridFormulario1.Formulario_ID = _formulario.Formulario_ID.ToString();
                                _GridFormulario1.URL_short = _formulario.URL_short;
                                _GridFormulario1.EstadoNro = 0;

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
                                        if(_pedidoEstado.Codigo == 2 || _pedidoEstado.Codigo == 3) // Si Cancelado o Eliminado, no concluye después
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
                                        // Filtro Juanchy
                                        lista_entregas_tipos _lista_entregas_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(c => c.Codigo == _pedido_entrega.Entrega_Tipo_ID);
                                        if (_lista_entregas_tipo != null)
                                        {
                                            _GridFormulario1.lblTipoEntrega = _lista_entregas_tipo.Nombre;
                                            _GridFormulario1.lblTipoCodigo = _lista_entregas_tipo.Codigo.ToString();

                                            if (soloJuanchy_value && (_lista_entregas_tipo.Codigo != 1 && _lista_entregas_tipo.Codigo != 2))
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
                                    lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(c => c.Codigo == _pedido.Pedido_Tamano_ID);
                                    if (_lista_pedido_tamano != null)
                                    {
                                        _GridFormulario1.lblTamano = _lista_pedido_tamano.Nombre;
                                    }
                                    #endregion END Pedido Tamaño

                                    #region Pedido Tipo ---------------------------------------------------------------------------------------------------------
                                    lista_pedido_tipos _lista_pedido_tipo = (lista_pedido_tipos)context.lista_pedido_tipos.FirstOrDefault(c => c.Pedido_Tipo_ID == _pedido.Pedido_Tipo_ID);
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

                                    #region Pedido Material ---------------------------------------------------------------------------------------------------------
                                    _GridFormulario1.lblMaterial = "Impreso";
                                    lista_pedido_materiales _lista_pedido_material = (lista_pedido_materiales)context.lista_pedido_materiales.FirstOrDefault(c => c.Pedido_Material_ID == _pedido.Pedido_Material_ID);
                                    if (_lista_pedido_material != null && !string.IsNullOrWhiteSpace(_lista_pedido_material.Nombre))
                                    {
                                        _GridFormulario1.lblMaterial = _lista_pedido_material.Nombre;
                                    }
                                    #endregion END Pedido Material

                                }
                                _GridFormularios_list.Add(_GridFormulario1);
                            }
                            number++;
                        } // foreach
                    }
                }
            }
            return _GridFormularios_list.ToArray();
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
            public string lblTipo { get; set; }
            public string lblMaterial { get; set; }
            public int lblCantidad { get; set; }
            public string lblZona { get; set; }
            public string Formulario_ID { get; set; }
            public string URL_short { get; set; }
            public string URL_gmaps { get; set; }
            public bool chbTieneBosquejo { get; set; }
            public string lblDisenoReferido { get; set; }
            public int EstadoNro { get; set; }
        }


        [WebMethod]
        public static bool PedidosUpdateState(int actionID, string formID)
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
                    if (formID_int > 0)
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

