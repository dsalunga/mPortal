<%@ Page Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.Windows._CMS_Utils_StyleBuilder" CodeBehind="StyleBuilder.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Style Builder</title>
    <%--<link href="/content/assets/styles/style.css" rel="stylesheet" type="text/css" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0">
                <tr>
                    <td valign="top">
                        <asp:Button ID="cmdFont" runat="server" Text="Font" Width="120px" OnClick="cmdFont_Click" /><br />
                        <asp:Button ID="cmdBackground" runat="server" Text="Background" Width="120px" OnClick="cmdBackground_Click" /><br />
                        <asp:Button ID="cmdText" runat="server" Text="Text" Width="120px" OnClick="cmdText_Click" /><br />
                        <asp:Button ID="cmdEdges" runat="server" Text="Edges" Width="120px" OnClick="cmdEdges_Click" /><br />
                        <asp:Button ID="cmdOther" runat="server" Text="Other" Width="120px" OnClick="cmdOther_Click" /><br />
                    </td>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="viewFont" runat="server">
                                <table border="0" width="300">
                                    <tr>
                                        <td>Font Family:&nbsp;<asp:TextBox ID="txtFontFamily" runat="server" Columns="30"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Color:&nbsp;<asp:TextBox ID="txtColor" runat="server" Columns="15"></asp:TextBox>&nbsp;&nbsp;Size:&nbsp;<asp:TextBox
                                            ID="txtFontSize" runat="server" Columns="10"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Effects:&nbsp;<asp:CheckBoxList ID="chkEffects" runat="server">
                                            <asp:ListItem>None</asp:ListItem>
                                            <asp:ListItem>Underline</asp:ListItem>
                                        </asp:CheckBoxList><br />
                                            Bold:
                                            <asp:DropDownList ID="cboBold" runat="server">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>Normal</asp:ListItem>
                                                <asp:ListItem>Bold</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="viewBackground" runat="server">
                            </asp:View>
                            <asp:View ID="viewText" runat="server">
                            </asp:View>
                            <asp:View ID="viewEdges" runat="server">
                            </asp:View>
                            <asp:View ID="viewOther" runat="server">
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
