<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGroupList.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.WebGroupList" %>
<%@ Register Src="../../AppBundle/Article/Controls/ArticleListPreview.ascx" TagName="ArticleListPreview"
    TagPrefix="uc1" %>
<div class="group-dashboard">
    <asp:Repeater runat="server" ID="Repeater1">
        <ItemTemplate>
            <div class="group-dashboard-item">
                <h3>
                    <a href='<%# Eval("PageUrl") %>'>
                        <%# Eval("DisplayHTML") %></a></h3>
                <uc1:ArticleListPreview ID="ArticleListPreview1" IsPermitted='<%# Eval("IsPermitted") %>'
                    AccessDeniedContent='<%# Eval("AccessDeniedContent") %>' PageId='<%# Eval("PageId") %>'
                    PageUrl='<%# Eval("PageUrl") %>' runat="server" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <%--<asp:DataList ID="DataList1" runat="server" RepeatColumns="4" RepeatDirection="Vertical" RepeatLayout="Flow">
        <ItemTemplate>
            <div class="group-dashboard-item">
                <h3>
                    <a href='<%# Eval("PageUrl") %>'>
                        <%# Eval("DisplayHTML") %></a></h3>
                <uc1:ArticleListPreview ID="ArticleListPreview1" IsPermitted='<%# Eval("IsPermitted") %>'
                    AccessDeniedContent='<%# Eval("AccessDeniedContent") %>' PageId='<%# Eval("PageId") %>'
                    PageUrl='<%# Eval("PageUrl") %>' runat="server" />
            </div>
            <br />
        </ItemTemplate>
    </asp:DataList>--%>
</div>
