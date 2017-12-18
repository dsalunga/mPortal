<%@ Page Language="c#" Inherits="WCMS.WebSystem.Windows.Upload" CodeBehind="Upload.aspx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://schemas.microsoft.com/intellisense/xhtml-transitional-10">
<head>
    <title>Upload Image</title>
    <link rel="stylesheet" type="text/css" href="<%=WebUtil.Version("~/content/assets/styles/upload.min.css")%>" />
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function FindControl(controlName) {
            return window.opener.document.getElementById(controlName);
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
        <div style="padding: 5px; background-color: #1c6295; color: White; font-size: 14px; text-align: center; font-weight: bold">
            Upload File
        </div>
        <br />
        <div style="text-align: center">
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="viewUpload" runat="server">
                    <table cellpadding="4" cellspacing="4">
                        <tr>
                            <td align="left">
                                <input id="fileToUpload" clientidmode="Static" type="file" name="fileToUpload" runat="server" size="70" />
                                <br />
                                <em>
                                    <asp:CheckBox ID="chkReplace" Text="Replace file if existing" runat="server" /></em>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span>Upload to:</span>
                                <br />
                                <asp:TextBox ID="txtUploadTo" runat="server" Columns="85" /></td>
                        </tr>
                        <tr id="panelUploadBtn">
                            <td align="left">
                                <asp:Button ID="btnUpload" runat="server" Font-Bold="true" Text="Upload" OnClick="btnUpload_Click"
                                    Height="30px" Width="95px" /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" /></td>
                        </tr>
                    </table>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $("#fileToUpload").change(function () {
                                var fileName = $(this).val();
                                $("#panelUploadBtn").css("display", fileName ? "" : "none");
                            });

                            $("#panelUploadBtn").css("display", "none");
                        });
                    </script>
                </asp:View>
                <asp:View ID="viewDone" runat="server">
                    <asp:Label CssClass="Header" ID="lblImageName" Font-Bold="True" runat="server" />
                    <br />
                    <br />
                    <asp:Button ID="cmdSave" runat="server" Width="85px" Text="Close" Height="30px" />
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
