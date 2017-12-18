function MM_swapImgRestore() { //v3.0
    var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
}

function MM_preloadImages() { //v3.0
    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();
        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
    }
}

function MM_findObj(n, d) { //v4.01
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && d.getElementById) x = d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
}

function NavigateLink(s_link) {
    //window.navigate(s_link);
    location.href = s_link;
}

function GetComboValue(cbo) {
    try {
        return cbo.item(cbo.selectedIndex).value;
    }
    catch (e) {
        return ".";
    }
}

function UpdateSMSCounter(smsTextArea, smsCharCounter, maxSMS) {
    var unicodeFlag = 0;
    var extraChars = 0;
    var msgCount = 0;

    for (var i = 0; (!unicodeFlag && (i < smsTextArea.value.length)); i++) {
        if ((smsTextArea.value.charAt(i) >= '0') && (smsTextArea.value.charAt(i) <= '9')) { }
        else if ((smsTextArea.value.charAt(i) >= 'A') && (smsTextArea.value.charAt(i) <= 'Z')) { }
        else if ((smsTextArea.value.charAt(i) >= 'a') && (smsTextArea.value.charAt(i) <= 'z')) { }
        else if (smsTextArea.value.charAt(i) == '@') { }
        else if (smsTextArea.value.charCodeAt(i) == 0xA3) { }
        else if (smsTextArea.value.charAt(i) == '$') { }
        else if (smsTextArea.value.charCodeAt(i) == 0xA5) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xE8) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xE9) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xF9) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xEC) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xF2) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xC7) { }
        else if (smsTextArea.value.charAt(i) == '\r') { }
        else if (smsTextArea.value.charAt(i) == '\n') { }
        else if (smsTextArea.value.charCodeAt(i) == 0xD8) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xF8) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xC5) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xE5) { }
        else if (smsTextArea.value.charCodeAt(i) == 0x394) { }
        else if (smsTextArea.value.charAt(i) == '_') { }
        else if (smsTextArea.value.charCodeAt(i) == 0x3A6) { }
        else if (smsTextArea.value.charCodeAt(i) == 0x393) { }
        else if (smsTextArea.value.charCodeAt(i) == 0x39B) { }
        else if (smsTextArea.value.charCodeAt(i) == 0x3A9) { }
        else if (smsTextArea.value.charCodeAt(i) == 0x3A0) { }
        else if (smsTextArea.value.charCodeAt(i) == 0x3A8) { }
        else if (smsTextArea.value.charCodeAt(i) == 0x3A3) { }
        else if (smsTextArea.value.charCodeAt(i) == 0x398) { }
        else if (smsTextArea.value.charCodeAt(i) == 0x39E) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xC6) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xE6) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xDF) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xC9) { }
        else if (smsTextArea.value.charAt(i) == ' ') { }
        else if (smsTextArea.value.charAt(i) == '!') { }
        else if (smsTextArea.value.charAt(i) == '\"') { }
        else if (smsTextArea.value.charAt(i) == '#') { }
        else if (smsTextArea.value.charCodeAt(i) == 0xA4) { }
        else if (smsTextArea.value.charAt(i) == '%') { }
        else if (smsTextArea.value.charAt(i) == '&') { }
        else if (smsTextArea.value.charAt(i) == '\'') { }
        else if (smsTextArea.value.charAt(i) == '(') { }
        else if (smsTextArea.value.charAt(i) == ')') { }
        else if (smsTextArea.value.charAt(i) == '*') { }
        else if (smsTextArea.value.charAt(i) == '+') { }
        else if (smsTextArea.value.charAt(i) == ',') { }
        else if (smsTextArea.value.charAt(i) == '-') { }
        else if (smsTextArea.value.charAt(i) == '.') { }
        else if (smsTextArea.value.charAt(i) == '/') { }
        else if (smsTextArea.value.charAt(i) == ':') { }
        else if (smsTextArea.value.charAt(i) == ';') { }
        else if (smsTextArea.value.charAt(i) == '<') { }
        else if (smsTextArea.value.charAt(i) == '=') { }
        else if (smsTextArea.value.charAt(i) == '>') { }
        else if (smsTextArea.value.charAt(i) == '?') { }
        else if (smsTextArea.value.charCodeAt(i) == 0xA1) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xC4) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xD6) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xD1) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xDC) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xA7) { }
        else if (smsTextArea.value.charCodeAt(i) == 0xBF) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0xE4) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0xF6) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0xF1) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0xFC) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0xE0) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x391) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x392) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x395) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x396) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x397) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x399) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x39A) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x39C) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x39D) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x39F) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x3A1) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x3A4) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x3A5) {
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x3A7) {
        }
        else if (smsTextArea.value.charAt(i) == '^') {
            extraChars++;
        }
        else if (smsTextArea.value.charAt(i) == '{') {
            extraChars++;
        }
        else if (smsTextArea.value.charAt(i) == '}') {
            extraChars++;
        }
        else if (smsTextArea.value.charAt(i) == '\\') {
            extraChars++;
        }
        else if (smsTextArea.value.charAt(i) == '[') {
            extraChars++;
        }
        else if (smsTextArea.value.charAt(i) == '~') {
            extraChars++;
        }
        else if (smsTextArea.value.charAt(i) == ']') {
            extraChars++;
        }
        else if (smsTextArea.value.charAt(i) == '|') {
            extraChars++;
        }
        else if (smsTextArea.value.charCodeAt(i) == 0x20AC) {
            extraChars++;
        }
        else {
            unicodeFlag = 1;
        }

        if (maxSMS > 0) {
            var smsCount = -1;
            if (unicodeFlag) {
                smsCount = i + 1;
                if (smsCount <= 70) {
                    smsCount = 1;
                }
                else {
                    smsCount += (67 - 1);
                    smsCount -= (smsCount % 67);
                    smsCount /= 67;
                }
            }
            else {
                smsCount = i + 1 + extraChars;
                if (smsCount <= 160) {
                    smsCount = 1;
                }
                else {
                    smsCount += (153 - 1);
                    smsCount -= (smsCount % 153);
                    smsCount /= 153;
                }
            }

            if (smsCount > maxSMS) {
                smsTextArea.value = smsTextArea.value.substring(0, i);
            }
        }
    }

    if (unicodeFlag) {
        msgCount = smsTextArea.value.length;
        if (msgCount <= 70) {
            msgCount = 1;
        }
        else {
            msgCount += (67 - 1);
            msgCount -= (msgCount % 67);
            msgCount /= 67;
        }
        smsCharCounter.innerHTML = "" + (smsTextArea.value.length) + " unicode characters, " + msgCount + " SMS message" + (msgCount > 1 ? "s" : "")
            + (maxSMS > 0 ? " (" + maxSMS + " MAX)" : "");
    }
    else {
        msgCount = smsTextArea.value.length + extraChars;
        if (msgCount <= 160) {
            msgCount = 1;
        }
        else {
            msgCount += (153 - 1);
            msgCount -= (msgCount % 153);
            msgCount /= 153;
        }

        smsCharCounter.innerHTML = "" + (smsTextArea.value.length + extraChars) + " characters, " + msgCount + " SMS message" + (msgCount > 1 ? "s" : "")
            + (maxSMS > 0 ? " (" + maxSMS + " MAX)" : "");
    }
}

