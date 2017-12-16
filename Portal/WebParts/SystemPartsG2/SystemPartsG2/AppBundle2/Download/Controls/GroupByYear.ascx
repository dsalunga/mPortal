<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Download.Controls.GroupByYear" EnableViewState="false" Codebehind="GroupByYear.ascx.cs" %>
<%@ Register Src="BasicList.ascx" TagName="BasicList" TagPrefix="uc1" %>
<asp:DataList ID="DataList1" runat="server">
    <ItemTemplate>
        <strong>
            <%# Eval("FileYear") %>
        </strong>
        <br />
        <uc1:BasicList ID="BasicList1" runat="server" MaxRecords='<%# MaxRecords %>' ForceDownload='<%# ForceDownload %>' Rows='<%# Rows %>' Columns='<%# Columns %>' Year='<%# Eval("FileYear") %>'  SitePageItemID='<%# iSitePageItemID %>' PageType='<%# sPageType %>' />
        <br />
    </ItemTemplate>
</asp:DataList>