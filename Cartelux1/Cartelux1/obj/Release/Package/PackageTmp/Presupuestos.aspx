<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Presupuestos.aspx.cs" Inherits="Cartelux1.Presupuestos" Title="Presupuestos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- STYLES EXTENSION -->
    <link rel="stylesheet" href="/Content/css/jquery.modal.css" />
    <link rel="stylesheet" href="/Content/css/chosen.css" />
    <link rel="stylesheet" href="/Content/css/MonthPicker.css" />
    <link rel="stylesheet" href="/Content/css/glDatePicker.darkneon.css" />
    <link rel="stylesheet" href="/Content/css/glDatePicker.default.css" />
    <link rel="stylesheet" href="/Content/css/glDatePicker.flatwhite.css" />
    <link rel="stylesheet" href="/Content/css/optionalStyling.css" />
    <link rel="stylesheet" href="/Content/css/bic_calendar.css" />
    <link rel="stylesheet" href="/Content/css/popbox.css" />

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/pages/dashboard.css" />
    <link rel="stylesheet" href="/Content/css/Modal_styles.css" />
    <%--<link rel="stylesheet" href="/Content/css/pages/calculadora.css" />--%>

    <!-- Bootstrap table -->
    <link rel="stylesheet" href="/Content/css/bootstrap-table.css" />

    <!-- STYLES REPORTES TEMPLATE -->

    <style type="text/css">
        .red, .green {
            font-size: x-large !important;
        }

        .font-size {
            font-size: x-large;
            padding-right: 10pt;
            padding-left: 10pt;
        }

        .comparacion {
            font-size: 13px !important;
            color: #1ABB9C;
        }

        .w_right {
            display: flex;
        }

        span {
            color: black !important;
        }

        .ui-dialog-titlebar {
            background-color: rgba(255, 119, 0, 0.78);
        }

        .ui-dialog-title {
            color: whitesmoke;
        }

        .ui-dialog-buttonset > button {
            background-color: rgb(254, 224, 134);
        }

        .ui-selectmenu-button {
            margin-bottom: 10px;
            width: 100% !important;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .filter-table .quick {
            margin-left: 0.5em;
            font-size: 0.8em;
            text-decoration: none;
        }

        .fitler-table .quick:hover {
            text-decoration: underline;
        }

        td.alt {
            background-color: #ffc;
            background-color: rgba(255, 255, 0, 0.2);
        }

        .td-style {
            margin-top: 7px;
        }

        #ddl_year {
            margin: auto;
            width: 50%;
        }

            #ddl_year > .dropdown-menu {
                position: relative !important;
            }

        .row-short {
            margin-left: 0;
            margin-right: 0;
        }

        .headerSortUp {
            background-position: top;
            background-repeat: no-repeat;
            background-image: url(../images/icons/sort_up.gif);
            background-color: #e9e7d7;
        }

        .headerSortDown {
            background-position: top;
            background-repeat: no-repeat;
            background-image: url(../images/icons/sort_down.gif);
            background-color: #e9e7d7;
        }

        .td-short {
            max-width: 120px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .td-very_short {
            max-width: 80px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">

    <!-- PAGE SCRIPTS -->
    <script type="text/javascript" src="/Content/js/jquery.quicksearch.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="/Content/js/chosen.jquery.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.filtertable.min.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.modal.js"></script>
    <script type="text/javascript" src="/Content/js/popbox.js"></script>

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/pages/auxiliar_functions.js"></script>
    <script type="text/javascript" src="/Content/js/pages/presupuestos.js"></script>

    <!-- JS -->

    <%--SOURCE: https://stackoverflow.com/questions/5540395/can-i-open-a-url-in-a-jqueryui-dialog
    SOURCE: http://fancybox.net/--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box box-default">
        <div class="box-header with-border" style="padding-bottom: 0;">

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-9">
                    <h1 style="font-size: 24px;">Presupuestos</h1>
                </div>
            </div>

            <div class="row panel panel-default" style="margin-top: 10px; padding-top: 10px;">

                <div class="col-xs-12 col-sm-12 col-md-10 pull-right">

                    <div id="tabsElementos">
                        <ul>
                            <li><a href="#tabsCalculos" class="tabsBases">Cálculos</a></li>
                            <li><a href="#tabsConfiguracion" class="tabsBases">Configuración</a></li>
                        </ul>
                        <!-- Tab tabsPrecios BEGIN -->
                        <div id="tabsCalculos" style="padding: 6px;">

                            <asp:UpdatePanel ID="upCalculos" runat="server" UpdateMode="Conditional">
                                <%--<Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnElementoAgregar" EventName="Click" />
                                </Triggers>--%>
                                <ContentTemplate>

                                    <h2>Lista de productos</h2>
                                    <asp:Label ID="gridElementos_lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <div id="div_gridElementos" style="overflow: auto;">
                                        <asp:GridView ID="gridElementos" runat="server" ClientIDMode="Static" HorizontalAlign="Center" AutoGenerateColumns="false" CssClass="table table-hover table-striped" AllowPaging="false" AllowSorting="false"
                                            DataKeyNames="Producto_ID"
                                            OnRowDataBound="gridElementos_RowDataBound"
                                            OnRowCommand="gridElementos_RowCommand">

                                            <Columns>
                                                <asp:BoundField DataField="Producto_ID" HeaderText="Elemento_ID" HtmlEncode="false" ItemStyle-CssClass="hiddencol hiddencol_real" HeaderStyle-CssClass="hiddencol hiddencol_real" />

                                                <asp:TemplateField HeaderText="" ControlStyle-CssClass="btn btn-warning btn-xs">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnElementoAgregar" runat="server" CommandName="View" CssClass="btn btn-success btn-xs fa fa-arrow-circle-down fa-x2" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblElementoNumero" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblElementoNombre" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Comentarios">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblElementoComentarios" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="$ Precio sugerido">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblElementoPrecio_sugerido" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:Label ID="lblgridElementosCount" runat="server" ClientIDMode="Static" Text="Resultados: 0" CssClass="lblResultados label label-info"></asp:Label>

                                    <hr />

                                        <h2>Productos seleccionados</h2>
                                    <asp:Label ID="gridElementosSeleccionados_lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <div id="div_gridElementosSeleccionados" style="overflow: auto;">
                                        <asp:GridView ID="gridElementosSeleccionados" runat="server" ClientIDMode="Static" HorizontalAlign="Center" AutoGenerateColumns="false" CssClass="table table-hover table-striped" AllowPaging="false" AllowSorting="false"
                                            DataKeyNames="lblElementoID"
                                            OnRowDataBound="gridElementosSeleccionados_RowDataBound"
                                            OnRowCommand="gridElementosSeleccionados_RowCommand">

                                            <Columns>
                                                <asp:BoundField DataField="lblElementoID" HeaderText="Elemento_ID" HtmlEncode="false" ItemStyle-CssClass="hiddencol hiddencol_real" HeaderStyle-CssClass="hiddencol hiddencol_real" />

                                                <asp:TemplateField HeaderText="" ControlStyle-CssClass="btn btn-warning btn-xs">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnElementoSeleccionadoQuitar" runat="server" CommandName="View" CssClass="btn btn-danger btn-xs fa fa-arrow-circle-up fa-x2" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblElementoSeleccionadoNumero" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblElementoNombre" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Comentarios">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblElementoComentarios" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Volumen CM2 / Fondo color">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txbVolumen_" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="$ Costo unitario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txbCostoUnitario_" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="$ Precio unitario / Sugerido">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txbPrecioUnitario_" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cantidad total">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txbCantidadTotal_" runat="server" CommandName="View" Text="1" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="$ Costo total">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txbCostoTotal_" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="$ Precio subtotal 1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txbPrecioSubtotal1_" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="% Descuento">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txbPrecioDescuento_" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="$ Precio subtotal 2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txbPrecioSubtotal2_" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Redondeo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txbPrecioRedondeo_" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="$ PRECIO FINAL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txbPrecioFinal_" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                        <button type="button" class="btn btn-info btn-xs" onclick="gridElementosSeleccionados_limpiarLista()">Limpiar lista</button>
                                        <button type="button" class="btn btn-success btn-xs" onclick="gridElementosSeleccionados_confirmarLista()">Confirmar lista</button>
                                    <asp:Label ID="Label2" runat="server" ClientIDMode="Static" Text="Resultados: 0" CssClass="lblResultados label label-info"></asp:Label>

                                </ContentTemplate>
                            </asp:UpdatePanel>


                        </div>







                    </div>
                </div>



            </div>

        </div>

        <div id="divPopbox_calcularVolumen" class='popbox-box popbox' style="width: 300px;">
            <div class='arrow' style="left: 250px;"></div>
            <div class='arrow-border' style="left: 250px;"></div>

            <div class="form-group" style="margin-bottom: 0;">
                <div class="row row-short" style="padding: 10px;">

                    <div class="alert alert-warning" role="alert">
                        <span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>
                        <span class="unselectable">Calcular volumen CM2</span>
                    </div>
                    <%--<input id="lbl_options_password" type="password" class="form-control" placeholder="Contraseña" name="login-username" required="required" />--%>

                    <div class="row row-short" style="display: inline-flex;">
                        <input type="number" id="input_divPopbox_calcularVolumen_Ancho" placeholder="Ancho CM." class="form-control" />
                        <label style="margin: 10px;">x </label>
                        <input type="number" id="input_divPopbox_calcularVolumen_Alto" placeholder="Ancho CM." class="form-control" />
                        <a id='input_divPopbox_calcularVolumen_OK' role='button' href='#' class='btnAcciones btn btn-primary fa fa-wpforms fa-2x' title='Confirmar'></a>
                    </div>

                </div>

            </div>
            <button type="button" class="btn close" style="padding: 5px;">
                <span class="fa fa-times-circle" aria-hidden="true"></span>
            </button>
        </div>

    </div>
</asp:Content>
