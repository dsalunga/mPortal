<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebAddress.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Misc.WebAddressController" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<h1 class="central page-header">
    User Address
</h1>
<table border="0">
    <tr>
        <td>Tag:<asp:RequiredFieldValidator ID="rfvTag" runat="server" ControlToValidate="cboTag"
            ErrorMessage="Tag">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:ComboBox ID="cboTag" runat="server" AutoCompleteMode="SuggestAppend">
                <asp:ListItem>Home</asp:ListItem>
                <asp:ListItem>Work</asp:ListItem>
            </asp:ComboBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Country:
        </td>
        <td>
            <asp:DropDownList DataTextField="CountryName" CssClass="input" DataValueField="CountryCode" ID="cboCountries"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboCountries_SelectedIndexChanged" AppendDataBoundItems="true">
                <asp:ListItem Value="-1" Text=""></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Address Line 1:
        </td>
        <td>
            <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Address Line 2:
        </td>
        <td>
            <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>City/Town:
        </td>
        <td>
            <asp:TextBox ID="txtCityTown" runat="server" CssClass="input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>State/Province:
        </td>
        <td style="padding-bottom: 15px">
            <asp:ComboBox ID="cboStateProvince" DataTextField="StateName" DataValueField="StateCode"
                runat="server" AutoCompleteMode="SuggestAppend">
            </asp:ComboBox>
        </td>
    </tr>
    <tr>
        <td>Zip/Postal Code:
        </td>
        <td>
            <asp:TextBox ID="txtZipCode" runat="server" CssClass="input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Phone Number:
        </td>
        <td>
            <asp:TextBox ID="txtPhoneNumber" CssClass="input" runat="server" Columns="75"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
            OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel"
            OnClick="cmdCancel_Click" CausesValidation="False" />
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
