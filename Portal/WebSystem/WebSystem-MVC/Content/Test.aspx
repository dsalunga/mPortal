<%@ Page Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.Test" CodeBehind="Test.aspx.cs"
    ValidateRequest="false" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Content/Controls/CKEditor.ascx" TagName="CKEditor" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Test Page</title>
    <link href="<%=WebUtil.Version("~/content/assets/styles/websystem.min.css")%>" type="text/css" rel="stylesheet" />
    <link href="<%=WebUtil.Version("~/content/assets/styles/websystem.admin.min.css")%>" type="text/css" rel="stylesheet" />
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery-ui.custom.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.core.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/datetimepicker.aspx.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/common.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/websystem.admin.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/plugins/ckeditor/ckeditor.min.js")%>" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="cmdTest" runat="server" OnClick="cmdTest_Click" Text="Test" />
            <img style="border: solid 1px black; margin: 3px 3px 3px 3px" id="imgThumb" runat="server" />
            <asp:Button ID="cmdSerialize" runat="server" Text="Serialize" OnClick="cmdSerialize_Click" />
            <asp:Button ID="cmdDeserialize" runat="server" Text="Deserialize" OnClick="cmdDeserialize_Click"
                Style="height: 26px" />
            <asp:Button ID="cmdWCFCacheTest" runat="server" Text="WCF Cache VS Direct SQL"
                Style="height: 26px" OnClick="cmdWCFCacheTest_Click" />
            <br />
            <asp:Button ID="cmdPerf01" runat="server" Text="Filter Dictionary - Where + ToList VS Loop + List"
                OnClick="cmdPerf01_Click" /><br />
            <asp:Button ID="cmdPerf02" runat="server"
                Text="Combine Items: List VS IEnumerable" OnClick="cmdPerf02_Click" />
            <asp:Button ID="cmdXmlObject" runat="server" OnClick="cmdXmlObject_Click"
                Text="XML Object" />
            <asp:Button ID="cmdFtpDownload" runat="server"
                Text="Ftp Download" OnClick="cmdFtpDownload_Click" />
            <br />
            <br />
            <asp:TextBox ID="txtEncrypt" runat="server" Columns="40" Rows="5" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Button ID="cmdEncrypt" runat="server" OnClick="cmdEncrypt_Click" Text="Encrypt" />
            <br />
            <br />
            <asp:TextBox ID="txtDecrypt" runat="server" Columns="40" Rows="5" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Button ID="cmdDecrypt" runat="server" OnClick="cmdDecrypt_Click" Text="Decrypt" />
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
        </div>
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <!--[if gte IE 6]>
            <input type="text" size="25" />
        <![endif]-->
        <br />
        <br />
        <asp:Button ID="cmdGetEditorText" runat="server" Text="Button" OnClick="cmdGetEditorText_Click" />
        <asp:Button ID="cmdError" runat="server" OnClick="cmdError_Click" Text="Show Error" />
        <br />
        <asp:TextBox ID="txtEditorText" runat="server" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox>
        <%=DateTime.Now.Ticks - (new DateTime(2000,1,1)).Ticks %>
    </form>
</body>
</html>
