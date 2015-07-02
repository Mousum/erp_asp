$(document).ready(function () {
    $('#check-item').click(function () {
        if ($(this).is(':checked')) {
            $('#div-check-item').fadeIn("slow"); 
            $('#checkpurchaseItem').prop('checked', true).prop("disabled", true);
            $('#checksellItem').prop('checked', true).prop("disabled", true);
            if ($('#checksellItem').is(":checked")) {
                $("#div-sell-item").fadeIn("slow");
            }
        } else {
            $('#div-check-item').fadeOut("slow");
            $('#checkpurchaseItem').prop("disabled", false);
            $('#checksellItem').prop("disabled", false);
        }
    });

    $('#checksellItem').click(function () {
        if ($(this).is(":checked")) {
            $("#div-sell-item").fadeIn("slow");
        } else {
            $("#div-sell-item").fadeOut("slow");
        }
    });
    
    $('#selladdAccount').click(function () {
        $("#sellmodalclick").trigger("click");
    });

    $('#purcheseaddAccount').click(function () {
        $("#purchesemodalclick").trigger("click");
    });
});