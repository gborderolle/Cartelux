
$(document).ready(function () {
    //new Clipboard('#btnCopy1');
});


//setTimeout(setQTip, 2000)

//function setQTip() {
//    $("#btnGenerar").qtip();
//}

//document.addEventListener('copy', function (e) {
//    e.clipboardData.setData('text/plain', 'foo');
//    e.preventDefault(); // default behaviour is to copy any selected text
//});

//$("#btnGenerar").tooltip({
//    disabled: true,
//    close: function (event, ui) { $(this).tooltip('disable'); }
//});


//$("#txbLink").on('click', function () {
//    $(this).select();
//});

function generarLink() {

    // Ajax call parameters
    console.log("Ajax call: GeneradorLink.aspx/GenerarLink. Params:");

    // Check existen mercaderías
    $.ajax({
        type: "POST",
        url: "GeneradorLink.aspx/GenerarLink",
        data: '{dummy: "dummy_text"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var resultado = response.d;
            if (resultado) {
                var link = resultado;
                if (link !== null) {
                    //copy to clipboard 
                    //window.clipboardData.setData('Text', link);

                    //var hdnID_generado = $("#hdnID_generado");
                    //if (hdnID_generado !== null) {
                    //    hdnID_generado.val(link);

                    //    //copyToClipboard(hdnID_generado);
                    //    //alert(link);
                    //}

                    // set and show tooltip
                    //$("#btnGenerar").tooltip('enable').tooltip('open');

                    $("#btnGenerar").attr("title", link);
                    $("#txbLink").val(link);
                    //$("#btnCopy1").attr("data-clipboard-text", link);
                    //$("#btnGenerar").qtip("enable");
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

//function copyToClipboard(elementId) {
//    var aux = document.createElement("input");
//    aux.setAttribute("value", document.getElementById(elementId).innerHTML);
//    document.body.appendChild(aux);
//    aux.select();
//    document.execCommand("copy");

//    document.body.removeChild(aux);

//}
