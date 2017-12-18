<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebOpen.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebOpen" %>
<div class="min-bottom-margin">
    <asp:Label runat="server" ID="lblObject" AssociatedControlID="txtId">Load 
            existing:</asp:Label>
    <asp:TextBox ID="txtId" runat="server" CssClass="input"></asp:TextBox>
    <input id="cmdBrowse" onclick="ShowObjectBrowser();" class="btn btn-default btn-sm" type="button" value="Browse..." />
</div>
<div class="control-box">
    <div>
        <asp:Button ID="cmdOpen" CssClass="btn btn-primary" runat="server" Text="Load"
            OnClick="cmdOpen_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="cmdCancel_Click" />
    </div>
</div>
<script type="text/javascript">
    function ShowObjectBrowser() {
        window.returnValue = { "param": WCMS.Dom.Get("<% =txtId.ClientID %>") };
        WCMS.Dom.Show('/content/windows/ObjectBrowser.aspx?ObjectId=<% =Request[WCMS.Framework.WebColumns.ObjectId] %>');
    }
</script>
