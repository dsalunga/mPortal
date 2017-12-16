<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Download.Controls.BasicListBase"
    EnableViewState="false" CodeBehind="BasicList.ascx.cs" ClassName="BasicList" %>
<asp:DataList ID="DataList1" runat="server" Width="100%">
    <ItemTemplate>
        <img alt="" src="/Assets/Uploads/Image/arrow.gif" width="9" height="9" />
        <span class="txt11" style="color: #0000ff; text-decoration: underline"><a href='/_Sections/Download/Handler.ashx?ID=<%# Eval("DownloadID") %>&Force=<% =(this.ForceDownload ? "true" : "false") %>'>
            <%# Eval("Name") %>
        </a></span>
    </ItemTemplate>
</asp:DataList>