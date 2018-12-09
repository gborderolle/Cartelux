<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Listados1.aspx.cs" Inherits="Cartelux1.Listados1" Title="Listados1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- STYLES EXTENSION -->

    <!-- Theme style -->
    <link rel="stylesheet" href="/Content/css/pages/infoBoxes.min.css" />
    <link rel="stylesheet" href="/Content/css/pages/datos.css" />

    <!-- AdminLTE Skins. Choose a skin from the css/skins
     folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="/Content/plugins/_all-skins.min.css"/>
    <!-- iCheck -->
    <link rel="stylesheet" href="/Content/plugins/iCheck/flat/blue.css"/>
    <!-- Morris chart -->
    <link rel="stylesheet" href="/Content/plugins/morris/morris.css"/>
    <!-- jvectormap -->
    <link rel="stylesheet" href="/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.css"/>
    <!-- Date Picker -->
    <link rel="stylesheet" href="/Content/plugins/datepicker/datepicker3.css"/>
    <!-- Daterange picker -->
    <link rel="stylesheet" href="/Content/plugins/daterangepicker/daterangepicker.css"/>
    <!-- bootstrap wysihtml5 - text editor -->
    <link rel="stylesheet" href="/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"/>


    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
<script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
<![endif]-->


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">

    <!-- PAGE SCRIPTS BONISOFT -->

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
    <script src="/Content/plugins/daterangepicker/daterangepicker.js"></script>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper1">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Listados        
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
                <li class="active">Listados</li>
            </ol>
        </section>

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

                                    <div class="divTables" id="divClientes" style="display: none;">
                                        
                                
                                        
                                        
                                        
                                                
<asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="gridPedidos" runat="server" ClientIDMode="Static" HorizontalAlign="Center" AutoGenerateColumns="false" CssClass="table table-hover table-striped" AllowPaging="false" AllowSorting="false"
                                            DataKeyNames="Pedido_ID"
                                            OnRowDataBound="gridPedidos_RowDataBound"
                                            OnRowCommand="gridPedidos_RowCommand">
    <Columns>
        <asp:BoundField DataField="Pedido_ID" HeaderText="ID" HtmlEncode="false" ReadOnly="true" ItemStyle-CssClass="hiddencol hiddencol_real" HeaderStyle-CssClass="hiddencol hiddencol_real" />
        <asp:BoundField DataField="Formulario_ID" HeaderText="Nombre" HtmlEncode="false" ReadOnly="true" />
    </Columns>

</asp:GridView>
<asp:HiddenField ClientIDMode="Static" ID="hdnPedidosCount" runat="server" />









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
