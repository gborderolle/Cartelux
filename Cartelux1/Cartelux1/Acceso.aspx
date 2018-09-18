<%@ Page Language="C#" MasterPageFile="~/SiteExternal.Master" AutoEventWireup="true" CodeBehind="Acceso.aspx.cs" Inherits="Cartelux1.Acceso" Title="Cartelux Publicidad" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- STYLES EXTENSION -->
    <link rel="stylesheet" href="/Content/css/jquery-ui.css" />

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/pages/login-styles.css" />
    <link rel="stylesheet" href="/Content/css/pages/login.css" />

    <style type="text/css">
        #collapse1 {
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
            -webkit-box-shadow: 0 8px 0 #979494, 0 15px 20px rgba(0, 0, 0, .35);
            -moz-box-shadow: 0 8px 0 #979494, 0 15px 20px rgba(0, 0, 0, .35);
            box-shadow: 0 8px 0 #979494, 0 15px 20px rgba(0, 0, 0, .35);
        }

        .grand {
            background-image: url('Content/img/background-cx.jpg');
            position: absolute;
    width: 100%;
    min-height: 100%;
    left: 0;
    top: 0;
    right: 0;
    background-size: cover;
    background-attachment: fixed;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/pages/login.js"></script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="generalContainer grand">

        <div class="loginFormContainer col-md-3 col-sm-10 col-xs-12">
             <div class="loginTitleArea unselectable">
                    <img class="loginTitleImage pull-left" src="/Content/img/login.png" />
                    <div class="loginTitleBread">Cartelux Administrativo</div>
                    <div class="loginTitleText">Sistema de autenticación</div>
                </div>

            <div class="loginFormElements">               
                <div class="loginFormContent" style="margin-bottom: 0;">

                    <div class="form-group unselectable">
                <input type="text" id="txbUser1" runat="server" placeholder="Usuario" class="txbUser form-control" style="padding: 25px; max-width: initial; font-size:larger;" />
                    </div>
                    <div class="form-group unselectable">
                <input type="password" id="txbPassword1" runat="server" placeholder="Contraseña" class="txbPassword form-control" style="padding: 25px; max-width: initial; font-size:larger;" />
                    </div>

                </div>
                <div class="loginFormButtonContainer" style="width: 100%;">
                    <button type="button" id="btnSubmit" class="btn btn-primary btn-lg" onclick="checkSubmit();">
                        <i class="fa fa-check"></i>&nbsp;Ingresar
                    </button>
                    <input type="submit" id="btnSubmit_candidato1" runat="server" onserverclick="btnSubmit_candidato1_ServerClick" class="btnSubmit_candidato" />

                    <div class="loginFormMessageContainer" style="box-sizing: inherit; width: 100%; padding: 0;">
                        <div class="loginWaitingMessage" style="display: none">
                            <div></div>
                        </div>
                        <div id="divMessages" class="alert alert-danger" role="alert" style="display: none; background-color: inherit; border-color: transparent; padding: 5px; margin-bottom: 5px;">
                            <span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>
                            <span class="sr-only">Error:</span>
                            <label id="lblMessages"/>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>


</asp:Content>
