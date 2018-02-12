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
                //Bind_GridMonths();
                Bind_DataConfig();
                Bind_GridFormularios(); // Pasar mes actual
            }

            gridFormularios.UseAccessibleHeader = true;
            gridFormularios.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        //protected void gridMonths_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandArgument != null)
        //    {
        //        if (!string.IsNullOrWhiteSpace(e.CommandArgument.ToString()) && !string.IsNullOrWhiteSpace(e.CommandName))
        //        {
        //        }
        //    }
        //}

        //protected void gridMonths_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // Source: http://www.codeproject.com/Tips/622720/Fire-GridView-SelectedIndexChanged-Event-without-S

        //    // Logger variables
        //    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    string methodName = stackFrame.GetMethod().Name;

        //    foreach (GridViewRow row in gridFormularios.Rows)
        //    {
        //        if (row.RowIndex == gridFormularios.SelectedIndex)
        //        {
        //            string Formulario_ID_str = gridFormularios.SelectedRow.Cells[0].Text;
        //            if (!string.IsNullOrWhiteSpace(Formulario_ID_str))
        //            {
        //                int Formulario_ID = 0;
        //                if (!int.TryParse(Formulario_ID_str, out Formulario_ID))
        //                {
        //                    Formulario_ID = 0;
        //                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, Formulario_ID_str);
        //                }

        //                if (Formulario_ID > 0)
        //                {
        //                    using (CarteluxDB context = new CarteluxDB())
        //                    {
        //                        formularios _formulario = (formularios)context.formularios.FirstOrDefault(c => c.Formulario_ID == Formulario_ID);
        //                        if (_formulario != null)
        //                        {
        //                            //lblClientName_1.Text = lblClientName_2.Text = _formulario.Nombre;

        //                            //BindGridViajes(cliente_ID);

        //                            // Filtrar por fechas del mes corriente por defecto
        //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "gridFormularios_OnSelectedIndexChanged", "<script type='text/javascript'></script>", false);

        //                            hdn_FormularioID.Value = Formulario_ID_str;
        //                        }
        //                    }
        //                }
        //            }
        //            row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
        //            foreach (TableCell cell in row.Cells)
        //            {
        //                cell.BackColor = ColorTranslator.FromHtml("#A1DCF2");
        //            }
        //        }
        //        else
        //        {
        //            row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        //            foreach (TableCell cell in row.Cells)
        //            {
        //                cell.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        //            }
        //        }
        //    }
        //}

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

                            Label lbl1 = e.Row.FindControl("lblTelefono") as Label;
                            if (lbl1 != null)
                            {
                                lbl1.Text = _cliente.Telefono;
                            }

                            lbl1 = e.Row.FindControl("lblNombre") as Label;
                            if (lbl1 != null)
                            {
                                lbl1.Text = _cliente.Nombre;
                            }

                            lbl1 = e.Row.FindControl("lblFechaEntrega") as Label;
                            if (lbl1 != null)
                            {
                                pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
                                if (_pedido_entrega != null)
                                {
                                    lbl1.Text = _pedido_entrega.Fecha_entrega.ToString();
                                }
                            }

                            // ???
                            lbl1 = e.Row.FindControl("lblTipoEntrega") as Label;
                            if (lbl1 != null)
                            {
                                lista_entregas_tipos _lista_entregas_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(c => c.Entrega_Tipo_ID == _pedido.Pedido_Tipo_ID);
                                if (_lista_entregas_tipo != null)
                                {
                                    lbl1.Text = _lista_entregas_tipo.Nombre;
                                }
                            }

                            lbl1 = e.Row.FindControl("lblTamano") as Label;
                            if (lbl1 != null)
                            {
                                lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(c => c.Pedido_Tamano_ID == _pedido.Pedido_Tamano_ID);
                                if (_lista_pedido_tamano != null)
                                {
                                    lbl1.Text = _lista_pedido_tamano.Nombre;
                                }
                            }

                            lbl1 = e.Row.FindControl("lblTipo") as Label;
                            if (lbl1 != null)
                            {
                                lista_pedido_tipos _lista_pedido_tipo = (lista_pedido_tipos)context.lista_pedido_tipos.FirstOrDefault(c => c.Pedido_Tipo_ID == _pedido.Pedido_Tipo_ID);
                                if (_lista_pedido_tipo != null)
                                {
                                    lbl1.Text = _lista_pedido_tipo.Nombre;
                                }
                            }

                            lbl1 = e.Row.FindControl("lblMaterial") as Label;
                            if (lbl1 != null)
                            {
                                lista_pedido_materiales _lista_pedido_material = (lista_pedido_materiales)context.lista_pedido_materiales.FirstOrDefault(c => c.Pedido_Material_ID == _pedido.Pedido_Material_ID);
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
                                    lbl1.Text = _pedido_entrega.Barrio;
                                }
                            }
                            
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

        private void Bind_GridMonths()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("#", typeof(int)), new DataColumn("Month", typeof(string)) });
            dt.Rows.Add(1, "Enero");
            dt.Rows.Add(2, "Febrero");
            dt.Rows.Add(3, "Marzo");
            dt.Rows.Add(4, "Abril");
            dt.Rows.Add(5, "Mayo");
            dt.Rows.Add(6, "Junio");
            dt.Rows.Add(7, "Julio");
            dt.Rows.Add(8, "Agosto");
            dt.Rows.Add(9, "Septiembre");
            dt.Rows.Add(10, "Octubre");
            dt.Rows.Add(11, "Noviembre");
            dt.Rows.Add(12, "Diciembre");
            //gridMonths.DataSource = dt;
            //gridMonths.DataBind();
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

        public static List<formularios> GetFormularios_ByMonth(CarteluxDB context, DateTime date1, DateTime date2)
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
                    foreach (pedidos _pedidos in pedidos_elements)
                    {
                        int pedido_entrega_ID = _pedidos.Pedido_Entrega_ID;
                        formularios _formulario = (formularios)context.formularios.FirstOrDefault(v => v.Formulario_ID == _pedidos.Formulario_ID);
                        if (_formulario != null)
                        {
                            // Get Formulario
                            formularios_elements.Add(_formulario);
                        }
                    }
                }
            }
            return formularios_elements;
        }


        #endregion

        #region WebMethods

        [WebMethod]
        public static _GridFormularios[] GetData_BindGridFormularios(string year_value, string month_value, bool soloVigentes_value)
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
                        int day_value = 1;
                        if (soloVigentes_value)
                        {
                            day_value = DateTime.Now.Day;
                        }

                        DateTime date1 = new DateTime(year_int, month_int, day_value);
                        int last_day = DateTime.DaysInMonth(year_int, month_int);
                        DateTime date2 = new DateTime(year_int, month_int, last_day);

                        _GridFormularios _GridFormulario1 = new Cartelux1.Dashboard._GridFormularios();

                        List<formularios> formularios_elements = GetFormularios_ByMonth(context, date1, date2);
                        foreach (formularios _formulario in formularios_elements)
                        {
                            if (_formulario != null)
                            {
                                _GridFormulario1.Formulario_ID = _formulario.Formulario_ID.ToString();
                                _GridFormulario1.URL_short = _formulario.URL_short;

                                clientes _cliente = (clientes)context.clientes.FirstOrDefault(c => c.Cliente_ID == _formulario.Cliente_ID);
                                pedidos _pedido = (pedidos)context.pedidos.FirstOrDefault(c => c.Formulario_ID == _formulario.Formulario_ID);
                                if (_cliente != null && _pedido != null)
                                {
                                    _GridFormulario1.lblTelefono = _cliente.Telefono;
                                    _GridFormulario1.lblNombre = _cliente.Nombre;

                                    pedido_entregas _pedido_entrega = (pedido_entregas)context.pedido_entregas.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
                                    if (_pedido_entrega != null)
                                    {
                                        _GridFormulario1.lblFechaEntrega = _pedido_entrega.Fecha_entrega.ToString();
                                    }

                                    lista_entregas_tipos _lista_entregas_tipo = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(c => c.Entrega_Tipo_ID == _pedido.Pedido_Tipo_ID);
                                    if (_lista_entregas_tipo != null)
                                    {
                                        _GridFormulario1.lblTipoEntrega = _lista_entregas_tipo.Nombre;
                                    }

                                    lista_pedido_tamanos _lista_pedido_tamano = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(c => c.Pedido_Tamano_ID == _pedido.Pedido_Tamano_ID);
                                    if (_lista_pedido_tamano != null)
                                    {
                                        _GridFormulario1.lblTamano = _lista_pedido_tamano.Nombre;
                                    }

                                    lista_pedido_tipos _lista_pedido_tipo = (lista_pedido_tipos)context.lista_pedido_tipos.FirstOrDefault(c => c.Pedido_Tipo_ID == _pedido.Pedido_Tipo_ID);
                                    if (_lista_pedido_tipo != null)
                                    {
                                        _GridFormulario1.lblTipo = _lista_pedido_tipo.Nombre;
                                    }

                                    lista_pedido_materiales _lista_pedido_material = (lista_pedido_materiales)context.lista_pedido_materiales.FirstOrDefault(c => c.Pedido_Material_ID == _pedido.Pedido_Material_ID);
                                    if (_lista_pedido_material != null)
                                    {
                                        _GridFormulario1.lblMaterial = _lista_pedido_material.Nombre;
                                    }

                                    if (_pedido_entrega != null)
                                    {
                                        _GridFormulario1.lblZona = _pedido_entrega.Barrio;
                                    }
                                }
                                _GridFormularios_list.Add(_GridFormulario1);
                            }
                        } // foreach
                    }
                }
            }
            return _GridFormularios_list.ToArray();
        }

        public class _GridFormularios
        {
            public string lblTelefono { get; set; }
            public string lblNombre { get; set; }
            public string lblFechaEntrega { get; set; }
            public string lblTipoEntrega { get; set; }
            public string lblTamano { get; set; }
            public string lblTipo { get; set; }
            public string lblMaterial { get; set; }
            public string lblZona { get; set; }
            public string Formulario_ID { get; set; }
            public string URL_short { get; set; }
        }

        #endregion
    }
}

