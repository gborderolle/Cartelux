<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pedidos.ascx.cs" Inherits="Cartelux1.User_Controls.Pedidos" %>
<h2>Lista de Pedidos de <asp:Label ID="lblName" runat="server" Text="" ForeColor="Orange"></asp:Label> | Tel: <asp:Label ID="lblTel" runat="server" Text=""></asp:Label></h2>

<asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
<asp:GridView ID="gridPedidos" runat="server" ClientIDMode="Static" HorizontalAlign="Center" AutoGenerateColumns="False" ShowFooter="True"
    CssClass="table table-hover table-striped">

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
        <asp:BoundField DataField="lblNumero" HeaderText="#" />
        <asp:BoundField DataField="lblFechaEntrega" HeaderText="Fecha Entrega" />
        <asp:BoundField DataField="lblTipoPedido" HeaderText="Tipo Pedido" />
        <asp:BoundField DataField="lblTematica" HeaderText="Temática" />
        <asp:BoundField DataField="lblTamano" HeaderText="Tamaño" />
        <asp:BoundField DataField="lblTipoEntrega" HeaderText="Tipo Entrega"/>
        <asp:BoundField DataField="lblComentarios" HeaderText="Comentarios"/>
    </Columns>

</asp:GridView>
<asp:HiddenField ClientIDMode="Static" ID="hdnPedidosCount" runat="server" />

