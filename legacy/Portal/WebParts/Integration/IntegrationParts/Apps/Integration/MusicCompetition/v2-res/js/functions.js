// JavaScript Document

var hasVoted = false;
var hasValidInfo = false;
var userName = "";
var doneUrl = ""; //"/Public/ASOP-Vote-Wall.aspx";
var isSvcAccount = false;

$(document).ready(function () {

    var showAlert = function (message, title, caller) {
        alert(message);
        /*
        $("#alertTitle").html(title);
        $("#alertMessage").html(message);
        $("#alertOK").attr("href", caller);

        $.mobile.changePage("#pageAlert", { role: "dialog" });
        */
    }

    var resetLogin = function () {
        $("#textUsername").val("");
        $("#textPassword").val("");
    }

    var resetFlow = function (reVote) {
        resetLogin();

        if (!reVote) {
            $("#textFirstName").val("");
            $("#textLastName").val("");
            $("#textEmail").val("");
            $("#textMobile").val("");

            hasValidInfo = false;
        }

        if (hasVoted) {
            //$("#textVotingCode").val("");
            //$("input[name='radio-candidate']").attr("checked", false).checkboxradio("refresh");

            hasVoted = false;

            return true;
        }
        else {
            //$.mobile.changePage("#pageLogin");
            viewLogin();

            return false;
        }
    }

    var logOffUser = function (f) {
        userName = "";

        $.ajax({
            type: "POST",
            url: "/Content/Parts/Common/FxService.asmx/LogOff",
            data: JSON.stringify({}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (f != null) {
                    f();
                }
                // Do nothing
            },
            error: function (request, status, error) {
                //$.mobile.hidePageLoadingMsg();

                showAlert("An error has occurred, please contact ADDCIT.", "Error", "#pageLogin");
            }
        });
    }

    // View Entries
    var viewEnties = function () {
        $('#logoimage').animate({ width: '245px', marginTop: '24px', marginLeft: '-20px' }, 'fast');
        $('#logo').animate({ height: '140px', width: '200px' }, 'fast');
        $('#entrylists').slideToggle('fast');
    }

    // View Results
    var viewResults = function () {
        $('#loginboard').slideToggle('fast');
        $('#logoimage').animate({ width: '245px', marginTop: '24px', marginLeft: '-20px' }, 'fast');
        $('#logo').animate({ height: '140px', width: '200px' }, 'fast');
        $('#viewresults').slideToggle('fast');
    }

    // View Vote Results
    var viewAfterVoteResults = function () {
        $('#darkcover').fadeOut('fast');
        $('#entrydetails').fadeOut('fast');
        $('#entrylists').slideToggle('fast');
        $('#viewresults').slideToggle('fast');
    }

    // View Info
    var viewInfo = function () {
        //$('#loginboard').slideToggle('fast');
        $('#signup').slideToggle('fast');
    }

    var viewLogin = function () {
        $('#logoimage').animate({ width: '260px', marginTop: '4px', marginLeft: '120px' }, 'fast');
        $('#logo').animate({ height: '500px', width: '500px' }, 'fast');
        $('#loginboard').slideToggle('fast');
    }

    // Page Intro
    var pageIntro = function PageIntro() {
        $('#logoimage').animate({
            marginTop: '4px',
            opacity: 1
        }, 1200, function () {
            $('#loginboard').animate({ opacity: 1 }, 600);

            if (userName != "") {
                $('#loginboard').slideToggle('fast');
                viewInfo();
            }
        });
    }

    var viewDetails = function () {
        //get the entry number of the .comentry clicked
        //temp = $(this).attr('data-entrynumber');

        var candidateId = $(this).data("entrynumber");
        var playCandidateId = $("#entrydetails").data("candidate");

        if (candidateId != playCandidateId) {
            //$.mobile.showPageLoadingMsg();

            $.ajax({
                type: "POST",
                url: "/Content/Parts/MusicCompetition/ASOP-WS.asmx/GetCandidate",
                data: JSON.stringify({
                    "id": candidateId
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //$.mobile.hidePageLoadingMsg();

                    if (data.d) {
                        $("#entrydetails").data("candidate", candidateId);

                        $("#playTitle").html(data.d.Entry);
                        $("#lyricsTitle").html(data.d.Entry);
                        $("#lyricsContent").html(data.d.Lyrics == "" ? "(No lyrics available)" : data.d.Lyrics);
                        $("#composer").html(data.d.Interpreter == "" ? data.d.Name : data.d.Name + ", " + data.d.Interpreter);
                        $("#photo").attr("src", data.d.PhotoFile == "" ? "/Content/Parts/MusicCompetition/v2-res/img/image.png" : "/Content/Parts/MusicCompetition/v2-res/img/entries/400px/" + data.d.PhotoFile);

                        if (data.d.SourceUrl != "") {
                            var sourceUrl = "<source src='" + data.d.SourceUrl + "'></source>";
                            if (data.d.SourceUrl2 != "") {
                                sourceUrl += "<source src='" + data.d.SourceUrl2 + "'></source>";
                            }

                            $("#audioControl").html("<audio controls='controls'>" + sourceUrl + "The audio format is not supported by your browser.</audio>");
                        }
                        else {
                            $("#audioControl").html("(No audio available)");
                        }

                        $('#darkcover').fadeIn('fast');
                        $('#entrydetails').fadeIn('fast');

                        //$.mobile.changePage("#pagePlay", { role: "dialog" });
                    }
                    else {
                        showAlert("An error has occurred, please contact ADDCIT.", "Error", "#pageVoting");
                    }
                },
                error: function (request, status, error) {
                    //$.mobile.hidePageLoadingMsg();

                    showAlert("An error has occurred, please contact ADDCIT.", "Error", "#pageVoting");
                }
            });
        }
        else {
            //$.mobile.changePage("#pagePlay", { role: "dialog" });

            $('#darkcover').fadeIn('fast');
            $('#entrydetails').fadeIn('fast');
        }
    }

    pageIntro();

    // Login Clicked
    $('#b1_login').click(function () {
        var username = $.trim($("#textUsername").val());
        var password = $.trim($("#textPassword").val());

        if (username == "" || password == "") {
            showAlert("Please enter your username or password.", "Username or Password", "#pageLogin");
            return false;
        }

        //$.mobile.showPageLoadingMsg();

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
                //$.mobile.hidePageLoadingMsg();

                if (data.d) {
                    userName = username;

                    isSvcAccount = data.d.IsServiceAccount;

                    if (!isSvcAccount) {
                        $("#textFirstName").val(data.d.FirstName);
                        $("#textLastName").val(data.d.LastName);
                        $("#textMobile").val(data.d.MobileNumber);
                        $("#textEmail").val(data.d.Email);
                    }

                    $("#textEmail").attr("readonly", !isSvcAccount);

                    resetLogin();

                    //$.mobile.changePage("#pageInfo");

                    $('#loginboard').slideToggle('fast');
                    viewInfo();
                }
                else {
                    showAlert("Invalid username or password.", "Invalid logon", "#pageLogin");
                }
            },
            error: function (request, status, error) {
                //$.mobile.hidePageLoadingMsg();

                showAlert("An error has occurred, please contact ADDCIT.", "Error", "#pageLogin");
            }
        });
    });

    /*
    $('#b2_results').click(function(){
        if (doneUrl != "") {
            location.href = doneUrl;
        }
        else {
            viewResults();
        }
    });*/
    //$('#b3_signup').click(viewInfo);

    $('#voteagain').click(function () {
        $('#viewresults').slideToggle('fast');

        viewLogin();
    });
    $('#votenow').click(function(){
        var errorMessage = "";
        var candidate = $("#entrydetails").data("candidate");

        /*
        var votingCode = $.trim($("#textVotingCode").val());
        if (votingCode == "") {
            errorMessage += "<li>Voting Code</li>";
        }
        */

        if (candidate == null || candidate == "") {
            errorMessage += "<li>Song</li>";
        }

        if (errorMessage != "") {
            showAlert("The following fields are requied:<ul>" + errorMessage + "</ul>", "Missing fields", "#pageVoting");
            return false;
        }

        if (userName == "" || !hasValidInfo) {
            showAlert("An error has occurred, please contact ADDCIT.", "Error", "#pageVoting");
            return false;
        }

        //$.mobile.showPageLoadingMsg();

        var firstName = $.trim($("#textFirstName").val());
        var lastName = $.trim($("#textLastName").val());
        var email = $.trim($("#textEmail").val());
        var mobile = $.trim($("#textMobile").val());

        $.ajax({
            type: "POST",
            url: "/Content/Parts/MusicCompetition/ASOP-WS.asmx/Vote",
            data: JSON.stringify({
                "vote": {
                    "Id": -1,
                    /*"Code": votingCode,*/
                    "FirstName": firstName,
                    "LastName": lastName,
                    "MobileNumber": mobile,
                    "Email": email,
                    "CandidateId": candidate,
                    "UserName": userName
                }
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //$.mobile.hidePageLoadingMsg();

                hasVoted = true;

                if (data.d == 0) {
                    // Voting successful!

                    logOffUser(function () {
                        if (doneUrl != "") {
                            location.href = doneUrl;
                        }
                        else {
                            viewAfterVoteResults();
                        }
                    });

                    /*
                    if (isSvcAccount) {
                        $.mobile.changePage("#pageDoneConfirm");
                    }
                    else {
                        $.mobile.changePage("#pageDone");
                    }*/
                    //location.href = doneUrl;

                    return true;
                }
                else {
                    switch (data.d) {
                        case 1:
                            showAlert("The email address you have entered has already been used for voting.", "Email already taken", "#pageVoting");
                            return false;

                        default:
                            showAlert("Unknown return code, please contact ADDCIT.", "Invalid response", "#pageVoting");
                            return false;
                    }
                }
            },
            error: function (request, status, error) {
                //$.mobile.hidePageLoadingMsg();

                showAlert("An error has occurred, please contact ADDCIT.", "Error", "#pageVoting");
            }
        });
    });

    $('#b_cancel').click(function () {
        logOffUser();

        $('#signup').slideToggle('fast');
        $('#loginboard').slideToggle('fast');
    });

    $('#b_signup').click(function () {
        var errorMessage = "";

        var firstName = $.trim($("#textFirstName").val());
        var lastName = $.trim($("#textLastName").val());
        var email = $.trim($("#textEmail").val());

        if (firstName == "") {
            errorMessage += "\n\r- First Name";
        }
        if (lastName == "") {
            errorMessage += "\n\r- Last Name";
        }
        if (email == "") {
            errorMessage += "\n\r- E-mail";
        }

        if (errorMessage != "") {
            showAlert("The following fields are requied: " + errorMessage, "Missing required fields", "#pageInfo");
            return false;
        }

        // Check email if not yet taken
        //$.mobile.showPageLoadingMsg();

        $.ajax({
            type: "POST",
            url: "/Content/Parts/MusicCompetition/ASOP-WS.asmx/IsEmailNotTaken",
            data: JSON.stringify({
                "email": email
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //$.mobile.hidePageLoadingMsg();

                if (data.d) {
                    // Email is available

                    hasValidInfo = true;

                    //$.mobile.changePage("#pageVoting");

                    $('#signup').slideToggle('fast');
                    viewEnties();

                    return true;
                }
                else {
                    showAlert("The email address you have entered has already been used for voting.", "Email already taken", "#pageInfo");
                    return false;
                }
            },
            error: function (request, status, error) {
                //$.mobile.hidePageLoadingMsg();
                showAlert("An error has occurred, please contact ADDCIT.", "Error", "#pageInfo");
            }
        });
    });

    $('.gbutton').hover(
        function () {
            $(this).removeClass('gradient1');
            $(this).addClass('gradient3');
        },
        function () {
            $(this).removeClass('gradient3');
            $(this).addClass('gradient1');
        }
    )

    $('.comentry').hover(
        function () {
            $(this).removeClass('gradient2');
            $(this).addClass('gradient4');
        },
        function () {
            $(this).removeClass('gradient4');
            $(this).addClass('gradient2');
        }
    )

    $('.comentry').click(viewDetails);

    $('#closethis').click(function () {
        $('#entrydetails').slideToggle('fast');
        $('#darkcover').fadeOut('fast');
    });

    $('#b_done, #done_buttons_confirm').click(function () {
        location.href = doneUrl;
    });

    $('#b2_results, #b_view_results, #b_view_results_confirm').click(function () {
        location.href = resultsUrl;
    });
});