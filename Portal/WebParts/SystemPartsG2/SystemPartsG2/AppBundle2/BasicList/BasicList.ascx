<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.BasicList._Sections_BasicList_BasicList"
    CodeBehind="BasicList.ascx.cs" %>
<asp:PlaceHolder ID="phList" runat="server" />
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="BasicListItem_Get" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="1" Name="Page" QueryStringField="Page" Type="Int32" />
        <asp:Parameter DefaultValue="" Name="PageSize" Type="Int32" ConvertEmptyStringToNull="true" />
        <asp:Parameter DefaultValue="" Name="PageType" Type="string" ConvertEmptyStringToNull="true" />
        <asp:Parameter DefaultValue="" Name="SitePageItemID" Type="Int32" ConvertEmptyStringToNull="true" />
    </SelectParameters>
</asp:SqlDataSource>
<br />
<div style="color: white" runat="server" id="divPaging" />
