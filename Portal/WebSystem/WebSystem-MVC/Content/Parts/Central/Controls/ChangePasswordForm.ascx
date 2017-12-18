<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Central.Controls.ChangePasswordForm"
    CodeBehind="ChangePasswordForm.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework.Utilities" %>
<% if (!Request.IsSecureConnection)
   { %>
<script src="<%=WebUtil.Version("~/content/assets/scripts/security/prng4.min.js")%>" type="text/javascript"></script>
<script src="<%=WebUtil.Version("~/content/assets/scripts/security/rng.min.js")%>" type="text/javascript"></script>
<script src="<%=WebUtil.Version("~/content/assets/scripts/security/jsbn.min.js")%>" type="text/javascript"></script>
<script src="<%=WebUtil.Version("~/content/assets/scripts/security/rsa.min.js")%>" type="text/javascript"></script>
<% } %>
<script type="text/javascript">
    
    function performPostSubmitTask() {
        $("#panelUpdateNote").css("display", "none");
        $("#panelButtons").css("display", "none");

        $("#panelAlert").css("background-color", "#fff");
        $("#panelAlert").html("<img src='/Content/Assets/Images/roller.gif' />&nbsp;Please wait...");
    }

    <% if (!Request.IsSecureConnection)
       { %>
    function prepareAndSubmit() {
        var password = $('#txtPassword').val();
        var newPassword = $('#txtNewPassword').val();
        var confirmNewPassword = $('#txtConfirmNewPassword').val();

        if ((newPassword != "" || password == undefined) && newPassword == confirmNewPassword) {
            var pkey = $('#hKey').val().split(',');

            var rsa = new RSAKey();
            var ticks = (new Date()).valueOf();

            rsa.setPublic(pkey[1], pkey[0]);

            if (password != undefined && password != "") {
                $('#txtPassword').val(rsa.encrypt(password));
            }

            var encNewPwd = rsa.encrypt(newPassword);

            $('#txtNewPassword').val(encNewPwd);
            $('#txtConfirmNewPassword').val(encNewPwd);

            $("#hKey").val(ticks);

            performPostSubmitTask();
        }

        return true;
    }

    <% }
       else
       { %>
    function prepareAndSubmit() { performPostSubmitTask(); return true; }
    <% } %>

</script>

<input type="hidden" id="hKey" value="<%= LoginSecurity.GetLoginKey() %>" />
<table width="100%">
    <tr id="trPassword" runat="server">
        <td style="width: 130px">Current Password:
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                ErrorMessage="Current Password" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtPassword" CssClass="input" AutoCompleteType="Disabled" runat="server" Columns="35" TextMode="Password" ClientIDMode="Static" Text=""></asp:TextBox>
            <asp:HiddenField ID="hiddenUserName" Value="-1" runat="server" ClientIDMode="Static" />
        </td>
    </tr>
    <tr id="trSeparator" runat="server">
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 130px; vertical-align: top">New Password:
            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                ErrorMessage="New Password" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtNewPassword" CssClass="input" runat="server" ClientIDMode="Static" AutoCompleteType="Disabled" Columns="35" TextMode="Password" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top">Confirm Password:
            <asp:RequiredFieldValidator ID="rfvConfirmNewPassword" runat="server" ControlToValidate="txtConfirmNewPassword"
                ErrorMessage="Confirm New Password" ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvConfirmNewPassword" runat="server" ErrorMessage="Confirm New Password"
                ControlToCompare="txtNewPassword" CssClass="input" ControlToValidate="txtConfirmNewPassword" ForeColor="Red">*</asp:CompareValidator>
        </td>
        <td>
            <asp:TextBox ID="txtConfirmNewPassword" CssClass="input" ClientIDMode="Static" AutoCompleteType="Disabled" runat="server" Columns="35" TextMode="Password" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr runat="server" id="panelRequireNewPwd">
        <td></td>
        <td>
            <asp:CheckBox ID="chkRequireNewPwd" CssClass="aspnet-checkbox" Text="User must change password at next logon"
                runat="server" />
        </td>
    </tr>
    <tr>
        <td valign="top">&nbsp;
        </td>
        <td>
            <span id="labelMsg" runat="server" enableviewstate="false" style="color: Red"></span>
        </td>
    </tr>
    <%--<tr>
        <td style="width: 151px">
            Security Question:
            <asp:RequiredFieldValidator ID="rfvSecurityQuestion" runat="server" ErrorMessage="Security Question"
                ControlToValidate="txtSecurityQuestion" Enabled="False">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtSecurityQuestion" runat="server" Columns="50" 
                Enabled="False"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 151px">
            Security Answer:
            <asp:RequiredFieldValidator ID="rfvSecurityAnswer" runat="server" ErrorMessage="Security Answer"
                ControlToValidate="txtSecurityAnswer" Enabled="False">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtSecurityAnswer" runat="server" Columns="20" Enabled="False"></asp:TextBox>
        </td>
    </tr>--%>
</table>
