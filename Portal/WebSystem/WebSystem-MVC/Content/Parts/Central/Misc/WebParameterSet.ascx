<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebParameterSet.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Misc.WebParameterSetController" %>
<h1 class="central page-header" runat="server" id="lblHeader"></h1>
<table width="100%">
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td width="60">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Name">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName"  CssClass="input" runat="server" Columns="75"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Schema:
                    </td>
                    <td>
                        <asp:DropDownList CssClass="input" ID="cboSchema" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click" CssClass="btn btn-primary" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click" CssClass="btn btn-default"
            CausesValidation="False" />
    </div>
</div>
