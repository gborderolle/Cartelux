/**** Local variables ****/

$(document).ready(function () {
    bindEvents();
    bindDelayEvents();
});

function bindEvents() {
    $("#tabsElementos").tabs();
    $("#gridElementos").filterTable();
    // SOURCE: https://github.com/sunnywalker/jQuery.FilterTable

    bindGridElementos();

    $(".close").click(function () {
        $(this).closest(".popbox").hide();
    })    
}

function bindDelayEvents() {
    setTimeout(function () {

        /* Formularios */
        $("#gridElementos").tablesorter();
        $("#gridElementosSeleccionados").tablesorter();

    }, 100);
}

function gridElementosSeleccionados_quitar(lblElementoID, rowID) {
    $("#" + rowID).remove();
}

function gridElementosSeleccionados_calcular(lblElementoID, isFondoColor) {
    var volumen = $("#txbVolumen_" + lblElementoID).val();
    var cantidad_pedido = $("#txbCantidadTotal_" + lblElementoID).val();
    if (volumen !== null && volumen > 0 && cantidad_pedido !== null) {

        // Ajax call parameters
        console.log("Ajax call: Presupuestos.aspx/GridElementosSeleccionados_calcular. Params:");
        console.log("lblElementoID, type: " + type(lblElementoID) + ", value: " + lblElementoID);
        console.log("volumen, type: " + type(volumen) + ", value: " + volumen);
        console.log("cantidad_pedido, type: " + type(cantidad_pedido) + ", value: " + cantidad_pedido);
        console.log("isFondoColor, type: " + type(isFondoColor) + ", value: " + isFondoColor);

        $.ajax({
            type: "POST",
            url: "Presupuestos.aspx/GridElementosSeleccionados_calcular",
            contentType: "application/json;charset=utf-8",
            data: '{lblElementoID: "' + lblElementoID + '",volumen: "' + volumen + '",cantidad_pedido: "' + cantidad_pedido + '",isFondoColor: "' + isFondoColor + '"}',
            dataType: "json",
            success: function (response) {
                var resultado = response.d;
                if (resultado !== null) {

                    $("#txbCostoUnitario_" + lblElementoID).val(numberWithCommas(resultado.costo_unitario));
                    $("#txbPrecioUnitario_" + lblElementoID).val(numberWithCommas(resultado.precio_unitario));
                    $("#txbCostoTotal_" + lblElementoID).val(numberWithCommas(resultado.costo_total));
                    $("#txbPrecioSubtotal1_" + lblElementoID).val(numberWithCommas(resultado.precio_total));

                    gridElementosSeleccionados_sumarTotales();

                } else {
                    alert("Error interno.");
                }
            }, // end success
            failure: function (response) {
            }
        }); // Ajax

    }
}

