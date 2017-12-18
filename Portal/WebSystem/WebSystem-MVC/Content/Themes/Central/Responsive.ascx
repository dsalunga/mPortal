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
        body {
            padding-top: 60px;
            padding-bottom: 40px;
        }

        .sidebar-nav {
            padding: 9px 0;
        }

        section {
            padding-top: 30px;
        }

            section > .page-header,
            section > .lead {
                color: #5a5a5a;
            }

        /*ul.nav li.dropdown:hover ul.dropdown-menu {
            display: block;
        }*/

        /* bootstrap-aspnet related fixes */
        .aspnet-checkbox input[type="checkbox"] + label, .aspnet-radio input[type="radio"] + label {
            display: inline;
        }

        .aspnet-checkbox input[type="checkbox"], .aspnet-radio input[type="radio"] {
            margin-top: 0;
            margin-right: 4px;
        }

        h1.page-header {
            border-bottom: none;
        }

            h1.page-header a, h1.page-header a:hover {
                color: #333;
                text-decoration: none;
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
    <asp:PlaceHolder ID="phHeader" runat="server"></asp:PlaceHolder>
    <div class="container-fluid">
        <form id="frmMain" runat="server">
            <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>--%>
            <asp:PlaceHolder ID="phContent" runat="server"></asp:PlaceHolder>
            <%--
                    <div class="row-fluid">
                        <div class="span4">
                            <h2>Heading</h2>
                            <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
                            <p><a class="btn btn-default" href="#">View details &raquo;</a></p>
                        </div>
                        <div class="span4">
                            <h2>Heading</h2>
                            <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
                            <p><a class="btn btn-default" href="#">View details &raquo;</a></p>
                        </div>
                        <div class="span4">
                            <h2>Heading</h2>
                            <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
                            <p><a class="btn btn-default" href="#">View details &raquo;</a></p>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span4">
                            <h2>Heading</h2>
                            <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
                            <p><a class="btn btn-default" href="#">View details &raquo;</a></p>
                        </div>
                        <div class="span4">
                            <h2>Heading</h2>
                            <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
                            <p><a class="btn btn-default" href="#">View details &raquo;</a></p>
                        </div>
                        <div class="span4">
                            <h2>Heading</h2>
                            <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
                            <p><a class="btn btn-default" href="#">View details &raquo;</a></p>
                        </div>
                    </div>
            --%>
        </form>
        <%--<div class="row-fluid">
            <div class="span2">
                <div class="well sidebar-nav affix-top">
                    <ul class="nav nav-list">
                        <li class="nav-header">Web Page</li>
                        <li class="divider"></li>
                        <li><a href="#">Link</a></li>
                        <li class="nav-header">Web Site</li>
                        <li class="active"><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>
                        <li class="divider"></li>

                        <li class="nav-header">Sidebar</li>
                        <li><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>
                        <li class="dropdown">
                            <a class="dropdown-toggle"
                                data-toggle="dropdown"
                                href="#">Dropdown
                                <b class="caret"></b>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="nav-header">Sidebar</li>
                                <li class="active"><a href="#">Link</a></li>
                                <li><a href="#">Link</a></li>
                                <li><a href="#">Link</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="span10">
                <%--<h1><%= GetValue("Title") %></h1>--%
                <!--
                <div class="hero-unit">
                    <h1>Hello, world!</h1>
                    <p>This is a template for a simple marketing or informational website. It includes a large callout called the hero unit and three supporting pieces of content. Use it as a starting point to create something more unique.</p>
                    <p><a class="btn btn-primary btn-large">Learn more &raquo;</a></p>
                </div>
                -->

            </div>
        </div>--%>

        <%--<hr />
        <footer>
            <p>&copy; 2012</p>
        </footer>--%>
    </div>

    <!-- Placed at the end of the document so the pages load faster -->
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery-ui.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/plugins/bootstrap3/js/bootstrap.min.js")%>"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/common.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.core.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.utils.min.js")%>" type="text/javascript"></script>
</body>
</html>
