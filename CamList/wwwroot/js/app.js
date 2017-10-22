var httpRequest;

function init() {
    var date = new Date();
    date = date.setMinutes(date.getMinutes()-date.getTimezoneOffset());
    document.getElementById("date").valueAsDate = new Date(date);
    getVideos(document.getElementById("date").value) 
}

function getVideos(date) {
    httpRequest = new XMLHttpRequest();

    if (!httpRequest) {
        alert('Giving up :( Cannot create an XMLHTTP instance');
        return false;
    }
    httpRequest.onreadystatechange = loadVideos;
    httpRequest.open('GET', 'api/outdoor/list/' + date);
    httpRequest.send();
}

function loadVideos() {
    if (httpRequest.readyState === XMLHttpRequest.DONE) {
        if (httpRequest.status === 200) {
            var contentDiv = document.getElementById("content");
            while (contentDiv.hasChildNodes()) {
                contentDiv.removeChild(contentDiv.lastChild);
            }
            var videos = JSON.parse(httpRequest.response);
            videos.reverse();
            videos.forEach(function (video) {
                var a = document.createElement("A");
                var br = document.createElement("BR");
                var linkText = document.createTextNode(video.date);
                a.appendChild(linkText);
                var onClickAttr = document.createAttribute("onclick");
                onClickAttr.value = "playVideo('api/outdoor/file/" + video.key + "')";
                a.setAttributeNode(onClickAttr);
                a.href = "#";
                contentDiv.appendChild(a);
                contentDiv.appendChild(br);
            });
            //alert(httpRequest.responseText);
        } else {
            alert('There was a problem with the request.');
        }
    }
}

function playVideo(sourceURL) {
    var video = document.getElementById('video');
    var source = document.createElement('source');

    source.setAttribute('src', sourceURL);
    source.setAttribute('type', 'video/mp4');


    video.pause();

    while (video.firstChild) {
        video.removeChild(video.firstChild);
    }


    video.appendChild(source);
    video.load();

    video.play();

}