function gridElementosSeleccionados_sumarTotales() {
    //Fuente: https://stackoverflow.com/questions/39686611/column-sum-of-multiple-columns-of-dynamic-gridview-using-javascript

    // COL: Costo unitario
    var sum = 0;
    $("input[id^='txbCostoUnitario_']").each(function (index) {
        var value = $(this).val();
        if (value !== null && value !== undefined && value != "") {
            sum += parseFloat(numberRemoveCommas(value));
        }
    });
    var txbCostoUnitario_ = sum;

    // COL: Precio unitario
    sum = 0;
    $("input[id^='txbPrecioUnitario_']").each(function (index) {
        var value = $(this).val();
        if (value !== null && value !== undefined && value != "") {
            sum += parseFloat(numberRemoveCommas(value));
        }
    });
    var txbPrecioUnitario_ = sum;

    // COL: Cantidad total
    sum = 0;
    $("input[id^='txbCantidadTotal_']").each(function (index) {
        var value = $(this).val();
        if (value !== null && value !== undefined && value != "") {
            sum += parseFloat(numberRemoveCommas(value));
        }
    });
    var txbCantidadTotal_ = sum;

    // COL: Costo total
    sum = 0;
    $("input[id^='txbCostoTotal_']").each(function (index) {
        var value = $(this).val();
        if (value !== null && value !== undefined && value != "") {
            sum += parseFloat(numberRemoveCommas(value));
        }
    });
    var txbCostoTotal_ = sum;

    // COL: Precio subtotal 1
    sum = 0;
    $("input[id^='txbPrecioSubtotal1_']").each(function (index) {
        var value = $(this).val();
        if (value !== null && value !== undefined && value != "") {
            sum += parseFloat(numberRemoveCommas(value));
        }
    });
    var txbPrecioSubtotal1_ = sum;

    // COL: Precio descuento
    sum = 0;
    $("input[id^='txbPrecioDescuento_']").each(function (index) {
        var value = $(this).val();
        if (value !== null && value !== undefined && value != "") {
            sum += parseFloat(numberRemoveCommas(value));
        }
    });
    var txbPrecioDescuento_ = sum;

    // COL: Precio subtotal 2
    sum = 0;
    $("input[id^='txbPrecioSubtotal2_']").each(function (index) {
        var value = $(this).val();
        if (value !== null && value !== undefined && value != "") {
            sum += parseFloat(numberRemoveCommas(value));
        }
    });
    var txbPrecioSubtotal2_ = sum;

    // COL: Precio redondeo
    sum = 0;
    $("input[id^='txbPrecioRedondeo_']").each(function (index) {
        var value = $(this).val();
        if (value !== null && value !== undefined && value != "") {
            sum += parseFloat(numberRemoveCommas(value));
        }
    });
    var txbPrecioRedondeo_ = sum;

    // COL: Precio final
    sum = 0;
    $("input[id^='txbPrecioFinal_']").each(function (index) {
        var value = $(this).val();
        if (value !== null && value !== undefined && value != "") {
            sum += parseFloat(numberRemoveCommas(value));
        }
    });
    var txbPrecioFinal_ = sum;

    gridElementosSeleccionados_updateTotales(txbCostoUnitario_, txbPrecioUnitario_, txbCantidadTotal_, txbCostoTotal_, txbPrecioSubtotal1_, txbPrecioDescuento_, txbPrecioSubtotal2_, txbPrecioRedondeo_, txbPrecioFinal_);
}

function gridElementosSeleccionados_updateTotales(txbCostoUnitario_, txbPrecioUnitario_, txbCantidadTotal_, txbCostoTotal_, txbPrecioSubtotal1_, txbPrecioDescuento_, txbPrecioSubtotal2_, txbPrecioRedondeo_, txbPrecioFinal_){
    $("#gridElementosSeleccionados > tfoot > tr").remove();
    $("#gridElementosSeleccionados > tfoot").append("<tr id='gridElementosSeleccionados_footer'><td class='td-very_short'>" +
    "" + "</td> <td class='td-very_short'>" +
    "" + "</td> <td class='td-very_short'>" +
    "" + "</td> <td class='td-very_short'>" +
    "" + "</td> <td class='td'>" + // Comentarios
    "" + "</td> <td class='td-very_short'>" + // Volumen
    "<h4>" + numberWithCommas(txbCostoUnitario_) + "</h4></td> <td class='td'>" + // Costo Unitario
    "<h4>" + numberWithCommas(txbPrecioUnitario_) + "</h4></td> <td class='td-very_short'>" + // Precio Unitario
    "<h4>" + numberWithCommas(txbCantidadTotal_) + "</h4></td> <td class='td-very_short'>" + // Cantidad total
    "<h4>" + numberWithCommas(txbCostoTotal_) + "</h4></td> <td class='td-very_short'>" + // Costo Total
    "<h4>" + numberWithCommas(txbPrecioSubtotal1_) + "</h4></td> <td class='td-very_short'>" + // Precio Subtotal 1
    "" + "</td> <td class='td-very_short'>" + // Descuento
    "<h4>" + numberWithCommas(txbPrecioSubtotal2_) + "</h4></td> <td class='td-very_short'>" + // Precio Subtotal 2
    "" + "</td> <td class='td-very_short'>" + // Redondeo
    "<h4>" + numberWithCommas(txbPrecioFinal_) + "</h4></td></tr>"); // Precio Final
}

