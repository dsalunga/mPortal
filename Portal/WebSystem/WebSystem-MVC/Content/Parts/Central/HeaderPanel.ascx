<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderPanel.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.HeaderPanel" %>
<%@ Import Namespace="WCMS.Framework" %>

<style type="text/css">
    #Body {
        padding: 0px;
    }
</style>
<script type="text/javascript">
    function LoadModule(moduleName) {
        switch (moduleName) {
            case "GlobalManager":
                OpenLink("<% =CentralPages.TreePanel %>", "frameLeft");
                OpenLink("/", "frameMain");
                break;

            case "SiteManager":
                OpenLink("<% =CentralPages.WebSiteTree %>", "frameLeft");
                OpenLink("<% =CentralPages.WebSites %>", "frameMain");
                break;

            case "WebTools":
                OpenLink("<% =CentralPages.WebToolsTree %>", "frameLeft");
                OpenLink("<% =CentralPages.WebSystemDashboard %>", "frameMain");
                break;

            case "SecurityManager":
                OpenLink("<% =CentralPages.WebSecurityTree %>", "frameLeft");
                OpenLink("<% =CentralPages.WebSystemDashboard %>", "frameMain");
                break;

            case "PartManager":
                OpenLink("/Central/Part/WebPartTree/", "frameLeft");
                OpenLink("/Central/Part/WebParts/", "frameMain");
                break;

            case "WebRegistry":
                OpenLink("/Central/Tools/WebRegistryTree/", "frameLeft");
                OpenLink("/Central/Tools/WebRegistry/", "frameMain");
                break;

            case "WebGroup":
                OpenLink("/Central/Security/WebGroupTree/", "frameLeft");
                OpenLink("/Central/Security/WebGroups/", "frameMain");
                break;

            case "WebFolder":
                OpenLink("/Central/Tools/WebFolderTree/", "frameLeft");
                OpenLink("/Central/Tools/WebDataExplorer/", "frameMain");
                break;

            case "WebOffice":
                OpenLink("/Central/Misc/Web-Office-Tree/", "frameLeft");
                OpenLink("/Central/Misc/Web-Offices/", "frameMain");
                break;
        }
    }

    function OpenLink(url, target) {
        /*
        var link = document.getElementById("aLink");
        link.setAttribute("href", url);
        link.setAttribute("target", target);
        link.click();
        */

        window.open(url, target);
    }
</script>
<table cellspacing="0" cellpadding="0" border="0">
    <tbody>
        <tr>
            <td runat="server" id="panelGlobalManager" valign="top">
                <!-- sample icon -->
                <a title="Global Manager" href="javascript: LoadModule('GlobalManager');">
                    <img width="40" border="0" src="/Content/Assets/Images/web.png" alt="Global Manager" /></a>&nbsp;
            </td>
            <td runat="server" id="panelSiteManager" visible="false" valign="top">
                <!-- Site Manager icon -->
                <a title="Web Site Manager" href="javascript: LoadModule('SiteManager');">
                    <img width="40" border="0" src="/Content/Assets/Images/www_link.png" alt="Web Site Manager" /></a>&nbsp;
            </td>
            <td runat="server" id="panelPartManager" visible="false" valign="top">
                <!-- WebPart Manager icon -->
                <a title="WebPart Manager" href="javascript: LoadModule('PartManager');">
                    <img width="40" border="0" src="/Content/Assets/Images/grafle.png" alt="WebPart Manager" /></a>&nbsp;
            </td>
            <td runat="server" id="panelWebTools" visible="false" valign="top">
                <!-- Tools icon -->
                <a title="Web Tools" href="javascript: LoadModule('WebTools');">
                    <img width="40" border="0" src="/Content/Assets/Images/gears.png" alt="Web Site Manager" /></a>&nbsp;
            </td>
            <td runat="server" id="panelSecurityManager" visible="false" valign="top">
                <!-- Security icon -->
                <a title="Web Security" href="javascript: LoadModule('SecurityManager');">
                    <img width="40" border="0" src="/Content/Assets/Images/locked.png" alt="Web Security Manager" /></a>&nbsp;
            </td>
            <!--
                            <td valign="top">
                                <!-- sample icon --
                                <a href="javascript: LoadModule('SiteManager');" title="Web Site Manager">
                                    <img alt="Web Site Manager" src="/Content/Assets/Images/statics-1.png" border="0" width="40" /></a>&nbsp;
                            </td>
                            -->
            <td runat="server" id="panelWebRegistry" visible="false" valign="top">
                <!-- Web Registry -->
                <a title="Web Registry" href="javascript: LoadModule('WebRegistry');">
                    <img width="40" border="0" src="/Content/Assets/Images/plugin.png" alt="Web Registry" /></a>&nbsp;
            </td>
            <td runat="server" id="panelWebGroup" visible="false" valign="top">
                <!-- Web Groups -->
                <a title="Web Groups" href="javascript: LoadModule('WebGroup');">
                    <img width="40" border="0" src="/Content/Assets/Images/user_group.png" alt="Web Groups" /></a>&nbsp;
            </td>
            <td runat="server" id="panelWebFolder" visible="false" valign="top">
                <!-- Web Data Explorer -->
                <a title="Web Data Explorer" href="javascript: LoadModule('WebFolder');">
                    <img width="40" border="0" src="/Content/Assets/Images/folder.png" alt="Web Data Explorer" /></a>&nbsp;
            </td>
            <td runat="server" id="panelWebOffice" visible="false" valign="top">
                <!-- Web Office -->
                <a title="Web Office Manager" href="javascript: LoadModule('WebOffice');">
                    <img width="40" border="0" src="/Content/Assets/Images/www_dial.png" alt="Web Office Manager" /></a>&nbsp;
            </td>
        </tr>
    </tbody>
</table>
