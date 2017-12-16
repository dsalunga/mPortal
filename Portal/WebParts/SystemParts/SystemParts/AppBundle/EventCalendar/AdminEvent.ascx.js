var weeklyIndex = 2;

var cboLocations = $("#cboLocations");
var cmdCheckLocation = $("#cmdCheckLocation");
var txtLocation = $("#txtLocation");
var chkOtherLocation = $("#chkOtherLocation");
var rblRecurrence = $('[name$=$rblRecurrence]'); //WCMS.Dom.GetByName("rblRecurrence");
var spanWeekdays = $("#spanWeekdays");

$(document).ready(function () {
    Location_Toggle();
    Weekdays_Toggle();

    $("#chkNoEnd").change(function () {
        CheckEndDate();
    });

    CheckEndDate();
});

function CheckEndDate() {
    var checkNoEnd = $("#chkNoEnd");
    if (checkNoEnd.length > 0) {
        var doesNotEndChecked = $(checkNoEnd).is(":checked");
        $("#txtRepeatUntil").attr("disabled", doesNotEndChecked);
        $("#imgRepeatUntilPicker").css("display", doesNotEndChecked ? "none" : "");
    }
}

function Location_Toggle() {
    if (chkOtherLocation.length > 0) {
        if ($(chkOtherLocation).is(":checked")) {
            $(cboLocations).attr("disabled", true);
            $(cmdCheckLocation).attr("disabled", true);
            $(txtLocation).attr("disabled", false);
        }
        else {
            $(txtLocation).attr("disabled", true);
            $(cboLocations).attr("disabled", false);
            $(cmdCheckLocation).attr("disabled", false);
        }
    }
}

function Weekdays_Toggle() {
    if (rblRecurrence.length > 0 && spanWeekdays.length > 0) {
        $(spanWeekdays).css("display", rblRecurrence[weeklyIndex].checked ? "" : "none");
    }
}

function Add_Click() {
    var addValue = $("#txtAdd").val().Trim();
    var baseGroup = $("#hBaseGroup").val();

    if (addValue == "") {
        ShowAccountBrowser("txtAdd", -1, 1, 1, 1, "cmdAdd", baseGroup);
        return false;
    }

    return true;
}


if (chkOtherLocation.length > 0) {
    $(chkOtherLocation).click(Location_Toggle);
}

if (rblRecurrence.length > 0) {
    for (var i = 0; i < rblRecurrence.length; i++) {
        WCMS.Dom.AddEvent(rblRecurrence[i], "click", Weekdays_Toggle);
    }
}