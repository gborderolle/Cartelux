var _CLIENTES = 0;
var _PEDIDOS = 0;

$(document).ready(function () {
    show_tabla();

    load_quicksearch();
    show_dato();
});

function load_quicksearch() {
    // Source: https://www.youtube.com/watch?v=Sy2J7cUv0QM

    var gridClientes1 = $("#gridClientes1 tbody tr");
    var gridPedidos = $("#gridPedidos tbody tr");

    if (gridClientes1 !== null) {
        $("#txbSearch").quicksearch(gridClientes1);
    }
    if (gridPedidos !== null) {
        $("#txbSearch").quicksearch(gridPedidos);
    }
}

function show_tabla() {
    if (QueryString !== null) {
        if (QueryString['tabla'] !== null && QueryString['tabla'] !== "") {
            var tabla = QueryString['tabla'];
            switch (tabla) {
                case "pedidos": {
                    $("#divPedidos").show();
                    _PEDIDOS = 1;
                    break;
                }
                case "clientes": {
                    $("#divClientes").show();
                    _CLIENTES = 1;
                    break;
                }
            }
        }
    }
}

function show_dato() {
    if (QueryString !== null) {
        if (_CLIENTES === 1) {
            if (QueryString['dato'] !== null && QueryString['dato'] !== "") {
                var dato = QueryString['dato'];
                $("#txbSearch").val(dato);
                $("#txbSearch").trigger("keyup");
            }
        }
    }
}

var QueryString = function () {
    // This function is anonymous, is executed immediately and 
    // the return value is assigned to QueryString!
    var query_string = {};
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        // If first entry with this name
        if (typeof query_string[pair[0]] === "undefined") {
            query_string[pair[0]] = decodeURIComponent(pair[1]);
            // If second entry with this name
        } else if (typeof query_string[pair[0]] === "string") {
            var arr = [query_string[pair[0]], decodeURIComponent(pair[1])];
            query_string[pair[0]] = arr;
            // If third or later entry with this name
        } else {
            query_string[pair[0]].push(decodeURIComponent(pair[1]));
        }
    }
    return query_string;
}();

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
    DoubleScroll(document.getElementById('gridClientes1'));
    DoubleScroll(document.getElementById('gridPedidos'));
}
