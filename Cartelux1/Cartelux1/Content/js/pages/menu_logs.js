$(document).ready(function () {
    bindEvents();
    bindDelayEvents();

    // Seleccionar primer cliente
    var first = $("#gridClientes tbody tr").first();
    if (first != null) {
        first.click();
    }
});


function bindEvents() {
    $("#gridLogs").tablesorter();
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });

}

function bindDelayEvents() {
    setTimeout(function () {

        /* Formularios */
        $("#gridLogs").tablesorter();
        var gridLogs = $("#gridLogs tbody tr");
        $("#txbSearchPedidos").quicksearch(gridLogs);

    }, 100);
}