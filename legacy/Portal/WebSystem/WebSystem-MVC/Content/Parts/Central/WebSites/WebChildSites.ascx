<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebChildSites.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebChildSites" %>
<%@ Register Src="../Controls/WebSitesControl.ascx" TagName="WebSitesControl" TagPrefix="uc1" %>
<%@ Register Src="../Controls/WebSiteTab.ascx" TagName="WebSiteTab" TagPrefix="uc2" %>
<uc2:WebSiteTab ID="WebSiteTab1" runat="server" />
<uc1:WebSitesControl ID="WebSitesControl1" runat="server" />
