<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StreamingConsole.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.Streaming.StreamingConsole" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN"
   "http://www.w3.org/TR/html4/strict.dtd">
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title><% =StreamOwner %> - Live Video Stream</title>

    <link rel="stylesheet" href="<%=WebUtil.Version("~/Content/Parts/Integration/streaming/css/screen.min.css")%>" type="text/css" media="screen, projection">
    <link rel="stylesheet" href="<%=WebUtil.Version("~/Content/Parts/Integration/streaming/css/wowza.min.css")%>" type="text/css" />

    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js")%>" type="text/javascript"></script>
</head>
<body>
    <div class="container">
        <!-- HEADER -->
        <div class="span-18">
            <h1><% =StreamOwner %> - Live Video Stream</h1>
        </div>
        <div class="span-6 last">
            <img src="/Content/Parts/Integration/Streaming/img/integration-logo.png" class="logo" width="220" height="50" alt="" />
        </div>
        <hr class="heading">
        <!-- END HEADER -->
        <div class="span-16">
            <object width="470" height="320">
                <param name="movie" value="/Content/Parts/Integration/Streaming/strobe/StrobeMediaPlayback.swf"></param>
                <param name="flashvars" value="src=http%3A%2F%2Fstream.someorg.org%3A1935%2F<%=StreamName%>%2Fsmil%3Aoutput%2Fmanifest-rtmp.f4m&streamType=live&autoPlay=true&scaleMode=zoom&controlBarMode=floating"></param>
                <param name="allowFullScreen" value="true"></param>
                <param name="allowscriptaccess" value="always"></param>
                <param name="wmode" value="direct"></param>
                <embed src="/Content/Parts/Integration/Streaming/strobe/StrobeMediaPlayback.swf" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true" wmode="direct" width="470" height="320" flashvars="src=http%3A%2F%2Fstream.someorg.org%3A1935%2F<%=StreamName%>%2Fsmil%3Aoutput%2Fmanifest-rtmp.f4m&streamType=live&autoPlay=true&scaleMode=zoom&controlBarMode=floating"></embed>
            </object>
        </div>

        <!-- FOOTER -->
        <div class="span-24">
            <hr class="heading">
            <p><a href="http://stream.someorg.org:1935/<%=StreamName%>/smil:output/playlist.m3u8" style="font-weight: bold; font-size: larger">Mobile Streaming for iOS and Android</a></p>
            <div class="span-1">
                <img src="/Content/Parts/Integration/Streaming/img/icon-company.png" width="34" height="35" alt="" />
            </div>
            <div class="span-23 last copyright">
                &nbsp;&#169;&nbsp;<% =DateTime.Now.Year %> Members Group of God International. All rights reserved.
               
                <div style="float: right"><a runat="server" id="linkHome" href="/">Home</a> <span runat="server" id="panelAdminLink" visible="false">| <a runat="server" id="linkAdmin" href="#">Admin</a> </span>| <a runat="server" id="linkLogOut" href="#">Log Out</a></div>
            </div>
        </div>
        <!-- END FOOTER -->
    </div>
    <% if (CheckSession)
       { %>
    <script type="text/javascript">
        ExecuteSessionCheck(location.href, 30 * 1000);
    </script>
    <% } %>
</body>
</html>
