/**** Local variables ****/
var PAGO_ID_SELECTED;
var CLIENTE_ID_SELECTED;

$(document).ready(function () {
    initVariables();
    bindEvents();
});

// attach the event binding function to every partial update
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
    bindEvents();
});

function month_selectMonth(month_value, soloVigentes_value) {
    if (month_value !== null && month_value > 0) {
        
        month_setMonthName(month_value);

        var year_value = 2018; // DUMMY
        var hdn_yearSelected = $("#hdn_yearSelected");
        if (hdn_yearSelected !== null) {
            year_value = hdn_yearSelected.val();
        }

        // Source: https://www.codeproject.com/Tips/775585/Bind-Gridview-using-AJAX
        // Ajax call parameters
        console.log("Ajax call: Dashboard.aspx/GetData_BindGridFormularios. Params:");
        console.log("year_value, type: " + type(year_value) + ", value: " + year_value);
        console.log("month_value, type: " + type(month_value) + ", value: " + month_value);
        console.log("soloVigentes_value, type: " + type(soloVigentes_value) + ", value: " + soloVigentes_value);
        
        $.ajax({
            type: "POST",
            url: "Dashboard.aspx/GetData_BindGridFormularios",
            contentType: "application/json;charset=utf-8",
            data: '{year_value: "' + year_value + '",month_value: "' + month_value + '",soloVigentes_value: "' + soloVigentes_value + '"}',
            dataType: "json",
            success: function (response) {

                $("#gridFormularios").empty();

                if (response.d.length > 0) {
                    $("#gridFormularios").append("<thead><tr><th class='hiddencol hiddencol_real' scope='col'>Formulario_ID</th> <th class='hiddencol hiddencol_real' scope='col'>URL_short</th> <th scope='col'>Teléfono</th> <th scope='col'>Nombre</th> <th scope='col'>Fecha de Entrega</th> <th scope='col'>Tipo de Entrega</th> <th scope='col'>Tamaño</th> <th scope='col'>Tipo cartel</th> <th scope='col'>Material</th> <th scope='col'>Zona</th> <th scope='col'>URL_short</th> <th scope='col'>Ir al Form</th> <th scope='col'>Ir a WhatsApp</th></tr></thead>");
                    for (var i = 0; i < response.d.length; i++) {
                        var goToURL = "<a id='btnURL' role='button' href='" + response.d[i].URL_short + "' class='btn btn-info glyphicon glyphicon-share-alt' title='' target='_blank'></a>";

                        var tel = response.d[i].lblTelefono;
                        // Si el número empieza con 0 lo borra
                        var first = tel.charAt(0);
                        if (first === "0") {
                            tel = tel.substring(1);
                        }

                        var url = "https://api.whatsapp.com/send?phone=598" + tel;
                        url += "&text=" + hashMessages["Msj_inicioCliente"];

                        var goToWPP = "<a id='btnURL' role='button' href='" + url + "' class='btn btn-info btn-xs fa fa-whatsapp fa-2x' title='' target='_blank'></a>";
                        var date = moment(response.d[i].lblFechaEntrega, "MM-DD-YYYY").format("DD-MM-YYYY");

                        $("#gridFormularios").append("<tr><td class='hiddencol hiddencol_real'>" +
                        response.d[i].Formulario_ID + "</td> <td class='hiddencol hiddencol_real'>" +
                        response.d[i].URL_short + "</td> <td>" +
                        response.d[i].lblTelefono + "</td> <td>" +
                        response.d[i].lblNombre + "</td> <td>" +
                        date + "</td> <td>" +
                        response.d[i].lblTipoEntrega + "</td> <td>" +
                        response.d[i].lblTamano + "</td> <td>" +
                        response.d[i].lblTipo + "</td> <td>" +
                        response.d[i].lblMaterial + "</td> <td>" +
                        response.d[i].lblZona + "</td><td>" +
                        response.d[i].URL_short + "</td><td>" +
                        goToURL + "</td><td>" +
                        goToWPP + "</td></tr>");
                    }
                }


            }, // end success
            failure: function (response) {
            }
        }); // Ajax

    }
}

