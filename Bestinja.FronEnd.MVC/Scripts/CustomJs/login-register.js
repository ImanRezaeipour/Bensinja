/*
 *
 * login-register modal
 * Autor: Creative Tim
 * Web-autor: creative.tim
 * Web script: http://creative-tim.com
 * 
 */

var interval;
var confirmForget = false;



function openLoginModal() {
    showLoginForm();
    $('.errors').removeClass('alert alert-danger').html("");
    setTimeout(function () {
        $('#loginModal').modal('show');
    }, 230);

}
function openRegisterModal() {
    $('.errors').removeClass('alert alert-danger').html("");
    showRegisterForm();
    setTimeout(function () {
        $('#loginModal').modal('show');
    }, 230);

}

function showRegisterForm() {
    $('.confirmbox').fadeOut('fast');
    $('.forgetBox').fadeOut('fast');
    $('.resetpasswordbox').fadeOut('fast');
    $('.loginBox').fadeOut('fast', function () {
        $('.registerBox').fadeIn('fast');
        $('.login-footer').fadeOut('fast', function () {
            $('.register-footer').fadeIn('fast');
        });
        $('#title-register').html('ثبت نام');

    });
    $('.error').removeClass('alert alert-danger').html('');

}
function showConfirmCodeForm(selector) {

    $("#loginModal ." + selector + "").fadeOut('fast', function () {
        $('.confirmbox').fadeIn('fast');

        $('#title-register').html('کد تایید');
    });

    $('.js-timeout').text("02:00");
    countdown();
    $('.error').removeClass('alert alert-danger').html('');

}
function showLoginForm() {
    $('.resetpasswordbox').fadeOut('fast');
    $('#loginModal .registerBox').fadeOut('fast', function () {
        $('.loginBox').fadeIn('fast');
        $('.register-footer').fadeOut('fast', function () {
            $('.login-footer').fadeIn('fast');
        });

        $('#title-register').html('ورود ');

    });
    $('.error').removeClass('alert alert-danger').html('');
}
//reset password page step 3
function showPasswordBox() {
    $('.confirmbox').fadeOut('fast', function () {
        $('.resetpasswordbox').fadeIn('fast');
        $('.login-footer').fadeOut('fast');
        $('#title-register').html('بازیابی رمز');
    });
}


function countdown() {
    clearInterval(interval);
    interval = setInterval(function () {
        var timer = $('.js-timeout').html();
        timer = timer.split(':');
        var minutes = timer[0];
        var seconds = timer[1];
        seconds -= 1;
        if (minutes < 0) return;
        else if (seconds < 0 && minutes != 0) {
            minutes -= 1;
            seconds = 59;
        }
        else if (seconds < 10 && length.seconds != 2) seconds = '0' + seconds;

        $('.js-timeout').html(minutes + ':' + seconds);

        if (minutes == 0 && seconds == 0) clearInterval(interval);
    }, 1000);
}

function confirmCode(event) {
   
    var code = $("#confirmCode").val();
    var $this = $(event);
    $this.button('loading');
    $.ajax({
        url: '/Home/ConfirmCode',
        data: { userName: $("#userName").val(), code: code },
        success: function (e) {


            if (e === true) {
                if (confirmForget) {

                    showPasswordBox();
                    return;
                };
                $('#loginModal').modal('toggle');
                AutenticateUser();
                $this.button('reset');
            } else {
                $("#confirmCode").val("");
                $this.button('reset');
                alert("کد وارد شده اشتباه است");
            }
        }, error: function () {
            alert("error");
        }
    });
};

function AutenticateUser() {
    $.ajax({
        url: "/Home/Autenticate",
        data: { userName:$("#userName").val()},
        success: function (data) {
           
            if (data == true) {
   //$('#loginModal').modal('toggle');
            }
         
        }, error:function(e) {
            alert(e);
        }
    });
}

