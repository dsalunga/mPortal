<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveInFolder.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.SaveInFolder" %>
<div class="min-bottom-margin">
    <asp:TextBox ID="txtFolder" runat="server" CssClass="input" Columns="50"></asp:TextBox>&nbsp;<input id="cmdBrowse" onclick="ShowObjectBrowser();" class="btn btn-default btn-sm" type="button" value="Browse..." />
</div>
<script type="text/javascript">
    function ShowObjectBrowser() {
        window.returnValue = { "param": WCMS.Dom.Get("<% =txtFolder.ClientID %>") };
        WCMS.Dom.Show("/Content/Windows/ObjectBrowser.aspx?Action=Save&ObjectId=<% =ObjectId %>");
    }
</script>

