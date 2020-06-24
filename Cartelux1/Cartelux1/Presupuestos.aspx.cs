using Cartelux1.Global_Objects;
using Cartelux1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cartelux1
{
    public partial class Presupuestos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_GridElementos();
                Bind_GridElementosSeleccionados();
            }

            gridElementos.UseAccessibleHeader = true;
            gridElementos.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridElementos.FooterRow.TableSection = TableRowSection.TableFooter;

            gridElementosSeleccionados.UseAccessibleHeader = true;
            gridElementosSeleccionados.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridElementosSeleccionados.FooterRow.TableSection = TableRowSection.TableFooter;
        }

        #region Events

        private void Bind_GridElementos()
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            int current_year = DateTime.Now.Year;
            int current_month = DateTime.Now.Month;

            using (CarteluxDB context = new CarteluxDB())
            {
                List<lista_productos> elementos_elements = context.lista_productos.ToList();
                if (elementos_elements.Count() > 0)
                {
                    gridElementos.DataSource = elementos_elements;
                    gridElementos.DataBind();

                    gridElementos.UseAccessibleHeader = true;
                    gridElementos.HeaderRow.TableSection = TableRowSection.TableHeader;

                    lblgridElementosCount.Text = "# " + elementos_elements.Count();
                }
                else
                {
                    var obj = new List<lista_productos>();
                    lista_productos _lista_productos = new lista_productos();
                    _lista_productos.Producto_ID = 0;
                    _lista_productos.Precio_sugerido = 0;

                    obj.Add(_lista_productos);

                    // Bind the DataTable which contain a blank row to the GridView
                    gridElementos.DataSource = obj;
                    gridElementos.DataBind();
                    int columnsCount = gridElementos.Columns.Count;
                    gridElementos.Rows[0].Cells.Clear();// clear all the cells in the row
                    gridElementos.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                    gridElementos.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

                    //You can set the styles here
                    gridElementos.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    gridElementos.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                    gridElementos.Rows[0].Cells[0].Font.Bold = true;

                    //set No Results found to the new added cell
                    gridElementos.Rows[0].Cells[0].Text = "No hay registros";
                }

            }
        }

        private void Bind_GridElementosSeleccionados()
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            int current_year = DateTime.Now.Year;
            int current_month = DateTime.Now.Month;

            using (CarteluxDB context = new CarteluxDB())
            {
                var obj = new List<_GridElementosSeleccionados>();
                _GridElementosSeleccionados _GridElementosSeleccionados1 = new _GridElementosSeleccionados();
                _GridElementosSeleccionados1.lblElementoID = 1;
                _GridElementosSeleccionados1.lblElementoSeleccionadoNumero = 1;
                _GridElementosSeleccionados1.lblElementoNombre = string.Empty;
                _GridElementosSeleccionados1.lblElementoComentarios = string.Empty;
                _GridElementosSeleccionados1.txbVolumen_ = 0;
                _GridElementosSeleccionados1.chkFondoColor_ = false;
                _GridElementosSeleccionados1.txbCostoUnitario_ = 0;
                _GridElementosSeleccionados1.txbPrecioUnitario_ = 0;
                _GridElementosSeleccionados1.chkPrecioSugerido_ = false;
                _GridElementosSeleccionados1.txbCantidadTotal_ = 0;
                _GridElementosSeleccionados1.txbCostoTotal_ = 0;
                _GridElementosSeleccionados1.txbPrecioSubtotal1_ = 0;
                _GridElementosSeleccionados1.txbPrecioDescuento_ = 0;
                _GridElementosSeleccionados1.txbPrecioSubtotal2_ = 0;
                _GridElementosSeleccionados1.txbPrecioRedondeo_ = 0;
                _GridElementosSeleccionados1.txbPrecioFinal_ = 0;

                obj.Add(_GridElementosSeleccionados1);

                // Bind the DataTable which contain a blank row to the GridView
                gridElementosSeleccionados.DataSource = obj;
                gridElementosSeleccionados.DataBind();
                int columnsCount = gridElementosSeleccionados.Columns.Count;
                gridElementosSeleccionados.Rows[0].Cells.Clear();// clear all the cells in the row
                gridElementosSeleccionados.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                gridElementosSeleccionados.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

                //You can set the styles here
                gridElementosSeleccionados.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                gridElementosSeleccionados.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                gridElementosSeleccionados.Rows[0].Cells[0].Font.Bold = true;

                //set No Results found to the new added cell
                gridElementosSeleccionados.Rows[0].Cells[0].Text = "No hay registros";
                gridElementosSeleccionados.Rows[0].ID = "gridElementosSeleccionados_blank";

            }
        }

        protected void gridElementos_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    lista_productos _lista_productos = (lista_productos)(e.Row.DataItem);
                    if (_lista_productos != null)
                    {
                        Label lbl1 = e.Row.FindControl("lblElementoNumero") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = e.Row.RowIndex.ToString();
                        }

                        lbl1 = e.Row.FindControl("lblElementoNombre") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _lista_productos.Nombre;
                        }

                        lbl1 = e.Row.FindControl("lblElementoComentarios") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _lista_productos.Comentarios;
                        }

                        lbl1 = e.Row.FindControl("lblElementoPrecio_sugerido") as Label;
                        if (lbl1 != null)
                        {
                            lbl1.Text = _lista_productos.Precio_sugerido.ToString();
                        }

                        LinkButton btn = e.Row.FindControl("btnElementoAgregar") as LinkButton;
                        if (btn != null)
                        {

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
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridElementos, "Select$" + e.Row.RowIndex);
                }
            }
        }

        protected void gridElementosSeleccionados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*
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
                    //lista_productos _lista_productos = (lista_productos)(e.Row.DataItem);
                    //if (_lista_productos != null)
                    //{
                    //    Label lbl1 = e.Row.FindControl("lblElementoSeleccionadoNumero") as Label;
                    //    if (lbl1 != null)
                    //    {
                    //        lbl1.Text = e.Row.RowIndex.ToString();
                    //    }

                    //}
                }

                #endregion Labels

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridElementosSeleccionados, "Select$" + e.Row.RowIndex);
                }
            }
            */
        }


        protected void gridElementos_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gridElementosSeleccionados_RowCommand(object sender, GridViewCommandEventArgs e)
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


        #endregion

        #region Static Methods

        [WebMethod]
        public static _GridElementos[] GetData_BindGridElementos()
        {
            List<_GridElementos> _GridElementos_list = new List<_GridElementos>();

            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            using (CarteluxDB context = new CarteluxDB())
            {
                int number = 1;
                List<lista_productos> elementos_elements = context.lista_productos.ToList();
                foreach (lista_productos _lista_productos in elementos_elements)
                {
                    if (_lista_productos != null)
                    {
                        _GridElementos _GridElementos1 = new Cartelux1.Presupuestos._GridElementos();
                        _GridElementos1.lblElementoID = _lista_productos.Producto_ID;
                        _GridElementos1.lblElementoNumero = number;
                        _GridElementos1.lblElementoNombre = _lista_productos.Nombre;
                        _GridElementos1.lblElementoPrecioSugerido = _lista_productos.Precio_sugerido;
                        _GridElementos1.lblElementoComentarios = _lista_productos.Comentarios;

                        _GridElementos_list.Add(_GridElementos1);
                    }
                    number++;
                } //foreach
            }
            return _GridElementos_list.ToArray();
        }

        [WebMethod]
        public static _GridElementosSeleccionados_calcular_Class GridElementosSeleccionados_calcular(string lblElementoID, string volumen, string cantidad_pedido, string isFondoColor)
        {
            _GridElementosSeleccionados_calcular_Class return_values = new _GridElementosSeleccionados_calcular_Class();

            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            int lblElementoID_int = 0;
            if (!int.TryParse(lblElementoID, out lblElementoID_int))
            {
                lblElementoID_int = 0;
                Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, lblElementoID);
            }

            decimal volumen_decimal = 0;
            if (!decimal.TryParse(volumen, out volumen_decimal))
            {
                volumen_decimal = 0;
                Logs.AddErrorLog("Excepcion. Convirtiendo decimal. ERROR:", className, methodName, volumen);
            }

            decimal cantidad_pedido_int = 0;
            if (!decimal.TryParse(cantidad_pedido, out cantidad_pedido_int))
            {
                cantidad_pedido_int = 0;
                Logs.AddErrorLog("Excepcion. Convirtiendo int. ERROR:", className, methodName, cantidad_pedido);
            }

            using (CarteluxDB context = new CarteluxDB())
            {
                lista_productos _lista_productos = (lista_productos)context.lista_productos.FirstOrDefault(v => v.Producto_ID == lblElementoID_int);
                if (_lista_productos != null)
                {
                    decimal? total_costo = 0;
                    decimal? total_precio = 0;

                    // COMPONENTES UNIDAD
                    List<producto_componente_unidad> _producto_componente_unidad = context.producto_componente_unidad.Where(v => v.Producto_ID == lblElementoID_int).ToList();
                    foreach (producto_componente_unidad _producto_componente_unidad1 in _producto_componente_unidad)
                    {
                        int? cantidad = _producto_componente_unidad1.Cantidad;
                        decimal? costo_unitario = 0;
                        decimal? precio_unitario = 0;

                        lista_componentes_unidad _lista_componentes_unidad = (lista_componentes_unidad)context.lista_componentes_unidad.FirstOrDefault(v => v.componente_unidad_ID == _producto_componente_unidad1.Componente_unidad_ID);
                        if (_lista_componentes_unidad != null)
                        {
                            costo_unitario = _lista_componentes_unidad.Costo_unidad;
                            precio_unitario = _lista_componentes_unidad.Precio_venta;
                        }

                        total_costo += costo_unitario * cantidad;
                        total_precio += precio_unitario * cantidad;
                    }

                    // COMPONENTES VOLUMEN
                    List<producto_componente_volumen> _producto_componente_volumen = context.producto_componente_volumen.Where(v => v.Producto_ID == lblElementoID_int).ToList();
                    foreach (producto_componente_volumen _producto_componente_volumen1 in _producto_componente_volumen)
                    {
                        decimal? costo_unitario = 0;
                        decimal? precio_unitario = 0;

                        lista_componentes_volumen _lista_componentes_volumen = (lista_componentes_volumen)context.lista_componentes_volumen.FirstOrDefault(v => v.componente_volumen_ID == _producto_componente_volumen1.producto_componente_volumen_ID);
                        if (_lista_componentes_volumen != null)
                        {
                            costo_unitario = _lista_componentes_volumen.Costo_volumen;
                            precio_unitario = _lista_componentes_volumen.Precio_venta;
                        }

                        total_costo += costo_unitario * volumen_decimal;
                        total_precio += precio_unitario * volumen_decimal;
                    }

                    // SI FONDO COLOR
                    bool isFondoColor_bool = false;
                    if (!Boolean.TryParse(isFondoColor, out isFondoColor_bool))
                    {
                        isFondoColor_bool = false;
                        Logs.AddErrorLog("Excepcion. Convirtiendo bool. ERROR:", className, methodName, isFondoColor);
                    }
                    if (isFondoColor_bool)
                    {
                        lista_componentes_volumen _lista_componentes_volumen = (lista_componentes_volumen)context.lista_componentes_volumen.FirstOrDefault(v => v.isFondoColor == true);
                        if (_lista_componentes_volumen != null)
                        {
                            total_precio += _lista_componentes_volumen.Precio_venta * volumen_decimal;
                        }
                    }

                    return_values.costo_unitario = total_costo;
                    return_values.precio_unitario = total_precio;
                    return_values.costo_total = total_costo * cantidad_pedido_int;
                    return_values.precio_total = total_precio * cantidad_pedido_int;
                }
            }
            return return_values;
        }

        [WebMethod]
        public static void gridElementosSeleccionados_confirmarLista(string[] _LISTA_elementosGuardar)
        //public static void gridElementosSeleccionados_confirmarLista(_LISTA_elementosGuardar_objeto[] _LISTA_elementosGuardar)
        //public static void gridElementosSeleccionados_confirmarLista(object[] _LISTA_elementosGuardar)
        //public static void gridElementosSeleccionados_confirmarLista(_LISTA_elementosGuardar_objeto[] _LISTA_elementosGuardar)
        {
            if (_LISTA_elementosGuardar != null)
            {
                // Logger variables
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
                System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
                string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                string methodName = stackFrame.GetMethod().Name;

                using (CarteluxDB context = new CarteluxDB())
                {
                    /*
                    presupuestos_generados presupuestos_generados1 = new presupuestos_generados();

                    int presupuesto_id = 1;
                    presupuestos_generados presupuestos_generados_checkID = (presupuestos_generados)context.presupuestos_generados.OrderByDescending(p => p.presupuesto_generado_ID).FirstOrDefault();
                    if (presupuestos_generados_checkID != null)
                    {
                        presupuesto_id = presupuestos_generados_checkID.presupuesto_generado_ID;
                    }

                    presupuestos_generados1.presupuesto_generado_ID = presupuesto_id;
                    context.presupuestos_generados.Add(presupuestos_generados1);

                    /*
                    string IP_client = Logs.GetIPAddress();
                    string UserID = "-";
                    string UserName = "-";
                    if (Session["UserID"] != null && !string.IsNullOrWhiteSpace(Session["UserID"].ToString()) && Session["UserName"] != null && !string.IsNullOrWhiteSpace(Session["UserName"].ToString()))
                    {
                        UserID = Session["UserID"].ToString();
                        UserName = Session["UserName"].ToString();
                    }
                    Logs.AddUserLog("OK: Genera nuevo presupuesto: '", "", UserID, UserName, IP_client);

                    foreach (_LISTA_elementosGuardar_objeto _lista_producto1 in _LISTA_elementosGuardar)
                    {
                        presupuestos_generados_productos producto1 = new presupuestos_generados_productos();
                        producto1.Producto_ID = _lista_producto1.lblElementoID;
                        producto1.Dato_fijo_txbNombre = _lista_producto1.input_lblElementoNombre_;
                        producto1.Dato_fijo_txbVolumen = _lista_producto1.txbVolumen_;
                        producto1.Dato_fijo_chkFondoColor = _lista_producto1.chkFondoColor_;
                        producto1.Dato_fijo_txbCostoUnitario = _lista_producto1.txbCostoUnitario_;
                        producto1.Dato_fijo_txbPrecioUnitario = _lista_producto1.txbPrecioUnitario_;
                        producto1.Dato_fijo_chkSugerido = _lista_producto1.chkPrecioSugerido_;
                        producto1.Dato_fijo_txbCantidadTotal = _lista_producto1.txbCantidadTotal_;
                        producto1.Dato_fijo_txbCostoTotal = _lista_producto1.txbCostoTotal_;
                        producto1.Dato_fijo_txbSubotal1 = _lista_producto1.txbPrecioSubtotal1_;
                        producto1.Dato_fijo_txbDescuento = _lista_producto1.txbDescuento_;
                        producto1.Dato_fijo_txbSubotal2 = _lista_producto1.txbPrecioSubtotal2_;
                        producto1.Dato_fijo_txbRedondeo = _lista_producto1.txbRedondeo_;
                        producto1.Dato_fijo_txbPrecioFinal = _lista_producto1.txbPrecioFinal_;
                        producto1.Dato_fijo_txbComentarios = _lista_producto1.input_lblComentarios_;

                        producto1.presupuesto_generado_ID = presupuesto_id;

                        context.presupuestos_generados_productos.Add(producto1);
                        //Guardar_Contexto(context);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Logs.AddErrorLog("Excepcion. Guardando en la base de datos. ERROR:", className, methodName, e.Message);
                        }
                    }
                        */

                }

            }
        }

        #endregion

        public class _GridElementosSeleccionados_calcular_Class
        {
            public decimal? costo_unitario { get; set; }
            public decimal? precio_unitario { get; set; }
            public decimal? costo_total { get; set; }
            public decimal? precio_total { get; set; }
        }

        public class _GridElementos
        {
            public int lblElementoID { get; set; }
            public int lblElementoNumero { get; set; }
            public string lblElementoNombre { get; set; }
            public decimal? lblElementoPrecioSugerido { get; set; }
            public string lblElementoComentarios { get; set; }
        }

        public class _GridElementosSeleccionados
        {
            public int lblElementoID { get; set; }
            public int lblElementoSeleccionadoNumero { get; set; }
            public string lblElementoNombre { get; set; }
            public string lblElementoComentarios { get; set; }
            public decimal txbVolumen_ { get; set; }
            public bool chkFondoColor_ { get; set; }
            public decimal txbCostoUnitario_ { get; set; }
            public decimal txbPrecioUnitario_ { get; set; }
            public bool chkPrecioSugerido_ { get; set; }
            public int txbCantidadTotal_ { get; set; }
            public decimal txbCostoTotal_ { get; set; }
            public decimal txbPrecioSubtotal1_ { get; set; }
            public int txbPrecioDescuento_ { get; set; }
            public decimal txbPrecioSubtotal2_ { get; set; }
            public decimal txbPrecioRedondeo_ { get; set; }
            public decimal txbPrecioFinal_ { get; set; }

        }

        public class _LISTA_elementosGuardar_objeto
        {
            public int lblElementoID { get; set; }
            public string input_lblElementoNombre_ { get; set; }
            public string input_lblComentarios_ { get; set; }
            public decimal txbVolumen_ { get; set; }
            public bool chkFondoColor_ { get; set; }
            public decimal txbCostoUnitario_ { get; set; }
            public decimal txbPrecioUnitario_ { get; set; }
            public bool chkPrecioSugerido_ { get; set; }
            public int txbCantidadTotal_ { get; set; }
            public decimal txbCostoTotal_ { get; set; }
            public decimal txbPrecioSubtotal1_ { get; set; }
            public int txbDescuento_ { get; set; }
            public decimal txbPrecioSubtotal2_ { get; set; }
            public decimal txbRedondeo_ { get; set; }
            public decimal txbPrecioFinal_ { get; set; }

        }

        public class _LISTA_elementosGuardar_objeto2
        {
            public string Column1 { get; set; }
        }

    }


}