<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calculadora.aspx.cs" Inherits="Cartelux1.Calculadora" Title="Calculadora" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- STYLES EXTENSION -->

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/pages/dashboard.css" />
    <%--<link rel="stylesheet" href="/Content/css/pages/calculadora.css" />--%>



    <!-- Bootstrap -->
    <%--<link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">--%>
    <!-- Font Awesome -->
    <%--    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">--%>


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
    <%--<script type="text/javascript" src="/Content/js/jquery.quicksearch.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.modal.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="/Content/js/chosen.jquery.js"></script>
    <script type="text/javascript" src="/Content/js/moment.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.filtertable.min.js"></script>
    <script type="text/javascript" src="/Content/js/glDatePicker.min.js"></script>
    <script type="text/javascript" src="/Content/js/bic_calendar.js"></script>
    <script type="text/javascript" src="/Content/js/popbox.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="/Content/js/clipboard.min.js"></script>
    <script type="text/javascript" src="/Content/js/clipboard-action.js"></script>--%>


    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/pages/auxiliar_functions.js"></script>
    <script type="text/javascript" src="/Content/js/pages/calculadora.js"></script>

    <!-- JS -->
    <%--<script type="text/javascript" src="/Content/js/jquery.bootstrap-touchspin.js"></script>--%>
    <%--<script type="text/javascript" src="/Content/fancybox/jquery.fancybox-1.3.4.js"></script>--%>

    <%--<script type="text/javascript" src="/Content/plugins/jquery-radiocharm/jquery-radiocharm.js"></script>--%>
    <%--<script type="text/javascript" src="/Content/plugins/icheck-1.x/icheck.min.js"></script>--%>

    <%--SOURCE: https://stackoverflow.com/questions/5540395/can-i-open-a-url-in-a-jqueryui-dialog
    SOURCE: http://fancybox.net/--%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="box box-default">
        <div class="box-header with-border" style="padding-bottom: 0;">

            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-9">
                    <h1 style="font-size: 24px;">Dashboard</h1>
                </div>
            </div>

            <div class="row panel panel-default" style="margin-top: 10px; padding-top: 10px;">

                <div class="col-xs-12 col-sm-12 col-md-10 pull-right">

                    <div id="tabsBases">
                        <ul>
                            <li><a href="#tabsPrecios" class="tabsBases">Precios</a></li>
                            <li><a href="#tabsCostos" class="tabsBases">Costos</a></li>
                        </ul>
                        <!-- Tab tabsPrecios BEGIN -->
                        <div id="tabsPrecios" style="padding: 6px;">





                            <!-- page content -->
                            <div class="right_col" role="main">
                                <div class="">
                                    <div class="page-title">
                                        <div class="title_left">
                                            <h3>Calculadora de presupuestos - Precios</h3>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">

                                            <!-- image cropping -->
                                            <div class="container cropper">
                                                <div class="row">

                                                    <div class="col-md-4">
                                                        <label>Valores individuales</label>
                                                        <br />

                                                        <div class="docs-data">
                                                            <div class="input-group input-group-sm">
                                                                <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                <label class="input-group-addon" for="dataX">Lona front Mate</label>
                                                                <input id="txb_Precios_lonaFrontMate" type="text" class="form-control" placeholder="- CM2" />
                                                                <span id="calc_Precios_lonaFrontMate" class="input-group-addon">$</span>
                                                            </div>
                                                            <div class="input-group input-group-sm">
                                                                <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                <label class="input-group-addon" for="dataY">Lona doble faz</label>
                                                                <input id="txb_Precios_lonaDobleFaz" type="text" class="form-control" placeholder="- CM2" />
                                                                <span id="calc_Precios_lonaDobleFaz" class="input-group-addon">$</span>
                                                            </div>
                                                            <div class="input-group input-group-sm">
                                                                <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                <label class="input-group-addon" for="dataWidth">Vinilo</label>
                                                                <input id="txb_Precios_vinilo" type="text" class="form-control" placeholder="- CM2" />
                                                                <span id="calc_Precios_vinilo" class="input-group-addon">$</span>
                                                            </div>
                                                            <div class="input-group input-group-sm">
                                                                <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                <label class="input-group-addon" for="dataHeight">Vinilo Canvas</label>
                                                                <input id="txb_Precios_viniloCanvas" type="text" class="form-control" placeholder="- CM2" />
                                                                <span id="calc_Precios_viniloCanvas" class="input-group-addon">$</span>
                                                            </div>
                                                            <div class="input-group input-group-sm">
                                                                <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                <label class="input-group-addon" for="dataRotate">Vinilo Microperforado</label>
                                                                <input id="txb_Precios_viniloMicroperforado" type="text" class="form-control" placeholder="- CM2" />
                                                                <span id="calc_Precios_viniloMicroperforado" class="input-group-addon">$</span>
                                                            </div>
                                                            <div class="input-group input-group-sm">
                                                                <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                <label class="input-group-addon" for="dataScaleX">Tela bandera</label>
                                                                <input id="txb_Precios_telaBandera" type="text" class="form-control" placeholder="0.069 CM2" />
                                                                <span id="calc_Precios_telaBandera" class="input-group-addon">$</span>
                                                            </div>
                                                            <div class="input-group input-group-sm">
                                                                <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                <label class="input-group-addon" for="dataScaleY">Fondo de color extra</label>
                                                                <input id="txb_Precios_fondoColor" type="text" class="form-control" placeholder="- CM2" />
                                                                <span id="calc_Precios_fondoColor" class="input-group-addon">$</span>
                                                            </div>
                                                            <div class="input-group input-group-sm">
                                                                <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                <label class="input-group-addon" for="dataScaleY">Banners c/ojalillos</label>
                                                                <input id="txb_Precios_bannersOjalillos" type="text" class="form-control" placeholder="-" />
                                                                <span id="calc_Precios_bannersOjalillos" class="input-group-addon">$</span>
                                                            </div>
                                                            <div class="input-group input-group-sm">
                                                                <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                <label class="input-group-addon" for="dataScaleY">Banners c/perfiles</label>
                                                                <input id="txb_Precios_bannersPerfiles" type="text" class="form-control" placeholder="- unitario" />
                                                                <span id="calc_Precios_bannersPerfiles" class="input-group-addon">$</span>
                                                            </div>
                                                            <div class="input-group input-group-sm">
                                                                <label class="input-group-addon" for="dataScaleY">Descuentos</label>
                                                                <input id="txb_Descuentos1" type="text" class="form-control" placeholder="%" />
                                                                <span id="calc_Descuentos1" class="input-group-addon">$</span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4 pull-right">
                                                        <label>Resultados acumulados</label>
                                                        <br />
                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /page content -->






                            </div>
                            <div id="tabsCostos" style="padding: 6px;">


                                <!-- page content -->
                                <div class="right_col" role="main">
                                    <div class="">
                                        <div class="page-title">
                                            <div class="title_left">
                                                <h3>Calculadora de presupuestos - Costos</h3>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">

                                                <!-- image cropping -->
                                                <div class="container cropper">
                                                    <div class="row">

                                                        <div class="col-md-4">
                                                            <label>Valores individuales</label>
                                                            <br />

                                                            <div class="docs-data">
                                                                <div class="input-group input-group-sm">
                                                                    <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                    <label class="input-group-addon" for="dataX">Lona front Mate</label>
                                                                    <input id="txb_Costos_lonaFrontMate" type="text" class="form-control" placeholder="- CM2" />
                                                                    <span id="calc_Costos_lonaFrontMate" class="input-group-addon">$</span>
                                                                </div>
                                                                <div class="input-group input-group-sm">
                                                                    <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                    <label class="input-group-addon" for="dataY">Lona doble faz</label>
                                                                    <input id="txb_Costos_lonaDobleFaz" type="text" class="form-control" placeholder="- CM2" />
                                                                    <span id="calc_Costos_lonaDobleFaz" class="input-group-addon">$</span>
                                                                </div>
                                                                <div class="input-group input-group-sm">
                                                                    <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                    <label class="input-group-addon" for="dataWidth">Vinilo</label>
                                                                    <input id="txb_Costos_vinilo" type="text" class="form-control" placeholder="- CM2" />
                                                                    <span id="calc_Costos_vinilo" class="input-group-addon">$</span>
                                                                </div>
                                                                <div class="input-group input-group-sm">
                                                                    <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                    <label class="input-group-addon" for="dataHeight">Vinilo Canvas</label>
                                                                    <input id="txb_Costos_viniloCanvas" type="text" class="form-control" placeholder="- CM2" />
                                                                    <span id="calc_Costos_viniloCanvas" class="input-group-addon">$</span>
                                                                </div>
                                                                <div class="input-group input-group-sm">
                                                                    <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                    <label class="input-group-addon" for="dataRotate">Vinilo Microperforado</label>
                                                                    <input id="txb_Costos_viniloMicroperforado" type="text" class="form-control" placeholder="- CM2" />
                                                                    <span id="calc_Costos_viniloMicroperforado" class="input-group-addon">$</span>
                                                                </div>
                                                                <div class="input-group input-group-sm">
                                                                    <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                    <label class="input-group-addon" for="dataScaleX">Tela bandera</label>
                                                                    <input id="txb_Costos_telaBandera" type="text" class="form-control" placeholder="0.069 CM2" />
                                                                    <span id="calc_Costos_telaBandera" class="input-group-addon">$</span>
                                                                </div>
                                                                <div class="input-group input-group-sm">
                                                                    <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                    <label class="input-group-addon" for="dataScaleY">Fondo de color extra</label>
                                                                    <input id="txb_Costos_fondoColor" type="text" class="form-control" placeholder="- CM2" />
                                                                    <span id="calc_Costos_fondoColor" class="input-group-addon">$</span>
                                                                </div>
                                                                <div class="input-group input-group-sm">
                                                                    <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                    <label class="input-group-addon" for="dataScaleY">Banners c/ojalillos</label>
                                                                    <input id="txb_Costos_bannersOjalillos" type="text" class="form-control" placeholder="-" />
                                                                    <span id="calc_Costos_bannersOjalillos" class="input-group-addon">$</span>
                                                                </div>
                                                                <div class="input-group input-group-sm">
                                                                    <span class="input-group-addon btn btn-outline-secondary"><i class="fa fa-wrench"></i></span>
                                                                    <label class="input-group-addon" for="dataScaleY">Banners c/perfiles</label>
                                                                    <input id="txb_Costos_bannersPerfiles" type="text" class="form-control" placeholder="- unitario" />
                                                                    <span id="calc_Costos_bannersPerfiles" class="input-group-addon">$</span>
                                                                </div>
                                                                <div class="input-group input-group-sm">
                                                                    <label class="input-group-addon" for="dataScaleY">Descuentos</label>
                                                                    <input id="txb_Descuentos2" type="text" class="form-control" placeholder="%" />
                                                                    <span id="calc_Descuentos2" class="input-group-addon">$</span>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-4 pull-right">
                                                            <label>Resultados acumulados</label>
                                                            <br />
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-9 docs-buttons">
                                                            <!-- <h3 class="page-header">Toolbar:</h3> -->
                                                            <div class="btn-group">
                                                                <button type="button" class="btn btn-primary" data-method="setDragMode" data-option="move" title="Move">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;setDragMode&quot;, &quot;move&quot;)">
                                                                        <span class="fa fa-arrows"></span>
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="setDragMode" data-option="crop" title="Crop">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;setDragMode&quot;, &quot;crop&quot;)">
                                                                        <span class="fa fa-crop"></span>
                                                                    </span>
                                                                </button>
                                                            </div>

                                                            <div class="btn-group">
                                                                <button type="button" class="btn btn-primary" data-method="zoom" data-option="0.1" title="Zoom In">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;zoom&quot;, 0.1)">
                                                                        <span class="fa fa-search-plus"></span>
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="zoom" data-option="-0.1" title="Zoom Out">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;zoom&quot;, -0.1)">
                                                                        <span class="fa fa-search-minus"></span>
                                                                    </span>
                                                                </button>
                                                            </div>

                                                            <div class="btn-group">
                                                                <button type="button" class="btn btn-primary" data-method="move" data-option="-10" data-second-option="0" title="Move Left">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;move&quot;, -10, 0)">
                                                                        <span class="fa fa-arrow-left"></span>
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="move" data-option="10" data-second-option="0" title="Move Right">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;move&quot;, 10, 0)">
                                                                        <span class="fa fa-arrow-right"></span>
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="move" data-option="0" data-second-option="-10" title="Move Up">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;move&quot;, 0, -10)">
                                                                        <span class="fa fa-arrow-up"></span>
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="move" data-option="0" data-second-option="10" title="Move Down">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;move&quot;, 0, 10)">
                                                                        <span class="fa fa-arrow-down"></span>
                                                                    </span>
                                                                </button>
                                                            </div>

                                                            <div class="btn-group">
                                                                <button type="button" class="btn btn-primary" data-method="rotate" data-option="-45" title="Rotate Left">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;rotate&quot;, -45)">
                                                                        <span class="fa fa-rotate-left"></span>
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="rotate" data-option="45" title="Rotate Right">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;rotate&quot;, 45)">
                                                                        <span class="fa fa-rotate-right"></span>
                                                                    </span>
                                                                </button>
                                                            </div>

                                                            <div class="btn-group">
                                                                <button type="button" class="btn btn-primary" data-method="scaleX" data-option="-1" title="Flip Horizontal">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;scaleX&quot;, -1)">
                                                                        <span class="fa fa-arrows-h"></span>
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="scaleY" data-option="-1" title="Flip Vertical">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;scaleY&quot;, -1)">
                                                                        <span class="fa fa-arrows-v"></span>
                                                                    </span>
                                                                </button>
                                                            </div>

                                                            <div class="btn-group">
                                                                <button type="button" class="btn btn-primary" data-method="crop" title="Crop">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;crop&quot;)">
                                                                        <span class="fa fa-check"></span>
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="clear" title="Clear">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;clear&quot;)">
                                                                        <span class="fa fa-remove"></span>
                                                                    </span>
                                                                </button>
                                                            </div>

                                                            <div class="btn-group">
                                                                <button type="button" class="btn btn-primary" data-method="disable" title="Disable">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;disable&quot;)">
                                                                        <span class="fa fa-lock"></span>
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="enable" title="Enable">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;enable&quot;)">
                                                                        <span class="fa fa-unlock"></span>
                                                                    </span>
                                                                </button>
                                                            </div>

                                                            <div class="btn-group">
                                                                <button type="button" class="btn btn-primary" data-method="reset" title="Reset">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;reset&quot;)">
                                                                        <span class="fa fa-refresh"></span>
                                                                    </span>
                                                                </button>
                                                                <label class="btn btn-primary btn-upload" for="inputImage" title="Upload image file">
                                                                    <input type="file" class="sr-only" id="inputImage" name="file" accept="image/*">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="Import image with Blob URLs">
                                                                        <span class="fa fa-upload"></span>
                                                                    </span>
                                                                </label>
                                                                <button type="button" class="btn btn-primary" data-method="destroy" title="Destroy">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;destroy&quot;)">
                                                                        <span class="fa fa-power-off"></span>
                                                                    </span>
                                                                </button>
                                                            </div>

                                                            <div class="btn-group btn-group-crop">
                                                                <button type="button" class="btn btn-primary" data-method="getCroppedCanvas">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;getCroppedCanvas&quot;)">Get Cropped Canvas
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="getCroppedCanvas" data-option="{ &quot;width&quot;: 160, &quot;height&quot;: 90 }">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;getCroppedCanvas&quot;, { width: 160, height: 90 })">160&times;90
                                                                    </span>
                                                                </button>
                                                                <button type="button" class="btn btn-primary" data-method="getCroppedCanvas" data-option="{ &quot;width&quot;: 320, &quot;height&quot;: 180 }">
                                                                    <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;getCroppedCanvas&quot;, { width: 320, height: 180 })">320&times;180
                                                                    </span>
                                                                </button>
                                                            </div>

                                                            <!-- Show the cropped image in modal -->
                                                            <div class="modal fade docs-cropped" id="getCroppedCanvasModal" aria-hidden="true" aria-labelledby="getCroppedCanvasTitle" role="dialog" tabindex="-1">
                                                                <div class="modal-dialog">
                                                                    <div class="modal-content">
                                                                        <div class="modal-header">
                                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                            <h4 class="modal-title" id="getCroppedCanvasTitle">Cropped</h4>
                                                                        </div>
                                                                        <div class="modal-body"></div>
                                                                        <div class="modal-footer">
                                                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                                            <a class="btn btn-primary" id="download" href="javascript:void(0);" download="cropped.png">Download</a>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- /.modal -->

                                                            <button type="button" class="btn btn-primary" data-method="getData" data-option data-target="#putData">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;getData&quot;)">Get Data
                                                                </span>
                                                            </button>
                                                            <button type="button" class="btn btn-primary" data-method="setData" data-target="#putData">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;setData&quot;, data)">Set Data
                                                                </span>
                                                            </button>
                                                            <button type="button" class="btn btn-primary" data-method="getContainerData" data-option data-target="#putData">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;getContainerData&quot;)">Get Container Data
                                                                </span>
                                                            </button>
                                                            <button type="button" class="btn btn-primary" data-method="getImageData" data-option data-target="#putData">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;getImageData&quot;)">Get Image Data
                                                                </span>
                                                            </button>
                                                            <button type="button" class="btn btn-primary" data-method="getCanvasData" data-option data-target="#putData">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;getCanvasData&quot;)">Get Canvas Data
                                                                </span>
                                                            </button>
                                                            <button type="button" class="btn btn-primary" data-method="setCanvasData" data-target="#putData">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;setCanvasData&quot;, data)">Set Canvas Data
                                                                </span>
                                                            </button>
                                                            <button type="button" class="btn btn-primary" data-method="getCropBoxData" data-option data-target="#putData">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;getCropBoxData&quot;)">Get Crop Box Data
                                                                </span>
                                                            </button>
                                                            <button type="button" class="btn btn-primary" data-method="setCropBoxData" data-target="#putData">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="$().cropper(&quot;setCropBoxData&quot;, data)">Set Crop Box Data
                                                                </span>
                                                            </button>
                                                            <button type="button" class="btn btn-primary" data-method="moveTo" data-option="0">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="cropper.moveTo(0)">0,0
                                                                </span>
                                                            </button>
                                                            <button type="button" class="btn btn-primary" data-method="zoomTo" data-option="1">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="cropper.zoomTo(1)">100%
                                                                </span>
                                                            </button>
                                                            <button type="button" class="btn btn-primary" data-method="rotateTo" data-option="180">
                                                                <span class="docs-tooltip" data-toggle="tooltip" title="cropper.rotateTo(180)">180°
                                                                </span>
                                                            </button>
                                                            <input type="text" class="form-control" id="putData" placeholder="Get data to here or set data with this value">
                                                        </div>
                                                        <!-- /.docs-buttons -->


                                                    </div>
                                                </div>
                                                <!-- /image cropping -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /page content -->






                            </div>
                        </div>









                    </div>
                </div>



            </div>

        </div>
</asp:Content>
