<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Playback.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Profile.LessonReviewer.Playback" %>
<%@ Register Assembly="Media-Player-ASP.NET-Control" Namespace="Media_Player_ASP.NET_Control"
    TagPrefix="cc1" %>
<div id="panelLoading">
    <h3>
        <img src="/Content/Assets/Images/roller.gif" alt="Loading..." />&nbsp;Checking Make-Up Streaming
        Service...
    </h3>
</div>
<div runat="server" clientidmode="Static" class="alert alert-danger" id="panelErrorMgs" style="display: none">
    Sorry, but the Make-Up Streaming Service is not accessible. Please make sure you
    are connected to the Singapore Locale Network and check whether the Make-Up Streaming
    Service is up. Kindly approach the assigned worker for assistance. You cannot continue
    your Make-Up session.
</div>
<input type="hidden" clientidmode="Static" id="hShowExitAlert" runat="server" value="0" />
<asp:HiddenField runat="server" ID="hEnablePlayback" ClientIDMode="Static" Value="0" />
<div id="panelPlayback" style="display: none;">
    <h4 runat="server" id="lblServiceTitle">
    </h4>
    <br />
    <div style="margin-bottom: 5px; width: 600px">
        <div id="panelLanguage" style="float: left">
            Select Language:&nbsp;<asp:DropDownList ID="cboLanguage" CssClass="input" ClientIDMode="Static" runat="server">
                <asp:ListItem Selected="True" Value=""></asp:ListItem>
                <asp:ListItem Value="TL">Tagalog</asp:ListItem>
                <asp:ListItem Value="EN">English</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="cmdSilentLanguage" OnClick="cmdSilentLanguage_Click" CausesValidation="false"
                ClientIDMode="Static" runat="server" Style="display: none" />
        </div>
        <div id="panelPlaylist" style="float: right; display: none">
            <select id="cboPlaylist" name="cboPlaylist">
                <option value=""># Play All #</option>
            </select>
            <asp:Button ID="cmdSilentPost" OnClick="cmdSilentPost_Click" CausesValidation="false"
                ClientIDMode="Static" runat="server" Style="display: none" />
            <asp:HiddenField runat="server" ID="hPlay" ClientIDMode="Static" />
        </div>
        <%--<div style="float: right; display: none">
            Play via:&nbsp;<img src="/Content/Assets/Images/Common/wmp.png" alt="Windows Media Player" title="Windows Media Player" />&nbsp;<img
                src="/Content/Assets/Images/Common/silverlight.png" alt="Silverlight" title="Silverlight" />
        </div>--%>
    </div>
    <div runat="server" id="panelPlayer" visible="false" style="width: 600px">
        <div runat="server" id="panelWMPlayer" clientidmode="Static">
            <cc1:Media_Player_Control ClientIDMode="Static" ID="mediaPlayer" MovieURL="" runat="server"
                Height="400px" Width="600px" AutoStart="true" uiMode="None" Volume="90" />
        </div>
        <div runat="server" id="panelOtherPlayer" clientidmode="Static" visible="false">
            <object id="nonIeWMPlayer" width="600px" height="400px" type="application/x-ms-wmp"
                viewastext>
                <param name="autoStart" value="True" />
                <param runat="server" id="paramUrl" clientidmode="Static" name="URL" value="" />
                <param name="enabled" value="True" />
                <param name="balance" value="0" />
                <param name="currentPosition" value="0" />
                <%--<param name="enableContextMenu" value="false" />--%>
                <param name="fullScreen" value="false" />
                <param name="mute" value="False" />
                <param name="playCount" value="1" />
                <param name="rate" value="1" />
                <param name="stretchToFit" value="False" />
                <param name="uiMode" value="none" />
                <param name="volume" value="90" />
            </object>
        </div>
        <div style="margin-top: 3px; float: left">
            <input type="button" class="btn btn-default btn-sm" style="width: 60px" id="btnPlayPause" value="Pause" onclick="togglePlayPause();" />
            <input type="button" class="btn btn-default btn-sm" value="Fullscreen" onclick="toggleFullscreen();" />
        </div>
        <div id="panelOptions" style="margin-top: 3px; float: right;" class="no-bottom-margin">
            <span id="lblInfo" class="badge1">--:--</span>&nbsp;
            <input type="button" class="btn btn-default btn-sm" id="btnStop" value="Stop" onclick="resetPlayback();" />
            <select id="cboOptions" class="input">
                <option value="">Options</option>
                <optgroup label="Rewind">
                    <option value="0.5">30 secs</option>
                    <option value="1">1 min</option>
                    <option value="2">2 mins</option>
                    <option value="3">3 mins</option>
                    <option value="5">5 mins</option>
                </optgroup>
                <optgroup label="Volume"><!-- does not work for chrome -->
                    <option value="20">20%</option>
                    <option value="40">40%</option>
                    <option value="60">60%</option>
                    <option value="80">80%</option>
                    <option value="100">100%</option>
                </optgroup>
            </select>
        </div>
        <br />
        <br />
        <br />
        <br />
        <div id="panelButtons" style="display: none">
            <div style="margin-bottom: 5px" class="aspnet-checkbox">
                <asp:CheckBox ID="chkConfirm" ClientIDMode="Static" runat="server" Text="I confirm I have finished watching all the videos in this Make-Up session." />
            </div>
            <div>
                <asp:Button ID="cmdContinue" CssClass="btn btn-default" Width="170px" ClientIDMode="Static"
                    runat="server" Text="Submit My Attendance" OnClick="cmdContinue_Click" OnClientClick="return disableExitAlert();" />&nbsp;<asp:Button
                        ID="cmdCancel" CssClass="btn btn-default" OnClientClick="return cancelSession();" runat="server"
                        Text="Cancel" Width="75px" OnClick="cmdCancel_Click" />
            </div>
        </div>
    </div>
