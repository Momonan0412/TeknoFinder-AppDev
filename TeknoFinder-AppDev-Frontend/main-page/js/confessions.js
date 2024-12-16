const sliders = document.querySelectorAll('.recent-posts-slider');
sliders.forEach(slider => {
    const leftBtn = slider.querySelector('.left-btn');
    const rightBtn = slider.querySelector('.right-btn');
    const posts = slider.querySelector('.recent-posts');

    let scrollAmount = 0;
    const scrollStep = 300;

    rightBtn.addEventListener('click', () => {
        posts.scrollLeft += scrollStep;
    });

    leftBtn.addEventListener('click', () => {
        posts.scrollLeft -= scrollStep;
    });
});
const profilepics =[

];
function stringToNumber(input, range = profilepics.length) {
    let hash = 0;

    // Generate a hash from the input string
    for (let i = 0; i < input.length; i++) {
        hash = input.charCodeAt(i) + ((hash << 5) - hash);
    }

    // Ensure the number is within the desired range
    return Math.abs(hash % range);
}
function getProfilePic(studentId){
    return profilepics[stringToNumber(studentId)];
}

const modal = document.getElementById("addConfessionModal");
const openModalBtn = document.getElementById("openAddConfessionModal");
const closeModalBtn = document.getElementById("closeModal");

$('.filter-btn').click(function () {
    let search = $('.search-bar').val();
    //clean the recent and my posts
    $('#recentPosts').html('');
    $('#myConfessions').html('');
    //fetch all confessions and filter through
    var token = sessionStorage.getItem("token");
    var studentId = sessionStorage.getItem("studentId");
    console.log("Token: ", token);
    console.log("Student ID: ", studentId);
    $.ajax({
        url: `http://localhost:5099/api/Confessions`,
        type: 'GET',
        headers: {
            Authorization: `Bearer ${token}`, // Add JWT token in Authorization header
        },
        contentType: 'application/json',
        success: function (response) {
            console.log("Response from server (response): ", response);
            //filter where content includes search
            response.filter(confession => confession.content.includes(search));
            confessions_global = response;
            response.sort((a, b) => new Date(b.createdOn) - new Date(a.createdOn)); // sort by date
            response.forEach(confession => {
                if (confession.content.includes(search)) {
                    $('#recentPosts').append(getConfessionTemplate(confession));
                }
                // get my confessions
                if (confession.studentId == studentId && confession.content.includes(search)){
                    $('#myConfessions').append(getConfessionTemplate3(confession));
                }
            });
        },
        error: function (error) {
            console.error("Error: ", error);
        }
    })
});

openModalBtn.onclick = function () {
    modal.style.display = "block";
}

closeModalBtn.onclick = function () {
    modal.style.display = "none";
}

window.onclick = function (event) {
    if (event.target === modal) {
        modal.style.display = "none";
    }
}

document.getElementById('addConfessionForm').addEventListener('submit', function (event) {
    event.preventDefault(); 
    const confessionText = document.getElementById("confessionText").value;
    if (confessionText) {
        alert('Confession added: ' + confessionText);
        modal.style.display = "none";
    }
});

const buildingFilter = document.querySelector('.building-filter');
const floorFilter = document.querySelector('.floor-filter');
const roomFilter = document.querySelector('.room-filter');

const roomOptions = {
    "NGE": {
        1: ['101', '102', '103', '104', '105'],
        2: ['201', '202', '203', '204', '205'],
        3: ['301', '302', '303', '304', '305'],
        4: ['401', '402', '403', '404', '405']
    },
    "RTL": {
        1: ['101', '102', '103', '104', '105'],
        2: ['201', '202', '203', '204', '205'],
        3: ['301', '302', '303', '304', '305'],
        4: ['401', '402', '403', '404', '405']
    },
    "GLE": {
        1: ['101', '102', '103', '104', '105'],
        2: ['201', '202', '203', '204', '205'],
        3: ['301', '302', '303', '304', '305'],
        4: ['401', '402', '403', '404', '405']
    }
};

buildingFilter.addEventListener('change', () => {
    console.log('global confessions', confessions_global);
    const selectedBuilding = buildingFilter.value;
    sessionStorage.setItem('selectedBuilding', selectedBuilding);
    sessionStorage.setItem('selectedFloor', 'None');
    sessionStorage.setItem('selectedRoom', 'None');
    floorFilter.innerHTML = '<option value="None">Select Floor</option>';
    roomFilter.innerHTML = '<option value="None">Select Room</option>';
    $('#filteredConfessions').html(''); // clear filtered confessions
    if (roomOptions[selectedBuilding]) {
        const floors = Object.keys(roomOptions[selectedBuilding]);
        floors.forEach(floor => {
            const option = document.createElement('option');
            option.value = floor;
            option.textContent = `Floor ${floor}`;
            floorFilter.appendChild(option);
        });
        const filteredConfessions = confessions_global.filter(confession => {
            const identifier = getConfessionIdentifier();
            console.log(identifier);
            return confession.contextType === identifier.type && confession.contextValue === identifier.location; 
        });
        console.log("Filtered Confessions: ", filteredConfessions);
        filteredConfessions.forEach(confession => {
            $('#filteredConfessions').append(getConfessionTemplate2(confession));
        });
    }
});

