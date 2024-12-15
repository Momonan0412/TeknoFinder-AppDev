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
    console.log("User Details: " + sessionStorage.getItem("userDetails"));
    const userDetails = JSON.parse(sessionStorage.getItem("userDetails"));
    console.log(window.location.href);
    // I know this is disgusting, please dont judge me I dont have a lot of time
    $('#reg-one').click(function() {
        console.log("redirecting....")
        userDetails.studentDTO.firstName = $('#firstname').val();
        userDetails.studentDTO.lastName = $('#lastname').val();
        userDetails.addUserDTO.username = $('#username').val();
        userDetails.studentDTO.yearLevel = $('#yearlevel').val();
        userDetails.studentDTO.program = $('#program').val();
        console.log(userDetails);
        sessionStorage.setItem("userDetails", JSON.stringify(userDetails));
        window.location.href = "register-student-two.html";
    });
    $('#reg-two').click(function() {
        console.log("reg two was clciked")
        userDetails.studentDTO.studentNumber = $('#studentnumber').val();
        userDetails.studentDTO.status = $('#studentstatus').val();
        userDetails.addUserDTO.email = $('#studentemail').val();
        userDetails.addUserDTO.password = $('#password').val();
        userDetails.addUserDTO.confirmPassword = $('#confirmpassword').val();
        console.log("Form Accepted")
        console.log(userDetails);
        sessionStorage.setItem("userDetails", JSON.stringify(userDetails));
        sessionStorage.removeItem("userDetails");
        $.ajax({
          url: 'http://localhost:5099/api/Account/Register',
          type: 'POST',
          contentType: 'application/json',
          data: JSON.stringify(userDetails),
          success: function(response) {
            window.location.href= "../../landing-page/templates/landing-home.html"
            console.log("Response from server: ", response);
          },
          error: function(error) {
            console.error("Error: ", error);
          }
        })
    });
});