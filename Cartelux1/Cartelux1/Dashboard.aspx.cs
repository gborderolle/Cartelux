using Cartelux1.Global_Objects;
using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
using System.Text;

namespace Cartelux1
{
    public partial class Dashboard : System.Web.UI.Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_GridFormularios();
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
            #region Buttons

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }

            #endregion Buttons

            #region Labels

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

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

        private void Bind_GridFormularios(string year = "", string month = "")
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
                current_month= month_int;
            }

            using (CarteluxDB context = new CarteluxDB())
            {
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


                //var elements = context.formularios.Where(v => v. == formulario_ID).OrderBy(e => e.Fecha_creado).ToList();

                /*
                // Filtro por fechas
                if (!string.IsNullOrWhiteSpace(month) && !string.IsNullOrWhiteSpace(year))
                {
                    int month_int = 0;
                    if (!int.TryParse(month, out month_int))
                    {
                        month_int = 0;
                        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, month);
                    }
                    int year_int = 0;
                    if (!int.TryParse(year, out year_int))
                    {
                        year_int = 0;
                        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, year);
                    }
                    if (month_int > 0 && year_int > 0)
                    {
                        // Obtiene pagos del mes corriente
                        elements = GetPagosByMonth(context, cliente_ID, month_int, year_int);

                        // Obtiene pagos del mes anterior - Saldo Inicial
                        decimal saldo_anterior = 0;
                        var elements_anterior = GetPagosByMonth(context, cliente_ID, --month_int, year_int);
                        if (elements_anterior.Count() > 0)
                        {
                            decimal total_importe = elements_anterior.Sum(x => x.Importe_viaje);
                            decimal total_pagos = elements_anterior.Sum(x => x.Monto);
                            saldo_anterior = total_importe - total_pagos;
                        }

                        hdn_SaldoAnterior.Value = saldo_anterior.ToString();
                        lblSaldo_inicial.Text = String.Format("{0:n}", saldo_anterior);
                    }

                    if (elements.Count() > 0)
                    {
                        gridPagos.DataSource = elements;
                        gridPagos.DataBind();

                        gridPagos.UseAccessibleHeader = true;
                        gridPagos.HeaderRow.TableSection = TableRowSection.TableHeader;

                        lblGridPagosCount.Text = "# " + elements.Count();
                    }
                    else
                    {
                        var obj = new List<fletero_pagos>();
                        obj.Add(new fletero_pagos());

                        // Bind the DataTable which contain a blank row to the GridView
                        gridPagos.DataSource = obj;
                        gridPagos.DataBind();
                        int columnsCount = gridPagos.Columns.Count;
                        gridPagos.Rows[0].Cells.Clear();// clear all the cells in the row
                        gridPagos.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                        gridPagos.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

                        //You can set the styles here
                        gridPagos.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        gridPagos.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                        gridPagos.Rows[0].Cells[0].Font.Bold = true;

                        //set No Results found to the new added cell
                        gridPagos.Rows[0].Cells[0].Text = "No hay registros";
                    }
                }
        */

            }
        }

        private List<formularios> GetFormularios_ByMonth(CarteluxDB context, DateTime date1, DateTime date2)
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
        public static string SelectMonth(string month_value)
        {
            string ID_result = "0";
            if (!string.IsNullOrWhiteSpace(month_value))
            {
                // Logger variables
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                int month_value_int = 0;
                if (!int.TryParse(month_value, out month_value_int))
                {
                    month_value_int = 0;
                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, month_value);
                }

                if (month_value_int > 0)
                {
                }
            }
            return ID_result;
        }

        #endregion
    }
}

