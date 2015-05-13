$(document).ajaxSend(function () {
    $('#loader-wrapper').css('display', 'block');
});

$(document).ajaxComplete(function () {
    $('#loader-wrapper').css('display', 'none');
});
//$(document).ready(function () {


//});

