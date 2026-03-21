<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.Registration.Login" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<link href="../../Styles/WebInterface.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Registration.css" rel="stylesheet" type="text/css" />
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td><img alt="" src="../../images/box/bluebox_top_left.png" /></td>
        <td style="background-image: url('../../images/box/bluebox_top_mid.png'); width: 12px"></td>
        <td><img alt="" src="../../images/box/bluebox_top_right.png" /></td>
    </tr>
    <tr>
        <td style="background-image: url('../../images/box/bluebox_mid_left.png'); width: 15px"></td>
        <td style="background-color: #F4F9FD; text-align: left">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" style="height: 8px" class="titletext">Login Authentication</td>
                </tr>
                <tr>
                    <td align="left" style="height: 8px"></td>
                </tr>
                <tr>
                    <td style="padding-left: 4px; text-align: left" class="columntdnowrap">User Name:</td>
                </tr>
                <tr>
                    <td style="height: 4px"></td>
                </tr>
                <tr>
                    <td style="padding-left: 4px; text-align: left">
                        <asp:TextBox ID="txtUserName" runat="server" Columns="28" CssClass="input" MaxLength="100" ValidationGroup="login"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" Display="None" ErrorMessage="Please enter your email as your username" SetFocusOnError="True" ValidationGroup="login"></asp:RequiredFieldValidator>
                        <act:ValidatorCalloutExtender ID="vceUserName" runat="server" TargetControlID="rfvUserName"></act:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                   <td style="height: 4px"></td>
                </tr>
                <tr>
                    <td style="padding-left: 4px; text-align: left" class="columntdnowrap">Password:</td>
                </tr>
                <tr>
                    <td style="height: 4px"></td>
                </tr>
                <tr>
                    <td style="padding-left: 4px; text-align: left">
                        <asp:TextBox ID="txtPassword" runat="server" Columns="28" CssClass="input" MaxLength="16" TextMode="Password" ValidationGroup="login"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" Display="None" ErrorMessage="Please enter your password." SetFocusOnError="True" ValidationGroup="login"></asp:RequiredFieldValidator>
                        <act:ValidatorCalloutExtender ID="vcePassword" runat="server" TargetControlID="rfvPassword"></act:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td style="height: 8px"></td>
                </tr>
                <tr>
                    <td align="right"><asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" ValidationGroup="login" Width="80px" /></td>
                </tr>
                <tr>
                    <td style="height: 4px"></td>
                </tr>
                <tr>
                    <td align="right" style="font-weight:bold; font-size: 9px" ><asp:LinkButton ID="lnkForgotPass" runat="server" CausesValidation="False" OnClick="lnkForgotPass_Click">Forgot your password?</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="height: 4px"></td>
                </tr>
                <tr>
                    <td align="right" style="font-weight:bold; font-size: 9px">Register here!&nbsp;<asp:LinkButton ID="lnkSignUp" runat="server" CausesValidation="False" OnClick="lnkSignUp_Click">Sign Up</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="height: 4px"></td>
                </tr>
            </table>
        </td>
        <td style="background-image: url('../../images/box/bluebox_mid_right.png'); width: 17px"></td>
    </tr>
    <tr>
        <td><img alt="" src="../../images/box/bluebox_bottom_left.png" /></td>
        <td style="background-image: url('../../images/box/bluebox_bottom_mid.png'); width: 11px"></td>
        <td><img alt="" src="../../images/box/bluebox_bottom_right.png" /></td>
    </tr>
</table>
