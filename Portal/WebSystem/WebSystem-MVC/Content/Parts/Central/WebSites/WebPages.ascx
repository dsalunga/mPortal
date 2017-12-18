<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPages.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebPagesController" %>
<%@ Register Src="../Controls/WebSiteTab.ascx" TagName="WebSiteTab" TagPrefix="uc1" %>
<%@ Register Src="../Controls/WebPagesControl.ascx" TagName="WebPagesControl" TagPrefix="uc2" %>
<asp:HiddenField runat="server" ID="hidPageURL" Value="" />
<uc1:WebSiteTab ID="WebSiteTab1" runat="server" />
<uc2:WebPagesControl ID="WebPagesControl1" runat="server" />
