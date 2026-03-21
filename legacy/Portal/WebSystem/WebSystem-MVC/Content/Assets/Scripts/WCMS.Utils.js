function ExecuteKeepAlive(interval) {
    var timerDelay = interval == null ? 5 * 60 * 1000 : interval;

    setInterval(function () {
        $.ajax({
            type: "GET",
            url: "/Content/Handlers/AjaxHandler.ashx",
            data: "Method=KeepAlive&" + (new Date()).valueOf()
        });
    }, timerDelay);
}

function ExecuteSessionCheck(invalidUrl, interval) {
    var timerDelay = interval == null ? 5 * 60 * 1000 : interval;

    return setInterval(function () {
        $.ajax({
            type: "GET",
            url: "/Content/Handlers/AjaxHandler.ashx",
            data: "Method=SessionValid&" + (new Date()).valueOf(),
            success: function (text) {
                if (text == '0') {
                    location.href = invalidUrl;
                }
            }
        });
    }, timerDelay);
}

function ExecuteSessionCheckCallback(interval, callback) {
    var timerDelay = interval == null ? 5 * 60 * 1000 : interval;

    return setInterval(function () {
        $.ajax({
            type: "GET",
            url: "/Content/Handlers/AjaxHandler.ashx",
            data: "Method=SessionValid&" + (new Date()).valueOf(),
            success: function (text) {
                callback(null, text);
            }
        });
    }, timerDelay);
}

function ResolveGeoIp(callback, ip) {
    var url = ip == null ? "https://www.telize.com/geoip?callback=?" : "https://www.telize.com/geoip/" + ip + "?callback=?";
    $.getJSON(url, function (json) {
        //$('#hLocation').val(json.city && json.city === json.country ? json.country : json.city + ', ' + json.country);
        var loc = typeof json.city === 'undefined' || json.city === json.country ? json.country : json.city + ', ' + json.country;
        callback(loc);
    }).fail(function () {
        // Error scenario
    });
}

function ShowDateTimePicker(target) {
    if (target != null) {
        window.returnValue = {
            "target": target
        };
        WCMS.Dom.Open("/content/windows/DateTimePicker.aspx?Value=" + target.value, 280, 345);
        return true;
    }
    return false;
}

function Upload(cntElement, uploadFolder, sArgs) {
    // s_args:
    // _f=true ->> return only filename
    // _filename=dest_filename (without extension) ->> uses the provided filename when saving

    var left = (screen.availWidth / 2) - (600 / 2);
    var top = (screen.availHeight / 2) - (300 / 2);
    window.open('/content/windows/Upload.aspx?UploadFolder=' + uploadFolder + '&Control=' + cntElement + sArgs, 'FileManager', 'width=600,height=300,left=' + left + ',top=' + top);
}

function BrowseLink(cntElement, partId, siteId) {
    var pId = partId == null ? -1 : partId;
    var left = (screen.availWidth / 2) - (600 / 2);
    var top = (screen.availHeight / 2) - (500 / 2);

    var windowUrl = "/content/windows/LinkBrowser.aspx?Control=" + cntElement + "&PartId=" + pId;
    if (typeof siteId != 'undefined' && siteId != null)
        windowUrl += "&SiteId=" + siteId;

    window.open(windowUrl, 'LinkBrowser', 'resizable=1,scrollbars=1,width=600,height=500,left=' + left + ',top=' + top);
    return false;
}

function ShowAccountBrowser(inputBox, objectId, returnUnique, appendValue, multi, returnClick, baseGroup) {
    window.returnValue = {
        "param": WCMS.Dom.Get(inputBox),
        "returnClick": returnClick == null ? null : WCMS.Dom.Get(returnClick)
    };

    returnUnique = returnUnique == 0 ? 0 : 1;
    multi = multi == 0 ? 0 : 1;
    var aValue = appendValue == null ? "0" : appendValue;
    var oId = objectId == null ? -1 : objectId; // WebUser=21, WebGroup=38

    var dialogUrl = "/content/windows/AccountBrowser.aspx?ObjectId=" + oId + "&ReturnUnique=" + returnUnique + "&AppendValue=" + aValue + "&Multi=" + multi;

    if (baseGroup != null)
        dialogUrl += "&BaseGroup=" + baseGroup;

    WCMS.Dom.Show(dialogUrl, 900, 652);
}

function GetPageName(pageUrl, targetObj) {
    $.ajax({
        type: "GET",
        url: "/content/handlers/AjaxHandler.ashx",
        data: "Method=GetText&" + (new Date()).valueOf() + "&Url=" + pageUrl,
        success: function (text) {
            //alert("Text: " + text);
            var targetText = $(targetObj).val();
            if (targetText == '') {
                $(targetObj).val(text);
            }
        }
    });
}

function resizeIframe(obj, cont) {
    cont.style.height = obj.contentWindow.document.body.scrollHeight + 'px';
}