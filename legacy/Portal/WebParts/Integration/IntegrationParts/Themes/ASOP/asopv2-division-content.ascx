<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WUserControl" ClientIDMode="Static" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= GetValue("Title") %></title>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    <link rel='stylesheet' media="all" href="<%=WebUtil.Version("~/content/Themes/Integration/MusicCompetition/v2/division/css/medium.min.css")%>" />
    <style type="text/css">
        .content_section_holder {
            height: 75px;
        }

        #bg2 {
            z-index: 2;
            background-position: center;
            height: 100%;
            width: 100%;
            background-repeat: no-repeat;
            min-height: 400px;
            max-height: 400px;
        }

        .footer {
            background-position: left;
            height: 100px;
            width: 100%;
            background-repeat: no-repeat;
        }
    </style>
    <%# GetValue("Resources") %>
</head>
<body>
    <form id="Form1" runat="server">
        <div id="bg2">
        </div>
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
            <h1>Talk</h1>
            <h2>Drop a message. Join the ASOP fever!</h2>--%>

            <%--<h1><%= GetValue("PageName") %></h1>
            <h2><%= GetValue("PageTagline") %></h2>--%>
        </div>
        <div id="content" class="content_section_holder gradient_section">
            <div class="content_wrap" id="breadcrumb" runat="server">
                <%--<p class="breadcrumbs"><a href="/asop/asia-oceania/">Home</a> » <a href="/asop/asia-oceania/talk/">Talk</a></p>--%>
            </div>
        </div>
        <div id="pages" class="pages" runat="server">
            <%--<img class="hostcity_pic" src="/Content/Themes/Integration/MusicCompetition/v2/division/images/hostlocale.jpg" alt="Singapore Marina Bay, Gardens at the Bay" width="800" />
            <h3>Talk about ASOP</h3>

            <div id="comments" class="comments">
                <p class="name_class">Name</p>
                <p class="message_class">Comments: Known as the "melting pot of Asia," Singapore's populace is a diversity of cultures. The four major races inhabiting the country include the majority of Chinese, Malay, Indian and Eurasian. Each race offers a distinct colour in the collage of Singapore's culture, religion, food and language.</p>
            </div>
            <div id="Div1" class="comments">
                <p class="name_class">Name</p>
                <p class="message_class">Comments: Known as the "melting pot of Asia," Singapore's populace is a diversity of cultures. The four major races inhabiting the country include the majority of Chinese, Malay, Indian and Eurasian. Each race offers a distinct colour in the collage of Singapore's culture, religion, food and language.</p>
            </div>
            <div class="comments">
                <p class="name_class">Name</p>
                <p class="message_class">Comments: Known as the "melting pot of Asia," Singapore's populace is a diversity of cultures. The four major races inhabiting the country include the majority of Chinese, Malay, Indian and Eurasian. Each race offers a distinct colour in the collage of Singapore's culture, religion, food and language.</p>
            </div>
            <div class="comments">
                <p class="name_class">Name</p>
                <p class="message_class">Comments: Known as the "melting pot of Asia," Singapore's populace is a diversity of cultures. The four major races inhabiting the country include the majority of Chinese, Malay, Indian and Eurasian. Each race offers a distinct colour in the collage of Singapore's culture, religion, food and language.</p>
            </div>
            <form class="form_class" action="#" method="post">
                <p>
                    <label>Name</label><br />
                    <input class="input_text rounded_corner" type="text" value="Name" name="comment_name" />
                </p>
                <p>
                    <label>Email Address</label><br />
                    <input class="input_text rounded_corner" type="text" value="Email Address" name="comment_eadd" />
                </p>
                <p>
                    <label>Message</label><br />
                    <textarea class="input_textarea">Message</textarea>
                </p>

                <input class="submit_class " type="submit" value="Submit" />

            </form>--%>
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
