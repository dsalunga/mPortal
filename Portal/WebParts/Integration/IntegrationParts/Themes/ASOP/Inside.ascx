<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WUserControl" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width = 800" />

    <title>ASOP Division Official Page</title>
    <%= GetValue("Resources") %>
    <script src="<%=WebUtil.Version("~/content/Themes/Integration/MusicCompetition/js/functions.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        var pageName = '<%= GetValue("PageName") %>';
        var escapedHash;

        function findHash() {
            var hash = window.location.hash;
            if (hash) {
                escapedHash = hash.replace("#!", "");

                //alert(escapedHash);
                if (escapedHash.length != hash.length) {
                    $("#panel_content").html("");

                    $.get("/Content/Parts/Content/Service.svc/GetContentByTitleJson?title=asop-" + escapedHash, function (data) {
                        if ($('#main_contents').is(':visible')) {
                            $("#panel_content").html(data.d);
                        }
                        else {
                            $('.asoplogo2').fadeOut('fast');
                            $('#bg').slideToggle('fast');
                            $("#panel_content").html(data.d);

                            $('#main_contents').slideToggle('fast');
                        }
                    });
                }
            }
        }

        $(document).ready(function () {
            $('.menuselect').hover(function () {
                $(this).find('.buttondown').fadeIn('fast');
            },
                function () {
                    temp = $(this).attr('data-btntext');
                    $(this).find('.buttondown').fadeOut('fast');
                }
            );

            $('.menuselect').click(function () {
                $('.asoplogo2').fadeOut('fast');
                temp = $(this).attr('data-submenupanel');
                $('.submenu:visible').slideToggle('fast');
                $(temp).slideToggle('fast');
            });

            $('.submenuselect').hover(function () {
                $(this).find('.buttondown').fadeIn('fast');
            },
                function () {
                    temp = $(this).attr('data-btntext');
                    $(this).find('.buttondown').fadeOut('fast');
                }
            );

            $('.submenuselect').click(function () {

                var temp2 = $(this).attr('data-scrollto');
                var temp3;
                temp = $(temp2).position();
                if ($('#panel_content').scrollTop() > 10) {
                    temp3 = $('#panel_content').scrollTop();
                    temp.top = temp3 + temp.top;
                }
                $('#panel_content').animate({ scrollTop: temp.top }, 'fast');
            });


            findHash();

            if ($("#panel_content").html() != "" && !$('#main_contents').is(':visible')) {
                $('.asoplogo2').fadeOut('fast');
                $('#bg').slideToggle('fast');
                $('#main_contents').slideToggle('fast');
            }
        });

        $(window).bind('hashchange', function () {
            findHash();
        });
    </script>
    <link rel='stylesheet' media='screen and (max-width: 800px)' href="<%=WebUtil.Version("~/content/Themes/Integration/MusicCompetition/css/narrow.css")%>" />
    <link rel='stylesheet' media='screen and (min-width: 801px)' href="<%=WebUtil.Version("~/content/Themes/Integration/MusicCompetition/css/medium.css")%>" />
    <style type="text/css">
        #aspnetForm, #frmMain {
            height: 100%;
        }

        .menuselect {
            width: 130px;
            height: 31px;
            background-image: url(/Content/Themes/Integration/MusicCompetition/images/buttonup.png);
            background-size: 130px, 31px;
            float: right;
            margin-left: -20px;
            cursor: pointer;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Themes/Integration/MusicCompetition/images/buttonup.png', sizingMethod='scale');
            -ms-filter: "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Themes/Integration/MusicCompetition/images/buttonup.png', sizingMethod='scale')";
        }

        .submenuselect {
            width: 130px;
            height: 31px;
            background-image: url(/Content/Themes/Integration/MusicCompetition/images/buttonup.png);
            background-size: 130px, 31px;
            float: left;
            margin-left: 35px;
            cursor: pointer;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Themes/Integration/MusicCompetition/images/buttonup.png', sizingMethod='scale');
            -ms-filter: "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Themes/Integration/MusicCompetition/images/buttonup.png', sizingMethod='scale')";
        }

        /*#bg{ background-image:url(/Content/Themes/Integration/MusicCompetition/images/bg.jpg); background-size:100%, 100%; height:100%; width:100%; background-repeat:no-repeat; min-height:600px;
		filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Themes/Integration/MusicCompetition/images/bg.jpg', sizingMethod='scale');
		-ms-filter: "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Themes/Integration/MusicCompetition/images/bg.jpg', sizingMethod='scale')";
	    }*/

        #bg2 {
            z-index: 2;
            background-image: url(/Content/Themes/Integration/MusicCompetition/images/bg2.jpg);
            background-size: 100%, 100%;
            height: 100%;
            width: 100%;
            background-repeat: no-repeat;
            min-height: 600px;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Themes/Integration/MusicCompetition/images/bg2.jpg', sizingMethod='scale');
            -ms-filter: "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Content/Themes/Integration/MusicCompetition/images/bg2.jpg', sizingMethod='scale')";
        }
    </style>
