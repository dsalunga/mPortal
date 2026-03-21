<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Contact.ConfigContactDetails"
    CodeBehind="AdminContactDetails.ascx.cs" AutoEventWireup="True" %>
<table>
    <tr>
        <td>
            <table>
                <tr>
                    <td width="110">Contact Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Contact Name">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Columns="90" CssClass="input"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>E-mail:<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="E-mail Address">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Valid e-mail format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            Enabled="False">*</asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Columns="90" CssClass="input"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="110">Subject:<asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                        ErrorMessage="Subject">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubject" runat="server" Columns="90" CssClass="input"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Contact Details:
                    </td>
                    <td>
                        <fckeditorv2:FCKeditor ID="txtDetails" runat="server" Height="400px" ToolbarSet="Basic">
                        </fckeditorv2:FCKeditor>
                    </td>
                </tr>
                <tr>
                    <td>Rank:<asp:RequiredFieldValidator ID="rfvRank" runat="server" ControlToValidate="txtRank"
                        ErrorMessage="Rank">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRank" CssClass="input" runat="server" Width="88px"></asp:TextBox>&nbsp;
                        <asp:CheckBox ID="chkActive" runat="server" CssClass="Command" Text="Active"></asp:CheckBox>
                    </td>
                </tr>
            </table>
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
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
