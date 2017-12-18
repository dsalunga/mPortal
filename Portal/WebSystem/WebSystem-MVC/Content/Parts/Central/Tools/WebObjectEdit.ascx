<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebObjectEdit.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.WebObjectEdit" %>
<table border="0">
    <tr>
        <td>Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" ReadOnly="true" CssClass="span4" runat="server" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Identity Column:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIdentityColumn"
            ErrorMessage="Identity Column" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtIdentityColumn" runat="server" Columns="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Cache Type:
        </td>
        <td>
            <asp:DropDownList ID="cboCacheType" runat="server">
                <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>
                <asp:ListItem Value="2">Partial</asp:ListItem>
                <asp:ListItem Value="1">Full</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td valign="top">Data Provider:
        </td>
        <td>
            <asp:TextBox ID="txtDataProviderName" CssClass="span6" runat="server" Columns="75" Rows="3" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top">Type:
        </td>
        <td>
            <asp:TextBox ID="txtTypeName" CssClass="span6" runat="server" Columns="75" Rows="3" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top">Manager:
        </td>
        <td>
            <asp:TextBox ID="txtManagerName" CssClass="span6" runat="server" Columns="75" Rows="3" TextMode="MultiLine"></asp:TextBox>
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
