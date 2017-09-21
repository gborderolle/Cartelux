<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="Cartelux1.Formulario" Title="Cartelux Publicidad" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- STYLES EXTENSION -->

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/formulario.css" />

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/formulario.js"></script>

    <style type="text/css">
        .dropdown {
            margin-bottom: 10px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <img src="/Content/img/CX-Banner-form.jpg" alt="Cartelux" class="img-responsive" style="width: 100%;" />
    <div class="box box-default" style="margin: 20px;">
        <div class="box-header with-border dark in div-form col-sm-12 col-md-6">

            <div class="row" style="margin: auto; display: block; text-align: center;">
                <h2 style="color: #ea7209;">Ingrese los datos de su pedido <a href="#" id="btnEdit" class="pull-right btn-xs " onclick="editFields()" style="position: absolute; right: 0; margin-right: 20px;">
                    <i class="fa fa-pencil"></i>
                </a></h2>
            </div>

            <br />
            <div class="row" style="margin: auto;">
                <div class="col-sm-12 col-md-6" style="margin: auto;">
                    <div class="login-container sub-form panel panel-default">

                        <label class="label-default">Contacto</label>
                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Su nombre completo" type="text" tabindex="1" required autofocus runat="server" id="txbNombre">
                        </div>
                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Teléfono de contacto" type="tel" tabindex="2" required runat="server" id="txbTel">
                        </div>
                        <hr />
                        <label class="label-default">Entrega (si aplica)</label>
                        <br />
                        <div class="dropdown">
                            <button class="btn btn-sm btn-info dropdown-toggle" type="button" data-toggle="dropdown">
                                Tipo de entrega
  <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu" id="ddlTipoEntrega">
                                <li><a href="javascript:;" onclick="return false;">Colocación</a></li>
                                <li><a href="javascript:;" onclick="return false;">Envío a domicilio</a></li>
                                <li><a href="javascript:;" onclick="return false;">Envío al interior</a></li>
                                <li><a href="javascript:;" onclick="return false;">Retiro en taller</a></li>
                            </ul>
                        </div>

                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Dirección de colocación " type="text" tabindex="4" runat="server" id="txbDireccion">
                        </div>
                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Barrio de colocación " type="text" tabindex="4" runat="server" id="txbBarrio">
                        </div>
                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Fecha y hora de colocación " type="text" tabindex="4" runat="server" id="txbFecha">
                        </div>

                        <hr />
                        <label class="label-default">Cartel</label>

                        <div class="dropdown">
                            <button class="btn btn-sm btn-info dropdown-toggle" type="button" data-toggle="dropdown">
                                Tamaño
  <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu" id="ddlTamano">
                                <li><a href="javascript:;" onclick="return false;">1,5 mts</a></li>
                                <li><a href="javascript:;" onclick="return false;">3 mts</a></li>
                                <li><a href="javascript:;" onclick="return false;">5 mts</a></li>
                                <li><a href="javascript:;" onclick="return false;">Otro</a></li>
                            </ul>
                        </div>

                        <div class="dropdown">
                            <button class="btn btn-sm btn-info dropdown-toggle" type="button" data-toggle="dropdown">
                                Motivo
  <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu" id="ddlMotivo">
                                <li><a href="javascript:;" onclick="return false;">Empresa</a></li>
                                <li><a href="javascript:;" onclick="return false;">15 Años</a></li>
                                <li><a href="javascript:;" onclick="return false;">Cumpleaños</a></li>
                                <li><a href="javascript:;" onclick="return false;">Recibimiento</a></li>
                                <li><a href="javascript:;" onclick="return false;">Evento</a></li>
                                <li><a href="javascript:;" onclick="return false;">Amor</a></li>
                                <li><a href="javascript:;" onclick="return false;">Bienvenida/Despedida</a></li>
                                <li><a href="javascript:;" onclick="return false;">Sindicato</a></li>
                                <li><a href="javascript:;" onclick="return false;">Nacimiento</a></li>
                                <li><a href="javascript:;" onclick="return false;">Otro</a></li>
                            </ul>
                        </div>

                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Texto" type="text" tabindex="5" runat="server" required id="txbTexto">
                        </div>
                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Estilo de diseño" type="text" tabindex="5" runat="server" id="txbDetalles">
                        </div>
                        <div class="form-group">
                            <button class="form-control btn btn-primary" clientidmode="static" name="submit" type="submit" data-submit="...Confirmando" runat="server" id="btnConfirmar" onserverclick="btnConfirmar_ServerClick">Guardar y confirmar</button>
                        </div>
                    </div>
                </div>
                <h4 id="msj_result" runat="server" style="margin: auto; visibility: hidden;"><span class="label label-success">Los datos se guardaron correctamente</span></h4>

            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdn_ddlTipoEntrega" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="hdn_ddlTamano" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="hdn_ddlMotivo" ClientIDMode="Static" runat="server" Value="" />

</asp:Content>
