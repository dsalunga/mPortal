function setCurrentLocation(map, latLng) {
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ "latLng": latLng },
        function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var addressLabel = results[0].formatted_address;
                $('#currentLocation').html(addressLabel);
                setMapMarker(map, latLng, true, 'My Location', "<div><h4>My Location</h4><p>" + addressLabel + "<p><div>");
            }
        }
    );
}

function buildInfoWindow(name, address, url) {
    return "<div><h4>" + name +
        "</h4><p><span class='glyphicon glyphicon-map-marker'></span> " + address + "</p><p><a class='btn btn-default btn-sm' href='" + url + "'>Details</a></p><div>";
}

function initializeMap(latLng) {
    var map = new google.maps.Map(document.getElementById('mapContainer'), {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        center: latLng,
        zoom: 15
    });
    return map;
}

function setMapMarker(map, latLng, isNearby, title, infoContent) {
    var marker = null;
    if (isNearby) {
        var image = {
            url: 'https://maps.google.com/mapfiles/ms/icons/blue-dot.png',
            anchor: new google.maps.Point(16, 32)
        };

        var shadow = {
            url: 'https://maps.google.com/mapfiles/ms/icons/msmarker.shadow.png',
            anchor: new google.maps.Point(16, 32)
        };

        marker = new google.maps.Marker({
            map: map,
            position: latLng,
            icon: image,
            shadow: shadow,
            title: title
        });
    } else {
        marker = new google.maps.Marker({
            map: map,
            position: latLng,
            title: title
        });
    }

    if (infoContent != null) {
        var infoWindow = new google.maps.InfoWindow({
            content: infoContent
        });

        google.maps.event.addListener(marker, 'click', function () {
            infoWindow.open(map, marker);
        });
    }
}