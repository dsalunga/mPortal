<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GetAccess.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.Account.ForgotPasswordV2" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<div class="system-forgot-pwd">
    <input type="hidden" id="hUserId" runat="server" value="-1" />
    <input type="hidden" id="hEmail" runat="server" value="" />
    <input type="hidden" id="hDOB" runat="server" value="" />
    <input type="hidden" id="hCID" runat="server" value="" />
    <input type="hidden" id="hLoginUrl" runat="server" value="" />
    <asp:MultiView ID="mvForgotPassword" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewRetrieve" runat="server">
            <style type="text/css">
                .primary-field {
                    margin-bottom: 15px;
                }
            </style>
            <div class="row">
                <div class="col-md-12">
                    Use this page when:
                    <ul>
                        <li>You want to register an account.</li>
                        <li>You cannot login or you want to reset your password.</li>
                        <li>You need access but you don't know what to do.</li>
                    </ul>
                    <br />
                    To begin, enter all the required information, click Submit and follow the next instructions :)
                    <br />
                    <br />
                    <table border="0" style="width: 100%">
                        <%--<tr>
                            <td colspan="2">
                                <h4 class="heading colr">Forgot Your Password?</h4>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">Please enter your Group ID (required) and any one of the optional fields, Date
                                of Membership or E-mail.<br />
                                <br />
                                <div class="alert alert-success">
                                    <strong>NOTE:</strong>&nbsp;A Temporary Password will be sent to your e-mail address and mobile
                            number (if available).
                                </div>
                                <div class="alert alert-warning alert-block">
                                    <button type="button" class="close" data-dismiss="alert">&times;</button>
                                    <strong>FOR Integration Ext USERS:</strong>&nbsp;If want to reset your Integration Ext password, you will need to do it via the Integration Ext website:<br />
                                    <a href="http://one.someorg.org/reg/forgotpass.aspx" target="_blank">Integration Ext - Forgot Password</a>.
                                </div>
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="width: 120px">
                                <asp:Label ID="lblEmailInput" runat="server" AssociatedControlID="txtEmail">E-mail&nbsp;</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" Columns="40" CssClass="input primary-field"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" ForeColor="Red" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Email is required." ToolTip="Email is required." ValidationGroup="PasswordRecovery1"
                                    Enabled="False">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDateOfMembership" runat="server" AssociatedControlID="txtDateOfMembership">Date of Membership&nbsp;</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateOfMembership" ClientIDMode="Static" placeholder="YYYY-MM-DD" CssClass="input primary-field" runat="server" Columns="20"></asp:TextBox>
                                <asp:CalendarExtender ID="txtDateOfMembership_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="txtDateOfMembership" Format="yyyy-MM-dd" DefaultView="Years">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvDateOfMembership" ForeColor="Red" runat="server"
                                    ControlToValidate="txtDateOfMembership" ErrorMessage="Date of Membership is required."
                                    ToolTip="Date of Membership is required." ValidationGroup="PasswordRecovery1" Enabled="False">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblExternalId" runat="server" Style="margin-bottom: 0" AssociatedControlID="txtExternalId">Group ID&nbsp;</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExternalId" ClientIDMode="Static" runat="server" CssClass="input" Columns="20" Style="margin-bottom: 2px;"></asp:TextBox>
                                <div>
                                    <asp:CheckBox ID="chkNoExternalId" ClientIDMode="Static" runat="server" CssClass="aspnet-checkbox text-muted" Checked="false" Text="I don't know my Group ID" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <br />
                                <recaptcha:RecaptchaControl
                                    ID="recaptcha"
                                    runat="server"
                                    PublicKey="REPLACE_WITH_RECAPTCHA_PUBLIC_KEY"
                                    PrivateKey="REPLACE_WITH_RECAPTCHA_PRIVATE_KEY" Theme="clean" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <br />
                                <asp:Button ID="cmdRetrieve" CssClass="btn btn-primary" runat="server" Text="Submit" ValidationGroup="PasswordRecovery1"
                                    Width="85px" OnClick="cmdRetrieve_Click" />&nbsp; <a href="Login/" title="Click here to login"
                                        runat="server" id="linkLogin">Cancel</a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <div class="alert alert-danger col-md-6 col-sm-10" runat="server" id="panelAlert" visible="false" enableviewstate="false">
                                    <asp:Literal ID="lblMessage" runat="server" EnableViewState="False"></asp:Literal>
                                </div>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="The following fields are required:"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="PasswordRecovery1" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <script type="text/javascript">
                $(document).ready(function () {
                    var toggleCID = function () {
                        //console.log('change');
                        if ($('#chkNoExternalId').is(":checked")) {
                            //$('#txtExternalId').attr('disabled', true);
                            //$('#txtExternalId').addClass('text-muted');
                            $('#txtExternalId').addClass('hide');
                        } else {
                            //$('#txtExternalId').attr('disabled', false);
                            //$('#txtExternalId').removeClass('text-muted');
                            $('#txtExternalId').removeClass('hide');
                        }
                    }

                    $('#chkNoExternalId').change(toggleCID);
                    toggleCID();
                });
            </script>
        </asp:View>
        <asp:View ID="viewInformAdmin" runat="server">
            <div class="row">
                <div class="col-md-8">
                    You account is found but it is <strong>not activated</strong>. To resolve this issue, I will ask the administrator to review your account.
                    <br />
                    <br />
                    We will contact you after the review, via your email <strong><span runat="server" id="lblEmailInformAdmin"></span></strong>. If you no longer have access to this email, please provide other means to contact you in the box below.
                    <br />
                    <br />
                    Comment or message to the reviewer:
                    <br />
                    <textarea runat="server" id="txtComments" rows="4" class="col-md-6 input"></textarea>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="cmdInformAdmin" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="cmdInformAdmin_Click" />&nbsp; <a href="#" title="Cancel"
                        runat="server" id="linkInformAdminCancel">Cancel</a>
                </div>
            </div>
        </asp:View>
        <asp:View ID="viewPageNote" runat="server">
            <div runat="server" id="panelPageNote" class="col-md-6">
            </div>
            <br />
            <div>If you have more additional concerns, please <a href="/contact" id="linkPageNoteContact" runat="server">contact us here</a></div>
        </asp:View>
        <asp:View ID="viewPeerApproval" runat="server">
            <div class="col-md-6">
                Intro
                <br />
                Display member info
                <br />
                Warnings
                <br />
                textbox: Tell me how did you know $(name)?
                <br />
                Approval actions
            </div>
        </asp:View>
        <asp:View ID="viewAdminSent" runat="server">
        </asp:View>
        <asp:View ID="viewAdminSentWithSSA" runat="server">
            <div class="col-md-6">
                <span runat="server" id="lblPeerApprovalIntroNew" visible="false">Thank you! Your registration has been submitted. Kindly wait for the Admins to activate your account or seek members approval.</span>
                <span runat="server" id="lblPeerApprovalIntroExisting" visible="false">Thank you! Your request has been sent. Kindly wait for the Admins to activate your account or seek members approval.</span>
                <br />
                There are two (2) ways to activate your account:
                <ol>
                    <li>Wait for the Admins to review and approved your account (you will receive an alert once done), or</li>
                    <li>By Self-Service Approval</li>
                </ol>
                <br />
                How Self-Service Approval works?
                <ul>
                    <li>Find a member who you know personally, if that member has an existing Portal account then he/she is eligible to help you.</li>
                    <li>Copy the Self-Service Approval URL below and share it to that member, tell him that he can help you activate your account by visiting that URL.</li>
                    <li>Once the he visited the URL, he will need to login using his own Portal account then he can proceed to approve your account.</li>
                    <li>Your account needs to be approved by 3 members who you know personally before it becomes fully activated.</li>
                </ul>
                <br />
                <br />
                <br />
                Self-Service Approval URL:<br />
                <strong><a runat="server" id="linkApproval" href="#"></a></strong>
                <br />
                <br />
                Current Approvals: <strong><span runat="server" id="lblApprovalCount">0</span> peer approval(s)</strong>
            </div>
        </asp:View>
        <asp:View ID="viewONEReset" runat="server">
            <div class="row">
                <div class="col-md-6">
                    Your Portal account is linked to your ONE Integration account. You need to perform the password reset via the ONE Integration website.
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12"><a href="http://one.someorg.org/reg/forgotpass.aspx" target="_blank" class="btn btn-primary">Continue ONE Reset</a>&nbsp;<a href="#">Cancel</a></div>
            </div>
        </asp:View>
        <asp:View ID="viewPortalReset" runat="server">
            <div class="row">
                <div class="col-md-6">
                    You are about to perform a password reset of your Portal account.
                    <br />
                    <br />
                    The following will be performed:
                    <ul>
                        <li>Your current password will be reset and a temporary password will be created.</li>
                        <li>The temporary password will be sent to your email: <strong><span runat="server" id="lblEmail"></span></strong></li>
                    </ul>
                    <br />
                    Once you recieved your temporary password, login using that password and change it right away.
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="cmdPerformReset" CssClass="btn btn-primary" runat="server" Text="Continue" OnClick="cmdPerformReset_Click" />&nbsp;<a href="#">Cancel</a>
                </div>
            </div>
        </asp:View>
        <asp:View ID="viewExistingONEConfirm" runat="server">
            <div class="row">
                <div class="col-md-6">
                    You have an existing ONE Integration account. If you can access it, please use the same account to login. If you prefer not to use it, you may register a new account but would be subject for approval. 
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="cmdGoToLogin" CssClass="btn btn-primary" runat="server" Text="Proceed to Login using my ONE account" OnClick="cmdGoToLogin_Click" />&nbsp;<asp:Button ID="cmdRegisterNewAccount" CssClass="btn btn-primary" runat="server" Text="Register new account" OnClick="cmdRegisterNewAccount_Click" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</div>
