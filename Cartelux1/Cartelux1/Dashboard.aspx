﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Cartelux1.Dashboard" Title="Dashboard" %>

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
    <link href="/Content/reportes_template/nprogress/nprogress.css" rel="stylesheet"/>
    <!-- iCheck -->
    <link href="/Content/reportes_template/iCheck/flat/green.css" rel="stylesheet"/>
    <!-- bootstrap-progressbar -->
    <link href="/Content/reportes_template/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet"/>
    <!-- Custom Theme Style -->
    <link href="/Content/reportes_template/custom.css" rel="stylesheet"/>

    <style type="text/css">
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
    <script type="text/javascript" src="/Content/js/jquery.modal.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="/Content/js/chosen.jquery.js"></script>
    <script type="text/javascript" src="/Content/js/moment.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.filtertable.min.js"></script>
    <script type="text/javascript" src="/Content/js/glDatePicker.min.js"></script>
    <script type="text/javascript" src="/Content/js/bic_calendar.js"></script>
    <script type="text/javascript" src="/Content/js/popbox.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.quicksearch.js"></script>

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
                <a id="aCollapse_left_panel" data-toggle="collapse" href="#left_panel" style="display: none;">Seleccionar mes</a>
                <div id="left_panel" class="col-xs-12 col-sm-12 col-md-2 pull-left panel-collapse collapse in">
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col-xs-12 col-sm-12 col-md-7 pull-right">
                        </div>
                    </div>

                    <div class="input-group row-short" style="padding: 5px; width: -webkit-fill-available;">
                        <label class="pull-left unselectable" style="padding: 6px; margin-left: 10px;">Año</label>
                        <asp:DropDownList ID="ddl_year" runat="server" ClientIDMode="Static" CssClass="form-control dropdown" />
                    </div>

                    <%--SOURCE: http://bootstrap-table.wenzhixin.net.cn/getting-started/--%>
                    <%--SOURCE: http://issues.wenzhixin.net.cn/bootstrap-table/--%>
                    <table id="gridMonth" data-toggle="table">
                        <tbody>
                            <tr id="tr-id-1" class="tr-class-1">
                                <td class="td-style btn btn-info btn-md btn-table td-class-1" data-value="1">Enero</td>
                            </tr>
                            <tr id="tr-id-2" class="tr-class-2">
                                <td class="td-style btn btn-info btn-md btn-table td-class-2" data-value="2">Febrero</td>
                            </tr>
                            <tr id="tr-id-3" class="tr-class-3">
                                <td class="td-style btn btn-info btn-md btn-table td-class-3" data-value="3">Marzo</td>
                            </tr>
                            <tr id="tr-id-4" class="tr-class-4">
                                <td class="td-style btn btn-info btn-md btn-table td-class-4" data-value="4">Abril</td>
                            </tr>
                            <tr id="tr-id-5" class="tr-class-5">
                                <td class="td-style btn btn-info btn-md btn-table td-class-5" data-value="5">Mayo</td>
                            </tr>
                            <tr id="tr-id-6" class="tr-class-6">
                                <td class="td-style btn btn-info btn-md btn-table td-class-6" data-value="6">Junio</td>
                            </tr>
                            <tr id="tr-id-7" class="tr-class-7">
                                <td class="td-style btn btn-info btn-md btn-table td-class-7" data-value="7">Julio</td>
                            </tr>
                            <tr id="tr-id-8" class="tr-class-8">
                                <td class="td-style btn btn-info btn-md btn-table td-class-8" data-value="8">Agosto</td>
                            </tr>
                            <tr id="tr-id-9" class="tr-class-9">
                                <td class="td-style btn btn-info btn-md btn-table td-class-9" data-value="9">Septiembre</td>
                            </tr>
                            <tr id="tr-id-10" class="tr-class-10">
                                <td class="td-style btn btn-info btn-md btn-table td-class-10" data-value="10">Octubre</td>
                            </tr>
                            <tr id="tr-id-11" class="tr-class-11">
                                <td class="td-style btn btn-info btn-md btn-table td-class-11" data-value="11">Noviembre</td>
                            </tr>
                            <tr id="tr-id-12" class="tr-class-12">
                                <td class="td-style btn btn-info btn-md btn-table td-class-12" data-value="12">Diciembre</td>
                            </tr>
                        </tbody>
                    </table>

                </div>

                <div class="col-xs-12 col-sm-12 col-md-10 pull-right">

                    <div id="tabsFormularios">
                        <ul>
                            <li><a href="#tabsFormularios_1" class="tabsFormularios">Formularios</a></li>
                            <li><a href="#tabsFormularios_2" class="tabsFormularios">Calendario</a></li>
                            <li><a href="#tabsFormularios_3" class="tabsFormularios">Reportes</a></li>
                        </ul>

                        <!-- Tab Formularios BEGIN -->
                        <div id="tabsFormularios_1" style="padding: 6px;">
                            <asp:UpdatePanel ID="upFormularios" runat="server">
                                <ContentTemplate>

                                    <asp:HiddenField ID="hdn_clientID" runat="server" ClientIDMode="Static" />

                                    <div class="row">
                                        <div class="col-sm-12 col-md-4 pull-left" style="display: grid;">
                                            <div class="row-short">
                                                <h2 style="margin-top: 0;">
                                                    <label id="lblMonth" class="pull-left unselectable">[MES]</label></h2>
                                            </div>
                                            <div class="row-short">
                                                <form action="#" method="get" class="sidebar-form" style="display: block !important; width: 100%;">
                                                    <div class="input-group ">
                                                        <input type="text" id="txbSearchPedidos" name="q" class="form-control" placeholder="Buscar...">
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
                                                        Entregas/Cols.
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

                                    <div class="row">
                                        <%--<div class="col-sm-12 col-md-6 pull-left">
                                            <div class="input-group" style="padding: 5px;">
                                                <asp:Button ID="btnSearch_saldos" runat="server" Text="Filtrar" CssClass="btn btn-sm btn-info btnUpdate pull-right"
                                                    OnClick="btnSearch_Click_saldos" UseSubmitBehavior="false" ClientIDMode="Static" CausesValidation="false" OnClientClick="Javascript:GetMonthFilter()" />
                                                <input id="txbMonthpicker" type="text" class="month-year-input" style="margin-right: 10px;">
                                            </div>
                                        </div>--%>
                                    </div>

                                    <asp:Label ID="gridFormularios_lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <div id="div_gridFormularios" style="overflow: auto;">
                                        <asp:GridView ID="gridFormularios" runat="server" ClientIDMode="Static" HorizontalAlign="Center" AutoGenerateColumns="false" CssClass="table table-hover table-striped" AllowPaging="false" AllowSorting="false"
                                            DataKeyNames="Formulario_ID"
                                            OnRowDataBound="gridFormularios_RowDataBound"
                                            OnRowCommand="gridFormularios_RowCommand">

                                            <Columns>
                                                <%--<asp:BoundField DataField="Cliente_ID" HeaderText="ID" HtmlEncode="false" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />--%>
                                                <asp:BoundField DataField="Formulario_ID" HeaderText="Formulario_ID" HtmlEncode="false" ItemStyle-CssClass="hiddencol hiddencol_real" HeaderStyle-CssClass="hiddencol hiddencol_real" />
                                                <asp:BoundField DataField="URL_short" HeaderText="URL_short" HtmlEncode="False" ItemStyle-CssClass="hiddencol hiddencol_real" HeaderStyle-CssClass="hiddencol hiddencol_real" />

                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNumber" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Entrega">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaEntrega" runat="server" CommandName="View" Text='<%# Eval("Fecha_creado", "{0:dd-MM-yyyy}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Teléfono">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTelefono" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNombre" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="T/Entrega">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipoEntrega" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tamaño">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTamano" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="T/Cartel">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipo" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Impr/Pint">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaterial" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--  <asp:TemplateField HeaderText="Cant">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCantidad" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Zona">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZona" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="¿Archivo?">
                                                    <ItemTemplate>
                                                        <asp:Label ID="chbTieneBosquejo" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Diseño referido">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDisenoReferido" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="CTO" ControlStyle-CssClass="btn btn-warning btn-xs">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnCTO" runat="server" CommandName="View" CssClass="btn btn-warning btn-xs fa fa-address-card" />
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
                                    <asp:Label ID="lblgridFormulariosCount" runat="server" ClientIDMode="Static" Text="Resultados: 0" CssClass="lblResultados label label-info"></asp:Label>

                                    <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                    <div class="row" style="margin: 0;">
                                        <div class="col-md-12 pull-left" style="padding: 10px;">
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <br />
                            <div class="row">
                                <label style="color: green">Concluído</label>
                                | 
        <label style="color: #333">Vigente</label>
                                | 
        <label style="color: blue">Aprobado</label>
                                | 
        <label style="color: red">Cancelado</label>
                            </div>
                        </div>

                        <div id="tabsFormularios_2">

                            <div style="overflow: auto; height: -webkit-fill-available;">

                                <asp:UpdatePanel ID="upCalendario" runat="server">
                                    <ContentTemplate>

                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-sm-12 col-md-8 pull-left">
                                                <h2>Calendario de pedidos</h2>
                                            </div>

                                            <%-- <div class="col-sm-12 col-md-4 pull-right">
                                                <form action="#" method="get" class="sidebar-form" style="display: block !important; width: 100%;">
                                                    <div class="input-group ">
                                                        <input type="text" id="txbSearchViajes" name="q" class="form-control" placeholder="Buscar...">
                                                        <span class="input-group-btn">
                                                            <button type="button" name="search" id="btnSearchViajes" class="btn btn-flat">
                                                                <i class="fa fa-search"></i>
                                                            </button>
                                                        </span>
                                                    </div>
                                                </form>
                                            </div>--%>
                                        </div>

                                        <%--SOURCE: http://glad.github.io/glDatePicker/#examples --%>

                                        <div id="calendar"></div>


                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <br />
                                <div class="row">
                                    <label style="color: #333">Envío al interior</label>
                                    | 
        <label style="color: green">Retiro en el taller</label>
                                    | 
        <label style="color: blue">Envío a domicilio</label>
                                    | 
        <label style="color: red">Colocación</label>
                                </div>

                            </div>

                        </div>

                        <div id="tabsFormularios_3">




                             <!-- page content -->
        <div class="right_col" role="main" style="margin-left:0">
          <!-- top tiles -->
          <div class="row tile_count">
                <h1>Pedidos</h1>
            <div class="col-md-3 col-sm-12 col-xs-12 tile_stats_count">
              <span class="count_top"><i class="fa fa-user"></i> Total Pedidos</span>
              <div class="count green">2500</div>
              <span class="count_bottom"><i class="green">4% </i> que el mes anterior</span>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top"><i class="fa fa-clock-o"></i> Total Carteles</span>
              <div class="count">123.50</div>
              <span class="count_bottom"><i class="green"><i class="fa fa-sort-asc"></i>3% </i> que el mes anterior</span>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top"><i class="fa fa-user"></i> Total Banners</span>
              <div class="count">2,500</div>
              <span class="count_bottom"><i class="green"><i class="fa fa-sort-asc"></i>34% </i> que el mes anterior</span>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top"><i class="fa fa-user"></i> Total Roll ups</span>
              <div class="count">4,567</div>
              <span class="count_bottom"><i class="red"><i class="fa fa-sort-desc"></i>12% </i> que el mes anterior</span>
            </div>
          </div>

            <div class="row tile_count">
                <h1>Otros</h1>
            <div class="col-md-4 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top"><i class="fa fa-user"></i> Colocaciones</span>
              <div class="count">2500</div>
              <span class="count_bottom"><i class="green">4% </i> que el mes anterior</span>
            </div>
                <div class="col-md-4 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top"><i class="fa fa-user"></i> Lona usada mts.</span>
              <div class="count">2500</div>
              <span class="count_bottom"><i class="green">4% </i> que el mes anterior</span>
            </div>
            <div class="col-md-4 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top"><i class="fa fa-clock-o"></i> Total Palos</span>
              <div class="count">123.50</div>
              <span class="count_bottom"><i class="green"><i class="fa fa-sort-asc"></i>3% </i> que el mes anterior</span>
            </div>            
          </div>
          <!-- /top tiles -->

          <br />


        </div>
        <!-- /page content -->










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
            </div>
        </div>
        <button type="button" class="btn close" style="padding: 5px;">
            <span class="fa fa-times-circle" aria-hidden="true"></span>
        </button>
    </div>

    <asp:HiddenField ID="hdn_FormularioID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdn_monthSelected" runat="server" ClientIDMode="Static" />

</asp:Content>
