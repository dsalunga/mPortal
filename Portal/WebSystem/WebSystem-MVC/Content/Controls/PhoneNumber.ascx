<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhoneNumber.ascx.cs"
    Inherits="WCMS.WebSystem.Controls.PhoneNumber" %>
<asp:HiddenField ID="hCountryCode" runat="server" Value="" />
<asp:HiddenField ID="hMaxDigits" runat="server" Value="" />
<span runat="server" id="lblPhoneCode" class="websys phone-number country-code">
</span>&nbsp;<asp:TextBox ID="txtPhoneNumber" Text="+00" runat="server" CssClass="websys phone-number number-input"></asp:TextBox>