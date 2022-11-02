function initializemap(lat,lon) {
    var latlng = new google.maps.LatLng(lat, lon);
    var options = {
        zoom: 9, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("map"), options);
}