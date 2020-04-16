/**** Local variables ****/
var IS_MOBILE;
var USER_ID;

//
var Precios_lonaFrontMate = 0;
var Precios_lonaDobleFaz = 0;
var Precios_vinilo = 0;
var Precios_viniloCanvas = 0;
var Precios_viniloMicroperforado = 0;
var Precios_telaBandera = 0;
var Precios_fondoColor = 0;
var Precios_bannersOjalillos = 0;
var Precios_bannersPerfiles = 0;
//
var Costos_lonaFrontMate = 0;
var Costos_lonaDobleFaz = 0;
var Costos_vinilo = 0;
var Costos_viniloCanvas = 0;
var Costos_viniloMicroperforado = 0;
var Costos_telaBandera = 0;
var Costos_fondoColor = 0;
var Costos_bannersOjalillos = 0;
var Costos_bannersPerfiles = 0;

$(document).ready(function () {
    checkMobile();
    initVariables();
    bindEvents();
    loadBaseCalculos();
});

// attach the event binding function to every partial update
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
    bindEvents(); 
});


function checkMobile() {
    var mobile = (/iphone|ipod|android|blackberry|mini|windows\sce|palm/i.test(navigator.userAgent.toLowerCase()));
    IS_MOBILE = false;
    if (mobile) {
        IS_MOBILE = true;
    }
}

function initVariables() {

    if (IS_MOBILE) {
       
    }
}

function bindEvents() {
    $("#tabsBases").tabs();
}

function check_nullValues(value) {
    var value_return = value;
    if (value === null || value === undefined) {
        value_return = "";
    }
    return value_return;
}

// Restricts input for each element in the set of matched elements to the given inputFilter.
(function ($) {
    $.fn.inputFilter = function (inputFilter) {
        return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            }
        });
    };
}(jQuery));

$(document).ready(function () {
    // Restrict input to digits by using a regular expression filter.
    $("#txb_Precios_lonaFrontMate").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#txb_Precios_lonaDobleFaz").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#txb_Precios_vinilo").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#txb_Precios_viniloCanvas").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#txb_Precios_viniloMicroperforado").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#txb_Precios_telaBandera").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#txb_Precios_fondoColor").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#txb_Precios_bannersOjalillos").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#txb_Precios_bannersPerfiles").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#txb_Descuentos1").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });

    // Text change

    $("#txb_Precios_lonaFrontMate").change(function () {
        var result = $(this).val() * Precios_lonaFrontMate;
        $("#calc_Precios_lonaFrontMate").text("$" + numberWithCommas(result.toFixed(2)));
    });
    $("#txb_Precios_lonaDobleFaz").change(function () {
        var result = $(this).val() * Precios_lonaDobleFaz;
        $("#calc_Precios_lonaDobleFaz").text("$" + numberWithCommas(result.toFixed(2)));
    });
    $("#txb_Precios_vinilo").change(function () {
        var result = $(this).val() * Precios_vinilo;
        $("#calc_Precios_vinilo").text("$" + numberWithCommas(result.toFixed(2)));
    });
    $("#txb_Precios_viniloCanvas").change(function () {
        var result = $(this).val() * Precios_viniloCanvas;
        $("#calc_Precios_viniloCanvas").text("$" + numberWithCommas(result.toFixed(2)));
    });
    $("#txb_Precios_viniloMicroperforado").change(function () {
        var result = $(this).val() * Precios_viniloMicroperforado;
        $("#calc_Precios_viniloMicroperforado").text("$" + numberWithCommas(result.toFixed(2)));
    });
    $("#txb_Precios_telaBandera").change(function () {
        var result = $(this).val() * Precios_telaBandera;
        $("#calc_Precios_telaBandera").text("$" + numberWithCommas(result.toFixed(2)));
    });
    $("#txb_Precios_fondoColor").change(function () {
        var result = $(this).val() * Precios_fondoColor;
        $("#calc_Precios_fondoColor").text("$" + numberWithCommas(result.toFixed(2)));
    });
    $("#txb_Precios_bannersOjalillos").change(function () {
        var result = $(this).val() * Precios_bannersOjalillos;
        $("#calc_Precios_bannersOjalillos").text("$" + numberWithCommas(result.toFixed(2)));
    });
    $("#txb_Precios_bannersPerfiles").change(function () {
        var result = $(this).val() * Precios_bannersPerfiles;
        $("#calc_Precios_bannersPerfiles").text("$" + numberWithCommas(result.toFixed(2)));
    });

});


/*
// Restricts input for the given textbox to the given inputFilter.
function setInputFilter(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
        textbox.addEventListener(event, function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            }
        });
    });
}

// Restrict input to digits and '.' by using a regular expression filter.
setInputFilter(document.getElementById("txb_Precios_lonaFrontMate"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Precios_lonaDobleFaz"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Precios_vinilo"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Precios_viniloCanvas"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Precios_viniloMicroperforado"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Precios_telaBandera"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Precios_fondoColor"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Precios_bannersOjalillos"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Precios_bannersPerfiles"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Descuentos1"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
//
setInputFilter(document.getElementById("txb_Costos_lonaFrontMate"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Costos_lonaDobleFaz"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Costos_vinilo"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Costos_viniloCanvas"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Costos_viniloMicroperforado"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Costos_telaBandera"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Costos_fondoColor"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Costos_bannersOjalillos"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Costos_bannersPerfiles"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
setInputFilter(document.getElementById("txb_Descuentos1"), function (value) {
    return /^\d*\.?\d*$/.test(value);
});
*/

