<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Central._CMS_Controls_UserProfileForm"
    CodeBehind="UserProfileForm.ascx.cs" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Register Src="../../../Controls/FullNamePicker.ascx" TagName="FullNamePicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../../Controls/ComboDatePicker.ascx" TagName="ComboDatePicker"
    TagPrefix="uc1" %>
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="viewProfile" runat="server">
        <table>
            <tr>
                <td width="120" valign="top">Full Name
                </td>
                <td>
                    <uc2:FullNamePicker ID="FullNamePicker1" runat="server" />
                </td>
            </tr>
            <tr runat="server" id="panelUsername">
                <td width="120">Username<asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                    ErrorMessage="Username" SetFocusOnError="True" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtUsername" CssClass="input" ClientIDMode="Static" runat="server" Columns="45" ReadOnly="False"></asp:TextBox>
                    <asp:HiddenField ID="hiddenUserId" Value="" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;
                </td>
            </tr>
            <tr>
                <td>Email Address<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Email Address" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Valid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ForeColor="Red">*</asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" CssClass="input" runat="server" Columns="35"></asp:TextBox>
                </td>
            </tr>
            <tr runat="server" id="panelEmail2nd">
                <td>Email (Secondary)
                </td>
                <td>
                    <asp:TextBox ID="txtEmail2" CssClass="input" runat="server" Columns="35"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Mobile Number
                </td>
                <td>
                    <asp:TextBox ID="txtMobileNumber" CssClass="input" runat="server" Columns="25"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Phone Number
                </td>
                <td>
                    <asp:TextBox ID="txtPhoneNumber" CssClass="input" runat="server" Columns="25"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td>
                    Birthdate:
                </td>
                <td>
                    <uc1:ComboDatePicker ID="ComboDatePicker1" runat="server"></uc1:ComboDatePicker>
                </td>
            </tr>
            --%>
            <tr>
                <td colspan="2">&nbsp;
                </td>
            </tr>
            <tr runat="server" id="panelSuffix">
                <td>Suffix
                </td>
                <td>
                    <asp:TextBox ID="txtSuffix" placeholder="Jr./Sr./II" runat="server" CssClass="input" Columns="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Gender
                </td>
                <td>
                    <asp:DropDownList ID="cboGender" runat="server" CssClass="input">
                        <asp:ListItem Selected="True" Text="" Value="U"></asp:ListItem>
                        <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="panelStatusText">
                <td style="vertical-align: top">Status Text
                </td>
                <td style="vertical-align: top">
                    <asp:TextBox ID="txtStatusText" runat="server" CssClass="input" Rows="4" Columns="35" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;
                </td>
            </tr>
            <tr runat="server" id="panelPhotoPath">
                <td>Photo Path
                </td>
                <td>
                    <asp:TextBox ID="txtPhotoPath" Columns="35" runat="server" CssClass="input" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr runat="server" visible="false">
                <td>Profile Picture:
                </td>
                <td align="center" style="background-color: #f9f7f7; padding: 2px">
                    <asp:ImageButton ID="imagePhoto" runat="server" OnClick="imagePhoto_Click" CausesValidation="False"
                        ToolTip="click image to change" />
                    <div style="font-style: italic; padding: 3px">
                        Click image to change
                    </div>
                    <asp:HiddenField ID="hiddenImageFilename" Value="silho.gif" runat="server" />
                </td>
            </tr>
            <tr runat="server" id="panelActive">
                <td>Status</td>
                <td>
                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="input">
                        <asp:ListItem Value="0">Pending Approval</asp:ListItem>
                        <asp:ListItem Value="1">Active</asp:ListItem>
                        <asp:ListItem Value="-1">Draft</asp:ListItem>
                        <asp:ListItem Value="2">Disabled</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="panelProvider">
                <td>Provider</td>
                <td>
                    <asp:DropDownList ID="cboUserProviders" runat="server" CssClass="input" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Id">
                        <asp:ListItem Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="panelLockOut" visible="false">
                <td></td>
                <td>
                    <asp:CheckBox ID="chkResetLockout" CssClass="aspnet-checkbox" Text="Reset Locked-out State"
                        runat="server" /></td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewPicture" runat="server">
        <div style="font-weight: bold">
            Your picture
        </div>
        <br />
        <table width="300">
            <tr>
                <td align="center">
                    <asp:Image ID="imagePreview" runat="server" ToolTip="Preview" />
                    <br />
                    <strong>Preview</strong>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" /><br />
                    <asp:Button ID="cmdUpload" runat="server" Text="Upload Now" OnClick="cmdUpload_Click"
                        Height="25px" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblUploadMessage" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="cmdImageOK" runat="server" Text="OK" OnClick="cmdImageOK_Click" Width="85px"
                        Font-Bold="True" Height="30px" />
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>
<script type="text/javascript">
    $(document).ready(function () {
        var txtUserName = $("#txtUsername");
        var txtLastName = $("#txtLastName");

        txtUserName.focus(BuildUserName);
        //txtUserName.blur(BuildUserName);
        txtLastName.blur(BuildUserName);

        /*
        var txtFirstName = $("#txtFirstName");
        

        if (txtFirstName.length > 0 && txtLastName.length > 0 && !txtFirstName.attr("readonly") && !txtLastName.attr("readonly")) {
        txtFirstName.change(BuildUserName);
        txtLastName.change(BuildUserName);
        }
        */
    });

    function BuildUserName() {
        var txtFirstName = $("#txtFirstName");
        var txtLastName = $("#txtLastName");
        var txtUserName = $("#txtUsername");

        if (txtUserName.length > 0 && txtUserName.val().length == 0 && txtFirstName.length > 0 && txtLastName.length > 0 && !txtFirstName.attr("readonly") && !txtLastName.attr("readonly")) {
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
</script>
