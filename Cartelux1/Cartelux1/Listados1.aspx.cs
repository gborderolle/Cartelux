using Cartelux1.Helpers;
using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Cartelux1
{
    public partial class Listados1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                //BindGrid1();
                //LoadGrid1();
            }
            lblMessage.Text = "";
            gridPedidos.UseAccessibleHeader = true;
            gridPedidos.HeaderRow.TableSection = TableRowSection.TableHeader;
            //this.LoadCompleted();
        }

        private void BindGrid1()
        {
            using (CarteluxDB context = new CarteluxDB())
            {
                hdnPedidosCount.Value = context.pedidos.Count().ToString();
                if (context.pedidos.Count() > 0)
                {
                    gridPedidos.DataSource = context.pedidos.ToList();
                    gridPedidos.DataBind();
                }
                else
                {
                    var obj = new List<pedidos>();
                    obj.Add(new pedidos());
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
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "updateCounts", "updateCounts();", true);
            }
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
                if (Request.QueryString["dato"] != null)
                {
                    string form_ID_str = Request.QueryString["dato"];

                    int formulario_ID = 0;
                    if (!int.TryParse(form_ID_str, out formulario_ID))
                    {
                        formulario_ID = 0;
                        //Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, form_ID_str);
                    }

                    if (formulario_ID > 0)
                    {
                        formularios _formulario = (formularios)context.formularios.FirstOrDefault(c => c.Formulario_ID == formulario_ID);
                        if (_formulario != null)
                        {
                            int cliente_ID = _formulario.Cliente_ID;
                            if (cliente_ID > 0)
                            {
                                List<pedidos> lista_pedidos1 = new List<pedidos>();

                                List<formularios> lista_formularios = (List<formularios>)context.formularios.Where(v => v.Cliente_ID == cliente_ID).ToList();
                                if (lista_formularios != null && lista_formularios.Count > 0)
                                {
                                    foreach (formularios _formulario1 in lista_formularios)
                                    {
                                        // Obtengo todos CADA pedido de todos los formularios de dicho cliente
                                        lista_pedidos1.Add((pedidos)context.pedidos.FirstOrDefault(v => v.Formulario_ID == _formulario1.Formulario_ID));
                                    }
                                    LoadGrid(lista_pedidos1);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadGrid(List<pedidos> lista_pedidos)
        {
            using (CarteluxDB context = new CarteluxDB())
            {
                hdnPedidosCount.Value = context.pedidos.Count().ToString();
                if (lista_pedidos != null && lista_pedidos.Count > 0)
                {
                    gridPedidos.DataSource = context.pedidos.ToList();
                    gridPedidos.DataBind();
                    //DataTable table = Extras.ConvertListToDataTable(lista_pedidos.ToList());
                    //DataTable table = Extras.ToDataTable(lista_pedidos.ToList());
                    //gridPedidos.DataSource = table;

                    //gridPedidos.DataSource = context.pedidos.ToList();
                    //gridPedidos.DataBind();
                }
                else
                {
                    var obj = new List<pedidos>();
                    obj.Add(new pedidos());
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
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "updateCounts", "updateCounts();", true);
            }
        }

        private void LoadGrid1()
        {
            using (CarteluxDB context = new CarteluxDB())
            {
                hdnPedidosCount.Value = context.pedidos.Count().ToString();
                if (1 > 0)
                {
                    gridPedidos.DataSource = context.pedidos.ToList();
                    gridPedidos.DataBind();
                    //DataTable table = Extras.ConvertListToDataTable(lista_pedidos.ToList());
                    //DataTable table = Extras.ToDataTable(lista_pedidos.ToList());
                    //gridPedidos.DataSource = table;

                    //gridPedidos.DataSource = context.pedidos.ToList();
                    //gridPedidos.DataBind();
                }
                else
                {
                    var obj = new List<pedidos>();
                    obj.Add(new pedidos());
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
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "updateCounts", "updateCounts();", true);
            }
        }

        protected void gridPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void gridPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        //protected void gridClientes_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //}

        //protected void gridClientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //}

        //protected void gridClientes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //}

        //protected void gridClientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

    }
}