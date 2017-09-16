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

            <div class="row" style="margin: 0; display: block; text-align: center;">
                <h2 style="color: #ea7209;">Ingrese los datos de su pedido</h2>
                <a href="#" id="btnEdit" class="pull-right btn-xs glyphicon glyphicon-pencil" onclick="editFields()" >
                    <i class="fa fa-search"></i>
                    </a>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-12 col-md-6" style="margin: auto;">
                    <div class="login-container sub-form panel panel-default">

                        <label class="label-default">Contacto</label>
                        <div class="form-group">
                            <input class="form-control btnEditable" placeholder="Su nombre completo" type="text" tabindex="1" required autofocus runat="server" id="txbNombre">
                        </div>
                        <div class="form-group">
                            <input class="form-control btnEditable" placeholder="Teléfono de contacto" type="tel" tabindex="2" required runat="server" id="txbTel">
                        </div>
                        <div class="form-group">
                            <input class="form-control btnEditable" placeholder="Edad" type="text" tabindex="3" runat="server" id="txbEdad">
                        </div>
                        <div class="form-group">
                            <input class="form-control btnEditable" placeholder="Motivo" type="text" tabindex="4" runat="server" id="txbMotivo">
                        </div>
                        <hr />
                        <label class="label-default">Entrega (si aplica)</label>
                        <div class="form-group">
                            <input class="form-control btnEditable" placeholder="Dirección de colocación " type="text" tabindex="4" runat="server" id="Text1">
                        </div>
                        <div class="form-group">
                            <input class="form-control btnEditable" placeholder="Barrio de colocación " type="text" tabindex="4" runat="server" id="Text2">
                        </div>
                        <div class="form-group">
                            <input class="form-control btnEditable" placeholder="Fecha y hora de colocación " type="text" tabindex="4" runat="server" id="Text3">
                        </div>
                        
                        <hr />
                        <label class="label-default">Cartel</label>
                        <div class="form-group">
                            <input class="form-control btnEditable" placeholder="Texto" type="text" tabindex="5" runat="server" id="txbTexto">
                        </div>
                        <div class="form-group">
                            <input class="form-control btnEditable" placeholder="Detalles de diseño" type="text" tabindex="5" runat="server" id="txbDetalles">
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
