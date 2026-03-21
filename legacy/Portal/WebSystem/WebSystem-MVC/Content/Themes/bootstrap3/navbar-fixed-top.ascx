<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WUserControl" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.WebSystem" %>
<%
    dynamic page = GetValue("Page"); //Page.Page;
    dynamic site = GetValue("Site"); //Page.Site;
%>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="keywords" content="keywords" />
    <meta name="description" content="description" />
    <meta name="author" content="" />

    <title><%= GetValue("Title") %></title>

    <!-- Bootstrap core CSS -->
    <link href="<%#WebUtil.Version("~/content/plugins/bootstrap3/css/bootstrap.min.css")%>" rel="stylesheet">
    <link href="<%#WebUtil.Version("~/content/assets/styles/websystemv2.min.css")%>" type="text/css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="<%#WebUtil.Version("~/content/assets/scripts/html5shiv.js")%>"></script>
      <script src="<%#WebUtil.Version("~/content/assets/scripts/respond.min.js")%>"></script>
    <![endif]-->

    <script src="<%#WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>"></script>
    <%# GetValue("Resources") %>
</head>

<body id="Body" data-fx='{"userId":<%= GetValue("UserId") %>,"pageId":<%= page.Id %>,"siteId":<%= site.Id %>}'>
    <a name="top"></a>
    <input type="hidden" id="__hidPageId" value="<%= page.Id %>" />
    <asp:PlaceHolder runat="server" ID="phHeader"></asp:PlaceHolder>
    <!-- Fixed navbar -->
    
    <% if(false){ %>
    <div class="navbar navbar-default navbar-fixed-top" role="navigation" id="top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="<%= site.BuildAbsoluteUrl() %>"><%= site.Name %></a>
            </div>
            <div class="navbar-collapse collapse">
                <asp:PlaceHolder runat="server" ID="phNavigation"></asp:PlaceHolder>
            </div><!--/.nav-collapse -->
        </div>
    </div>
    <% } %>
    

    <div class="container">
        <form runat="server" id="formMain">
            <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>--%>
            <asp:PlaceHolder runat="server" ID="phMain"></asp:PlaceHolder>
        </form>
        <br />
        <hr />
        <footer>
            <p class="text-muted"><%=site.Name %> &copy; <%=DateTime.Now.Year %> <a href="#top" class="text-muted pull-right">Back to top</a></p>
        </footer>
    </div> <!-- /container -->
    <script src="<%=WebUtil.Version("~/content/plugins/bootstrap3/js/bootstrap.min.js")%>"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery-ui.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.ui.touch-punch.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.core.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/common.min.js")%>" type="text/javascript"></script>
    <asp:PlaceHolder runat="server" ID="phFooter"></asp:PlaceHolder>
</body>
</html>
