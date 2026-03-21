var hasVoted = false;
var hasValidInfo = false;
var userName = "";

$.mobile.defaultPageTransition = "slide";

$('#pageLogin').live('pageinit', function (event) {
    $("#buttonLogin").click(function () {
        var username = $.trim($("#textUsername").val());
        var password = $.trim($("#textPassword").val());

        if (username == "" || password == "") {
            showAlert("Please enter your username or password.", "Username or Password", "#pageLogin");
            return false;
        }

        $.mobile.showPageLoadingMsg();

        $.ajax({
            type: "POST",
            url: "/Content/Parts/Common/FxService.asmx/Login",
            data: JSON.stringify({
                "userName": username,
                "password": password,
                "loginSession": true
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $.mobile.hidePageLoadingMsg();

                if (data.d) {
                    userName = username;

                    /*
                    if (!data.d.IsServiceAccount) {
                        $("#textFirstName").val(data.d.FirstName);
                        $("#textLastName").val(data.d.LastName);
                        $("#textMobile").val(data.d.MobileNumber);
                        $("#textEmail").val(data.d.Email);
                    }
                    */

                    resetLogin();

                    $.mobile.changePage("#pageDashboard");
                }
                else {
                    showAlert("Invalid username or password.", "Invalid logon", "#pageLogin");
                }
            },
            error: function (request, status, error) {
                $.mobile.hidePageLoadingMsg();

                showAlert("An error has occurred, please contact ADDCIT: " + error, "Error", "#pageLogin");
            }
        });
    });
});

$("#pageScore").live("pageinit", function (event) {
    /*
    $("#buttonInfoCancel").click(function () {
        
    });

    $("#buttonInfoNext").click(function () {
        
    });
    */

    /*
    if (userName == "") {
        resetFlow(false);

        $.mobile.changePage("#pageLogin");
        return false;
    }
    */
});

$("#pageDone").live("pageinit", function (event) {
    if (!hasVoted) {
        $.mobile.changePage("#pageLogin");
    }

    $("#buttonDone").click(function () {
        if (resetFlow(false)) {
            userName = "";

            $.mobile.changePage("#pageLogin");
        }
    });

    $("#buttonVoteAgain").click(function () {
        if (userName != "" && resetFlow(true)) {
            $.mobile.changePage("#pageVoting");
        }
    });
});

function resetLogin() {
    $("#textUsername").val("");
    $("#textPassword").val("");
}

function resetFlow(reVote) {
    resetLogin();

    if (!reVote) {
        $("#textFirstName").val("");
        $("#textLastName").val("");
        $("#textEmail").val("");
        $("#textMobile").val("");

        hasValidInfo = false;
    }

    if (hasVoted) {
        $("#textVotingCode").val("");
        $("input[name='radio-candidate']").attr("checked", false).checkboxradio("refresh");

        hasVoted = false;

        return true;
    }
    else {
        $.mobile.changePage("#pageLogin");

        return false;
    }
}

function showAlert(message, title, caller) {
    $("#alertTitle").html(title);
    $("#alertMessage").html(message);
    $("#alertOK").attr("href", caller);

    $.mobile.changePage("#pageAlert", { role: "dialog" });
}