<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RemoteIndexRecentUpdates.ascx.cs" Inherits="WCMS.WebSystem.Content.Parts.FileManager.RemoteIndexRecentUpdates" %>
<%--<h4 class="heading colr">Recent Files</h4>--%>
<div class="wp-RemoteIndexer">
    <asp:GridView ID="gridIndexes" runat="server" CssClass="table table-borderless" AllowPaging="False" AllowSorting="False"
        AutoGenerateColumns="False" CellPadding="2" DataKeyNames="Name" ShowHeader="false"
        ForeColor="#333333" ShowFooter="false" GridLines="None" Width="100%" EmptyDataText="No files found."
        PageSize="999">
        <%--<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />--%>
        <Columns>
            <asp:TemplateField ItemStyle-VerticalAlign="Top" HeaderText="Recent Files" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <img src='<%# (bool)Eval("IsFolder") ? "/Content/Assets/Images/Common/folder2.gif" : "/Content/Assets/Images/Common/txt.gif" %>' />
                    <a href='<%# String.Format(Eval("FolderLinkFormat").ToString(), Eval("Id")) %>' title='<%# Eval("Name") %>' runat="server" visible='<%# (bool)Eval("IsFolder") %>'><%# Eval("Name") %></a>
                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("ItemUrl") %>' Visible='<%# !(bool)Eval("IsFolder") %>' EnableViewState="false"><%# Eval("Name") %></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="TypeName" HeaderText="Type" SortExpression="TypeName" Visible="false">
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>--%>
            <%--<asp:BoundField DataField="DateModified" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                HeaderText="Date Modified" SortExpression="DateModified" Visible="false" DataFormatString="{0:dd-MMM h\:mmt}" ItemStyle-Wrap="false" />--%>
            <asp:BoundField DataField="DateModifiedString" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                HeaderText="Date Modified" SortExpression="DateModifiedString" Visible="true" ItemStyle-Wrap="false" />
            <%--<asp:BoundField DataField="SizeString" HeaderText="Size" SortExpression="Size" Visible="false">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>--%>
        </Columns>
        <RowStyle BackColor="White" />
        <%--BackColor="#F5F5E6"--%>
        <%--<EditRowStyle BackColor="#2461BF" />--%>
        <%--<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />--%>
        <%--<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />--%>
        <%--<HeaderStyle BackColor="#5C5247" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />--%>
        <%--<AlternatingRowStyle BackColor="White" />--%>
    </asp:GridView>
</div>
