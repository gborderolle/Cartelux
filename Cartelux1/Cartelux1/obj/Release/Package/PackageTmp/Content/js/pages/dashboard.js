/**** Local variables ****/
var IS_MOBILE;
var EVENTS_LIST = [];
var MONTH_SELECTED;
var YEAR_SELECTED;
var USER_ID;

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

// SOURCE: https://www.jqueryscript.net/time-clock/Simple-jQuery-Calendar-Schedule-Plugin-For-Bootstrap-Bic-Calendar.html
function loadCalendar() {
    months_list = ["ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DIC"];
    days_list = ["LUN", "MAR", "MIE", "JUE", "VIE", "SAB", "DOM"];

    $('#calendar').empty();
    var day = 1 + "";
    var month = MONTH_SELECTED + "";
    var year = YEAR_SELECTED;
    var date_comlete = day + "/" + month + "/" + year;

    $('#calendar').bic_calendar({
        // the date to display, as a string in dd/MM/YYYY format, or as a Date object
        date: moment(date_comlete, "DD/MM/YYYY").format("DD/MM/YYYY"),

        events: EVENTS_LIST,
        enableSelect: false,
        dayNames: days_list,
        monthNames: months_list,
        showDays: true,
        displayMonthController: false,
        displayYearController: false,                                
        //set ajax call
        //reqAjax: {
        //    type: 'get',
        //    url: 'http://bic.cat/bic_calendar/index.php'
        //}
    });
}

/**
 * Onclick of Close in AddEvent Box.
 */
function rzCloseAddEvent() {
    ical.closeAddEvent();
}

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
    //DoubleScroll(document.getElementById('div_gridFormularios'));
}

function checkMobile() {
    var mobile = (/iphone|ipod|android|blackberry|mini|windows\sce|palm/i.test(navigator.userAgent.toLowerCase()));
    IS_MOBILE = false;
    if (mobile) {
        IS_MOBILE = true;
    }
}

function initVariables() {
    var year = (new Date()).getFullYear() + "";

    var dd = document.getElementById('ddl_year');
    for (var i = 0; i < dd.options.length; i++) {
        if (dd.options[i].text === year) {
            dd.selectedIndex = i;
            break;
        }
    }

    //$("#ddl_year").prop('selectedIndex', 1);
    var year_value = $('#ddl_year :selected').text();    

    if (IS_MOBILE) {
        $("#aCollapse_left_panel").show();
        $("#aCollapse_left_panel").click();
    }

    setTimeout(trigger_events, 50);
}

function bindEvents() {
    $("#tabsFormularios").tabs();
    $("#gridFormularios").filterTable();
    // SOURCE: https://github.com/sunnywalker/jQuery.FilterTable

    $("#gridProyectos").filterTable();

    month_onClickEvent();
    //load_calendar();

    $(".close").click(function () {
        $(this).closest(".popbox").hide();
    })   
}

function load_calendar() {
    $('#example3').glDatePicker(
{
    showAlways: true,
    cssName: 'darkneon',
    selectedDate: new Date(2013, 0, 5),
    specialDates: [
        {
            date: new Date(2013, 0, 8),
            data: { message: 'Meeting every day 8 of the month' },
            repeatMonth: true
        },
        {
            date: new Date(0, 0, 1),
            data: { message: 'Happy New Year!' },
            repeatYear: true
        },
    ],
    onClick: function (target, cell, date, data) {
        target.val(date.getFullYear() + ' - ' +
                    date.getMonth() + ' - ' +
                    date.getDate());

        if (data !== null) {
            alert(data.message + '\n' + date);
        }
    }
});
}

function bindDelayEvents() {
    setTimeout(function () {

        /* Formularios */
        $("#gridFormularios").tablesorter();
        var gridFormularios = $("#gridFormularios tbody tr");
        $("#txbSearchPedidos").quicksearch(gridFormularios);

    }, 100);
}

function bindDelayEvents_proy() {
    setTimeout(function () {

        /* Proyectos */
        $("#gridProyectos").tablesorter();
        var gridProyectos = $("#gridProyectos tbody tr");
        $("#txbSearchPedidos_proy").quicksearch(gridProyectos);

    }, 100);
}

function month_clearSelected() {
    $(".btn-table").removeClass("btn-warning");
    $(".btn-table").removeClass("btn-info");
    $(".btn-table").addClass("btn-info");
}

function month_paintSelected(month_index) {
    $(".btn-table").filter("td[data-value='" + month_index + "']").toggleClass("btn-warning");
}