function gridElementosSeleccionados_btnAgregar1(lblElementoID, lblElementoNumero, lblElementoNombre) {

    $("#gridElementosSeleccionados_blank").remove();

    var rowID = "gridElementosSeleccionados_" + lblElementoID;

    var btnQuitarID = "btnQuitar_" + lblElementoID;
    var btnQuitar = "<a id=\"" + btnQuitarID + "\" role='button' href='#' class='btn btn-danger btn-xs fa fa-arrow-circle-up fa-2x' onclick='return gridElementosSeleccionados_quitar(\"" + lblElementoID + "\", \"" + rowID + "\")'></a>";

    var btnCalcularVolumen = "<a id='btnCalcularVolumen_" + lblElementoID + "' role='button' href='#' class='btn btn-info btn-xs fa fa-calculator fa-2x' onclick='return show_calcularVolumen(\"" + lblElementoID + "\")'></a>";
    var input_volumen = "<input type='text' id='txbVolumen_" + lblElementoID + "' class='form-control' placeholder='Ingresar volumen CM2 + F/C' />";
    var chk_fondoColor = "<input type='checkbox' id='chkFondoColor_" + lblElementoID + "' class='form-control' title='Aplicar fondo color' />";

    var input_cantidadTotal = "<input type='text' id='txbCantidadTotal_" + lblElementoID + "' class='form-control' placeholder='Ingresar cantidad' value='1'/>";
    var input_costoUnitario = "<input type='text' id='txbCostoUnitario_" + lblElementoID + "' class='form-control' readonly/>";

    var input_precioUnitario = "<input type='text' id='txbPrecioUnitario_" + lblElementoID + "' class='form-control' readonly/>";
    var chk_precioSugerido = "<input type='checkbox' id='chkPrecioSugerido_" + lblElementoID + "' class='form-control' title='Usar precio sugerido'/>";

    var input_costoTotal = "<input type='text' id='txbCostoTotal_" + lblElementoID + "' class='form-control' readonly/>";

    var input_precioSubotal1 = "<input type='text' id='txbPrecioSubtotal1_" + lblElementoID + "' class='form-control' readonly/>";
    var input_descuento = "<input type='text' id='txbDescuento_" + lblElementoID + "' class='form-control' placeholder='Ingresar descuento' value='0'/>";
    var input_precioSubtotal2 = "<input type='text' id='txbPrecioSubtotal2_" + lblElementoID + "' class='form-control' readonly/>";
    var input_redondeo = "<input type='text' id='txbRedondeo_" + lblElementoID + "' class='form-control' placeholder='Ingresar redondeo' value='0'/>";
    var input_precioFinal = "<input type='text' id='txbPrecioFinal_" + lblElementoID + "' class='form-control' readonly/>";


    $("#gridElementosSeleccionados").append("<tr id='" + rowID + "'><td class='hiddencol hiddencol_real'>" +
    lblElementoID + "</td> <td class='td-very_short'>" +
    btnQuitar + "</td> <td class='td-very_short'>" +
    lblElementoNumero + "</td> <td class='td-very_short'>" +
    lblElementoNombre + "</td> <td class='td-very_short'>" +
    "</td> <td class='td' style='display: flex;'>" + // Comentarios
    btnCalcularVolumen + input_volumen + chk_fondoColor + "</td> <td class='td-very_short'>" + // Volumen
    input_costoUnitario + "</td> <td class='td' style='display: flex;'>" + // Costo Unitario
    input_precioUnitario + chk_precioSugerido + "</td> <td class='td-very_short'>" + // Precio Unitario
    input_cantidadTotal + "</td> <td class='td-very_short'>" + // Cantidad total
    input_costoTotal + "</td> <td class='td-very_short'>" + // Costo Total
    input_precioSubotal1 + "</td> <td class='td-very_short'>" + // Precio Subtotal 1
    input_descuento + "</td> <td class='td-very_short'>" + // Descuento
    input_precioSubtotal2 + "</td> <td class='td-very_short'>" + // Precio Subtotal 2
    input_redondeo + "</td> <td class='td-very_short'>" + // Redondeo
    input_precioFinal + "</td></tr>"); // Precio Final

    $("#gridElementosSeleccionados").append("</tbody>");

    gridElementosSeleccionados_sumarTotales();

    // Volver a cargar eventos sobre la grilla
    bindDelayEvents();
    bindOnChangeEvents(lblElementoID);

}

