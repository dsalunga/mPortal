<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceVideoDurationInput.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.LessonReviewer.ServiceVideoDurationInput" %>
<%@ Register Assembly="Media-Player-ASP.NET-Control" Namespace="Media_Player_ASP.NET_Control"
    TagPrefix="cc1" %>
<asp:HiddenField ID="hStartDate" runat="server" Value="" ClientIDMode="Static" />
<asp:HiddenField ID="hEndDate" runat="server" Value="" ClientIDMode="Static" />
<asp:HiddenField ID="hMemberId" runat="server" Value="-1" ClientIDMode="Static" />
<asp:HiddenField ID="hLocaleId" runat="server" Value="-1" ClientIDMode="Static" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewCalendar" runat="server">
        <div style="margin-bottom: 5px" class="no-bottom-margin">
            <asp:Button ID="cmdPrevious" CssClass="btn btn-default btn-sm" runat="server" OnClick="cmdPrevious_Click" Text="&lt;&lt;"
                Visible="True" />
            <asp:DropDownList ID="cboMonth" CssClass="input" runat="server" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList>
            <asp:DropDownList ID="cboYear" runat="server" CssClass="input col-md-2" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList>
            <asp:Button ID="cmdNext" runat="server" CssClass="btn btn-default btn-sm" OnClick="cmdNext_Click" Text="&gt;&gt;" Visible="True" />
            &nbsp;
            <asp:Button ID="cmdToday" runat="server" Text="Today" CssClass="btn btn-default btn-sm" OnClick="cmdToday_Click" Visible="True" />
        </div>
        <asp:Calendar CssClass="Calendar-Table" ID="monthCalendar" runat="server" BackColor="White"
            BorderColor="White" BorderWidth="1px" EnableTheming="False" FirstDayOfWeek="Sunday"
            Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" NextPrevFormat="FullMonth"
            OnDayRender="monthCalendar_DayRender" OnVisibleMonthChanged="monthCalendar_VisibleMonthChanged"
            ShowGridLines="True">
            <SelectedDayStyle BackColor="#3366cc" CssClass="Calendar-Day Selected-Calendar-Day" />
            <TodayDayStyle CssClass="Calendar-Day Today-Day" />
            <OtherMonthDayStyle CssClass="Calendar-Day Other-Month-Day" />
            <DayStyle CssClass="Calendar-Day" Width="120px" />
            <NextPrevStyle Font-Bold="True" Font-Size="9pt" ForeColor="#333333" VerticalAlign="Bottom" />
            <DayHeaderStyle CssClass="Day-Header" Font-Bold="True" />
            <TitleStyle BorderWidth="1px" BorderColor="#BBBBBB" CssClass="EventCalendar-Calendar-Title"
                Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
        </asp:Calendar>
        <br />
        <div>
            <div style="float: left; width: 150px">
                <div style="width: 15px; height: 15px; background-color: #25EF2B; float: left; margin-top: 2px;
                    margin-right: 4px;">
                </div>
                With Duration
            </div>
            <div style="float: left;">
                <div style="width: 15px; height: 15px; background-color: #DA4D4D; float: left; margin-top: 2px;
                    margin-right: 4px;">
                </div>
                Without Duration
            </div>
        </div>
    </asp:View>
    <asp:View ID="viewDurationInput" runat="server">
        <div style="padding: 10px 0 10px 0">
            <h3 runat="server" id="lblServiceName" style="margin-bottom: 5px">
            </h3>
            <span runat="server" id="lblServiceDate"></span>
            <br />
            <br />
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <label for="cboHour">
                            Duration (HH:MM):</label>
                    </td>
                    <td class="no-bottom-margin">
                        <asp:DropDownList ID="cboHour" CssClass="input" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <strong>:</strong>
                    </td>
                    <td class="no-bottom-margin">
                        <asp:DropDownList ID="cboMinute" CssClass="input" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;<input type="button" id="cmdFetcher" onclick="tryFetchDuration();" value="Try Fetch"
                            style="display: none;" class="btn btn-default" />
                    </td>
                </tr>
            </table>
            <div id="panelPlayer" style="display: none">
                <div runat="server" id="panelWMPlayer" clientidmode="Static">
                    <cc1:Media_Player_Control ClientIDMode="Static" ID="mediaPlayer" MovieURL="" runat="server"
                        Height="400px" Width="600px" AutoStart="false" />
                </div>
                <div runat="server" id="panelOtherPlayer" clientidmode="Static" visible="false">
                    <object id="nonIeWMPlayer" width="600px" height="400px" type="application/x-ms-wmp"
                        viewastext>
                        <param name="autoStart" value="False" />
                        <param runat="server" id="paramUrl" clientidmode="Static" name="URL" value="" />
                        <param name="enabled" value="True" />
                        <param name="balance" value="0" />
                        <param name="currentPosition" value="0" />
                        <param name="enableContextMenu" value="True" />
                        <param name="fullScreen" value="False" />
                        <param name="mute" value="False" />
                        <param name="playCount" value="1" />
                        <param name="rate" value="1" />
                        <param name="stretchToFit" value="False" />
                        <param name="uiMode" value="full" />
                    </object>
                </div>
            </div>
            <br />
            <div style="margin-top: 5px">
                <asp:Button ID="cmdUpdate" ClientIDMode="Static" CssClass="btn btn-primary" Width="85px"
                    runat="server" Text="Update" OnClick="cmdUpdate_Click" />&nbsp;<asp:Button ID="cmdCancel"
                        CssClass="btn btn-default" Width="75px" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
                        Visible="false" />
                <a href="" runat="server" id="linkCancel" style="font-weight: bold;">Cancel</a>
            </div>
            <br />
            <div style="color: Red" id="panelMsg">
                <asp:Literal ID="lblMsg" runat="server" EnableViewState="False"></asp:Literal>
            </div>
        </div>
    </asp:View>
