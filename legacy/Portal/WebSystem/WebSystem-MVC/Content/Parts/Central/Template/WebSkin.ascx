<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSkin.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Template.WebSkinController" %>
<table style="border: 0px">
    <tr>
        <td>Name:<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Template:&nbsp;
        </td>
        <td>
            <asp:DropDownList ID="cboTemplates" DataTextField="Name" DataValueField="Id" runat="server"
                AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value="-1"># None #</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Rank:
        </td>
        <td>
            <asp:TextBox ID="txtRank" Text="0" runat="server" Columns="5"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkSetDefault" CssClass="aspnet-checkbox" Text="Set as Default Theme" runat="server" />
            <br />
            <br />
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