function month_setMonthName(month_value) {
    if (month_value !== null && month_value > 0) {

        var month_name = "Enero";
        switch (month_value) {
            case 1:
                {
                    month_name = "Enero";
                    break;
                }
            case 2:
                {
                    month_name = "Febrero";
                    break;
                }
            case 3:
                {
                    month_name = "Marzo";
                    break;
                }
            case 4:
                {
                    month_name = "Abril";
                    break;
                }
            case 5:
                {
                    month_name = "Mayo";
                    break;
                }
            case 6:
                {
                    month_name = "Junio";
                    break;
                }
            case 7:
                {
                    month_name = "Julio";
                    break;
                }
            case 8:
                {
                    month_name = "Agosto";
                    break;
                }
            case 9:
                {
                    month_name = "Septiembre";
                    break;
                }
            case 10:
                {
                    month_name = "Octubre";
                    break;
                }
            case 11:
                {
                    month_name = "Noviembre";
                    break;
                }
            case 12:
                {
                    month_name = "Diciembre";
                    break;
                }
        }
        $("#lblMonth").text(month_name);
    }
}

function setupMonthPicker() {
    // Fecha de pago customización
    $('#add_txbFecha').datepicker({
        //changeMonth: false,
        //changeYear: false,
        showButtonPanel: false,
        dateFormat: 'dd-mm-yy'
    });

    // setup date format
    var d = new Date();
    var n = d.getMonth() + 1;
    var y = d.getFullYear();

    $('#add_txbFecha').datepicker("setDate", new Date(d.getFullYear(), $("#hdn_monthSelected").val() - 1, d.getDate()));
}

function initVariables() {
    $("#ddl_year").prop('selectedIndex', 1);
    var year_value = $('#ddl_year :selected').text();

    var hdn_yearSelected = $("#hdn_yearSelected");
    if (hdn_yearSelected !== null) {
        hdn_yearSelected.val(year_value);
    }

    // Click first Month
    setTimeout(clickCurrentMonth, 50);
}

function clickCurrentMonth() {
    var d = new Date();
    var m = d.getMonth() + 1;
    if (m <= 12 && $("#tr-id-" + m + " td")[1] !== null) {
        $("#tr-id-" + m + " td")[1].click();
    }
}

function loadDDLEvents() {
    $(".add_ddlFormas").val('').prop('disabled', true).trigger("liszt:updated");

    $("#editModal_rad_cliente").on('change', function () {
        if ($('input[name=edit_rad_cliente]:checked').val() === "pago") {
            $(".edit_ddlFormas").val('').prop('disabled', false).trigger("liszt:updated");
        } else {
            $(".edit_ddlFormas").val('').prop('disabled', true).trigger("liszt:updated");
        }
    });
}

function month_onClickEvent() {
    $("#gridMonth td").click(function () {
        var month_value = $(this).data('value');
        if (month_value !== null && month_value > 0) {
            var hdn_monthSelected = $("#hdn_monthSelected");
            if (hdn_monthSelected !== null && chbSoloVigentes !== null) {
                hdn_monthSelected.val(month_value);
                month_selectMonth(month_value, true)
            }
        }
    });
}

function filtrar_soloVigentes() {
    var chbSoloVigentes = $("#chbSoloVigentes");
    var hdn_monthSelected = $("#hdn_monthSelected");
    if (hdn_monthSelected !== null && hdn_monthSelected.val() !== null && chbSoloVigentes !== null) {
        var soloVigentes_value = chbSoloVigentes.is(":checked")
        var month_value = hdn_monthSelected.val();
        month_selectMonth(month_value, soloVigentes_value);
    }
}


function bindEvents() {
    $("#tabsPedidos").tabs();
    $("#tabsFormularios").tabs();

    month_onClickEvent();
}

function GetMonthFilter() {
    var txbMonthpicker = $('#txbMonthpicker').MonthPicker('GetSelectedMonth');
    var txbYearpicker = $('#txbMonthpicker').MonthPicker('GetSelectedYear');

    if (isNaN(txbMonthpicker)) {
        var d = new Date();
        var n = d.getMonth() + 1;
        txbMonthpicker = n;

        txbYearpicker = d.getYear();
    }
    var hdn_monthSelected = $("#hdn_monthSelected");
    if (hdn_monthSelected !== null && hdn_monthSelected.val() !== null && txbMonthpicker !== null) {

        var value = txbMonthpicker + "|" + txbYearpicker;
        hdn_monthSelected.val(value);
    }
}