</asp:MultiView>
<asp:Literal ID="scriptMakeUpTest" runat="server"></asp:Literal>
<script type="text/javascript">
    // Player States: 1-Stopped, 3-Playing, 6-Buffering, 7-Waiting, 9-Transitioning, 11-Reconnecting
    var maxFetchRetries = 7;

    var fetchState = 0; // 0 - begin
    var mediaCount = 0;
    var currentMediaIndex = 0;
    var totalDuration = 0;
    var fetchRetries = 0;

    $(document).ready(function () {
        // Streaming Service Check
        if (typeof makeUpTest == "undefined" || makeUpTest != "OK") {
            // Failed
        } else {
            // Success
            showFetcher();
        }
    });

    function tryFetchDuration() {
        showPlayer(true);

        var player = getPlayer();
        if (player) {
            if (player.playState != 1) {
                player.controls.stop();
            }

            resetFetcher();
            invokeFetcher();
        }
    }

    function resetFetcher() {
        fetchState = 0;
        mediaCount = 0;
        currentMediaIndex = 0;
        totalDuration = 0;
        fetchRetries = 0;
    }

    function showPlayer(show) {
        //console.log('show player: ' + show);
        $("#panelPlayer").css("display", show ? "" : "none");
    }

    function invokeFetcher() {
        setTimeout(beginFetch, 1000);
    }

    function beginFetch() {
        try {
            var player = getPlayer();
            if (player) {
                if (player.playState == 3) {
                    //console.log('state: 3');
                    if (fetchState == 0) {
                        // Initialiaze / Move to first file
                        mediaCount = player.currentPlaylist.count;
                        if (mediaCount > 0) {
                            fetchState = 1;
                            invokeFetcher();
                        }
                    }
                    else if (fetchState == 1) {
                        if (mediaCount > 0) {
                            var media = player.currentMedia;
                            totalDuration += media.duration;
                            currentMediaIndex++;

                            if (mediaCount > currentMediaIndex) {
                                player.controls.next();
                                invokeFetcher();
                            }
                            else {
                                // Complete here
                                showPlayer(false);
                                player.controls.stop();

                                setDuration(totalDuration);
                                resetFetcher();
                            }
                        }
                    }

                    fetchRetries = 0;
                }
                else if (fetchRetries < maxFetchRetries) {
                    //console.log('fetch retry...');
                    fetchRetries++;
                    if (player.playState != 3 && player.playState != 6 && player.playState != 7 && player.playState != 9 && player.playState != 11) {
                        // when not playing, buffering, waiting, transitioning, and reconnecting
                        player.controls.play();
                    }

                    invokeFetcher();
                }
                else {
                    showPlayer(false);
                    alert("Sorry, unable to fetch duration.");
                }
            }
        }
        catch (err) {
            alert("Sorry, unable to fetch duration. Error: " + err.description);
        }
    }

    function setDuration(duration) {
        // get hours
        var remMins = duration % (60 * 60);
        var remSecs = remMins % 60;

        var hours = (duration - remMins) / (60 * 60);
        var mins = (remMins - remSecs) / 60;

        $("#cboHour").val(hours.toString());
        $("#cboMinute").val(mins.toString());

        alert("Done! Total duration (in HH:MM) is " + formatTimeDigit(hours.toString()) + ":" + formatTimeDigit(mins.toString()));
    }

    function showFetcher() {
        $("#cmdFetcher").css("display", "");
    }

    function formatTimeDigit(timeDigit) {
        if (timeDigit.length == 1) {
            return "0" + timeDigit;
        }
        return timeDigit;
    }

    function getPlayer() {
        var player = $("#nonIeWMPlayer")[0];
        if (!player)
            player = $("#mediaPlayer > object")[0];
        return player;
    }
</script>
