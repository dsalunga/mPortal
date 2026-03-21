<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeBehind="WebOpen.aspx.cs" Inherits="WCMS.WebSystem.Admin.WebOpen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblObject" AssociatedControlID="txtId">Load 
            existing:</asp:Label>
                <asp:TextBox ID="txtId" runat="server"></asp:TextBox>
                <input id="cmdBrowse" onclick="ShowObjectBrowser();" type="button" value="Browse..." />
            </td>
        </tr>
        <tr>
            <td class="ControlBox">
                <asp:Button ID="cmdOpen" Width="75px" CssClass="Command" runat="server" Text="Load"
                    OnClick="cmdOpen_Click" />
                <asp:Button ID="cmdCancel" CssClass="Command" runat="server" Text="Cancel" OnClick="cmdCancel_Click" />
            </td>
        </tr>
    </table>
    
    <script type="text/javascript">
        function ShowObjectBrowser() {
            window.returnValue = { "param": WCMS.Dom.Get("<% =txtId.ClientID %>") };
            WCMS.Dom.Show('/Admin/Windows/ObjectBrowser.aspx?ObjectId=<% =Request[WCMS.Framework.WebColumns.ObjectId] %>');
        }
    </script>

</asp:Content>
