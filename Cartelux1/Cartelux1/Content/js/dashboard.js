/**** Local variables ****/
var PAGO_ID_SELECTED;
var CLIENTE_ID_SELECTED;

$(document).ready(function () {
    //initVariables();
    bindEvents();

    // Seleccionar primer cliente
    var first = $("#gridClientes tbody tr").first();
    if (first !== null) {
        first.click();
    }
});

// attach the event binding function to every partial update
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
    bindEvents();
});

function month_selectMonth(month_value) {
    if (month_value !== null && month_value > 0) {
        
        month_setMonthName(month_value);

        var hdn_monthSelected = $("#hdn_monthSelected");
        if (hdn_monthSelected !== null && hdn_monthSelected.val() !== null && hdn_monthSelected != null) {
            hdn_monthSelected.val(month_value);
        }


        // Ajax call parameters
        console.log("Ajax call: Dashboard.aspx/SelectMonth. Params:");
        console.log("month_value, type: " + type(month_value) + ", value: " + month_value);

        // Check existen mercaderías
        $.ajax({
            type: "POST",
            url: "Dashboard.aspx/SelectMonth",
            data: '{month_value: "' + month_value + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var resultado = response.d;

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

    $('#add_txbFecha').datepicker("setDate", new Date(d.getFullYear(), $("#hdn_txbMonthpicker").val() - 1, d.getDate()));
}

function initVariables() {
    var d = new Date();
    var m = d.getMonth() + 1;
    $("#hdn_txbMonthpicker").val(m);
}

function loadDDLEvents() {
    $(".add_ddlFormas").val('').prop('disabled', true).trigger("liszt:updated");

    $("#editModal_rad_cliente").on('change', function () {
        if ($('input[name=edit_rad_cliente]:checked').val() == "pago") {
            $(".edit_ddlFormas").val('').prop('disabled', false).trigger("liszt:updated");
        } else {
            $(".edit_ddlFormas").val('').prop('disabled', true).trigger("liszt:updated");
        }
    });
}

function loadInputDDL() {
    // Dropdownlist input
    $(".chzn-select").chosen();
    $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
}

function loadFilter_CurrentMonth() {
    var date = new Date();
    var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);

    $(".txbFiltro_saldos1").val(moment(firstDay).format("DD-MM-YYYY"));
    $(".txbFiltro_saldos2").val(moment(lastDay).format("DD-MM-YYYY"));
}

function addToday(tipo) {
    var date = moment(new Date()).format("DD");
    //var date = moment(new Date()).format("DD-MM-YYYY");
    //var date = $.datepicker.formatDate('dd/mm/yy', new Date());
    switch (tipo) {
        case 1: {
            $("#add_txbFecha").val(date);
            break;
        }
        case 2: {
            $("#edit_txbFecha").val(date);
            break;
        }
    }
}

function month_onClickEvent() {
    $("#gridMonth td").click(function () {
        var month_value = $(this).data('value');
        if (month_value !== null && month_value > 0) {
            month_selectMonth(month_value)
        }
    });
}


function bindEvents() {
    $("#tabsPedidos").tabs();

    month_onClickEvent();

    //$(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
    //$("#gridClientes").tablesorter();
    //$("#gridViajes").tablesorter();
    //$("#gridViajesImprimir").tablesorter();
    //$("#gridPagos").tablesorter();

    //// setup date format
    //var d = new Date();
    //var n = d.getMonth() + 1;
    //var y = d.getFullYear();
    //$('#txbMonthpicker').MonthPicker({ StartYear: y, ShowIcon: true });

    //$("#gridPagos tr").click(function () {
    //    PAGO_ID_SELECTED = $(this).find(".hiddencol").html();
    //});

    //$("#gridClientes tr").click(function () {
    //    CLIENTE_ID_SELECTED = $(this).find(".hiddencol").html();
    //    //$(this).css("background-color", "black");
    //    //$(this).find("td").css("background-color", "black");
    //});


    //// Source: https://www.youtube.com/watch?v=Sy2J7cUv0QM
    //var gridClientes = $("#gridClientes tbody tr");
    //$("#txbSearchClientes").quicksearch(gridClientes);

    //var gridViajes = $("#gridViajes tbody tr");
    //$("#txbSearchViajes").quicksearch(gridViajes);

    //var gridPagos = $("#gridPagos tbody tr");
    //$("#txbSearchPagos").quicksearch(gridPagos);

    //$('#txbSearchClientes').keydown(function () {
    //    var count = "# " + $('#gridClientes tbody tr:visible').length;
    //    $("#lblGridClientesCount").text(count);
    //});

    //$('#txbSearchViajes').keydown(function () {
    //    var count = "# " + $('#gridViajes tbody tr:visible').length;
    //    $("#lblGridViajesCount").text(count);
    //});

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
    var hdn_txbMonthpicker = $("#hdn_txbMonthpicker");
    if (hdn_txbMonthpicker !== null && hdn_txbMonthpicker.val() !== null && txbMonthpicker !== null) {

        var value = txbMonthpicker + "|" + txbYearpicker;
        hdn_txbMonthpicker.val(value);
    }
}

