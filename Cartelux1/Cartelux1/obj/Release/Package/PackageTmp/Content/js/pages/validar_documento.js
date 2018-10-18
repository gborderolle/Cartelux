function validarCedula(ci){
    var arrCoefs = [2,9,8,7,6,3,4,1];
    var suma = 0;
    var difCoef = parseInt(arrCoefs.length - ci.length);
    for (var i = ci.length - 1; i > -1; i--){
        var dig = ci.substring(i, i+1);
        var digInt = parseInt(dig);
        var coef = arrCoefs[i+difCoef];
        suma = suma + digInt * coef;
    }
    // si la suma es múltiplo de 10 es una ci válida
    if ( (suma % 10) == 0 ) {
        return true;
    } else {
        return false;
    }
}
/*
function validarRut(numero, dv) {
    if (!isNaN(numero) || numero.length == 0 || numero.length > 8) {
        return false;
    } else {
        if (getDV(numero) == dv) return true;
    }
    return false;
}
function getDV(numero) {
    nuevo_numero = numero.toString().split("").reverse().join("");
    for (i = 0, j = 2, suma = 0; i < nuevo_numero.length; i++, ((j == 7) ? j = 2 : j++)) {
        suma += (parseInt(nuevo_numero.charAt(i)) * j);
    }
    n_dv = 11 - (suma % 11);
    return ((n_dv == 11) ? 0 : ((n_dv == 10) ? "K" : n_dv));
}
*/

function validarRut(rut) {
    var number = parseInt(rut);
    if (number.length == 0 || number.length > 12) {
        return false;
    } else {
        return validate_isRUT(rut);
    }
    return false;
}

function validate_isRUT(rut) {
    if (rut.length != 12) {
        return false;
    }
    if (!/^([0-9])*$/.test(rut)) {
        return false;
    }
    var dc = rut.substr(11, 1);
    var rut = rut.substr(0, 11);
    var total = 0;
    var factor = 2;

    for (i = 10; i >= 0; i--) {
        total += (factor * rut.substr(i, 1));
        factor = (factor == 9) ? 2 : ++factor;
    }

    var dv = 11 - (total % 11);

    if (dv == 11) {
        dv = 0;
    } else if (dv == 10) {
        dv = 1;
    }
    if (dv == dc) {
        return true;
    }
    return false;
}
