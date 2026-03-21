<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WUserControl" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= GetValue("Title") %></title>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->

    <link href="<%# WebUtil.Version("~/content/plugins/bootstrap3/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%# WebUtil.Version("~/content/assets/styles/websystem.min.css") %>" type="text/css" rel="stylesheet" />
    <script src="<%# WebUtil.Version("~/content/assets/scripts/jquery.min.js") %>" type="text/javascript"></script>

    <%# GetValue("Resources") %>
</head>
<body id="Body" data-fx='{"userId":<%= GetValue("UserId") %>,"pageId":<%= GetValue("PageId") %>,"siteId":<%= GetValue("SiteId") %>}'>
    <a name="top"></a>
    <asp:PlaceHolder runat="server" ID="phHeader"></asp:PlaceHolder>
    <form id="formMain" runat="server">
    </form>
    <script src="<%#WebUtil.Version("~/content/assets/scripts/jquery-ui.min.js")%>" type="text/javascript"></script>
    <script src="<%#WebUtil.Version("~/content/assets/scripts/jquery.ui.touch-punch.min.js")%>" type="text/javascript"></script>
    <script src="<%#WebUtil.Version("~/content/plugins/bootstrap3/js/bootstrap.min.js")%>"></script>
    <script src="<%#WebUtil.Version("~/content/assets/scripts/common.js")%>" type="text/javascript"></script>
    <script src="<%#WebUtil.Version("~/content/assets/scripts/wcms.core.js")%>" type="text/javascript"></script>
    <script src="<%#WebUtil.Version("~/content/assets/scripts/wcms.utils.js")%>" type="text/javascript"></script>
</body>
</html>
