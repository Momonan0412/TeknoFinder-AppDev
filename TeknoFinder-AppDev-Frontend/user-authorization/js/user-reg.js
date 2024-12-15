$(document).ready(function () {
    var tempUserDetails = {
    "studentDTO": {
        "firstName": "",
        "lastName": "",
        "program": "",
        "yearLevel": "",
        "status": ""
    },
    "addUserDTO": {
        "email": "",
        "password": "",
        "confirmPassword": "",
        "isActive": true
    }
    }
    if(sessionStorage.getItem("userDetails") == null) {
        sessionStorage.setItem("userDetails", JSON.stringify(tempUserDetails));
    }
    const userDetails = JSON.parse(sessionStorage.getItem("userDetails"));
    console.log(window.location.href);
    $('.btn-next').click(function() {
        console.log("redirecting....")
        userDetails.studentDTO.firstName = $('#firstname').val();
        userDetails.studentDTO.lastName = $('#lastname').val();
        console.log(userDetails);
        sessionStorage.setItem("userDetails", JSON.stringify(userDetails));
        window.location.href = "login-email.html";

    });
});