</head>
<body>
    <div id="bg2"></div>
    <!--<div id="bg"></div>-->

    <div id="menu">
        <div id="selectcontain" runat="server" clientidmode="static">
            <%--<a href="/vote/">
                <div class="menuselect" data-btntext="votepage">
                    <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 30px;">vote</div>
                </div>
            </a>
            <a href="/contact-us/">
                <div class="menuselect" data-btntext="contact us">
                    <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 30px;">contact us</div>
                </div>
            </a>
            <a href="#!about">
                <div class="menuselect" data-btntext="about">
                    <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 33px;">about</div>
                </div>
            </a>
            <a href="/videos/">
                <div class="menuselect" data-btntext="videos">
                    <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext">videos</div>
                </div>
            </a>
            <a href="/photos/">
                <div class="menuselect" data-btntext="photos">
                    <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext">photos</div>
                </div>
            </a>
            <a href="#!theshow">
                <div class="menuselect" data-submenupanel="#theshow_menu" data-btntext="the show">
                    <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 33px;">the show</div>
                </div>
            </a>
            <a href="#!asopwinner">
                <div class="menuselect" data-btntext="asop winner">
                    <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 20px;">asop winner</div>
                </div>
            </a>
            <a href="#!finalists">
                <div class="menuselect" data-btntext="finalists">
                    <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext">finalists</div>
                </div>
            </a>--%>
        </div>
    </div>
    <div id="contents">
        <div class="wrapper">
            <a href="/">
                <img class="asoplogo" src="/Content/Themes/Integration/MusicCompetition/images/largelogo.png" alt="ASOP Logo" /></a>
            <img class="asoplogo2" src="/Content/Themes/Integration/MusicCompetition/images/largelogo.png" />

            <div id="main_contents">
                <div id="submenu_panel">
                    <a href="/">
                        <img class="menulogo" src="/Content/Themes/Integration/MusicCompetition/images/logosmall2.png" width="210" /></a>
                    <div id="theshow_menu" class="submenu border1">
                        <div class="submenuselect" data-btntext="host city" data-scrollto="#theshow">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 30px;">host city</div>
                        </div>
                        <div class="submenuselect" data-btntext="host locale" data-scrollto="#hostlocaletag">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 30px;">host locale</div>
                        </div>
                        <div class="submenuselect" data-btntext="venue" data-scrollto="#venuetag">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 40px;">venue</div>
                        </div>
                    </div>
                    <div id="multimedia_menu" class="submenu border1">
                        <div class="submenuselect" data-btntext="videos" data-scrollto="#videotag">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 30px;">videos</div>
                        </div>
                        <div class="submenuselect" data-btntext="photos" data-scrollto="#photostag">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 30px;">photos</div>
                        </div>
                    </div>
                </div>
                <form runat="server" id="frmMain">
                    <div id="panel_content" runat="server" clientidmode="static"></div>
                </form>
            </div>
            <div id="menu2">
                <div id="selectcontain2" runat="server" clientidmode="static">
                    <%--<a href="/vote/">
                        <div class="menuselect" data-btntext="votepage">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 30px;">vote</div>
                        </div>
                    </a>
                    <a href="/contact-us/">
                        <div class="menuselect" data-btntext="contact us">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 30px;">contact us</div>
                        </div>
                    </a>
                    <a href="#!about">
                        <div class="menuselect" data-btntext="about">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 33px;">about</div>
                        </div>
                    </a>
                    <a href="/videos/">
                        <div class="menuselect" data-btntext="videos">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext">videos</div>
                        </div>
                    </a>
                    <a href="/photos/">
                        <div class="menuselect" data-btntext="photos">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext">photos</div>
                        </div>
                    </a>
                    <a href="#!theshow">
                        <div class="menuselect" data-submenupanel="#theshow_menu" data-btntext="the show">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 33px;">the show</div>
                        </div>
                    </a>--%>
                    <%--<a href="#!asopwinner">
                        <div class="menuselect" data-btntext="asop winner">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext" style="padding-left: 20px;">asop winner</div>
                        </div>
                    </a>
                    <a href="#!finalists">
                        <div class="menuselect" data-btntext="finalists">
                            <img class="buttondown" src="/Content/Themes/Integration/MusicCompetition/images/buttondown.png" width="130" /><div class="buttontext">finalists</div>
                        </div>
                    </a>--%>
                </div>
            </div>
            <div id="bottombar">A Song Of Praise 2012 All Rights Reserved</div>
        </div>
    </div>
    <%--<script type="text/javascript">
        clearInterval(ruark);
        jQuery("#asop_slideshow > .asop_pic:gt(0)").hide();
        var ruark = setInterval(function () {
            jQuery('#asop_slideshow > .asop_pic:first')
              .fadeOut(500)
              .next()
              .fadeIn(500)
              .end()
              .appendTo('#asop_slideshow');
        }, 6000);
    </script>--%>
</body>
</html>
