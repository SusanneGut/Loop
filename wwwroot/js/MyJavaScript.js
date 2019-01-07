function openActivity(activityId) {
    var x = document.getElementsByClassName("activity");
    for (var i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    document.getElementById(activityId).style.display = "block";
}