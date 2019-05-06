using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cartelux1.User_Controls
{
    public partial class Clientes : System.Web.UI.UserControl
    {
        public event Action LoadCompleted = delegate { };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
            lblMessage.Text = "";
            gridClientes1.UseAccessibleHeader = true;
            gridClientes1.HeaderRow.TableSection = TableRowSection.TableHeader;
            this.LoadCompleted();
        }

        private void BindGrid()
        {
            using (CarteluxDB context = new CarteluxDB())
            {
                hdnClientesCount.Value = context.clientes.Count().ToString();
                if (context.clientes.Count() > 0)
                {
                    gridClientes1.DataSource = context.clientes.ToList();
                    gridClientes1.DataBind();
                }
                else
                {
                    var obj = new List<clientes>();
                    obj.Add(new clientes());
                    // Bind the DataTable which contain a blank row to the GridView
                    gridClientes1.DataSource = obj;
                    gridClientes1.DataBind();
                    int columnsCount = gridClientes1.Columns.Count;
                    gridClientes1.Rows[0].Cells.Clear();// clear all the cells in the row
                    gridClientes1.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                    gridClientes1.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

                    //You can set the styles here
                    gridClientes1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    gridClientes1.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                    gridClientes1.Rows[0].Cells[0].Font.Bold = true;
                    //set No Results found to the new added cell
                    gridClientes1.Rows[0].Cells[0].Text = "No hay registros";
                }
                ScriptManager.RegisterStartupScript(this, typeof(Page), "updateCounts", "updateCounts();", true);
            }
        }

        private void Empty_Grid()
        {
            var obj = new List<clientes>();
            obj.Add(new clientes());
            // Bind the DataTable which contain a blank row to the GridView
            gridClientes1.DataSource = obj;
            gridClientes1.DataBind();
            int columnsCount = gridClientes1.Columns.Count;
            gridClientes1.Rows[0].Cells.Clear();// clear all the cells in the row
            gridClientes1.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            gridClientes1.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

            //You can set the styles here
            gridClientes1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            gridClientes1.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            gridClientes1.Rows[0].Cells[0].Font.Bold = true;
            //set No Results found to the new added cell
            gridClientes1.Rows[0].Cells[0].Text = "No hay registros";
        }

        protected void gridClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void gridClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            if (e.CommandName == "InsertNew")
            {
                
            }
            else if (e.CommandName == "View")
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
            else
            {
                //BindGrid();
            }
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
            GridViewRow pagerRow = gridClientes1.BottomPagerRow;
            // Recupera el control DropDownList...
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            //// Se Establece la propiedad PageIndex para visualizar la página seleccionada...
            gridClientes1.PageIndex = pageList.SelectedIndex;
            //Quita el mensaje de información si lo hubiera...
            lblMessage.Text = "";
        }

    }
}