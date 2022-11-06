function initializemap(lat,lon) {
    var latlng = new google.maps.LatLng(lat, lon);
    var options = {
        zoom: 13, center: latlng,
/*        mapTypeId: google.maps.MapTypeId.ROADMAP,*/
        mapId: '1dff965742cecac1'
    };
    var map = new google.maps.Map(document.getElementById("map"), options);
    const ftsIcon = {
        url: "../images/icon.svg", // url
        scaledSize: new google.maps.Size(18, 18), // scaled size
        origin: new google.maps.Point(0, 0), // origin
        anchor: new google.maps.Point(10, 10) // anchor
    };
    var markerg = new google.maps.Marker({
        position: latlng,
        map: map,
        title: "Follow the sun",
        icon: ftsIcon,
    });
}