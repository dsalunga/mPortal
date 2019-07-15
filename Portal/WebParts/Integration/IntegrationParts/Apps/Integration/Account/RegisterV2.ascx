<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterV2.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.Account.RegistrationV2" %>
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
            <p>Welcome to <span style="color: red;">ADDMUSICMINISTRY.ORG</span>. If you continue to browse and use this website, you are agreeing to comply with and be bound by the following terms and conditions of use, which together with our privacy policy govern <span style="color: red;">MUSIC MINISTRY CORE&rsquo;S</span>&nbsp; relationship with you in relation to this website. If you disagree with any part of these terms and conditions, please do not use our website.</p>
            <p>
                The term &lsquo;ADD MUSIC MINISTRY Portal&rsquo; or &lsquo;us&rsquo; or &lsquo;we&rsquo; refers to the owner of the website whose registered office <span style="color: red">is&nbsp; ADD Convention Center, Sampaloc Street, Apalit, Pampanga, Philippines..</span> The term &lsquo;you&rsquo; refers to the user or viewer of our website.
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
            <p>This website and its content is copyright of ADD Music Ministry Portal - &copy; <span style="color: red">Integration 2012</span>. All rights reserved.</p>
            <p>Any redistribution or reproduction of part or all of the contents in any form is prohibited other than the following:</p>
            <ul>
                <li>You may print or download to a local hard disk extracts for your personal and non-commercial use only</li>
                <li>You may copy the content to individual third parties for their personal use, but only if you acknowledge the website as the source of the material</li>
            </ul>
            <p>You may not, except with our express written permission, distribute or commercially exploit the content. Nor may you transmit it or store it in any other website or other form of electronic retrieval system.</p>
            <p>&nbsp;</p>
        </div>
        <div>
            <asp:Button ID="cmdAgree" CssClass="btn btn-primary" runat="server" Text="I AGREE AND ACCEPT" OnClick="cmdAgree_Click" />&nbsp;<a runat="server" id="linkDisagreeTerms" class="btn btn-default" href="/">Cancel</a>
        </div>
    </asp:View>
    <asp:View ID="viewONEFirstTime" runat="server">
        <h3>Integration Ext Account - First Time Login</h3>
        <p>
            You have chosen your Integration Ext account as your primary login to Music Ministry Portal. In order to continue using your Integration Ext account, you need to link your Integration Ext account to Music Ministry Portal. 
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
        <div>
            Please enter your email<asp:RequiredFieldValidator ID="rfvEnterEmail" runat="server" ErrorMessage="Your e-mail address" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtEnterEmail">*</asp:RequiredFieldValidator>
        </div>
        <div style="padding: 5px 0 15px 0">
            <asp:TextBox ID="txtEnterEmail" placeholder="Your e-mail" runat="server" Columns="45" CssClass="input"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="cmdNext" runat="server" Text="Next"
                CssClass="btn btn-primary" OnClick="cmdEmailNext_Click" />
        </div>
    </asp:View>
    <asp:View ID="viewPortalUser" runat="server">
        <div class="Header">Please enter your Integration Portal Password:</div>
        <br />
        <table>
            <tr>
                <td>E-mail:<asp:RequiredFieldValidator ID="rfvPortalEmail" runat="server" ErrorMessage="E-mail" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtPortalEmail">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtPortalEmail" ReadOnly="true" runat="server" Columns="35"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Password:<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtPassword">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Portal Password"
                        runat="server" Columns="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="cmdPortalFinish" runat="server" Text="Finish"
                        CssClass="btn btn-primary" OnClick="cmdPortalFinish_Click" />&nbsp;<a href="./" class="btn btn-default">Cancel</a>
                </td>
            </tr>
        </table>
    </asp:View>

    <asp:View ID="viewEnterInfo" runat="server">
        <div class="Header">Enter your registration info</div>
        <br />
        <table>
            <%--<tr>
                <td>User Name:<asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="User Name" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtUserName">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" CssClass="input-xlarge" placeholder="Username (Login)" runat="server" Columns="35"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>--%>
            
            <%--<tr>
                <td>&nbsp;</td>
            </tr>--%>
            
            <tr>
                <td>First Name:<asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtFirstName">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-xlarge" placeholder="First Name" Columns="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Last Name:<asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Last Name" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtLastName">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="input-xlarge" placeholder="Last Name" Columns="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Gender:<asp:RequiredFieldValidator ID="rfvGender" runat="server" ErrorMessage="Gender" ControlToValidate="cboGender" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:DropDownList ID="cboGender"
                        runat="server" AppendDataBoundItems="true" CssClass="input">
                        <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="MALE" Value="M"></asp:ListItem>
                        <asp:ListItem Text="FEMALE" Value="F"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>E-mail:
                </td>
                <td>
                    <asp:TextBox ID="txtRegisterEmail" CssClass="input-xlarge" ReadOnly="true" runat="server" Columns="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Mobile Number:
                </td>
                <td>
                    <asp:TextBox ID="txtMobileNumber" placeholder="Mobile Number (Optional)" runat="server"
                        Columns="30" CssClass="input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Country:<asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="Country" ControlToValidate="cboCountries" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:DropDownList DataTextField="CountryName" DataValueField="CountryCode" ID="cboCountries"
                        runat="server" AppendDataBoundItems="true" CssClass="input">
                        <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Group ID:<asp:RequiredFieldValidator ID="rfvExternalID" runat="server" ErrorMessage="Group ID" ControlToValidate="txtExternalID" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtExternalID" runat="server" placeholder="Group ID" Columns="30" CssClass="input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Date of Membership:<asp:RequiredFieldValidator ID="rfvDateOfMembership" runat="server" ErrorMessage="Date of Membership" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtMembershipDate">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtMembershipDate" placeholder="Membership Date (YYYY-MM-DD)" runat="server"
                        ClientIDMode="Static" Columns="30" CssClass="input"></asp:TextBox>
                    <asp:CalendarExtender ID="txtMembershipDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtMembershipDate" Format="yyyy-MM-dd" DefaultView="Years">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>Locale Chapter:<asp:RequiredFieldValidator ID="rfvGroupLocale" runat="server" ErrorMessage="Group Locale" ControlToValidate="txtLocale" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtLocale" TextMode="MultiLine" Rows="3" CssClass="input-xlarge" ToolTip="The Group Locale where are you are currently registered." placeholder="Your Group Locale"
                        runat="server" Columns="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="cmdInfoNext" CssClass="btn btn-primary" runat="server" Text="Next" OnClick="cmdInfoNext_Click" />&nbsp;<a class="btn link-cancel" href="./">Cancel</a>
                </td>
            </tr>
        </table>
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
                    <asp:Button ID="cmdUpload" runat="server" CssClass="btn btn-primary" ClientIDMode="Static" Text="Upload Now" OnClick="cmdUpload_Click" />&nbsp;</span><span id="panelSkip" runat="server" clientidmode="Static"><asp:Button ID="cmdUploadSkip" runat="server" Text="Skip" CssClass="btn btn-inverse" OnClick="cmdUploadSkip_Click" />&nbsp;</span><a class="btn link-cancel" href="./">Cancel</a>
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
                <asp:Button ID="cmdReUpload" CssClass="btn btn-warning" runat="server" Text="Re-Upload" OnClick="cmdReUpload_Click" />&nbsp;<a class="btn link-cancel" href="./">Cancel</a>
            </asp:View>
        </asp:MultiView>

    </asp:View>
    <asp:View ID="viewONELinkFinish" runat="server">
        <div class="alert">Your Integration Ext Account is now linked to your Portal account! From now on, your Portal login will your Integration Ext account.</div>
        <a class="btn btn-primary" href="/Public/Login/">Continue to Login</a>
    </asp:View>
    <asp:View ID="viewFinish" runat="server">
        <div class="alert">Registration Done! Please check your e-mail inbox.</div>
    </asp:View>
    <asp:View ID="viewONEFinish" runat="server">
        <div class="alert">Registration Done! You may now start using your Integration Ext account in Music Ministry Portal by logging in.</div>
        <a class="btn btn-primary" href="/Public/Login/">Continue to Login</a>
    </asp:View>
    <asp:View ID="viewRegisterDisabled" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="alert alert-warning">
                    <div style="padding-bottom: 5px">
                        <strong>Note:</strong> Registration is done via the Integration Ext website. Once you are registered, you may return here and login using your Integration Ext account.
                    </div>
                    <a class="btn btn-warning" href="http://one.someorg.org/reg/reg.aspx">Register @ Integration Ext</a>
                    <br />
                    <br />
                    <div style="padding-bottom: 5px">
                        Are you from locale of Singapore?
                    </div>
                    <a class="btn btn-warning" href="http://portal.someorg.org.sg/public/Register">Register @ SG Portal</a>
                </div>

                <br />
                <div class="alert alert-success">
                    <div style="padding-bottom: 10px">
                        Already have a Integration Ext account? Just log-in.
                    </div>
                    <a class="btn btn-success" href="/Public/Login/">Login</a>
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