floorFilter.addEventListener('change', () => {
    $('#filteredConfessions').html(''); // clear filtered confessions
    const selectedBuilding = buildingFilter.value;
    const selectedFloor = floorFilter.value;
    sessionStorage.setItem('selectedBuilding', selectedBuilding);
    sessionStorage.setItem('selectedFloor', selectedFloor);
    sessionStorage.setItem('selectedRoom', 'None')
    roomFilter.innerHTML = '<option value="None">Select Room</option>';

    if (roomOptions[selectedBuilding] && roomOptions[selectedBuilding][selectedFloor]) {
        const rooms = roomOptions[selectedBuilding][selectedFloor];
        rooms.forEach(room => {
            const option = document.createElement('option');
            option.value = room;
            sessionStorage.setItem('selectedRoom', room);
            option.textContent = `Room ${room}`;
            roomFilter.appendChild(option);
        });
        const filteredConfessions = confessions_global.filter(confession => {
            const identifier = getConfessionIdentifier();
            console.log(identifier);
            return confession.contextType === identifier.type && confession.contextValue === identifier.location; 
        });
        console.log("Filtered Confessions: ", filteredConfessions);
        filteredConfessions.forEach(confession => {
            $('#filteredConfessions').append(getConfessionTemplate2(confession));
        });
    }
});

roomFilter.addEventListener('change', () => {
    $('#filteredConfessions').html(''); // clear filtered confessions
    const selectedBuilding = buildingFilter.value;
    const selectedFloor = floorFilter.value;
    const selectedRoom = roomFilter.value;
    sessionStorage.setItem('selectedBuilding', selectedBuilding);
    sessionStorage.setItem('selectedFloor', selectedFloor);
    sessionStorage.setItem('selectedRoom', selectedRoom);
    const filteredConfessions = confessions_global.filter(confession => {
        const identifier = getConfessionIdentifier();
        console.log(identifier);
        return confession.contextType === identifier.type && confession.contextValue === identifier.location; 
    });
    console.log("Filtered Confessions: ", filteredConfessions);
    filteredConfessions.forEach(confession => {
        $('#filteredConfessions').append(getConfessionTemplate2(confession));
    });
});

function getConfessionIdentifier(){
    var location = "NGE101" // CHANGE
    var type = "Classroom" // CHANGE
    // get selected building room and floor
    let building = sessionStorage.getItem("selectedBuilding");
    let floor = sessionStorage.getItem("selectedFloor");
    let room = sessionStorage.getItem("selectedRoom");
    console.log(building, floor, room);
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
    return {location, type};
}

function getConfessionTemplate(confession){
    context = {
        firstname: confession.student.firstName,
        lastname: confession.student.lastName,
        username: confession.student.username,
        title: confession.title,
        content: confession.content,
        location: confession.contextValue,
        datetime: new Date(confession.createdOn).toLocaleString('en-GB', { dateStyle: 'short', timeStyle: 'short' })
    }
    // console.log("Preparinf confession template...");
    // console.log("Context: ", context);
    // console.log("First Name: ", context.username);
    let postTemplate = `
    <div class="post-item">
        <div class="profilepic-container">
            <img src="" style="min-width: 70px; min-height: 70px; max-width: 70px; max-height: 70px; ">
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
function getConfessionTemplate2(confession){
    context = {
        firstname: confession.student.firstName,
        lastname: confession.student.lastName,
        username: confession.student.username,
        title: confession.title,
        content: confession.content,
        location: confession.contextValue,
    datetime: new Date(confession.createdOn).toLocaleString('en-GB', { dateStyle: 'short', timeStyle: 'short' })
    }
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
    `;
    return postTemplate;
}

function getConfessionTemplate3(confession){
    context = {
        firstname: confession.student.firstName,
        lastname: confession.student.lastName,
        username: confession.student.username,
        title: confession.title,
        content: confession.content,
        location: confession.contextValue,
        datetime: new Date(confession.createdOn).toLocaleString('en-GB', { dateStyle: 'short', timeStyle: 'short' })
    }
    let postTemplate = `
    <div class="post-item">
        <div class="profilepic-container">
            <img src=".jpg" >
        </div>
        <div class="post-details">
            <div class="detail-head">
                <p class="post-text" style="letter-spacing: 1px;"><strong>${context.firstname} ${context.lastname}</strong></p>
                <span>(${context.username})</span>
                <i class="fa fa-trash" id="delete-icon"></i>
            </div>
            <hr>
            <p class="post-text">${context.content}</p>
            <div class="datelocation">
                <span class="post-info">${context.location}</span>
                <span class="post-info">${context.datetime}</span>
            </div>
        </div>
    </div>
    `;
    return postTemplate;
}

var confessions_global = [];
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
            confessions_global = response;
            response.sort((a, b) => new Date(b.createdOn) - new Date(a.createdOn)); // sort by date
            response.forEach(confession => {
                // console.log("Confession: ", confession);
                $('#recentPosts').append(getConfessionTemplate(confession));
                // get my confessions
                if (confession.studentId == studentId){
                    $('#myConfessions').append(getConfessionTemplate3(confession));
                }
            });
        },
        error: function (error) {
            console.error("Error: ", error);
        }
    })


    // post confession
    $('#submit-confession').click(function () {
        var identifier = getConfessionIdentifier();
        var location = identifier.location;
        var type = identifier.type;
        
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