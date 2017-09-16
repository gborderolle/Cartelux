function editFields()
{
    setFieldsReadOnly(false);
}

function setFieldsReadOnly(value)
{
    $(".btnEditable").attr("readonly", value);
    $("#btnConfirmar").disabled = value;

    if(value)
    {
        $("#btnConfirmar").addAttr("disabled");
    } else
        {
        $("#btnConfirmar").removeAttr("disabled");
    }
}
