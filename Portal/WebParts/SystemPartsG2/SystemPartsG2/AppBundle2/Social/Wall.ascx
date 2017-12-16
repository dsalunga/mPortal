<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Wall.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Social.WallController" %>
<style type="text/css">
    #panelWall
    {
        font-family: 'lucida grande' , tahoma, verdana, arial, sans-serif;
        font-size: 11px;
    }
    
    #panelWall textarea
    {
        display: inline-block;
        background-color: #fff;
        background-image: none;
        border-style: none;
        resize: none;
        outline: none;
        overflow: hidden;
        font-size: 11px;
    }
    
    #panelNewWallPost
    {
        margin-bottom: 10px;
    }
    
    #panelNewWallPost textarea
    {
        height: 16px;
        width: 98%;
        font-size: 13px;
    }
    
    #panelNewWallPost div.uiPostControlContainer
    {
        text-align: right;
        display: none;
        background-color: #F2F2F2;
        padding: 3px;
        border-top: 1px solid #E6E6E6;
    }
    
    #panelNewWallPost .uiPostControlContainer input[type="submit"]
    {
        padding: 4px;
        margin-left: 10px;
    }
    
    #panelNewWallPost .uiPostControlContainer input[type="checkbox"]
    {
        padding: 2px;
    }
    
    #panelWallUpdates div.postActions
    {
        padding-top: 5px;
    }
    
    #panelWallUpdates div.postHeading
    {
        padding-bottom: 3px;
    }
    
    #panelWallUpdates div.panelWallPost a.postDeleteButton, div.commentItem a.commentDeleteButton
    {
        background-image: url("/Images/Common/fb-close.png");
        background-position: 0 0;
        background-repeat: no-repeat;
        display: block;
        width: 15px;
        height: 14px;
        cursor: pointer;
        visibility: hidden;
    }
    
    div.panelWallPost div.mainWrapper
    {
        padding-left: 58px;
    }
    
    div.panelComments
    {
        background-color: #EDEFF4;
        padding: 5px 5px 14px 5px;
        margin-top: 5px;
        display: none;
    }
    
    a.commentAction
    {
        cursor: pointer;
    }
    
    pre.commentMessage, pre.postMessage
    {
        display: inline;
        font-family: 'lucida grande' , tahoma, verdana, arial, sans-serif;
        font-size: 11px;
    }
</style>
<div id="panelWall" data-wall='{"oid":<%=ObjectId %>,"rid":<%=RecordId %>}'>
    <div id="panelNewWallPost" runat="server" clientidmode="Static" style="border: 1px solid #B4BBCD;">
        <textarea id="txtNewPost" class="newPostText" runat="server" placeholder="Write a message..."></textarea>
        <div class="uiPostControlContainer">
            <input type="checkbox" data-role="none" id="chkSendEmail" runat="server" clientidmode="Static" /><label
                for="chkSendEmail">E-mail</label>&nbsp;
            <input type="checkbox" data-role="none" id="chkSendSMS" runat="server" clientidmode="Static" /><label
                for="chkSendEmail">SMS</label>
            <asp:Button ID="cmdPost" runat="server" Text="Post" Width="65px" OnClick="cmdPost_Click" />
        </div>
    </div>
    <div id="panelWallUpdates" runat="server" clientidmode="Static" style="padding-top: 10px;">
    </div>
</div>
<script type="text/javascript" src="/Content/Parts/Social/js/social.js"></script>
