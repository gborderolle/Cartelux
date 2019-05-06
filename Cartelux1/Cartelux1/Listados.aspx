<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Listados.aspx.cs" Inherits="Cartelux1.Listados" Title="Listados" %>

<%@ Register Src="/User_Controls/Pedidos.ascx" TagPrefix="uc1" TagName="Pedidos" %>
<%@ Register Src="/User_Controls/Clientes.ascx" TagPrefix="uc1" TagName="Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- STYLES EXTENSION -->

    <!-- Theme style -->
    <link rel="stylesheet" href="/Content/css/pages/infoBoxes.min.css" />
    <link rel="stylesheet" href="/Content/css/pages/datos.css" />

    <!-- AdminLTE Skins. Choose a skin from the css/skins
     folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="/Content/plugins/_all-skins.min.css" />
    <!-- iCheck -->
    <link rel="stylesheet" href="/Content/plugins/iCheck/flat/blue.css" />
    <!-- Morris chart -->
    <link rel="stylesheet" href="/Content/plugins/morris/morris.css" />
    <!-- jvectormap -->
    <link rel="stylesheet" href="/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.css" />
    <!-- Date Picker -->
    <link rel="stylesheet" href="/Content/plugins/datepicker/datepicker3.css" />
    <!-- Daterange picker -->
    <link rel="stylesheet" href="/Content/plugins/daterangepicker/daterangepicker.css" />
    <!-- bootstrap wysihtml5 - text editor -->
    <link rel="stylesheet" href="/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" />


    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
<script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
<![endif]-->

    <style type="text/css">
        .container-full {
            background-color: #eee !important;
            margin: 0 auto;
            display: block;
            width: 100%;
        }

        table tr th {
            font-weight: 500;
            line-height: 1.1;
            background-color: #428bca;
        }

        .body-content {
            margin-top: 30px;
        }

        @media (max-width: 768px) {
            body {
                width: fit-content;
            }

            h2 {
                font-size: large;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">

    <!-- PAGE SCRIPTS -->

    <!-- Morris.js charts -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script src="/Content/plugins/morris/morris.min.js"></script>
    <!-- Sparkline -->
    <script src="/Content/plugins/sparkline/jquery.sparkline.min.js"></script>
    <!-- jvectormap -->
    <script src="/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="/Content/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>
    <!-- jQuery Knob Chart -->
    <script src="/Content/plugins/knob/jquery.knob.js"></script>
    <!-- daterangepicker -->
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.2/moment.min.js"></script>--%>
    <%--<script src="/Content/plugins/daterangepicker/daterangepicker.js"></script>--%>
    <!-- datepicker -->
    <script src="/Content/plugins/datepicker/bootstrap-datepicker.js"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
    <!-- Slimscroll -->
    <script src="/Content/plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <!-- FastClick -->
    <script src="/Content/plugins/fastclick/fastclick.js"></script>
    <!-- AdminLTE App -->
    <script src="/Content/plugins/app.min.js"></script>
    <!-- AdminLTE for demo purposes -->
    <script src="/Content/plugins/demo.js"></script>

    <!-- PAGE SCRIPTS CARTELUX -->
    <script type="text/javascript" src="/Content/js/jquery.quicksearch.js"></script>
    <script type="text/javascript" src="/Content/js/moment.js"></script>

    <!-- Page JS -->
    <script type="text/javascript" src="/Content/js/pages/listados.js"></script>

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/Modal_styles.css" />

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper1">

        <!-- Main content -->
        <section class="content">

            <!-- =========================================================== -->

            <div class="box box-default">
                <div class="box-header with-border" style="padding-bottom: 0;">

                    <div class="row">
                        <div class="col-md-9">
                            <asp:Button ID="backButton" runat="server" Text="Volver" CssClass="btn btn-info pull-left" OnClientClick="JavaScript:window.history.back(1);return false;" />
                            <h3 class="box-title">
                                <label id="lblTableActive" style="font-weight: normal;"></label>
                            </h3>
                        </div>

                        <div class="col-md-2 pull-right" style="margin-right: 10px;">
                            <form action="#" method="get" class="sidebar-form" style="display: block !important; width: 100%;">
                                <div class="input-group ">
                                    <input type="text" id="txbSearch" name="q" class="form-control" placeholder="Buscar...">
                                    <span class="input-group-btn">
                                        <button type="button" name="search" id="search-btn" class="btn btn-flat">
                                            <i class="fa fa-search"></i>
                                        </button>
                                    </span>
                                </div>
                            </form>
                        </div>
                    </div>

                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div id="divContent">

                                    <div class="divTables" id="divPedidos" style="display: none;">
                                        <asp:UpdatePanel ID="upPedidos" runat="server">
                                            <ContentTemplate>
                                                <uc1:Pedidos runat="server" ID="Pedidos" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="divTables" id="divClientes" style="display: none;">
                                        <asp:UpdatePanel ID="upClientes" runat="server">
                                            <ContentTemplate>
                                                <uc1:Clientes runat="server" ID="Clientes" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>

                            <!-- /.form-group -->
                        </div>
                        <!-- /.col -->
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->
                </div>
                <!-- /.box-body -->
            </div>



            <!-- =========================================================== -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->



</asp:Content>
