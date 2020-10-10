/******** Auxiliar Functions ********/

// Variable object types
var TYPES = {
    'undefined': 'undefined',
    'number': 'number',
    'boolean': 'boolean',
    'string': 'string',
    '[object Function]': 'function',
    '[object RegExp]': 'regexp',
    '[object Array]': 'array',
    '[object Date]': 'date',
    '[object Error]': 'error'
},
 TOSTRING = Object.prototype.toString;

// Get variable object type
function type(o) {
    return TYPES[typeof o] || TYPES[TOSTRING.call(o)] || (o ? 'object' : 'null');
};

function TryParseInt(str, defaultValue) {
    var retValue = defaultValue;
    if (str !== null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseInt(str);
            }
        }
    }
    return retValue;
}


function TryParseFloat(str, defaultValue) {
    var retValue = defaultValue;
    if (str !== null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseFloat(str);
            }
        }
    }
    return retValue;
}


function show_message_confirm(msg) {
    $('#dialog p').text(hashMessages[msg]);
    $("#dialog").dialog({
        open: {},
        resizable: false,
        height: 150,
        modal: true,
        buttons: {
            "Confirmar": function () {
                $(this).dialog("close");
                return true;

            },
            "Cancelar": function () {
                $(this).dialog("close");
                return false;
            }
        }
    });
}

function show_message_info(msg) {
    $('#dialog p').text(hashMessages[msg]);
    $("#dialog").dialog({
        open: {},
        resizable: false,
        height: 150,
        modal: true,
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
                return true;
            }
        }
    });
}

function roundUp(number, precision) {
    Math.ceil(number * precision) / precision;
}

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function numberRemoveCommas(x) {
    return parseFloat(x.replace(/,/g, ''));
}

function arrayRemoveByID(array, value) {
    return array.filter(function (obj) { return obj.lblElementoID !== lblElementoID; });
}

function zero_or_isNaN(str) {
    return_value = 0;
    if (!isNaN(str)) {
        return_value = str;
    }
    return return_value;
}