function bindOnChangeEvents(lblElementoID) {
    $("#txbVolumen_" + lblElementoID).change(function () {
        gridElementosSeleccionados_calcular(lblElementoID, false);
    });
    $("#txbCantidadTotal_" + lblElementoID).change(function () {
        gridElementosSeleccionados_calcular(lblElementoID, false);
    });
    $("#chkFondoColor_" + lblElementoID).change(function () {
        gridElementosSeleccionados_calcular(lblElementoID, this.checked);
    });
    $("#chkPrecioSugerido_" + lblElementoID).change(function () {
        gridElementosSeleccionados_setPrecioSugerido(lblElementoID, this.checked);
    });
    $("#txbDescuento_" + lblElementoID).change(function () {
        gridElementosSeleccionados_updateDescuento(lblElementoID);
    });
    $("#txbRedondeo_" + lblElementoID).change(function () {
        gridElementosSeleccionados_updateRedondeo(lblElementoID);
    });
}

function gridElementosSeleccionados_updateDescuento(lblElementoID) {
    var txbDescuento_ = $("#txbDescuento_" + lblElementoID);
    var txbPrecioSubtotal1_ = $("#txbPrecioSubtotal1_" + lblElementoID);
    var txbPrecioSubtotal2_ = $("#txbPrecioSubtotal2_" + lblElementoID);
    if (txbDescuento_ !== null && txbDescuento_ !== undefined &&
    txbPrecioSubtotal1_ !== null && txbPrecioSubtotal1_ !== undefined && txbPrecioSubtotal1_.val() != "" &&
        txbPrecioSubtotal2_ !== null && txbPrecioSubtotal2_ !== undefined) {
        var txbDescuento_value = TryParseFloat(txbDescuento_.val(), 10);
        var txbPrecioSubtotal1_value = numberRemoveCommas(txbPrecioSubtotal1_.val());
        var descuento = txbPrecioSubtotal1_value - (txbDescuento_value * txbPrecioSubtotal1_value / 100);
        txbPrecioSubtotal2_.val(numberWithCommas(descuento));

        gridElementosSeleccionados_sumarTotales();
    }
}

function gridElementosSeleccionados_updateRedondeo(lblElementoID) {
    var txbRedondeo_ = $("#txbRedondeo_" + lblElementoID);
    var txbPrecioSubtotal2_ = $("#txbPrecioSubtotal2_" + lblElementoID);
    var txbPrecioFinal_ = $("#txbPrecioFinal_" + lblElementoID);
    if (txbRedondeo_ !== null && txbRedondeo_ !== undefined &&
    txbPrecioSubtotal2_ !== null && txbPrecioSubtotal2_ !== undefined && txbPrecioSubtotal2_.val() != "" &&
        txbPrecioFinal_ !== null && txbPrecioFinal_ !== undefined) {
        var txbRedondeo_value = TryParseFloat(txbRedondeo_.val(), 10);
        var txbPrecioSubtotal2_value = numberRemoveCommas(txbPrecioSubtotal2_.val());
        var redondeo = txbPrecioSubtotal2_value - txbRedondeo_value;
        txbPrecioFinal_.val(numberWithCommas(redondeo));

        gridElementosSeleccionados_sumarTotales();
    }
}

