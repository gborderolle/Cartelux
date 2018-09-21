wpp_url = "";
formulario_url = "";
texto = "Por favor: ingrese los datos del pedido en el siguiente formulario, muchas gracias."

$(document).ready(function () {
    setInterval(get_refresh, 2000);
});

function CopyTextAUX() {
    /* Get the text field */
    var copyText = document.getElementById("txbLink");

    /* Select the text field */
    copyText.select();

    /* Copy the text inside the text field */
    document.execCommand("Copy");
}

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

                                    $("#aBtnGoToURL").attr("href", response.id);

                                    formulario_url = response.id;

                                    }, // end success
                                failure: function (response) {
                                    alert("Error interno generando LINK.");

                                    $("#btnGenerar").attr("title", form_url);
                                    $("#txbLink").val(form_url);
                                }
                            }); // Ajax

                        } else {
                            // Ambiente local
                            $("#btnGenerar").attr("title", form_url);
                            $("#txbLink").val(form_url);

                            $("#aBtnGoToURL").attr("href", form_url);
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
            //window.location = wpp_url;
            //window.open(wpp_url, '_blank');


            //location.reload();

            //doMagic_fire();

            doMagic2();
            window.open(wpp_url, '_blank');
        }
    } else {
        alert("Haz click en Generar URL para continuar");
    }
}

function doMagic_fire() {
    var DOMContentLoaded_event = document.createEvent("Event");
    DOMContentLoaded_event.initEvent("DOMContentLoaded", true, true);
    window.document.dispatchEvent(DOMContentLoaded_event);
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
                                setCopyBtnText("¡Listo!");
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

function btn_refresh() {
    var txbContactPhone = $("#txbContactPhone").val();
    if (txbContactPhone !== null && txbContactPhone.length > 0) {
        get_refresh();
    } else {
        alert("Debe generar la URL antes.");
    }
}

function get_refresh() {

    var txbContactPhone = $("#txbContactPhone").val();
    if (txbContactPhone !== null && txbContactPhone.length > 0) {

    // Ajax call parameters
    console.log("Ajax call 1: GeneradorURL.aspx/CheckFormStatus_1. Params:");
    console.log("Params: txbContactPhone - " + txbContactPhone);

    $.ajax({
        type: "POST",
        url: "GeneradorURL.aspx/CheckFormStatus_1",
        data: '{txbContactPhone: "' + txbContactPhone + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var resultado = response.d;
            if (resultado !== null) {

                var img_formStatus = $("#img_formStatus");
                if (img_formStatus !== null && img_formStatus !== undefined && img_formStatus.length > 0) {
                    if (resultado) {
                        img_formStatus.attr("src", "Content/img/checked.png");
                    } else {
                        img_formStatus.attr("src", "Content/img/cancel.png");
                    }
                }
            } else {
                alert("Error interno.");
            }

        }, // end success
        failure: function (response) {
            alert("Error interno generando LINK.");
        }
    }); // Ajax

    } 
}

function btn_goForm() {
    var txbLink = $("#txbLink").val();
    if (txbLink !== null && txbLink.length > 0 && txbLink !== "?") {
        window.open(txbLink, '_blank');
    } else {
        alert("El formulario no ha sido generado aún.");
    }
}