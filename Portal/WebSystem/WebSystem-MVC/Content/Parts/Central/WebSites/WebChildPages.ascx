<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebChildPages.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebChildPages" %>
<%@ Register Src="../Controls/WebPageTab.ascx" TagName="WebPageTab" TagPrefix="uc1" %>
<%@ Register Src="../Controls/WebPagesControl.ascx" TagName="WebPagesControl" TagPrefix="uc2" %>
<uc1:WebPageTab ID="WebPageTab1" runat="server" />
<uc2:WebPagesControl ID="WebPagesControl1" runat="server" />
