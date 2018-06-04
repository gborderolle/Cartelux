<%@ Page Language="C#" MasterPageFile="~/SiteExternal.Master" AutoEventWireup="true" CodeBehind="UniteGallery.aspx.cs" Inherits="Cartelux1.UniteGallery" Title="Gallería" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- STYLES EXTENSION -->
    <link rel="stylesheet" href="/Content/css/jquery-ui.css" />
    <link rel="stylesheet" href="/Content/unitegallery/css/unite-gallery.css" />
    <link rel="stylesheet" href="/Content/unitegallery/css/ug-theme-default.css" />

    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/pages/formulario.css" />

    <script type="text/javascript">
        var unite_api;
        jQuery(document).ready(function () {

            load_gallery(2);
        });

        function load_unitedgallery() {
            // SOURCE: http://unitegallery.net/index.php?page=examples-api
            unite_api = $("#unitegallery").unitegallery();

            unite_api.on("enter_fullscreen", function () {
                init_1();
            });
            unite_api.on("item_change", function (num, data) {
                init_2(data.title);
            });
        }

        function load_gallery(tipo) {
            var location = "/Resources";
            switch (tipo) {
                case 1: { // personas - banners
                    location += "/personas/banners/"
                    break;
                }
                case 2: { // personas - pasacalles - 15 Años
                    location += "/personas/pasacalles/15_anos/"
                    break;
                }
            }
            getImageNames(location);
            resizeiFrame();
        }

        function resizeiFrame() {
            if (parent.IS_MOBILE) {
                $("#fancybox-wrap", window.parent.document).css("width", "-webkit-fill-available");
            }
        }
            function getImageNames(location) {
            // Ajax call parameters
            console.log("Ajax call 1: http://pedidos.cartelux.uy. Params:");
            console.log("actionID, type: " + type(location) + ", value: " + location);
            console.log("End Ajax call");
            // SOURCE: https://stackoverflow.com/questions/6994212/is-there-a-way-to-return-a-list-of-all-the-image-file-names-from-a-folder-using

            $.ajax({
                type: "POST",
                url: "UniteGallery.aspx/GetFileNames",
                data: '{location: "' + location + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var resultado = response.d;
                    if (resultado !== null && resultado) {
                        create_images(location, resultado);
                    } else {
                        alert("Error interno.");
                    }
                }
            }); // Ajax

            //$.ajax({
            //    type: "POST",
            //    //url: "http://pedidos.cartelux.uy" + location,
            //    url: location,
            //    success: function (data) {
            //        $(data).find("td > a").each(function () {
            //            // will loop through 
            //            console.log("Found a file: " + $(this).attr("href"));
            //        });
            //    }
            //}); // Ajax
        }

        function create_images(location, resultado) {
            if (location != null && resultado != null) {
                $.each((resultado), function (index, value) {
                    console.log(index + ": " + value);

                    var img = $('<img/>').attr({
                        id: value,
                        alt: value,
                        src: location + value,
                        'data-image': location + value,
                        style: "display:none"
                    });
                    $("#unitegallery").append(img);
                }); // each

                load_unitedgallery();
            }
        }

        function init_1() {
            var obj = $(".ug-lightbox-numbers");
            if (obj !== null) {
                var text = obj.text();
                if (text !== null && text.length > 0) {
                    var text_array = text.split(" ");
                    if (text_array !== null && text_array.length > 0) {
                        var selected_num = text_array.split(" ")[0];
                        var item = unite_api.getItem(selected_num);
                        if (item !== null) {
                            var title = item.tile;

                            $(".ug-textpanel-textwrapper").css("height: 10%;");
                            var btn = $('<input/>').attr({
                                type: "button",
                                id: "field",
                                onclick: "selectThis('" + title + "')",
                                value: "Seleccionar",
                                style: "color:black"
                            });
                            $(".ug-textpanel-title").append("<br>");
                            $(".ug-textpanel-title").append(btn);
                            $(".ug-textpanel").css("height", "50px");
                            $(".ug-textpanel-textwrapper").css("height", "50px");
                        }
                    }
                }
            }
        }

        function init_2(title) {
            $(".ug-textpanel-textwrapper").css("height: 10%;");
            var btn = $('<input/>').attr({
                type: "button",
                id: "field",
                onclick: "selectThis('" + title + "')",
                value: "Seleccionar",
                style: "color:black"
            });
            $(".ug-textpanel-title").append("<br>");
            $(".ug-textpanel-title").append(btn);
            $(".ug-textpanel").css("height", "50px");
            $(".ug-textpanel-textwrapper").css("height", "50px");
        }

        function selectThis(title) {
            if (title !== null && title !== undefined) {
                //alert(title);
                var hdnDisenoSeleccionado = $("#hdnDisenoSeleccionado", parent.document)
                if (hdnDisenoSeleccionado !== null) {
                    hdnDisenoSeleccionado.val(title);
                }
                var aLinkToUnitegallery = $("#aLinkToUnitegallery", parent.document)
                if (aLinkToUnitegallery !== null) {
                    aLinkToUnitegallery.text(title);
                }
                parent.$.fancybox.close();
            }
        }
    </script>
    <style type="text/css">
        .unitegallery {
            margin: 0px auto;
            max-width: 100%;
            min-width: 150px;
            overflow: visible;
            height: auto;
            width: auto;
            background-color: #eee;
        }

         .container-full {
            background-color: #e8e8e8 !important;
        }

        .ug-thumb-wrapper {
            cursor: pointer !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server">

    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/pages/auxiliar_functions.js"></script>

    <!-- JS -->
    <script type="text/javascript" src="/Content/unitegallery/js/unitegallery.min.js"></script>
    <%--<script type="text/javascript" src="/Content/unitegallery/js/ug-theme-default.js"></script>--%>
    <%--SOURCE: http://unitegallery.net/index.php?page=default-options--%>

    <script type="text/javascript" src="/Content/unitegallery/themes/tiles/ug-theme-tiles.js"></script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3 id="lblAlbumTitle">EMPRESAS - Roll ups</h3>

    <div id="unitegallery" class="ug-gallery-wrapper ug-theme-tiles ug-under-480 unitegallery">
        <%--<img alt="imgID1" src="/Resources/Unitegallery/a1.jpg" data-image="/Resources/Unitegallery/a1.jpg" style="display: none"/>--%>
        <%--<img alt="imgID2" src="/Resources/Unitegallery/a2.jpg" data-image="/Resources/Unitegallery/a2.jpg" style="display: none"/>--%>
        <%--<img alt="imgID3" src="/Resources/Unitegallery/a3.jpg" data-image="/Resources/Unitegallery/a3.jpg" style="display: none"/>--%>
    </div>

</asp:Content>
