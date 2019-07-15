<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterSG.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.Account.Registration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<link href="<%=WebUtil.Version("~/content/assets/styles/registration.min.css")%>" rel="stylesheet" type="text/css" />
<asp:HiddenField runat="server" ID="hLocaleFilter" Value="" />
<div class="system-reg" style="text-align: left">
    <!-- Group information -->
    <table cellpadding="1" cellspacing="1" style="float: left">
        <tr>
            <td align="left" colspan="2">
                <h4 class="heading colr" runat="server" id="lblTitle">
                    Member Registration</h4>
            </td>
        </tr>
        <tr>
            <td style="width: 110px; text-align: left">
                Group ID
            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtExternalIDNo" CssClass="input" runat="server" Columns="20" ValidationGroup="retrieve"
                    TabIndex="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvExternalIDNo" runat="server" ControlToValidate="txtExternalIDNo"
                    Display="None" ErrorMessage="Please enter your Group ID" ValidationGroup="retrieve"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="vceExternalIDNo" runat="server" TargetControlID="rfvExternalIDNo">
                </asp:ValidatorCalloutExtender>
                &nbsp;<a href="" runat="server" style="color: Gray; text-decoration: underline" id="linkNoExternal"
                    title="Click here to register if you don't have an External account." tabindex="2">No
                    External account?</a> <a href="" runat="server" style="color: Gray; text-decoration: underline"
                        id="linkWithExternal" title="Click here to register if you have an existing External account."
                        tabindex="3">Already registered to External?</a>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                Date of Membership
            </td>
            <td style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="padding-right: 1px;">
                            <asp:TextBox ID="txtMembershipDate" CssClass="span2 input" placeholder="YYYY-MM-DD" runat="server" MaxLength="10" ValidationGroup="retrieve"
                                TabIndex="4" ClientIDMode="Static"></asp:TextBox>
                            <asp:CalendarExtender ID="txtMembershipDate_CalendarExtender" runat="server" Enabled="True"
                                TargetControlID="txtMembershipDate" Format="yyyy-MM-dd" DefaultView="Years">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvMembershipDate" runat="server" ControlToValidate="txtMembershipDate"
                                Display="None" ErrorMessage="Please enter your Date of Membership" SetFocusOnError="True"
                                ValidationGroup="retrieve"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Image ID="imgMembershipDate" runat="server" ImageUrl="~/Content/Assets/Images/calendar.gif" ImageAlign="AbsMiddle"
                                TabIndex="5" ClientIDMode="Static" Visible="false" />&nbsp;<asp:Button ID="btnRetrieve"
                                    runat="server" OnClick="btnRetrieve_Click" CssClass="btn btn-default btn-inline btn-sm" Text="Retrieve" ValidationGroup="retrieve"
                                    TabIndex="6" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" id="rowLocale" visible="false">
            <td style="text-align: left">
                Group Locale
            </td>
            <td>
                <asp:TextBox ID="txtLocale" runat="server" CssClass="span5 input" Columns="52" ValidationGroup="register"
                    TabIndex="7"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLocale" runat="server" ControlToValidate="txtLocale"
                    Display="None" ErrorMessage="Please enter your Group Locale" ValidationGroup="register"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="vceLocale" runat="server" TargetControlID="rfvLocale">
                </asp:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr runat="server" id="panelMemberInfoHr">
            <td colspan="2">
                <hr />
            </td>
        </tr>
    </table>
    <!-- Name and picture -->
    <div runat="server" id="panelMemberInfo" style="float: left">
        <table cellpadding="1" cellspacing="1">
            <tr>
                <td style="width: 110px; text-align: left">
                    First Name
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" ClientIDMode="Static" runat="server" CssClass="input" Columns="31"
                        ValidationGroup="register" ReadOnly="True" Enabled="False" TabIndex="8"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                        Display="None" ErrorMessage="Please enter your First Name" ValidationGroup="register"></asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="rfvFirstName_ValidatorCalloutExtender" runat="server"
                        TargetControlID="rfvFirstName">
                    </asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr id="rowMiddleName" runat="server" visible="false">
                <td style="text-align: left">
                    Middle Name
                </td>
                <td>
                    <asp:TextBox ID="txtMiddleName" ClientIDMode="Static" runat="server" Columns="31"
                        ValidationGroup="register" CssClass="input" TabIndex="9"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMiddleName" runat="server" ControlToValidate="txtMiddleName"
                        Display="None" ErrorMessage="Please enter your Middle Name" ValidationGroup="register"></asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vceMiddleName" runat="server" TargetControlID="rfvMiddleName">
                    </asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Last Name
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" CssClass="input" ClientIDMode="Static" runat="server" Columns="31" ReadOnly="True"
                        ValidationGroup="register" Enabled="False" TabIndex="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                        Display="None" ErrorMessage="Please enter your Last Name" ValidationGroup="register"></asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvLastName">
                    </asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    E-mail
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" CssClass="input" runat="server" Columns="31" ValidationGroup="register"
                        Enabled="False" ReadOnly="true" TabIndex="11"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        Display="None" ErrorMessage="Please enter your E-mail." ValidationGroup="register"></asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vceEmail" runat="server" TargetControlID="rfvEmail">
                    </asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr id="rowContact" runat="server">
                <td style="text-align: left">
                    Mobile Number
                </td>
                <td>
                    <asp:TextBox ID="txtMobileNumber" CssClass="span2 input" ReadOnly="true" runat="server" Columns="25" ValidationGroup="register"
                        TabIndex="12"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator ID="rfvContactNumber" runat="server" ControlToValidate="txtMobileNumber"
                    Display="None" ErrorMessage="Please enter your Mobile Number" ValidationGroup="register"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="vceContactNumber" runat="server" TargetControlID="rfvContactNumber">
                </asp:ValidatorCalloutExtender>--%>
                </td>
            </tr>
            <tr id="rowAddress" runat="server" visible="false">
                <td style="text-align: left">
                    Home Address
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="span5 input" Columns="52" ValidationGroup="register"
                        TabIndex="13"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvHomeAddress" runat="server" ControlToValidate="txtAddress"
                        Display="None" ErrorMessage="Please enter your Home Address" ValidationGroup="register"></asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vceHomeAddress" runat="server" TargetControlID="rfvHomeAddress">
                    </asp:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
        </table>
        <asp:Image ID="imgPhoto" runat="server" CssClass="PhotoFrame" BorderWidth="1px" Style="float: left; margin-left: 10px;" />
        <!-- Account information -->
        <table cellpadding="1" cellspacing="1">
            <%--<tr>
                <td style="width: 110px; text-align: left">
                    User Name
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" ClientIDMode="Static" runat="server" Columns="31" ValidationGroup="register"
                        Enabled="false" TabIndex="14" CssClass="input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                        Display="None" ErrorMessage="Please enter a User Name" ValidationGroup="register"></asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vceUsername" runat="server" TargetControlID="rfvUsername">
                    </asp:ValidatorCalloutExtender>
                </td>
            </tr>--%>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnRegister" CssClass="btn btn-primary" runat="server" Text="Register" OnClick="btnRegister_Click"
                        ValidationGroup="register" Width="85px" Enabled="false" CommandArgument="Register"
                        UseSubmitBehavior="False" TabIndex="17" />
                    <br />
                    <br />
                    NOTE: Your Temporary Password will be sent to your e-mail address and SMS number,
                    please make sure your contact information is correct.
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="width: 110px; text-align: left">
            </td>
            <td>
                <br />
                <div style="font-weight: bold">
                    Already have an account? <a runat="server" id="linkLogin" href="/Singapore/Public/Login/">
                        Click here to Login</a></div>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField runat="server" ID="hRedirect" Value="0" />
