<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigOpen.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Content.ConfigOpen" %>
<%@ Import Namespace="WCMS.Framework" %>
<table>
    <tr>
        <td class="min-bottom-margin" style="padding-top: 10px">
            <asp:Label runat="server" ID="lblContent" AssociatedControlID="txtContentId">Load 
            existing content:</asp:Label>
            <asp:TextBox ID="txtContentId" runat="server" CssClass="input"></asp:TextBox>
            <input id="cmdBrowse" onclick="ShowObjectBrowser();" class="btn btn-default btn-sm" type="button" value="Browse..." />
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="cmdOpenContent" runat="server" CssClass="btn btn-primary" Text="Load Content" OnClick="cmdOpenContent_Click" />
            <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                OnClick="cmdCancel_Click" />
        </td>
    </tr>
</table>

<script type="text/javascript">
    function ShowObjectBrowser() {
        window.returnValue = { "param": WCMS.Dom.Get("<% =txtContentId.ClientID %>") };
        WCMS.Dom.Show("/Content/Windows/ObjectBrowser.aspx?ObjectId=<% =WebObjects.WebContent %>");
    }
</script>

