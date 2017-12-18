<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WMPControl.ascx.cs"
    Inherits="WCMS.WebSystem.Controls.WMPControl" %>
<!-- Name: IE -->
<object id="Player" width="600px" height="400px" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6"
    viewastext>
    <param name="autoStart" value="True" />
    <param name="URL" value="/Content/Parts/Profile/MakeUp/Playback.ashx?ServiceType=PM&Date=2011-02-09&Language=EN" />
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
<input type="button" name="BtnPlay" value="Play" onclick="StartMeUp()" />
<input type="button" name="BtnStop" value="Stop" onclick="ShutMeDown()" />
<script>
<!--
    function StartMeUp() {
        Player.URL = "laure.wma";
    }

    function ShutMeDown() {
        Player.controls.stop();
    }
-->
</script>
<!-- Name: Firefox -->
<object id="Player2" type="application/x-ms-wmp" width="300" height="200">
    <param name="url" value="seattle.wmv" />
    <param name="autostart" value="true" />
</object>
<input type="button" value="Vol" onclick="ChangeVolume()" />
<script>
    function ChangeVolume() {
        Player2.settings.volume = 90;
    }
</script>
<!-- Snipping -->
<script type="text/javascript">
    if (-1 != navigator.userAgent.indexOf("MSIE")) {
        /*
        document.write('<OBJECT id="Player"');
        document.write(' classid="clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"');
        document.write(' width=300 height=200></OBJECT>');
        */
    }
    else if (-1 != navigator.userAgent.indexOf("Firefox")) {
        /*
        document.write('<OBJECT id="Player"');
        document.write(' type="application/x-ms-wmp"');
        document.write(' width=300 height=200></OBJECT>');
        */
    }         
</script>
