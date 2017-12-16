<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigFileManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.FileManager.ConfigFileManager" %>
<div class="wp-FileManager" style="margin-top: 15px;">
    <div>
        Total:&nbsp;<strong><span id="lblStorageSize" runat="server"></span></strong>
    </div>
    <div>
        Used:&nbsp;<strong><span id="lblStorageUsage" runat="server"></span></strong></div>
    <div>
        Free:&nbsp;<strong><span id="lblStorageFree" runat="server"></span></strong></div>
    <br />
    <%--<div style="height: 50px; width: 500px; background-color: Green">
    </div>--%>
    <br />
</div>
