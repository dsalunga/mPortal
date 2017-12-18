<%@ Control Language="C#" AutoEventWireup="true" ClassName="Basic" Inherits="WCMS.WebSystem.WUserControl" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<%@ Import Namespace="WCMS.Common.Utilities" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><%= GetValue("Title") %></title>

    <!-- Le styles -->
    <link href="<%#WebUtil.Version("~/content/plugins/bootstrap3/css/bootstrap.min.css")%>" rel="stylesheet" />
    <style type="text/css">
        /*body {
            padding-top: 60px;
            padding-bottom: 40px;
        }*/

        body {
            padding: 5px;
        }

        .sidebar-nav {
            padding: 9px 0;
        }

        /* bootstrap-aspnet related fixes */
        .aspnet-checkbox input[type="checkbox"] + label, .aspnet-radio input[type="radio"] + label {
            display: inline;
        }

        .aspnet-checkbox input[type="checkbox"], .aspnet-radio input[type="radio"] {
            margin-top: 0;
            margin-right: 4px;
        }
    </style>
    <link href="<%#WebUtil.Version("~/content/assets/styles/websystemv2.min.css")%>" type="text/css" rel="stylesheet" />

    <script src="<%#WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->

    <%# GetValue("Resources") %>
</head>
<body id="Body" data-fx='{"userId":<%= GetValue("UserId") %>,"pageId":<%= GetValue("PageId") %>,"siteId":<%= GetValue("SiteId") %>}'>
    <a name="top"></a>
    <form id="frmMain" runat="server">
        <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>--%>
        <asp:PlaceHolder ID="phContent" runat="server"></asp:PlaceHolder>
    </form>

    <!-- Placed at the end of the document so the pages load faster -->
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery-ui.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/plugins/bootstrap3/js/bootstrap.min.js")%>"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/common.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.core.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js")%>" type="text/javascript"></script>
</body>
</html>
