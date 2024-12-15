function getConfessionTemplate(context){
console.log("Preparinf confession template...");
console.log("Context: ", context);
console.log("First Name: ", context.username);
let postTemplate = `
<div class="post-item">
    <div class="profilepic-container">
        <img src=".jpg" >
    </div>
    <div class="post-details">
        <div class="detail-head">
            <p class="post-text" style="letter-spacing: 1px;"><strong>${context.firstname} ${context.lastname}</strong></p>
            <span>(${context.username})</span>
        </div>
        <hr>
        <p class="post-title">${context.title}</p>
        <p class="post-text">${context.content}</p>
        <div class="datelocation">
            <span class="post-info">${context.location}</span>
            <span class="post-info">${context.datetime}</span>
        </div>
    </div>
</div>
`
return postTemplate;
}

$(document).ready(function () {
    var token = sessionStorage.getItem("token");
    var studentId = sessionStorage.getItem("studentId");
    console.log("Token: ", token);
    console.log("Student ID: ", studentId);
    //fetch all confessions
    $.ajax({
        url: `http://localhost:5099/api/Confessions`,
        type: 'GET',
        contentType: 'application/json',
        headers: {
          Authorization: `Bearer ${token}`, // Add JWT token in Authorization header
        },
        success: function (response) {
            console.log("Response from server (response): ", response);
            response.forEach(confession => {
                context = {
                    firstname: confession.student.firstName,
                    lastname: confession.student.lastName,
                    username: "I am BUG",
                    title: confession.title,
                    content: confession.content,
                    location: confession.contextValue,
                    datetime: "BUG"
                }
                // console.log("Confession: ", confession);
                $('.recent-posts').append(getConfessionTemplate(context));
            });
        },
        error: function (error) {
            console.error("Error: ", error);
        }
    })
});