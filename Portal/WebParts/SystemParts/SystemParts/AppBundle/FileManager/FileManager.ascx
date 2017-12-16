<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.FileManager.FileManagerController" %>
<%@ Register Src="Controls/Breadcrumb.ascx" TagName="Breadcrumb" TagPrefix="uc1" %>
<asp:HiddenField ID="hObjectId" runat="server" Value="-1" />
<asp:HiddenField ID="hRecordId" runat="server" Value="-1" />
<asp:HiddenField ID="hEnableVersion" runat="server" Value="0" />
<div class="_wp_FileManager wp-FileManager">
    <%--<h3>File Manager</h3>--%>
    <uc1:Breadcrumb ID="Breadcrumb1" runat="server" />
    <br />
    <div class="control-box" runat="server" id="rowFolderControls">
        <div>
            <asp:Button ID="cmdAddfiles" CssClass="btn btn-default btn-sm" runat="server" Text="Add files" OnClick="cmdAddfiles_Click" />&nbsp;
                <asp:Button ID="cmdCreatefolder" CssClass="btn btn-default btn-sm" runat="server" Text="Create folder" OnClick="cmdCreatefolder_Click" />
            <div class="pull-right" style="font-size: larger" id="panelStorageInfo" runat="server"
                visible="false">
                <asp:Label ID="lblStorageInfo" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <asp:GridView ID="gridViewFolders" CssClass="table table-borderless" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" CellPadding="2" DataKeyNames="Name" DataSourceID="ObjectDataSourceFolders"
        ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="No files found."
        OnRowCommand="GridViewFolders_RowCommand" PageSize="20">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" CssClass="_FileManager_Button" runat="server"
                        CommandName="View-File" ImageUrl='<%# (bool)Eval("IsFolder") ? "~/Content/Assets/Images/Common/folder2.gif" : "~/Content/Assets/Images/Common/txt.gif" %>'
                        AlternateText="View details" ToolTip="View details" CommandArgument='<%# Eval("Name") %>' />
                    <%-- Folder --%>
                    <asp:LinkButton runat="server" ID="cmdViewSubFolders" CommandName="View-SubFolders"
                        Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("Name") %>' Visible='<%# (bool)Eval("IsFolder") %>'></asp:LinkButton>
                    <%-- File --%>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="View-File" Text='<%# Eval("Name") %>'
                        CommandArgument='<%# Eval("Name") %>' Visible="false"></asp:LinkButton>

                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# Eval("FullPath") %>' Visible='<%# !(bool)Eval("IsFolder") %>'><%# Eval("Name") %></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SizeString" HeaderText="Size" SortExpression="Size">
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="TypeName" HeaderText="Type" SortExpression="TypeName">
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="DateModified" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                HeaderText="Date Modified" SortExpression="DateModified" />
            <asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="Center" Width="18px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <img id="Img1" runat="server" visible='<%# !string.IsNullOrEmpty((string)Eval("CoUser")) %>' src="/Content/Assets/Images/Common/ico_flag_red.gif" title='<%# Eval("CoUser") %>' alt='<%# Eval("CoUser") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#F5F5E6" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#5C5247" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSourceFolders" SelectMethod="GetFolders" runat="server"
        TypeName="WCMS.WebSystem.WebParts.FileManager.FileManagerController">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="selectedPath" Type="String" />
            <asp:Parameter DefaultValue="" Name="defaultPath" Type="String" />
            <asp:ControlParameter ControlID="hEnableVersion" DefaultValue="0" Name="enableVersion"
                PropertyName="Value" Type="Int32" />
            <asp:ControlParameter ControlID="hObjectId" DefaultValue="0" Name="objectId" PropertyName="Value"
                Type="Int32" />
            <asp:ControlParameter ControlID="hRecordId" DefaultValue="0" Name="recordId" PropertyName="Value"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div>
        <asp:Label CssClass="Header" Style="color: Red" ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>
</div>