</div>
<asp:Literal ID="scriptMakeUpTest" runat="server"></asp:Literal>
<script type="text/javascript">
    var DEFAULT_FILE = 'Playback';
    var firstItem = '';
    var showedFullscreenMsg = false;
    var sessionId = '<% =Request["SessionId"] %>';
    var date = '<% =Request["Date"] %>';
    var serviceType = '<% =Request["ServiceType"] %>';
    var prevPos = <% =PrevPos %>;
    var prevFile = '<% =PrevFile %>';
    var saveSessionUrl = '/Content/Parts/Integration/makeup.ashx?SessionId=' + sessionId + '&Date=' + date + '&ServiceType=' + serviceType;

    $(document).ready(function () {
        disableInputs(true);
        if ($("#hEnablePlayback").val() == "1") {
            // Streaming Service Check
            if (typeof makeUpTest == "undefined" || makeUpTest != "OK") {
                // Failed
                failedCheck();
            }
            else {
                console.log('check success...begin player setup.');
                var player = getPlayer();
                setTimeout(function(){
                    player.enableContextMenu = false;
                }, 1000);

                //hideLoader();
                disableInputs(false);
                $("#chkConfirm").change(function () {
                    var checked = $(this).is(":checked");
                    $("#cmdContinue").attr("disabled", !checked);
                });

                $('#cboOptions').change(function(){
                    var optval = $(this).val();
                    //console.log('option change: ' + optval);
                    if(optval != ''){
                        var optvalint = parseFloat(optval);
                        if(optvalint > 10) {
                            // Volume
                            player.settings.volume = optvalint;
                        } else {
                            optvalint = optvalint*60;
                            var secs = player.controls.currentPosition;
                            if(secs > optvalint){
                                player.controls.currentPosition = secs - optvalint;
                            }
                        }

                        $(this).val('');
                    }
                });

                var confirmChecked = $("#chkConfirm").is(":checked");
                $("#cmdContinue").attr("disabled", !confirmChecked);
                $("#panelPlayback").css("display", "");

                // Display Segments
                /*var mediaSegments = { "SegmentCount": 3, "Segments": [
                { "Index": 0, "Caption": "Part 1" },
                { "Index": 3, "Caption": "Part 2" },
                { "Index": 5, "Caption": "Part 3"}]
                };*/

                $("#cboLanguage").change(function () {
                    disableExitAlert();
                    $("#cmdSilentLanguage")[0].click();
                });

                /*
                if (typeof mediaSegments != "undefined") {
                    var browserIsIE = getIEVersion() > 0;
                    var cboSegments = $("#cboParts");
                    var cboPlaylist = $("#cboPlaylist");
                    var playValue = $("#hPlay").val();
                    var i = 0;

                    var _cboSegments = cboSegments[0];
                    var _cboPlaylist = cboPlaylist[0];

                    for (i = 0; i < mediaSegments.Segments.length; i++) {
                        var segment = mediaSegments.Segments[i];
                        if (browserIsIE && mediaSegments.CurrentSegment == -1) {
                            var newOption = document.createElement("OPTION");
                            newOption.innerHTML = segment.Caption;
                            newOption.value = segment.Index.toString();

                            _cboSegments.appendChild(newOption);
                        }

                        var optionPlaylist = document.createElement("OPTION");
                        optionPlaylist.innerHTML = segment.Caption;
                        optionPlaylist.value = segment.Number;

                        _cboPlaylist.appendChild(optionPlaylist);
                    }

                    if (playValue != "") {
                        cboPlaylist.val(playValue);
                    }

                    // Code for IE only
                    if (browserIsIE && mediaSegments.CurrentSegment == -1) {
                        cboSegments.change(function () {
                            // Segment Changed
                            var mediaIndex = cboSegments.val();
                            if (mediaIndex != "") {
                                // The following code works only for IE!
                                if (getIEVersion() > 0) {
                                    var player = getPlayer();
                                    if (player != null) {
                                        // Sets the current playing item.
                                        player.controls.currentItem = player.currentPlaylist.item(parseInt(mediaIndex));
                                        if (player.playState != 3 && player.playState != 6 && player.playState != 7 && player.playState != 9 && player.playState != 11) {
                                            // when not playing, buffering, waiting, transitioning, and reconnecting
                                            player.controls.play();
                                        }
                                    }
                                }
                                else {
                                    alert("As of the moment, this feature is only supported in Internet Explorer.");
                                }

                                cboSegments.val("");
                            }
                        });

                        $("#panelParts").css("display", "");
                    }

                    cboPlaylist.change(function () {
                        var segmentNumber = cboPlaylist.val();
                        disableExitAlert();
                        $("#hPlay").val(segmentNumber);
                        $("#cmdSilentPost")[0].click();
                    });

                    $("#panelPlaylist").css("display", "");
                }
                */

                if(prevFile != '' && prevFile != DEFAULT_FILE){
                    setTimeout(restorePlayerState, 500)
                } else {
                    setTimeout(beginSavePlayerState, 500)
                }

                setTimeout(displayInfo, 1000)
            }

            $("#panelButtons").css("display", "");
            ExecuteKeepAlive();
        }
        else {
            failedCheck();
        }

        hideLoader();
    });

    function disableInputs(disabled) {
        $("#chkConfirm").attr("disabled", disabled);
        $("#cmdContinue").attr("disabled", disabled);
    }

    function failedCheck() {
        $("#panelErrorMgs").css("display", "");
    }

    function hideLoader() {
        $("#panelLoading").css("display", "none");
    }

    function displayInfo(){
        var player = getPlayer();
        if(player){
            setInterval(function(){
                var cs = player.controls.currentPositionString;
                if(cs != ''){
                    if(player.currentMedia){
                        $('#lblInfo').html(cs + '/' + player.currentMedia.durationString);
                    } else {
                        $('#lblInfo').html(cs);
                    }
                } else {
                    $('#lblInfo').html('--:--');
                }
            }, 1000);
        }
    }

    function getPlayer() {
        var player = $("#nonIeWMPlayer")[0];
        if (!player)
            player = $("#mediaPlayer > object")[0];
        return player;
    }

    function beginSavePlayerState() {
        var player = getPlayer();
        if (player) {
            console.log('begin saving...');
            
            if (firstItem == '' && player.playState == 3) { // save first file for ref
                firstItem = player.controls.currentItem.name;
            }

            setInterval(function () {
                var currSecs = player.controls.currentPosition;
                var filename = player.controls.currentItem.name;

                $.get(saveSessionUrl + '&pos=' + currSecs + '&file=' + filename, function () {
                    //console.log('Save session done.');
                });
            }, 1000 * 30); // every 3 mins

            /*
            var fileindex = 0;
            for (var i = 0; i < player.currentPlaylist.count; i++){
                if (filename === player.currentPlaylist.item(i).name) {
                    fileindex = i;
                    break;
                }
            }
            */
            //console.log('name: ' + filename + ', secs: ' + currSecs);
        } else {
            //console.log('save: cannot get player handle');
        }
    }

    function restorePlayerState() {
        var player = getPlayer();
        if (player && (prevFile != '' && prevFile != DEFAULT_FILE)) {
            try {
                var restorePlayerBody = function(){
                    if (player.playState == 3) {
                        // Initialiaze / Move to first file
                        var mediaCount = player.currentPlaylist.count;
                        if (mediaCount > 0) {
                            //console.log('begin restore: ' + prevPos + ', ' + prevFile);

                            if(firstItem == ''){ // save first file for ref
                                firstItem = player.controls.currentItem.name;
                            }

                            var mediaIndex = 0;
                            var restoreScanFiles = function(){
                                console.log('scan: ' + mediaIndex);
                                if(mediaIndex < mediaCount){
                                    var media = player.currentMedia;
                                    if(media.name === prevFile){
                                        if(media.duration > prevPos && prevPos != 0){
                                            // Seek here
                                            player.controls.currentPosition = (prevPos > 30) ? prevPos - 30 : prevPos;
                                        }

                                        //console.log('restore done. invoke saving...');
                                        beginSavePlayerState();
                                        // End of seeking
                                    } else {
                                        player.controls.next();
                                        setTimeout(restoreScanFiles, 1000);
                                    }
                                } else {
                                    // Did not find the file
                                    //console.log('cannot find file to restore. restore done.');
                                }
                            }
                            setTimeout(restoreScanFiles, 500);
                        }
                    } else {
                        //console.log('playState: ' + player.playState);
                        setTimeout(restorePlayerBody, 500);
                    }
                };

                restorePlayerBody();
            } catch (err) {
                //alert("Sorry, unable to restore position. Error: " + err.description);
                //console.log('error: ' + err.description);
            }
        } else {
            //console.log('restore: cannot get player handle');
        }
    }

    function resetPlayback(){
        var reset = confirm('Sure you want to stop and start from the beginning?');
        if(reset){
            var player = getPlayer();
            if (player) {
                player.controls.stop();
                $('#btnPlayPause').val('Play');

                if(firstItem != ''){
                    var toPrevFile = function(){
                        if(firstItem != player.controls.currentItem.name){
                            player.controls.previous();
                            setTimeout(toPrevFile, 500);
                        }
                    }

                    setTimeout(toPrevFile, 500);
                }
            }
        }
    }

    function toggleFullscreen() {
        var player = getPlayer();
        if (player) {
            if (player.playState != 3){
                player.controls.play();
            }

            if (player.playState == 3) {
                if (!showedFullscreenMsg) {
                    alert('NOTE: To cancel Fullscreen, press ESC.');
                    showedFullscreenMsg = true;
                }

                player.fullScreen = true;
            }
        }
    }

    function togglePlayPause() {
        var player = getPlayer();
        if (player) {
            if (player.playState != 3){
                player.controls.play();
                $('#btnPlayPause').val('Pause');
            } else {
                player.controls.pause();
                $('#btnPlayPause').val('Play');
            }
        }
    }

    function disableExitAlert() {
        $("#hShowExitAlert").val("0");
        return true;
    }

    function cancelSession() {
        var userAnswer = confirm("WARNING: You will be not able to submit your attendance. Are you sure you want to CANCEL your Make-Up session?");
        if (userAnswer) {
            $("#hShowExitAlert").val("0");
        }
        return userAnswer;
    }

    window.onbeforeunload = function (evt) {
        var showAlert = $("#hShowExitAlert").val() == "1";
        if (showAlert) {
            return "WARNING: PLEASE SUBMIT YOUR ATTENDANCE BY CLICKING THE 'Submit My Attendance' BUTTON!!!\n\nIf you are leaving this page and you have not submitted your attendance, please CANCEL this window to continue submitting your attendance.";
        }
    }
</script>
