using Cartelux1.Global_Objects;
using Cartelux1.Helpers;
using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cartelux1
{
    public partial class Trading_Dashboard1 : System.Web.UI.Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_GridTradeActivos();
                Bind_GridOrdenesPendientes();
            }

            gridTradesActivos.UseAccessibleHeader = true;
            gridTradesActivos.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridTradesActivos.FooterRow.TableSection = TableRowSection.TableFooter;

            gridOrdenesPendientes.UseAccessibleHeader = true;
            gridOrdenesPendientes.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridOrdenesPendientes.FooterRow.TableSection = TableRowSection.TableFooter;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            //string hdn_ordenID_str = hdn_ordenID.Value;
            //if (!string.IsNullOrWhiteSpace(hdn_ordenID_str))
            //{

            //}
        }

        protected void gridTradesActivos_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gridTradesActivos_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    trading_ordenes _orden = (trading_ordenes)(e.Row.DataItem);
                    if (_orden != null)
                    {

                    trading_lista_operaciones _operacion = (trading_lista_operaciones)context.trading_lista_operaciones.FirstOrDefault(c => c.Operacion_ID == _orden.Operacion_ID);
                    trading_trades _trade = (trading_trades)context.trading_trades.FirstOrDefault(c => c.Trade_ID == _orden.Trade_ID);
                    if (_operacion != null && _trade != null)
                    {
                        Label lbl1 = e.Row.FindControl("lblNumber") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = e.Row.RowIndex.ToString();
                        }

                            lbl1 = e.Row.FindControl("lblOperacion") as Label;
                            if (lbl1 != null)
                            {
                                lbl1.Text = _operacion.Nombre;
                            }

                            trading_activos _activo = (trading_activos)context.trading_activos.FirstOrDefault(c => c.Activo_ID == _trade.Activo_ID);
                            if (_activo != null)
                            {
                                trading_lista_instrumentos _instrumento = (trading_lista_instrumentos)context.trading_lista_instrumentos.FirstOrDefault(c => c.Instrumento_ID == _activo.Instrumento_ID);
                                if (_instrumento != null)
                                {
                                    lbl1 = e.Row.FindControl("lblInstrumento") as Label;
                                    if (lbl1 != null)
                                    {
                                        lbl1.Text = _instrumento.Nombre;
                                    }
                                }

                                lbl1 = e.Row.FindControl("lblTicker") as Label;
                                if (lbl1 != null)
                                {
                                    lbl1.Text = _activo.Ticker;
                                }

                                lbl1 = e.Row.FindControl("lblNombre") as Label;
                                if (lbl1 != null)
                                {
                                    lbl1.Text = _activo.Nombre;
                                }
                            }

                            lbl1 = e.Row.FindControl("lblValorUnitario") as Label;
                            if (lbl1 != null)
                            {
                                if (_trade.Valor != null)
                                {
                                    lbl1.Text = "$ " + _trade.Valor.ToString();
                                }
                            }

                            lbl1 = e.Row.FindControl("lblCantidad") as Label;
                            if (lbl1 != null)
                            {
                                if (_trade.Cantidad != null)
                                {
                                    lbl1.Text = _trade.Cantidad.ToString();
                                }
                            }

                            lbl1 = e.Row.FindControl("lblValorTotal") as Label;
                            if (lbl1 != null)
                            {
                                if (_trade.Valor != null && _trade.Cantidad != null)
                                {
                                    lbl1.Text = "$ " + (_trade.Valor * _trade.Cantidad).ToString();
                                }
                            }
                            /*
                        if (lbl1 != null)
                        {
                            trading_ordenes _pedido_entrega = (trading_ordenes)context.trading_ordenes.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
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
                                string tamano_tipo_ID_str = _pedido.Pedido_Tamano_ID.ToString(); // Se usa ID, no código
                                if (_orden.Fecha_update <= GlobalVariables.GetDatetimeFormated("22-07-2019")) //dd-MM-yyyy
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
                                lbl1.Text = _orden.Monto.ToString();
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
                        */

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
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridTradesActivos, "Select$" + e.Row.RowIndex);
                }
            } // context
        }

        protected void gridTradesActivos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Source: http://www.codeproject.com/Tips/622720/Fire-GridView-SelectedIndexChanged-Event-without-S

            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            foreach (GridViewRow row in gridTradesActivos.Rows)
            {
                if (row.RowIndex == gridTradesActivos.SelectedIndex)
                {
                    string Orden_ID_str = gridTradesActivos.SelectedRow.Cells[0].Text;
                    if (!string.IsNullOrWhiteSpace(Orden_ID_str))
                    {
                        int Orden_ID = 0;
                        if (!int.TryParse(Orden_ID_str, out Orden_ID))
                        {
                            Orden_ID = 0;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, Orden_ID_str);
                        }

                        if (Orden_ID > 0)
                        {
                            using (CarteluxDB context = new CarteluxDB())
                            {
                                trading_ordenes _orden = (trading_ordenes)context.trading_ordenes.FirstOrDefault(c => c.Orden_ID == Orden_ID);
                                if (_orden != null)
                                {
                                    //lblClientName_1.Text = lblClientName_2.Text = _orden.Nombre;

                                    //BindGridViajes(cliente_ID);

                                    // Filtrar por fechas del mes corriente por defecto
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "gridTradesActivos_OnSelectedIndexChanged", "<script type='text/javascript'></script>", false);

                                    hdn_OrdenID.Value = Orden_ID_str;
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

        protected void gridOrdenesPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gridOrdenesPendientes_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    trading_ordenes _orden = (trading_ordenes)(e.Row.DataItem);
                    if (_orden != null)
                    {

                        trading_lista_operaciones _operacion = (trading_lista_operaciones)context.trading_lista_operaciones.FirstOrDefault(c => c.Operacion_ID == _orden.Operacion_ID);
                        trading_trades _trade = (trading_trades)context.trading_trades.FirstOrDefault(c => c.Trade_ID == _orden.Trade_ID);
                        if (_operacion != null && _trade != null)
                        {
                            Label lbl1 = e.Row.FindControl("lblNumber") as Label;
                            if (lbl1 != null)
                            {
                                lbl1.Text = e.Row.RowIndex.ToString();
                            }

                            lbl1 = e.Row.FindControl("lblOperacion") as Label;
                            if (lbl1 != null)
                            {
                                lbl1.Text = _operacion.Nombre;
                            }

                            trading_activos _activo = (trading_activos)context.trading_activos.FirstOrDefault(c => c.Activo_ID == _trade.Activo_ID);
                            if (_activo != null)
                            {
                                trading_lista_instrumentos _instrumento = (trading_lista_instrumentos)context.trading_lista_instrumentos.FirstOrDefault(c => c.Instrumento_ID == _activo.Instrumento_ID);
                                if (_instrumento != null)
                                {
                                    lbl1 = e.Row.FindControl("lblInstrumento") as Label;
                                    if (lbl1 != null)
                                    {
                                        lbl1.Text = _instrumento.Nombre;
                                    }
                                }

                                lbl1 = e.Row.FindControl("lblTicker") as Label;
                                if (lbl1 != null)
                                {
                                    lbl1.Text = _activo.Ticker;
                                }

                                lbl1 = e.Row.FindControl("lblNombre") as Label;
                                if (lbl1 != null)
                                {
                                    lbl1.Text = _activo.Nombre;
                                }
                            }

                            lbl1 = e.Row.FindControl("lblValorUnitario") as Label;
                            if (lbl1 != null)
                            {
                                if (_trade.Valor != null)
                                {
                                    lbl1.Text = "$ " + _trade.Valor.ToString();
                                }
                            }

                            lbl1 = e.Row.FindControl("lblCantidad") as Label;
                            if (lbl1 != null)
                            {
                                if (_trade.Cantidad != null)
                                {
                                    lbl1.Text = _trade.Cantidad.ToString();
                                }
                            }

                            lbl1 = e.Row.FindControl("lblValorTotal") as Label;
                            if (lbl1 != null)
                            {
                                if (_trade.Valor != null && _trade.Cantidad != null)
                                {
                                    lbl1.Text = "$ " + (_trade.Valor * _trade.Cantidad).ToString();
                                }
                            }

                            /*
                        if (lbl1 != null)
                        {
                            trading_ordenes _pedido_entrega = (trading_ordenes)context.trading_ordenes.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
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
                                string tamano_tipo_ID_str = _pedido.Pedido_Tamano_ID.ToString(); // Se usa ID, no código
                                if (_orden.Fecha_update <= GlobalVariables.GetDatetimeFormated("22-07-2019")) //dd-MM-yyyy
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
                                lbl1.Text = _orden.Monto.ToString();
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
                        */

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
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridOrdenesPendientes, "Select$" + e.Row.RowIndex);
                }
            } // context
        }

        protected void gridOrdenesPendientes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Source: http://www.codeproject.com/Tips/622720/Fire-GridView-SelectedIndexChanged-Event-without-S

            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            foreach (GridViewRow row in gridOrdenesPendientes.Rows)
            {
                if (row.RowIndex == gridOrdenesPendientes.SelectedIndex)
                {
                    string Orden_ID_str = gridOrdenesPendientes.SelectedRow.Cells[0].Text;
                    if (!string.IsNullOrWhiteSpace(Orden_ID_str))
                    {
                        int Orden_ID = 0;
                        if (!int.TryParse(Orden_ID_str, out Orden_ID))
                        {
                            Orden_ID = 0;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, Orden_ID_str);
                        }

                        if (Orden_ID > 0)
                        {
                            using (CarteluxDB context = new CarteluxDB())
                            {
                                trading_ordenes _orden = (trading_ordenes)context.trading_ordenes.FirstOrDefault(c => c.Orden_ID == Orden_ID);
                                if (_orden != null)
                                {
                                    //lblClientName_1.Text = lblClientName_2.Text = _orden.Nombre;

                                    //BindGridViajes(cliente_ID);

                                    // Filtrar por fechas del mes corriente por defecto
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "gridOrdenesPendientes_OnSelectedIndexChanged", "<script type='text/javascript'></script>", false);

                                    hdn_OrdenID.Value = Orden_ID_str;
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

        private void Bind_GridTradeActivos()
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            #region Fill gridTradesActivos

            using (CarteluxDB context = new CarteluxDB())
            {
                if (context.trading_ordenes != null)
                {
                    List<trading_ordenes> trading_ordenes_elements = context.trading_ordenes.OrderByDescending(e => e.Datetime).ToList();

                    if (trading_ordenes_elements.Count() > 0)
                    {
                        gridTradesActivos.DataSource = trading_ordenes_elements;
                        gridTradesActivos.DataBind();

                        gridTradesActivos.UseAccessibleHeader = true;
                        gridTradesActivos.HeaderRow.TableSection = TableRowSection.TableHeader;

                        lblGridTradesActivosCount.Text = "# " + trading_ordenes_elements.Count();
                    }
                    else
                    {
                        var obj = new List<trading_ordenes>();
                        trading_ordenes _orden1 = new trading_ordenes();
                        _orden1.Orden_ID = 1;
                        _orden1.Operacion_ID = 1;
                        _orden1.Trade_ID = 1;
                        _orden1.Estado_ID = 1;
                        _orden1.Datetime = DateTime.MinValue;

                        obj.Add(_orden1);

                        // Bind the DataTable which contain a blank row to the GridView
                        gridTradesActivos.DataSource = obj;
                        gridTradesActivos.DataBind();
                        int columnsCount = gridTradesActivos.Columns.Count;
                        gridTradesActivos.Rows[0].Cells.Clear();// clear all the cells in the row
                        gridTradesActivos.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                        gridTradesActivos.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

                        //You can set the styles here
                        gridTradesActivos.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        gridTradesActivos.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                        gridTradesActivos.Rows[0].Cells[0].Font.Bold = true;

                        //set No Results found to the new added cell
                        gridTradesActivos.Rows[0].Cells[0].Text = "No hay registros";
                    }
                }
            } // using

            #endregion
        }

        private void Bind_GridOrdenesPendientes()
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            #region Fill gridOrdenesPendientes

            using (CarteluxDB context = new CarteluxDB())
            {
                if (context.trading_ordenes != null)
                {
                    List<trading_ordenes> trading_ordenes_elements = context.trading_ordenes.OrderByDescending(e => e.Datetime).ToList();

                    if (trading_ordenes_elements.Count() > 0)
                    {
                        gridOrdenesPendientes.DataSource = trading_ordenes_elements;
                        gridOrdenesPendientes.DataBind();

                        gridOrdenesPendientes.UseAccessibleHeader = true;
                        gridOrdenesPendientes.HeaderRow.TableSection = TableRowSection.TableHeader;

                        lblGridOrdenesPendientesCount.Text = "# " + trading_ordenes_elements.Count();
                    }
                    else
                    {
                        var obj = new List<trading_ordenes>();
                        trading_ordenes _orden1 = new trading_ordenes();
                        _orden1.Orden_ID = 1;
                        _orden1.Operacion_ID = 1;
                        _orden1.Trade_ID = 1;
                        _orden1.Estado_ID = 1;
                        _orden1.Datetime = DateTime.MinValue;

                        obj.Add(_orden1);

                        // Bind the DataTable which contain a blank row to the GridView
                        gridOrdenesPendientes.DataSource = obj;
                        gridOrdenesPendientes.DataBind();
                        int columnsCount = gridOrdenesPendientes.Columns.Count;
                        gridOrdenesPendientes.Rows[0].Cells.Clear();// clear all the cells in the row
                        gridOrdenesPendientes.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                        gridOrdenesPendientes.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

                        //You can set the styles here
                        gridOrdenesPendientes.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        gridOrdenesPendientes.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                        gridOrdenesPendientes.Rows[0].Cells[0].Font.Bold = true;

                        //set No Results found to the new added cell
                        gridOrdenesPendientes.Rows[0].Cells[0].Text = "No hay registros";
                    }
                }
            } // using

            #endregion
        }

        #endregion

        #region Static Methods

        [WebMethod]
        public static _GridFormularios[] GetData_BindGridFormularios(string year_value, string month_value, bool soloVigentes_value, bool soloEntrCol_value, bool incluirCancelados_value)
        {
            List<_GridFormularios> _gridOrdenes_list = new List<_GridFormularios>();
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

                        int number = 1;
                        List<trading_ordenes> trading_ordenes_elements = new List<trading_ordenes>(); //GetFormularios_PorMes(context, date1, date2, incluirCancelados_value);
                        foreach (trading_ordenes _orden in trading_ordenes_elements)
                        {
                            if (_orden != null)
                            {
                                /*
                                _GridFormularios _GridFormulario1 = new Cartelux1.Dashboard._GridFormularios();
                                _GridFormulario1.Orden_ID = _orden.Orden_ID.ToString();
                                _GridFormulario1.URL_short = _orden.URL_short;
                                _GridFormulario1.EstadoNro = 0;
                                */

                                /*
                                 * Estados de pedidos:
                                 * 0: Vigente
                                 * 1: Concluído
                                 * 2: Cancelado
                                 * 3: Eliminado
                                 * 4: Diseño OK, pronto para imprimir
                                 * */

                                bool es_cancelado = false;


                                /*
                                clientes _cliente = (clientes)context.clientes.FirstOrDefault(c => c.Cliente_ID == _orden.Cliente_ID);
                                pedidos _pedido = (pedidos)context.pedidos.FirstOrDefault(c => c.Orden_ID == _orden.Orden_ID);
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

                                /*
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

                                /*
                                    string tamano_tipo_ID_str = _pedido.Pedido_Tamano_ID.ToString(); // Se usa ID, no código
                                    if (_orden.Fecha_update <= GlobalVariables.GetDatetimeFormated("22-07-2019")) //dd-MM-yyyy
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
                                    _GridFormulario1.lblMonto = _orden.Monto;
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
                                _gridOrdenes_list.Add(_GridFormulario1);

                                */
                            }
                            number++;
                        } // foreach
                    }
                }
            }
            return _gridOrdenes_list.ToArray();
        }

        [WebMethod]
        public static int? GetData_BindgridOrdenes_MesAnterior(string year_value, string month_value)
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

                        // monto_mesAnterior = GetFormularios_PorMes_GetMonto(context, date1, date2);
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
            public string Orden_ID { get; set; }
            public string URL_short { get; set; }
            public string URL_gmaps { get; set; }
            public bool chbTieneBosquejo { get; set; }
            public string lblDisenoReferido { get; set; }
            public int EstadoNro { get; set; }
            public string lblTipoCartelCodigo { get; set; }
        }

        [WebMethod]
        public static bool PedidosUpdateState(int actionID, string formID, string userID)
        {
            bool ret = false;
            /*
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
                        pedidos _pedido = (pedidos)context.pedidos.FirstOrDefault(v => v.Orden_ID.Equals(formID_int));
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

            */
            return ret;
        }
        #endregion
    }
}

