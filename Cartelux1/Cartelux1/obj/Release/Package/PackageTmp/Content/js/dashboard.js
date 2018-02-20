/**** Local variables ****/
var PAGO_ID_SELECTED;
var CLIENTE_ID_SELECTED;
var IS_MOBILE;

$(document).ready(function () {
    checkMobile();
    initVariables();
    bindEvents();
    bindDelayEvents();
});

// attach the event binding function to every partial update
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
    bindEvents();
    bindDelayEvents();
});

function DoubleScroll(element) {
    var scrollbar = document.createElement('div');
    scrollbar.appendChild(document.createElement('div'));
    scrollbar.style.overflow = 'auto';
    scrollbar.style.overflowY = 'hidden';
    scrollbar.style.width = 'auto';
    scrollbar.firstChild.style.width = element.scrollWidth + 'px';
    scrollbar.firstChild.style.paddingTop = '1px';
    scrollbar.firstChild.appendChild(document.createTextNode('\xA0'));
    scrollbar.onscroll = function () {
        element.scrollLeft = scrollbar.scrollLeft;
    };
    element.onscroll = function () {
        scrollbar.scrollLeft = element.scrollLeft;
    };
    element.parentNode.insertBefore(scrollbar, element);
}

window.onload = function () {
    DoubleScroll(document.getElementById('div_gridFormularios'));
}

function checkMobile() {
    var mobile = (/iphone|ipod|android|blackberry|mini|windows\sce|palm/i.test(navigator.userAgent.toLowerCase()));
    IS_MOBILE = false;
    if (mobile) {
        IS_MOBILE = true;
    }
}

function initVariables() {
    $("#ddl_year").prop('selectedIndex', 1);
    var year_value = $('#ddl_year :selected').text();

    if (IS_MOBILE) {
        $("#aCollapse_left_panel").show();
        $("#aCollapse_left_panel").click();
    }

    // Click first Month
    setTimeout(clickCurrentMonth, 50);
}

function bindEvents() {
    $("#tabsFormularios").tabs();
    $("#gridFormularios").filterTable();
    // SOURCE: https://github.com/sunnywalker/jQuery.FilterTable

    month_onClickEvent();
}

function bindDelayEvents() {
    setTimeout(function () {
        if ($("#gridFormularios tbody tr").length > 0) {
            //$("#gridFormularios").tablesorter();
        }
    }, 100);
}

function month_clearSelected() {
    $(".btn-table").removeClass("btn-success");
    $(".btn-table").removeClass("btn-primary");
    $(".btn-table").addClass("btn-primary");
}

function month_paintSelected(month_index) {
    $(".btn-table").filter("td[data-value='" + month_index + "']").toggleClass("btn-success");
}

function month_selectMonth(month_value, soloVigentes_value) {
    if (month_value !== null && month_value !== undefined && month_value > 0 && soloVigentes_value !== null && soloVigentes_value !== undefined) {
        
        month_setMonthName(month_value);
        month_clearSelected()
        month_paintSelected(month_value);

        var year_value = 2018; // DUMMY
        var ddl_year = $("#ddl_year option:selected");
        if (ddl_year !== null && ddl_year !== undefined && ddl_year.text() !== null && ddl_year.text() !== undefined) {
            year_value = ddl_year.text();

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
                        $("#gridFormularios").append("<thead><tr><th class='hiddencol hiddencol_real' scope='col'>Formulario_ID</th> <th class='hiddencol hiddencol_real' scope='col'>URL Form</th> <th scope='col'>#</th> <th scope='col'>Fecha de Entrega</th> <th scope='col'>Teléfono</th> <th scope='col'>Nombre</th> <th scope='col'>Tipo de Entrega</th> <th scope='col'>Tamaño</th> <th scope='col'>Tipo cartel</th> <th scope='col'>Material</th> <th scope='col'>Zona</th> <th scope='col'>URL_short</th> <th scope='col'>Ir al Form</th> <th scope='col'>Ir a WhatsApp</th></tr></thead><tbody>");
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
                            var date = moment(response.d[i].lblFechaEntrega, "DD-MM-YYYY").format("DD-MM-YYYY");

                            $("#gridFormularios").append("<tr><td class='hiddencol hiddencol_real'>" +
                            check_nullValues(response.d[i].Formulario_ID) + "</td> <td class='hiddencol hiddencol_real'>" +
                            check_nullValues(response.d[i].URL_short) + "</td> <td class='td-very_short'>" +
                            check_nullValues(response.d[i].lblNumber) + "</td> <td class='td-very_short'>" +
                            check_nullValues(date) + "</td> <td class='td-very_short'>" +
                            check_nullValues(response.d[i].lblTelefono) + "</td> <td class='td-very_short'>" +
                            check_nullValues(response.d[i].lblNombre) + "</td> <td class='td-very_short'>" +
                            check_nullValues(response.d[i].lblTipoEntrega) + "</td> <td class='td-very_short'>" +
                            check_nullValues(response.d[i].lblTamano) + "</td> <td class='td-very_short'>" +
                            check_nullValues(response.d[i].lblTipo) + "</td> <td class='td-very_short'>" +
                            check_nullValues(response.d[i].lblMaterial) + "</td> <td class='td-very_short'>" +
                            check_nullValues(response.d[i].lblZona) + "</td><td class='td-short'>" +
                            check_nullValues(response.d[i].URL_short) + "</td><td class='td-very_short'>" +
                            goToURL + "</td><td class='td-very_short'>" +
                            goToWPP + "</td></tr>");
                        } // for
                        $("#gridFormularios").append("</tbody>");
                    }

                }, // end success
                failure: function (response) {
                }
            }); // Ajax
        }
    }
}

function check_nullValues(value) {
    var value_return = value;
    if (value === null || value === undefined) {
        value_return = "";
    }
    return value_return;
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

function clickCurrentMonth() {
    var d = new Date();
    var m = d.getMonth() + 1;
    if (m <= 12 && $("#tr-id-" + m + " td")[0] !== null) {
        $("#tr-id-" + m + " td")[0].click();
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

                var month_value_int = month_value;
                if (type(month_value) === "string") {
                    month_value_int = TryParseInt(month_value, 0);
                }
                $("#chbSoloVigentes").prop('checked', false);
                month_selectMonth(month_value_int, true);

                if (IS_MOBILE) {
                    $("#aCollapse_left_panel").show();
                    $("#aCollapse_left_panel").click();
                }
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

        var month_value_int = month_value;
        if (type(month_value) === "string") {
            month_value_int = TryParseInt(month_value, 0);
        }
        month_selectMonth(month_value_int, !soloVigentes_value)
    }
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

