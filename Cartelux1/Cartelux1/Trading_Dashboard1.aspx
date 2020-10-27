<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Trading_Dashboard1.aspx.cs" Inherits="Cartelux1.Trading_Dashboard1" Title="Trading Dashboard" %>

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

    <!-- Bootstrap table -->
    <link rel="stylesheet" href="/Content/css/bootstrap-table.css" />

    <!-- STYLES REPORTES TEMPLATE -->

    <!-- NProgress -->
    <link href="/Content/reportes_template/nprogress/nprogress.css" rel="stylesheet" />
    <!-- iCheck -->
    <link href="/Content/reportes_template/iCheck/flat/green.css" rel="stylesheet" />
    <!-- bootstrap-progressbar -->
    <link href="/Content/reportes_template/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet" />
    <!-- Custom Theme Style -->
    <link href="/Content/reportes_template/custom.css" rel="stylesheet" />

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

        .center_panel {
            padding-left: 10%;
            padding-right: 10%;
            width: 100%;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">

    <!-- PAGE SCRIPTS -->
    <script type="text/javascript" src="/Content/js/jquery.quicksearch.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.modal.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="/Content/js/chosen.jquery.js"></script>
    <script type="text/javascript" src="/Content/js/moment.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.filtertable.min.js"></script>
    <script type="text/javascript" src="/Content/js/glDatePicker.min.js"></script>
    <script type="text/javascript" src="/Content/js/bic_calendar.js"></script>
    <script type="text/javascript" src="/Content/js/popbox.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.tablesorter.js"></script>

    <!-- Bootstrap table -->
    <script type="text/javascript" src="/Content/js/bootstrap-table.js"></script>
    <script type="text/javascript" src="/Content/js/bootstrap-table-es-ES.js"></script>

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/pages/auxiliar_functions.js"></script>
    <script type="text/javascript" src="/Content/js/pages/dashboard.js"></script>

    <!-- STYLES REPORTES TEMPLATE -->

    <!-- FastClick -->
    <script src="/Content/reportes_template/fastclick/lib/fastclick.js"></script>
    <!-- NProgress -->
    <script src="/Content/reportes_template/nprogress/nprogress.js"></script>
    <!-- Chart.js -->
    <script src="/Content/reportes_template/Chart.js/dist/Chart.min.js"></script>
    <!-- gauge.js -->
    <script src="/Content/reportes_template/gauge.js/dist/gauge.min.js"></script>
    <!-- bootstrap-progressbar -->
    <script src="/Content/reportes_template/bootstrap-progressbar/bootstrap-progressbar.min.js"></script>
    <!-- iCheck -->
    <script src="/Content/reportes_template/iCheck/icheck.min.js"></script>
    <!-- Skycons -->
    <script src="/Content/reportes_template/skycons/skycons.js"></script>
    <!-- Flot -->
    <script src="/Content/reportes_template/Flot/jquery.flot.js"></script>
    <script src="/Content/reportes_template/Flot/jquery.flot.pie.js"></script>
    <script src="/Content/reportes_template/Flot/jquery.flot.time.js"></script>
    <script src="/Content/reportes_template/Flot/jquery.flot.stack.js"></script>
    <script src="/Content/reportes_template/Flot/jquery.flot.resize.js"></script>
    <!-- Flot plugins -->
    <script src="/Content/reportes_template/flot.orderbars/js/jquery.flot.orderBars.js"></script>
    <script src="/Content/reportes_template/flot-spline/js/jquery.flot.spline.min.js"></script>
    <script src="/Content/reportes_template/flot.curvedlines/curvedLines.js"></script>
    <!-- DateJS -->
    <script src="/Content/reportes_template/DateJS/build/date.js"></script>

    <script type="text/javascript">
        <%--WHATSAPP_URL = '<%=ConfigurationManager.AppSettings["WhatsApp_URL"].ToString()%>';--%>
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box box-default">
        <div class="box-header with-border" style="padding-bottom: 0;">

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-9">
                    <h1 style="font-size: 24px;">Dashboard</h1>
                </div>
            </div>

            <div class="row panel panel-default" style="margin-top: 10px; padding-top: 10px;">
                <div class="col-xs-12 col-sm-12 col-md-2 pull-left panel-collapse collapse in" style="display: none;">

                    <div class="input-group row-short" style="padding: 5px; width: -webkit-fill-available;">
                        <asp:DropDownList ID="ddl_year" runat="server" ClientIDMode="Static" CssClass="form-control dropdown" />
                    </div>

                    <%--SOURCE: http://bootstrap-table.wenzhixin.net.cn/getting-started/--%>
                    <%--SOURCE: http://issues.wenzhixin.net.cn/bootstrap-table/--%>
                </div>

                <div class="col-xs-12 col-sm-12 col-md-10" style="padding-left: 10%; padding-right: 10%; width: 100%;">

                    <div id="tabsFormularios">
                        <ul>
                            <li><a href="#tabsOrdenes" class="tabsFormularios">Trades activos</a></li>
                        </ul>

                        <div id="tabsOrdenes">

                            <!-- Tab Ordenes BEGIN -->
                            <asp:UpdatePanel ID="upOrdenes" runat="server">
                                <ContentTemplate>

                                    <%--<asp:HiddenField ID="hdn_ordenID" runat="server" ClientIDMode="Static" />--%>

                                    <div class="row">
                                        <div class="col-sm-12 col-md-4 pull-left" style="display: grid;">
                                            <div class="row-short">
                                                <form action="#" method="get" class="sidebar-form" style="display: block !important; width: 100%;">
                                                    <div class="input-group ">
                                                        <input type="text" id="txbSearchPedidos" name="q" class="form-control" placeholder="Buscar..." />
                                                        <span class="input-group-btn">
                                                            <a id="btnSearchPedidos" role="button" href="#" name="search" class="btn btn-info">
                                                                <i class="glyphicon glyphicon-search"></i>
                                                            </a>
                                                        </span>
                                                    </div>
                                                </form>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-6 pull-right">
                                            <div class="input-group pull-right" style="padding: 5px;">
                                                <div class="form-check">
                                                    <input id="chbSoloEntrCol" class="form-check-input" type="checkbox" onclick="filtrar()">
                                                    <label class="form-check-label unselectable" for="chbSoloEntrCol">
                                                        Entregas e Inst.
                                                    </label>
                                                </div>

                                                <div class="form-check">
                                                    <input id="chbSoloVigentes" class="form-check-input" type="checkbox" onclick="filtrar()">
                                                    <label class="form-check-label unselectable" for="chbSoloVigentes">
                                                        Todo el mes
                                                    </label>
                                                </div>

                                                <div class="form-check">
                                                    <input id="chbInclCancelados" class="form-check-input" type="checkbox" onclick="filtrar()">
                                                    <label class="form-check-label unselectable" for="chbInclCancelados">
                                                        Incluir cancelados
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <hr />
                                    <br />
                                    <div class="row-short">
                                        <h2 style="margin-top: 0;">
                                            <label class="unselectable">Trades activos</label></h2>
                                    </div>
                                    <br />
                                    <asp:Label ID="gridTradesActivos_lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <div id="div_gridTradesActivos" style="overflow: auto;">
                                        <asp:GridView ID="gridTradesActivos" runat="server" ClientIDMode="Static" HorizontalAlign="Center" AutoGenerateColumns="false" CssClass="table table-hover table-striped" AllowPaging="false" AllowSorting="false"
                                            DataKeyNames="Orden_ID"
                                            OnRowDataBound="gridTradesActivos_RowDataBound"
                                            OnRowCommand="gridTradesActivos_RowCommand">

                                            <Columns>
                                                <asp:BoundField DataField="Orden_ID" HeaderText="Orden_ID" HtmlEncode="false" ItemStyle-CssClass="hiddencol hiddencol_real" HeaderStyle-CssClass="hiddencol hiddencol_real" />

                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNumber" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Datetime" HeaderText="Datetime" />

                                                <%--<asp:TemplateField HeaderText="Operación">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOperacion" runat="server" CommandName="View" Text='<%# Eval("Fecha_creado", "{0:dd-MM-yyyy}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Operación">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOperacion" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Instrumento">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInstrumento" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ticker">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTicker" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNombre" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Valor unitario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorUnitario" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCantidad" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Valor total">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorTotal" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="OPC" ControlStyle-CssClass="btn btn-warning btn-xs">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnOPC" runat="server" CommandName="View" CssClass="btn btn-primary btn-xs fa fa-asterisk" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:Label ID="lblGridTradesActivosCount" runat="server" ClientIDMode="Static" Text="Resultados: 0" CssClass="lblResultados label label-info"></asp:Label>


                                    <hr />
                                    <br />
                                    <div class="row-short">
                                        <h2 style="margin-top: 0;">
                                            <label class="unselectable">Órdenes pendientes</label></h2>
                                    </div>
                                    <br />


                                    <asp:Label ID="gridOrdenesPendientes_lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <div id="div_gridOrdenesPendientes" style="overflow: auto;">
                                        <asp:GridView ID="gridOrdenesPendientes" runat="server" ClientIDMode="Static" HorizontalAlign="Center" AutoGenerateColumns="false" CssClass="table table-hover table-striped" AllowPaging="false" AllowSorting="false"
                                            DataKeyNames="Orden_ID"
                                            OnRowDataBound="gridOrdenesPendientes_RowDataBound"
                                            OnRowCommand="gridOrdenesPendientes_RowCommand">

                                            <Columns>
                                                <asp:BoundField DataField="Orden_ID" HeaderText="Orden_ID" HtmlEncode="false" ItemStyle-CssClass="hiddencol hiddencol_real" HeaderStyle-CssClass="hiddencol hiddencol_real" />

                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNumber" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Datetime" HeaderText="Datetime" />

                                                <asp:TemplateField HeaderText="Operación">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOperacion" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Instrumento">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInstrumento" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ticker">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTicker" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNombre" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Valor unitario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorUnitario" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCantidad" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Valor total">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorTotal" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="OPC" ControlStyle-CssClass="btn btn-warning btn-xs">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnOPC" runat="server" CommandName="View" CssClass="btn btn-primary btn-xs fa fa-asterisk" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:Label ID="lblGridOrdenesPendientesCount" runat="server" ClientIDMode="Static" Text="Resultados: 0" CssClass="lblResultados label label-info"></asp:Label>










                                    <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                    <div class="row" style="margin: 0;">
                                        <div class="col-md-12 pull-left" style="padding: 10px;">
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <br />
                            <div class="row">
                                <%--<label style="color: green">Concluído</label>
                                | --%>
                                <label style="color: #333">Vigente</label>
                                | 
        <label style="color: blue">Aprobado</label>
                                | 
        <label style="color: red">Cancelado</label>
                            </div>


                        </div>

                    </div>
                </div>
            </div>

        </div>

        <div id="dialog" title="Mensaje Cartelux">
            <p style="text-align: left;"></p>
        </div>

        <div id="simplePopbox" class='popbox'></div>
        <div id="divPopbox_OPC" class='popbox-box popbox'>
            <div class='arrow' style="left: 250px;"></div>
            <div class='arrow-border' style="left: 250px;"></div>
            <div class="row row-short" style="padding: 10px;">
                <label id="lbl_options_header_OPC" class="label" style="font-size: 100%; color: rgba(68, 89, 156, 1); font-size: 16px;">Borrar elemento seleccionado</label>
            </div>
            <div class="form-group" style="margin-bottom: 0;">
                <div class="row row-short" style="padding: 10px;">
                    <div id="lbl_options_divBox_OPC" class="alert alert-warning" role="alert">
                        <span id="lbl_options_color_OPC" class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>
                        <span id="lbl_options_info_OPC" class="unselectable">Opciones de estado</span>
                    </div>
                </div>
                <div id="popbox_footer_OPC" class="row row-short" style="margin-top: -7px;">
                    <%--<button id="lbl_options_button1_OPC" type="button" class="btn btn-success btnAcciones" title="Impreso">
                    <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                </button>--%>
                    <button id="lbl_options_button2_OPC" type="button" class="btn btn-primary btnAcciones" title="Aprobado">
                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                    </button>
                    <button id="lbl_options_button3_OPC" type="button" class="btn btn-danger btnAcciones" title="Cancelado">
                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                    </button>
                    <button id="lbl_options_button4_OPC" type="button" class="btn btn-default btnAcciones" title="Vigente">
                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                    </button>
                </div>
            </div>
            <button type="button" class="btn close" style="padding: 5px;">
                <span class="fa fa-times-circle" aria-hidden="true"></span>
            </button>
        </div>

        <div id="divPopbox_CTO" class='popbox-box popbox'>
            <div class='arrow' style="left: 250px;"></div>
            <div class='arrow-border' style="left: 250px;"></div>
            <div class="row row-short" style="padding: 10px;">
                <label id="lbl_options_header_CTO" class="label" style="font-size: 100%; color: rgba(68, 89, 156, 1); font-size: 16px;">Borrar elemento seleccionado</label>
            </div>
            <div class="form-group" style="margin-bottom: 0;">
                <div class="row row-short" style="padding: 10px;">

                    <div id="lbl_options_divBox_CTO" class="alert alert-warning" role="alert">
                        <span id="lbl_options_color_CTO" class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>
                        <span id="lbl_options_info_CTO" class="unselectable">Opciones de contacto</span>
                    </div>
                    <%--<input id="lbl_options_password" type="password" class="form-control" placeholder="Contraseña" name="login-username" required="required" />--%>
                </div>
                <div id="popbox_footer_CTO" class="row row-short" style="margin-top: -7px;">
                    <a id='lbl_options_button1_CTO' role='button' href='#' class='btnAcciones btn btn-primary fa fa-wpforms fa-2x' title='Ir a formulario' target='_blank'></a>
                    <a id='lbl_options_button2_CTO' role='button' href='#' class='btnAcciones btn btn-danger fa fa-map-marker fa-2x' title='Ir a Google Maps' target='_blank'></a>
                    <a id='lbl_options_button3_CTO' role='button' href='#' class='btnAcciones btn btn-success fa fa-whatsapp fa-2x' title='Ir a WhatsApp' target='_blank'></a>

                    <input class="form-control pull-left text-to-copy" id="txbLink" onclick="this.select();" value="?" style="width: 85%; display: none;" />

                </div>
            </div>
            <button type="button" class="btn close" style="padding: 5px;">
                <span class="fa fa-times-circle" aria-hidden="true"></span>
            </button>
        </div>

        <asp:HiddenField ID="hdn_OrdenID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdn_monthSelected" runat="server" ClientIDMode="Static" />

    </div>
</asp:Content>

