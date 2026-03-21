<%@ Control Language="C#" AutoEventWireup="true" ClassName="WCMS.WebSystem.WebParts.Photo.InstagramGallery" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%
    var context = new WContext(this);
    var element = context.Element;
    var instagramId = element.GetParameterValue("InstagramId");
    var instagramUsername = element.GetParameterValue("InstagramUsername");

    if (string.IsNullOrEmpty(instagramId))
        instagramId = "38126460";
%>

<script type="text/javascript" src="<%=WebUtil.Version("~/content/plugins/fancybox/jquery.mousewheel-3.0.2.pack.js")%>"></script>
<script type="text/javascript" src="<%=WebUtil.Version("~/content/plugins/fancybox/jquery.fancybox-1.3.1.pack.js")%>"></script>
<link rel="stylesheet" type="text/css" href="<%=WebUtil.Version("~/content/plugins/fancybox/jquery.fancybox-1.3.1.css")%>" media="screen" />

<style type="text/css">
    #gallery-photos {
        padding: 15px;
        margin: 0;
        overflow: hidden;
        border-top: 1px solid #FCFCFC;
    }

        #gallery-photos #gallery-more {
            float: left;
            margin-top: 20px;
            margin-left: 15px;
            display: none;
        }

            #gallery-photos #gallery-more a {
                text-decoration: none;
            }

            #gallery-photos #gallery-more img {
                float: left;
            }

            #gallery-photos #gallery-more div {
                float: left;
                margin-top: 2px;
                font-weight: bold;
            }

        #gallery-photos ul {
            list-style: none;
            float: left;
            padding: 0;
            margin: 0;
        }

            #gallery-photos ul li {
                display: block;
                float: left;
                padding: 15px;
                width: auto;
                border: none;
            }

                #gallery-photos ul li img {
                    padding: 5px;
                    background-color: #fff;
                    border: 1px solid rgba(0, 0, 0, 0.117647);
                }
</style>

<script type="text/javascript" src="<%=WebUtil.Version("~/content/plugins/embedagram/jquery-embedagram.pack.js")%>"></script>
<script type="text/javascript">
    function formatTitle(title, currentArray, currentIndex, currentOpts) {
        return '<div id="fancybox-custom-title"><span><a href="javascript:;" onclick="$.fancybox.close();"><img title="close" src="/Content/Assets/Images/closelabel.gif" /></a></span>' + (title && title.length ? '<strong>' + title + '</strong>' : '') + (currentArray.length > 1 ? 'Photo ' + (currentIndex + 1) + ' of ' + currentArray.length : '') + '</div>';
    }

    $(document).ready(function () {
        $('#photo-list').embedagram({
            instagram_id: <%= instagramId %>,
            success: function () { 
                //$('#photo-list'); 

                $("#photo-list li a").fancybox({
                    'transitionIn': 'elastic',
                    'transitionOut': 'elastic',
                    'titlePosition': 'inside',
                    'cyclic': true,
                    'overlayOpacity': 0.5,
                    'overlayColor': '#000',
                    'showCloseButton': false,
                    'titleFormat': formatTitle
                });

                var moreLink = $('#gallery-more');
                if(moreLink.length > 0){
                    $(moreLink).show('fast');
                }
            },
            limit: 50,
            link_type: 'img',
            thumb_width: 150
        });
    });
</script>

<div id="gallery-photos">
    <ul id="photo-list">
        <li>
            <p>Please wait...</p>
            <img alt="" src="/Content/Assets/Images/Common/indicator.gif" />
        </li>
    </ul>
    <% if (!string.IsNullOrEmpty(instagramUsername))
       { %>
    <div id="gallery-more">
        <a href="http://instagram.com/<%= instagramUsername %>/" target="_blank">
            <img src="/Content/Assets/Images/common/instagram.png" alt="" /><div>&nbsp;view all photos</div>
        </a>
    </div>
    <% } %>
</div>
