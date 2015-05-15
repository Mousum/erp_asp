﻿// for account setting Page

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
    //$('.account1stSub').click(function () {

    //    var url = $('.UpdateEmail').attr("data-url");
    //    var Email = $("#Users_Email").val();
    //    var Password = $("#Users_Password").val();

    //    if (isValidEmailAddress(Email)) {
    //        $('.email-danger').text("Ivalid Email Address");
    //        $('.email-danger').show('slow');

    //    } else {
    //        if (Password.length < 5) {
    //            $('.email-danger').text("Password More Than 5 Character");
    //            $('.email-danger').show('slow');
    //            return false;
    //        }
    //        $.ajax({
    //            url: url,
    //            type: "post",
    //            data: { Email: Email, Password: Password },
    //            success: function (data) {
    //                if (data.success == "True") {
    //                    $("#Users_Email").val(data.msg);
    //                    $("#Users_Password").val(data.msgpass);
    //                    $('.email-danger').text("Email & Password Successfully Updated");
    //                    $('.email-danger').show('slow');

    //                } else {
    //                    $('.email-danger').text("Email Address Already Used");
    //                    $('.email-danger').show('slow');
    //                }

    //            },
    //            error: function () {

    //            }
    //        });
    //    }

    //});

});
//function isValidEmailAddress(emailAddress) {
//    var emailReg = /^([\w-\.]+@@([\w-]+\.)+[\w-]{2,4})?$/;
//    return emailReg.test(emailAddress);
//}

function hideMsg() {
    $('.msg-danger').hide('slow');
}
function showMsg() {
    $('.msg-danger').show('slow');
}

//Update Password 
$("#editpass").click(function () {
    $("#editpassdiv").toggle("slow");
});
$("#PassCancel").click(function () {
    $("#editpassdiv").hide("slow");
});
$("#passSave").click(function () {

    if ($("#dbpass").val() != $("#oldPass").val()) {
        $(".email-danger").text("Your Entered Wrong Current Password").show('slow');

    }
    else if ($("#dbpass").val() == $("#nPassWord").val()) {
        $(".email-danger").text("Your Current Password and New Password Are Same,Please Try New One").show('slow');

    }
    else if ($("#nPassWord").val().length < 5) {
        $(".email-danger").text("Password Must Be Longer Then 5 Charecter").show('slow');

    }
    else if ($("#nPassWord").val() != $("#cnPassWord").val()) {
        $(".email-danger").text("'New Password' and 'Confirm New Password' Are Must Be The Same").show('slow');
    }
    else {
        var url = $('.UpdatePassword').attr("data-url");
        var Password = $("#nPassWord").val();
        $.ajax({
            url: url,
            type: "post",
            data: { password: Password },
            success: function (data) {
                if (data.success == "True") {
                    $("#dbMail").val(data.msg);
                    $("#dbpass").val(data.msgpass);
                    $('.email-danger').text("Password Successfully Updated");
                    $('.email-danger').show('slow');
                    $("#nPassWord").val("");
                    $("#cnPassWord").val("");
                    $("#oldPass").val("");
                    $("#editpassdiv").hide("slow");

                } else {
                    $('.email-danger').text("Password Cannot Updated this time");
                    $('.email-danger').show('slow');
                }

            },
            error: function () {

            }
        });
    }

});


//Update Email 
$("#editmail").click(function () {
    $("#editmaildiv").toggle("slow");
});
$("#EmailCancel").click(function () {
    $("#editmaildiv").hide("slow");
})
$("#emailSave").click(function () {
    var Email = $("#newmail").val();

    if ($("#dbpass").val() != $("#PassWord").val()) {
        $(".email-danger").text("Wrong Current Password").show('slow');
    }
    else if (Email == $("#dbMail").val()) {
        $(".email-danger").text("Entered Same Email , Please Enter A New Email").show('slow');
    }
    else if (isValidEmailAddress(Email)) {
        $('.email-danger').text("Invalid Email Address");
        $('.email-danger').show('slow');

    }
    else {
        var url = $('.UpdateEmail').attr("data-url");
        $.ajax({
            url: url,
            type: "post",
            data: { Email: Email },
            success: function (data) {
                if (data.success == "True") {
                    $("#dbMail").val(data.msg);
                    $("#dbpass").val(data.msgpass);
                    $('.email-danger').text("Email Successfully Updated");
                    $('.email-danger').show('slow');
                    $("#newmail").val("");
                    $("#PassWord").val("")
                    $("#editmaildiv").hide("slow");

                } else {
                    $('.email-danger').text("Email Address Already Used");
                    $('.email-danger').show('slow');
                }

            },
            error: function () {

            }
        });
    }
})

function isValidEmailAddress(emailAddress) {
    var emailReg = /^([\w-\.]+@@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test(emailAddress);
}


// for all ajax load
$(document).ajaxSend(function () {
    $('#loader-wrapper').css('display', 'block');
});

$(document).ajaxComplete(function () {
    $('#loader-wrapper').css('display', 'none');
});



