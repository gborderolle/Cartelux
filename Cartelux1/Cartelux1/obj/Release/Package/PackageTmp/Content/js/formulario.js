$(document).ready(function() {
    var ID = getUrlParameter('ID');
    if (ID !== null && ID !== undefined && ID.length > 0) {
        //loadPreviousState();
    } else {
        $("#btnEdit").hide();
    }

    _TEL = getUrlParameter('TEL');
    if (_TEL !== null && _TEL !== undefined && _TEL.length > 0) {
        $("#txbCX_tel").val(_TEL);
    }

    setTimeout(function() {
        $("#ddlTipoEntrega-button").on("click", function() {
            loadEvents();
        });
    }, 500);

    TAB_COUNT = $("#hdnPedidoCantidad").val();
    $("#lblTabCount").text(TAB_COUNT);

}); // END On Ready

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

function loadEvents() {
    $("#ddlTipoEntrega-menu li.ui-menu-item div").on("click", function() {
        var value = $(this).attr("id");
        if (value !== null && value.length > 0) {
            switch (value) {
                case "ui-id-2":
                    { // Colocación
                        readonlyControl(false, "txbDireccion");
                        //readonlyControl(false, "ddlLugarEntrega");
                        readonlyControl(true, "txbCiudad");
                        break;
                    }
                case "ui-id-3":
                    { // Envío
                        readonlyControl(false, "txbDireccion");
                        //readonlyControl(false, "ddlLugarEntrega");
                        readonlyControl(true, "txbCiudad");
                        break;
                    }
                case "ui-id-4":
                    { // Interior
                        readonlyControl(true, "txbDireccion");
                        //readonlyControl(true, "ddlLugarEntrega");
                        readonlyControl(false, "txbCiudad");
                        break;
                    }
                case "ui-id-5":
                    { // Taller
                        readonlyControl(true, "txbDireccion");
                        //readonlyControl(true, "ddlLugarEntrega");
                        readonlyControl(true, "txbCiudad");
                        break;
                    }
            }
        }
    });
}

function loadPreviousState() {
    var selectedIndex = $("#ddlTipoEntrega option:selected").index()
    if (selectedIndex !== null && selectedIndex > 0) {
        switch (selectedIndex) {
            case 1:
                { // Colocación
                    readonlyControl(false, "txbDireccion");
                    //readonlyControl(false, "ddlLugarEntrega");
                    readonlyControl(true, "txbCiudad");
                    break;
                }
            case 2:
                { // Envío
                    readonlyControl(false, "txbDireccion");
                    //readonlyControl(false, "ddlLugarEntrega");
                    readonlyControl(true, "txbCiudad");
                    break;
                }
            case 3:
                { // Interior
                    readonlyControl(true, "txbDireccion");
                    //readonlyControl(true, "ddlLugarEntrega");
                    readonlyControl(false, "txbCiudad");
                    break;
                }
            case 4:
                { // Taller
                    readonlyControl(true, "txbDireccion");
                    //readonlyControl(true, "ddlLugarEntrega");
                    readonlyControl(true, "txbCiudad");
                    break;
                }
        }
    }
}

function readonlyControl(doEnable, idControl) {
    $("#" + idControl).attr("readonly", doEnable);
}

function readonlyControl(doEnable, idControl) {
    $("#" + idControl).attr("readonly", doEnable);
}

//function loadEvents() {
//    //$("#ddlTipoEntrega").change(function () {
//    //    var selectedValue = parseInt(jQuery(this).val());
//    //    alert(selectedValue);

//        ////Depend on Value i.e. 0 or 1 respective function gets called. 
//        //switch (selectedValue) {
//        //    case 0:
//        //        handlerFunctionA();
//        //        break;
//        //    case 1:
//        //        handlerFunctionB();
//        //        break;
//        //        //etc... 
//        //    default:
//        //        alert("catch default");
//        //        break;
//    //}

//    $("#ddlTipoEntrega").on("change", function () {

//        //// to get the value and id of selected option
//        //var str = $('option:selected', this).attr('id');
//        //var value = $('option:selected', this).attr('value');
//        //alert(str);

//    });
//}

function editFields() {
    setFieldsReadOnly(false);
}

function setFieldsReadOnly(value) {
    $(".txbEditable").attr("readonly", value);
    $("#btnConfirmar").disabled = value;
    $(".dropdown").attr("disabled", value);

    if (value) {
        $("#btnConfirmar").addAttr("disabled");
    } else {
        $("#btnConfirmar").removeAttr("disabled");
    }
}

function confirmacionPedido() {
    $("#dialog p").text(hashMessages["ConfirmacionPedido"]);
    $("#dialog").dialog({
        buttons: {
            "Cerrar": function() {
                $(this).dialog("close");
            }
        }
    });
}

/* JS Goolge Maps API - Search location with map */
// Source: https://www.youtube.com/watch?v=2n_r0NDekgc
// https://www.youtube.com/watch?v=Zxf1mnP5zcw&t=1061s

google.maps.event.addDomListener(window, 'load', initialize);

/* -----         Loads the map once the page is loaded   ------- */

function initialize() {
    var map = new google.maps.Map(document.getElementById("map-canvas"), {
        center: {
            lat: 30.2655498,
            lng: -97.7452663
        },
        zoom: 12
    });

    var marker = new google.maps.Marker({
        position: {
            lat: 30.2655498,
            lng: -97.7452663
        },
        map: map,
        draggable: true
    });


    var searchBox = new google.maps.places.SearchBox(document.getElementById('mapSearch'));

    google.maps.event.addDomListener(searchBox, 'places_changed', function () {
        var places = searchBox.getPlaces();
        var bounds = new google.maps.LatLngBounds();
        var i, place;

        for (i = 0; place = places[i]; i++) {
            bounds.extend(place.geometry.location);
            marker.setPosition(place.geometry.location);
        }
        map.fitBounds(bounds);
        map.setZoom(15);
    })
}