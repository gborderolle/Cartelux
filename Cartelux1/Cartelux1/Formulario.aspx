<%@ Page Language="C#" MasterPageFile="~/SiteExternal.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="Cartelux1.Formulario" Title="Formulario" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- STYLES EXTENSION -->
    <link rel="stylesheet" href="/Content/css/jquery-ui.css" />
    <link rel="stylesheet" href="/Content/css/jquery.bootstrap-touchspin.css" />
    <link rel="stylesheet" href="/Content/fancybox/jquery.fancybox-1.3.4.css" />

    <link rel="stylesheet" href="/Content/plugins/jquery-radiocharm/jquery-radiocharm.css" />
    <link rel="stylesheet" href="/Content/plugins/icheck-1.x/skins/futurico/futurico.css" />
    <link rel="stylesheet" href="/Content/plugins/icheck-1.x/skins/flat/blue.css" />

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/pages/formulario.css" />

    <script type="text/javascript">
        TAB_COUNT = 1;
        IS_TAB_1 = 1;

        $(function () {

            $('input').iCheck({
                checkboxClass: 'icheckbox_flat-blue',
                radioClass: 'iradio_flat-blue',
                increaseArea: '20%' // optional
            });

            $(".dropdown").selectmenu();
            $("#txbFecha").datepicker({ dateFormat: 'dd-mm-yy' });
            $("#txbFecha_tab2").datepicker({ dateFormat: 'dd-mm-yy' });
            google.maps.event.addDomListener(window, 'load', initialize);

            // SOURCE: https://www.jqueryscript.net/demo/Touch-Friendly-jQuery-Input-Spinner-Plugin-For-Bootstrap-3-TouchSpin/
            $("#txtRepeat").TouchSpin({
                min: 1,
                max: 10,
                stepinterval: 1,
                maxboostedstep: 10
            });

            $("#txtRepeat").on('change', function () {
                var txtRepeat = $("#txtRepeat").val();
                TAB_COUNT = txtRepeat;
                $("#hdnPedidoCantidad").val(txtRepeat);
            });

        });

        function clientGoogleMaps() {
            var maps_url = _current_completeURL;
            if (maps_url != null && maps_url.length > 0) {
                //document.location = maps_url;
                window.open(maps_url, '_blank');
            }
        }

        // WhatsApp
        function clientWhatsApp() {
            var tel = _TEL;
            if (tel != null && tel.length > 0) {
                // Si el número empieza con 0 lo borra
                var first = tel.charAt(0);
                if (first === "0") {
                    tel = tel.substring(1);
                }
                var url = "https://api.whatsapp.com/send?phone=598" + tel;
                url += "&text=" + hashMessages["Msj_inicioCliente"];
                window.open(url, '_blank');
            }
        }

        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

        function addCartel() {
            TAB_COUNT++;
            $("#hdnPedidoCantidad").val(TAB_COUNT);
            $("#lblTabCount").text(TAB_COUNT);
        }

        function removeCartel() {
            if (TAB_COUNT > 1) {
                TAB_COUNT--;
                $("#hdnPedidoCantidad").val(TAB_COUNT);
                $("#lblTabCount").text(TAB_COUNT);
            }
        }

    </script>
    <style type="text/css">
        body {
            background-color: #f2e0cf;
        }

        hr {
            margin-top: 10px;
            margin-bottom: 10px;
        }

        li {
            font-size: x-large;
        }

        .price {
            text-align: right;
        }

        #ddlMedioDePago-button {
            width: 47% !important;
        }

        .ddlBorder {
            border-style: solid;
            border-width: 1px;
        }

        .btnConfirm1 {
            background-image: none;
            background-color: #ea7209;
            height: 40px;
            font-size: larger;
        }

        .btnConfirm2 {
            background-image: none;
            background-color: #34aa57;
            height: 40px;
            font-size: larger;
        }

        .btnConfirm3 {
            background-image: none;
            height: 30px;
            font-size: medium;
            float: right;
            width: auto;
        }

        .control-short {
            width: 90%;
        }

        ._label1 {
            font-size: x-large;
            color: #ea7209;
        }

        ._label2 {
            font-size: x-large;
            color: #419641;
        }

        .row-special {
            margin: 0;
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

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        .ctrl-required {
            border-color: red;
        }

        .ctrl-required_correct {
            border-color: #34aa57;
        }

        .multitext {
            height: 100px;
        }

        .login-container {
            font-size: large;
        }

        .hand-cursor > label {
            cursor: pointer !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/pages/auxiliar_functions.js"></script>
    <script type="text/javascript" src="/Content/js/pages/formulario.js"></script>
    <script type="text/javascript" src="/Content/js/pages/validar_documento.js"></script>

    <!-- JS -->
    <script type="text/javascript" src="/Content/js/jquery.bootstrap-touchspin.js"></script>
    <script type="text/javascript" src="/Content/fancybox/jquery.fancybox-1.3.4.js"></script>

    <script type="text/javascript" src="/Content/plugins/jquery-radiocharm/jquery-radiocharm.js"></script>
    <script type="text/javascript" src="/Content/plugins/icheck-1.x/icheck.min.js"></script>


    <%--SOURCE: https://stackoverflow.com/questions/5540395/can-i-open-a-url-in-a-jqueryui-dialog
    SOURCE: http://fancybox.net/--%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12 col-md-12" style="padding: 0;">
        <div class="box-header with-border dark in div-form1 col-sm-12 col-md-4" style="display: inline-block;">
            <div class="row" style="margin: auto; display: block; text-align: center; margin-bottom: 0;">
                <div class="loginTitleContainer"></div>
            </div>
            <div class="form-check pull-right" style="margin: 20px;">
            </div>
            <br />
            <%--<div class="alert alert-warning alert-dismissible" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <i class="glyphicon glyphicon-warning-sign"></i>&nbsp;<strong>Atención:</strong> con la confirmación de este formulario, ud. se compromete a la realización del pedido y abono del mismo. Cartelux se encuentra adherido a Clearing de Infomes - Equifax®.
            </div>--%>
            <div id="tabPedidos">

                <div class="panel with-nav-tabs panel-danger">
                    <%--<div class="panel-heading" style="padding-bottom: 0; display: -webkit-box;">--%>
                    <div class="panel-heading" style="padding-bottom: 0; width: -webkit-fill-available;">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tabsPedidos_1" id="aTabsPedidos_1" data-toggle="tab">Pedido</a></li>
                            <%--<li class=""><a href="#tabsPedidos_2" id="aTabsPedidos_2" data-toggle="tab">Roll up</a></li>--%>
                            <%--<li class=""><a href="#tabsPedidos_2" id="aTabsPedidos_2" data-toggle="tab">Cartelux</a></li>--%>
                        </ul>
                        <%--                        <input id="chbRepetidos" class="form-check-input form-control pull-right" type="checkbox" onclick="checkbox_repetidos()" style="width: 40px; position: absolute; right: 30px; display:none;">--%>
                    </div>

                    <div style="margin: 20px;">
                        <div class="row-special unselectable" id="repetidos_group" style="display: none;">
                            <input id="txtRepeat" type="text" value="0" class="form-control" />
                        </div>
                    </div>

                    <div class="panel-body" style="padding-top: 0;">
                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="tabsPedidos_1">

                                <div class="row" style="margin: auto; display: inline-flex; width: 100%;">

                                    <div class="col-sm-12 col-md-12" style="margin: auto; padding: 0;">
                                        <div class="login-container sub-form">
                                            <label class="_label1 unselectable" style="font-weight: normal;">1) Datos del cliente</label>
                                            <div class="form-group unselectable">
                                                <input id="txbTelefono" class="form-control ctrl-required" type="number" tabindex="1" placeholder="Teléfono de contacto" runat="server" clientidmode="static" pattern=".{6,}" title="6 dígitos mínimo" autofocus />
                                            </div>
                                            <div class="form-group unselectable">
                                                <input id="txbNombre" class="form-control txbEditable ctrl-required" placeholder="Nombre completo / Empresa" type="text" tabindex="2" runat="server" clientidmode="static" />
                                            </div>
                                            <div class="form-group unselectable">
                                                <input id="txbEmail" class="form-control txbEditable" placeholder="Email" type="text" tabindex="3" runat="server" clientidmode="static" />
                                            </div>
                                            <!--Radio group-->
                                            <div style="display: none;">
                                                <div class="row-short pull-left">
                                                    Tipo de documento: 
                                                <asp:RadioButton ID="radDoc1" runat="server" ClientIDMode="Static" CssClass="radio-inline hand-cursor radio2" Checked="true" Text="&nbsp;CI / DNI" GroupName="radDoc" />
                                                    <asp:RadioButton ID="radDoc2" runat="server" ClientIDMode="Static" CssClass="radio-inline hand-cursor radio2" Text="&nbsp;RUT" GroupName="radDoc" />
                                                </div>
                                                <div class="form-group unselectable">
                                                    <input id="txbDocumento" class="form-control txbEditable ctrl-required" placeholder="Sin puntos ni guiones - Ej: 12345678 / 123456789123" type="number" tabindex="4" runat="server" clientidmode="static" />
                                                </div>
                                            </div>
                                            <!--Radio group-->
                                            <hr />
                                            <label class="_label1 unselectable" style="font-weight: normal;">2) Datos del pedido</label>
                                            <br />

                                            <asp:DropDownList ID="ddlTamano1" runat="server" ClientIDMode="Static" CssClass="dropdown txbEditable ctrl-required form-control" />
                                            <div style="display: none;">
                                                <label style="font-weight: normal;">¿Tiene bosquejo o ejemplo?</label>
                                                <asp:CheckBox ID="chbBosquejo" runat="server" ClientIDMode="Static" CssClass="txbEditable" Enabled="false" TextAlign="Left" />
                                                <a id="aCollapse_bosquejo" data-toggle="collapse" href="#div_bosquejo" class="collapsed">Cargar aquí</a>
                                            </div>
                                            <div id="div_bosquejo" class="col-xs-12 col-sm-12 col-md-12 panel-collapse collapse in" aria-expanded="false">
                                                <div class="form-group unselectable">
                                                    <p style="font-size: small;">Si lo desea cargue un bosquejo hecho a mano del diseño deseado aquí</p>
                                                    <asp:FileUpload ID="MyFileUpload" runat="server" accept="image/*" />
                                                </div>
                                                <hr />
                                            </div>

                                            <br />
                                            <a id="aLinkToUnitegallery" href="UniteGallery.aspx" style="color: darkgreen; display: none;">ELEGIR DISEÑO</a>
                                            <br />

                                            <!--Radio group-->
                                            <div class="row-short pull-left">
                                                Tipo de cartel: 
                                                <asp:RadioButton ID="radImpreso1" runat="server" ClientIDMode="Static" CssClass="radio-inline hand-cursor" Checked="true" Text="&nbsp;IMPRESO" GroupName="radImpreso" />
                                                <asp:RadioButton ID="radImpreso2" runat="server" ClientIDMode="Static" CssClass="radio-inline hand-cursor" Text="&nbsp;PINTADO" GroupName="radImpreso" />
                                            </div>
                                            <!--Radio group-->
                                            <br />
                                            <div class="form-group unselectable" style="display: none;">
                                                <asp:TextBox runat="server" ID="txbTexto1" TextMode="multiline" CssClass="form-control txbEditable multitext" placeholder="Indicaciones del diseño (opcional)"></asp:TextBox>
                                            </div>
                                            <asp:DropDownList ID="ddlTematica" runat="server" ClientIDMode="Static" CssClass="dropdown txbEditable ctrl-required form-control" />

                                            <hr />
                                            <label class="_label1 unselectable" style="font-weight: normal;">3) Datos de la entrega</label>
                                            <br />

                                            <asp:DropDownList ID="ddlTipoEntrega1" runat="server" ClientIDMode="Static" CssClass="dropdown txbEditable ctrl-required ddlBorder"></asp:DropDownList>

                                            <div class="form-group unselectable">
                                                <input id="txbCiudad" class="form-control txbEditable ctrl-required" placeholder="Ciudad de envío" type="text" tabindex="5" runat="server" clientidmode="static" />
                                            </div>
                                            <div class="form-group unselectable">
                                                <input id="txbFecha" type="text" class="form-control txbEditable ctrl-required" placeholder="Día de entrega" tabindex="6" runat="server" clientidmode="static" />
                                            </div>

                                            <div class="form-group" id="dir_group" style="display: none;">
                                                <div class="form-group row" style="margin-left: 0; margin-right: 0;">
                                                    <input id="txbDireccion_calle" class="form-control txbEditable ctrl-required pull-left" placeholder="Calle" type="text" tabindex="7" runat="server" clientidmode="static" style="width: 47%;" />
                                                    <input id="txbDireccion_numero" class="form-control txbEditable pull-right" placeholder="Número de puerta" type="number" tabindex="8" runat="server" clientidmode="static" style="width: 47%;" />
                                                </div>
                                                <div class="form-group row" style="margin-left: 0; margin-right: 0;">
                                                    <input id="txbDireccion_apto" class="form-control txbEditable pull-left" placeholder="Apto" type="text" tabindex="9" runat="server" clientidmode="static" style="width: 47%;" />
                                                    <input id="txbDireccion_esquina" class="form-control txbEditable pull-right" placeholder="Esquina" type="text" tabindex="10" runat="server" clientidmode="static" style="width: 47%;" />
                                                </div>
                                            </div>

                                            <div class="form-group" id="dir_groupX" style="display: none;">
                                                Dirección escrita
                                                <input id="txbDireccion" class="form-control txbEditable ctrl-required" placeholder="Dirección de entrega en texto" type="text" tabindex="11" runat="server" clientidmode="static" />
                                                <hr />
                                            </div>

                                            <hr />
                                            <label class="_label1 unselectable" style="font-weight: normal;">4) Datos del pago</label>
                                            <br />

                                            <div class="form-group" id="pago_group">
                                                <div class="form-group row" style="margin-left: 0; margin-right: 0;">
                                            <asp:DropDownList ID="ddlMedioDePago" runat="server" ClientIDMode="Static" CssClass="dropdown txbEditable ctrl-required ddlMedioDePago" tabindex="11"/>
                                                    <%--<input id="Text1" class="form-control txbEditable ctrl-required pull-left" placeholder="Calle" type="text" tabindex="7" runat="server" clientidmode="static" style="width: 47%;" />--%>
                                                    <input id="txbMonto" class="form-control txbEditable pull-right" placeholder="Monto $" type="number" tabindex="12" runat="server" clientidmode="static" style="width: 47%;" />
                                                </div>
                                            </div>

                                            <div class="form-group unselectable">
                                                <asp:Button ID="btnConfirmar1" runat="server" CssClass="form-control btn btn-danger btnConfirm1" OnClick="btnConfirmar_ServerClick" OnClientClick="return pre_confirm_tab1();" ClientIDMode="Static" Text="GUARDAR" />
                                            </div>
                                            <div class="form-group unselectable">
                                                <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-info btnConfirm3" OnClick="btnLimpiar_ServerClick" ClientIDMode="Static" Text="Limpiar datos" />
                                            </div>

                                            <div class="form-group unselectable" style="display: none;">

                                                <style type="text/css">
                                                    #map-canvas {
                                                        width: 400px;
                                                        height: 400px;
                                                    }                                                   
                                                </style>

                                                <div class="form-group" id="map_group">
                                                    Dirección en el Mapa
                                                    <input type="text" id="mapSearch" class="form-control ctrl-required" placeholder="Dirección de entrega" />
                                                    <div id="map-canvas" style="margin: auto; width: 100%;"></div>
                                                    <p id="mapSearch_msg" style="font-size: small; color: red;">Asegúrese que la ubicación en el mapa sea la correcta por favor</p>
                                                </div>

                                            </div>

                                        </div>
                                        <div class="row">
                                            <h4 id="msj_result" runat="server" style="margin: auto; display: none;"><span class="label label-success">Los datos se guardaron correctamente</span></h4>
                                        </div>
                                        <hr />
                                        <div style="font-size: small" class="unselectable">
                                            <div class="row" style="margin-left: 0; margin-right: 0;">
                                                <span>Última actualización del pedido:</span>
                                                <label id="lblLastUpdate" runat="server">-</label>
                                            </div>
                                            <div class="row" style="margin-left: 0; margin-right: 0;">
                                                <a href="http://cartelux.uy/album" title="" target="_blank" style="font-size: large;">Ver el álbum de fotos</a>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="tab-pane fade in" id="tabsPedidos_2">

                                <%--<div class="row" style="margin: auto; display: inline-flex; width: 100%;">
                                    <div class="col-sm-12 col-md-12" style="margin: auto; padding: 0;">
                                        <div class="login-container sub-form">
                                            <label class="_label1 unselectable">Contacto cliente</label>
                                            <div class="form-group unselectable">
                                                Teléfono
                                                <div class="row-special" style="display: flex;">
                                                    <input class="form-control" id="txbCX_tel" readonly="true" style="width: 90%; display: inline-block; margin-right: 10px;" />
                                                    <a href="#" class="btn btn-warning" style="display: inline-block;" onclick="clientWhatsApp()">
                                                        <i class="fa fa-whatsapp fa-2x"></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class="form-group unselectable">
                                                Google Maps
                                                <div class="row-special" style="display: flex;">
                                                    <input class="form-control" id="txbCX_URL" readonly="true" style="width: 90%; display: inline-block; margin-right: 10px;" />
                                                    <a href="#" class="btn btn-warning" style="display: inline-block;" onclick="clientGoogleMaps()">
                                                        <i class="fa fa-map fa-2x"></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class="form-group unselectable">
                                                Dirección
                                                <div class="row-special" style="display: flex;">
                                                    <input class="form-control" type="text" tabindex="5" runat="server" id="txbCX_dir" clientidmode="static" readonly="true" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>--%>

                                <div class="row" style="margin: auto; display: inline-flex; width: 100%;">

                                    <div class="col-sm-12 col-md-12" style="margin: auto; padding: 0;">
                                        <div class="login-container sub-form">
                                            <label class="_label1 unselectable" style="font-weight: normal;">1) Ingrese sus datos</label>
                                            <div class="form-group unselectable">
                                                <input id="txbTelefono_tab2" class="form-control ctrl-required" type="number" placeholder="Teléfono de contacto" runat="server" clientidmode="static" pattern=".{6,}" title="6 dígitos mínimo" />
                                            </div>
                                            <div class="form-group unselectable">
                                                <input id="txbNombre_tab2" class="form-control txbEditable ctrl-required" placeholder="Nombre completo / Empresa" type="text" runat="server" clientidmode="static" />
                                            </div>
                                            <!--Radio group-->
                                            <div style="display: none;">
                                                <div class="row-short pull-left">
                                                    Tipo de documento: 
                                                <asp:RadioButton ID="radDoc1_tab2" runat="server" ClientIDMode="Static" CssClass="radio-inline hand-cursor radio2" Text="&nbsp;CI/DNI" GroupName="radDoc_tab2" />
                                                    <asp:RadioButton ID="radDoc2_tab2" runat="server" ClientIDMode="Static" CssClass="radio-inline hand-cursor radio2" Text="&nbsp;RUT" GroupName="radDoc_tab2" />
                                                </div>
                                                <div class="form-group unselectable">
                                                    <input id="txbDocumento_tab2" class="form-control txbEditable ctrl-required" placeholder="Documento - Sin puntos ni guiones" type="number" runat="server" clientidmode="static" />
                                                </div>
                                            </div>
                                            <!--Radio group-->
                                            <hr />
                                            <label class="_label1 unselectable" style="font-weight: normal;">2) Datos del roll up</label>
                                            <br />

                                            <%--<asp:DropDownList ID="ddlTipoCartel" runat="server" ClientIDMode="Static" CssClass="dropdown txbEditable ctrl-required" />--%>

                                            <asp:DropDownList ID="ddlTamano1_tab2" runat="server" ClientIDMode="Static" CssClass="dropdown txbEditable ctrl-required form-control" />
                                            <label style="font-weight: normal;">¿Tiene el diseño?</label>
                                            <asp:CheckBox ID="chbBosquejo_tab2" runat="server" ClientIDMode="Static" CssClass="txbEditable" Enabled="false" TextAlign="Left" />
                                            <a id="aCollapse_bosquejo_tab2" data-toggle="collapse" href="#div_bosquejo_tab2" class="collapsed">Cargar aquí</a>
                                            <div id="div_bosquejo_tab2" class="col-xs-12 col-sm-12 col-md-12 panel-collapse collapse in" aria-expanded="false">
                                                <div class="form-group unselectable">
                                                    <p style="font-size: small;">Si lo desea cargue el disñeo a imprimir aquí</p>
                                                    <asp:FileUpload ID="MyFileUpload_tab2" runat="server" accept="image/*" />
                                                </div>
                                                <hr />
                                            </div>

                                            <br />
                                            <br />
                                            <div class="form-group unselectable" style="display: none;">
                                                <asp:TextBox runat="server" ID="txbTexto1_tab2" TextMode="multiline" CssClass="form-control txbEditable multitext" placeholder="Indicaciones del diseño (opcional)"></asp:TextBox>
                                            </div>

                                            <hr />
                                            <label class="_label1 unselectable" style="font-weight: normal;">3) Datos de la entrega</label>
                                            <br />

                                            <asp:DropDownList ID="ddlTipoEntrega1_tab2" runat="server" ClientIDMode="Static" CssClass="dropdown txbEditable ctrl-required ddlBorder"></asp:DropDownList>

                                            <div class="form-group unselectable">
                                                <input id="txbCiudad_tab2" class="form-control txbEditable ctrl-required" placeholder="Ciudad de envío" type="text" runat="server" clientidmode="static" />
                                            </div>
                                            <div class="form-group unselectable">
                                                <input id="txbFecha_tab2" type="text" class="form-control txbEditable ctrl-required" placeholder="Día de entrega" runat="server" clientidmode="static" />
                                            </div>
                                            <hr />

                                            <div class="form-group" id="dir_group_tab2" style="display: none;">
                                                <div class="form-group row" style="margin-left: 0; margin-right: 0;">
                                                    <input id="txbDireccion_calle_tab2" class="form-control txbEditable ctrl-required pull-left" placeholder="Calle" type="text" runat="server" clientidmode="static" style="width: 45%;" />
                                                    <input id="txbDireccion_numero_tab2" class="form-control txbEditable pull-right" placeholder="Número de puerta" type="number" runat="server" clientidmode="static" style="width: 45%;" />
                                                </div>
                                                <div class="form-group row" style="margin-left: 0; margin-right: 0;">
                                                    <input id="txbDireccion_apto_tab2" class="form-control txbEditable pull-left" placeholder="Apto" type="text" runat="server" clientidmode="static" style="width: 45%;" />
                                                    <input id="txbDireccion_esquina_tab2" class="form-control txbEditable pull-right" placeholder="Esquina" type="text" runat="server" clientidmode="static" style="width: 45%;" />
                                                </div>
                                            </div>

                                            <div class="form-group" id="dir_groupX_tab2" style="display: none;">
                                                Dirección escrita
                                                <input id="txbDireccion_tab2" class="form-control txbEditable ctrl-required" placeholder="Dirección de entrega en texto" type="text" runat="server" clientidmode="static" />
                                                <hr />
                                            </div>

                                            <%--<div class="form-group unselectable" style="display: none;">

                                                <style>
                                                    #map-canvas {
                                                        width: 400px;
                                                        height: 400px;
                                                    }
                                                </style>

                                                <div class="form-group" id="map_group_tab2">
                                                    Dirección en el Mapa
                                                    <input type="text" id="mapSearch_tab2" class="form-control ctrl-required" placeholder="Dirección de entrega" />
                                                    <div id="map-canvas_tab2" style="margin: auto; width: 100%;"></div>
                                                    <p id="mapSearch_msg_tab2" style="font-size: small; color: red;">Asegúrese que la ubicación en el mapa sea la correcta por favor</p>
                                                </div>

                                            </div>--%>

                                            <br />
                                            <div class="form-group unselectable">
                                                <asp:Button ID="btnConfirmar1_tab2" runat="server" CssClass="form-control btn btn-danger btnConfirm1" OnClick="btnConfirmar_ServerClick_tab2" OnClientClick="return pre_confirm_tab2();" ClientIDMode="Static" Text="GUARDAR" />
                                            </div>

                                        </div>
                                        <div class="row">
                                            <h4 id="msj_result_tab2" runat="server" style="margin: auto; display: none;"><span class="label label-success">Los datos se guardaron correctamente</span></h4>
                                        </div>
                                        <hr />
                                        <div style="font-size: small" class="unselectable">
                                            <div class="row" style="margin-left: 0; margin-right: 0;">
                                                <span>Última actualización del pedido:</span>
                                                <label id="lblLastUpdate_tab2" runat="server">-</label>
                                            </div>
                                            <div class="row" style="margin-left: 0; margin-right: 0;">
                                                <a href="http://cartelux.uy/album" title="" target="_blank">Click aquí para ver el álbum de fotos</a>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                        </div>
                    </div>

                </div>
            </div>
            <div class="loading" align="center">
                Confirmando pedido, por favor espere.<br />
                <br />
                <img src="/Content/img/loader.gif" alt="Cartelux" />
            </div>
            <div id="dialog" title="Mensaje Cartelux">
                <p style="text-align: left;"></p>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyADf3pOam_5HnhHHawjWJtcGcn7pv39AGA&libraries=places"></script>

    <asp:HiddenField ID="hdnCurrentLocationURL" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="hdnCurrentLAT" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="hdnCurrentLNG" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="hdnPedidoCantidad" runat="server" ClientIDMode="Static" Value="1" />
    <asp:HiddenField ID="hdnIS_TAB_PASACALLE" runat="server" ClientIDMode="Static" Value="1" />
    <asp:HiddenField ID="hdnDisenoSeleccionado" runat="server" ClientIDMode="Static" Value="1" />
</asp:Content>
