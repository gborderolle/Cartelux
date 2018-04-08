<%@ Page Language="C#" MasterPageFile="~/SiteExternal.Master" AutoEventWireup="true" CodeBehind="GeneradorURL.aspx.cs" Inherits="Cartelux1.GeneradorURL" Title="Generador URL" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- STYLES EXTENSION -->
    <!-- PAGE CSS -->
    <link rel="stylesheet" href="/Content/css/generador.css" />
    <!-- PAGE JS -->
    <script type="text/javascript" src="/Content/js/generador.js"></script>
    <script type="text/javascript" src="/Content/js/clipboard.min.js"></script>
    <script type="text/javascript" src="/Content/js/clipboard-action.js"></script>
    <%--<script type="text/javascript" src="/Content/js/clipboard.js"></script>--%>
    <style type="text/css">
        body {
            background-color: #f2e0cf;
        }

        .center {
            display: block;
            margin: auto;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubbodyContent" runat="server"></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12 col-md-12" style="display: inline-block;">
        <div class="box-header with-border dark in div-form" style="display: inline-block;">
            <div class="row center">
                <div style="text-align: center;">
                    <button class="btn pull-left" onclick="btn_refresh(); return false;" style="margin-left:5px; padding:3px;"> <img src="Content/img/reload.png" class="img-fluid" style="width:20px;"/></button>
                    <h2 id="lbl_h2" style="color: #ea7209;">Nuevo pedido
                    <div class="pull-right" style="margin-right:5px;"> <img id="img_formStatus" src="Content/img/cancel.png" class="img-fluid" style="width:20px;"/> </div>
                  <a href="#" id="btnRefresh" class="pull-right btn" onclick="location.reload();" style="position: absolute; right: 0; margin-right: 20px; display:none;">
                      <i class="fa fa-refresh"></i>
                  </a>
                    </h2>
                </div>
            </div>
            <br />
            <div class="row center">
                <div class="col-sm-12 col-md-10 center">
                    <div class="login-container sub-form">
                        <div class="form-group">
                            Número del cliente - 091373622
                            <input type="number" class="form-control" id="txbContactPhone" autofocus />
                            <br />
                            <input class="form-control btn-primary btn-lg" style="height: auto;" type="button" tabindex="1" runat="server" id="btnGenerar" clientidmode="static" value="Generar URL" title="Click para generar una URL con ID único" onclick="generarURL();" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row center">
                <div class="col-sm-12 col-md-10 center">
                    <input class="form-control text-to-copy pull-left" id="txbLink" onclick="this.select();" value="?" style="width: 85%;" readonly />
                    <%--<button onclick="CopyTextAUX()"></button>--%>

                    <%--SOURCE: https://clipboardjs.com/--%>
                    <a id="aBtnCopy" href="#" class="btn btn-lg pull-right" data-clipboard-action="copy" data-clipboard-target="input#txbLink" style="padding:8px;">
                        <i class="glyphicon glyphicon-copy"></i>
                    </a>

                    <button type="button" class="form-control btn-warning js-copy-btn btn-lg" style="height: auto;" id="btnCopy" onclick="enviarWPP()">Copiar y enviar por WhatsApp</button>
                    <%--<input class="form-control btn-warning js-copy-btn" type="button" tabindex="2" id="btnCopy" value="Copiar y enviar por WhatsApp" title="Click para enviar por WhatsApp" onclick="enviarWPP();">--%>
                </div>
            </div>
            <br />
            <br />
            <br />
        </div>
    </div>
    <script type="text/javascript">

        var clipboard = new Clipboard('#aBtnCopy');
        clipboard.on('success', function (e) {
            console.log(e);
        });
        clipboard.on('error', function (e) {
            console.log(e);
        });


        //<![CDATA[
        /*jslint browser:true, white:true, single:true*/
        (function () {
            'use strict';

            doMagic();

        }());
        //]]>

        function doMagic() {


            var textClassName = 'text-to-copy';
            var buttonClassName = 'js-copy-btn';
            var sets = {};
            var regexBuilder = function (prefix) {
                return new RegExp(prefix + '\\S*');
            };

            window.addEventListener('DOMContentLoaded', function () {

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

            });
        }

    </script>
    <script>
        // tell the embed parent frame the height of the content
        if (window.parent && window.parent.parent) {
            window.parent.parent.postMessage(["resultsFrame", {
                height: document.body.getBoundingClientRect().height,
                slug: "dx5vvzc1"
            }], "*")
        }
    </script>
</asp:Content>
