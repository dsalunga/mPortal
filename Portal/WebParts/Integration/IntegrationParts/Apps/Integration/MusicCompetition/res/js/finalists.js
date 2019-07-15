
$(document).ready(function () {
    competitionId = parseInt($("#hCompetitionId").val());
    var isJudge = $(".finalist_section").data("judge") === 1;

    if (!votingEnded) {

        var toggleVoteOptions = function () { $("#vote-options").slideToggle('fast'); }
        var toggleLogin = function () { $('.login').slideToggle('fast'); }
        var toggleVoted = function () { $('#panel-voted').slideToggle('fast'); }
        var showVoted = function () {
            $("#vote-options").hide('fast');
            $('.login').hide('fast');
            $('.signup').hide('fast');
            $('#panel-voted').show('fast');
        }
        var toggleReview = function () { $('.signup').slideToggle('fast'); }
        var showAlert = function (message, title) { alert(message); }
        var hasInfo = function () { return email != '' && firstName != '' && lastName != '' && email != ''; }

        var resetLogin = function () {
            $("#textUsername").val("");
            $("#textPassword").val("");
        }

        var resetFlow = function (reVote) {
            resetLogin();

            if (hasVoted) {
                hasVoted = false;
                return true;
            }
            else {
                return false;
            }
        }

        var logOffUser = function (f) {
            userName = '';
            mobile = '';
            firstName = '';
            lastName = '';
            email = '';

            $('#portalStatus').html('Music Ministry Portal');

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
                    showAlert("[Log-off] An error has occurred, please contact the Portal Administrator.", "Error");
                }
            });
        }

        var loginUser = function () {
            var username = $.trim($("#textUsername").val());
            var password = $.trim($("#textPassword").val());
            if (username == '' || password == '') {
                showAlert('Please enter your username or password.', 'Username or Password');
                return false;
            }

            $.ajax({
                type: 'POST',
                url: voteOption == 0 ? '/Content/Parts/Common/FxService.asmx/Login' : '/Content/Parts/Integration/Member.asmx/OneLogin',
                data: JSON.stringify({
                    "userName": username,
                    "password": password,
                    "loginSession": true
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d) {
                        userName = username;

                        firstName = data.d.FirstName;
                        lastName = data.d.LastName;
                        mobile = data.d.Mobile;
                        email = data.d.Email;

                        validateInfo(function () {
                            if (voteOption != 2) {
                                resetLogin();
                            }

                            toggleLogin();
                            displayInfo();
                        });
                    }
                    else {
                        showAlert("Invalid username or password.", "Invalid logon");
                    }
                },
                error: function (request, status, error) {
                    showAlert("[Login] An error has occurred, please contact the Portal Administrator.", "Error");
                }
            });
        }

        var displayInfo = function () {
            $("#textFirstName").val(firstName);
            $("#textLastName").val(lastName);
            $("#textMobile").val(mobile);
            $("#textEmail").val(email);

            toggleReview();
        }

        var validateInfo = function (whenValidCallback) {
            if (!hasInfo()) {
                showAlert('You do not have a valid information for voting, please contact the Portal Administrator.', 'Error');
                return false;
            }

            var errorMessage = "";
            if (firstName == "") { errorMessage += "\n\r- First Name"; }
            if (lastName == "") { errorMessage += "\n\r- Last Name"; }
            if (email == "") { errorMessage += "\n\r- E-mail"; }

            if (errorMessage != "") {
                showAlert("The following fields are requied: " + errorMessage, "Missing required fields");
                return false;
            }

            var lEmail = email.toLowerCase();
            if (lEmail.indexOf("+") != -1 || lEmail.indexOf("@") == -1) {
                showAlert("Invalid email format.", "Invalid email");
                return false;
            }

            var lEmail = email.toLowerCase();
            if (lEmail.indexOf("+") != -1 || lEmail.indexOf("@") == -1
                || lEmail.indexOf("@yopmail.com") != -1
                || lEmail.indexOf("@auoie.com") != -1
                || lEmail.indexOf("@sofimail.com") != -1
                || lEmail.indexOf("@jetable.org") != -1
                || lEmail.indexOf("@mailinator.com") != -1
                || lEmail.indexOf("@guerrillamail.com") != -1) {
                showAlert("Invalid email format.", "Invalid email");
                return false;
            }

            $.ajax({
                type: 'POST',
                url: '/Content/Parts/MusicCompetition/ASOP-WS.asmx/IsEmailNotTaken',
                data: JSON.stringify({
                    'competitionId': competitionId,
                    'email': email
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    if (data.d == true && whenValidCallback != null) {
                        whenValidCallback();
                    }
                    else {
                        //showAlert("Your email address has already been used for voting. If you would like to vote again, please log-off and use a different account.", "Email already taken");
                        showVoted();
                    }
                },
                error: function (request, status, error) {
                    showAlert("[Validate] An error has occurred, please contact the Portal Administrator.", "Error");
                }
            });
        }

        var voteNow = function () {
            var errorMessage = "";
            var candidate = $("#entrydetails").data("candidate");

            if (candidate == null || candidate == "") {
                errorMessage += "<li>Song</li>";
            }

            if (errorMessage != "") {
                showAlert("The following fields are required:<ul>" + errorMessage + "</ul>", "Missing fields");
                return false;
            }

            var sendVoteRequest = function () {
                $.ajax({
                    type: "POST",
                    url: "/Content/Parts/MusicCompetition/ASOP-WS.asmx/Vote",
                    data: JSON.stringify({
                        "vote": {
                            "Id": -1,
                            "CompetitionId": competitionId,
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

                        hasVoted = true;

                        if (data.d == 0) {
                            // Voting successful!
                            $("#vote-options").hide('fast');
                            $('.thankyou').show('fast');
                            return true;
                        }
                        else {
                            switch (data.d) {
                                case 2:
                                    showAlert("The your email has already been used for voting.", "Email already taken");

                                    showVoted();

                                    return false;

                                case 3:
                                    showAlert("Sorry, voting is closed.", "Voting closed");
                                    return false;

                                default:
                                    showAlert("Unknown return code, please contact the Portal Administrator.", "Invalid response");
                                    return false;
                            }
                        }
                    },
                    error: function (request, status, error) {
                        showAlert("[Vote] An error has occurred, please contact the Portal Administrator.", "Error");
                    }
                });
            }

            if (hasInfo()) {
                validateInfo(function () {
                    sendVoteRequest();
                });
            } else if (userName != '') {
                $.ajax({
                    type: "POST",
                    url: "/Content/Parts/Common/FxService.asmx/Login",
                    data: JSON.stringify({
                        "userName": '',
                        "password": '',
                        "loginSession": true
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d && !data.d.IsServiceAccount) {
                            userName = username;
                            firstName = data.d.FirstName;
                            lastName = data.d.LastName;
                            mobile = data.d.Mobile;
                            email = data.d.Email;

                            validateInfo(function () {
                                sendVoteRequest();
                            });
                        }
                        else {
                            showAlert("Invalid login or session information.", "Invalid logon");
                        }
                    },
                    error: function (request, status, error) {
                        showAlert("[Login-fetch] An error has occurred, please contact the Portal Administrator.", "Error");
                    }
                });
            }
        }


        // Login button clicked
        $('#login').click(loginUser);

        // Login Form Cancel
        $('#login-cancel').click(function () {
            toggleLogin();
            toggleVoteOptions();
        });

        // vote now panel clicked
        $("#vote-entry").click(function () {
            $("#vote-entry").slideToggle('fast');
            $("#vote-options").slideToggle('fast');
        });

        // User submits the info form
        $("#submitInfo").click(function () {
            validateInfo(function () {
                toggleReview();
                voteNow();
            });
        });

        // Login Form Cancel
        $('#vote-cancel').click(function () {
            toggleReview();
            toggleVoteOptions();
        });

        $("#re-vote").click(function () {
            if (voteOption == 2) {
                FB.logout(function (response) {
                    // user is now logged out
                    toggleVoted();
                    toggleVoteOptions();
                });
            } else if (voteOption == 0) {
                logOffUser();
                resetFlow();

                toggleVoted();
                toggleVoteOptions();
            }

            return false;
        });

        $('.vote-option-portal').click(function () {
            $('#login-form-method').html('Music Ministry Portal');
            voteOption = 0;

            if (hasVoted) {
                $("#vote-options").hide('fast');
                $('#panel-voted').show('fast');
            } else {
                if (userName != '') {
                    voteNow();
                } else {
                    toggleVoteOptions();
                    toggleLogin();
                }
            }
        });

        $('.vote-option-one').click(function () {
            $('#login-form-method').html('Integration Ext');
            voteOption = 1;

            toggleVoteOptions();
            toggleLogin();
        });


        /* ------ FACEBOOK ----- */

        var fbConnected = function () {
            FB.api('/me', function (response) {
                //alert('Your email is ' + response.email);
                userName = '';
                firstName = response.first_name;
                lastName = response.last_name;
                mobile = ''; // response.mobile;
                email = response.email;

                //voteNow();
                validateInfo(function () {
                    $("#vote-options").hide('fast');
                    displayInfo();
                });
            });
        }

        var fbChange = function (response) {
            if (fbChecking) {
                fbChecking = false;
                if (response.status === 'connected') {
                    fbConnected();
                } else {
                    alert('Facebook authorization failed. Make sure to login and authorize the Integration Portal app in Facebook in order to vote.');
                }
            }
        }

        //window.fbAsyncInit = function () {
        var initFb = function () {
            FB.init({
                appId: '180244368803725', // App ID
                channelUrl: '/Content/fb-channel.aspx', // Channel File '//someorg.org.sg/Content/fb-channel.aspx'
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                xfbml: true  // parse XFBML
            });

            FB.Event.subscribe('auth.authResponseChange', fbChange);
        };

        initFb();

        $('.vote-option-fb').click(function () {
            voteOption = 2;

            FB.getLoginStatus(function (response) {
                if (response.status === 'connected') {
                    // fbChecking = true;
                    fbConnected();
                } else {
                    FB.login(function (response) {
                        fbChecking = true;
                        fbChange(response);
                    }, { scope: 'email' });
                }
            });
        });

        FB.getLoginStatus(function (response) {
            if (response.status === 'connected') {
                FB.api('/me', function (response) {
                    var statusText = $('#loggedInTemplate').html().replace('{0}', response.name).replace('{1}', 'https://www.facebook.com/' + response.username);
                    $('#fbStatus').html(statusText);
                });
            }
        });
    }


    /* ------ VIEW FINALIST DETAILS ----- */

    var displayDetails = function () {
        $('.finalist_entries').slideToggle('fast');
        $('.finalist_entry_section').slideToggle('fast');

        /* Voting-specific code  */
        $("#vote-entry").slideToggle('fast');
        /*
        if (!votingEnded) {
            if (hasVoted) {
                $('#panel-voted').slideToggle('fast');
            } else {
                $("#vote-entry").slideToggle('fast');
            }
        }*/
    }

    var _populateDetails = function (source, cid) {
        var candidateId = cid > 0 ? cid : parseInt($(source).data("entrynumber"));
        var playCandidateId = parseInt($("#entrydetails").data("candidate"));

        if (candidateId != playCandidateId) {
            $.ajax({
                type: "POST",
                url: "/Content/Parts/MusicCompetition/ASOP-WS.asmx/GetCandidate",
                data: JSON.stringify({
                    "id": candidateId
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d) {
                        var composerInterpreter = data.d.Interpreter == "" ? data.d.Name : data.d.Name + ", " + data.d.Interpreter;

                        $("#entrydetails").data("candidate", candidateId);
                        $("#playTitle").html(data.d.Entry);
                        //$("#lyricsTitle").html(data.d.Entry);
                        $("#lyricsContent").html(data.d.Lyrics == "" ? "(No lyrics available)" : data.d.Lyrics);
                        $("#composer").html(data.d.Name);
                        $("#interpreter").html(data.d.Interpreter);
                        $("#photo").attr("src", $("#hResourcePath").val() + "Photos/" + (data.d.PhotoFile == "" ? defaultPhoto : data.d.PhotoFile));
                        $("#photo").attr("alt", composerInterpreter);
                        $("#photo").attr("title", composerInterpreter);

                        if (data.d.SourceUrl != "") {
                            var sourceUrl = "<source src='" + ($("#hResourcePath").val() + "Music/" + (isJudge ? "full/" : "") + data.d.SourceUrl) + "'></source>";
                            if (data.d.SourceUrl2 != "") {
                                sourceUrl += "<source src='" + data.d.SourceUrl2 + "'></source>";
                            }

                            $("#audioControl").html("<audio controls='controls'>" + sourceUrl + "The audio format is not supported by your browser.</audio>");
                        }
                        else {
                            $("#audioControl").html("(No audio available)");
                        }

                        displayDetails();
                    }
                    else {
                        showAlert("[Entry-details: No-data] An error has occurred, please contact the Portal Administrator.", "Error");
                    }
                },
                error: function (request, status, error) {
                    showAlert("[Entry-details] An error has occurred, please contact the Portal Administrator.", "Error");
                }
            });
        }
        else {
            displayDetails();
        }
    }
    var populateDetails = function () { _populateDetails(this, -1); }

    $("#close_entry").click(function () {
        /* Voting-specific code  */
        if (!votingEnded) {
            $('.login').hide('fast');
            $('.signup').hide('fast');
            $('.thankyou').hide('fast');
            $('#panel-voted').hide('fast');
            $('#vote-options').hide('fast');
        }

        $("#vote-entry").hide('fast');
        $('.finalist_entries').slideToggle('fast');
        $('.finalist_entry_section').slideToggle('fast');
    });

    // Finalist photo clicked
    $(".finalist_pics").click(populateDetails);

    var c = parseInt($("#hCandidateId").val());
    if (c > 0) {
        _populateDetails(null, c);
    }
});