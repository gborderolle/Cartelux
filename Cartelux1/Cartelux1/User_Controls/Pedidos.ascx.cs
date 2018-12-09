using Cartelux1.Global_Objects;
using Cartelux1.Helpers;
using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cartelux1.User_Controls
{
    public partial class Pedidos : System.Web.UI.UserControl
    {
        public event Action LoadCompleted = delegate { };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
            lblMessage.Text = "";
            gridPedidos.UseAccessibleHeader = true;
            gridPedidos.HeaderRow.TableSection = TableRowSection.TableHeader;
            this.LoadCompleted();
        }

        private void BindGrid()
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            using (CarteluxDB context = new CarteluxDB())
            {
                if (Request.QueryString["tabla"] != null && Request.QueryString["dato"] != null)
                {
                    string tabla_str = Request.QueryString["tabla"];
                    string form_ID_str = Request.QueryString["dato"];

                    if (!string.IsNullOrWhiteSpace(tabla_str) && tabla_str.Equals("pedidos"))
                    {
                        int formulario_ID = 0;
                        if (!int.TryParse(form_ID_str, out formulario_ID))
                        {
                            formulario_ID = 0;
                            Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, form_ID_str);
                        }

                        if (formulario_ID > 0)
                        {
                            formularios _formulario = (formularios)context.formularios.FirstOrDefault(c => c.Formulario_ID == formulario_ID);
                            if (_formulario != null)
                            {
                                int cliente_ID = _formulario.Cliente_ID;
                                if (cliente_ID > 0)
                                {
                                    // Cliente
                                    clientes _cliente = (clientes)context.clientes.FirstOrDefault(c => c.Cliente_ID == cliente_ID);
                                    if (_cliente != null)
                                    {
                                        lblName.Text = _cliente.Nombre;
                                        lblTel.Text = _cliente.Telefono;
                                    }

                                    List<pedidos> lista_pedidos1 = new List<pedidos>();

                                    List<formularios> lista_formularios = (List<formularios>)context.formularios.Where(v => v.Cliente_ID == cliente_ID).ToList();
                                    if (lista_formularios != null && lista_formularios.Count > 0)
                                    {
                                        foreach (formularios _formulario1 in lista_formularios)
                                        {
                                            // Obtengo todos CADA pedido de todos los formularios de dicho cliente
                                            lista_pedidos1.Add((pedidos)context.pedidos.FirstOrDefault(v => v.Formulario_ID == _formulario1.Formulario_ID));
                                        }

                                        int contador = 1;
                                        List<_GridPedidosAdaptado> lista_pedidos_adaptada = new List<_GridPedidosAdaptado>();
                                        foreach (pedidos _pedido in lista_pedidos1)
                                        {
                                            _GridPedidosAdaptado pedido_adaptado = new _GridPedidosAdaptado();

                                            pedido_adaptado.lblNumero = contador.ToString();
                                            pedido_adaptado.lblTematica = _formulario.Comentarios;

                                            // Fecha entrega
                                            pedido_entregas _pedido_entregas = (pedido_entregas)context.pedido_entregas.FirstOrDefault(c => c.Pedido_Entrega_ID == _pedido.Pedido_Entrega_ID);
                                            if (_pedido_entregas != null)
                                            {
                                                pedido_adaptado.lblFechaEntregaReal = _pedido_entregas.Fecha_entrega;
                                                pedido_adaptado.lblFechaEntrega = _pedido_entregas.Fecha_entrega?.ToString(GlobalVariables.ShortDateTime_format1);

                                                // Tipo entrega
                                                lista_entregas_tipos _lista_entregas_tipos = (lista_entregas_tipos)context.lista_entregas_tipos.FirstOrDefault(c => c.Entrega_Tipo_ID == _pedido_entregas.Entrega_Tipo_ID);
                                                if (_lista_entregas_tipos != null)
                                                {
                                                    pedido_adaptado.lblTipoEntrega = _lista_entregas_tipos.Nombre;
                                                }
                                            }

                                            // Tipo pedido
                                            lista_pedido_tipos _lista_pedido_tipos = (lista_pedido_tipos)context.lista_pedido_tipos.FirstOrDefault(c => c.Pedido_Tipo_ID == _pedido.Pedido_Tipo_ID);
                                            if (_lista_pedido_tipos != null)
                                            {
                                                pedido_adaptado.lblTipoPedido = _lista_pedido_tipos.Nombre;
                                            }

                                            // Temática
                                            lista_pedido_tematicas _lista_pedido_tematicas = (lista_pedido_tematicas)context.lista_pedido_tematicas.FirstOrDefault(c => c.Pedido_Tematica_ID == _pedido.Pedido_Tematica_ID);
                                            if (_lista_pedido_tematicas != null)
                                            {
                                                pedido_adaptado.lblTematica = _lista_pedido_tematicas.Nombre;
                                            }

                                            // Tamaño
                                            lista_pedido_tamanos _lista_pedido_tamanos = (lista_pedido_tamanos)context.lista_pedido_tamanos.FirstOrDefault(c => c.Pedido_Tamano_ID == _pedido.Pedido_Tamano_ID);
                                            if (_lista_pedido_tamanos != null)
                                            {
                                                pedido_adaptado.lblTamano = _lista_pedido_tamanos.Nombre;
                                            }

                                            lista_pedidos_adaptada.Add(pedido_adaptado);
                                            contador++;
                                        } // foreach

                                        //LoadGrid(lista_pedidos1);
                                        LoadGrid(lista_pedidos_adaptada);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Empty_Grid();
                    }
                }
            }
        }

        private void LoadGrid(List<_GridPedidosAdaptado> lista_pedidos_adaptada)
        {
            using (CarteluxDB context = new CarteluxDB())
            {
                hdnPedidosCount.Value = context.pedidos.Count().ToString();
                if (lista_pedidos_adaptada != null && lista_pedidos_adaptada.Count > 0)
                {
                    //gridPedidos.DataSource = context.pedidos.ToList();
                    //gridPedidos.DataBind();
                    //DataTable table = Extras.ConvertListToDataTable(lista_pedidos.ToList());
                    //DataTable table = Extras.ToDataTable(lista_pedidos.ToList());
                    //gridPedidos.DataSource = table;

                    gridPedidos.DataSource = lista_pedidos_adaptada.OrderByDescending(e => e.lblFechaEntregaReal);
                    gridPedidos.DataBind();
                    //gridPedidos.DataSource = context.pedidos.ToList();
                    //gridPedidos.DataBind();
                }
                else
                {
                    Empty_Grid();
                }
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "updateCounts", "updateCounts();", true);
            }
        }

        private void Empty_Grid()
        {
            var obj = new List<_GridPedidosAdaptado>();
            obj.Add(new _GridPedidosAdaptado());
            // Bind the DataTable which contain a blank row to the GridView
            gridPedidos.DataSource = obj;
            gridPedidos.DataBind();
            int columnsCount = gridPedidos.Columns.Count;
            gridPedidos.Rows[0].Cells.Clear();// clear all the cells in the row
            gridPedidos.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            gridPedidos.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

            //You can set the styles here
            gridPedidos.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            gridPedidos.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            gridPedidos.Rows[0].Cells[0].Font.Bold = true;
            //set No Results found to the new added cell
            gridPedidos.Rows[0].Cells[0].Text = "No hay registros";
        }

        protected void gridPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region Action buttons

            ScriptManager ScriptManager1 = ScriptManager.GetCurrent(this.Page);
            if (ScriptManager1 != null)
            {
            }

            #endregion
        }

        protected void gridPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        //protected void gridPedidos_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    //gridPedidos.EditIndex = e.NewEditIndex;
        //    //BindGrid();
        //}

        //protected void gridPedidos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    //gridPedidos.EditIndex = -1;
        //    //BindGrid();
        //}

        //protected void gridPedidos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //}

        //protected void gridPedidos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //}

        protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recupera la fila.
            GridViewRow pagerRow = gridPedidos.BottomPagerRow;
            // Recupera el control DropDownList...
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            //// Se Establece la propiedad PageIndex para visualizar la página seleccionada...
            gridPedidos.PageIndex = pageList.SelectedIndex;
            //Quita el mensaje de información si lo hubiera...
            lblMessage.Text = "";
        }

        public class _GridPedidosAdaptado
        {
            public string lblNumero { get; set; }
            public Nullable<System.DateTime> lblFechaEntregaReal { get; set; }
            public string lblFechaEntrega { get; set; }
            public string lblTipoPedido { get; set; }
            public string lblTematica { get; set; }
            public string lblTamano { get; set; }
            public string lblTipoEntrega { get; set; }
            public string lblComentarios { get; set; }
        }
    }
}