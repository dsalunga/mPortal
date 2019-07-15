<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WUserControl" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%= GetValue("Title") %></title>

    <link href="<%#WebUtil.Version("~/content/plugins/bootstrap3/css/bootstrap.min.css")%>" rel="stylesheet" />
    <style type="text/css">
        .aspnet-checkbox input[type="checkbox"] + label, .aspnet-radio input[type="radio"] + label {
            display: inline;
            font-weight: normal;
        }

        .aspnet-checkbox input[type="checkbox"], .aspnet-radio input[type="radio"] {
            margin: 5px 5px 5px 0;
        }
    </style>
    <link href="<%#WebUtil.Version("~/content/assets/styles/websystem.min.css")%>" type="text/css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="<%#WebUtil.Version("~/content/assets/scripts/html5shiv.js")%>"></script>
      <script src="<%#WebUtil.Version("~/content/assets/scripts/respond.min.js")%>"></script>
    <![endif]-->
    <script src="<%#WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>

    <%# GetValue("Resources") %>
</head>
<body id="Body" data-fx='{"userId":<%= GetValue("UserId") %>,"pageId":<%= GetValue("PageId") %>,"siteId":<%= GetValue("SiteId") %>}'>
    <a name="top"></a>
    <asp:PlaceHolder runat="server" ID="phHeader"></asp:PlaceHolder>
    <form runat="server" id="formMain">
        <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>--%>
    </form>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery-ui.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.ui.touch-punch.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/plugins/bootstrap3/js/bootstrap.min.js")%>"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/common.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.core.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js")%>" type="text/javascript"></script>

    <asp:PlaceHolder runat="server" ID="phFooterLast"></asp:PlaceHolder>
</body>
</html>