function loadBaseCalculos() {
    // Source: https://www.codeproject.com/Tips/775585/Bind-Gridview-using-AJAX
    // Ajax call parameters
    console.log("Ajax call: Calculadora.aspx/GetData_BaseCalculos. Params:");

    $.ajax({
        type: "POST",
        url: "Calculadora.aspx/GetData_BaseCalculos",
        contentType: "application/json;charset=utf-8",
        data: '{dummy: ""}',
        dataType: "json",
        success: function (response) {

            if (response.d.length > 0) {
                for (var i = 0; i < response.d.length; i++) {

                    // Códigos:
                    /*
                    1	Lona front Mate
                    2	Lona doble faz
                    3	Vinilo
                    4	Vinilo Canvas
                    5	Vinilo Microperforado
                    6	Tela bandera
                    7	Fondo de color extra
                    8	Banners c/ojalillos
                    9	Banners c/perfiles
                     * */

                    var codigo = check_nullValues(response.d[i].Codigo);
                    switch (codigo) {
                        case 1:
                            {
                                Precios_lonaFrontMate = check_nullValues(response.d[i].Precio);
                                Costos_lonaFrontMate = check_nullValues(response.d[i].Costo);
                                break;
                            }
                        case 2:
                            {
                                Precios_lonaDobleFaz = check_nullValues(response.d[i].Precio);
                                Costos_lonaDobleFaz = check_nullValues(response.d[i].Costo);
                                break;
                            }
                        case 3:
                            {
                                Precios_vinilo = check_nullValues(response.d[i].Precio);
                                Costos_vinilo = check_nullValues(response.d[i].Costo);
                                break;
                            }
                        case 4:
                            {
                                Precios_viniloCanvas = check_nullValues(response.d[i].Precio);
                                Costos_viniloCanvas = check_nullValues(response.d[i].Costo);
                                break;
                            }
                        case 5:
                            {
                                Precios_viniloMicroperforado = check_nullValues(response.d[i].Precio);
                                Costos_viniloMicroperforado = check_nullValues(response.d[i].Costo);
                                break;
                            }
                        case 6:
                            {
                                Precios_telaBandera = check_nullValues(response.d[i].Precio);
                                Costos_telaBandera = check_nullValues(response.d[i].Costo);
                                break;
                            }
                        case 7:
                            {
                                Precios_fondoColor = check_nullValues(response.d[i].Precio);
                                Costos_fondoColor = check_nullValues(response.d[i].Costo);
                                break;
                            }
                        case 8:
                            {
                                Precios_bannersOjalillos = check_nullValues(response.d[i].Precio);
                                Costos_bannersOjalillos = check_nullValues(response.d[i].Costo);
                                break;
                            }
                        case 9:
                            {
                                Precios_bannersPerfiles = check_nullValues(response.d[i].Precio);
                                Costos_bannersPerfiles = check_nullValues(response.d[i].Costo);
                                break;
                            }
                    }
                } // for


                // Load placeholders
                $("#txb_Precios_lonaFrontMate").attr("placeholder", "$ " + Precios_lonaFrontMate + " por 1 CM2");
                $("#txb_Precios_lonaDobleFaz").attr("placeholder", "$ " + Precios_lonaDobleFaz + " por 1 CM2");
                $("#txb_Precios_vinilo").attr("placeholder", "$ " + Precios_vinilo + " por 1 CM2");
                $("#txb_Precios_viniloCanvas").attr("placeholder", "$ " + Precios_viniloCanvas + " por 1 CM2");
                $("#txb_Precios_viniloMicroperforado").attr("placeholder", "$ " + Precios_viniloMicroperforado + " por 1 CM2");
                $("#txb_Precios_telaBandera").attr("placeholder", "$ " + Precios_telaBandera + " por 1 CM2");
                $("#txb_Precios_fondoColor").attr("placeholder", "$ " + Precios_fondoColor + " por 1 CM2");
                $("#txb_Precios_bannersOjalillos").attr("placeholder", "$ " + Precios_bannersOjalillos + " por 1 CM2");
                $("#txb_Precios_bannersPerfiles").attr("placeholder", "$ " + Precios_bannersPerfiles + " por 1 CM2");

                //

                $("#txb_Costos_lonaFrontMate").attr("placeholder", "$ " + Costos_lonaFrontMate + " por 1 CM2");
                $("#txb_Costos_lonaDobleFaz").attr("placeholder", "$ " + Costos_lonaDobleFaz + " por 1 CM2");
                $("#txb_Costos_vinilo").attr("placeholder", "$ " + Costos_vinilo + " por 1 CM2");
                $("#txb_Costos_viniloCanvas").attr("placeholder", "$ " + Costos_viniloCanvas + " por 1 CM2");
                $("#txb_Costos_viniloMicroperforado").attr("placeholder", "$ " + Costos_viniloMicroperforado + " por 1 CM2");
                $("#txb_Costos_telaBandera").attr("placeholder", "$ " + Costos_telaBandera + " por 1 CM2");
                $("#txb_Costos_fondoColor").attr("placeholder", "$ " + Costos_fondoColor + " por 1 CM2");
                $("#txb_Costos_bannersOjalillos").attr("placeholder", "$ " + Costos_bannersOjalillos + " por 1 CM2");
                $("#txb_Costos_bannersPerfiles").attr("placeholder", "$ " + Costos_bannersPerfiles + " por 1 CM2");
            }

        }, // end success
        failure: function (response) {
        }
    }); // Ajax


    
}