<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Clientes.ascx.cs" Inherits="Cartelux1.User_Controls.Clientes" %>
<h2>Lista de Clientes</h2>

<asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
<asp:GridView ID="gridClientes1" runat="server" ClientIDMode="Static" HorizontalAlign="Center" AutoGenerateColumns="False" ShowFooter="True"
    CssClass="table table-hover table-striped"
    DataKeyNames="Cliente_ID"
    OnRowCommand="gridClientes_RowCommand"
    OnRowDataBound="gridClientes_RowDataBound">

    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
    <EmptyDataTemplate>
        ¡No hay clientes con los parámetros seleccionados!  
    </EmptyDataTemplate>

    <%--Paginador...--%>
    <PagerTemplate>
        <div class="row" style="margin-top: 20px;">
            <div class="col-lg-1" style="text-align: right;">
                <h5>
                    <asp:Label ID="MessageLabel" Text="Ir a la pág." runat="server" /></h5>
            </div>
            <div class="col-lg-1" style="text-align: left;">
                <asp:DropDownList ID="PageDropDownList" Width="50px" AutoPostBack="true" OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged" runat="server" CssClass="form-control" /></h3>
            </div>
            <div class="col-lg-10" style="text-align: right;">
                <h3>
                    <asp:Label ID="CurrentPageLabel" runat="server" CssClass="label label-warning" /></h3>
            </div>
        </div>
    </PagerTemplate>

    <Columns>
        <asp:BoundField DataField="Cliente_ID" HeaderText="ID" HtmlEncode="false" ReadOnly="true" ItemStyle-CssClass="hiddencol hiddencol_real" HeaderStyle-CssClass="hiddencol hiddencol_real" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" HtmlEncode="false" ReadOnly="true" />
        <asp:BoundField DataField="Telefono" HeaderText="Telefono" HtmlEncode="false" ReadOnly="true" />
        <asp:BoundField DataField="Email" HeaderText="Email" HtmlEncode="false" ReadOnly="true" />
        <asp:BoundField DataField="Comentarios" HeaderText="Comentarios" HtmlEncode="false" ReadOnly="true" />
    </Columns>

</asp:GridView>
<asp:HiddenField ClientIDMode="Static" ID="hdnClientesCount" runat="server" />

