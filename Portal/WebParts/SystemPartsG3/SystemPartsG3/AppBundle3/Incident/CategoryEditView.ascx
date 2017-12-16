<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryEditView.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Incident.CategoryEditView" %>
<table border="0">
    <tr>
        <td>Name:<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" CssClass="input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">Description:
        </td>
        <td style="vertical-align: top">
            <asp:TextBox ID="txtDescription" runat="server" CssClass="input" MaxLength="999" Columns="75" Rows="4"
                TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Group:
        </td>
        <td>
            <asp:DropDownList ID="cboGroup" DataTextField="Name" CssClass="input" DataValueField="Id" runat="server"
                AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value="-1"># None #</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Rank:
        </td>
        <td>
            <asp:TextBox ID="txtRank" Text="0" runat="server" CssClass="input" Columns="5"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Instance:
        </td>
        <td>
            <asp:DropDownList ID="cboInstance" DataTextField="Name" CssClass="input" DataValueField="Id" runat="server"
                AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value="-1"># None #</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
            OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn" runat="server" Text="Cancel"
            OnClick="cmdCancel_Click" CausesValidation="False" />
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
