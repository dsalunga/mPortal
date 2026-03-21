<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Contact.AdminInquiriesDetails"
    CodeBehind="AdminInquiriesDetails.ascx.cs" %>
<div class="control-box">
    <div>
        <asp:Button ID="cmdReturn" CssClass="btn btn-default" runat="server" Text="Return" OnClick="cmdReturn_Click"></asp:Button>
    </div>
</div>
<table width="100%" border="0">
    <tr>
        <td width="150">SUBJECT:
        </td>
        <td>
            <asp:Label ID="lblSubject" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>TYPE:
        </td>
        <td>
            <asp:Label ID="lblInquiryType" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td></td>
    </tr>
    <tr>
        <td>NAME:
        </td>
        <td>
            <asp:Label ID="lblName" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>EMAIL:
        </td>
        <td>
            <asp:Label ID="lblEmail" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>ADDRESS 1:
        </td>
        <td>
            <asp:Label ID="lblAddressLine1" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>ADDRESS 2:
        </td>
        <td>
            <asp:Label ID="lblAddressLine2" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>CITY:
        </td>
        <td>
            <asp:Label ID="lblCity" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>COUNTRY:
        </td>
        <td>
            <asp:Label ID="lblCountry" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>STATE:
        </td>
        <td>
            <asp:Label ID="lblState" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>ZIP CODE:
        </td>
        <td>
            <asp:Label ID="lblZipCode" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>CONTACT NO:
        </td>
        <td>
            <asp:Label ID="lblPhone" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>FAX:
        </td>
        <td>
            <asp:Label ID="lblFax" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td></td>
    </tr>
    <tr>
        <td>SENT TO:
        </td>
        <td>
            <asp:Label ID="lblSendTo" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>SENT TO EMAIL:
        </td>
        <td>
            <asp:Label ID="lblSendToEmail" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>DATE &amp; TIME:
        </td>
        <td>
            <asp:Label ID="lblDateTime" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td></td>
    </tr>
    <tr>
        <td>MESSAGE:
        </td>
        <td>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </td>
    </tr>
</table>
