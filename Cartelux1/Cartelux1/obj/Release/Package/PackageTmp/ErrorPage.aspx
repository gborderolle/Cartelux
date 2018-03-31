<%@ Page Title="Error" Language="C#" MasterPageFile="~/SiteExternal.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Cartelux1.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>¡Uhh!</h2>
    <h3>Ocurrió un error en el sistema. Vuelve a intentarlo.</h3>

    <asp:Button ID="backButton" runat="server" Text="Volver" CssClass="btn btn-info pull-left" OnClientClick="JavaScript:window.history.back(1);return false;" />
    <br>
</asp:Content>


