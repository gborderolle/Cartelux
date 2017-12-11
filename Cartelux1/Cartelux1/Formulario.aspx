<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="Cartelux1.Formulario" Title="Cartelux Publicidad" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- STYLES EXTENSION -->
    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/formulario.css" />
    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/formulario.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".dropdown").selectmenu();
        });

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

    </script>
    <style type="text/css">
                body {
            background-color: #f2e0cf;
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

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server"></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<img src="/Content/img/CX-Banner-form.jpg" alt="Cartelux" class="img-responsive" style="width: 100%;" />--%>
    <div class="box box-default">
        <div class="box-header with-border dark in div-form col-sm-12 col-md-6">
            <div class="row" style="margin: auto; display: block; text-align: center;">
                <h2 style="color: #ea7209;">Ingrese los datos de su pedido <a href="#" id="btnEdit" class="pull-right btn " onclick="editFields()" style="position: absolute; right: 0; margin-right: 20px;">
                    <i class="fa fa-pencil"></i>
                </a>
                </h2>
            </div>
            <br />
            <div class="row" style="margin: auto;">
                <div class="col-sm-12 col-md-8" style="margin: auto; padding: 0;">
                    <div class="login-container sub-form panel panel-default">
                        <label class="label-default">1) Ingrese sus datos </label>
                        <div class="form-group">
                            Teléfono de contacto
                            <input class="form-control ctrl-required" type="number" tabindex="1" required runat="server" id="txbTel" clientidmode="static" pattern=".{6,}" title="6 dígitos mínimo" autofocus>
                        </div>
                        <div class="form-group">
                            <input class="form-control txbEditable ctrl-required" placeholder="Nombre completo" type="text" tabindex="2" required runat="server" id="txbNombre" clientidmode="static">
                        </div>
                        <hr />
                        <label class="label-default">2) Datos del cartel</label>
                        <br />
                        <select name="ddlTamano" id="ddlTamano" class="dropdown txbEditable" runat="server" clientidmode="static">
                            <option disabled selected>Tamaño</option>
                            <option>1,5 mts</option>
                            <option>3 mts</option>
                            <option>5 mts</option>
                            <option>Otro</option>
                        </select>
                        <%--<select name="ddlMotivo" id="ddlMotivo" class="dropdown txbEditable" runat="server" clientidmode="static">
                            <option disabled selected>Temática</option>
                            <option>15 Años</option>
                            <option>Cumpleaños</option>
                            <option>Recibimiento</option>
                            <option>Romántico</option>
                            <option>Bienvenida/Despedida</option>
                            <option>Nacimiento</option>
                            <option>Evento</option>
                            <option>Empresa</option>
                            <option>Sindicato</option>
                            <option>Otro</option>
                        </select>--%>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="txbTexto1" TextMode="multiline" CssClass="form-control txbEditable ctrl-required multitext" placeholder="Texto del cartel" TabIndex="3" required="required"></asp:TextBox>
                        </div>
                        <%--<div class="form-group">
                            <input class="form-control txbEditable" placeholder="Sugerencias de diseño" type="text" tabindex="4" runat="server" id="txbDetalles" clientidmode="static">
                        </div>--%>
                        <hr />
                        <label class="label-default">3) Datos de la entrega</label>
                        <br />
                        <select name="ddlTipoEntrega" id="ddlTipoEntrega" class="dropdown txbEditable" runat="server" clientidmode="static">
                            <option disabled selected>Tipo de entrega</option>
                            <option id="colocacion">Colocación</option>
                            <option id="envio">Envío a domicilio</option>
                            <option id="interior">Envío al interior</option>
                            <option id="taller">Retiro en taller</option>
                        </select>
                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Dirección de colocación" type="text" tabindex="5" runat="server" id="txbDireccion" clientidmode="static">
                        </div>
                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Localidad de envío (Interior)" type="text" tabindex="6" runat="server" id="txbCiudad" clientidmode="static">
                        </div>
                        <%--<select name="ddlLugarEntrega" id="ddlLugarEntrega" class="dropdown txbEditable" runat="server" clientidmode="static">
                            <option disabled selected>Lugar de colocación / entrega</option>
                            <option disabled style="font-weight: bold">MONTEVIDEO</option>
                            <option value="Aguada">Aguada</option>
                            <option value="Aires puros">Aires puros</option>
                            <option value="Arroyo seco">Arroyo seco</option>
                            <option value="Atahualpa">Atahualpa</option>
                            <option value="Barrio sur">Barrio sur</option>
                            <option value="Bella vista">Bella vista</option>
                            <option value="Belvedere">Belvedere</option>
                            <option value="Bolivar">Bolivar</option>
                            <option value="Brazo oriental">Brazo oriental</option>
                            <option value="Buceo">Buceo</option>
                            <option value="Capurro">Capurro</option>
                            <option value="Carrasco">Carrasco</option>
                            <option value="Carrasco norte">Carrasco norte</option>
                            <option value="Centro">Centro</option>
                            <option value="Cerrito de la victoria">Cerrito de la victoria</option>
                            <option value="Ciudad vieja">Ciudad vieja</option>
                            <option value="Colón">Colón</option>
                            <option value="Conciliación">Conciliación</option>
                            <option value="Cordón">Cordón</option>
                            <option value="Flor de maroñas">Flor de maroñas</option>
                            <option value="Goes">Goes</option>
                            <option value="Ituzaingó">Ituzaingó</option>
                            <option value="Jacinto vera">Jacinto vera</option>
                            <option value="Jardines del Hipódromo">Jardines del Hipódromo</option>
                            <option value="La blanqueada">La blanqueada</option>
                            <option value="La comercial">La comercial</option>
                            <option value="La figurita">La figurita</option>
                            <option value="La teja">La teja</option>
                            <option value="Larrañaga">Larrañaga</option>
                            <option value="Las acacias">Las acacias</option>
                            <option value="Lezica">Lezica</option>
                            <option value="Malvín">Malvín</option>
                            <option value="Malvín norte">Malvín norte</option>
                            <option value="Manga">Manga</option>
                            <option value="Maroñas">Maroñas</option>
                            <option value="Mercado modelo">Mercado modelo</option>
                            <option value="Nuevo parís">Nuevo parís</option>
                            <option value="Pajas blancas">Pajas blancas</option>
                            <option value="Palermo">Palermo</option>
                            <option value="Parque batlle">Parque batlle</option>
                            <option value="Parque guaraní">Parque guaraní</option>
                            <option value="Parque rodó">Parque rodó</option>
                            <option value="Paso de la arena">Paso de la arena</option>
                            <option value="Peñarol">Peñarol</option>
                            <option value="Piedras blancas">Piedras blancas</option>
                            <option value="Pocitos">Pocitos</option>
                            <option value="Prado">Prado</option>
                            <option value="Punta carretas">Punta carretas</option>
                            <option value="Reducto">Reducto</option>
                            <option value="Retiro">Retiro</option>
                            <option value="Santiago vázquez">Santiago vázquez</option>
                            <option value="Sayago">Sayago</option>
                            <option value="Tres cruces">Tres cruces</option>
                            <option value="Tres ombúes">Tres ombúes</option>
                            <option value="Unión">Unión</option>
                            <option value="Villa Muñoz">Villa Muñoz</option>
                            <option value="Villa del cerro">Villa del cerro</option>
                            <option value="Villa dolores">Villa dolores</option>
                            <option value="Villa española">Villa española</option>
                            <option disabled style="font-weight: bold">CANELONES</option>
                            <option value="Atlántida">Atlántida</option>
                            <option value="Barros blancos">Barros blancos</option>
                            <option value="Canelones">Canelones</option>
                            <option value="Ciudad de la costa">Ciudad de la costa</option>
                            <option value="Colonia nicolich">Colonia nicolich</option>
                            <option value="Joaquín Suarez">Joaquín Suarez</option>
                            <option value="La floresta">La floresta</option>
                            <option value="La paz">La paz</option>
                            <option value="Las piedras">Las piedras</option>
                            <option value="Migues">Migues</option>
                            <option value="Montes">Montes</option>
                            <option value="Pando">Pando</option>
                            <option value="Parque del plata">Parque del plata</option>
                            <option value="Paso carrasco">Paso carrasco</option>
                            <option value="Progreso">Progreso</option>
                            <option value="Salinas">Salinas</option>
                            <option value="San jacinto">San jacinto</option>
                            <option value="Santa lucía">Santa lucía</option>
                            <option value="Santa rosa">Santa rosa</option>
                            <option value="Sauce">Sauce</option>
                            <option value="Soca">Soca</option>
                            <option value="Tala">Tala</option>
                            <option value="Toledo">Toledo</option>
                            <option disabled style="font-weight: bold">SAN JOSÉ</option>
                            <option value="Ciudad del plata">Ciudad del plata</option>
                            <option value="Elcida paullier">Elcida paullier</option>
                            <option value="Libertad">Libertad</option>
                            <option value="San josé">San josé</option>
                            <option disabled style="font-weight: bold">OTRO</option>
                            <option value="Otro">Otro</option>
                        </select>--%>
                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Día de colocación o envío sugerido" type="text" tabindex="7" runat="server" id="txbFecha" clientidmode="static">
                        </div>                        
                        <div class="form-group">
                            <button class="form-control btn btn-danger" clientidmode="static" name="submit" type="submit" data-submit="...Confirmando" runat="server" id="btnConfirmar" onserverclick="btnConfirmar_ServerClick">GUARDAR</button>
                            <%--onclick="ShowProgress();"--%>
                        </div>
                    </div>
                    <div class="row">
                        <h4 id="msj_result" runat="server" style="margin: auto; display: none;"><span class="label label-success">Los datos se guardaron correctamente</span></h4>
                    </div>
                    <div style="font-size: x-small">
                        <div class="row" style="margin-left:0; margin-right:0;">
                            <span>Última actualización del pedido:</span>
                            <label id="lblLastUpdate" runat="server">-</label>
                        </div>
                        <div class="row" style="margin-left:0; margin-right:0;">
                            <%--<img src="/Content/img/Dropbox_logo.png" class="img-responsive" alt="Cartelux" style="width: 5%;" />--%>
                            <a href="https://goo.gl/wk49L6" title="" target="_blank">Click aquí para ver el álbum de fotos</a>
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
</asp:Content>