function month_selectMonth(month_value, soloVigentes_value, soloEntrCol_value, incluirCancelados_value) {
    if (month_value !== null && month_value !== undefined && month_value > 0 && soloVigentes_value !== null && soloVigentes_value !== undefined &&
        soloEntrCol_value !== null && soloEntrCol_value !== undefined && incluirCancelados_value !== null && incluirCancelados_value !== undefined) {
        
        month_setMonthName(month_value);
        month_clearSelected()
        month_paintSelected(month_value);

        month_value += "";

        var year_value = 2018; // DUMMY
        var ddl_year = $("#ddl_year option:selected");
        if (ddl_year !== null && ddl_year !== undefined && ddl_year.text() !== null && ddl_year.text() !== undefined) {
            year_value = ddl_year.text();
            YEAR_SELECTED = year_value;

            /* Formularios */

            // Source: https://www.codeproject.com/Tips/775585/Bind-Gridview-using-AJAX
            // Ajax call parameters
            console.log("Ajax call: Dashboard.aspx/GetData_BindGridFormularios. Params:");
            console.log("year_value, type: " + type(year_value) + ", value: " + year_value);
            console.log("month_value, type: " + type(month_value) + ", value: " + month_value);
            console.log("soloVigentes_value, type: " + type(soloVigentes_value) + ", value: " + soloVigentes_value);
            console.log("soloEntrCol_value, type: " + type(soloEntrCol_value) + ", value: " + soloEntrCol_value);
            console.log("incluirCancelados_value, type: " + type(incluirCancelados_value) + ", value: " + incluirCancelados_value);

            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetData_BindGridFormularios",
                contentType: "application/json;charset=utf-8",
                data: '{year_value: "' + year_value + '",month_value: "' + month_value + '",soloVigentes_value: "' + soloVigentes_value + '",soloEntrCol_value: "' + soloEntrCol_value + '",incluirCancelados_value: "' + incluirCancelados_value + '"}',
                dataType: "json",
                success: function (response) {

                    //$("#gridFormularios").empty();
                    $("#gridFormularios tbody").remove();
                    if (response.d.length > 0) {
                        $("#gridFormularios").empty();
                        $("#gridFormularios").append("<thead><tr><th class='hiddencol hiddencol_real' scope='col'>Formulario_ID</th> <th class='hiddencol hiddencol_real' scope='col'>URL Form</th> <th scope='col'>#</th> <th scope='col'>Fecha</th> <th scope='col'>Teléfono</th> <th scope='col'>Nombre</th> <th scope='col'>T/Entrega</th> <th scope='col'>Tamaño</th> <th scope='col'>T/Cartel</th> <th scope='col'>Temática</th> <th scope='col'>MedioP</th> <th scope='col'>$ Monto</th> <th scope='col'>Us</th> <th scope='col'>CTO</th> <th scope='col'>OPC</th> </tr></thead><tbody>"); //<th scope='col'>GMaps</th> <th scope='col'>Form</th> <th scope='col'>WPP</th> //<th scope='col'>Cant</th> <th scope='col'>Zona</th> <th scope='col'>¿Archivo?</th> <th scope='col'>Diseño referido</th>
                        for (var i = 0; i < response.d.length; i++) {

                            //var goToFormulario = "<a id='btnURL' role='button' href='" + response.d[i].URL_short + "' class='btn btn-warning glyphicon fa fa-wpforms' title='' target='_blank'></a>";
                            var goToFormulario = response.d[i].URL_short;

                            //var goToGMaps = "<a id='btnGMaps' role='button' href='" + response.d[i].URL_gmaps + "' class='btn btn-warning fa fa-map' title='' target='_blank'></a>";
                            var goToGMaps = response.d[i].URL_gmaps;

                            var telefono = response.d[i].lblTelefono;
                            // Si el número empieza con 0 lo borra
                            var first = telefono.charAt(0);
                            if (first === "0") {
                                telefono = telefono.substring(1);
                            }

                            // Tipo de entregas:
                            // 0: Colocación
                            // 1: Entrega
                            // 2: Envío al interior
                            // 3: Retiro taller

                            // Estados de pedidos:
                            // 0: Vigente - gris
                            // 1: Concluído - verde
                            // 2: Cancelado - rojo
                            // 3: Eliminado - rojo
                            // 4: Diseño aprobado, pronto para imprimir - azul

                            var text_color = "style='color:#333;'";
                            var estadoNro = check_nullValues(response.d[i].EstadoNro);
                            if (estadoNro === "") {
                                text_color = "style='color:green;'";
                            } else {
                                switch (estadoNro) {
                                    case 0:
                                        {
                                            text_color = "style='color:#333;'";
                                            break;
                                        }
                                    case 1:
                                        {
                                            text_color = "style='color:green;'";
                                            break;
                                        }
                                    case 2:
                                        {
                                            text_color = "style='color:red;'";
                                            break;
                                        }
                                    case 3:
                                        {
                                            text_color = "style='color:red;'";
                                            break;
                                        }
                                    case 4:
                                        {
                                            text_color = "style='color:blue;'";
                                            break;
                                        }
                                }
                            }
                            console.log(estadoNro);
                            console.log(text_color);

                            var lblTipoCodigo = check_nullValues(response.d[i].lblTipoCodigo);
                            if (lblTipoCodigo !== null || lblTipoCodigo !== "") {
                                if (lblTipoCodigo === 3 || lblTipoCodigo === 4) {
                                    //goToGMaps = "<a id='btnGMaps' role='button' href='#' class='btn btn-warning fa fa-map disabled' title='' disabled='disabled' aria-disabled='true'></a>";
                                }
                            }

                            var url = "https://api.whatsapp.com/send?phone=598" + telefono;
                            //url += "&text=" + hashMessages["Msj_inicioCliente"];

                            //var goToWPP = "<a id='btnWPP' role='button' href='" + url + "' class='btn btn-warning btn-xs fa fa-whatsapp fa-2x' title='' target='_blank'></a>";
                            var goToWPP = url;

                            var formID = check_nullValues(response.d[i].Formulario_ID);
                            var nombre = check_nullValues(response.d[i].lblNombre);

                            // Botón OPC - Opciones
                            var btnOPC_id = "btnOPC_" + formID;
                            console.log("goToOPC values");
                            console.log(formID);
                            console.log(nombre);
                            console.log(telefono);
                            var goToOPC = "<a id=\"" + btnOPC_id + "\" role='button' href='#' class='btn btn-info btn-xs fa fa-asterisk fa-2x' onclick='return showActionMenu_OPC(\"" + formID + "\", \"" + telefono + "\", \"" + nombre + "\", \"" + btnOPC_id + "\")'></a>";
                            // ----------------------

                            // Botón CTO - Contacto
                            var btnCTO_id = "btnCTO_" + formID;
                            console.log("goToCTO values");
                            console.log(goToFormulario);
                            console.log(goToGMaps);
                            console.log(goToWPP);
                            console.log("-------------- END --------------");
                            var goToCTO = "<a id=\"" + btnCTO_id + "\" role='button' href='#' class='btn btn-warning btn-xs fa fa-address-card fa-2x' onclick='return showActionMenu_CTO(\"" + formID + "\", \"" + nombre + "\", \"" + goToFormulario + "\", \"" + goToGMaps + "\", \"" + goToWPP + "\", \"" + btnCTO_id + "\")'></a>";
                            // ----------------------

                            var date = moment(response.d[i].lblFechaEntrega, "DD-MM-YYYY").format("DD-MM-YYYY");
                            var tel1 = check_nullValues(response.d[i].lblTelefono);


                            $("#gridFormularios").append("<tr><td class='hiddencol hiddencol_real' " + text_color + ">" +
                            formID + "</td> <td class='hiddencol hiddencol_real' " + text_color + ">" +
                            //check_nullValues(response.d[i].URL_short) + "</td> <td class='td-very_short' " + text_color + "><a href='Listados.aspx?tabla=pedidos&dato=" + tel1 + "'>" +
                            check_nullValues(response.d[i].URL_short) + "</td> <td class='td-very_short' " + text_color + "><a href='Listados.aspx?tabla=pedidos&dato=" + formID + "'>" +
                            check_nullValues(response.d[i].lblNumero) + "</a></td> <td class='td-very_short' " + text_color + ">" +
                            // Listados.aspx?tabla=" + tabla + "&dato=" + dato);
                            check_nullValues(date) + "</td> <td class='td-very_short' " + text_color + "><a href='Listados.aspx?tabla=clientes&dato="+ tel1 +"'>" +
                            tel1 + "</a></td> <td class='td-very_short' " + text_color + ">" +
                            nombre + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblTipoEntrega) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblTamano) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblTipo) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblTematica) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblMedioP) + "</td> <td class='td-very_short price' " + text_color + ">$ " +
                            check_nullValues(response.d[i].lblMonto) + ",00</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblUsuario) + "</td> <td class='td-very_short' " + text_color + ">" +
                            //check_nullValues(response.d[i].lblCantidad) + "</td> <td class='td-very_short' " + text_color + ">" +
                            //check_nullValues(response.d[i].lblZona) + "</td><td class='td-short' " + text_color + ">" +
                            //check_nullValues(convertBool(response.d[i].chbTieneBosquejo)) + "</td><td class='td-short' " + text_color + ">" +
                            //check_nullValues(convertBool(response.d[i].lblDisenoReferido)) + "</td><td class='td-short'>" +
                            //goToGMaps + "</td><td class='td-very_short'>" +
                            //goToFormulario + "</td><td class='td-very_short'>" +
                            //goToWPP + "</td><td class='td-very_short'>" +
                            goToCTO + "</td><td class='td-very_short'>" +
                            goToOPC + "</td></tr>");

                            create_calendar_events(response.d[i]);

                        } // for
                        $("#gridFormularios").append("</tbody>");

                        // Volver a cargar eventos sobre la grilla
                        bindDelayEvents();
                    }

                    // Load calendario completo
                    loadCalendar();

                }, // end success
                failure: function (response) {
                }
            }); // Ajax


            // ------------------ ------------------ ------------------ ------------------ ------------------ ------------------

            /* Proyectos */

            // Source: https://www.codeproject.com/Tips/775585/Bind-Gridview-using-AJAX
            // Ajax call parameters
            console.log("Ajax call: Dashboard.aspx/GetData_BindGridProyectos. Params:");
            console.log("year_value, type: " + type(year_value) + ", value: " + year_value);
            console.log("month_value, type: " + type(month_value) + ", value: " + month_value);

            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetData_BindGridProyectos",
                contentType: "application/json;charset=utf-8",
                data: '{year_value: "' + year_value + '",month_value: "' + month_value + '"}',
                dataType: "json",
                success: function (response) {

                    $("#gridProyectos tbody").remove();
                    if (response.d.length > 0) {
                        $("#gridProyectos").empty();
                        $("#gridProyectos").append("<thead><tr><th class='hiddencol hiddencol_real' scope='col'>Proyecto_ID</th> <th scope='col'>#</th> <th scope='col'>Nombre</th> <th scope='col'>Descripción</th> <th scope='col'>Fecha estimada</th> <th scope='col'>T/Contacto 1</th> <th scope='col'>Tel 1</th> <th scope='col'>Email 1</th> <th scope='col'>Contacto 2</th> <th scope='col'>Tel 2</th> <th scope='col'>Email 2</th> <th scope='col'>Comentarios</th> <th scope='col'>Estado</th> </tr></thead><tbody>");
                        for (var i = 0; i < response.d.length; i++) {

                            var proyID = check_nullValues(response.d[i].Proyecto_ID);
                            var nombre = check_nullValues(response.d[i].lblNombre_proy);

                            $("#gridProyectos").append("<tr><td class='hiddencol hiddencol_real' " + text_color + ">" +
                            proyID + "</td> <td class='hiddencol hiddencol_real' " + text_color + ">" +
                            check_nullValues(response.d[i].lblNumber_proy) + "</td> <td class='td-very_short' " + text_color + ">" +
                            nombre + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblDescripcion_proy) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblFechaEstimada) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblContacto1) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblTelefono1) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblEmail1) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblContacto2) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblTelefono2) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblEmail2) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblComentarios_proy) + "</td> <td class='td-very_short' " + text_color + ">" +
                            check_nullValues(response.d[i].lblEstado_proy) + "</td> </tr>");

                        } // for
                        $("#gridProyectos").append("</tbody>");


                        // Volver a cargar eventos sobre la grilla
                        bindDelayEvents_proy();
                    }

                    // Load calendario completo
                    loadCalendar();

                }, // end success
                failure: function (response) {
                }
            }); // Ajax
        }
    }
}

