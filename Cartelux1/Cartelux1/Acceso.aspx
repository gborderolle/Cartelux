<%@ Page Language="C#" MasterPageFile="~/SiteExternal.Master" AutoEventWireup="true" CodeBehind="Acceso.aspx.cs" Inherits="Cartelux1.Acceso" Title="Cartelux Publicidad" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- STYLES EXTENSION -->
    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/login-styles.css" />

    <link rel="stylesheet" href="/Content/css/login.css" />
    <link rel="stylesheet" href="/Content/css/jquery-ui.css" />

    <style type="text/css">
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/login.js"></script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="generalContainer">

        <div class="loginFormContainer col-md-4 col-sm-10 col-xs-12" style="margin: auto; float: inherit;">
            <a data-toggle="collapse" href="#collapse1">Acceder</a>
            <div id="collapse1" class="panel-collapse collapse">
                <div class="loginFormElements">

                    <div class="loginTitleArea unselectable">
                        <img class="loginTitleImage pull-left img-responsive img-circle" src="/Content/img/Cartelux1.ico" />
                        <div class="loginTitleBread">Cartelux Administrativo</div>
                        <div class="loginTitleText">Sistema de autenticación</div>
                    </div>

                    <div class="loginFormContent" style="margin-bottom: 0;">

                        <div class="form-group">
                            Usuario:
                <input type="text" id="txbUser1" runat="server" placeholder="Usuario" class="txbUser form-control" style="padding: 25px; max-width: initial;" />
                        </div>
                        <div class="form-group">
                            Contraseña:
                <input type="password" id="txbPassword1" runat="server" placeholder="Contraseña" class="txbPassword form-control" style="padding: 25px; max-width: initial;" />
                        </div>

                    </div>
                    <div class="loginFormButtonContainer" style="width: 100%;">
                        <button type="button" id="btnSubmit" class="btn btn-primary btn-lg" onclick="checkSubmit();" style="margin: auto; width: -webkit-fill-available;">
                            <i class="fa fa-check"></i>&nbsp;Ingresar
                        </button>
                        <input type="submit" id="btnSubmit_candidato1" runat="server" onserverclick="btnSubmit_candidato1_ServerClick"
                            style="display: none;" class="btnSubmit_candidato" />

                        <div class="loginFormMessageContainer" style="box-sizing: inherit; width: 100%; padding: 0;">
                            <div class="loginWaitingMessage" style="display: none">
                                <div></div>
                            </div>
                            <div id="divMessages" class="alert alert-danger" role="alert" style="display: none; background-color: inherit; border-color: transparent; padding: 5px; margin-bottom: 5px;">
                                <span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>
                                <span class="sr-only">Error:</span>
                                <label id="lblMessages" style="font-weight: normal;" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>


</asp:Content>
