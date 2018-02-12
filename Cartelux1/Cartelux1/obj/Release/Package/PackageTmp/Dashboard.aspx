<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Cartelux1.Dashboard" Title="Cartelux Publicidad" %>

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
        #ddl_year {
            margin: auto;
            width: 50%;
        }

            #ddl_year > .dropdown-menu {
                position: relative !important;
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
    <%--<script type="text/javascript" src="/Content/js/MonthPicker.js"></script>--%>

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

                <div class="col-xs-12 col-sm-12 col-md-3 pull-left">
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col-xs-12 col-sm-12 col-md-7 pull-right">
                        </div>
                    </div>

                    <asp:DropDownList ID="ddl_year" runat="server" ClientIDMode="Static" CssClass="form-control dropdown"/>

                    <%--SOURCE: http://bootstrap-table.wenzhixin.net.cn/getting-started/--%>
                    <%--SOURCE: http://issues.wenzhixin.net.cn/bootstrap-table/--%>
                    <table id="gridMonth" data-toggle="table">
                        <%--data-search="true" --%>
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Seleccionar mes
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr id="tr-id-1" class="tr-class-1">
                                <td id="td-id-1" class="td-class-1">
                                    <h4>1</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="1">Enero</td>
                            </tr>
                            <tr id="tr-id-2" class="tr-class-2">
                                <td id="td-id-2" class="td-class-2">
                                    <h4>2</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="2">Febrero</td>
                            </tr>
                            <tr id="tr-id-3" class="tr-class-3">
                                <td id="td-id-3" class="td-class-3">
                                    <h4>3</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="3">Marzo</td>
                            </tr>
                            <tr id="tr-id-4" class="tr-class-4">
                                <td id="td-id-4" class="td-class-4">
                                    <h4>4</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="4">Abril</td>
                            </tr>
                            <tr id="tr-id-5" class="tr-class-5">
                                <td id="td-id-5" class="td-class-5">
                                    <h4>5</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="5">Mayo</td>
                            </tr>
                            <tr id="tr-id-6" class="tr-class-6">
                                <td id="td-id-6" class="td-class-6">
                                    <h4>6</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="6">Junio</td>
                            </tr>
                            <tr id="tr-id-7" class="tr-class-7">
                                <td id="td-id-7" class="td-class-7">
                                    <h4>7</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="7">Julio</td>
                            </tr>
                            <tr id="tr-id-8" class="tr-class-8">
                                <td id="td-id-8" class="td-class-8">
                                    <h4>8</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="8">Agosto</td>
                            </tr>
                            <tr id="tr-id-9" class="tr-class-9">
                                <td id="td-id-9" class="td-class-9">
                                    <h4>9</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="9">Septiembre</td>
                            </tr>
                            <tr id="tr-id-10" class="tr-class-10">
                                <td id="td-id-10" class="td-class-10">
                                    <h4>10</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="10">Octubre</td>
                            </tr>
                            <tr id="tr-id-11" class="tr-class-11">
                                <td id="td-id-11" class="td-class-11">
                                    <h4>11</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="1">Noviembre</td>
                            </tr>
                            <tr id="tr-id-12" class="tr-class-12">
                                <td id="td-id-12" class="td-class-12">
                                    <h4>12</h4>
                                </td>
                                <td class="btn btn-primary btn-lg btn-table" data-value="12">Diciembre</td>
                            </tr>
                        </tbody>
                    </table>



                    <%-- <asp:GridView ID="gridMonths" runat="server" ClientIDMode="Static" HorizontalAlign="Left" AutoGenerateColumns="false" CssClass="table table-hover table-striped" AllowPaging="false"
                        OnRowCommand="gridMonths_RowCommand"
                        OnSelectedIndexChanged="gridMonths_OnSelectedIndexChanged">
                        <RowStyle HorizontalAlign="Left" />

                    </asp:GridView>--%>
                </div>

                <div class="col-xs-12 col-sm-12 col-md-9 pull-right">

                    <div id="tabsFormularios">
                        <ul>
                            <li><a href="#tabsFormularios_1" class="tabsFormularios">Formularios</a></li>
                            <li><a href="#tabsFormularios_2" class="tabsFormularios">Calendario</a></li>
                        </ul>

                        <!-- Tab Viajes BEGIN -->
                        <div id="tabsFormularios_1">

                            <div style="overflow: auto;">

                                <asp:UpdatePanel ID="upFormularios" runat="server">
                                    <ContentTemplate>

                                        <asp:HiddenField ID="hdn_clientID" runat="server" ClientIDMode="Static" />

                                        <div class="row">
                                            <div class="col-sm-12 col-md-4 pull-left">
                                                <h2>
                                                    <label id="lblMonth" class="pull-left">[MES]</label></h2>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 col-md-6 pull-left">
                                                <div class="input-group" style="padding: 5px;">
                                                    <asp:Button ID="btnSearch_saldos" runat="server" Text="Filtrar" CssClass="btn btn-sm btn-info btnUpdate pull-right"
                                                        OnClick="btnSearch_Click_saldos" UseSubmitBehavior="false" ClientIDMode="Static" CausesValidation="false" OnClientClick="Javascript:GetMonthFilter()" />
                                                    <input id="txbMonthpicker" type="text" class="month-year-input" style="margin-right: 10px;">
                                                </div>
                                            </div>

                                            <div class="col-sm-12 col-md-6 pull-right">
                                                <div class="input-group pull-right" style="padding: 5px;">
                                                    <div class="form-check">
                                                        <input id="chbSoloVigentes" class="form-check-input" type="checkbox" onclick="filtrar_soloVigentes()">
                                                        <label class="form-check-label" for="chbSoloVigentes">
                                                            Todo el mes
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:Label ID="gridFormularios_lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="gridFormularios" runat="server" ClientIDMode="Static" HorizontalAlign="Center" AutoGenerateColumns="false" CssClass="table table-hover table-striped" AllowPaging="false"
                                            DataKeyNames="Formulario_ID"
                                            OnRowDataBound="gridFormularios_RowDataBound"
                                            OnRowCommand="gridFormularios_RowCommand">

                                            <Columns>
                                                <%--<asp:BoundField DataField="Cliente_ID" HeaderText="ID" HtmlEncode="false" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />--%>
                                                <asp:BoundField DataField="Formulario_ID" HeaderText="Formulario_ID" HtmlEncode="false" ItemStyle-CssClass="hiddencol hiddencol_real" HeaderStyle-CssClass="hiddencol hiddencol_real" />
                                                <asp:BoundField DataField="URL_short" HeaderText="URL_short" HtmlEncode="False" ItemStyle-CssClass="hiddencol hiddencol_real" HeaderStyle-CssClass="hiddencol hiddencol_real" />

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

                                                 <asp:TemplateField HeaderText="Fecha de Entrega">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaEntrega" runat="server" CommandName="View" Text='<%# Eval("Fecha_creado", "{0:dd-MM-yyyy}") %>' />
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

                                                <asp:BoundField DataField="URL_short" HeaderText="URL Formulario" HtmlEncode="False" />

                                                <asp:TemplateField HeaderText="Ir al Form" ControlStyle-CssClass="btn btn-info btn-xs">
                                                    <ItemTemplate>
                                                        <a id="btnURL" role="button" href='<%# Eval("URL_short") %>' class="btn btn-info btn-xs glyphicon glyphicon-share-alt" title="" target="_blank"></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblgridFormulariosCount" runat="server" ClientIDMode="Static" Text="Resultados: 0" CssClass="lblResultados label label-info"></asp:Label>

                                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                        <div class="row" style="margin: 0;">
                                            <div class="col-md-12 pull-left" style="padding: 10px;">
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                        </div>

                        <div id="tabsFormularios_2">

                            <div style="overflow: auto;">

                                <asp:UpdatePanel ID="upViajes" runat="server">
                                    <ContentTemplate>

                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-sm-12 col-md-8 pull-left">
                                                <h2>Datos de los viajes</h2>
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
    <asp:HiddenField ID="hdn_yearSelected" runat="server" ClientIDMode="Static" />


</asp:Content>