function month_selectMonth_reports(month_value, soloVigentes_value, soloEntrCol_value, incluirCancelados_value) {
    if (month_value !== null && month_value !== undefined && month_value > 0 && soloVigentes_value !== null && soloVigentes_value !== undefined &&
        soloEntrCol_value !== null && soloEntrCol_value !== undefined && incluirCancelados_value !== null && incluirCancelados_value !== undefined) {

        month_value += "";

        var year_value = 2018; // DUMMY
        var ddl_year = $("#ddl_year option:selected");
        if (ddl_year !== null && ddl_year !== undefined && ddl_year.text() !== null && ddl_year.text() !== undefined) {
            year_value = ddl_year.text();
            YEAR_SELECTED = year_value;

            reportes_clearData();

            // Source: https://www.codeproject.com/Tips/775585/Bind-Gridview-using-AJAX
            // Ajax call parameters
            console.log("Ajax call: Dashboard.aspx/GetData_BindGridFormularios. Params:");
            console.log("year_value, type: " + type(year_value) + ", value: " + year_value);
            console.log("month_value, type: " + type(month_value) + ", value: " + month_value);
            console.log("soloVigentes_value, type: " + type(soloVigentes_value) + ", value: " + soloVigentes_value);
            console.log("soloEntrCol_value, type: " + type(soloEntrCol_value) + ", value: " + soloEntrCol_value);
            console.log("incluirCancelados_value, type: " + type(incluirCancelados_value) + ", value: " + incluirCancelados_value);

            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetData_BindGridFormularios",
                contentType: "application/json;charset=utf-8",
                data: '{year_value: "' + year_value + '",month_value: "' + month_value + '",soloVigentes_value: "' + soloVigentes_value + '",soloEntrCol_value: "' + soloEntrCol_value + '",incluirCancelados_value: "' + incluirCancelados_value + '"}',
                dataType: "json",
                success: function (response) {

                    // total pedidos
                    var total_pedidos = 0;

                    // total carteles = pasacalles
                    var total_carteles = 0;

                    // total banners
                    var total_banners = 0;

                    // total roll ups
                    var total_rollups = 0;

                    // total colocaciones
                    var total_colocaciones = 0;

                    // total envios a domicilio
                    var total_envios_domicilio = 0;

                    // total envios al interior
                    var total_envios_interior = 0;

                    // total retiros taller
                    var total_retirosTaller = 0;

                    // total lona usada
                    var total_lona_mts = 0;

                    // total palos
                    var total_palos = 0;

                    if (response.d.length > 0) {

                        total_pedidos = response.d.length;
                        for (var i = 0; i < response.d.length; i++) {

                            // Cancelados
                            var estadoNro = check_nullValues(response.d[i].EstadoNro);
                            if (estadoNro === 2 || estadoNro === 3) {
                                total_pedidos--;
                            }

                            // Tipo cartel
                            switch (response.d[i].lblTipo) {
                                case "Pasacalle":
                                    {
                                        total_carteles++;
                                        break;
                                    }
                                case "Banner":
                                    {
                                        total_banners++;
                                        break;
                                    }
                                case "Roll up":
                                    {
                                        total_rollups++;
                                        break;
                                    }
                            }

                            // Tipo entrega
                            switch (response.d[i].lblTipoEntrega) {
                                case "Colocación":
                                    {
                                        total_colocaciones++;
                                        break;
                                    }
                                case "Envío a domicilio":
                                    {
                                        total_envios_domicilio++;
                                        break;
                                    }
                                case "Envío al interior":
                                    {
                                        total_envios_interior++;
                                        break;
                                    }
                                case "Retiro en el taller":
                                    {
                                        total_retirosTaller++;
                                        break;
                                    }
                            }

                            // Largo de lona usada
                            var lblTamano_largo_cm = response.d[i].lblTamano_largo_cm;
                            if (type(lblTamano_largo_cm) === "string") {
                                total_lona_mts += TryParseInt(lblTamano_largo_cm, 0);
                            }
                            
                        } // for

                        if (total_lona_mts !== 0) {
                            total_lona_mts = total_lona_mts / 100;
                        }

                        total_palos = total_carteles * 2;

                        reportes_loadData(total_pedidos, total_carteles, total_banners, total_rollups, total_colocaciones, total_envios_domicilio, total_envios_interior, total_retirosTaller, total_lona_mts, total_palos);

                        // Volver a cargar eventos sobre la grilla
                        //bindDelayEvents();
                    }

                    // Load calendario completo
                    //loadCalendar();

                }, // end success
                failure: function (response) {
                }
            }); // Ajax
        }
    }
}

