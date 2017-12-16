<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WCMS.WebSystem.WebParts.Download.Controls.DetailedList" Codebehind="DetailedList.ascx.cs" %>
<asp:DataList ID="DataList1" runat="server" Width="100%">
    <ItemTemplate>
        <li style="color: White"><a style="color: White; font-weight: bold" href='/_Sections/Download/Handler.ashx?ID=<%# Eval("DownloadID") %>&Force=<% =(this.ForceDownload ? "true" : "false") %>'>
            <%# Eval("Name") %>
        </a>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;<span style="color: White"><%# ShowFileInfo((int)Eval("DownloadID")) %></span>
        </li>
        <br />
    </ItemTemplate>
</asp:DataList>