$(document).ready(function () {
    var ID = getUrlParameter('ID');
    if (ID != null && ID.length > 0) {
        //load_events();
        //load_ddls();
    } else {
        $("#btnEdit").hide();
    }

}); // END On Ready

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

function editFields()
{
    setFieldsReadOnly(false);
}

function setFieldsReadOnly(value)
{
    $(".txbEditable").attr("readonly", value);
    $("#btnConfirmar").disabled = value;
    $(".dropdown").attr("disabled", value);

    if(value)
    {
        $("#btnConfirmar").addAttr("disabled");
    } else
        {
        $("#btnConfirmar").removeAttr("disabled");
    }
}

function confirmacionPedido() {
    $("#dialog p").text(hashMessages["ConfirmacionPedido"]);
    $("#dialog").dialog({
        buttons: {
            "Confirmar": function () {
                $(this).dialog("close");
            }
        }
    });
}
