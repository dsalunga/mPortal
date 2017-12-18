<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MaintainScrollPositionOnPostback="true"
    CodeBehind="Default.aspx.cs" Inherits="WCMS.WebSystem.WebParts.Central.CentralLoader" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="~/Content/Parts/Central/Breadcrumb.ascx" TagName="Breadcrumb" TagPrefix="uc1" %>
<%@ Register Src="~/Content/Controls/ContextActionBar.ascx" TagName="ContextActionBar" TagPrefix="uc2" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc3" %>
<%--<%@ Register Src="~/Content/Parts/Central/Controls/WebGenericTab.ascx" TagName="WebGenericTab" TagPrefix="uc4" %>--%>
<%@ Register Src="HeaderNavbar.ascx" TagName="HeaderNavbar" TagPrefix="uc5" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="robots" content="none,noarchive" />
    <title><%= GetValue("Title") %></title>

    <link href="<%#WebUtil.Version("~/content/plugins/bootstrap3/css/bootstrap.min.css")%>" rel="stylesheet">
    <style type="text/css">
        body {
            padding-top: 60px;
            padding-bottom: 40px;
        }

        .aspnet-checkbox input[type="checkbox"] + label, .aspnet-radio input[type="radio"] + label {
            display: inline;
        }

        .aspnet-checkbox input[type="checkbox"], .aspnet-radio input[type="radio"] {
            margin-top: 0;
            margin-right: 4px;
        }

        .aspnet-radio label {
            margin-right: 8px;
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
    <link href="<%#WebUtil.Version("~/content/assets/styles/websystem.controls.min.css")%>" type="text/css" rel="stylesheet" />
    <link href="<%#WebUtil.Version("~/content/assets/styles/websystem.admin.min.css")%>" type="text/css" rel="stylesheet" />

    <script src="<%#WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="<%#WebUtil.Version("~/content/assets/scripts/html5shiv.js")%>"></script>
      <script src="<%#WebUtil.Version("~/content/assets/scripts/respond.min.js")%>"></script>
    <![endif]-->

    <%# GetValue("Resources") %>
</head>
<body>
    <a name="top"></a>
    <uc5:HeaderNavbar ID="HeaderNavbar1" runat="server" />
    <div class="container-fluid">
        <form id="form1" runat="server">
            <uc1:Breadcrumb ID="Breadcrumb1" runat="server" />
            <% if (!string.IsNullOrEmpty(PageTitle))
               { %>
            <h1 id="lblHeader" class="page-header" style="margin-bottom: 5px; font-weight: normal; margin-top: 30px;">
                <a href="<% =PageUrl %>"><% =PageTitle %></a>
            </h1>
            <% } %>
            <div>
                <asp:PlaceHolder ID="phCMSControl" runat="server"></asp:PlaceHolder>
                <div id="lblStatus" runat="server" class="alert alert-danger"></div>
            </div>
            <div runat="server" id="trFinish" visible="false" class="control-box-strong1 row pull-left" style="padding-top: 10px">
                <div class="col-md-12">
                    <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click" Visible="false" CssClass="btn btn-primary" />
                    <asp:Button ID="cmdFinish" runat="server" Text="Close" OnClick="cmdFinish_Click" CssClass="btn btn-default"
                        CausesValidation="False" ToolTip="Closes this form and return back to previous page. NOTE: All unsaved changes will be IGNORED!" />
                </div>
            </div>
        </form>
    </div>
    <script src="<%=WebUtil.Version("~/Content/Assets/Scripts/jquery-ui.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/plugins/bootstrap3/js/bootstrap.min.js")%>"></script>
    <script src="<%=WebUtil.Version("~/Content/Assets/Scripts/Common.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/Content/Assets/Scripts/DateTimePicker.aspx.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/Content/Assets/Scripts/WCMS.Core.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/Content/Assets/Scripts/WCMS.Utils.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/Content/Assets/Scripts/WebSystem.Admin.min.js")%>" type="text/javascript"></script>
</body>
</html>
