<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="Cartelux1.Formulario" Title="Cartelux Publicidad" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- STYLES EXTENSION -->

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/formulario.css" />

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/formulario.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <img src="/Content/img/CX-Banner-form.png" alt="Cartelux" class="img-responsive" style="width: 100%;" />
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
                        <span class="label label-primary">Tipo de entrega</span>
                        <div id="rad_entrega">
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_entrega1" type="radio" name="entrega" value="Colocación" runat="server" checked>Colocación</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_entrega2" type="radio" name="entrega" value="Entrega" runat="server">Envío a domicilio</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_entrega3" type="radio" name="entrega" value="Interior" runat="server">Envío al interior</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_entrega4" type="radio" name="entrega" value="Taller" runat="server">Retiro en taller</label>
                            </div>
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

                        <div id="rad_motivo">
                            Motivo
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_motivo1" type="radio" name="motivo" value="EMP" runat="server" checked>Empresa</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_motivo2" type="radio" name="motivo" value="15" runat="server">15 Años</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_motivo3" type="radio" name="motivo" value="CUM" runat="server">Cumpleaños</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_motivo4" type="radio" name="motivo" value="REC" runat="server">Recibimiento</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_motivo5" type="radio" name="motivo" value="EVT" runat="server">Evento</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_motivo6" type="radio" name="motivo" value="AMR" runat="server">Amor</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_motivo7" type="radio" name="motivo" value="BDE" runat="server">Bienvenida/Despedida</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_motivo8" type="radio" name="motivo" value="SIN" runat="server">Sindicato</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_motivo9" type="radio" name="motivo" value="NAC" runat="server">Nacimiento</label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <input id="rad_motivo10" type="radio" name="motivo" value="OTR" runat="server">Otro</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Texto" type="text" tabindex="5" runat="server" required id="txbTexto">
                        </div>
                        <div class="form-group">
                            <input class="form-control txbEditable" placeholder="Detalles de diseño" type="text" tabindex="5" runat="server" id="txbDetalles">
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

</asp:Content>
