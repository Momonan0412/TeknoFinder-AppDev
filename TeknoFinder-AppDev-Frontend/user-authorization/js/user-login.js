$(document).ready(function () {
    $(".btn-login").click(function () {
        var url = 'http://localhost:5099/api/Account/LoginStudentNumber';
        var loginData = {}
        // check if id is email-login or number-login
        if($('.btn-login').attr('id') == 'email-login'){
            loginData.email = $("#email").val();
            url = 'http://localhost:5099/api/Account/LoginEmail';
        }
        else{
            loginData.studentNumber = $("#studentnumber").val();
        }
        loginData.password = $("#password").val();
        console.log(loginData);
        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(loginData),
            success: function (response) {
                console.log("Response from server: ", response);
                sessionStorage.setItem("token", response.accessToken);
                sessionStorage.setItem("studentId", response.studentId);
                sessionStorage.setItem("userId", response.userId);
                window.location.href = "../../main-page/templates/main-home.html";
            },
            error: function (error) {
                console.error("Error: ", error);
            }
        });
    });
});