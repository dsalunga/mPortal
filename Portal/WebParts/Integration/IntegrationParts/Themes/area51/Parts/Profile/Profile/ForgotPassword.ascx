<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Profile.ForgotPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<div class="system-forgot-pwd">
    <asp:MultiView ID="mvForgotPassword" runat="server" ActiveViewIndex="1">
        <asp:View ID="viewPageNote" runat="server">
            <div runat="server" id="panelPageNote" style="text-align: left; width: 450px">
            </div>
        </asp:View>
        <asp:View ID="viewRetrieve" runat="server">
            <table border="0" cellpadding="1" style="width: 450px">
                <tr>
                    <td align="left" colspan="2">
                        <h4 class="heading colr">Forgot Your Password?</h4>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">Please enter your Church ID (required) and any one of the optional fields, Date
                        of Membership or E-mail.<br />
                        <br />
                        <div class="alert alert-success">
                            <strong>NOTE:</strong>&nbsp;A Temporary Password will be sent to your e-mail address and mobile
                    number (if available).
                        </div>
                        <div class="alert alert-warning alert-block">
                            <button type="button" class="close" data-dismiss="alert">&times;</button>
                            <strong>FOR Integration ONE USERS:</strong>&nbsp;If want to reset your Integration ONE password, you will need to do it via the Integration ONE website:<br />
                            <a href="http://one.someorg.org/reg/forgotpass.aspx" target="_blank">Integration ONE - Forgot Password</a>.
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lblExternalId" runat="server" AssociatedControlID="txtExternalId">Church ID:&nbsp;</asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtExternalId" runat="server" CssClass="input" Columns="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvExternalId" ForeColor="Red" runat="server" ControlToValidate="txtExternalId"
                            ErrorMessage="Church ID is required." ToolTip="Church ID is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lblDateOfMembership" runat="server" AssociatedControlID="txtDateOfMembership">Date of Membership:&nbsp;</asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDateOfMembership" placeholder="YYYY-MM-DD" CssClass="input" runat="server" Columns="30"></asp:TextBox>
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
                        <asp:TextBox ID="txtEmail" runat="server" Columns="30" CssClass="input"></asp:TextBox>
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
                        <asp:Button ID="cmdRetrieve" CssClass="btn btn-primary" runat="server" Text="Submit" ValidationGroup="PasswordRecovery1"
                            Width="85px" OnClick="cmdRetrieve_Click" />&nbsp; <a href="Login/" title="Click here to login"
                                runat="server" id="linkLogin">Click here to Login</a>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</div>
