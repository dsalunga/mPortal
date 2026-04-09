<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WUserControl" ClientIDMode="Static" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<meta name="viewport" content="width=device-width, initial-scale=1.0" />--%>
    <title><%= GetValue("Title") %></title>

    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/Themes/Integration/MusicCompetition/v2/division/js/nivo-slider/jquery.nivo.slider.pack.js")%>" type="text/javascript"></script>
    <%--<link rel='stylesheet' media='screen and (max-width: 800px)' href='/content/Themes/Integration/MusicCompetition/v2/division/css/narrow.css' />--%>
    <link rel="stylesheet" href="<%=WebUtil.Version("~/content/Themes/Integration/MusicCompetition/v2/division/js/nivo-slider/nivo-slider.css")%>" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=WebUtil.Version("~/content/Themes/Integration/MusicCompetition/v2/division/js/nivo-slider/themes/default/default.css")%>" type="text/css" media="all" />
    <link rel='stylesheet' media="all" href="<%=WebUtil.Version("~/content/Themes/Integration/MusicCompetition/v2/division/css/medium.min.css")%>" />
    <style type="text/css">
        body > form {
            height: 100%;
        }

        #contents{margin-top:-600px; margin-bottom:19px;}

        #bg2 {
            z-index: 2;
            background-position: center;
            height: 100%;
            width: 100%;
            background-repeat: no-repeat;
            min-height: 575px;
            max-height: 600px;
        }

        .footer {
            background-position: left;
            height: 100px;
            width: 100%;
            background-repeat: no-repeat;
        }

        .footer_divider {
            display: none;
        }

        .nivoSlider {
            position: relative;
            background: url(/content/Themes/Integration/MusicCompetition/v2/division/images/loading.gif) no-repeat 50% 50%;
            min-height: 300px;
            background-color: transparent !important;
        }

            .nivoSlider img {
                position: absolute;
                top: 0;
                left: 0;
                display: none;
            }

            .nivoSlider a {
                border: 0;
                display: block;
            }
    </style>
    <script type="text/javascript">
        $(window).load(function () {
            $('#slider').nivoSlider({
                randomStart: true,
                controlNav: false
            });
        });
    </script>
    <%# GetValue("Resources") %>
</head>

<body>
    <div id="bg2">
    </div>
    <form id="Form1" runat="server">
        <div id="contents" class="header" runat="server">
            <%--<a href="/asop/asia-oceania/">
                <img class="logo" src="/Content/Themes/Integration/MusicCompetition/v2/division/images/logosmall.png" alt="Asop Logo" /></a>
            <ul class="main_nav_ul">
                <li class="textshadow1 menu_list"><a href="/asop/asia-oceania/">Home</a></li>
                <li class="textshadow1 menu_list"><a href="/asop/asia-oceania/the-show/">The Show</a>
                    <ul class="submenunav subnav1">
                        <li>
                            <a href="/asop/asia-oceania/the-show/">Host City</a>
                        </li>
                        <li>
                            <a href="/asop/asia-oceania/the-show/">The Finalists</a>
                        </li>
                        <ul class="subnav_bg"></ul>
                    </ul>
                </li>
                <li class="textshadow1 menu_list"><a href="/asop/asia-oceania/gallery/">Gallery</a>
                    <ul class="submenunav subnav2">
                        <li>
                            <a href="/asop/asia-oceania/gallery/">Photos</a>
                        </li>
                        <li>
                            <a href="/asop/asia-oceania/gallery/">Videos</a>
                        </li>
                        <ul class="subnav_bg"></ul>
                    </ul>
                </li>
                <li class="textshadow1 menu_list"><a href="/asop/asia-oceania/talk/">Talk</a></li>
                <li class="textshadow1 menu_list"><a href="/asop/asia-oceania/vote/">Vote</a></li>
                <li class="textshadow1 menu_list"><a href="/asop/asia-oceania/about/">About</a></li>
                <li class="social_icons menu_list"><a>
                    <img src="/Content/Themes/Integration/MusicCompetition/v2/division/images/twitter.png" alt="" /></a></li>
                <li class="social_icons menu_list"><a>
                    <img src="/Content/Themes/Integration/MusicCompetition/v2/division/images/rss.png" alt="" /></a></li>
                <li class="social_icons menu_list"><a>
                    <img src="/Content/Themes/Integration/MusicCompetition/v2/division/images/facebook.png" alt="" /></a></li>
            </ul>
            <div class="slider-wrapper theme-default">
                <div id="slider" class="nivoSlider">
                    <img src="/Content/Themes/Integration/MusicCompetition/v2/division/images/home1.jpg" width="800" alt="" />
                    <img src="/Content/Themes/Integration/MusicCompetition/v2/division/images/home2.jpg" width="800" alt="" title="" />
                    <img src="/Content/Themes/Integration/MusicCompetition/v2/division/images/home3.jpg" width="800" alt="" title="" />
                    <img src="/Content/Themes/Integration/MusicCompetition/v2/division/images/home4.jpg" width="800" alt="" />
                </div>
            </div>
            <p class="textshadow1">
                Host City: Singapore<br />
                Date & Time: April 28, 2013 - 7pm SGT<br />
                For inquiries please contact: <a style="color: #FFFF00;" href="mailto:asop-support@example.test">asop-support@example.test</a>
            </p>--%>
        </div>
        <div id="content" class="content_section_holder gradient_section" runat="server">
            <%--<div class="content_wrap">
                <div class="content_section">
                    <img src="/Content/Themes/Integration/MusicCompetition/v2/division/images/note.png" alt="" width="100" />
                    <h3>District 1</h3>
                    <p class="textshadow1">
                        ASOP District Eliminations:<br />
                        April 25, 2013
                    </p>
                    <a>Visit Website</a>
                </div>
                <div class="content_section">
                    <img src="/Content/Themes/Integration/MusicCompetition/v2/division/images/note.png" alt="" width="100" />
                    <h3>District 2</h3>
                    <p class="textshadow1">
                        ASOP District Eliminations:<br />
                        May 1, 2013
                    </p>
                    <a>Visit Website</a>
                </div>
                <div class="content_section">
                    <img src="/Content/Themes/Integration/MusicCompetition/v2/division/images/note.png" alt="" width="100" />
                    <h3>District 3</h3>
                    <p class="textshadow1">
                        ASOP District Eliminations:<br />
                        April 28, 2013
                    </p>
                    <a>Visit Website</a>
                </div>
            </div>--%>
        </div>

        <div class="footer_divider"></div>
        <div id="footer" class="footer" runat="server">
            <%--<ul>
                <li class="textshadow1"><a href="/asop/asia-oceania/">Home</a></li>
                <li class="textshadow1"><a href="#">The Show</a></li>
                <li class="textshadow1"><a href="#">Gallery</a></li>
                <li class="textshadow1"><a href="/asop/asia-oceania/talk/">Talk</a></li>
                <li class="textshadow1"><a href="#">Vote</a></li>
                <li class="textshadow1"><a href="/asop/asia-oceania/about/">About</a></li>
            </ul>
            <p>copyright © 2013 A Song Of Praise Music Festival</p>--%>
        </div>
    </form>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery-ui.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js")%>" type="text/javascript"></script>
</body>
</html>
