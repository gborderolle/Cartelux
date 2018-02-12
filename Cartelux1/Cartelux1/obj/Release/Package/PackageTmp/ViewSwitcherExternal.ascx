<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSwitcherExternal.ascx.cs" Inherits="Cartelux1.ViewSwitcherExternal" %>
<div id="viewSwitcher">
    <%: CurrentView %> view | <a href="<%: SwitchUrl %>" data-ajax="false">Switch to <%: AlternateView %></a>
</div>