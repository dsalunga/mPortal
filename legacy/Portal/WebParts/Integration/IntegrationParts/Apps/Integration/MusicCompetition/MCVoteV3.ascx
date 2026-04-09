<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MCVoteV3.ascx.cs" Inherits="WCMS.WebSystem.Apps.MusicCompetition.MCVoteV2" %>

<script runat="server">
    public string EvalPhoto(string photoFile)
    {
        return string.IsNullOrEmpty(photoFile) ? "image.png" : string.Format("245px/{0}", photoFile);
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width = 1024" />

    <title><%= EventTitle %></title>

    <!--[if gte IE 9]>
      <style type="text/css">
        .gradient {
           filter: none;
        }
      </style>
    <![endif]-->

    <link href="/Content/Parts/MusicCompetition/v3-res/css/vote.css" type="text/css" rel="stylesheet" />
    <script src="/Content/Assets/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/Content/Parts/MusicCompetition/v3-res/js/vote.js" type="text/javascript"></script>
    <script type="text/javascript">
        userName = "<%= UserName %>";
        doneUrl = "<%= DoneUrl %>";
        resultsUrl = "<%= ResultsUrl %>";
    </script>

    <style type="text/css">
        body {
            background: #003427 url(/Content/Parts/MusicCompetition/v3-res/img-vote/bg_header.jpg) no-repeat;
            background-size: 100%, 100%;
            height: 100%;
            width: 1024px;
            margin: auto;
            padding: inherit;
            font-family: Arial, Verdana, Helvetica, sans-serif;
            /*font-weight: 700;*/
            color: white;
            font-size: 14px;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Parts/MusicCompetition/v3-res/img-vote/bg_header.jpg', sizingMethod='scale');
            -ms-filter: "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Parts/MusicCompetition/v3-res/img-vote/bg_header.jpg', sizingMethod='scale')";
        }
    </style>
</head>
<body>
    <div id="container">
        <div id="logo" style="height: 140px">
            <img id="logoimage" src="/Content/Parts/MusicCompetition/v3-res/img-vote/asoplogo.png" alt="" width="260" />
        </div>
        <%--<img id="logoimages2" src="/Content/Parts/MusicCompetition/v3-res/img-vote/entries.png" alt="" width="500" />--%>

        <!-- Login -->
        <div id="loginboard" class="gradient loginpad roundcorner1 border1 shadow1">
            <h1 class="shadow2 roundcorner2"><span class="textshadow1">Please login</span></h1>
            <p>
                For Singapore users, please log in with your existing Integration SG Portal account.<br />
                <br />
                For users from other countries, you may use this public access:<br />
                Username: MCVoter<br />
                Password: asop123
            </p>
            <div>
                <label for="textUsername">Username:</label>
                <input name="textUsername" id="textUsername" placeholder="Username" value="" />
            </div>
            <div>
                <label for="textPassword">Password:</label>
                <input type="password" name="textPassword" id="textPassword" placeholder="Password" value="" />
            </div>
            <div id="logbuttons" style="margin-left: 55px">
                <div id="b1_login" class="gbutton loginpad roundcorner1 border1 shadow2">
                    <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>Login</span>
                </div>
                <div id="b2_results" class="gbutton loginpad roundcorner1 border1 shadow2">
                    <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>View Results</span>
                </div>
                <%--<div id="b3_signup" class="gbutton loginpad roundcorner1 border1 shadow2">
                    <img src="/Content/Parts/MusicCompetition/v2-res/img/note.png" alt="" height="25" /><span>SignUp</span>
                </div>--%>
            </div>
            <br />
            <br />
            <br />
            <%--<p>
                Email verification will be required for non-Integration SG portal users. Kindly check your
                email after voting to validate your votes.
            </p>--%>
            <%= EventInfoContent %>
            <%--<p>Voting Ends by September 23, 2012 - 9pm.</p>
            <p>
                If you experience any technical issue with this online voting system please contact
                +65-9000-1773 or asop-support@example.test.
            </p>--%>
        </div>

        <!-- Info -->
        <div id="signup" class="gradient loginpad roundcorner1 border1 shadow1">
            <h1 class="shadow2 roundcorner2"><span class="textshadow1">Enter your information</span></h1>
            <p>
                Please enter your valid information and click the Next button to vote.
            </p>
            <div>
                <label for="textFirstName">First Name:</label>
                <input type="text" name="textFirstName" id="textFirstName" runat="server" clientidmode="Static" placeholder="First Name" />
            </div>
            <div>
                <label for="textLastName">Last Name:</label>
                <input type="text" name="textLastName" id="textLastName" runat="server" clientidmode="Static" placeholder="Last Name" />
            </div>
            <div>
                <label for="textEmail">Email:</label>
                <input type="text" name="textEmail" id="textEmail" placeholder="E-mail" value="<%= Email %>" />
            </div>
            <%--<div>
                <label for="textPassword">Password:</label>
                <input type="password" name="textPassword" id="Password1" />
            </div>--%>
            <div>
                <label for="textMobile">Mobile:</label>
                <input type="text" name="textMobile" id="textMobile" placeholder="Mobile Number" value="<%= Mobile %>" />
            </div>
            <div id="signup_buttons">
                <div id="b_signup" class="gbutton loginpad roundcorner1 border1 shadow2">
                    <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>Next</span>
                </div>
                <div id="b_cancel" class="gbutton loginpad roundcorner1 border1 shadow2">
                    <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>Cancel</span>
                </div>
            </div>
            <p>
                An e-mail verification is required for non-Integration SG portal users. For users from other countries, kindly click the confirmation link in your email to consider your vote official.
            </p>
        </div>

        <!-- Entries -->
        <div id="entrylists" class="gradient loginpad roundcorner1 border1 shadow1">
            <h1 class="shadow2 roundcorner2"><span class="textshadow1">Make your choice. Vote now!</span></h1>
            <p>
                Please choose the song then click Vote Now to vote. You can also listen to the song and view the lyrics.
            </p>
            <div id="entries_images">
                <asp:HiddenField ID="hCompetitionId" runat="server" Value="-1" />
                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1">
                    <ItemTemplate>
                        <div class="comentry gradient2 roundcorner2 border1 shadow2" data-entrynumber='<%# Eval("Id") %>'>
                            <img class="roundcorner2" width="245" src="/Content/Parts/Integration/MusicMinistry/ASOP/Entries/<%# EvalPhoto(Eval("PhotoFile").ToString()) %>" title="SONG:&nbsp;<%# Eval("Entry") %>, COMPOSER:&nbsp;<%# Eval("Name") %>, INTERPRETER:&nbsp;<%# Eval("Interpreter") %>" alt="" />
                            <%--<p>
                                COMPOSER:&nbsp;<%# Eval("Name") %>, INTERPRETER:&nbsp;<%# Eval("Interpreter") %><br />
                                <%# Eval("Entry") %>
                            </p>--%>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetCandidates"
                    TypeName="WCMS.WebSystem.Apps.MusicCompetition.MCVoteV2">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hCompetitionId" DefaultValue="-1" Name="competitionId" PropertyName="Value" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <%--<p>
                <em>NOTE:</em>&nbsp;Audio playback is supported only on IE9+, Chrome 6+, Safari 5+/Safari
                for iPhone, FireFox 5+, Andriod Browser (Gingerbread+) and Opera 11+.
            </p>--%>
        </div>

        <div id="darkcover"></div>

        <!-- Entry Details -->
        <div id="entrydetails" class="loginpad roundcorner1 border1 shadow1" data-candidate="">
            <div id="votenow" class="gbutton loginpad roundcorner1 border1 shadow2">
                <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>Vote This</span>
            </div>
            <div id="closethis" class="gbutton loginpad roundcorner1 border1 shadow2">
                <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>Go Back</span>
            </div>
            <h1 class="shadow2 roundcorner2"><span class="textshadow1" id="playTitle">Song Title Here</span></h1>
            <img class="detailimage" id="photo" src="/Content/Parts/MusicCompetition/v3-res/img-vote/image.png" alt="" width="400" />
            <h4 id="composer">Composer and Interpreter Here</h4>
            <p id="audioControl">
            </p>
            <%--<h4>Streaming Song Here</h4>--%>
            <h4 id="lyricsTitle">Lyrics</h4>
            <div id="lyricsContent">
            </div>
            <br />
            <p style="font-size: smaller">
                <em>NOTE:</em>&nbsp;Audio playback is supported only on IE9+, Chrome 6+, Safari 5+/Safari
                for iPhone, FireFox 5+, Andriod Browser (Gingerbread+) and Opera 11+.
            </p>
        </div>

        <div id="confirmation" class="gradient loginpad roundcorner1 border1 shadow1">
            <h1 class="shadow2 roundcorner2"><span class="textshadow1">Please Confirm Your Vote</span></h1>
            <p>
                Your vote needs confirmation to be processed successfully. A confirmation link has been sent to your email address. Thank you!
            </p>
            <div id="exitvote" class="gbutton loginpad roundcorner1 border1 shadow2">
                <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>Back</span>
            </div>
        </div>

        <!-- After Vote Info -->
        <div id="afterVoteInfo" class="gradient loginpad roundcorner1 border1 shadow1">
            <h1 class="shadow2 roundcorner2"><span class="textshadow1">Thank you! Voting Successful.</span></h1>
            <p>
                Your vote has been recorded. You will receive an email to confirm that your vote
                was successful.
            </p>
            <div id="done_buttons">
                <div id="b_done" class="gbutton loginpad roundcorner1 border1 shadow2 exitvote">
                    <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>Vote Again</span>
                </div>
                <div id="b_view_results" class="gbutton loginpad roundcorner1 border1 shadow2 exitvote">
                    <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>View Results</span>
                </div>
            </div>
            <br />
            <br />
            <br />
            <p>
                <em>NOTE:</em>Please note that this voting system will not affect any scores during
                the finals night. The decision of the board of judges is final.
            </p>
        </div>

        <!-- After Vote Info -->
        <div id="afterVoteInfoConfirm" class="gradient loginpad roundcorner1 border1 shadow1">
            <h1 class="shadow2 roundcorner2"><span class="textshadow1">Thank You for Voting!
                <br />
                WAIT!</span></h1>
            <p>
                Your vote has been recorded, you need to verify your email in order for your vote
                to be counted.
            </p>
            <p>
                Please open your email and click the verification link to validate your vote. Check your (spam folder or bulk folder) should you not see the confirmation email in your inbox.
                <br />
                <br />
                Your vote will ONLY BE CONSIDERED OFFICIAL once you confirm.
            </p>
            <div id="done_buttons_confirm">
                <div id="b_done_confirm" class="gbutton loginpad roundcorner1 border1 shadow2 exitvote">
                    <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>Vote Again</span>
                </div>
                <div id="b_view_results_confirm" class="gbutton loginpad roundcorner1 border1 shadow2 exitvote">
                    <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>View Results</span>
                </div>
            </div>
            <br />
            <br />
            <br />
            <p>
                <em>NOTE:</em>&nbsp;Please note that this voting system will not affect any scores
                during the finals night. The decision of the board of judges is final.
            </p>
        </div>

        <!-- Results -->
        <div id="viewresults" class="gradient loginpad roundcorner1 border1 shadow1">
            <h1 class="shadow2 roundcorner2"><span class="textshadow1">ASOP SG People's Choice Results</span></h1>
            <div id="categories">
                <div id="song_entry" class="category">
                    Song
                    <div class="entrylist">
                        <div>Song Title Here</div>
                        <div>Song Title Here</div>
                        <div>Song Title Here</div>
                        <div>Song Title Here</div>
                    </div>
                </div>
                <div id="composer_entry" class="category">
                    Composer
                    <div class="entrylist">
                        <div>Composer Name Here</div>
                        <div>Composer Name Here</div>
                        <div>Composer Name Here</div>
                        <div>Composer Name Here</div>
                    </div>
                </div>
                <div id="totalvotes_entry" class="category">
                    Total Votes
                    <div class="entrylist">
                        <div>Total Votes Here</div>
                        <div>Total Votes Here</div>
                        <div>Total Votes Here</div>
                        <div>Total Votes Here</div>
                    </div>
                </div>
                <div id="officialvotes_entry" class="category">
                    Official Votes
                    <div class="entrylist">
                        <div>OV Here</div>
                        <div>OV Here</div>
                        <div>OV Here</div>
                        <div>OV Here</div>
                    </div>
                </div>
            </div>
            <div id="voteagain" class="gbutton loginpad roundcorner1 border1 shadow2">
                <img src="/Content/Parts/MusicCompetition/v3-res/img-vote/note.png" alt="" height="25" /><span>Back</span>
            </div>
        </div>
    </div>

</body>
</html>
