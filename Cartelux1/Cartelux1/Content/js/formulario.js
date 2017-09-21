$(document).ready(function () {
    var ID = getUrlParameter('ID');
    if (ID != null && ID.length > 0) {
        load_events();
        load_ddls();
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

function load_ddls() {
    $('#hdn_ddlTipoEntrega').val($(this).text());
    $('#hdn_ddlTamano').val($(this).text());
    $('#hdn_ddlMotivo').val($(this).text());
}

function load_events() {
    $('#ddlTipoEntrega li').on('click', function () {
        $('#hdn_ddlTipoEntrega').val($(this).text());
    });

    $('#ddlTamano li').on('click', function () {
        $('#hdn_ddlTamano').val($(this).text());
    });

    $('#ddlMotivo li').on('click', function () {
        $('#hdn_ddlMotivo').val($(this).text());
    });
}

function editFields()
{
    setFieldsReadOnly(false);
}

function setFieldsReadOnly(value)
{
    $(".txbEditable").attr("readonly", value);
    $("#btnConfirmar").disabled = value;

    if(value)
    {
        $("#btnConfirmar").addAttr("disabled");
    } else
        {
        $("#btnConfirmar").removeAttr("disabled");
    }
}