function gridElementosSeleccionados_setPrecioSugerido(lblElementoID, isPrecioSugerido) {
    var input_precioUnitario = $("#txbPrecioUnitario_" + lblElementoID);
    var input_lblElementoPrecioSugerido = $("#input_lblElementoPrecioSugerido_" + lblElementoID);
    if (input_precioUnitario !== null && input_precioUnitario !== undefined &&
        input_lblElementoPrecioSugerido !== null && input_lblElementoPrecioSugerido !== undefined) {
        if (isPrecioSugerido) {
            var cantidad_pedido = $("#txbCantidadTotal_" + lblElementoID).val();
            if (cantidad_pedido !== null) {

                var input_lblElementoPrecioSugerido_value = input_lblElementoPrecioSugerido.val();
                input_precioUnitario.val(input_lblElementoPrecioSugerido_value);

                var precioSugerido_cantidad = numberRemoveCommas(input_lblElementoPrecioSugerido_value) * cantidad_pedido;

                if (!isNaN(precioSugerido_cantidad)) {
                    $("#txbCostoTotal_" + lblElementoID).val(0);
                    $("#txbPrecioSubtotal1_" + lblElementoID).val(numberWithCommas(precioSugerido_cantidad));

                    gridElementosSeleccionados_sumarTotales();
                }
            }
        } else { //else isPrecioSugerido
            var chkFondoColor = $("#chkFondoColor_" + lblElementoID);
            if (chkFondoColor !== null && chkFondoColor !== undefined) {
                gridElementosSeleccionados_calcular(lblElementoID, chkFondoColor.is(":checked"));               
            }
        }
    }
    
}

function show_calcularVolumen(lblElementoID) {
    var divPopbox = $("#divPopbox_calcularVolumen");
    var btnID = $("#btnCalcularVolumen_" + lblElementoID);
    if (divPopbox !== null && divPopbox !== undefined && btnID !== null && btnID !== undefined) {

        var new_w = parseInt(divPopbox.css("width"), 10);
        var new_h = parseInt(divPopbox.css("height"), 10);

        divPopbox.offset({ top: btnID.offset().top });
        divPopbox.offset({ left: btnID.offset().left - new_w });

        $("#divPopbox_calcularVolumen").show("highlight", 700);

        // Cargar acciones
        var input_divPopbox_calcularVolumen_OK = $("#input_divPopbox_calcularVolumen_OK");
        var txbLink = $("#txbLink");

        var divPopbox_calcularVolumen_OK = "javascript:divPopbox_calcularVolumen_OK(" + lblElementoID + ")";

        if (input_divPopbox_calcularVolumen_OK !== null && input_divPopbox_calcularVolumen_OK !== undefined){
            input_divPopbox_calcularVolumen_OK.attr('href', divPopbox_calcularVolumen_OK);
        }
    }
}

function divPopbox_calcularVolumen_OK(lblElementoID) {
    var input_divPopbox_calcularVolumen_Ancho = $("#input_divPopbox_calcularVolumen_Ancho").val();
    var input_divPopbox_calcularVolumen_Alto = $("#input_divPopbox_calcularVolumen_Alto").val();
    if(input_divPopbox_calcularVolumen_Ancho !== null && input_divPopbox_calcularVolumen_Ancho !== undefined && 
        input_divPopbox_calcularVolumen_Alto !== null && input_divPopbox_calcularVolumen_Alto !== undefined) {

        var new_an = parseInt(input_divPopbox_calcularVolumen_Ancho, 10);
        var new_al = parseInt(input_divPopbox_calcularVolumen_Alto, 10);

        if (!isNaN(new_an) && !isNaN(new_al)) {
            var result = new_an * new_al;
            $("#txbVolumen_" + lblElementoID).val(result);
            gridElementosSeleccionados_calcular(lblElementoID, false);

            $(".popbox-box.popbox").hide();
        }
    }
}

