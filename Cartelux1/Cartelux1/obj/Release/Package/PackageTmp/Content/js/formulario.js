$(document).ready(function () {
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

    setTimeout(function () {
        loadEvents();

        // Load enabled controls
        loadPreviousState();
    }, 500);

    TAB_COUNT = $("#hdnPedidoCantidad").val();
    $("#lblTabCount").text(TAB_COUNT);

    var hdnCurrentLAT = $("#hdnCurrentLAT").val();
    var hdnCurrentLNG = $("#hdnCurrentLNG").val();
    var hdnCurrentLocationURL = $("#hdnCurrentLocationURL").val();
    if (hdnCurrentLAT !== null && hdnCurrentLAT !== undefined && hdnCurrentLAT.length > 0 && hdnCurrentLAT !== "0" &&
        hdnCurrentLNG !== null && hdnCurrentLNG !== undefined && hdnCurrentLNG.length > 0 && hdnCurrentLNG !== "0" &&
        hdnCurrentLocationURL !== null && hdnCurrentLocationURL !== undefined && hdnCurrentLNG.length > 0 && hdnCurrentLocationURL !== "0") {

        _current_lat = hdnCurrentLAT;
        _current_lng = hdnCurrentLNG;
        _current_completeURL = hdnCurrentLocationURL;

        $("#txbCX_URL").val(hdnCurrentLocationURL);
    }
    else {
        _current_lat = -34.8725572;
        _current_lng = -56.2050191;
        _current_completeURL = "";
    }

    // Hide all controls
    hideAllControls();

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
    $("#ddlTipoEntrega1-menu").on("click", function () {

        var controls = $(".ui-selectmenu-text");
        if (controls !== null && controls !== undefined && controls.length > 0 && controls[1] != null) {
            var text = $(".ui-selectmenu-text")[1].innerText;
            var value = $("#ddlTipoEntrega1 option").filter(function () {
                return this.text == text;
            }).attr('selected', true).val();
            if (value !== null && value !== undefined && value.length > 0) {

                // Hide all controls
                hideAllControls();

                switch (value) {
                    case "1":
                        { // Colocación

                            // Show 
                            showControl("txbDireccion", true);
                            showControl("mapSearch", true);
                            showControl("mapSearch_msg", true);
                            showControl("map-canvas", true);

                            // Hide
                            showControl("txbCiudad", false);

                            break;
                        }
                    case "2":
                        { // Envío

                            // Show 
                            showControl("txbDireccion", true);
                            showControl("mapSearch", true);
                            showControl("mapSearch_msg", true);
                            showControl("map-canvas", true);

                            // Hide
                            showControl("txbCiudad", false);

                            break;
                        }
                    case "3":
                        { // Interior

                            // Show 
                            showControl("txbCiudad", true);

                            // Hide
                            showControl("txbDireccion", false);
                            showControl("mapSearch", false);
                            showControl("mapSearch_msg", false);
                            showControl("map-canvas", false);

                            break;
                        }
                    case "4":
                        { // Taller

                            // Hide
                            showControl("txbCiudad", false);
                            showControl("txbDireccion", false);
                            showControl("mapSearch", false);
                            showControl("mapSearch_msg", false);
                            showControl("map-canvas", false);

                            break;
                        }
                }
            }
        }
    });
}

function loadPreviousState() {
    var controls = $(".ui-selectmenu-text");
    if (controls !== null && controls !== undefined && controls.length > 0 && controls[1] != null) {
        var text = $(".ui-selectmenu-text")[1].innerText;
        var value = $("#ddlTipoEntrega1 option").filter(function () {
            return this.text == text;
        }).attr('selected', true).val();
        if (value !== null && value !== undefined && value.length > 0) {

            switch (value) {
                case 1:
                    { // Colocación

                        // Show 
                        showControl("txbDireccion", true);
                        showControl("mapSearch", true);
                        showControl("mapSearch_msg", true);
                        showControl("map-canvas", true);

                        // Hide
                        showControl("txbCiudad", false);

                        break;
                    }
                case 2:
                    { // Envío

                        // Show 
                        showControl("txbDireccion", true);
                        showControl("mapSearch", true);
                        showControl("mapSearch_msg", false);
                        showControl("map-canvas", true);

                        // Hide
                        showControl("txbCiudad", false);

                        break;
                    }
                case 3:
                    { // Interior

                        // Show 
                        showControl("txbCiudad", true);

                        // Hide
                        showControl("txbDireccion", false);
                        showControl("mapSearch", false);
                        showControl("mapSearch_msg", false);
                        showControl("map-canvas", false);

                        break;
                    }
                case 4:
                    { // Taller

                        // Hide
                        showControl("txbCiudad", false);
                        showControl("txbDireccion", false);
                        showControl("mapSearch", false);
                        showControl("mapSearch_msg", false);
                        showControl("map-canvas", false);

                        break;
                    }
            }
        }
    }
}

function readonlyControl(doEnable, idControl) {
    $("#" + idControl).attr("readonly", doEnable);
}

function showControl(idControl, doShow) {
    if (doShow) {
        $("#" + idControl).show();
    }
    else {
        $("#" + idControl).hide();
    }
}

function hideAllControls() {
    showControl("txbCiudad", false);
    showControl("txbDireccion", false);
    showControl("mapSearch", false);
    showControl("mapSearch_msg", false);
    showControl("map-canvas", false);
}

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

// https://www.youtube.com/watch?v=mQ6kXrBqJcc


/* -----         Loads the map once the page is loaded   ------- */

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        alert("Geolocation is not supported by this browser.");
    }
}
function showPosition(position) {
    var lat = position.coords.latitude;
    var lng = position.coords.longitude;
    map.setCenter(new google.maps.LatLng(lat, lng));
}

var _current_lat;
var _current_lng;
var _current_completeURL;
var _bounds;

function initialize() {

    var map = new google.maps.Map(document.getElementById("map-canvas"), {
        center: {
            lat: _current_lat,
            lng: _current_lng
        },
        zoom: 15
    });

    var marker = new google.maps.Marker({
        position: {
            lat: _current_lat,
            lng: _current_lng
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

        if (bounds !== null && bounds !== undefined &&
        bounds.b !== null && bounds.b !== undefined &&
        bounds.b.b !== null && bounds.b.b !== undefined &&
            bounds.f.f !== null && bounds.f.f !== undefined) {
            _bounds = bounds;
            _current_lat = bounds.f.f;
            _current_lng = bounds.b.b;

            $("#hdnCurrentLAT").val(_current_lat);
            $("#hdnCurrentLNG").val(_current_lng);

            // https://www.google.com/maps/search/?api=1&query=47.5951518,-122.3316393&query_place_id=ChIJKxjxuaNqkFQR3CK6O1HNNqY

            var url_short = map.getCenter().toUrlValue();
            var url_complete = "https://www.google.com/maps/search/?api=1&query=" + url_short;
            _current_completeURL = url_complete;
            $("#hdnCurrentLocationURL").val(url_complete);
        }
    })
}