<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebOffice.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Misc.WebOfficeController" %>
<h1 class="central page-header" runat="server" id="lblHeader"></h1>
<table width="100%">
    <tr>
        <td width="115">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Address:
        </td>
        <td>
            <asp:TextBox ID="txtAddressLine1" runat="server" TextMode="MultiLine" Rows="3" Columns="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Phone Number:
        </td>
        <td>
            <asp:TextBox ID="txtPhoneNumber" runat="server" Columns="20"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Mobile Number:
        </td>
        <td>
            <asp:TextBox ID="txtMobileNumber" runat="server" Columns="20"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Email Address:
        </td>
        <td>
            <asp:TextBox ID="txtEmailAddress" runat="server" Columns="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Contact Person:
        </td>
        <td>
            <asp:TextBox ID="txtContactPerson" runat="server" Columns="40"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Parent:
        </td>
        <td>
            <asp:TextBox ID="txtParent" runat="server" Columns="20"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" />
    </div>
</div>
