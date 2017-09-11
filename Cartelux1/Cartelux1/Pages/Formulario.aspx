<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="Cartelux1.Pages.Formulario" Title="Formulario" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- STYLES EXTENSION -->

    <!-- PAGE CSS -->
    <%--<link rel="stylesheet" href="/Content/css/formulario.css" />--%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="box box-default">
        <div class="box-header with-border dark in" style="margin: auto; width: 50%; border: 3px solid green; padding: 10px;">

            <div class="row">
                <div class="col-md-9 modal-header" style="">
                    <h2 style=";">Por favor, ingrese los datos de su pedido</h2>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6" style="">
            <div class="login-container sub-form panel panel-default">

            <div class="form-group">
                <input class="form-control" placeholder="Your name" type="text" tabindex="1" required autofocus>
            </div>
            <div class="form-group">
                <input class="form-control" placeholder="Your Email Address" type="email" tabindex="2" required>
            </div>
            <div class="form-group">
                <input class="form-control" placeholder="Your Phone Number (optional)" type="tel" tabindex="3" required>
            </div>
            <div class="form-group">
                <input class="form-control" placeholder="Your Web Site (optional)" type="url" tabindex="4" required>
            </div>
            <div class="form-group">
                <textarea class="form-control" placeholder="Type your message here...." tabindex="5" required></textarea>
            </div>
            <div class="form-group">
                <button class="form-control btn btn-primary" name="submit" type="submit" id="contact-submit" data-submit="...Sending">Submit</button>
            </div>
    </div>
    </div>
    </div>
        </div>
        </div>

</asp:Content>
