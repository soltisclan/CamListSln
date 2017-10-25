var httpRequest;

function init() {
    var date = new Date();
    date = date.setMinutes(date.getMinutes() - date.getTimezoneOffset());
    resizeList();
    getDates();
}

function getDates() {
    httpRequest = new XMLHttpRequest();

    if (!httpRequest) {
        alert('Giving up :( Cannot create an XMLHTTP instance');
        return false;
    }
    httpRequest.onreadystatechange = setDates;
    httpRequest.open('GET', 'api/outdoor/list/');
    httpRequest.send();
}

function setDates() {

    if (httpRequest.readyState === XMLHttpRequest.DONE) {
        if (httpRequest.status === 200) {
            var dates = JSON.parse(httpRequest.response);
            var datepicker = document.getElementById("date");
            datepicker.setAttribute("max", dates[dates.length - 1]);
            datepicker.setAttribute("min", dates[0]);
            datepicker.valueAsDate = new Date(dates[dates.length - 1]);
            getVideos(document.getElementById("date").value) 
        }
    }

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
            var listDiv = document.getElementById("list");
            while (listDiv.hasChildNodes()) {
                listDiv.removeChild(listDiv.lastChild);
            }
            var videos = JSON.parse(httpRequest.response);
            var previousHour = "";
            videos.reverse();
            videos.forEach(function (video) {
                var a = document.createElement("A");
                var br = document.createElement("BR");
                var fileSize = video.size / 1048576;
                var linkTime = document.createTextNode(getTime(video.date) + " (" + fileSize.toFixed(2) + "MB)");

                var currentHour = getTime(video.date).substring(0, 2);
                if (currentHour != previousHour) {
                    var p = document.createElement("P");
                    var linkHour = document.createTextNode(currentHour);
                    p.appendChild(linkHour);
                    listDiv.appendChild(p);
                }
                previousHour = currentHour;

                a.appendChild(linkTime);
                var onClickAttr = document.createAttribute("onclick");
                onClickAttr.value = "playVideo('api/outdoor/file/" + video.key + "')";
                a.setAttributeNode(onClickAttr);
                a.href = "#";
                listDiv.appendChild(a);
                listDiv.appendChild(br);
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

function getTime(datetime) {
    return datetime.split("T")[1];
}

function resizeList() {
	var list = document.getElementById("list");
	var video = document.getElementById("video");
	var datepicker = document.getElementById("datepicker");

	if (window.innerWidth < 992) {
		var height = window.innerHeight - video.offsetHeight - datepicker.offsetHeight;
		list.style.height = height + "px"
	} else  {
		var height = video.offsetHeight - datepicker.offsetHeight;
		list.style.height = height +"px";
	}
}