function ClearSMSCounter(smsCharCounter) {
    smsCharCounter.innerHTML = "";
}


// return Microsoft Internet Explorer (major) version number, or 0 for others.
// This function works by finding the "MSIE " string and extracting the version number
// following the space, up to the decimal point for the minor version, which is ignored.
function msieversion() {
    var ua = window.navigator.userAgent
    var msie = ua.indexOf("MSIE ")
    if (msie > 0)        // is Microsoft Internet Explorer; return version number
        return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)))
    else
        return 0    // is other browser
}

function getIEVersion() {
    var ua = window.navigator.userAgent
    var msie = ua.indexOf("MSIE ")
    if (msie > 0)      // is Microsoft Internet Explorer; return version number
        return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
    else
        return 0;      // is other browser
}

// this does not work on IE8
function CheckAll(chk, chk_item) {
    var i, chk_items, isChecked;

    chk_items = document.getElementsByName(chk_item);
    isChecked = chk.checked;

    if (chk_items == null) {
        return;
    }

    if (chk_items.length > 0) {
        // MULTIPLE ITEMS
        for (i = 0; i < chk_items.length; i++) {
            chk_items[i].checked = isChecked; // this line
        }
    }
    else {
        // ONE ITEM ONLY
        chk_items.checked = isChecked;
    }
}

function CheckEnter(e) {
    if (!e) e = window.event;
    var key = e.keyCode ? e.keyCode : e.which;
    if (key == 13) {
        e.returnValue = false;
        if (window.event)
            window.event.keyCode = 0;
        $('#loginButton').click();
    }
}

/* Scripts/Common.js */

function ShowDelete() {
    return confirm('Are sure you want to delete?');
}

function closeWindow(value) {
    window.returnValue = value;
    window.close();
    return false;
}

function GenerateUrlName(rawName) {
    var r = rawName;
    var cs = new Array(/ /g, "/", "?", "\"", "\'", /</g, />/g, "[", /]/g, /&/g, /@/g, "\\");

    for (i = 0; i < cs.length; i++) {
        r = r.replace(cs[i], "-");
    }

    return r;
}

function GenerateFolderName(rawName) {
    var r = rawName;
    var cs = new Array(/ /g, "/", "?", "\"", "\'", /</g, />/g, "[", /]/g, /&/g, /@/g, "\\");

    for (i = 0; i < cs.length; i++) {
        r = r.replace(cs[i], "_");
    }

    return r;
}