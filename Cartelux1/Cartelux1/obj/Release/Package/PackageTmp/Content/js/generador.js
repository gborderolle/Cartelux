wpp_url = "";
texto = "Por favor: ingrese los datos del pedido en el siguiente formulario, muchas gracias."

function generarURL() {

    var txbContactPhone = $("#txbContactPhone").val();
    if (txbContactPhone !== null && txbContactPhone.length > 0) {

        // Si el número empieza con 0 lo borra
        var first = txbContactPhone.charAt(0);
        if (first === "0") {
            txbContactPhone = txbContactPhone.substring(1);
        }

        // Ajax call parameters
        console.log("Ajax call 1: GeneradorURL.aspx/GenerarURL. Params:");
        console.log("Params: txbContactPhone - " + txbContactPhone);

        // Check existen mercaderías
        $.ajax({
            type: "POST",
            url: "GeneradorURL.aspx/GenerarURL",
            data: '{txbContactPhone: "' + txbContactPhone + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var resultado = response.d;
                if (resultado !== null && resultado.length > 0) {

                    var resultado_array = resultado.split("|");
                    var form_url = resultado_array[0];
                    var isLocal = resultado_array[1];

                    // https://api.whatsapp.com/send?phone=59891373622
                    wpp_url = "https://api.whatsapp.com/send?phone=598" + txbContactPhone + "&text=" + texto;

                    //if ((form_url !== null && form_url.length > 0) && (isLocal !== null && isLocal.length > 0)) {
                    if ((wpp_url !== null && wpp_url.length > 0) && (isLocal !== null && isLocal.length > 0)) {
                        console.log("Es ambiente local: " + isLocal);

                        // Si es ambiente de producción acortar URL
                        if (isLocal != 1) {
                            // Google URL Shortener
                            //API Key (Cartelux project) = AIzaSyB70_7sF6wk7YFEXhoUWRHp_VhK8vd3QOQ
                            //console.log("Ajax call 2: Google URL Shortener. Params: longUrl=" + wpp_url);
                            console.log("Ajax call 2: Google URL Shortener. Params: longUrl=" + form_url);
                            console.log("Ajax call 2: API Key (Cartelux project). Params: key=AIzaSyB70_7sF6wk7YFEXhoUWRHp_VhK8vd3QOQ.");

                            $.ajax({
                                type: "POST",
                                url: "https://www.googleapis.com/urlshortener/v1/url?key=AIzaSyB70_7sF6wk7YFEXhoUWRHp_VhK8vd3QOQ",
                                data: '{longUrl: "' + form_url + '"}',
                                //data: '{longUrl: "' + wpp_url + '"}',
                                contentType: "application/json",
                                dataType: "json",
                                success: function (response) {

                                    $("#btnGenerar").attr("title", response.id);
                                    $("#txbLink").val(response.id);

                                    }, // end success
                                failure: function (response) {
                                    alert("Error interno generando LINK.");

                                    $("#btnGenerar").attr("title", form_url);
                                    //$("#btnGenerar").attr("title", wpp_url);
                                    $("#txbLink").val(form_url);
                                    //$("#txbLink").val(wpp_url);
                                }
                            }); // Ajax

                        } else {
                            // Ambiente local
                            $("#btnGenerar").attr("title", form_url);
                            //$("#btnGenerar").attr("title", wpp_url);
                            $("#txbLink").val(form_url);
                            //$("#txbLink").val(wpp_url);
                        }
                        // En todos los casos

                        wpp_url = "https://api.whatsapp.com/send?phone=598" + txbContactPhone + "&text=" + texto;

                        //window.location = wpp_url;
                        //window.open(wpp_url, '_blank');
                    }
                } else {
                    alert("Error interno generando LINK.");
                }

            }, // end success
            failure: function (response) {
                alert("Error interno generando LINK.");
            }
        }); // Ajax
    }else{
        alert("Ingresar el número del cliente.");
    }
}

function enviarWPP() {
    var txbLink = $("#txbLink").val();
    if (txbLink !== null && txbLink.length > 0 && txbLink !== "?") {
        if (wpp_url !== null && wpp_url.length > 0) {
            window.location = wpp_url;
        }
    } else {
        alert("Haz click en Generar URL para continuar");
    }
}

function copyToClipboard(elem) {
    // create hidden text element, if it doesn't already exist
    var targetId = "_hiddenCopyText_";
    var isInput = elem.tagName === "INPUT" || elem.tagName === "TEXTAREA";
    var origSelectionStart, origSelectionEnd;
    if (isInput) {
        // can just use the original source element for the selection and copy
        target = elem;
        origSelectionStart = elem.selectionStart;
        origSelectionEnd = elem.selectionEnd;
    } else {
        // must use a temporary form element for the selection and copy
        target = document.getElementById(targetId);
        if (!target) {
            var target = document.createElement("textarea");
            target.style.position = "absolute";
            target.style.left = "-9999px";
            target.style.top = "0";
            target.id = targetId;
            document.body.appendChild(target);
        }
        target.textContent = elem.textContent;
    }
    // select the content
    var currentFocus = document.activeElement;
    target.focus();
    target.setSelectionRange(0, target.value.length);

    // copy the selection
    var succeed;
    try {
        succeed = document.execCommand("copy");
    } catch (e) {
        succeed = false;
    }
    // restore original focus
    if (currentFocus && typeof currentFocus.focus === "function") {
        currentFocus.focus();
    }

    if (isInput) {
        // restore prior selection
        elem.setSelectionRange(origSelectionStart, origSelectionEnd);
    } else {
        // clear temporary content
        target.textContent = "";
    }
    return succeed;
}