function bindGridElementos() {

    // Ajax call parameters
    console.log("Ajax call: Presupuestos.aspx/GetData_BindGridElementos. Params:");

    $.ajax({
        type: "POST",
        url: "Presupuestos.aspx/GetData_BindGridElementos",
        contentType: "application/json;charset=utf-8",
        //data: '{year_value: "' + year_value + '",month_value: "' + month_value + '",soloVigentes_value: "' + soloVigentes_value + '",soloEntrCol_value: "' + soloEntrCol_value + '",incluirCancelados_value: "' + incluirCancelados_value + '"}',
        dataType: "json",
        success: function (response) {

            $("#gridElementos tbody").remove();
            if (response.d.length > 0) {
                $("#gridElementos").empty();
                $("#gridElementos").append("<thead><tr><th class='hiddencol hiddencol_real' scope='col'>Elemento_ID</th> <th scope='col'></th> <th scope='col'>#</th> <th scope='col'>Nombre</th> <th scope='col'>$ Precio sugerido</th> <th scope='col'>Comentarios</th> </tr></thead><tbody>");
                for (var i = 0; i < response.d.length; i++) {

                    var lblElementoID = check_nullValues(response.d[i].lblElementoID);
                    var lblElementoNumero = check_nullValues(response.d[i].lblElementoNumero);
                    var lblElementoNombre = check_nullValues(response.d[i].lblElementoNombre);
                    var lblElementoPrecioSugerido = check_nullValues(response.d[i].lblElementoPrecioSugerido);
                    var lblElementoComentarios = check_nullValues(response.d[i].lblElementoComentarios);

                    var rowID = "gridElementos" + lblElementoID;

                    // Botón Agregar
                    var btnAgregar_id = "btnAgregar_" + lblElementoID;
                    var goBtnAgregar = "<a id=\"" + btnAgregar_id + "\" role='button' href='#' style='margin-right:5px;' title='Agregar producto' class='btn btn-success btn-xs fa fa-arrow-circle-down fa-2x' onclick='return gridElementosSeleccionados_btnAgregar1(\"" + lblElementoID + "\", \"" + lblElementoNumero + "\", \"" + lblElementoNombre + "\")'></a>";
                    // ----------------------

                    // Botón Historial
                    var btnHistorial_id = "btnHistorial_" + lblElementoID;
                    var goBtnHistorial = "<a id=\"" + btnHistorial_id + "\" role='button' href='#' style='margin-right:5px;' title='Ver historial' class='btn btn-warning btn-xs fa fa-history fa-2x' onclick='return gridElementos_btnHistorial(\"" + lblElementoID + "\")'></a>";
                    // ----------------------

                    var input_lblElementoPrecioSugerido = "<input type='text' id='input_lblElementoPrecioSugerido_" + lblElementoID + "' value='" + numberWithCommas(lblElementoPrecioSugerido) + "' class='form-control' readonly>";

                    $("#gridElementos").append("<tr><td class='hiddencol hiddencol_real'>" +
                    lblElementoID + "</td> <td class='td-very_short'>" +
                    goBtnAgregar + goBtnHistorial + "</td> <td class='td-very_short'>" +
                    lblElementoNumero + "</td> <td class='td-very_short'>" +
                    lblElementoNombre + "</td> <td class='td-very_short'>" +
                    input_lblElementoPrecioSugerido + "</td> <td class='td-very_short'>" +
                    lblElementoComentarios + "</td></tr>");

                } // for
                $("#gridElementos").append("</tbody>");

                // Volver a cargar eventos sobre la grilla
                bindDelayEvents();
            }

        }, // end success
        failure: function (response) {
        }
    }); // Ajax

}

function check_nullValues(value) {
    var value_return = value;
    if (value === null || value === undefined) {
        value_return = "";
    }
    return value_return;
}

function gridElementosSeleccionados_limpiarLista() {
    $("#gridElementosSeleccionados > tbody > tr").remove();
    $("#gridElementosSeleccionados > tfoot > tr").remove();
}