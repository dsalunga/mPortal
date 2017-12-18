<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmtpAnalyzer.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.SmtpAnalyzer" %>
<%@ Register Src="../../../Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc1" %>
<div>
    <strong>SMTP Host Configuration</strong>
</div>
<table width="100%">
    <tr>
        <td style="width: 200px">Host:
        </td>
        <td>
            <asp:TextBox ID="txtHost" runat="server" Columns="50">localhost</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Port:
        </td>
        <td>
            <asp:TextBox ID="txtPort" runat="server" Columns="10">25</asp:TextBox>
            <asp:CheckBox ID="chkEnableSSL" runat="server" CssClass="aspnet-checkbox" Text="Use SSL" Checked="False" />
        </td>
    </tr>
    <tr>
        <td>Username:
        </td>
        <td>
            <asp:TextBox ID="txtUsername" runat="server" Columns="40"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Password:
        </td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" Columns="40" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <br />
            <strong>E-mail Content</strong>
        </td>
    </tr>
    <tr>
        <td>From:
        </td>
        <td>
            <asp:TextBox ID="txtFrom" runat="server" Columns="50">email-sender@localhost</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>To:
        </td>
        <td>
            <asp:TextBox ID="txtTo" runat="server" Columns="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Subject:
        </td>
        <td>
            <asp:TextBox ID="txtSubject" runat="server" Columns="70" TextMode="SingleLine">This is a test e-mail</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Message:
        </td>
        <td>
            <uc1:TextEditor ID="txtMessage" runat="server"
                Text="This is a test &lt;h1 style=&quot;color: green&quot;&gt;e-mail message.&lt;/h1&gt;"
                EditorToolbarSet="Basic" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkHtml" runat="server" CssClass="aspnet-checkbox" Text="Send in HTML format" Checked="True" />
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdSend" runat="server" Text="Send Message" OnClick="cmdSend_Click"
            CssClass="btn btn-default" />
    </div>
</div>
<br />
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>