function reportes_clearData() {
    $("#total_pedidos").text(0);
    $("#total_carteles").text(0);
    $("#total_banners").text(0);
    $("#total_rollups").text(0);
    $("#total_colocaciones").text(0);
    $("#total_lona_mts").text(0);
    $("#total_palos").text(0);
}

function reportes_loadData(total_pedidos, total_carteles, total_banners, total_rollups, total_colocaciones, total_envios_domicilio, total_envios_interior, total_retirosTaller, total_lona_mts, total_palos) {
    $("#total_pedidos").text(total_pedidos);
    $("#total_carteles").text(total_carteles);
    $("#total_banners").text(total_banners);
    $("#total_rollups").text(total_rollups);
    $("#total_colocaciones").text(total_colocaciones);
    $("#total_lona_mts").text(total_lona_mts);
    $("#total_palos").text(total_palos);
}

function create_calendar_events(values) {
    if (values !== null && values !== undefined) {
        var color = "blue"; // Envío a domicilio

        if (values.lblTipoEntrega !== null && values.lblTipoEntrega !== undefined) {
            switch (values.lblTipoEntrega) {
                case "Colocación": {
                    color = "red";
                    break;
                }
                case "Retiro en el taller": {
                    color = "green";
                    break;
                }
                case "Envío al interior": {
                    color = "gray";
                    break;
                }
            }
        }

        if (values.lblFechaEntrega !== null && values.lblTipoEntrega !== null && values.lblNombre !== null && values.lblNumber !== null) {
            var object = {
                date: check_nullValues(moment(values.lblFechaEntrega, "DD-MM-YYYY").format("D/M/YYYY")),
                title: check_nullValues(values.lblTipoEntrega),
                color: color,
                //link: check_nullValues(values.URL_short),
                //linkTarget: '_blank',
                content: 'Nombre: ' + check_nullValues(values.lblNombre) + ' <br> Tel: ' + check_nullValues(values.lblNumber),
                displayMonthController: true,
                displayYearController: true,
                nMonths: 6
            }
            EVENTS_LIST.push(object);
        }
    }
}

