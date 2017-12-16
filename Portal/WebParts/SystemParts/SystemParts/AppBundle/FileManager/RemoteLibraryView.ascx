<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RemoteLibraryView.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.RemoteIndexer.RemoteLibraryView" %>
<%@ Register Src="Controls/IndexerBreadcrumb.ascx" TagName="Breadcrumb" TagPrefix="uc1" %>
<asp:HiddenField runat="server" ID="hLibraryId" Value="-1" />
<asp:HiddenField runat="server" ID="hDownloadUrl" Value="" />
<asp:HiddenField runat="server" ID="hDownloadTimeOut" Value="0" />
<div class="wp-RemoteIndexer">
    <uc1:Breadcrumb ID="Breadcrumb1" runat="server" />
    <div runat="server" id="rowFolderControls" class="min-bottom-margin">
        <span runat="server" id="panelLibraries" visible="false">Library:&nbsp;<asp:DropDownList ID="cboRemoteLibraries" DataValueField="Id" DataTextField="Name"
            runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceLibraries" AppendDataBoundItems="true"
            OnSelectedIndexChanged="cboRemoteLibraries_SelectedIndexChanged">
            <asp:ListItem Value="-1" Text="* Select a library *" Selected="True" />
        </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSourceLibraries" runat="server" SelectMethod="GetActiveLibraries"
                TypeName="WCMS.WebSystem.WebParts.RemoteIndexer.RemoteLibraryView"></asp:ObjectDataSource>
        </span>
        <div class="pull-right">
            <div class="input-append">
                <asp:TextBox ID="txtSearch" Columns="25" runat="server" CssClass="span3 input" ToolTip="To search the whole library, go to the root folder, otherwise only the current folder will be searched (excluding its subfolders)."></asp:TextBox>
                <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click"
                    CssClass="btn btn-default btn-sm" /><asp:Button ID="cmdReset" runat="server" Text="Reset" CssClass="btn btn-default btn-sm" OnClick="cmdReset_Click" />
            </div>
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="gridViewFolders" runat="server" CssClass="table table-borderless" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="2" DataKeyNames="Name" DataSourceID="ObjectDataSourceIndexes"
            ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="No files found."
            OnRowCommand="GridViewFolders_RowCommand" PageSize="999" EnableViewState="false">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <img src='<%# (bool)Eval("IsFolder") ? "/Content/Assets/Images/Common/folder2.gif" : "/Content/Assets/Images/Common/txt.gif" %>' />

                        <%-- Folder --%>
                        <a href='<%# String.Format(Eval("FolderLinkFormat").ToString(), Eval("Id")) %>' enableviewstate="false" title='<%# Eval("Name") %>' runat="server" visible='<%# (bool)Eval("IsFolder") %>'><%# Eval("Name") %></a>

                        <%-- File --%>
                        <%--<asp:LinkButton runat="server" ID="cmdDownload" CommandName="Download-File"
                            Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("Id") %>' Visible='<%# !(bool)Eval("IsFolder") && hDownloadUrl.Value=="" %>' EnableViewState="false" />--%>

                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("ItemUrl") %>' Visible='<%# !(bool)Eval("IsFolder") %>' EnableViewState="false"><%# Eval("Name") %></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TypeName" HeaderText="Type" SortExpression="TypeName">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DateModified" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Date Modified" SortExpression="DateModified" />
                <asp:BoundField DataField="SizeString" HeaderText="Size" SortExpression="Size">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <RowStyle BackColor="#F5F5E6" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#5C5247" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="White" />
            <PagerSettings PageButtonCount="25" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceIndexes" SelectMethod="GetIndexes" runat="server"
            TypeName="WCMS.WebSystem.WebParts.RemoteIndexer.RemoteLibraryView">
            <SelectParameters>
                <asp:ControlParameter ControlID="hLibraryId" DefaultValue="-1" Name="libraryId" PropertyName="Value"
                    Type="Int32" />
                <asp:QueryStringParameter DefaultValue="-1" Name="parentId" QueryStringField="Id"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtSearch" Name="keyword" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <asp:Label CssClass="Header" EnableViewState="false" Style="color: Red" ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <br />
    <div>
        This library was last indexed on&nbsp;<strong><span id="lblLastIndexDate" runat="server">UNKNOWN</span></strong>.
                    <div runat="server" id="panelUserNamePwd" visible="false">
                        Please use username and password:&nbsp;<strong><span id="lblUserNamePwd" runat="server">username/password</span></strong>
                    </div>
    </div>
</div>
