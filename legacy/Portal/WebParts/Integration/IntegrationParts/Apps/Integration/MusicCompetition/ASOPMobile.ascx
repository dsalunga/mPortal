<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ASOPMobile.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.MusicCompetition.ASOPMobile" %>
<!DOCTYPE html>
<html>
<head>
    <title><%= EventTitle %></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.1.0/jquery.mobile-1.1.0.min.css" />
    <link rel="stylesheet" href="/Content/Parts/MusicCompetition/res/css/style.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/mobile/1.1.0/jquery.mobile-1.1.0.min.js"></script>
    <script type="text/javascript" src="/Content/Parts/MusicCompetition/res/js/script.js"></script>
    <script type="text/javascript">
        userName = "<%= UserName %>";
        doneUrl = "<%= DoneUrl %>";
    </script>
</head>
<body>
    <div data-role="page" id="pageLogin">
        <div data-role="header" data-theme="e">
            <h1><%= ShortEventTitle %></h1>
        </div>
        <div data-role="content">
            <%--<img src="/Content/Assets/Uploads/Image/ASOP/new-logo2.png" />--%>
            <img src="/Content/Assets/Uploads/Image/ASOP/asopoval.jpg" />
            <h1>Please login</h1>
            <p>
                To login, you can use your existing Integration SG Portal account or you can use this public
                access: Username: MCVoter Password: asop123
            </p>
            <div data-role="fieldcontain">
                <label for="textUsername">
                    Username:
                </label>
                <input type="text" name="textUsername" placeholder="Username" id="textUsername" value="" />
            </div>
            <div data-role="fieldcontain">
                <label for="textPassword">
                    Password:
                </label>
                <input type="password" name="textPassword" id="textPassword" placeholder="Password"
                    value="" />
            </div>
            <p>
                <a href="#" id="buttonLogin" data-role="button" data-theme="b" data-inline="true"
                    data-icon="arrow-r" data-iconpos="right">Login</a><a href="/Vote-Results/" data-ajax="false" data-role="button" data-inline="true"
                        data-icon="star" data-iconpos="right" data-theme="b">View Results</a>
            </p>
            <p>
                Email verification will be required for non-Integration SG portal users. Kindly check your
                email after voting to validate your votes.
            </p>
            <p>Voting Ends by September 23, 2012 - 9pm.</p>
            <p>
                If you experience any technical issue with this online voting system please contact
                +65-9000-1773 or asop-support@example.test.
            </p>
        </div>
        <div data-role="footer">
            <h4><%= ShortEventTitle %></h4>
        </div>
    </div>
    <div data-role="page" id="pageInfo">
        <div data-role="header" data-theme="e">
            <h1><%= ShortEventTitle %></h1>
        </div>
        <div data-role="content">
            <h1>Enter your information</h1>
            <p>
                Please enter your your information and click the next button
            </p>
            <div data-role="fieldcontain">
                <label for="textFirstName">
                    First Name:
                </label>
                <input type="text" name="textFirstName" id="textFirstName" placeholder="First Name"
                    value="" runat="server" clientidmode="Static" />
            </div>
            <div data-role="fieldcontain">
                <label for="textLastName">
                    Last Name:
                </label>
                <input type="text" name="textLastName" id="textLastName" placeholder="Last Name"
                    value="" runat="server" clientidmode="Static" />
            </div>
            <div data-role="fieldcontain">
                <label for="textEmail">
                    E-mail:
                </label>
                <input type="email" name="textEmail" id="textEmail" placeholder="E-mail" value="<%= Email %>" />
            </div>
            <div data-role="fieldcontain">
                <label for="textMobile">
                    Mobile:
                </label>
                <input type="tel" name="textMobile" id="textMobile" placeholder="Mobile Number" value="<%= Mobile %>" />
            </div>
            <p>
                <a href="#" id="buttonInfoNext" data-role="button" data-icon="arrow-r" data-iconpos="right"
                    data-theme="b" data-inline="true">Next</a><a href="#" id="buttonInfoCancel" data-role="button"
                        data-theme="a" data-inline="true" data-icon="delete" data-iconpos="right">Cancel
                    </a>
            </p>
            <p>
                Email verification will be required for non-Integration SG portal users. Kindly check your
                email after voting to validate your votes.
            </p>
        </div>
        <div data-role="footer">
            <h4><%= ShortEventTitle %></h4>
        </div>
    </div>
    <div data-role="page" id="pageVoting">
        <div data-role="header" data-theme="e">
            <h1><%= ShortEventTitle %></h1>
        </div>
        <div data-role="content">
            <h1>Vote now</h1>
            <p>
                Please choose the song then click vote to proceed. You can also toggle the "play"
                button to listen and view the lyrics.
            </p>
            <%--
            <div data-role="fieldcontain">
                <label for="textVotingCode">
                    Voting Code:</label>
                <input type="text" name="textVotingCode" id="textVotingCode" placeholder="Voting Code"
                    value="" />
            </div>
            --%>
            <div data-role="fieldcontain">
                <asp:HiddenField ID="hCompetitionId" runat="server" Value="-1" />
                <fieldset data-role="controlgroup" id="selectCandidate">
                    <legend>Choose a Song:</legend>
                    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1">
                        <ItemTemplate>
                            <div class="ui-grid-a">
                                <div class="ui-block-a" style="width: 85%">
                                    <input type="radio" name="radio-candidate" id='radio-choice-<%# Eval("Id") %>' value='<%# Eval("Id") %>' />
                                    <label id="label<%# Eval("Id") %>" for='radio-choice-<%# Eval("Id") %>'>
                                        <%# Eval("Entry") %>,&nbsp;<%# Eval("Name") %>
                                    </label>
                                </div>
                                <div class="ui-block-b" style="width: 30px">
                                    <a href="#" class="play-button" data-candidate='<%# Eval("Id") %>'>
                                        <img alt="" style="height: auto; padding-top: 7px; padding-left: 6px" src="/Content/Assets/Images/symbols/36-circle-play.png" />
                                    </a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetCandidates"
                        TypeName="WCMS.WebSystem.Apps.MusicCompetition.ASOPMobile">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hCompetitionId" DefaultValue="-1" Name="competitionId" PropertyName="Value" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </fieldset>
            </div>
            <p>
                <a href="#" id="buttonVote" data-role="button" data-theme="b" data-inline="true"
                    data-icon="check" data-iconpos="right">Vote!</a><a href="#" id="buttonVoteCancel"
                        data-role="button" data-theme="a" data-inline="true" data-icon="delete" data-iconpos="right">
                        Cancel</a>
            </p>
            <p>
                <em>NOTE:</em>Audio playback is supported only on IE9+, Chrome 6+, Safari 5+/Safari
                for iPhone, FireFox 5+, Andriod Browser (Gingerbread+) and Opera 11+.
            </p>
        </div>
        <div data-role="footer">
            <h4><%= ShortEventTitle %></h4>
        </div>
    </div>
    <div data-role="page" id="pageDone">
        <div data-role="header" data-theme="e">
            <h1>Voting Successful</h1>
        </div>
        <div data-role="content">
            <h1>Thank you!</h1>
            <p>
                Your vote has been recorded. You will receive an email to confirm that your vote
                was successful.
            </p>
            <p>
                <a href="#" id="buttonDone" data-role="button" data-inline="true" data-icon="home"
                    data-iconpos="right" data-theme="b">Done</a><a href="/Vote-Results/" data-ajax="false" data-role="button" data-inline="true"
                        data-icon="star" data-iconpos="right" data-theme="b">View Results</a>
                <%--<a href="#" id="buttonVoteAgain" data-role="button"
                        data-inline="true" data-icon="refresh" data-iconpos="right" data-theme="b">Vote
                        again!</a>--%>
            </p>
            <p>
                <em>NOTE:</em>Please note that this voting system will not affect any scores during
                the finals night. The decision of the board of judges is final.
            </p>
        </div>
        <div data-role="footer">
            <h4><%= ShortEventTitle %></h4>
        </div>
    </div>
    <div data-role="page" id="pageDoneConfirm">
        <div data-role="header" data-theme="e">
            <h1>Voting Successful, Pending Confirmation</h1>
        </div>
        <div data-role="content">
            <h1>Thank you!</h1>
            <p>
                Your vote has been recorded, you need to verify your email in order for your vote
                to be counted.
            </p>
            <p>
                You will receive an email to confirm your vote and verify your email. Please check
                your email inbox and confirm your vote by clicking the hyperlink included in the
                email.
            </p>
            <p>
                <a href="#" id="buttonDoneConfirm" data-role="button" data-inline="true" data-icon="home"
                    data-iconpos="right" data-theme="b">Done</a><a href="/Vote-Results/" data-ajax="false" data-role="button" data-inline="true"
                        data-icon="star" data-iconpos="right" data-theme="b">View Results</a>
            </p>
            <p>
                <em>NOTE:</em>&nbsp;Please note that this voting system will not affect any scores
                during the finals night. The decision of the board of judges is final.
            </p>
        </div>
        <div data-role="footer">
            <h4><%= ShortEventTitle %></h4>
        </div>
    </div>
    <div data-role="page" id="pageAlert">
        <div data-role="header" data-theme="e">
            <h1 id="alertTitle">Alert Title</h1>
        </div>
        <div data-role="content">
            <h3 id="alertMessage">Alert Message</h3>
            <p>
                <a id="alertOK" href="#" data-role="button" data-inline="true">OK</a>
            </p>
        </div>
        <div data-role="footer">
            <h4></h4>
        </div>
    </div>
    <div data-role="page" id="pagePlay" data-candidate="">
        <div data-role="header" data-theme="e">
            <h1 id="playTitle"></h1>
        </div>
        <div data-role="content">
            <p id="audioControl">
            </p>
            <h3 id="lyricsTitle"></h3>
            <div id="lyricsContent">
            </div>
            <p>
                <a id="playOK" href="#pageVoting" data-role="button" data-inline="true">OK</a>
            </p>
        </div>
        <div data-role="footer">
            <h4></h4>
        </div>
    </div>
</body>
</html>
