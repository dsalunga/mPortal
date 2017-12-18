<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParameterSetSelector.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.ParameterSetSelector" %>
<asp:DropDownList ID="cboParameterSet" runat="server" AppendDataBoundItems="true"
    DataSourceID="ObjectDataSource1" DataTextField="Name" DataValueField="Id">
    <asp:ListItem Selected="True" Text="# SELECT #" Value="-1"></asp:ListItem>
</asp:DropDownList>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.Controls.ParameterSetSelector"></asp:ObjectDataSource>
