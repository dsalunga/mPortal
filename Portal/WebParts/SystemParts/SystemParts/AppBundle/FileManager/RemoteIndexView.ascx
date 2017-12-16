<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RemoteIndexView.ascx.cs" Inherits="WCMS.WebSystem.WebParts.RemoteIndexer.RemoteIndexView" %>
<%@ Register Src="Controls/IndexerBreadcrumb.ascx" TagName="Breadcrumb" TagPrefix="uc1" %>
<div class="wp-RemoteIndex wp-FileManager">
    <uc1:Breadcrumb ID="Breadcrumb1" runat="server" />
    <br />
    <div class="command-list">
        <a href="#" runat="server" id="linkOpen" class="btn btn-default">Open</a>&nbsp;<a runat="server" id="linkDownload" href="#" class="btn btn-default">Force Download</a>
    </div>
    <div class="file-view">
        <br />
        <img alt="File or folder thumbnail" runat="server" id="imgThumbnail" />
        <br />
        <br />
        <h3 id="lblFileName" runat="server">file name goes here</h3>
        <div class="field-line">
            <span class="field-label">Date Modified:</span><strong id="lblDateModified" runat="server"></strong>
        </div>
        <div id="panelSize" runat="server" class="field-line">
            <span class="field-label">Size:</span><strong id="lblSize" runat="server">0kb</strong>
        </div>
        <div class="field-line">
            <span class="field-label">Permalink:</span><strong><a target="_blank" href="#" runat="server"
                id="linkPermalink">&nbsp;</a></strong>
        </div>
        <div class="field-line">
            <span id="lblStatus" runat="server" enableviewstate="false" style="color: Red"></span>
        </div>
    </div>
</div>