function loginAjax(event) {
   
    var $this = $(event);
    $this.button('loading');
    var model = {
        UserName: $("#login-userName").val(),
        Password: $("#password").val()
    };
    $.ajax({
        url: '/Home/Login',
        data: JSON.stringify(model),
        type: 'post',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            if (result === false) {
                shakeModal();
                return;
            } else {
                $('#loginModal').modal('toggle');
                location.reload();
            }
            $this.button('reset');
        },error:function (e) {
            alert(e.statusText);
            $this.button('reset');
        }
    });
    
}


function ConfirmAgain(button) {
    var $this = $(button);
    $this.button('loading');
    $.ajax({
        url: "Home/SendCodeAgain",
        data: { userName: $("#userName").val() },
        success: function () {
            $this.button('reset');
            $('.js-timeout').html("2:00");
            countdown();
        }, error: function () {
            $this.button('reset');
        }
    });
}

function Register(event) {


    var registerModel = {
        UserName: $("#register_userName").val(),
        Password: $("#register_password").val(),
        ConfirmPassword: $("#confirmPassword").val()
    };

    var $this = $(event);
    $this.button('loading');
    $.ajax({
        url: "/Home/Register",
        data: JSON.stringify(registerModel),
        type: 'post',
        async: true,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (e) {
            if (!CheckError(e, $this)) {
                return;
            }
            if (e.ConfirmCode !== 0) {
                showConfirmCodeForm("registerBox");
                $("#userName").val(e.userName);
            } else {
                alert("چنین کاربری قبلا ثبت نام کرده");
                $this.button('reset');
                return;
            }
            showConfirmCodeForm("registerBox");
            $this.button('reset');
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
            $this.button('reset');
        }
    });


};

$(".next").click(function() {
    $('.errors').removeClass('alert alert-danger').html("");
});

function shakeModal() {
    $('#loginModal .modal-dialog').addClass('shake');
    $('.errors').addClass('alert alert-danger').html("نام کاربری یا رمز عبور اشتباه است");
    $('input[type="password"]').val('');
    setTimeout(function () {
        $('#loginModal .modal-dialog').removeClass('shake');
    }, 1000);
}

//forget step 1
function forgetPassword() {
    $('.loginBox').fadeOut('fast', function () {
        $('.forgetBox').fadeIn('fast');
      
        $('#title-register').html('فراموشی رمز');
    });
}

//send phone number  step 2
function forgetPasswordBtn(button) {

    var $this = $(button);
    $this.button('loading');
    var userName = $("#phonenumber").val();
    $("#userName").val(userName);
    $.ajax({
        url: '/Home/CheckExistUser',
        data: { userName: userName },
        type: 'get',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {

            if (response === true) {

                showConfirmCodeForm("forgetBox");
                confirmForget = true;
                $("#userName").val(userName);
                $this.button('reset');
            } else {
                alert("کاربری با این شماره ثبت نام نکرده");
                $this.button('reset');
            }
        }, error: function () {
            alert("error");
            $this.button('reset');
        }
    });
}

function changePassword(event) {
    var $this = $(event);
    $this.button('loading');

    var newPass = $("#new-pass").val();
    var confirmNewPass = $("#confirm-new-pass").val();
    var userName = $("#userName").val();
    
    var resetPass = {
        Password: newPass,
        confirmPassword: confirmNewPass,
        userName: userName
    };

    $.ajax({

        url: 'Home/ChangePassword',
        data: JSON.stringify(resetPass),
        type: 'post',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            if (response === true) {
                showLoginForm();
                $this.button('reset');
            } else {
                CheckError(response, $this);
            }

        }, error: function (xhr, status, error) {
            $this.button('reset');
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });

}

function CheckError(e, event) {
    if (e.length > 0) {
        $(".error").html("");
        for (var i = 0; i < e.length; i++) {
            $(".error").append("<li class='text-danger'>" + e[i] + "</span>");
        }
        event.button('reset');
        return false;
    };

    return true;
}

