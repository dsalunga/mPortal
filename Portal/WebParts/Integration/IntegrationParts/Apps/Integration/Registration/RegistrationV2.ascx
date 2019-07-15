<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationV2.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.RegistrationV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="The following fields are required:" ShowMessageBox="True" ShowSummary="False" />
<asp:HiddenField ID="hUserId" runat="server" Value="-1" />
<input type="hidden" id="hHasTerms" runat="server" value="" />
<input type="hidden" id="hLinkExternalRegister" runat="server" value="" />
<div runat="server" id="lblStatus" class="alert alert-danger" enableviewstate="false"></div>
<div runat="server" id="lblAlert" class="alert alert-warning" enableviewstate="false"></div>
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewTerms" runat="server">
        <div runat="server" id="panelTerms">
            <h2>ADD Music Ministry Portal</h2>
            <p><strong>Usage Terms and Conditions</strong></p>
            <p>Welcome to <span style="color: red;">Integration Portal</span>. If you continue to browse and use this website, you are agreeing to comply with and be bound by the following terms and conditions of use, which together with our privacy policy govern <span style="color: red;">ADDCIT Asia Oceania&rsquo;s</span>&nbsp; relationship with you in relation to this website. If you disagree with any part of these terms and conditions, please do not use our website.</p>
            <p>
                The term &lsquo;Integration Portal&rsquo; or &lsquo;us&rsquo; or &lsquo;we&rsquo; refers to the owner of the website whose registered office <span style="color: red">is&nbsp; Integration Singapore.</span> The term &lsquo;you&rsquo; refers to the user or viewer of our website.
            </p>
            <p>
                The use of this website is subject to the following terms of use:
            </p>
            <ul>
                <li>The content of the pages of this website is for your general information and use only. It is subject to change without notice.</li>
                <li>This website uses cookies to monitor browsing preferences. Cookies must be enabled.</li>
                <li>Neither we nor any third parties provide any warranty or guarantee as to the accuracy, timeliness, performance, completeness or suitability of the information and materials found or offered on this website for any particular purpose. You acknowledge that such information and materials may contain inaccuracies or errors and we expressly exclude liability for any such inaccuracies or errors to the fullest extent permitted by law.</li>
                <li>Your use of any information or materials on this website is entirely at your own risk, for which we shall not be liable. It shall be your own responsibility to ensure that any products, services or information available through this website meet your specific requirements.</li>
                <li>This website contains material which is owned by or licensed to us. This material includes, but is not limited to, the design, layout, look, appearance and graphics<b><u>. Reproduction is prohibited</u></b> other than in accordance with the copyright notice, which forms part of these terms and conditions.</li>
                <li>All trademarks reproduced in this website, which are not the property of, or licensed to the operator, are acknowledged on the website.</li>
                <li>Unauthorised use of this website may give rise to a claim for damages and/or be a criminal offence.</li>
                <li>From time to time, this website may also include links to other websites. These links are provided for your convenience to provide further information. They do not signify that we endorse the website(s). We have no responsibility for the content of the linked website(s).</li>
                <li>Your use of this website and any dispute arising out of such use of the website is subject to the laws of Singapore.</li>
            </ul>
            <p>&nbsp;</p>
            <p><strong>Copyright Notice</strong></p>
            <p>This website and its content is copyright of Integration Portal - &copy; <span style="color: red">Integration 2012</span>. All rights reserved.</p>
            <p>Any redistribution or reproduction of part or all of the contents in any form is prohibited other than the following:</p>
            <ul>
                <li>You may print or download to a local hard disk extracts for your personal and non-commercial use only</li>
                <li>You may copy the content to individual third parties for their personal use, but only if you acknowledge the website as the source of the material</li>
            </ul>
            <p>You may not, except with our express written permission, distribute or commercially exploit the content. Nor may you transmit it or store it in any other website or other form of electronic retrieval system.</p>
            <p>&nbsp;</p>
        </div>
        <div>
            <asp:Button ID="cmdAgree" CssClass="btn btn-primary" runat="server" Text="I AGREE AND ACCEPT" OnClick="cmdAgree_Click" />&nbsp;<a runat="server" id="linkDisagreeTerms" class="btn btn-default" href="/public/Login">Cancel</a>
        </div>
    </asp:View>
    <asp:View ID="viewONEFirstTime" runat="server">
        <h3>Integration Ext Account - First Time Login</h3>
        <p>
            You have chosen your Integration Ext account as your primary login to Integration Portal. In order to continue using your Integration Ext account, you need to link your Integration Ext account to Integration Portal. 
        </p>
        <p>Please click the continue button to confirm.</p>
        <br />
        <div>
            <button id="cmdONESetupContinue" class="btn btn-primary" runat="server" onserverclick="cmdONESetupContinue_ServerClick">Continue using my Integration Ext account</button>
            &nbsp;
            <button id="cmdONESetupCancel" class="btn btn-default" runat="server" onserverclick="cmdONESetupCancel_ServerClick">Cancel</button>
        </div>
    </asp:View>
    <asp:View ID="viewEnterEmail" runat="server">
        <div class="form-group">
            <label for="exampleInputEmail1">Please enter your e-mail:<asp:RequiredFieldValidator ID="rfvEnterEmail" runat="server" ErrorMessage="Your e-mail address" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtEnterEmail">*</asp:RequiredFieldValidator></label>
            <div class="row">
                <div class="col-sm-6 col-md-5">
                    <asp:TextBox ID="txtEnterEmail" CssClass="form-control" placeholder="Your e-mail" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div>
            <asp:Button ID="cmdNext" runat="server" Text="Next"
                CssClass="btn btn-primary" OnClick="cmdEmailNext_Click" />&nbsp;<a class="btn btn-default" href="/public/Login">Cancel</a>
        </div>
    </asp:View>
    <asp:View ID="viewPortalUser" runat="server">
        <div class="Header">You have an existing Integration Portal account. If you are experiencing issues, please contact the Portal admin via the contact form. <%--Please enter your password:--%></div>
        <br />
        <table>
            <%--<tr>
                <td>E-mail:<asp:RequiredFieldValidator ID="rfvPortalEmail" runat="server" ErrorMessage="E-mail" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtPortalEmail">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtPortalEmail" CssClass="input" ReadOnly="true" runat="server" Columns="35"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Password:<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtPassword">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" CssClass="input" TextMode="Password" placeholder="Portal Password"
                        runat="server" Columns="50"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <%--<td></td>--%>
                <td>
                    <%--<asp:Button ID="cmdPortalFinish" runat="server" Text="Finish"
                        CssClass="btn btn-primary" OnClick="cmdPortalFinish_Click" />&nbsp;--%><a href="/public/Login" class="btn btn-primary">Login Now</a>&nbsp;<a class="btn btn-danger" href="/public/Forgot">Forgot Password</a>
                </td>
            </tr>
        </table>
    </asp:View>

    <asp:View ID="viewEnterInfo" runat="server">
        <div class="Header">Please enter your details.</div>
        <br />
        <div class="form-horizontal">
            <div class="form-group">
                <label for="txtFirstName" class="col-sm-3 col-md-2 control-label">
                    First Name:<asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtFirstName">*</asp:RequiredFieldValidator></label>
                <div class="col-sm-8 col-md-6">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="txtLastName" class="col-sm-3 col-md-2 control-label">Last Name:<asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Last Name" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtLastName">*</asp:RequiredFieldValidator></label>
                <div class="col-sm-8 col-md-6">
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="cboGender" class="col-sm-3 col-md-2 control-label">Gender:<asp:RequiredFieldValidator ID="rfvGender" runat="server" ErrorMessage="Gender" ControlToValidate="cboGender" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator></label>
                <div class="col-sm-8 col-md-6">
                    <asp:DropDownList ID="cboGender" CssClass="form-control"
                        runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    &nbsp;
                </div>
            </div>
            <div class="form-group">
                <label for="txtRegisterEmail" class="col-sm-3 col-md-2 control-label">E-mail:</label>
                <div class="col-sm-8 col-md-6">
                    <asp:TextBox ID="txtRegisterEmail" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="txtMobileNumber" class="col-sm-3 col-md-2 control-label">Mobile Number:</label>
                <div class="col-sm-8 col-md-6">
                    <asp:TextBox ID="txtMobileNumber" CssClass="form-control" placeholder="Mobile Number (Optional)" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    &nbsp;
                </div>
            </div>
            <div class="form-group">
                <label for="txtExternalID" class="col-sm-3 col-md-2 control-label">Group ID:<asp:RequiredFieldValidator ID="rfvExternalID" runat="server" ErrorMessage="Group ID" ControlToValidate="txtExternalID" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator></label>
                <div class="col-sm-8 col-md-6">
                    <asp:TextBox ID="txtExternalID" CssClass="form-control" runat="server" placeholder="Group ID"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="txtMembershipDate" class="col-sm-3 col-md-2 control-label">Date of Membership:<asp:RequiredFieldValidator ID="rfvDateOfMembership" runat="server" ErrorMessage="Date of Membership" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtMembershipDate">*</asp:RequiredFieldValidator></label>
                <div class="col-sm-8 col-md-6">
                    <asp:TextBox ID="txtMembershipDate" CssClass="form-control" placeholder="Membership Date (YYYY-MM-DD)" runat="server"
                        ClientIDMode="Static"></asp:TextBox>
                    <asp:CalendarExtender ID="txtMembershipDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtMembershipDate" Format="yyyy-MM-dd" DefaultView="Years">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="form-group">
                <label for="txtLocale" class="col-sm-3 col-md-2 control-label">Group Locale:<asp:RequiredFieldValidator ID="rfvGroupLocale" runat="server" ErrorMessage="Group Locale" ControlToValidate="txtLocale" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator></label>
                <div class="col-sm-8 col-md-6">
                    <asp:TextBox ID="txtLocale" TextMode="MultiLine" Rows="3" CssClass="form-control" ToolTip="The Group Locale where are you are currently registered." placeholder="Group Locale"
                        runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="cboCountries" class="col-sm-3 col-md-2 control-label">Locale Country:<asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="Locale Country" ControlToValidate="cboCountries" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator></label>
                <div class="col-sm-8 col-md-6">
                    <asp:DropDownList DataTextField="CountryName" CssClass="form-control" DataValueField="CountryCode" ID="cboCountries"
                        runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-3 col-md-offset-2 col-sm-10 col-md-6">
                    <asp:Button ID="cmdInfoNext" CssClass="btn btn-primary" runat="server" Text="Next" OnClick="cmdInfoNext_Click" />&nbsp;<a class="btn btn-default link-cancel" href="/public/Login">Cancel</a>
                </div>
            </div>
        </div>
    </asp:View>

    <asp:View ID="viewPhotoUploader" runat="server">
        <asp:HiddenField ID="hExtension" runat="server" Value="" />
        <asp:MultiView ID="MultiViewPhotoUpload" runat="server" ActiveViewIndex="0">
            <asp:View ID="viewUpload" runat="server">
                <div class="Header">Upload Your Profile Photo</div>
                <br />
                <asp:FileUpload ID="photoUpload" ClientIDMode="Static" runat="server" />
                <br />
                <br />
                <span id="uploadPanel">
                    <asp:Button ID="cmdUpload" runat="server" CssClass="btn btn-primary" ClientIDMode="Static" Text="Upload Now" OnClick="cmdUpload_Click" />&nbsp;</span><span id="panelSkip" runat="server" clientidmode="Static"><asp:Button ID="cmdUploadSkip" runat="server" Text="Skip" CssClass="btn btn-inverse" OnClick="cmdUploadSkip_Click" />&nbsp;</span><a class="btn btn-default link-cancel" href="/public/Login">Cancel</a>
                <script type="text/javascript">
                    $(document).ready(function () {
                        $("#photoUpload").change(function () {
                            var fileName = $(this).val();
                            $("#uploadPanel").css("display", fileName ? "" : "none");
                        });

                        $("#uploadPanel").css("display", "none");
                    });
                </script>
            </asp:View>
            <asp:View ID="viewPreview" runat="server">
                <div class="Header">Photo Preview</div>
                <br />
                <asp:Image ID="imagePreview" runat="server" />
                <br />
                <br />
                <asp:Button ID="cmdRegister" runat="server" Text="Accept & Finish"
                    CssClass="btn btn-primary" OnClick="cmdRegister_Click" />&nbsp;
                <asp:Button ID="cmdReUpload" CssClass="btn btn-warning" runat="server" Text="Re-Upload" OnClick="cmdReUpload_Click" />&nbsp;<a class="btn btn-default link-cancel" href="/public/Login">Cancel</a>
            </asp:View>
        </asp:MultiView>

    </asp:View>
    <asp:View ID="viewONELinkFinish" runat="server">
        <div class="alert alert-success">Your Integration Ext Account is now linked to your Integration Portal account! From now on, your Integration Portal login will your Integration Ext account.</div>
        <a class="btn btn-primary" href="/public/Login/">Continue to Login</a>
    </asp:View>
    <asp:View ID="viewFinish" runat="server">
        <div class="alert alert-success">
            <strong>Registration successful!</strong>
            <br />
            You have completed the registration, however, your account is sent for review.
            <br />
            <br />
            You will recieve an e-mail with your temporary password once your account is approved. Please check your e-mail inbox regularly.
        </div>
        <a class="btn btn-primary" href="/public/Login/">Done</a>
    </asp:View>
    <asp:View ID="viewONEFinish" runat="server">
        <div class="alert alert-success">Registration successful! You may now start using your Integration Ext account in Integration Portal by logging in.</div>
        <a class="btn btn-primary" href="/public/Login/">Continue to Login</a>
    </asp:View>
    <asp:View ID="viewRegisterOptions" runat="server">
        <div class="row">
            <div class="col-md-8 col-sm-12">
                <div class="alert alert-info">
                    <div style="padding-bottom: 10px">
                        <strong>Do you have an Integration Ext account?</strong>
                        <br />
                        If yes, just log-in!
                    </div>
                    <a class="btn btn-info" href="/public/Login/">Login Now</a>
                </div>
                <br />

                <div class="alert alert-danger">
                    <div style="padding-bottom: 5px">
                        <strong>Forgot your password?</strong>
                        <br />
                        Click here if you have an existing account but you forgot your password.
                    </div>
                    <a class="btn btn-danger" href="/public/Forgot">Forgot Password</a>
                </div>
                <br />

                <div class="alert alert-success">
                    <%--<div style="padding-bottom: 5px">
                        <strong>Are you from locale of Singapore?</strong>
                    </div>
                    <a class="btn btn-success" href="http://portal.someorg.org.sg/public/Register">Register @ SG Portal</a>
                    <br />
                    <br />
                    <div style="padding-bottom: 5px">
                        <strong>Want to register an Integration Ext account?</strong>
                        <br />
                        Integration Ext gives you additional access to other MCGI.org services. Once you are registered, you may return here and login using your Integration Ext account.
                    </div>
                    <a class="btn btn-success" href="http://one.someorg.org/reg/reg.aspx">Register @ Integration Ext</a>
                    <br />
                    <br />--%>
                    <div style="padding-bottom: 5px">
                        <%--<strong>Don't know which option to choose?</strong>--%>
                        <strong>Need a new Account?</strong>
                        <br />
                        Click here to register :)
                    </div>
                    <a class="btn btn-success" runat="server" id="linkRegisterLocal" href="http://one.someorg.org/reg/reg.aspx">Register @ Integration Portal</a>
                </div>
            </div>
        </div>
    </asp:View>

</asp:MultiView>

<script type="text/javascript">
    $(document).ready(function () {
        $(".link-cancel").click(function () {
            return confirm("This will discard all data you have entered, are you sure you want to cancel?");
        });
    });
</script>