function check_zeros(value) {
    return value.replace(/0/g, "");
} 

function convertBool(value) {
    var ret = "No";
    if (value) {
        ret = "Si";
    }
    return ret;
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
        $("#lblMonth_proy").text(month_name);
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

function trigger_events() {
    // Click first Month
    clickCurrentMonth();

    // Styles to filter
    styles_filter();
}

function clickCurrentMonth() {
    var d = new Date();
    var m = d.getMonth() + 1;
    if (m <= 12 && $("#tr-id-" + m + " td")[0] !== null) {
        $("#tr-id-" + m + " td")[0].click();
    }
}

function styles_filter() {
    var filter_table = $(".filter-table");
    if (filter_table !== null && filter_table !== undefined) {
        filter_table.find("input").addClass("form-control");
        filter_table.attr("placeholder", "Filtrar por campo");
        filter_table.text("");
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
                MONTH_SELECTED = month_value_int;

                $("#tabsFormularios li:nth-child(1) a").click();

                $("#chbSoloVigentes").prop('checked', false);
                $("#chbSoloEntrCol").prop('checked', false);
                $("#chbInclCancelados").prop('checked', false);
                
                month_selectMonth(month_value_int, true, false, false);

                // Reportes data
                month_selectMonth_reports(month_value_int, false, false, true);

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

function filtrar() {
    var chbSoloEntrCol = $("#chbSoloEntrCol");
    var chbSoloVigentes = $("#chbSoloVigentes");
    var chbInclCancelados = $("#chbInclCancelados");
    var hdn_monthSelected = $("#hdn_monthSelected");
    if (hdn_monthSelected !== null && hdn_monthSelected.val() !== null && chbSoloEntrCol !== null && chbSoloVigentes !== null && chbInclCancelados !== null) {
        var soloEntrCol_value = chbSoloEntrCol.is(":checked")
        var soloVigentes_value = chbSoloVigentes.is(":checked")
        var incluirCancelados_value = chbInclCancelados.is(":checked")
        var month_value = hdn_monthSelected.val();

        var month_value_int = month_value;
        if (type(month_value) === "string") {
            month_value_int = TryParseInt(month_value, 0);
        }
        month_selectMonth(month_value_int, !soloVigentes_value, soloEntrCol_value, incluirCancelados_value)
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

function showActionMenu_OPC(formID, tel, nombre, btnID_value) {
    var divPopbox = $("#divPopbox_OPC");
    var btnID = $("#" + btnID_value);
    if (divPopbox !== null && divPopbox !== undefined && btnID !== null && btnID !== undefined) {

        var new_w = parseInt(divPopbox.css("width"), 10);
        var new_h = parseInt(divPopbox.css("height"), 10);

        //var prom_w = (Math.abs(new_w)) / 2;
        //var prom_h = (Math.abs(new_h)) / 2;

        divPopbox.offset({ top: btnID.offset().top });
        divPopbox.offset({ left: btnID.offset().left - new_w });

        //divPopbox.position(btnID.position());

        $("#lbl_options_header_OPC").text(nombre);
        //$("#lbl_options_info_OPC").text("Seleccione una acción");
        $("#divPopbox_OPC").show("highlight", 700);

        // Estados de pedidos:
        // 0: Vigente - gris
        // 1: Concluído - verde
        // 2: Cancelado - rojo
        // 3: Eliminado - rojo
        // 4: Diseño aprobado, pronto para imprimir - azul

        // Cargar acciones
        var lbl_options_button1 = $("#lbl_options_button1_OPC"); // verde - Concluído
        var lbl_options_button2 = $("#lbl_options_button2_OPC"); // azul - Diseño aprobado
        var lbl_options_button3 = $("#lbl_options_button3_OPC"); // rojo - Cancelado
        var lbl_options_button4 = $("#lbl_options_button4_OPC"); // negro - Inicial
        if (lbl_options_button1 !== null && lbl_options_button1 !== undefined && 
        lbl_options_button2 !== null && lbl_options_button2 !== undefined && 
        lbl_options_button3 !== null && lbl_options_button3 !== undefined &&
        lbl_options_button4 !== null && lbl_options_button4 !== undefined) {
            var onclick_1 = "doAction_OPC(4, \"" + formID + "\")"; // azul - Diseño aprobado
            var onclick_2 = "doAction_OPC(1, \"" + formID + "\")"; // verde - Concluído
            var onclick_3 = "doAction_OPC(3, \"" + formID + "\")"; // rojo - Cancelado
            var onclick_4 = "doAction_OPC(0, \"" + formID + "\")"; // negro - Inicial
            lbl_options_button1.attr('onclick', onclick_2); // verde - Concluído
            lbl_options_button2.attr('onclick', onclick_1); // azul - Diseño aprobado
            lbl_options_button3.attr('onclick', onclick_3); // rojo - Cancelado
            lbl_options_button4.attr('onclick', onclick_4); // negro - Inicial
        }
    }
}

function showActionMenu_CTO(formID, nombre, goToFormulario, goToGMaps, goToWPP, btnID_value) {
    var divPopbox = $("#divPopbox_CTO");
    var btnID = $("#" + btnID_value);
    if (divPopbox !== null && divPopbox !== undefined && btnID !== null && btnID !== undefined) {

        var new_w = parseInt(divPopbox.css("width"), 10);
        var new_h = parseInt(divPopbox.css("height"), 10);

        //var prom_w = (Math.abs(new_w)) / 2;
        //var prom_h = (Math.abs(new_h)) / 2;

        divPopbox.offset({ top: btnID.offset().top });
        divPopbox.offset({ left: btnID.offset().left - new_w });

        //divPopbox.position(btnID.position());

        $("#lbl_options_header_CTO").text(nombre);
        //$("#lbl_options_info_CTO").text("Seleccione una acción");
        $("#divPopbox_CTO").show("highlight", 700);

        // Estados de pedidos:
        // 0: Vigente - gris
        // 1: Concluído - verde
        // 2: Cancelado - rojo
        // 3: Eliminado - rojo
        // 4: Diseño aprobado, pronto para imprimir - azul

        // Cargar acciones
        var lbl_options_button1 = $("#lbl_options_button1_CTO");
        var lbl_options_button2 = $("#lbl_options_button2_CTO");
        var lbl_options_button3 = $("#lbl_options_button3_CTO");
        var txbLink = $("#txbLink");
        
        if (lbl_options_button1 !== null && lbl_options_button1 !== undefined &&
        lbl_options_button2 !== null && lbl_options_button2 !== undefined &&
        lbl_options_button3 !== null && lbl_options_button3 !== undefined &&
        txbLink !== null && txbLink !== undefined) {
            lbl_options_button1.attr('href', goToFormulario);
            lbl_options_button2.attr('href', goToGMaps);
            lbl_options_button3.attr('href', goToWPP);
            txbLink.val(goToWPP);
        }
    }
}

function doAction_OPC(actionID, formID) {
    // Ajax call parameters
    console.log("Ajax call 1: Dashboard.aspx/PedidosUpdateState. Params:");
    console.log("actionID, type: " + type(actionID) + ", value: " + actionID);
    console.log("formID, type: " + type(formID) + ", value: " + formID);
    console.log("End Ajax call");

    $.ajax({
        type: "POST",
        url: "Dashboard.aspx/PedidosUpdateState",
        data: '{actionID: "' + actionID + '",formID: "' + formID + '",userID: "' + USER_ID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var resultado = response.d;
            if (resultado !== null && resultado) {
                $(".popbox-box.popbox").hide();
                showMessage(hashMessages["ConfirmacionCambios"]);
                //setTimeout(location.reload(), 5000);
                CX_Message_AutoClose();
            } else {
                alert("Error interno.");
            }
        }
    }); // Ajax
}

function doAction_CTO(actionID, formID) {
    // Ajax call parameters
    console.log("Ajax call 1: Dashboard.aspx/PedidosUpdateState. Params:");
    console.log("actionID, type: " + type(actionID) + ", value: " + actionID);
    console.log("formID, type: " + type(formID) + ", value: " + formID);
    console.log("End Ajax call");

    $.ajax({
        type: "POST",
        url: "Dashboard.aspx/PedidosUpdateState",
        data: '{actionID: "' + actionID + '",formID: "' + formID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var resultado = response.d;
            if (resultado !== null && resultado) {
                $(".popbox-box.popbox").hide();
                showMessage(hashMessages["ConfirmacionCambios"]);
                //setTimeout(location.reload(), 5000);
                CX_Message_AutoClose();
            } else {
                alert("Error interno.");
            }
        }
    }); // Ajax
}

function CX_Message_AutoClose() {
    $("button.ui-widget").attr("onclick", "location.reload()");
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


function doMagic2() {
    var textClassName = 'text-to-copy';
    var buttonClassName = 'js-copy-btn';
    var sets = {};
    var regexBuilder = function (prefix) {
        return new RegExp(prefix + '\\S*');
    };

    var texts = Array.prototype.slice.call(document.querySelectorAll(
      '[class*=' + textClassName + ']'));
    var buttons = Array.prototype.slice.call(document.querySelectorAll(
      '[class*=' + buttonClassName + ']'));

    var classNameFinder = function (arr, regex, namePrefix) {
        return arr.map(function (item) {
            return (item.className.match(regex)) ? item.className
              .match(regex)[0].replace(namePrefix, '') : false;
        }).sort();
    };

    sets.texts = classNameFinder(
      texts, regexBuilder(textClassName), textClassName);

    sets.buttons = classNameFinder(
      buttons, regexBuilder(buttonClassName), buttonClassName);

    var matches = sets.texts.map(function (ignore, index) {
        return sets.texts[index].match(sets.buttons[index]);
    });

    var throwErr = function (err) {
        throw new Error(err);
    };
    var iPhoneORiPod = false;
    var iPad = false;
    var oldSafari = false;
    var navAgent = window.navigator.userAgent;

    // CHEQUEO
    var txbLink = $("#txbLink").val();
    if (txbLink !== null && txbLink.length > 0 && txbLink !== "?") {

        if (
          (/^((?!chrome).)*safari/i).test(navAgent)
            // ^ Fancy safari detection thanks to: https://stackoverflow.com/a/23522755
          &&
          !(/^((?!chrome).)*[0-9][0-9](\.[0-9][0-9]?)?\ssafari/i).test(
            navAgent)
            // ^ Even fancier Safari < 10 detection thanks to regex.  :^)
        ) {
            oldSafari = true;
        }
        // We need to test for older Safari and the device,
        // because of quirky awesomeness.
        if (navAgent.match(/iPhone|iPod/i)) {
            iPhoneORiPod = true;
        } else if (navAgent.match(/iPad/i)) {
            iPad = true;
        }
        var cheval = function (btn, text) {
            var copyBtn = document.querySelector(btn);

            var setCopyBtnText = function (textToSet) {
                copyBtn.textContent = textToSet;
            };
            if (iPhoneORiPod || iPad) {
                if (oldSafari) {
                    setCopyBtnText("Select text");
                }
            }
            if (copyBtn) {
                copyBtn.addEventListener('click', function () {
                    var oldPosX = window.scrollX;
                    var oldPosY = window.scrollY;
                    // Clone the text-to-copy node so that we can
                    // create a hidden textarea, with its text value.
                    // Thanks to @LeaVerou for the idea.
                    var originalCopyItem = document.querySelector(text);
                    var dollyTheSheep = originalCopyItem.cloneNode(true);
                    var copyItem = document.createElement('textarea');
                    copyItem.style.opacity = 0;
                    copyItem.style.position = "absolute";
                    // If .value is undefined, .textContent will
                    // get assigned to the textarea we made.
                    copyItem.value = dollyTheSheep.value || dollyTheSheep
                      .textContent;
                    document.body.appendChild(copyItem);
                    if (copyItem) {
                        // Select the text:
                        copyItem.focus();
                        copyItem.selectionStart = 0;
                        // For some reason the 'copyItem' does not get
                        // the correct length, so we use the OG.
                        //copyItem.selectionEnd = originalCopyItem.textContent.length;
                        copyItem.selectionEnd = 999999999;
                        try {
                            // Now that we've selected the text, execute the copy command:
                            document.execCommand('copy');
                            if (oldSafari) {
                                if (iPhoneORiPod) {
                                    setCopyBtnText("Now tap 'Copy'");
                                } else if (iPad) {
                                    // The iPad doesn't have the 'Copy' box pop up,
                                    // you have to tap the text first.
                                    setCopyBtnText(
                                      "Now tap the text, then 'Copy'");
                                } else {
                                    // Just old!
                                    setCopyBtnText("Press Command + C to copy");
                                }
                            } else {
                                setCopyBtnText("¡OK!");
                            }
                        } catch (ignore) {
                            setCopyBtnText("Please copy manually");
                        }
                        originalCopyItem.focus();
                        // Restore the user's original position to avoid
                        // 'jumping' when they click a copy button.
                        window.scrollTo(oldPosX, oldPosY);
                        originalCopyItem.selectionStart = 0;
                        originalCopyItem.selectionEnd = originalCopyItem.textContent
                          .length;
                        copyItem.remove();
                    } else {
                        throwErr(
                          "You don't have an element with the class: '" +
                          textClassName +
                          "'. Please check the cheval README."
                        );
                    }
                });
            } else {
                throwErr(
                  "You don't have a <button> with the class: '" +
                  buttonClassName + "'. Please check the cheval README."
                );
            }
        };

        // Loop through all sets of elements and buttons:
        matches.map(function (i) {
            cheval('.' + buttonClassName + i, '.' + textClassName + i);
        });

    } // CHEQUEO
}