<asp:Panel ID="pnlMsg1" runat="server" CssClass="ModalBkg" HorizontalAlign="Center"
    Style="display: none; padding: 10px">
    <asp:Panel ID="pnlMessage" runat="server" ForeColor="Black" Width="100%">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color: white"
            class="gridborder">
            <tr>
                <td class="height8">
                </td>
            </tr>
            <tr>
                <td class="txtmodal">
                    <asp:Label ID="lblMsg1" runat="server" CssClass="txtbold"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="cmdMsgOK" OnClick="cmdMsgOK_Click" runat="server" CausesValidation="False"
                        ImageUrl="~/Content/Assets/Images/common/okmsg.gif" />
                </td>
            </tr>
            <tr>
                <td class="height8">
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<asp:Button ID="btnModal1" runat="server" Style="display: none" Text="Modal Popup" />
<asp:ModalPopupExtender ID="mpeMsg1" runat="server" BackgroundCssClass="ModalBkg"
    CancelControlID="btnModal1" OkControlID="btnModal1" PopupControlID="pnlMsg1"
    TargetControlID="btnModal1">
</asp:ModalPopupExtender>
<%--<script type="text/javascript">
    $(document).ready(function () {
        var txtFirstName = $("#txtFirstName");
        var txtLastName = $("#txtLastName");

        if (txtFirstName.length > 0 && txtLastName.length > 0 && !txtFirstName.attr("readonly") && !txtLastName.attr("readonly")) {
            txtFirstName.change(BuildUserName);
            txtLastName.change(BuildUserName);
        }
    });

    function BuildUserName() {
        var txtFirstName = $("#txtFirstName");
        var txtLastName = $("#txtLastName");
        var txtUserName = $("#txtUserName");

        if (txtFirstName.length > 0 && txtLastName.length > 0 && txtUserName.length > 0 && !txtFirstName.attr("readonly") && !txtLastName.attr("readonly")) {
            var firstName = txtFirstName.val().Trim().GetFirstWord();
            if (firstName != "") {
                var lastName = txtLastName.val().Trim().GetFirstWord();
                if (lastName != "") {
                    var userName = firstName + "." + lastName;
                    txtUserName.val(userName);
                }
            }
        }
    }
</script>--%>
