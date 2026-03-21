<%@ Control Language="C#" AutoEventWireup="true" ClassName="WCMS.WebSystem.Apps.MusicCompetition.MCFinalistsV2" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>

<%
    var context = new WebPartContext(this);
    var element = context.Element;
    var instagramId = element.GetParameterValue("InstagramId");

    if (string.IsNullOrEmpty(instagramId))
        instagramId = "38126460";
%>

<style type="text/css">
    #gallery-photos {
        padding: 15px;
        margin: 0;
        /* height: 210px; */
        /* width: 280px; */
        overflow: hidden;
        border-top: 1px solid #FCFCFC;
        background-color: rgb(234, 234, 234);
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
                    /*
        max-width: 60px;
        height: 60px;*/
                }
</style>

<script type="text/javascript" src="/content/plugins/embedagram/jquery-embedagram.pack.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#photo-list').embedagram({
            instagram_id: <%= instagramId %>,
            success: function () { $('#photo-list'); },
            limit: 5,
            link_type: 'img',
            thumb_width: 150
        });
    });
</script>

<div id="gallery-photos">
    <ul id="photo-list"></ul>
</div>
