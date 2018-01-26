<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="Cartelux1.Formulario" Title="Cartelux Publicidad" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- STYLES EXTENSION -->
    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/formulario.css" />
    <link rel="stylesheet" href="/Content/css/jquery-ui.css" />

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/formulario.js"></script>
    <script type="text/javascript">

        TAB_COUNT = 1;

        $(function () {
            $(".dropdown").selectmenu();
        });

        function clientWhatsApp() {
            var tel = _TEL;
            if (tel != null && tel.length > 0) {

                // Si el número empieza con 0 lo borra
                var first = tel.charAt(0);
                if (first === "0") {
                    tel = tel.substring(1);
                }
                document.location = "https://api.whatsapp.com/send?phone=598" + tel;
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

        ._label {
            font-size: x-large;
            color: #ea7209;
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

        .multitext {
            height: 100px;
        }

        .login-container {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server"></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12 col-md-12" style="padding: 0;">
        <div class="box-header with-border dark in div-form col-sm-12 col-md-4" style="display: inline-block;">
            <div class="row" style="margin: auto; display: block; text-align: center; margin-bottom: 0;">
                <h2 style="color: #ea7209;"><a href="#" id="btnEdit" class="pull-left btn " onclick="editFields()" style="position: absolute; left: 0; margin-left: 20px;">
                    <i class="fa fa-pencil fa-2x"></i>
                </a>Datos de su pedido 
                </h2>
                <div style="position: absolute; right: 0;">
                    Carteles iguales:
                    <label class="_label" id="lblTabCount">1</label>
                    <br />
                    <a class="btn btn-sm btn-info" onclick="addCartel();">+</a>
                    <a class="btn btn-sm btn-info" onclick="removeCartel();">-</a>
                </div>
            </div>
            <br />

            <div id="tabPedidos">

                <div class="panel with-nav-tabs panel-danger">
                    <div class="panel-heading" style="padding-bottom: 0;">

                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tabsPedidos_1" id="aTabsPedidos_1" data-toggle="tab">Pedido</a></li>
                            <li class=""><a href="#tabsPedidos_2" id="aTabsPedidos_2" data-toggle="tab">Cartelux</a></li>
                        </ul>

                    </div>
                    <div class="panel-body">
                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="tabsPedidos_1">

                                <div class="row" style="margin: auto; display: inline-flex; width: 100%;">
                                    <div class="col-sm-12 col-md-10" style="margin: auto; padding: 0;">
                                        <div class="login-container sub-form">
                                            <label class="_label">1) Ingrese sus datos</label>
                                            <div class="form-group">
                                                <input class="form-control ctrl-required" type="number" tabindex="99" placeholder="Teléfono de contacto" required runat="server" id="txbTel" clientidmode="static" pattern=".{6,}" title="6 dígitos mínimo" />
                                            </div>
                                            <div class="form-group">
                                                <input class="form-control txbEditable ctrl-required" placeholder="Nombre completo" type="text" tabindex="1" required runat="server" id="txbNombre" clientidmode="static" autofocus />
                                            </div>
                                            <hr />
                                            <label class="_label">2) Datos del cartel</label>
                                            <br />
                                            <select name="ddlTamano" id="ddlTamano" class="dropdown txbEditable" runat="server" clientidmode="static">
                                                <option disabled selected>Tamaño</option>
                                                <option>1,5 mts</option>
                                                <option>3 mts</option>
                                                <option>5 mts</option>
                                                <option>Otro</option>
                                            </select>

                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txbTexto1" TextMode="multiline" CssClass="form-control txbEditable ctrl-required multitext" placeholder="Texto del cartel" TabIndex="3" required="required"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <p style="font-size: small;">Si lo desea cargue un bosquejo hecho a mano del diseño deseado aquí</p>
                                                <%--<input id="MyFileUpload" type="file" runat="server" class="file" style="width: 85%; margin: auto; margin-top: 8px; height: 24px;" accept=".jpg,.jepg,.png,.pdf"/>--%>
                                                <asp:FileUpload ID="MyFileUpload" runat="server" accept="image/*" />

                                            </div>


                                            <hr />
                                            <label class="_label">3) Datos de la entrega</label>
                                            <br />
                                            <select name="ddlTipoEntrega" id="ddlTipoEntrega" class="dropdown txbEditable" runat="server" clientidmode="static">
                                                <option disabled selected>Tipo de entrega</option>
                                                <option id="colocacion">Colocación</option>
                                                <option id="envio">Envío a domicilio</option>
                                                <option id="interior">Envío al interior</option>
                                                <option id="taller">Retiro en taller</option>
                                            </select>
                                            <div class="form-group">
                                                <input class="form-control txbEditable" placeholder="Dirección de colocación" type="text" tabindex="5" runat="server" id="txbDireccion" clientidmode="static" />
                                            </div>
                                            <div class="form-group">
                                                <input class="form-control txbEditable" placeholder="Localidad de envío (Interior)" type="text" tabindex="6" runat="server" id="txbCiudad" clientidmode="static" />
                                            </div>

                                            <div class="form-group">
                                                <input class="form-control txbEditable" placeholder="Día de colocación o envío sugerido" type="text" tabindex="7" runat="server" id="txbFecha" clientidmode="static" />
                                            </div>
                                            <div class="form-group">
                                                <button class="form-control btn btn-danger" clientidmode="static" name="Submit" type="submit" data-submit="...Confirmando" runat="server" id="btnConfirmar" onserverclick="btnConfirmar_ServerClick">GUARDAR</button>
                                                <%--onclick="ShowProgress();"--%>
                                            </div>

                                            <div class="form-group">

                                                <style>
                                                    #map-canvas {
                                                        width: 400px;
                                                        height: 400px;
                                                    }
                                                </style>

                                                <input type="text" id="mapSearch" size="50"/>
                                                <div id="map-canvas"></div>

                                                <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA5b9fHo2L4fPpJZhRehtJGUjdXfgPkbUE&libraries=places"></script>

                                            </div>


                                        </div>
                                        <div class="row">
                                            <h4 id="msj_result" runat="server" style="margin: auto; display: none;"><span class="label label-success">Los datos se guardaron correctamente</span></h4>
                                        </div>
                                        <hr />
                                        <div style="font-size: small">
                                            <div class="row" style="margin-left: 0; margin-right: 0;">
                                                <span>Última actualización del pedido:</span>
                                                <label id="lblLastUpdate" runat="server">-</label>
                                            </div>
                                            <div class="row" style="margin-left: 0; margin-right: 0;">
                                                <%--<img src="/Content/img/Dropbox_logo.png" class="img-responsive" alt="Cartelux" style="width: 5%;" />--%>
                                                <a href="https://goo.gl/as2weV" title="" target="_blank">Click aquí para ver el álbum de fotos</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="tab-pane fade in" id="tabsPedidos_2">

                                <div class="row" style="margin: auto; display: inline-flex; width: 100%;">
                                    <div class="col-sm-12 col-md-10" style="margin: auto; padding: 0;">
                                        <div class="login-container sub-form">
                                            <label class="_label">Contacto cliente</label>
                                            <div class="form-group">
                                                Teléfono
                                                <div class="row-special" style="display: flex;">
                                                    <input class="form-control" id="txbCX_tel" readonly="true" style="width: 90%;" />
                                                    <a href="#" class="pull-right btn" onclick="clientWhatsApp()">
                                                        <i class="fa fa-whatsapp fa-2x"></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                Dirección
                                                <div class="row-special" style="display: flex;">
                                                    <input class="form-control" type="text" tabindex="5" runat="server" id="txbCX_dir" clientidmode="static" readonly="true" />
                                                </div>
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

    <asp:HiddenField ID="hdnPedidoCantidad" runat="server" ClientIDMode="Static" Value="1" />
</asp:Content>
