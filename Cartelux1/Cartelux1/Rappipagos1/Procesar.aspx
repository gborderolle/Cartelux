<%@ Page Language="C#" MasterPageFile="~/SiteExternal.Master" AutoEventWireup="true" CodeBehind="Procesar.aspx.cs" Inherits="Cartelux1.Rappipagos1.Procesar" Title="Rappipagos | Iniciar" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- STYLES EXTENSION -->

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/pages/rappipagos/iniciar.css" />
    <link rel="stylesheet" href="/Content/css/pages/generador.css" />

    <style type="text/css">
        body {
            background-color: #f2e0cf;
        }

        .center {
            display: block;
            margin: auto;
            width: 100%;
        }

        @media screen and (min-width: 768px) {
            .div-form {
                width: 25%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/pages/rappipagos/iniciar.js"></script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12 col-md-12" style="display: inline-block;">
        <div class="box-header with-border dark in div-form" style="display: inline-block;">
            <div class="row center">
                <div style="text-align: center;">
                    <h2 id="lbl_h2" style="color: #ea7209;">Iniciar procesamiento
                    </h2>
                </div>
            </div>
            <br />
            <div class="row center">
                <div class="col-sm-12 col-md-10 center">
                    <div class="login-container sub-form">
                        <div class="form-group">
                            <%--<input class="form-control label-primary label-lg" style="height: auto;" value="Cargar factura" title="Click para cargar factura" />--%>
                            <h3 style="margin: 10px;"><span class="label label-primary">Cargar factura</span></h3>
                            <asp:FileUpload runat="server" CssClass="form-control" ID="btnUpload" ClientIDMode="Static" />
                            <%--<asp:Button ID="btnUpload" runat="server" Text="Cargar factura" CssClass="form-control btn-info btn btn-lg" OnClick="btnUpload_Click" UseSubmitBehavior="false" ClientIDMode="Static" CausesValidation="false" />--%>

                        </div>
                    </div>
                </div>
            </div>

            <hr style="margin: 0;" />
            <div class="row center">
                <div class="col-sm-12 col-md-10 center">
                    <h3 style="margin: 10px;"><span class="label label-primary">Resultado</span></h3>
                    <div style="display: flex;">
                        <input class="form-control text-to-copy pull-left" id="txbLink" onclick="this.select();" value="?" style="width: 85%;" readonly />

                        <a id="aBtnCopy" href="#" class="btn btn-lg" data-clipboard-action="copy" data-clipboard-target="input#txbLink" style="padding: 8px;" title="Copiar Formulario">
                            <i class="glyphicon glyphicon-copy"></i>
                        </a>
                        <a id="aBtnGoToURL" href="#" class="btn btn-lg pull-right" style="padding: 8px;" title="Ir a Formulario">
                            <i class="glyphicon glyphicon-flash"></i>
                        </a>
                    </div>
                    <%--<button type="button" class="form-control btn-warning js-copy-btn btn-lg" style="height: auto;" id="btnProcess" onclick="btnProcess_Click">Procesar factura</button>--%>
                    <asp:Button ID="btnProcess" runat="server" Text="Procesar factura" CssClass="form-control btn-warning btn btn-lg" OnClick="btnProcess_Click" UseSubmitBehavior="false" ClientIDMode="Static" CausesValidation="false" />

                </div>
            </div>
            <br />
            <br />
            <br />
        </div>
    </div>
    <script type="text/javascript">

    </script>
</asp:Content>
