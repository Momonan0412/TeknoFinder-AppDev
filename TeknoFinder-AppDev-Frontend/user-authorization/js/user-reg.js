$(document).ready(function () {
    var tempUserDetails = {
  "studentDTO": {
    "firstName": "",
    "lastName": "",
    "studentNumber": "",
    "program": "",
    "yearLevel": "",
    "status": ""
  },
  "addUserDTO": {
    "email": "",
    "username": "",
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
        window.location.href = "register-student-two.html";
    });
    $('.btn-prev').click(function() {
        userDetails.studentDTO.studentNumber = $('').val();
        userDetails.addUserDTO.password = $('').val();
        userDetails.addUserDTO.confirmPassword = $('').val();
        console.log(userDetails);
        sessionStorage.setItem("userDetails", JSON.stringify(userDetails));
        window.location.href = "register-student-two.html";
    });
});