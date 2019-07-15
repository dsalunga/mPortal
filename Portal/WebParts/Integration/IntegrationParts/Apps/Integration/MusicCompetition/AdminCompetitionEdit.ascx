<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminCompetitionEdit.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.MusicCompetition.AdminCompetitionEdit" %>
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
        <td>Judges:
        </td>
        <td>
            <asp:TextBox ID="txtJudges" runat="server" CssClass="col-md-4 input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Competition Date:
        </td>
        <td>
            <asp:TextBox ID="txtDate" runat="server" CssClass="col-md-3 input"></asp:TextBox>
        </td>
    </tr>
    <tr runat="server" id="panelBestInterpreter" visible="false">
        <td>Best Interpreter:
        </td>
        <td>
            <asp:DropDownList ID="cboInterpreters" AppendDataBoundItems="true" CssClass="input" runat="server" DataValueField="Id" DataTextField="Name">
                <asp:ListItem Value="-1" Text=""></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr runat="server" id="panelPeoplesChoice" visible="false">
        <td>People's Choice:
        </td>
        <td>
            <asp:DropDownList ID="cboPeoplesChoice" AppendDataBoundItems="true" CssClass="input" runat="server" DataValueField="Id" DataTextField="Name">
                <asp:ListItem Value="-1" Text=""></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkLocked" CssClass="aspnet-checkbox" Checked="true" runat="server" Text="Judging Locked" />
            &nbsp;
            <asp:CheckBox ID="chkVotingLocked" CssClass="aspnet-checkbox" Checked="true" runat="server" Text="Voting Locked" />
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
