<%@ Page Language="C#" AutoEventWireup="true" Inherits="_CMS_TextEditor"
    ValidateRequest="false" Codebehind="TextEditor.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Text File Editor</title>
    <link rel="Stylesheet" type="text/css" href="Style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <h1>
            Text File Editor</h1>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="padding-right: 5px">
            <tr>
                <td class="Header">
                    <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="35" Wrap="False"
                        Font-Names="Courier New,Arial,Verdana" Font-Size="10pt" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="ControlBox">
                    <asp:Button ID="cmdUpdate" runat="server" Text="Update" Width="85px" OnClick="cmdUpdate_Click"
                        Font-Bold="True" Height="30px" />
                    <asp:Button ID="cmdCancel" runat="server" Text="Cancel" Width="85px" OnClick="cmdCancel_Click"
                        Height="30px" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
