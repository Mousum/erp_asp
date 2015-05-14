// for account setting Page

$(document).ready(function () {
    var urlset = $('.GetSettingsByUserId').attr("data-url");
    $.ajax({
        url: urlset,
        type: "GET",
        data: {},
        success: function (data) {
            //console.log(typeof data.lgcompany);
            if (data.lgdash === true) {
                $('#radio-2').prop('checked', true);
            } else if (data.lgcompany === true) {
                $('#radio-1').prop('checked', true);
            } else {
                $('#radio-3').prop('checked', true);
            }
            if (data.TimezoneId) {
                $('#zoneId option[value=' + data.TimezoneId + ']').attr('selected', 'selected');
            }
            if (data.TimezoneId) {
                $('#ComanyId option[value=' + data.Companies.Id + ']').attr('selected', 'selected');
            }

        },
        error: function () {

        }
    });

    $('#zoneId').click(function () {
        hideMsg();
    });
    $("input[name*='logCheck']").click(function () {
        hideMsg();
    });

    $(".account2nSub").click(function () {
        var radio = $('input[name=logCheck]:radio:checked').val();
        var chackOne = "false";
        var chackTwo = "false";
        var chackThree = "false";
        if (radio == "radio-1") {
            chackOne = "true";
        } else if (radio == "radio-2") {
            chackTwo = "true";
        } else if (radio == "radio-3") {
            chackThree = "true";
        }
        var zoneId = $("#zoneId").val();
        var ComanyId = $('#ComanyId').val();

        if (chackOne == "true" || chackTwo == "true" || chackThree == "true") {

            if (zoneId == "") {
                $('.msg-danger').text("Please Select timeZone");
                $('.msg-danger').show('slow');
                return false;
            } 
            console.log(chackOne);
            console.log(chackTwo);
            console.log(chackThree);
            //var setting = { "Settings": { "lgcompany": chackOne, "lgdash": chackTwo, "lglast": chackThree, "TimezoneId": zoneId } };
            var url = $('.UpdateSettings').attr("data-url");
            $.ajax({
                url: url,
                type: "post",
                data: { lgcompany: chackOne, lgdash: chackTwo, lglast: chackThree, TimezoneId: zoneId, ComanyId: ComanyId },
                success: function (data) {
                    $('.msg-danger').text("Successfully Added");
                    $('.msg-danger').show('slow');
                },
                error: function () {

                }
            });
        } else {

            $('.msg-danger').text("Please Select When You Log-In");
            $('.msg-danger').show('slow');
        }
    });
    $("#Users_Email").click(function () {
        $('.email-danger').hide('slow');
    });
    $("#Users_Password").click(function () {
        $('.email-danger').hide('slow');
    })
    $('.account1stSub').click(function () {
        
        var url = $('.UpdateEmail').attr("data-url");
        var Email = $("#Users_Email").val();
        var Password = $("#Users_Password").val();
        
        if (isValidEmailAddress(Email)) {
            $('.email-danger').text("Ivalid Email Address");
            $('.email-danger').show('slow');
            
        } else {
            if (Password.length < 5) {
                $('.email-danger').text("Password More Than 5 Character");
                $('.email-danger').show('slow');
                return false;
            }
            $.ajax({
                url: url,
                type: "post",
                data: { Email: Email, Password: Password },
                success: function (data) {
                    if (data.success == "True") {
                        $("#Users_Email").val(data.msg);
                        $("#Users_Password").val(data.msgpass);
                        $('.email-danger').text("Email & Password Successfully Updated");
                        $('.email-danger').show('slow');
                       
                    } else {
                        $('.email-danger').text("Email Address Already Used");
                        $('.email-danger').show('slow');
                    }

                },
                error: function () {

                }
            });
        }
        
    });

});
function isValidEmailAddress(emailAddress) {
    var emailReg = /^([\w-\.]+@@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test(emailAddress);
}

function hideMsg() {
    $('.msg-danger').hide('slow');
}
function showMsg() {
    $('.msg-danger').show('slow');
}




// for all ajax load
$(document).ajaxSend(function () {
    $('#loader-wrapper').css('display', 'block');
});

$(document).ajaxComplete(function () {
    $('#loader-wrapper').css('display', 'none');
});



