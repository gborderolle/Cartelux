﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Cartelux1.Dashboard" Title="Cartelux Publicidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- STYLES EXTENSION -->
    <link rel="stylesheet" href="/Content/css/jquery.modal.css" />
    <link rel="stylesheet" href="/Content/css/chosen.css" />
    <link rel="stylesheet" href="/Content/css/MonthPicker.css" />

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/dashboard.css" />
    <link rel="stylesheet" href="/Content/css/Modal_styles.css" />

    <!-- Bootstrap table -->
    <link rel="stylesheet" href="/Content/css/bootstrap-table.css" />

    <style type="text/css">
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

    <!-- Bootstrap table -->
    <script type="text/javascript" src="/Content/js/bootstrap-table.js"></script>
    <script type="text/javascript" src="/Content/js/bootstrap-table-es-ES.js"></script>

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/auxiliar_functions.js"></script>
    <script type="text/javascript" src="/Content/js/dashboard.js"></script>

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
                        <%--data-search="true" --%>
                        <thead>
                            <tr>
                                <th>Seleccionar mes
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr id="tr-id-1" class="tr-class-1">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-1" data-value="1">Enero</td>
                            </tr>
                            <tr id="tr-id-2" class="tr-class-2">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-2" data-value="2">Febrero</td>
                            </tr>
                            <tr id="tr-id-3" class="tr-class-3">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-3" data-value="3">Marzo</td>
                            </tr>
                            <tr id="tr-id-4" class="tr-class-4">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-4" data-value="4">Abril</td>
                            </tr>
                            <tr id="tr-id-5" class="tr-class-5">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-5" data-value="5">Mayo</td>
                            </tr>
                            <tr id="tr-id-6" class="tr-class-6">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-6" data-value="6">Junio</td>
                            </tr>
                            <tr id="tr-id-7" class="tr-class-7">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-7" data-value="7">Julio</td>
                            </tr>
                            <tr id="tr-id-8" class="tr-class-8">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-8" data-value="8">Agosto</td>
                            </tr>
                            <tr id="tr-id-9" class="tr-class-9">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-9" data-value="9">Septiembre</td>
                            </tr>
                            <tr id="tr-id-10" class="tr-class-10">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-10" data-value="10">Octubre</td>
                            </tr>
                            <tr id="tr-id-11" class="tr-class-11">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-11" data-value="11">Noviembre</td>
                            </tr>
                            <tr id="tr-id-12" class="tr-class-12">
                                <td class="td-style btn btn-primary btn-lg btn-table td-class-12" data-value="12">Diciembre</td>
                            </tr>
                        </tbody>
                    </table>

                </div>

                <div class="col-xs-12 col-sm-12 col-md-10 pull-right">

                    <div id="tabsFormularios">
                        <ul>
                            <li><a href="#tabsFormularios_1" class="tabsFormularios">Formularios</a></li>
                            <li><a href="#tabsFormularios_2" class="tabsFormularios">Calendario</a></li>
                        </ul>

                        <!-- Tab Viajes BEGIN -->
                        <div id="tabsFormularios_1">
                            <asp:UpdatePanel ID="upFormularios" runat="server">
                                <ContentTemplate>

                                    <asp:HiddenField ID="hdn_clientID" runat="server" ClientIDMode="Static" />

                                    <div class="row">
                                        <div class="col-sm-12 col-md-4 pull-left">
                                            <h2 style="margin-top:0;">
                                                <label id="lblMonth" class="pull-left unselectable">[MES]</label></h2>
                                        </div>
                                        <div class="col-sm-12 col-md-6 pull-right">
                                            <div class="input-group pull-right" style="padding: 5px;">
                                                <div class="form-check">
                                                    <input id="chbSoloVigentes" class="form-check-input" type="checkbox" onclick="filtrar_soloVigentes()">
                                                    <label class="form-check-label unselectable" for="chbSoloVigentes">
                                                        Todo el mes
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

                                                <asp:TemplateField HeaderText="Fecha de Entrega">
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

                                                <asp:TemplateField HeaderText="Tipo de Entrega">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipoEntrega" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tamaño">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTamano" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tipo cartel">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipo" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Material">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaterial" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Zona">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZona" runat="server" CommandName="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="URL_short" HeaderText="URL Form" HtmlEncode="False" />

                                                <asp:TemplateField HeaderText="Ir al Form" ControlStyle-CssClass="btn btn-info btn-xs">
                                                    <ItemTemplate>
                                                        <a id="btnURL" role="button" href='<%# Eval("URL_short") %>' class="btn btn-info btn-xs glyphicon glyphicon-share-alt" title="" target="_blank"></a>
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
                        </div>

                        <div id="tabsFormularios_2">

                            <div style="overflow: auto;">

                                <asp:UpdatePanel ID="upViajes" runat="server">
                                    <ContentTemplate>

                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-sm-12 col-md-8 pull-left">
                                                <h2>Calendario de pedidos</h2>
                                            </div>

                                            <div class="col-sm-12 col-md-4 pull-right">
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
                                            </div>
                                        </div>

                                        <asp:Label ID="gridViajeslblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>

                                        <asp:Label ID="lblGridViajesCount" runat="server" ClientIDMode="Static" Text="# 0" CssClass="lblResultados label label-info"></asp:Label>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                        </div>

                    </div>

                </div>

            </div>
        </div>
    </div>


    <div id="dialog" title="Mensaje Cartelux" style="height: 0 !important;">
        <p style="text-align: left;"></p>
    </div>

    <asp:HiddenField ID="hdn_FormularioID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdn_monthSelected" runat="server" ClientIDMode="Static" />

</asp:Content>
