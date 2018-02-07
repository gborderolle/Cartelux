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
                //BindGridClientes();
                //BindModalAgregarPago();
                //BindModalModificarPago();
            }

            //gridClientes.UseAccessibleHeader = true;
            //gridClientes.HeaderRow.TableSection = TableRowSection.TableHeader;

            gridClientes_lblMessage.Text = string.Empty;
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
                    string value = hdn_txbMonthpicker.Value;
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        string[] words = value.Split('|');
                        if (words != null && words.Length > 1)
                        {
                            string month = words[0];
                            string year = words[1];
                            //BindGridPagos(cliente_ID, month, year);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSearch_Click_saldos", "<script type='text/javascript'>$('#txbMonthpicker').val('0" + month + "/" + year + "'); </script>", false);
                        }
                    }
                }
            }
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            gridPagos.PageIndex = e.NewPageIndex;

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
                    string value = hdn_txbMonthpicker.Value;
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        string[] words = value.Split('|');
                        if (words != null && words.Length > 1)
                        {
                            string month = words[0];
                            string year = words[1];
                            //BindGridPagos(cliente_ID, month, year);
                        }
                    }
                }
            }
        }

        protected void grid2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gridClientes.PageIndex = e.NewPageIndex;
            //BindGridClientes();
        }

        protected void grid3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            gridViajes.PageIndex = e.NewPageIndex;

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
                    //BindGridViajes(cliente_ID);
                }
            }
        }

        protected void gridClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {
                if (!string.IsNullOrWhiteSpace(e.CommandArgument.ToString()) && !string.IsNullOrWhiteSpace(e.CommandName))
                {
                }
            }
        }

        protected void gridClientes_RowDataBound(object sender, GridViewRowEventArgs e)
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
                //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridClientes, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gridViajes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //#region DDL Default values

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    // Fleteros ----------------------------------------------------
            //    Label lbl = e.Row.FindControl("lblFletero") as Label;
            //    if (lbl != null)
            //    {
            //        lbl.Text = string.Empty;
            //        using (bonisoftEntities context = new bonisoftEntities())
            //        {
            //            viaje viaje = (viaje)(e.Row.DataItem);
            //            if (viaje != null)
            //            {
            //                int id = viaje.Fletero_ID;
            //                fletero fletero = (fletero)context.fleteros.FirstOrDefault(c => c.Fletero_ID == id);
            //                if (fletero != null)
            //                {
            //                    string nombre = fletero.Nombre;
            //                    lbl.Text = nombre;
            //                    //lbl.CommandArgument = "fleteros," + fletero.Nombre;
            //                }
            //            }
            //        }
            //    }

            //    // Camion ----------------------------------------------------
            //    lbl = e.Row.FindControl("lblCamion") as Label;
            //    if (lbl != null)
            //    {
            //        lbl.Text = string.Empty; using (bonisoftEntities context = new bonisoftEntities())
            //        {
            //            viaje viaje = (viaje)(e.Row.DataItem);
            //            if (viaje != null)
            //            {
            //                int id = viaje.Camion_ID;
            //                camion camion = (camion)context.camiones.FirstOrDefault(c => c.Camion_ID == id);
            //                if (camion != null)
            //                {
            //                    string nombre = camion.Matricula_zorra;
            //                    lbl.Text = nombre;
            //                    //lbl.CommandArgument = "camiones," + camion.Marca;
            //                }
            //            }
            //        }
            //    }

            //    // Chofer ----------------------------------------------------
            //    lbl = e.Row.FindControl("lblChofer") as Label;
            //    if (lbl != null)
            //    {
            //        lbl.Text = string.Empty; using (bonisoftEntities context = new bonisoftEntities())
            //        {
            //            viaje viaje = (viaje)(e.Row.DataItem);
            //            if (viaje != null)
            //            {
            //                int id = viaje.Chofer_ID;
            //                chofer chofer = (chofer)context.choferes.FirstOrDefault(c => c.Chofer_ID == id);
            //                if (chofer != null)
            //                {
            //                    string nombre = chofer.Nombre_completo;
            //                    lbl.Text = nombre;
            //                    //lbl.CommandArgument = "choferes," + chofer.Nombre_completo;
            //                }
            //            }
            //        }
            //    }

            //    // Proveedor ----------------------------------------------------
            //    lbl = e.Row.FindControl("lblProveedor") as Label;
            //    if (lbl != null)
            //    {
            //        lbl.Text = string.Empty; using (bonisoftEntities context = new bonisoftEntities())
            //        {
            //            viaje viaje = (viaje)(e.Row.DataItem);
            //            if (viaje != null)
            //            {
            //                int id = viaje.Proveedor_ID;
            //                proveedor proveedor = (proveedor)context.proveedores.FirstOrDefault(c => c.Proveedor_ID == id);
            //                if (proveedor != null)
            //                {
            //                    string nombre = proveedor.Nombre;
            //                    lbl.Text = nombre;
            //                    //lbl.CommandArgument = "proveedores," + proveedor.Nombre;
            //                }
            //            }
            //        }
            //    }

            //    lbl = e.Row.FindControl("lblFechaPartida") as Label;
            //    if (lbl != null)
            //    {
            //        using (bonisoftEntities context = new bonisoftEntities())
            //        {
            //            viaje viaje = (viaje)(e.Row.DataItem);
            //            if (viaje != null)
            //            {
            //                if (viaje.Fecha_partida == DateTime.MinValue)
            //                {
            //                    lbl.Text = string.Empty;
            //                }
            //            }
            //        }

            //    }

            //}
            //#endregion
        }

        protected void gridViajes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

            }
            else if (e.CommandName.Equals("View"))
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

        protected void gridPagos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //#region DDL Default values

            //// Formas de pago ----------------------------------------------------
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    LinkButton lnk = e.Row.FindControl("lblForma") as LinkButton;
            //    if (lnk != null)
            //    {
            //        lnk.Text = string.Empty; using (bonisoftEntities context = new bonisoftEntities())
            //        {
            //            fletero_pagos pago = (fletero_pagos)(e.Row.DataItem);
            //            if (pago != null)
            //            {
            //                int id = pago.Forma_de_pago_ID;
            //                forma_de_pago forma = (forma_de_pago)context.forma_de_pago.FirstOrDefault(c => c.Forma_de_pago_ID == id);
            //                if (forma != null)
            //                {
            //                    string nombre = forma.Forma;
            //                    lnk.Text = nombre;
            //                    lnk.CommandArgument = "formas," + nombre;
            //                }
            //            }
            //        }
            //    }

            //    Label lbl = e.Row.FindControl("lblFechaPago") as Label;
            //    if (lbl != null)
            //    {
            //        using (bonisoftEntities context = new bonisoftEntities())
            //        {
            //            fletero_pagos pago = (fletero_pagos)(e.Row.DataItem);
            //            if (pago != null)
            //            {
            //                if (pago.Fecha_pago == DateTime.MinValue)
            //                {
            //                    lbl.Text = string.Empty;
            //                }
            //            }
            //        }
            //    }

            //}

            //#endregion
        }

        protected void gridPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument != null)
            {

            }
            else if (e.CommandName.Equals("View"))
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

        protected void gridClientes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Source: http://www.codeproject.com/Tips/622720/Fire-GridView-SelectedIndexChanged-Event-without-S

            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            //foreach (GridViewRow row in gridClientes.Rows)
            //{
            //    if (row.RowIndex == gridClientes.SelectedIndex)
            //    {
            //        string cliente_ID_str = gridClientes.SelectedRow.Cells[0].Text;
            //        if (!string.IsNullOrWhiteSpace(cliente_ID_str))
            //        {
            //            int cliente_ID = 0;
            //            if (!int.TryParse(cliente_ID_str, out cliente_ID))
            //            {
            //                cliente_ID = 0;
            //                Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, cliente_ID_str);
            //            }

            //            if (cliente_ID > 0)
            //            {
            //                using (bonisoftEntities context = new bonisoftEntities())
            //                {
            //                    fletero cliente = (fletero)context.fleteros.FirstOrDefault(c => c.Fletero_ID == cliente_ID);
            //                    if (cliente != null)
            //                    {
            //                        lblClientName_1.Text = lblClientName_2.Text = cliente.Nombre;

            //                        BindGridViajes(cliente_ID);
            //                        BindGridViajesImprimir(cliente_ID);

            //                        // Filtrar por fechas del mes corriente por defecto
            //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "gridClientes_OnSelectedIndexChanged", "<script type='text/javascript'>loadFilter_CurrentMonth(); loadInputDDL(); setupMonthPicker();</script>", false);

            //                        BindGridPagos(cliente_ID, DateTime.Now.Month.ToString());

            //                        hdn_clientID.Value = cliente_ID_str;

            //                        gridViajes.UseAccessibleHeader = true;
            //                        gridViajes.HeaderRow.TableSection = TableRowSection.TableHeader;
            //                    }
            //                }
            //            }
            //        }
            //        row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            cell.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            //        }
            //    }
            //    else
            //    {
            //        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            cell.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            //        }
            //    }
            //}
        }

        #endregion Events

        //#region General methods

        //private void BindGridClientes()
        //{
        //    //using (bonisoftEntities context = new bonisoftEntities())
        //    //{
        //    //    var elements = context.fleteros.OrderBy(e => e.Nombre).ToList();
        //    //    if (elements.Count() > 0)
        //    //    {
        //    //        gridClientes.DataSource = elements;
        //    //        gridClientes.DataBind();

        //    //        gridClientes.UseAccessibleHeader = true;
        //    //        gridClientes.HeaderRow.TableSection = TableRowSection.TableHeader;

        //    //        lblGridClientesCount.Text = "# " + elements.Count();
        //    //    }
        //    //    else
        //    //    {
        //    //        var obj = new List<fletero>();
        //    //        obj.Add(new fletero());

        //    //        // Bind the DataTable which contain a blank row to the GridView
        //    //        gridClientes.DataSource = obj;
        //    //        gridClientes.DataBind();
        //    //        int columnsCount = gridClientes.Columns.Count;
        //    //        gridClientes.Rows[0].Cells.Clear();// clear all the cells in the row
        //    //        gridClientes.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
        //    //        gridClientes.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

        //    //        //You can set the styles here
        //    //        gridClientes.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //    //        gridClientes.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
        //    //        gridClientes.Rows[0].Cells[0].Font.Bold = true;

        //    //        //set No Results found to the new added cell
        //    //        gridClientes.Rows[0].Cells[0].Text = "No hay registros";
        //    //    }
        //    //}
        //}

        //private void BindGridViajes(int cliente_ID)
        //{
        //    if (cliente_ID > 0)
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            var elements = context.viajes.Where(v => v.Fletero_ID == cliente_ID && !v.isFicticio).ToList();
        //            if (elements.Count() > 0)
        //            {
        //                gridViajes.DataSource = elements;
        //                gridViajes.DataBind();

        //                gridViajes.UseAccessibleHeader = true;
        //                gridViajes.HeaderRow.TableSection = TableRowSection.TableHeader;

        //                lblGridViajesCount.Text = "# " + elements.Count();
        //            }
        //            else
        //            {
        //                var obj = new List<viaje>();
        //                obj.Add(new viaje());

        //                /* Grid Viajes */

        //                // Bind the DataTable which contain a blank row to the GridView
        //                gridViajes.DataSource = obj;
        //                gridViajes.DataBind();
        //                int columnsCount = gridViajes.Columns.Count;
        //                gridViajes.Rows[0].Cells.Clear();// clear all the cells in the row
        //                gridViajes.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
        //                gridViajes.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

        //                //You can set the styles here
        //                gridViajes.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //                gridViajes.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
        //                gridViajes.Rows[0].Cells[0].Font.Bold = true;

        //                //set No Results found to the new added cell
        //                gridViajes.Rows[0].Cells[0].Text = "No hay registros";
        //            }
        //        }
        //    }
        //}

        //private void BindGridViajesImprimir(int cliente_ID, string date_start = "", string date_end = "")
        //{
        //    if (cliente_ID > 0)
        //    {
        //        // Logger variables
        //        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //        System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //        string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //        string methodName = stackFrame.GetMethod().Name;

        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            var elements = context.fletero_pagos.Where(v => v.Fletero_ID == cliente_ID).ToList();

        //            // Filtro por fechas
        //            if (!string.IsNullOrWhiteSpace(date_start) && !string.IsNullOrWhiteSpace(date_end))
        //            {
        //                DateTime date1 = DateTime.MinValue;
        //                if (!DateTime.TryParseExact(date_start, GlobalVariables.ShortDateTime_format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
        //                {
        //                    date1 = DateTime.MinValue;
        //                    Logs.AddErrorLog("Excepcion. Convirtiendo datetime. ERROR:", className, methodName, date_start);
        //                }

        //                DateTime date2 = DateTime.MinValue;
        //                if (!DateTime.TryParseExact(date_end, GlobalVariables.ShortDateTime_format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date2))
        //                {
        //                    date2 = DateTime.MinValue;
        //                    Logs.AddErrorLog("Excepcion. Convirtiendo datetime. ERROR:", className, methodName, date_end);
        //                }

        //                elements = context.fletero_pagos.Where(v => v.Fletero_ID == cliente_ID && (v.Fecha_pago >= date1 && v.Fecha_pago <= date2)).OrderBy(e => e.Fecha_pago).ToList();
        //            }

        //            if (elements.Count() > 0)
        //            {
        //                gridViajesImprimir.DataSource = elements;
        //                gridViajesImprimir.DataBind();

        //                gridViajesImprimir.UseAccessibleHeader = true;
        //                gridViajesImprimir.HeaderRow.TableSection = TableRowSection.TableHeader;

        //                lblGridViajesImprimirCount.Text = "# " + elements.Count();
        //            }
        //            else
        //            {
        //                var obj = new List<fletero_pagos>();
        //                obj.Add(new fletero_pagos());

        //                /* Grid Viajes */

        //                // Bind the DataTable which contain a blank row to the GridView
        //                gridViajesImprimir.DataSource = obj;
        //                gridViajesImprimir.DataBind();
        //                int columnsCount = gridViajesImprimir.Columns.Count;
        //                gridViajesImprimir.Rows[0].Cells.Clear();// clear all the cells in the row
        //                gridViajesImprimir.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
        //                gridViajesImprimir.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

        //                //You can set the styles here
        //                gridViajesImprimir.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //                gridViajesImprimir.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
        //                gridViajesImprimir.Rows[0].Cells[0].Font.Bold = true;

        //                //set No Results found to the new added cell
        //                gridViajesImprimir.Rows[0].Cells[0].Text = "No hay registros";
        //            }
        //        }
        //    }
        //}

        //private void BindGridPagos(int cliente_ID, string month = "", string year = "")
        //{
        //    if (cliente_ID > 0)
        //    {
        //        // Logger variables
        //        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //        System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //        string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //        string methodName = stackFrame.GetMethod().Name;

        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            var elements = context.fletero_pagos.Where(v => v.Fletero_ID == cliente_ID && (v.isFicticio ?? false)).OrderBy(e => e.Fecha_pago).ToList();

        //            // Filtro por fechas
        //            if (!string.IsNullOrWhiteSpace(month) && !string.IsNullOrWhiteSpace(year))
        //            {
        //                int month_int = 0;
        //                if (!int.TryParse(month, out month_int))
        //                {
        //                    month_int = 0;
        //                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, month);
        //                }
        //                int year_int = 0;
        //                if (!int.TryParse(year, out year_int))
        //                {
        //                    year_int = 0;
        //                    Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, year);
        //                }
        //                if (month_int > 0 && year_int > 0)
        //                {
        //                    // Obtiene pagos del mes corriente
        //                    elements = GetPagosByMonth(context, cliente_ID, month_int, year_int);

        //                    // Obtiene pagos del mes anterior - Saldo Inicial
        //                    decimal saldo_anterior = 0;
        //                    var elements_anterior = GetPagosByMonth(context, cliente_ID, --month_int, year_int);
        //                    if (elements_anterior.Count() > 0)
        //                    {
        //                        decimal total_importe = elements_anterior.Sum(x => x.Importe_viaje);
        //                        decimal total_pagos = elements_anterior.Sum(x => x.Monto);
        //                        saldo_anterior = total_importe - total_pagos;
        //                    }

        //                    hdn_SaldoAnterior.Value = saldo_anterior.ToString();
        //                    lblSaldo_inicial.Text = String.Format("{0:n}", saldo_anterior);
        //                }

        //                if (elements.Count() > 0)
        //                {
        //                    gridPagos.DataSource = elements;
        //                    gridPagos.DataBind();

        //                    gridPagos.UseAccessibleHeader = true;
        //                    gridPagos.HeaderRow.TableSection = TableRowSection.TableHeader;

        //                    lblGridPagosCount.Text = "# " + elements.Count();
        //                }
        //                else
        //                {
        //                    var obj = new List<fletero_pagos>();
        //                    obj.Add(new fletero_pagos());

        //                    /* Grid Viajes */

        //                    // Bind the DataTable which contain a blank row to the GridView
        //                    gridPagos.DataSource = obj;
        //                    gridPagos.DataBind();
        //                    int columnsCount = gridPagos.Columns.Count;
        //                    gridPagos.Rows[0].Cells.Clear();// clear all the cells in the row
        //                    gridPagos.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
        //                    gridPagos.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

        //                    //You can set the styles here
        //                    gridPagos.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //                    gridPagos.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
        //                    gridPagos.Rows[0].Cells[0].Font.Bold = true;

        //                    //set No Results found to the new added cell
        //                    gridPagos.Rows[0].Cells[0].Text = "No hay registros";
        //                }
        //            }


        //        }
        //    }
        //}

        //public static List<fletero_pagos> GetPagosByMonth(bonisoftEntities context, int cliente_ID, int month_int, int year_int, bool include_ficticio = false)
        //{
        //    // Si el mes = 0 (Enero ==> mes = 12)
        //    if (month_int == 0)
        //    {
        //        month_int = 12;
        //        year_int--;
        //    }

        //    DateTime date1 = new DateTime(year_int, month_int, 1);
        //    int last_day = DateTime.DaysInMonth(year_int, month_int);
        //    DateTime date2 = new DateTime(year_int, month_int, last_day);

        //    // Excluye ficticios
        //    var ret = context.fletero_pagos.Where(v => v.Fletero_ID == cliente_ID && (v.Fecha_pago >= date1 && v.Fecha_pago <= date2) && (!v.isFicticio.HasValue)).OrderBy(e => e.Fecha_pago).ToList();
        //    if (include_ficticio)
        //    {
        //        ret = context.fletero_pagos.Where(v => v.Fletero_ID == cliente_ID && (v.Fecha_pago >= date1 && v.Fecha_pago <= date2)).OrderBy(e => e.Fecha_pago).ToList();
        //    }
        //    return ret;
        //}

        //public static List<fletero_pagos> GetPagosToMonth(bonisoftEntities context, int cliente_ID, int month_int, int year_int, bool include_ficticio = false)
        //{
        //    // Desde hace 5 años
        //    DateTime date1 = new DateTime(DateTime.Now.Year - 5, 1, 1);

        //    // Si el mes = 0 (Enero ==> mes = 12)
        //    if (month_int == 0)
        //    {
        //        month_int = 12;
        //        year_int--;
        //    }

        //    // Hasta el año y mes del filtro 
        //    int last_day = DateTime.DaysInMonth(year_int, month_int);
        //    DateTime date2 = new DateTime(year_int, month_int, last_day);

        //    // Excluye ficticios
        //    var ret = context.fletero_pagos.Where(v => v.Fletero_ID == cliente_ID && (v.Fecha_pago >= date1 && v.Fecha_pago <= date2) && (!v.isFicticio.HasValue)).OrderBy(e => e.Fecha_pago).ToList();
        //    if (include_ficticio)
        //    {
        //        ret = context.fletero_pagos.Where(v => v.Fletero_ID == cliente_ID && (v.Fecha_pago >= date1 && v.Fecha_pago <= date2)).OrderBy(e => e.Fecha_pago).ToList();
        //    }
        //    return ret;
        //}

        //private void BindModalAgregarPago()
        //{
        //    // Formas de pago --------------------------------------------------
        //    if (add_ddlFormas != null)
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            DataTable dt1 = new DataTable();
        //            dt1 = Extras.ToDataTable(context.forma_de_pago.ToList());

        //            add_ddlFormas.DataSource = dt1;
        //            add_ddlFormas.DataTextField = "Forma";
        //            add_ddlFormas.DataValueField = "Forma_de_pago_ID";
        //            add_ddlFormas.DataBind();
        //            add_ddlFormas.Items.Insert(0, new ListItem("Elegir", "0"));

        //        }
        //    }
        //}

        //private void BindModalModificarPago()
        //{
        //    // Formas de pago --------------------------------------------------
        //    if (edit_ddlFormas != null)
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            DataTable dt1 = new DataTable();
        //            dt1 = Extras.ToDataTable(context.forma_de_pago.ToList());

        //            edit_ddlFormas.DataSource = dt1;
        //            edit_ddlFormas.DataTextField = "Forma";
        //            edit_ddlFormas.DataValueField = "Forma_de_pago_ID";
        //            edit_ddlFormas.DataBind();
        //            edit_ddlFormas.Items.Insert(0, new ListItem("Elegir", "0"));

        //        }
        //    }
        //}

        //protected void PrintCurrentPage(object sender, EventArgs e)
        //{
        //    gridViajesImprimir.PagerSettings.Visible = false;
        //    gridViajesImprimir.DataBind();
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    gridViajesImprimir.RenderControl(hw);
        //    string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
        //    System.Text.StringBuilder sb = new StringBuilder();
        //    sb.Append("<script type = 'text/javascript'>");
        //    sb.Append("window.onload = new function(){");
        //    sb.Append("var printWin = window.open('', '', 'left=0");
        //    sb.Append(",top=0,width=1000,height=600,status=0');");
        //    sb.Append("printWin.document.write(\"");
        //    sb.Append(gridHTML);
        //    sb.Append("\");");
        //    sb.Append("printWin.document.close();");
        //    sb.Append("printWin.focus();");
        //    sb.Append("printWin.print();");
        //    sb.Append("printWin.close();};");
        //    sb.Append("</script>");
        //    ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        //    gridViajesImprimir.PagerSettings.Visible = true;
        //    gridViajesImprimir.DataBind();
        //}

        //protected void PrintAllPages(object sender, EventArgs e)
        //{
        //    gridViajesImprimir.AllowPaging = false;
        //    gridViajesImprimir.DataBind();
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    gridViajesImprimir.RenderControl(hw);
        //    string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<script type = 'text/javascript'>");
        //    sb.Append("window.onload = new function(){");
        //    sb.Append("var printWin = window.open('', '', 'left=0");
        //    sb.Append(",top=0,width=1000,height=600,status=0');");
        //    sb.Append("printWin.document.write(\"");
        //    sb.Append(gridHTML);
        //    sb.Append("\");");
        //    sb.Append("printWin.document.close();");
        //    sb.Append("printWin.focus();");
        //    sb.Append("printWin.print();");
        //    sb.Append("printWin.close();};");
        //    sb.Append("</script>");
        //    ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        //    gridViajesImprimir.AllowPaging = true;
        //    gridViajesImprimir.DataBind();
        //}

        //protected void ExportToExcel(object sender, EventArgs e)
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment;filename=Datos.xls");
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);

        //        //To Export all pages
        //        gridPagos.AllowPaging = false;

        //        //Hide cells
        //        gridPagos.HeaderRow.Cells[0].Visible = false;
        //        gridPagos.HeaderRow.Cells[1].Visible = false;
        //        gridPagos.HeaderRow.Cells[7].Visible = false;

        //        gridPagos.HeaderRow.BackColor = Color.White;
        //        foreach (TableCell cell in gridPagos.HeaderRow.Cells)
        //        {
        //            cell.BackColor = Color.LightBlue;
        //        }
        //        foreach (GridViewRow row in gridPagos.Rows)
        //        {
        //            //Hide cells
        //            row.Cells[0].Visible = false;
        //            row.Cells[1].Visible = false;
        //            row.Cells[7].Visible = false;

        //            row.BackColor = Color.White;
        //            foreach (TableCell cell in row.Cells)
        //            {
        //                if (row.RowIndex % 2 == 0)
        //                {
        //                    cell.BackColor = Color.LightGray;
        //                }
        //                else
        //                {
        //                    cell.BackColor = gridPagos.RowStyle.BackColor;
        //                }
        //                cell.CssClass = "textmode";
        //            }
        //        }

        //        gridPagos.RenderControl(hw);

        //        //style to format numbers to string
        //        string style = @"<style> .textmode { } </style>";
        //        Response.Write(style);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //}

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Verifies that the control is rendered */
        //}

        //#endregion General methods

        //#region Static methods

        //private static string AgregarFormaPago(string valor)
        //{
        //    string ID_result = "0";

        //    // Logger variables
        //    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    string methodName = stackFrame.GetMethod().Name;

        //    using (bonisoftEntities context = new bonisoftEntities())
        //    {
        //        forma_de_pago obj = new forma_de_pago();
        //        obj.Forma = valor;
        //        obj.Comentarios = string.Empty;

        //        context.forma_de_pago.Add(obj);
        //        context.SaveChanges();

        //        #region Guardar log 
        //        try
        //        {
        //            int id = 1;
        //            forma_de_pago forma_de_pago = (forma_de_pago)context.forma_de_pago.OrderByDescending(p => p.Forma_de_pago_ID).FirstOrDefault();
        //            if (forma_de_pago != null)
        //            {
        //                id = forma_de_pago.Forma_de_pago_ID;
        //            }

        //            string userID1 = HttpContext.Current.Session["UserID"].ToString();
        //            string username = HttpContext.Current.Session["UserName"].ToString();
        //            Logs.AddUserLog("Agrega forma de pago", forma_de_pago.GetType().Name + ": " + id, userID1, username);

        //            ID_result = id.ToString();
        //        }
        //        catch (Exception ex)
        //        {
        //            Logs.AddErrorLog("Excepcion. Guardando log. ERROR:", className, methodName, ex.Message);
        //        }
        //        #endregion
        //    }

        //    return ID_result;
        //}

        //#endregion 

        //#region Web methods

        //[WebMethod]
        //public static string Update_Saldos(string clienteID_str, string month_str, string year_str)
        //{
        //    // Logger variables
        //    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    string methodName = stackFrame.GetMethod().Name;

        //    string ret = string.Empty;
        //    if (!string.IsNullOrWhiteSpace(clienteID_str) && !string.IsNullOrWhiteSpace(month_str) && !string.IsNullOrWhiteSpace(year_str))
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            int cliente_ID = 0;
        //            if (!int.TryParse(clienteID_str, out cliente_ID))
        //            {
        //                cliente_ID = 0;
        //                Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, clienteID_str);
        //            }

        //            int month_int = 0;
        //            if (!int.TryParse(month_str, out month_int))
        //            {
        //                month_int = 0;
        //                Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, month_str);
        //            }

        //            int year_int = 0;
        //            if (!int.TryParse(year_str, out year_int))
        //            {
        //                year_int = 0;
        //                Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, year_str);
        //            }

        //            if (cliente_ID > 0 && month_int > 0 && year_int > 0)
        //            {
        //                #region Cálculo saldo inicial

        //                // Recorro todos los meses hasta el anterior-actual, traigo todos los pagos hasta month_int
        //                decimal saldo_inicial = 0;
        //                var elements_anterior = GetPagosToMonth(context, cliente_ID, month_int - 1, year_int, true);
        //                if (elements_anterior.Count() > 0)
        //                {
        //                    decimal total_importe = elements_anterior.Sum(x => x.Importe_viaje);
        //                    decimal total_pagos1 = elements_anterior.Sum(x => x.Monto);
        //                    saldo_inicial = total_importe - total_pagos1;
        //                }

        //                #endregion Cálculo saldo inicial

        //                #region Cálculo saldo final

        //                decimal total_importes = 0;
        //                decimal total_pagos = 0;
        //                var elements = GetPagosByMonth(context, cliente_ID, month_int, year_int);
        //                if (elements.ToList().Count > 0)
        //                {
        //                    total_importes = elements.Sum(x => x.Importe_viaje);
        //                    total_pagos = elements.Sum(x => x.Monto);
        //                }

        //                // Al revés de los Clientes
        //                decimal saldo_final = total_pagos - (total_importes + saldo_inicial);
        //                ret = saldo_inicial.ToString() + "|" + saldo_final.ToString();

        //                #endregion Cálculo saldo final
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod]
        //public static bool IngresarPago(string clienteID_str, string fecha_str, string ddlFormas, string monto_str, string comentarios_str)
        //{
        //    // Logger variables
        //    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    string methodName = stackFrame.GetMethod().Name;

        //    bool ret = false;
        //    if (!string.IsNullOrWhiteSpace(clienteID_str))
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            int cliente_ID = 0;
        //            if (!int.TryParse(clienteID_str, out cliente_ID))
        //            {
        //                cliente_ID = 0;
        //                Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, clienteID_str);
        //            }

        //            if (cliente_ID > 0)
        //            {
        //                fletero_pagos obj = new fletero_pagos();

        //                obj.Fletero_ID = cliente_ID;
        //                obj.Comentarios = comentarios_str;
        //                obj.Fecha_registro = DateTime.Now;

        //                DateTime date = DateTime.MinValue;
        //                if (!string.IsNullOrWhiteSpace(fecha_str))
        //                {
        //                    if (!DateTime.TryParseExact(fecha_str, GlobalVariables.ShortDateTime_format, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
        //                    {
        //                        date = DateTime.MinValue;
        //                        Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo datetime. ERROR:", className, methodName, fecha_str);
        //                    }
        //                }
        //                obj.Fecha_pago = date;

        //                decimal value = 0;
        //                if (!decimal.TryParse(monto_str, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
        //                {
        //                    value = 0;
        //                    Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo decimal. ERROR:", className, methodName, monto_str);
        //                }

        //                obj.Monto = value;

        //                int ddl = 0;
        //                if (!int.TryParse(ddlFormas, out ddl))
        //                {
        //                    ddl = 0;
        //                    Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlFormas);
        //                }
        //                obj.Forma_de_pago_ID = ddl;

        //                context.fletero_pagos.Add(obj);
        //                context.SaveChanges();

        //                #region Guardar log 
        //                try
        //                {
        //                    int id = 1;
        //                    fletero_pagos cliente_pago1 = (fletero_pagos)context.fletero_pagos.OrderByDescending(p => p.Fletero_pagos_ID).FirstOrDefault();
        //                    if (cliente_pago1 != null)
        //                    {
        //                        id = cliente_pago1.Fletero_pagos_ID;
        //                    }

        //                    string userID1 = HttpContext.Current.Session["UserID"].ToString();
        //                    string username = HttpContext.Current.Session["UserName"].ToString();
        //                    Global_Objects.Logs.AddUserLog("Agrega pago", obj.GetType().Name + ": " + id, userID1, username);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Global_Objects.Logs.AddErrorLog("Excepcion. Guardando log. ERROR:", className, methodName, ex.Message);
        //                }
        //                #endregion

        //                ret = true;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod]
        //public static bool BorrarPago(string pagoID_str)
        //{
        //    // Logger variables
        //    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    string methodName = stackFrame.GetMethod().Name;

        //    bool ret = false;
        //    if (!string.IsNullOrWhiteSpace(pagoID_str))
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            int pago_ID_str = 0;
        //            if (!int.TryParse(pagoID_str, out pago_ID_str))
        //            {
        //                pago_ID_str = 0;
        //                Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, pagoID_str);
        //            }

        //            if (pago_ID_str > 0)
        //            {
        //                fletero_pagos pago = (fletero_pagos)context.fletero_pagos.FirstOrDefault(v => v.Fletero_pagos_ID == pago_ID_str);
        //                if (pago != null)
        //                {
        //                    context.fletero_pagos.Remove(pago);
        //                    context.SaveChanges();

        //                    #region Guardar log 
        //                    try
        //                    {
        //                        string userID1 = HttpContext.Current.Session["UserID"].ToString();
        //                        string username = HttpContext.Current.Session["UserName"].ToString();
        //                        Global_Objects.Logs.AddUserLog("Borra pago", pago.GetType().Name + ": " + pago.Fletero_pagos_ID, userID1, username);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Global_Objects.Logs.AddErrorLog("Excepcion. Guardando log. ERROR:", className, methodName, ex.Message);
        //                    }
        //                    #endregion

        //                    ret = true;
        //                }
        //            }
        //        }
        //    }
        //    return ret;
        //}

        //[WebMethod]
        //public static bool IngresarImporte(string clienteID_str, string fecha_str, string monto_str, string comentarios_str)
        //{
        //    // Logger variables
        //    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    string methodName = stackFrame.GetMethod().Name;

        //    bool ret = false;
        //    if (!string.IsNullOrWhiteSpace(clienteID_str))
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            int cliente_ID = 0;
        //            if (!int.TryParse(clienteID_str, out cliente_ID))
        //            {
        //                cliente_ID = 0;
        //                Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, clienteID_str);
        //            }

        //            if (cliente_ID > 0)
        //            {
        //                fletero_pagos obj = new fletero_pagos();

        //                obj.Fletero_pagos_ID = cliente_ID;
        //                obj.Comentarios = comentarios_str;
        //                obj.Fecha_registro = DateTime.Now;

        //                DateTime date = DateTime.MinValue;
        //                if (!string.IsNullOrWhiteSpace(fecha_str))
        //                {
        //                    if (!DateTime.TryParseExact(fecha_str, GlobalVariables.ShortDateTime_format, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
        //                    {
        //                        date = DateTime.MinValue;
        //                        Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo datetime. ERROR:", className, methodName, fecha_str);
        //                    }
        //                }
        //                obj.Fecha_pago = date;

        //                decimal value = 0;
        //                if (!decimal.TryParse(monto_str, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
        //                {
        //                    value = 0;
        //                    Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo decimal. ERROR:", className, methodName, monto_str);
        //                }
        //                obj.Monto = value;

        //                obj.Forma_de_pago_ID = 0;

        //                context.fletero_pagos.Add(obj);
        //                context.SaveChanges();

        //                #region Guardar log 
        //                try
        //                {
        //                    int id = 1;
        //                    fletero_pagos cliente_pago1 = (fletero_pagos)context.fletero_pagos.OrderByDescending(p => p.Fletero_pagos_ID).FirstOrDefault();
        //                    if (cliente_pago1 != null)
        //                    {
        //                        id = cliente_pago1.Fletero_pagos_ID;
        //                    }

        //                    string userID1 = HttpContext.Current.Session["UserID"].ToString();
        //                    string username = HttpContext.Current.Session["UserName"].ToString();
        //                    Global_Objects.Logs.AddUserLog("Agrega pago", obj.GetType().Name + ": " + id, userID1, username);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Global_Objects.Logs.AddErrorLog("Excepcion. Guardando log. ERROR:", className, methodName, ex.Message);
        //                }
        //                #endregion

        //                ret = true;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod]
        //public static bool BorrarImporte(string pagoID_str)
        //{
        //    // Logger variables
        //    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    string methodName = stackFrame.GetMethod().Name;

        //    bool ret = false;
        //    if (!string.IsNullOrWhiteSpace(pagoID_str))
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            int pago_ID_str = 0;
        //            if (!int.TryParse(pagoID_str, out pago_ID_str))
        //            {
        //                pago_ID_str = 0;
        //                Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, pagoID_str);
        //            }

        //            if (pago_ID_str > 0)
        //            {
        //                fletero_pagos pago = (fletero_pagos)context.fletero_pagos.FirstOrDefault(v => v.Fletero_pagos_ID == pago_ID_str);
        //                if (pago != null)
        //                {
        //                    context.fletero_pagos.Remove(pago);
        //                    context.SaveChanges();

        //                    #region Guardar log 
        //                    try
        //                    {
        //                        string userID1 = HttpContext.Current.Session["UserID"].ToString();
        //                        string username = HttpContext.Current.Session["UserName"].ToString();
        //                        Global_Objects.Logs.AddUserLog("Borra pago", pago.GetType().Name + ": " + pago.Fletero_pagos_ID, userID1, username);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Global_Objects.Logs.AddErrorLog("Excepcion. Guardando log. ERROR:", className, methodName, ex.Message);
        //                    }
        //                    #endregion

        //                    ret = true;
        //                }
        //            }
        //        }
        //    }
        //    return ret;
        //}

        //[WebMethod]
        //public static string ModificarPago_1(string pagoID_str)
        //{
        //    // Logger variables
        //    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    string methodName = stackFrame.GetMethod().Name;


        //    string ret = string.Empty;
        //    if (!string.IsNullOrWhiteSpace(pagoID_str))
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            int pago_ID = 0;
        //            if (!int.TryParse(pagoID_str, out pago_ID))
        //            {
        //                pago_ID = 0;
        //                Global_Objects.Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, pagoID_str);
        //            }

        //            if (pago_ID > 0)
        //            {
        //                fletero_pagos pago = (fletero_pagos)context.fletero_pagos.FirstOrDefault(v => v.Fletero_pagos_ID == pago_ID);
        //                if (pago != null)
        //                {
        //                    string fecha = pago.Fecha_pago.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        //                    ret = fecha + "|" + pago.Forma_de_pago_ID + "|" + pago.Monto + "|" + pago.Comentarios;
        //                }
        //            }
        //        }
        //    }
        //    return ret;
        //}

        //[WebMethod]
        //public static bool ModificarPago_2(string pagoID_str, string fecha_str, string ddlFormas, string monto_str, string comentarios_str)
        //{
        //    // Logger variables
        //    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    string methodName = stackFrame.GetMethod().Name;

        //    bool ret = false;
        //    if (!string.IsNullOrWhiteSpace(pagoID_str))
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            int pago_ID = 0;
        //            if (!int.TryParse(pagoID_str, out pago_ID))
        //            {
        //                pago_ID = 0;
        //                Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, pagoID_str);
        //            }

        //            if (pago_ID > 0)
        //            {
        //                fletero_pagos pago = (fletero_pagos)context.fletero_pagos.FirstOrDefault(v => v.Fletero_pagos_ID == pago_ID);
        //                if (pago != null)
        //                {
        //                    DateTime date = pago.Fecha_pago;
        //                    if (!string.IsNullOrWhiteSpace(fecha_str))
        //                    {
        //                        if (!DateTime.TryParseExact(fecha_str, GlobalVariables.ShortDateTime_format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        //                        {
        //                            date = pago.Fecha_pago;
        //                            Logs.AddErrorLog("Excepcion. Convirtiendo datetime. ERROR:", className, methodName, fecha_str);
        //                        }
        //                    }
        //                    pago.Fecha_pago = date;

        //                    int ddl = 0;
        //                    ddl = pago.Forma_de_pago_ID;
        //                    if (!int.TryParse(ddlFormas, out ddl))
        //                    {
        //                        ddl = pago.Forma_de_pago_ID;
        //                        Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, ddlFormas);
        //                    }
        //                    pago.Forma_de_pago_ID = ddl;

        //                    decimal value = pago.Monto;
        //                    if (!decimal.TryParse(monto_str, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
        //                    {
        //                        value = pago.Monto;
        //                        Logs.AddErrorLog("Excepcion. Convirtiendo decimal. ERROR:", className, methodName, monto_str);
        //                    }

        //                    pago.Monto = value;

        //                    pago.Comentarios = comentarios_str;

        //                    context.SaveChanges();

        //                    #region Guardar log 
        //                    try
        //                    {
        //                        string userID1 = HttpContext.Current.Session["UserID"].ToString();
        //                        string username = HttpContext.Current.Session["UserName"].ToString();
        //                        Global_Objects.Logs.AddUserLog("Modifica pago", pago.GetType().Name + ": " + pago.Fletero_pagos_ID, userID1, username);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Global_Objects.Logs.AddErrorLog("Excepcion. Guardando log. ERROR:", className, methodName, ex.Message);
        //                    }
        //                    #endregion

        //                    ret = true;
        //                }
        //            }
        //        }
        //    }
        //    return ret;
        //}

        //[WebMethod]
        //public static bool ModificarImporte_2(string pagoID_str, string fecha_str, string monto_str, string comentarios_str)
        //{
        //    // Logger variables
        //    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        //    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        //    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    string methodName = stackFrame.GetMethod().Name;

        //    bool ret = false;
        //    if (!string.IsNullOrWhiteSpace(pagoID_str))
        //    {
        //        using (bonisoftEntities context = new bonisoftEntities())
        //        {
        //            int pago_ID = 0;
        //            if (!int.TryParse(pagoID_str, out pago_ID))
        //            {
        //                pago_ID = 0;
        //                Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, pagoID_str);
        //            }

        //            if (pago_ID > 0)
        //            {
        //                fletero_pagos pago = (fletero_pagos)context.fletero_pagos.FirstOrDefault(v => v.Fletero_pagos_ID == pago_ID);
        //                if (pago != null)
        //                {
        //                    DateTime date = pago.Fecha_pago;
        //                    if (!string.IsNullOrWhiteSpace(fecha_str))
        //                    {
        //                        if (!DateTime.TryParseExact(fecha_str, GlobalVariables.ShortDateTime_format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        //                        {
        //                            date = pago.Fecha_pago;
        //                            Logs.AddErrorLog("Excepcion. Convirtiendo datetime. ERROR:", className, methodName, fecha_str);
        //                        }
        //                    }
        //                    pago.Fecha_pago = date;

        //                    decimal value = pago.Monto;
        //                    if (!decimal.TryParse(monto_str, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
        //                    {
        //                        value = pago.Monto;
        //                        Logs.AddErrorLog("Excepcion. Convirtiendo decimal. ERROR:", className, methodName, monto_str);
        //                    }
        //                    pago.Monto = value;

        //                    pago.Comentarios = comentarios_str;

        //                    context.SaveChanges();

        //                    #region Guardar log 
        //                    try
        //                    {
        //                        string userID1 = HttpContext.Current.Session["UserID"].ToString();
        //                        string username = HttpContext.Current.Session["UserName"].ToString();
        //                        Global_Objects.Logs.AddUserLog("Modifica pago", pago.GetType().Name + ": " + pago.Fletero_pagos_ID, userID1, username);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Global_Objects.Logs.AddErrorLog("Excepcion. Guardando log. ERROR:", className, methodName, ex.Message);
        //                    }
        //                    #endregion

        //                    ret = true;
        //                }
        //            }
        //        }
        //    }
        //    return ret;
        //}

        ////[WebMethod]
        ////public static bool ViajeFicticio_2(string saldo_str, string comentarios, string month_str, string clienteID_str)
        ////{
        ////    // Logger variables
        ////    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        ////    System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
        ////    string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        ////    string methodName = stackFrame.GetMethod().Name;

        ////    bool ret = false;
        ////    if (!string.IsNullOrWhiteSpace(clienteID_str) && !string.IsNullOrWhiteSpace(month_str))
        ////    {
        ////        using (bonisoftEntities context = new bonisoftEntities())
        ////        {
        ////            int clienteID = 0;
        ////            if (!int.TryParse(clienteID_str, out clienteID))
        ////            {
        ////                clienteID = 0;
        ////                Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, clienteID_str);
        ////            }

        ////            int month_int = 0;
        ////            if (!int.TryParse(month_str, out month_int))
        ////            {
        ////                month_int = 0;
        ////                Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, month_str);
        ////            }

        ////            if (clienteID > 0 && month_int > 0)
        ////            {
        ////                // Mes anterior para saldo anterior
        ////                month_int--;

        ////                // Get month
        ////                DateTime date1 = new DateTime(DateTime.Now.Year, month_int, 1);
        ////                int last_day = DateTime.DaysInMonth(DateTime.Now.Year, month_int);
        ////                DateTime date2 = new DateTime(DateTime.Now.Year, month_int, last_day);

        ////                bool isNew = false;
        ////                decimal saldo = 0;

        ////                // Busca un pago ficticio en el mes anterior
        ////                fletero_pagos fletero_pagos = GetPagosByMonth(context, clienteID, month_int, true).FirstOrDefault();
        ////                if (fletero_pagos != null)
        ////                {
        ////                    saldo = fletero_pagos.Importe_viaje; //
        ////                }
        ////                else
        ////                {
        ////                    isNew = true;

        ////                    fletero_pagos = new fletero_pagos();
        ////                    fletero_pagos.isFicticio = true;
        ////                    fletero_pagos.Fletero_ID = clienteID;
        ////                }
        ////                if (!string.IsNullOrWhiteSpace(saldo_str))
        ////                {
        ////                    if (!decimal.TryParse(saldo_str, NumberStyles.Number, CultureInfo.InvariantCulture, out saldo))
        ////                    {
        ////                        saldo = 0;
        ////                        Logs.AddErrorLog("Excepcion. Convirtiendo decimal. ERROR:", className, methodName, saldo_str);
        ////                    }
        ////                }
        ////                fletero_pagos.Importe_viaje = saldo; //
        ////                fletero_pagos.Comentarios = comentarios;

        ////                if (isNew)
        ////                {
        ////                    fletero_pagos.Fecha_pago = date1;
        ////                    fletero_pagos.Fecha_registro = DateTime.Now;
        ////                    fletero_pagos.Forma_de_pago_ID = 0;
        ////                    fletero_pagos.Monto = 0;
        ////                    fletero_pagos.Viaje_ID = 0;

        ////                    context.fletero_pagos.Add(fletero_pagos);
        ////                }
        ////                context.SaveChanges();

        ////                #region Guardar log
        ////                try
        ////                {
        ////                    int id = 1;
        ////                    if (fletero_pagos != null)
        ////                    {
        ////                        id = fletero_pagos.Fletero_pagos_ID;
        ////                    }

        ////                    string userID1 = HttpContext.Current.Session["UserID"].ToString();
        ////                    string username = HttpContext.Current.Session["UserName"].ToString();
        ////                    Logs.AddUserLog("Modifica fletero_pagos ficticio", fletero_pagos.GetType().Name + ": " + fletero_pagos.Fletero_pagos_ID, userID1, username);
        ////                }
        ////                catch (Exception ex)
        ////                {
        ////                    Logs.AddErrorLog("Excepcion. Guardando log. ERROR:", className, methodName, ex.Message);
        ////                }
        ////                #endregion

        ////                ret = true;
        ////            }
        ////        }
        ////    }
        ////    return ret;
        ////}

        //[WebMethod]
        //public static string AgregarOpcionDDL(string tipo, string valor)
        //{
        //    string ID_result = "0";
        //    if (!string.IsNullOrWhiteSpace(tipo) && !string.IsNullOrWhiteSpace(valor))
        //    {
        //        switch (tipo)
        //        {
        //            case "forma_pago":
        //                {
        //                    ID_result = AgregarFormaPago(valor);
        //                    break;
        //                }
        //        }
        //    }
        //    return ID_result;
        //}

        //#endregion

    }
}

