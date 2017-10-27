function generarURL() {

    // Ajax call parameters
    console.log("Ajax call 1: GeneradorURL.aspx/GenerarURL. Params:");

    // Check existen mercaderías
    $.ajax({
        type: "POST",
        url: "GeneradorURL.aspx/GenerarURL",
        data: '{dummy: "dummy_text"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var resultado = response.d;
            if (resultado !== null && resultado.length > 0) {

                var resultado_array = resultado.split("|");
                var link = resultado_array[0];
                var isLocal = resultado_array[1];

                if ((link !== null && link.length > 0) && (isLocal !== null && isLocal.length > 0)) {
                    console.log("Es ambiente local: " + isLocal);

                    // Si es ambiente de producción acortar URL
                    if (isLocal != 1) {
                        // Google URL Shortener
                        //API Key (Cartelux project) = AIzaSyB70_7sF6wk7YFEXhoUWRHp_VhK8vd3QOQ
                        console.log("Ajax call 2: Google URL Shortener. Params: longUrl=" + link);
                        console.log("Ajax call 2: API Key (Cartelux project). Params: key=AIzaSyB70_7sF6wk7YFEXhoUWRHp_VhK8vd3QOQ.");

                        $.ajax({
                            type: "POST",
                            url: "https://www.googleapis.com/urlshortener/v1/url?key=AIzaSyB70_7sF6wk7YFEXhoUWRHp_VhK8vd3QOQ",
                            data: '{longUrl: "' + link + '"}',
                            contentType: "application/json",
                            dataType: "json",
                            success: function (response) {

                                $("#btnGenerar").attr("title", response.id);
                                $("#txbLink").val(response.id);

                            }, // end success
                            failure: function (response) {
                                alert("Error interno generando LINK.");

                                $("#btnGenerar").attr("title", link);
                                $("#txbLink").val(link);
                            }
                        }); // Ajax

                    } else {
                        // Ambiente local
                        $("#btnGenerar").attr("title", link);
                        $("#txbLink").val(link);
                    }
                    $("#btnCopy").text("Copiar");
                }
            } else {
                alert("Error interno generando LINK.");
            }

        }, // end success
        failure: function (response) {
            alert("Error interno generando LINK.");
        }
    }); // Ajax
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