<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Central.Controls.UserGroupsFormController"
    CodeBehind="UserRolesForm.ascx.cs" %>
<asp:HiddenField ID="hidUserId" Value="" runat="server" />
<asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="ObjectDataSource1"
    DataTextField="Name" DataValueField="Id" meta:resourcekey="CheckBoxList1Resource1">
</asp:CheckBoxList>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.UserGroupsFormController"></asp:ObjectDataSource>
