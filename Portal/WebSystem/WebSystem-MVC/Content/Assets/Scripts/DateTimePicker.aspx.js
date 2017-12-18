function pageLoad() {
    toggleTime(WCMS.Dom.Get("chkTime"));
}

function toggleTime(chk) {
    WCMS.Dom.Get("cboHour").disabled = !chk.checked;
    WCMS.Dom.Get("cboMinute").disabled = !chk.checked;
    WCMS.Dom.Get("cboSecond").disabled = !chk.checked;
}

function returnDate() {
    if (isInline()) {
        var rValue = window.parent.returnValue;
        if (rValue.callback) {
            rValue.callback(WCMS.Dom.Get("hiddenValue").value);
        }
    } else {
        var rValue = window.opener.returnValue;
        if (rValue != null) {
            rValue.target.value = WCMS.Dom.Get("hiddenValue").value;
            window.close();
        }
    }
}

function closeWindow(value) {
    if (isInline()) {
        var rValue = window.parent.returnValue;
        if (rValue.callback) {
            rValue.callback();
        }
    } else {
        window.returnValue = value;
        window.close();
    }
    return false;
}

function isInline() {
    return WCMS.Dom.Get("hInline").value == "1";
}