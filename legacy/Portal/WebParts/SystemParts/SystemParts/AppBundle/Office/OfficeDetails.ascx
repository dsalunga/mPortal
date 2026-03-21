<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OfficeDetails.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Office.OfficeDetails" %>
<h2>
    Locale Information</h2>
<br />
<h3>
    <strong runat="server" id="lblName">Locale Name here</strong>
</h3>
<img src="/Content/Assets/Images/Common/generic-building.jpg" style="border: solid 2px #aaa; margin: 2px" />
<br />
<br />
<div class="FieldLine">
    <asp:Label CssClass="FieldLabel" ID="Label4" AssociatedControlID="lblEmailAddress"
        runat="server">Email Address:</asp:Label>
    <strong runat="server" id="lblEmailAddress"></strong>
</div>
<div class="FieldLine">
    <asp:Label CssClass="FieldLabel" ID="Label1" AssociatedControlID="txtAddressLine1"
        runat="server">Address:</asp:Label>
    <span id="txtAddressLine1" runat="server" style="font-weight: bold"></span>
</div>
<div class="FieldLine">
    <asp:Label CssClass="FieldLabel" ID="Label6" AssociatedControlID="txtPhoneNumber"
        runat="server">Phone Number:</asp:Label>
    <span id="txtPhoneNumber" runat="server" style="font-weight: bold"></span>
</div>
<div class="FieldLine">
    <asp:Label CssClass="FieldLabel" ID="Label5" AssociatedControlID="txtMobileNumber"
        runat="server">Mobile Number:</asp:Label>
    <span id="txtMobileNumber" runat="server" style="font-weight: bold"></span>
</div>
<div class="FieldLine">
    <asp:Label CssClass="FieldLabel" ID="Label3" AssociatedControlID="txtContactPerson"
        runat="server">Contact Person:</asp:Label>
    <span id="txtContactPerson" runat="server" style="font-weight: bold"></span>
</div>
<br />
<div>
    <asp:Button CssClass="Command" Width="85px" ID="cmdCancel" runat="server" Text="OK"
        OnClick="cmdCancel_Click" Visible="False" />
</div>
<br />
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
<br />
<br />
