<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPagePanelHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebPagePanelHome" %>
<%@ Register Src="../Controls/WebGenericTab.ascx" TagName="WebGenericTab" TagPrefix="uc2" %>
<uc2:WebGenericTab ID="WebGenericTab1" runat="server" />
<div class="row">
    <div class="col-md-6">
        <h1 class="central page-header" runat="server" id="lblHeader">
            Page Panel
        </h1>
        <table>
            <tr id="rowProperties" runat="server">
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a id="linkProperties" runat="server" href="#">
                                    <img src="/Content/Assets/Images/file_edit.png" class="TaskListIcon" border="0" />
                                </a>
                            </td>
                            <td class="Header">Properties
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Place description here
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td rowspan="2">
                                <a id="linkParameters" runat="server" href="#">
                                    <img src="/Content/Assets/Images/piece.png" class="TaskListIcon" border="0" />
                                </a>
                            </td>
                            <td class="Header">Parameters
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Place description here
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
