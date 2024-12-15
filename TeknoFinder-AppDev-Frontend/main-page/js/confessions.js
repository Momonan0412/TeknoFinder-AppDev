function getConfessionTemplate(confession){
    context = {
        firstname: confession.student.firstName,
        lastname: confession.student.lastName,
        username: "I am BUG",
        title: confession.title,
        content: confession.content,
        location: confession.contextValue,
        datetime: "BUG"
    }
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
// Building - Floor - Room
const Codes ={
    "NGE":{
        1:[1,2,3]
    },
    "GLE":{
        1:[1,2,3]
    }
}


$(document).ready(function () {
    let confessions_global = [];
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
            confessions_global = response;
            response.forEach(confession => {
                // console.log("Confession: ", confession);
                $('#recentPosts').append(getConfessionTemplate(confession));
                // get my confessions
                if (confession.studentId == studentId){
                    $('#myConfessions').append(getConfessionTemplate(confession));
                }
            });
        },
        error: function (error) {
            console.error("Error: ", error);
        }
    })


    // post confession
    $('#submit-confession').click(function () {
        var location = "NGE101" // CHANGE
        var type = "Classroom" // CHANGE
        // get selected building room and floor
        let building = sessionStorage.getItem("selectedBuilding");
        let floor = sessionStorage.getItem("selectedFloor");
        let room = sessionStorage.getItem("selectedRoom");

        if (building != "None"){
            type = "Building";
            location = building;
        }
        if (floor != "None"){
            type = "Floor";
            location += `${floor}00`;
        }
        if(room != "None"){
            type = "Classroom";
            location = `${building}${room}`;
        }
        console.log("Location: ", location);
        console.log("Type: ", type);
        var title = $('#confessionTitle').val();
        var content = $('#confessionText').val();
        var studentId = sessionStorage.getItem("studentId");
        var token = sessionStorage.getItem("token");
        console.log("Token: ", token);
        console.log("Student ID: ", studentId);
        var confession = {
            title: title,
            content: content,
            contextType: type,
            contextValue: location,
        }
        console.log("Confession: ", confession);
        $.ajax({
            url: `http://localhost:5099/api/Confessions`,
            type: 'POST',
            contentType: 'application/json',
            headers: {
                Authorization: `Bearer ${token}`, // Add JWT token in Authorization header
            },
            data: JSON.stringify(confession),
            success: function (response) {
                console.log("Response from server: ", response);
                alert("Confession posted successfully!");
                window.location.reload();
            },
            error: function (error) {
                console.error("Error: ", error);
            }
        });
    });


    // build building dropdown

    
});