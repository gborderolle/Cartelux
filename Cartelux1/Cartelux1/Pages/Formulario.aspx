<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="Cartelux1.Pages.Formulario" Title="Cartelux Publicidad" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- STYLES EXTENSION -->

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/formulario.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <img src="/Content/img/CX-Banner-form.png" alt="Cartelux" class="img-responsive" style="width: 100%;" />
    <div class="box box-default" style="margin: 20px;">
        <div class="box-header with-border dark in div-form col-sm-12 col-md-6">

             <div class="row" style="margin: 0; display: block;">
                <div style="text-align: center;">
                    <h2 style="color: #ea7209;">Ingrese los datos de su pedido</h2>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-12 col-md-6" style="margin: auto;">
                    <div class="login-container sub-form panel panel-default">

                        <div class="form-group">
                            <input class="form-control" placeholder="Nombre completo" type="text" tabindex="1" required autofocus runat="server" id="txbNombre">
                        </div>
                        <div class="form-group">
                            <input class="form-control" placeholder="Teléfono de contacto" type="tel" tabindex="2" required runat="server" id="txbTel">
                        </div>
                        <div class="form-group">
                            <input class="form-control" placeholder="Edad" type="text" tabindex="3" runat="server" id="txbEdad">
                        </div>
                        <div class="form-group">
                            <input class="form-control" placeholder="Motivo" type="text" tabindex="4" runat="server" id="txbMotivo">
                        </div>
                        <div class="form-group">
                            <input class="form-control" placeholder="Detalles de diseño" type="text" tabindex="5" runat="server" id="txbDetalles">
                        </div>
                        <div class="form-group">
                            <button class="form-control btn btn-primary" name="submit" type="submit" data-submit="...Confirmando" runat="server" id="btnConfirmar" onserverclick="btnConfirmar_ServerClick">Confirmar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
