$(document).ready(function () {
    $(".btn-login").click(function () {
        var loginData = {
            "email": $("#email").val(),
            "password": $("#password").val()
        }
        console.log(loginData);
        $.ajax({
            url: 'http://localhost:5099/api/Account/Login',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(loginData),
            success: function (response) {
                console.log("Response from server: ", response);
                sessionStorage.setItem("token", response.accessToken);
                sessionStorage.setItem("studentId", response.studentId);
                sessionStorage.setItem("userId", response.userId);
                window.location.href = "../../landing-page/templates/landing-home.html";
            },
            error: function (error) {
                console.error("Error: ", error);
            }
        });
    });
});