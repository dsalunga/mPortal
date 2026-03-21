<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WUserControl" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<meta name="viewport" content="width=device-width, initial-scale=1.0" />--%>
    <title><%= GetValue("Title") %></title>

    <link href="<%#WebUtil.Version("~/content/plugins/bootstrap/css/bootstrap.min.css")%>" rel="stylesheet" />
    <style type="text/css">
        .aspnet-checkbox input[type="checkbox"] + label, .aspnet-radio input[type="radio"] + label {
            display: inline;
        }

        .aspnet-checkbox input[type="checkbox"], .aspnet-radio input[type="radio"] {
            margin: 5px 5px 5px 0;
        }
    </style>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->

    <link href="<%#WebUtil.Version("~/content/assets/styles/websystem.min.css")%>" type="text/css" rel="stylesheet" />
    <script src="<%#WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    
    <%# GetValue("Resources") %>
</head>
<body id="Body" data-fx='{"userId":<%= GetValue("UserId") %>,"pageId":<%= GetValue("PageId") %>,"siteId":<%= GetValue("SiteId") %>}'>
    <a name="top"></a>
    <form runat="server">
    </form>

    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery-ui.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/plugins/bootstrap/js/bootstrap.min.js")%>"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/common.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.core.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js")%>" type="text/javascript"></script>
</body>
</html>
