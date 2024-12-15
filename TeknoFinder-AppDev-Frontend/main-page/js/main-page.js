/* Footer Functions */
// getting identity
$(document).ready(function () {
    var token = sessionStorage.getItem("token");
    var studentId = sessionStorage.getItem("studentId");
    console.log("Token: ", token);
    console.log("Student ID: ", studentId);
    $.ajax({
        url: `http://localhost:5099/api/Students/${studentId}`,
        type: 'GET',
        contentType: 'application/json',
        headers: {
          Authorization: `Bearer ${token}`, // Add JWT token in Authorization header
        },
        success: function (response) {
            console.log("Response from server: ", response);
            $('#student-username-label').text(response.username);
            $('#student-number-label').text(response.studentNumber);
            // $('#student-program').text(response.program);
            // $('#student-year').text(response.yearLevel);
            // $('#student-status').text(response.status);

        },
        error: function (error) {
            console.error("Error: ", error);
        }
    })
});
document.addEventListener("DOMContentLoaded", function () {
	const scrollToTopBtn = document.getElementById("scrollToTopBtn");

	window.addEventListener("scroll", () => {
		if (window.scrollY > 300) {
		  scrollToTopBtn.style.display = "block";
		} else {
		  scrollToTopBtn.style.display = "none";
		}
	});

	scrollToTopBtn.addEventListener("click", () => {
		window.scrollTo({
		  top: 0,
		  behavior: "smooth",
		});
	});
});


const howItWorksLink = document.getElementById("citu360Link");
const howItWorksSection = document.getElementById("citu360");

howItWorksLink.addEventListener("click", function (event) {
	event.preventDefault();

	howItWorksSection.scrollIntoView({
		behavior: "smooth", 
		block: "start" 
	});
});

/* Image Slider */
let items = document.querySelectorAll('.slider .list .item');
let next = document.getElementById('next');
let prev = document.getElementById('prev');
let thumbnails = document.querySelectorAll('.thumbnail .item');

// config param
let countItem = items.length;
let itemActive = 0;
// event next click
next.onclick = function(){
    itemActive = itemActive + 1;
    if(itemActive >= countItem){
        itemActive = 0;
    }
    showSlider();
}
//event prev click
prev.onclick = function(){
    itemActive = itemActive - 1;
    if(itemActive < 0){
        itemActive = countItem - 1;
    }
    showSlider();
}

// auto run slider
let refreshInterval = setInterval(() => {
    next.click();
}, 5000)

function showSlider(){
    // remove item active old
    let itemActiveOld = document.querySelector('.slider .list .item.active');
    let thumbnailActiveOld = document.querySelector('.thumbnail .item.active');
    itemActiveOld.classList.remove('active');
    thumbnailActiveOld.classList.remove('active');

    // active new item
    items[itemActive].classList.add('active');
    thumbnails[itemActive].classList.add('active');
    setPositionThumbnail();

    // clear auto time run slider
    clearInterval(refreshInterval);
    refreshInterval = setInterval(() => {
        next.click();
    }, 5000)
}

function setPositionThumbnail () {
    let thumbnailActive = document.querySelector('.thumbnail .item.active');
    let rect = thumbnailActive.getBoundingClientRect();
    if (rect.left < 0 || rect.right > window.innerWidth) {
        thumbnailActive.scrollIntoView({ behavior: 'smooth', inline: 'nearest' });
    }
}

// click thumbnail
thumbnails.forEach((thumbnail, index) => {
    thumbnail.addEventListener('click', () => {
        itemActive = index;
        showSlider();
    })
})
