<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Common.LoginController" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework.Utilities" %>
<div class="system-login">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="viewLogin" runat="server">
            <asp:HiddenField ID="hLocation" runat="server" ClientIDMode="Static" Value="" />
            <div id="lblError" runat="server" class="alert alert-danger"></div>
            <asp:Literal runat="server" ID="lblAlert" EnableViewState="false"></asp:Literal>
            <% if (!Request.IsSecureConnection)
               { %>
            <script src="<%=WebUtil.Version("~/content/assets/scripts/security/prng4.min.js")%>" type="text/javascript"></script>
            <script src="<%=WebUtil.Version("~/content/assets/scripts/security/rng.min.js")%>" type="text/javascript"></script>
            <script src="<%=WebUtil.Version("~/content/assets/scripts/security/jsbn.min.js")%>" type="text/javascript"></script>
            <script src="<%=WebUtil.Version("~/content/assets/scripts/security/rsa.min.js")%>" type="text/javascript"></script>
            <input type="hidden" id="hKey" value="<%= LoginSecurity.GetLoginKey() %>" />
            <input type="hidden" id="hUserName" runat="server" clientidmode="static" value="" />
            <input type="hidden" id="hPassword" runat="server" clientidmode="static" value="" />
            <% } %>
            <script type="text/javascript">
                <% if (!Request.IsSecureConnection)
                   { %>
                function prepareAndSubmit() {
                    var userName = $('#txtUserName').val();
                    var password = $('#txtPassword').val();
                    if (userName != "") {
                        var pkey = $('#hKey').val().split(',');
                        var rsa = new RSAKey();
                        var ticks = (new Date()).valueOf();

                        rsa.setPublic(pkey[1], pkey[0]);

                        $('#hUserName').val(rsa.encrypt(userName));
                        $('#hPassword').val(rsa.encrypt(password));
                        $("#txtUserName").attr("disabled", true);
                        $("#txtPassword").attr("disabled", true);
                        //$("#txtUserName").val(ticks);
                        //$("#txtPassword").val(ticks);
                        $("#hKey").val("");
                    }
                    return true;
                }
                <% }
                   else
                   { %>
                function prepareAndSubmit() { return true; }
                <% } %>

                var chkToggle = false;
                function showRememberMsg(msg) {
                    if (!chkToggle) {
                        var chk = $('#chkRememberMe');
                        if ($(chk).is(':checked')) {
                            var msgConfirm = confirm(msg);
                            if (!msgConfirm) {
                                chkToggle = true;
                                $(chk).attr('checked', false);
                                chkToggle = false;
                            }
                        }
                    }
                }

                $(document).ready(function () {
                    var txtUserName = $("#txtUserName");
                    if (txtUserName.length > 0) {
                        txtUserName.focus();
                    }
                    /*var chk = $('#chkRememberMe');
                    if ($(chk).length) {
                        $(chk).change(function () {
                            showRememberMsg("WARNING: Do not select this option if you are using a shared or public device. Are you sure you want to remember your password?");
                        });
                    }*/

                    ResolveGeoIp(function (loc) { $('#hLocation').val(loc); });
                });

            </script>
            <table border="0">
                <tr>
                    <td style="text-align: right">Username:<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                        ErrorMessage="Username or E-mail is required." ForeColor="Red" ToolTip="Username or E-mail is required."
                        ValidationGroup="Login1">*</asp:RequiredFieldValidator>&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" CssClass="input" ClientIDMode="Static" runat="server" Text="" Columns="30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; padding-top: 10px">Password:<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="Password is required." ForeColor="Red" ToolTip="Password is required."
                        ValidationGroup="Login1">*</asp:RequiredFieldValidator>&nbsp;
                    </td>
                    <td style="padding-top: 10px">
                        <asp:TextBox ID="txtPassword" CssClass="input" ClientIDMode="Static" runat="server" TextMode="Password" Text="" Columns="30"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="panelOtp" visible="false">
                    <td style="text-align: right">Send OTP Via
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblOtp" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">E-mail</asp:ListItem>
                            <asp:ListItem Value="1">SMS</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr runat="server" id="panelRememberMe" visible="true">
                    <td></td>
                    <td>
                        <div class="checkbox" style="margin-top: 0; padding-left: 0">
                            <asp:CheckBox ID="chkRememberMe" CssClass="aspnet-checkbox" runat="server" ClientIDMode="Static" Text="Keep me logged in" />
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="cmdLogin" Width="80px" CssClass="btn btn-primary" data-loading-text="Logging in..." runat="server" Text="Login"
                            ValidationGroup="Login1" OnClick="cmdLogin_Click" OnClientClick="prepareAndSubmit();" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <br />
                        <div>
                            Forgot your password? <a id="linkForgot" runat="server" href="./?Mode=Forgot"
                                class="BlueLink" title="Click here to recover your password">Click here</a>
                        </div>
                    </td>
                </tr>
                <tr runat="server" id="panelSignUpLink" clientidmode="static" visible="false">
                    <td></td>
                    <td>
                        <hr style="height: 1px; margin-top: 5px; margin-bottom: 5px" />
                        <div>
                            Don't have a login ID? <a id="linkSignup" runat="server" href="#" class="BlueLink"
                                title="Click here to Register">Register Now</a>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="viewOtpVerification" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 160px; text-align: right">
                        <asp:Label ID="Label1" runat="server" AssociatedControlID="txtOtpCode">One-time PIN:&nbsp;&nbsp;</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOtpCode" runat="server" Columns="30"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPIN" ForeColor="Red" runat="server" ControlToValidate="txtOtpCode"
                            ErrorMessage="OTP PIN is required." ToolTip="OTP PIN required.">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="cmdNewOtpPin" CausesValidation="false" runat="server" OnClick="cmdNewOtpPin_Click">Request New PIN</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <div style="margin-top: 10px">
                            <asp:Button ID="cmdOtpSubmit" CssClass="btn btn-primary" runat="server" Text="Submit"
                                OnClick="cmdOtpSubmit_Click" /><asp:Button ID="cmdOtpCancel" runat="server" Text="Cancel"
                                    CssClass="btn btn-default" CausesValidation="False" OnClick="cmdOtpCancel_Click" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <br />
                        <div style="margin-right: 20px">
                            NOTE: Your OTP PIN has been sent to your <span id="lblOtpType" runat="server"></span>
                            . It will expire after <span id="lblOtpExpiry" runat="server"></span>.
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="color: red">
                        <br />
                        <asp:Literal ID="lblOtpMsg" runat="server" EnableViewState="False"></asp:Literal>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="viewForgot" runat="server">
            <asp:MultiView ID="mvForgotPassword" runat="server" ActiveViewIndex="1">
                <asp:View ID="viewRetrieveSuccess" runat="server">
                    <table border="0" cellpadding="0">
                        <tr>
                            <td>Thank you! Your password has been sent to your email,&nbsp;<strong><span id="lblEmail"
                                runat="server"></span></strong>
                                <br />
                                <br />
                                <a href="Login/" title="Click here to login">Click here to login</a>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="viewRetrieve" runat="server">
                    <table border="0" cellpadding="1">
                        <tr>
                            <td colspan="2" class="Header">Forgot Your Password?<br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">Enter your Username or Email to receive your password.<br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtRetrieveUserName">Username or Email:&nbsp;</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRetrieveUserName" CssClass="form-control input-sm" runat="server" Columns="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="txtRetrieveUserName" ErrorMessage="Username or Email is required."
                                    ToolTip="Username or Email is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: red">
                                <asp:Literal ID="lblRetrieveFailed" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <asp:Button ID="cmdRetrieve" runat="server" Text="Submit" ValidationGroup="PasswordRecovery1"
                                    CssClass="btn btn-primary" OnClick="cmdRetrieve_Click" />&nbsp; <a href="Login/"
                                        title="Click here to login">Click here to login</a>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </asp:View>
        <asp:View ID="viewActivation" runat="server">
            <asp:MultiView ID="MultiView2" runat="server">
                <asp:View ID="viewSuccess" runat="server">
                    Thank you! Your account has been successfully activated.
                    <br />
                    <a runat="server" id="linkLogin" href="Login/" title="Click here to login">Click
                        here to login</a>
                </asp:View>
                <asp:View ID="viewNoNeed" runat="server">
                    Your account has been activated already.
                    <br />
                    <br />
                    <a runat="server" id="linkLogin2" href="Login/" title="Click here to login">Click
                        here to login</a>
                </asp:View>
                <asp:View ID="viewFailed" runat="server">
                    Sorry!
                    <br />
                    <br />
                    <a href="Login/" runat="server" id="linkLogin3" title="Click here to login">Click
                        here to login</a>
                </asp:View>
            </asp:MultiView>
        </asp:View>
    </asp:MultiView>
</div>