(function ($) {
    var hasVoted = false;
    var hasValidInfo = false;
    var userName = "";
    var doneUrl = "";
    var isSvcAccount = false;

    $.mobile.defaultPageTransition = "slide";

    $("#pageLogin").live("pagebeforeload", function (event) {
        if (userName != "") {
            $.mobile.changePage("#pageInfo");
        }
    });

    $('#pageLogin').live('pageinit', function (event) {
        if (userName != "") {
            $.mobile.changePage("#pageInfo");
        }

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

                        isSvcAccount = data.d.IsServiceAccount;

                        if (!isSvcAccount) {
                            $("#textFirstName").val(data.d.FirstName);
                            $("#textLastName").val(data.d.LastName);
                            $("#textMobile").val(data.d.MobileNumber);
                            $("#textEmail").val(data.d.Email);
                        }

                        $("#textEmail").attr("readonly", !isSvcAccount);

                        resetLogin();

                        $.mobile.changePage("#pageInfo");
                    }
                    else {
                        showAlert("Invalid username or password.", "Invalid logon", "#pageLogin");
                    }
                },
                error: function (request, status, error) {
                    $.mobile.hidePageLoadingMsg();

                    showAlert("An error has accurred, please contact ADDCIT.", "Error", "#pageLogin");
                }
            });
        });
    });

    $("#pageInfo").live("pageinit", function (event) {
        $("#buttonInfoCancel").click(function () {
            logOffUser();
            resetFlow(false);

            $.mobile.changePage("#pageLogin");
        });

        $("#buttonInfoNext").click(function () {
            var errorMessage = "";

            var firstName = $.trim($("#textFirstName").val());
            var lastName = $.trim($("#textLastName").val());
            var email = $.trim($("#textEmail").val());

            if (firstName == "") {
                errorMessage += "<li>First Name</li>";
            }
            if (lastName == "") {
                errorMessage += "<li>Last Name</li>";
            }
            if (email == "") {
                errorMessage += "<li>E-mail</li>";
            }

            if (errorMessage != "") {
                showAlert("The following fields are requied:<ul>" + errorMessage + "</ul>", "Missing required fields", "#pageInfo");
                return false;
            }

            // Check email if not yet taken
            $.mobile.showPageLoadingMsg();

            $.ajax({
                type: "POST",
                url: "/Content/Parts/MusicCompetition/ASOP-WS.asmx/IsEmailNotTaken",
                data: JSON.stringify({
                    "email": email
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $.mobile.hidePageLoadingMsg();

                    if (data.d) {
                        // Email is available

                        hasValidInfo = true;

                        $.mobile.changePage("#pageVoting");

                        return true;
                    }
                    else {
                        showAlert("The email address you have entered has already been used for voting.", "Email already taken", "#pageInfo");
                        return false;
                    }
                },
                error: function (request, status, error) {
                    $.mobile.hidePageLoadingMsg();

                    showAlert("An error has accurred, please contact ADDCIT.", "Error", "#pageInfo");
                }
            });
        });

        if (userName == "") {
            resetFlow(false);

            $.mobile.changePage("#pageLogin");
            return false;
        }
    });

    $("#pageVoting").live("pageinit", function (event) {
        $("#buttonVote").click(function () {
            var errorMessage = "";
            var candidate = $("input[name='radio-candidate']:checked").val();

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
                showAlert("An error has accurred, please contact ADDCIT.", "Error", "#pageVoting");
                return false;
            }

            $.mobile.showPageLoadingMsg();

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
                    $.mobile.hidePageLoadingMsg();

                    hasVoted = true;

                    if (data.d == 0) {
                        // Voting successful!

                        logOffUser();

                        if (isSvcAccount) {
                            $.mobile.changePage("#pageDoneConfirm");
                        }
                        else {
                            $.mobile.changePage("#pageDone");
                        }
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
                    $.mobile.hidePageLoadingMsg();

                    showAlert("An error has accurred, please contact ADDCIT.", "Error", "#pageVoting");
                }
            });
        });

        $("#buttonVoteCancel").click(function () {
            logOffUser();
            resetFlow(false);

            $.mobile.changePage("#pageLogin");
        });

        $(".play-button").click(function () {
            var candidateId = $(this).data("candidate");
            var playCandidateId = $("#pagePlay").data("candidate");

            if (candidateId != playCandidateId) {
                $.mobile.showPageLoadingMsg();

                $.ajax({
                    type: "POST",
                    url: "/Content/Parts/MusicCompetition/ASOP-WS.asmx/GetCandidate",
                    data: JSON.stringify({
                        "id": candidateId
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $.mobile.hidePageLoadingMsg();

                        if (data.d) {
                            $("#pagePlay").data("candidate", candidateId);

                            $("#playTitle").html(data.d.Entry);
                            $("#lyricsTitle").html(data.d.Entry);
                            $("#lyricsContent").html(data.d.Lyrics == "" ? "(No lyrics available)" : data.d.Lyrics);

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

                            $.mobile.changePage("#pagePlay", { role: "dialog" });
                        }
                        else {
                            showAlert("An error has accurred, please contact ADDCIT.", "Error", "#pageVoting");
                        }
                    },
                    error: function (request, status, error) {
                        $.mobile.hidePageLoadingMsg();

                        showAlert("An error has accurred, please contact ADDCIT.", "Error", "#pageVoting");
                    }
                });
            }
            else {
                $.mobile.changePage("#pagePlay", { role: "dialog" });
            }
        });

        if (!hasValidInfo) {
            //resetFlow(false);

            if (userName != "") {
                $.mobile.changePage("#pageInfo");
            }
            else {
                $.mobile.changePage("#pageLogin");
            }
            return false;
        }
    });

    $("#pageDone").live("pageinit", function (event) {
        if (!hasVoted) {
            $.mobile.changePage("#pageLogin");
        }

        var doneFunc = function () {
            if (resetFlow(false)) {
                logOffUser();
                $.mobile.changePage("#pageLogin");
            }
        };

        $("#buttonDone").click(doneFunc);

        /*
        $("#buttonVoteAgain").click(function () {
            if (userName != "" && resetFlow(true)) {
                $.mobile.changePage("#pageVoting");
            }
        });
        */
    });

    $("#pageDoneConfirm").live("pageinit", function (event) {
        if (!hasVoted) {
            $.mobile.changePage("#pageLogin");
        }

        var doneFunc = function () {
            if (resetFlow(false)) {
                logOffUser();
                $.mobile.changePage("#pageLogin");
            }
        };

        $("#buttonDoneConfirm").click(doneFunc);

        /*
        $("#buttonVoteAgain").click(function () {
            if (userName != "" && resetFlow(true)) {
                $.mobile.changePage("#pageVoting");
            }
        });
        */
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

    function logOffUser() {
        userName = "";

        $.ajax({
            type: "POST",
            url: "/Content/Parts/Common/FxService.asmx/LogOff",
            data: JSON.stringify({}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                // Do nothing
            },
            error: function (request, status, error) {
                $.mobile.hidePageLoadingMsg();

                showAlert("An error has accurred, please contact ADDCIT.", "Error", "#pageLogin");
            }
        });
    }

    function showAlert(message, title, caller) {
        $("#alertTitle").html(title);
        $("#alertMessage").html(message);
        $("#alertOK").attr("href", caller);

        $.mobile.changePage("#pageAlert", { role: "dialog" });
    }
})(jQuery);