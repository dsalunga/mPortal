<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminComposerEdit.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.MusicCompetition.AdminComposerEdit" %>
<%@ Register Src="~/Content/Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
<table border="0">
    <tr>
        <td>Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Composer" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" CssClass="col-md-4 input" Columns="60"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Nick Name:
        </td>
        <td>
            <asp:TextBox ID="txtNickName" runat="server" Columns="50" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Locale:
        </td>
        <td>
            <asp:TextBox ID="txtLocale" runat="server" CssClass="col-md-5 input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">Entry:<asp:RequiredFieldValidator ID="rfvEntry" runat="server" ControlToValidate="txtEntry"
            ErrorMessage="Entry" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td style="vertical-align: top">
            <asp:TextBox ID="txtEntry" runat="server" CssClass="col-md-5 input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Work:
        </td>
        <td>
            <asp:TextBox ID="txtWork" runat="server" CssClass="col-md-5 input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Photo File:
        </td>
        <td>
            <asp:TextBox ID="txtPhotoFile" runat="server" CssClass="col-md-3 input" Columns="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">Description:
        </td>
        <td>
            <uc2:TextEditor IsPlainTextDefault="true" EditorToolbarSet="Basic" ID="txtDescription"
                runat="server" />
        </td>
    </tr>
    <tr>
        <td>Competition:
        </td>
        <td>
            <asp:DropDownList ID="cboCompetition" CssClass="input" AppendDataBoundItems="true" runat="server" DataTextField="Name" DataValueField="Id">
                <asp:ListItem Text="- None -" Value="-1"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkActive" CssClass="aspnet-checkbox" Checked="true" runat="server" Text="Active" />
        </td>
    </tr>
</table>
<br />
<br />
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
