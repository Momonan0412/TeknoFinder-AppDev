function getConfessionTemplate(context){
const postTemplate = `
<div class="post-details">
    <div class="detail-head">
        <p class="post-text" style="letter-spacing: 1px;"><strong>${context.firstname} ${context.lastname}}</strong></p>
        <span>(${context.username})</span>
    </div>
    <hr>
    <p class="post-text">{confession content}</p>
    <div class="datelocation">
        <span class="post-info">{Building #, Floor #, Room #}</span>
        <span class="post-info">{date | time}</span>
    </div>
</div>
`
}

$(document).ready(function () {

});