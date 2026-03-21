<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WCMS.WebSystem.WebParts.Download._Sections_Download_WM_Download_02" Codebehind="PM_Downloads_Edit_02.ascx.cs" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<table border="0">
    <tr>
        <td>
            Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75"></asp:TextBox></td>
    </tr>
    <tr>
        <td valign="top">
            Decription:
        </td>
        <td>
            <fckeditorv2:fckeditor id="txtdescription" runat="server" toolbarset="Banner"></fckeditorv2:fckeditor>
        </td>
    </tr>
    <tr>
        <td>
            File:<asp:RequiredFieldValidator ID="rfvImage" runat="server" ControlToValidate="txtFilename"
                ErrorMessage="Filename">*</asp:RequiredFieldValidator></td>
        <td>
            <asp:TextBox ID="txtFilename" runat="server" Columns="50" />
            <asp:Button ID="cmdUpload" runat="server" Text="Upload..." CausesValidation="False" />
        </td>
    </tr>
    <tr>
        <td>
            Date:
        </td>
        <td>
            <asp:TextBox ID="txtDate" runat="server" Columns="30"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            Rank:
        </td>
        <td>
            <asp:TextBox ID="txtRank" runat="server" Columns="10"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2" align="left" class="control_box">
            <asp:Button ID="cmdUpdate" Width="85px" runat="server" Text="Update" OnClick="cmdUpdate_Click"
                Height="30px" />
            <asp:Button ID="cmdCancel" Width="85px" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
                CausesValidation="False" Height="30px" />
        </td>
    </tr>
</table>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
<asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>