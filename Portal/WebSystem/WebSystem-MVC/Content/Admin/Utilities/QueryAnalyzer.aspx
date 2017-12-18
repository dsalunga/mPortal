<%@ Page Language="c#" Inherits="WCMS.Web.QueryAnalyzer" ValidateRequest="False" Codebehind="QueryAnalyzer.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Query Analyzer -
        <% =DateTime.Now %>
    </title>
    <link href="QueryAnalyzer.aspx.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="frmSection" method="post" runat="server">
        <table width="100%">
            <tr>
                <td>Connection String:
                    <%--<asp:TextBox ID="txtCode" runat="server" Columns="10"></asp:TextBox>--%>&nbsp;
                    <asp:TextBox ID="txtQS" runat="server" Columns="50"></asp:TextBox>
                    <asp:CheckBox ID="chkCustom" runat="server" Text="Custom" /></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtQuery" runat="server" Rows="25" Width="100%" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="cmdExecute" runat="server" Width="80px" Text="Execute" OnClick="cmdExecute_Click">
                    </asp:Button><asp:RadioButton ID="chkQuery" runat="server" Text="Query" GroupName="radioOptions"
                        Checked="True" />&nbsp;<asp:RadioButton ID="chkDownload" runat="server" Text="Download"
                            GroupName="radioOptions" />&nbsp;
                    <asp:RadioButton ID="chkUpload" runat="server" Text="Upload" GroupName="radioOptions" />
                    <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
                    <asp:CheckBox ID="chkSchema" runat="server" Text="Schema" /></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    <asp:PlaceHolder ID="phGrid" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
