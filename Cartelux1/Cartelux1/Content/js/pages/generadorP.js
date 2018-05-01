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
        style_controls(false);

        // Load enabled controls
        loadPreviousState();
    }, 300);

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

    $("#aCollapse_bosquejo").click();

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

function style_controls(correct) {
    var combos = $("select.ctrl-required").next("[role='combobox']");
    if (combos !== null && combos !== undefined && combos.length > 0) {
        if (correct) {
            combos.css("border-color", "#34aa57");
        } else {
            combos.css("border-color", "red");
        }

        combos.css("height", "50px");
        combos.css("font-size", "larger");
    }
}

function loadEvents() {
    $("#ddlTipoEntrega1-menu").on("click", function () {

        var controls = $("#ddlTipoEntrega1-button span");
        if (controls !== null && controls !== undefined && controls.length > 0 && controls[1] !== null && controls[1] !== undefined) {
            var text = controls[1].innerText;
            var value = $("#ddlTipoEntrega1 option").filter(function () {
                return this.text === text;
            }).attr('selected', true).val();

            //$("#ddlTipoEntrega1-button span")[1]

            if (value !== null && value !== undefined && value.length > 0) {

                // Hide all controls
                hideAllControls();

                switch (value) {
                    case "1":
                        { // Colocación

                            // Show 
                            showControl("dir_group", true);
                            showControl("map_group", true);
                            showControl("map-canvas", true);

                            // Hide
                            showControl("txbCiudad", false);

                            break;
                        }
                    case "2":
                        { // Envío

                            // Show 
                            showControl("dir_group", true);
                            showControl("map_group", true);
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
                            showControl("dir_group", false);
                            showControl("map_group", false);
                            showControl("map-canvas", false);

                            break;
                        }
                    case "4":
                        { // Taller

                            // Hide
                            showControl("txbCiudad", false);
                            showControl("dir_group", false);
                            showControl("map_group", false);
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
    if (controls !== null && controls !== undefined && controls.length > 0 && controls[1] !== null) {
        var text = $(".ui-selectmenu-text")[1].innerText;
        var value = $("#ddlTipoEntrega1 option").filter(function () {
            return this.text === text;
        }).attr('selected', true).val();
        if (value !== null && value !== undefined && value.length > 0) {

            switch (value) {
                case 1:
                    { // Colocación

                        // Show 
                        showControl("dir_group", true);
                        showControl("map_group", true);
                        showControl("map-canvas", true);

                        // Hide
                        showControl("txbCiudad", false);

                        break;
                    }
                case 2:
                    { // Envío

                        // Show 
                        showControl("dir_group", true);
                        showControl("map_group", true);
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
                        showControl("dir_group", false);
                        showControl("map_group", false);
                        showControl("map-canvas", false);

                        break;
                    }
                case 4:
                    { // Taller

                        // Hide
                        showControl("txbCiudad", false);
                        showControl("dir_group", false);
                        showControl("map_group", false);
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

function checkbox_repetidos() {
    var chbRepetidos = $("#chbRepetidos");
    if (chbRepetidos !== null) {
        var chbRepetidos_value = chbRepetidos.is(":checked");
        if (chbRepetidos_value) {
            $("#repetidos_group").show();
        } else {
            $("#repetidos_group").hide();
        }
    }
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
    showControl("dir_group", false);
    showControl("map_group", false);
    showControl("map-canvas", false);
}

function editFields() {
    //setFieldsReadOnly(false);
}

function setFieldsReadOnly(value) {
    $(".txbEditable").attr("readonly", value);
    $("#btnConfirmar1").disabled = value;
    $(".dropdown").attr("disabled", value);

    if (value) {
        $("#btnConfirmar1").addAttr("disabled");
    } else {
        $("#btnConfirmar1").removeAttr("disabled");
    }
}

function confirmacionPedido() {
    showMessage(hashMessages["ConfirmacionPedido"]);
}

function showMessage(value) {
    $("#dialog p").text(value);
    $("#dialog").dialog({
        buttons: {
            "Cerrar": function () {
                $(this).dialog("close");
            }
        }
    });
}

function apply_savedStyle() {
    var btnConfirmar1 = $("#btnConfirmar1");
    if (btnConfirmar1 !== null && btnConfirmar1 !== undefined && btnConfirmar1.length > 0) {
        btnConfirmar1.val("CORRECTO");
        btnConfirmar1.removeClass("btn-danger");
        btnConfirmar1.removeClass("btnConfirm1");
        btnConfirmar1.addClass("btn-success");
        btnConfirmar1.addClass("btnConfirm2");
    }

    var labels = $("._label1");
    if (labels !== null && labels !== undefined && labels.length > 0) {
        labels.removeClass("_label1");
        labels.addClass("_label2");
    }

    var form = $(".div-form1");
    if (form !== null && form !== undefined && form.length > 0) {
        form.removeClass("div-form2");
        form.addClass("div-form2");
    }


    setTimeout(function () {
        style_controls(true);

        var controls = $(".ctrl-required");
        if (controls !== null && controls !== undefined && controls.length > 0) {
            controls.removeClass("ctrl-required");
            controls.addClass("ctrl-required_correct");
        }

    }, 300);
}

function pre_confirm() {
    var _check_logic = check_logic();
    var _check_emptyFields = check_emptyFields();

    var ok = true;
    if (!_check_logic || !_check_emptyFields) {
        showMessage(hashMessages["FaltanDatos"]);
        ok = false;
    }
    return ok;
}

function check_logic() {
    var ok = true;

    var ddlTamano1 = 0;
    var controls = $("#ddlTamano1-button span");
    if (controls !== null && controls !== undefined && controls.length > 0 && controls[1] !== null && controls[1] !== undefined) {
        var text = controls[1].innerText;
        var value = $("#ddlTamano1 option").filter(function () {
            return this.text === text;
        }).attr('selected', true).val();
        if (value !== null && value !== undefined && value.length > 0) {
            // Parse int
            ddlTamano1 = value;
            if (type(value) === "string") {
                ddlTamano1 = TryParseInt(value, 0);
            }
        }
    }
    if (ddlTamano1 === 0) {
        ok = false;
    }

    var ddlTipoEntrega1 = 0;
    controls = $("#ddlTipoEntrega1-button span");
    if (controls !== null && controls !== undefined && controls.length > 0 && controls[1] !== null && controls[1] !== undefined) {
        text = controls[1].innerText;
        value = $("#ddlTipoEntrega1 option").filter(function () {
            return this.text === text;
        }).attr('selected', true).val();
        if (value !== null && value !== undefined && value.length > 0) {
            // Parse int
            ddlTipoEntrega1 = value;
            if (type(value) === "string") {
                ddlTipoEntrega1 = TryParseInt(value, 0);
            }
        }
    }
    if (ddlTipoEntrega1 === 0) {
        ok = false;
    }

    // Colocación o envío a domicilio
    if (ddlTipoEntrega1 === 1 || ddlTipoEntrega1 === 2) {
        var txbDireccion_calle = $("#txbDireccion_calle").val();
        var txbDireccion_numero = $("#txbDireccion_numero").val();
        var txbDireccion_esquina = $("#txbDireccion_esquina").val();
        //var mapSearch = $("#mapSearch").val();
        if ((txbDireccion_calle === null || txbDireccion_calle === undefined || txbDireccion_calle.length === 0)
        || (txbDireccion_numero === null || txbDireccion_numero === undefined || txbDireccion_numero.length === 0)
            || (txbDireccion_esquina === null || txbDireccion_esquina === undefined || txbDireccion_esquina.length === 0)) {
            ok = false;
        }
    }

    // Envío al interior
    if (ddlTipoEntrega1 === 3) {
        var txbCiudad = $("#txbCiudad").val();
        if (txbCiudad === null || txbCiudad === undefined || txbCiudad.length === 0) {
            ok = false;
        }
    }
    return ok;
}

function check_emptyFields() {
    var ok = true;

    var txbNombre = $("#txbNombre").val();
    var txbTel = $("#txbTel").val();
    var txbFecha = $("#txbFecha").val();

    if ((txbNombre === null || txbNombre === undefined || txbNombre.length === 0)
       || (txbTel === null || txbTel === undefined || txbTel.length === 0)
       || (txbFecha === null || txbFecha === undefined || txbFecha.length === 0)) {
        ok = false;
    }
    return ok;
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

function showControl_withDelay(value1, value2) {
    setTimeout(function () {
        showControl(value1, value2);
    }, 300);
}