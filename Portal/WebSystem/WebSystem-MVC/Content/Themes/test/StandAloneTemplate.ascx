<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StandAloneTemplate.ascx.cs" Inherits="WCMS.WebSystem.Content.Themes.test.StandAloneTemplate" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>test</title>

    <link rel="stylesheet" type="text/css" href="<%# WebUtil.Version("~/content/assets/styles/websystem.min.css") %>" />

    <script src="<%# WebUtil.Version("~/content/assets/scripts/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%# WebUtil.Version("~/content/assets/scripts/jquery-ui.min.js") %>" type="text/javascript"></script>
    <script src="<%# WebUtil.Version("~/content/assets/scripts/common.min.js") %>" type="text/javascript"></script>
    <script src="<%# WebUtil.Version("~/content/assets/scripts/wcms.core.min.js") %>" type="text/javascript"></script>
    <script src="<%# WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js") %>" type="text/javascript"></script>
</head>
<body id="Body">
    <form runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    </form>
</body>
</html>
