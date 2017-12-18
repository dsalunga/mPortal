<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSites.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebSitesController" %>
<%@ Register Src="../Controls/WebSiteTab.ascx" TagName="WebSiteTab" TagPrefix="uc2" %>
<%@ Register Src="../Controls/WebSitesControl.ascx" TagName="WebSitesControl" TagPrefix="uc1" %>
<h2>Web Sites</h2>
<uc2:WebSiteTab ID="WebSiteTab1" runat="server" />
<uc1:WebSitesControl ID="WebSitesControl1" runat="server" />
