function getIEVersion() {
    var ua = window.navigator.userAgent
    var msie = ua.indexOf("MSIE ")
    if (msie > 0)      // is Microsoft Internet Explorer; return version number
        return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
    else
        return 0;      // is other browser
}