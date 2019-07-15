<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MCJudges.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.MusicCompetition.MCJudges" %>
<!DOCTYPE html>
<html>
<head>
    <title>ASOP Music Festival SG-Mid-Year 2012 ASOP Judges Scoring</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.1.0/jquery.mobile-1.1.0.min.css" />
    <link rel="stylesheet" href="/Content/Parts/MusicCompetition/res/css/style.css" />
    <script src="http://code.jquery.com/jquery-1.7.1.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.1.0/jquery.mobile-1.1.0.min.js"></script>
    <script src="/Content/Parts/MusicCompetition/res/js/judges.js"></script>
</head>
<body>
    <div data-role="page" id="pageLogin">
        <div data-role="header" data-theme="e">
            <h1>
                ASOP Judges Scoring</h1>
        </div>
        <div data-role="content">
            <img src="/Content/Assets/Uploads/Image/ASOP/new-logo2.png" />
            <h1>
                Judge Login</h1>
            <p>
                To login, you can use your existing Integration SG Portal account.</p>
            <div data-role="fieldcontain">
                <label for="textUsername">
                    Username:</label>
                <input type="text" name="textUsername" placeholder="Username" id="textUsername" value="" />
            </div>
            <div data-role="fieldcontain">
                <label for="textPassword">
                    Password:</label>
                <input type="password" name="textPassword" id="textPassword" placeholder="Password"
                    value="" />
            </div>
            <p>
                <a href="#" id="buttonLogin" data-role="button" data-theme="b" data-inline="true"
                    data-icon="arrow-r" data-iconpos="right">Login</a></p>
            <p>
                If you experience any technical issue with this ASOP online system please contact
                97771773 for assistance.</p>
        </div>
        <div data-role="footer">
            <h4>
                ASOP Music Festival SG-Mid-Year 2012
            </h4>
        </div>
    </div>
    <div data-role="page" id="pageDashboard">
        <div data-role="header" data-theme="e">
            <h1>
                ASOP Judges Scoring</h1>
        </div>
        <div data-role="content">
            <h2>
                Compositions (Songs) & Interpreters</h2>
            <div data-role="collapsible-set" data-theme="b" data-content-theme="b">
                <div data-role="collapsible" data-collapsed="true">
                    <h3>
                        Our Hymn of Faith</h3>
                    <h3>
                        SONG / COMPOSITION: Our Hymn of Faith</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    MUSICALITY (30%):
                                </td>
                                <td>
                                    90.00%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    LYRICS/MESSAGE (40%):
                                </td>
                                <td>
                                    75.00%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (30%):
                                </td>
                                <td>
                                    80.00%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    TOTAL SCORE (100%):
                                </td>
                                <td>
                                    81.00%
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        &nbsp;</p>
                    <h3>
                        INTERPRETER: Pablo Buan</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    VOICE QUALITY (30%):
                                </td>
                                <td>
                                    80.00%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    INTERPRETATION (40%):
                                </td>
                                <td>
                                    70.00%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    STAGE PRESENCE (20%):
                                </td>
                                <td>
                                    80.00%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (10%):
                                </td>
                                <td>
                                    95.00%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    TOTAL SCORE (100%):
                                </td>
                                <td>
                                    77.50%
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <a href="#pageScore" data-role="button" data-theme="b" data-inline="true" data-icon="plus"
                            data-iconpos="right">Update</a></p>
                </div>
                <div data-role="collapsible">
                    <h3>
                        Nagtamo Ng Lingap</h3>
                    <h3>
                        SONG / COMPOSITION</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    MUSICALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    LYRICS/MESSAGE (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        &nbsp;</p>
                    <h3>
                        INTERPRETER</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    VOICE QUALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    INTERPRETATION (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    STAGE PRESENCE (20%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (10%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <a href="#pageScore" data-role="button" data-theme="b" data-inline="true" data-icon="plus"
                            data-iconpos="right">Update</a></p>
                </div>
                <div data-role="collapsible">
                    <h3>
                        Through The Storm</h3>
                    <h3>
                        SONG / COMPOSITION</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    MUSICALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    LYRICS/MESSAGE (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        &nbsp;</p>
                    <h3>
                        INTERPRETER</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    VOICE QUALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    INTERPRETATION (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    STAGE PRESENCE (20%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (10%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <a href="#pageScore" data-role="button" data-theme="b" data-inline="true" data-icon="plus"
                            data-iconpos="right">Update</a></p>
                </div>
                <div data-role="collapsible">
                    <h3>
                        Alabok Na Sisidlan</h3>
                    <h3>
                        SONG / COMPOSITION</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    MUSICALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    LYRICS/MESSAGE (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        &nbsp;</p>
                    <h3>
                        INTERPRETER</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    VOICE QUALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    INTERPRETATION (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    STAGE PRESENCE (20%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (10%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <a href="#pageScore" data-role="button" data-theme="b" data-inline="true" data-icon="plus"
                            data-iconpos="right">Update</a></p>
                </div>
                <div data-role="collapsible">
                    <h3>
                        Sa Iyo</h3>
                    <h3>
                        SONG / COMPOSITION</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    MUSICALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    LYRICS/MESSAGE (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        &nbsp;</p>
                    <h3>
                        INTERPRETER</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    VOICE QUALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    INTERPRETATION (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    STAGE PRESENCE (20%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (10%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <a href="#pageScore" data-role="button" data-theme="b" data-inline="true" data-icon="plus"
                            data-iconpos="right">Update</a></p>
                </div>
                <div data-role="collapsible">
                    <h3>
                        Ligaya Ng Puso Ama</h3>
                    <h3>
                        SONG / COMPOSITION</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    MUSICALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    LYRICS/MESSAGE (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        &nbsp;</p>
                    <h3>
                        INTERPRETER</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    VOICE QUALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    INTERPRETATION (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    STAGE PRESENCE (20%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (10%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <a href="#pageScore" data-role="button" data-theme="b" data-inline="true" data-icon="plus"
                            data-iconpos="right">Update</a></p>
                </div>
                <div data-role="collapsible">
                    <h3>
                        Finding Reason</h3>
                    <h3>
                        SONG / COMPOSITION</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    MUSICALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    LYRICS/MESSAGE (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        &nbsp;</p>
                    <h3>
                        INTERPRETER</h3>
                    <table>
                        <thead>
                            <tr>
                                <th scope="col">
                                    CRITERIA
                                </th>
                                <th scope="col">
                                    SCORE
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    VOICE QUALITY (30%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    INTERPRETATION (40%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    STAGE PRESENCE (20%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OVER-ALL IMPACT (10%):
                                </td>
                                <td>
                                    <em>Not yet entered</em>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <a href="#pageScore" data-role="button" data-theme="b" data-inline="true" data-icon="plus"
                            data-iconpos="right">Update</a></p>
                </div>
            </div>
        </div>
        <div data-role="footer">
            <h4>
                ASOP Music Festival SG-Mid-Year 2012
            </h4>
        </div>
    </div>
    <div data-role="page" id="pageScore">
        <div data-role="header" data-theme="e">
            <h1>
                ASOP Judges Scoring</h1>
        </div>
        <div data-role="content">
            <h1>
                Enter Scores: Our Hymn Of Faith</h1>
            <p>
                Please enter your your scores and click the Update button</p>
            <p>
                &nbsp;</p>
            <h3>
                SONG / COMPOSITION: Our Hymn Of Faith</h3>
            <div data-role="fieldcontain">
                <label for="textFirstName">
                    MUSICALITY (30%):</label>
                <input type="range" name="textFirstName" id="textFirstName" value="90" min="0" max="100" />
            </div>
            <div data-role="fieldcontain">
                <label for="slider-a">
                    LYRICS/MESSAGE (40%):</label>
                <input type="range" name="slider-a" id="slider-a" value="75" min="0" max="100" />
            </div>
            <div data-role="fieldcontain">
                <label for="slider-b">
                    OVER-ALL IMPACT (30%):</label>
                <input type="range" name="slider-b" id="slider-b" value="80" min="0" max="100" />
            </div>
            <div data-role="fieldcontain">
                <label for="text2">
                    TOTAL SCORE (100%):</label>
                <input type="text" name="text2" id="text2" value="81.00%" readonly />
            </div>
            <p>
                &nbsp;</p>
            <h3>
                INTERPRETER: Pablo Buan</h3>
            <div data-role="fieldcontain">
                <label for="Range1">
                    VOICE QUALITY (30%):</label>
                <input type="range" name="Range1" id="Range1" value="80" min="0" max="100" />
            </div>
            <div data-role="fieldcontain">
                <label for="Range2">
                    INTERPRETATION (40%):</label>
                <input type="range" name="Range2" id="Range2" value="70" min="0" max="100" />
            </div>
            <div data-role="fieldcontain">
                <label for="Range3">
                    STAGE PRESENCE (20%):</label>
                <input type="range" name="Range3" id="Range3" value="80" min="0" max="100" />
            </div>
            <div data-role="fieldcontain">
                <label for="Range4">
                    OVER-ALL IMPACT (10%):</label>
                <input type="range" name="Range4" id="Range4" value="95" min="0" max="100" />
            </div>
            <div data-role="fieldcontain">
                <label for="text1">
                    TOTAL SCORE (100%):</label>
                <input type="text" name="text1" id="text1" value="77.50%" readonly />
            </div>
            <p>
                <a href="#" id="buttonInfoNext" data-role="button" data-icon="arrow-r" data-iconpos="right"
                    data-theme="b" data-inline="true">Update</a><a href="#pageDashboard" id="buttonInfoCancel"
                        data-role="button" data-theme="a" data-inline="true" data-icon="delete" data-iconpos="right">Cancel</a></p>
        </div>
        <div data-role="footer">
            <h4>
                ASOP Music Festival SG-Mid-Year 2012
            </h4>
        </div>
    </div>
    <div data-role="page" id="pageDone">
        <div data-role="header" data-theme="e">
            <h1>
                Voting Successful</h1>
        </div>
        <div data-role="content">
            <h1>
                Thank you!</h1>
            <p>
                Your vote has been recorded and your voting code has been included in the lucky
                dip.</p>
            <p>
                You will receive an email and a SMS notification to confirm your vote. Please keep
                any of these notification as it will be your proof for the lucky dip. So keep voting
                for more chances of winning!</p>
            <p>
                <a href="#" id="buttonDone" data-role="button" data-inline="true" data-icon="home"
                    data-iconpos="right" data-theme="b">Done</a><a href="#" id="buttonVoteAgain" data-role="button"
                        data-inline="true" data-icon="refresh" data-iconpos="right" data-theme="b">Vote
                        again!</a></p>
            <p>
                NOTE: Please note that this voting system will not affect any scores during the
                finals night. The decision of the board of judges is final.</p>
        </div>
        <div data-role="footer">
            <h4>
                ASOP Music Festival SG-Mid-Year 2012
            </h4>
        </div>
    </div>
    <div data-role="page" id="pageAlert">
        <div data-role="header" data-theme="e">
            <h1 id="alertTitle">
                Alert Title</h1>
        </div>
        <div data-role="content">
            <h3 id="alertMessage">
                Alert Message</h3>
            <p>
                <a id="alertOK" href="#" data-role="button" data-inline="true">OK</a></p>
        </div>
        <div data-role="footer">
            <h4>
            </h4>
        </div>
    </div>
    <div data-role="page" id="pagePlay" data-candidate="">
        <div data-role="header" data-theme="e">
            <h1 id="playTitle">
            </h1>
        </div>
        <div data-role="content">
            <p id="audioControl">
            </p>
            <h3 id="lyricsTitle">
            </h3>
            <div id="lyricsContent">
            </div>
            <p>
                <a id="playOK" href="#pageVoting" data-role="button" data-inline="true">OK</a></p>
        </div>
        <div data-role="footer">
            <h4>
            </h4>
        </div>
    </div>
</body>
</html>
