<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Download.Controls.BasicListBase" EnableViewState="false" ClassName="BasicList_A3" Codebehind="BasicList.ascx.cs" %>
<asp:DataList ID="DataList1" runat="server" Width="100%">
    <ItemTemplate>
        <img height="6" alt="" src="/Assets/Uploads/Image/HOME/AL3Carrers/bullet01.gif" width="6" />
        <span class="txt11" style="color: #0000ff; text-decoration: underline"><a href='/_Sections/Download/Handler.ashx?ID=<%# Eval("DownloadID") %>&Force=<% =(this.ForceDownload ? "true" : "false") %>'>
            <%# Eval("Name") %>
        </a></span>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <img height="6" alt="" src="/Assets/Uploads/Image/HOME/AL3Carrers/bullet02.gif" width="6" />
        <span class="txt11" style="color: #0000ff; text-decoration: underline"><a href='/_Sections/Download/Handler.ashx?ID=<%# Eval("DownloadID") %>&Force=<% =(this.ForceDownload ? "true" : "false") %>'>
            <%# Eval("Name") %>
        </a></span>
    </AlternatingItemTemplate>
</asp:DataList>