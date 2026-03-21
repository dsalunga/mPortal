<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListPart.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Test.ListPart" %>
<h1>
    List</h1>
<asp:Button ID="cmdFindDetails" runat="server" OnClick="cmdFindDetails_Click" Text="Find Details" />
<br />
<br />
<asp:Button ID="cmdFindCategory" runat="server" Text="Find Category" 
    onclick="cmdFindCategory_Click" />
