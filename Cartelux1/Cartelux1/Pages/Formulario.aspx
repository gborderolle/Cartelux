﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="Cartelux1.Pages.Formulario" Title="Formulario" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- STYLES EXTENSION -->

    <!-- PAGE CSS -->
    <%--<link rel="stylesheet" href="/Content/css/formulario.css" />--%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="box box-default" style="margin:20px;">
        <div class="box-header with-border dark in" style="margin: auto; width: 50%; border: 4px solid #F70; padding: 10px;">

            <div class="row">
                <div class="col-md-9">
                    <h2 style="color:#ea7209;">Por favor, ingrese los datos de su pedido</h2>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6" style="margin: auto;">
            <div class="login-container sub-form panel panel-default">

            <div class="form-group">
                <input class="form-control" placeholder="Nombre completo" type="text" tabindex="1" required autofocus>
            </div>
            <div class="form-group">
                <input class="form-control" placeholder="Edad" type="email" tabindex="2" required>
            </div>
            <div class="form-group">
                <input class="form-control" placeholder="Teléfono" type="tel" tabindex="3" required>
            </div>
            <div class="form-group">
                <input class="form-control" placeholder="Motivo" type="url" tabindex="4" required>
            </div>
            <div class="form-group">
                <textarea class="form-control" placeholder="Detalles de diseño" tabindex="5" required></textarea>
            </div>
            <div class="form-group">
                <button class="form-control btn btn-primary" name="submit" type="submit" id="contact-submit" data-submit="...Sending">Confirmar</button>
            </div>
    </div>
    </div>
    </div>
        </div>
        </div>

</asp:Content>
