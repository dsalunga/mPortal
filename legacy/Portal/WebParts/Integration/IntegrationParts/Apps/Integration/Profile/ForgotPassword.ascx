<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.ForgotPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<asp:MultiView ID="mvForgotPassword" runat="server" ActiveViewIndex="1">
    <asp:View ID="viewPageNote" runat="server">
        <div runat="server" id="panelPageNote" style="text-align: left; width: 450px">
        </div>
    </asp:View>
    <asp:View ID="viewRetrieve" runat="server">
        <table border="0" cellpadding="1" style="width: 450px">
            <%--<tr>
                <td align="left" colspan="2" class="Header">Forgot Your Password?<br />
                    <br />
                </td>
            </tr>--%>
            <tr>
                <td style="text-align: left" colspan="2">
                    <div class="alert alert-warning">
                        <button type="button" class="close" data-dismiss="alert">&times;</button>
                        <strong>For Integration Ext Users:</strong>&nbsp;If want to reset your password, you will need to do it via the Integration Ext website.
                        <div style="margin-top: 10px">
                            <a class="btn btn-warning" href="http://one.someorg.org/reg/forgotpass.aspx" target="_blank">Reset via Integration Ext</a>
                        </div>
                    </div>
                    <div>
                        Please enter your Group ID (required) and any one of the optional fields, Date
                    of Membership or E-mail. A Temporary Password will be sent to your e-mail address and mobile
                    number (if available).
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblExternalId" runat="server" AssociatedControlID="txtExternalId">Group ID:&nbsp;</asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtExternalId" runat="server" CssClass="input" Columns="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvExternalId" ForeColor="Red" runat="server" ControlToValidate="txtExternalId"
                        ErrorMessage="Group ID is required." ToolTip="Group ID is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 5px 0 5px 0">
                    <hr style="margin-top: 10px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblDateOfMembership" runat="server" AssociatedControlID="txtDateOfMembership">Date of Membership:&nbsp;</asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDateOfMembership" CssClass="input" placeholder="YYYY-MM-DD" runat="server" Columns="30"></asp:TextBox>
                    <asp:CalendarExtender ID="txtDateOfMembership_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtDateOfMembership" Format="yyyy-MM-dd" DefaultView="Years">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfvDateOfMembership" ForeColor="Red" runat="server"
                        ControlToValidate="txtDateOfMembership" ErrorMessage="Date of Membership is required."
                        ToolTip="Date of Membership is required." ValidationGroup="PasswordRecovery1" Enabled="False">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">-- OR --
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblEmailInput" runat="server" AssociatedControlID="txtEmail">E-mail:&nbsp;</asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="input" Columns="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" ForeColor="Red" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Email is required." ToolTip="Email is required." ValidationGroup="PasswordRecovery1"
                        Enabled="False">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="color: red">
                    <br />
                    <asp:Literal ID="lblMessage" runat="server" EnableViewState="False"></asp:Literal>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="The following fields are required:"
                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="PasswordRecovery1" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <br />
                    <asp:Button ID="cmdRetrieve" runat="server" Text="Submit" ValidationGroup="PasswordRecovery1"
                        CssClass="btn btn-primary" OnClick="cmdRetrieve_Click" />&nbsp; <a href="./Login.aspx"
                            title="Cancel" runat="server" id="linkLogin">Cancel</a>
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>