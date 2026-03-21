<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileView.ascx.cs" Inherits="WCMS.WebSystem.WebParts.FileManager.FileView" %>
<%@ Register Src="Controls/Breadcrumb.ascx" TagName="Breadcrumb" TagPrefix="uc1" %>
<asp:HiddenField ID="hFileId" runat="server" Value="-1" />
<div class="wp-FileManager">
    <uc1:Breadcrumb ID="Breadcrumb1" runat="server" />
    <br />
    <div class="command-list" style="border-bottom: 1px solid #bbb; font-weight: bold">
        <asp:LinkButton ID="cmdDownload" runat="server" OnClick="cmdDownload_Click">Download</asp:LinkButton>
        <span runat="server" id="panelRename" visible="false">&nbsp;<a runat="server" href=""
            id="linkRename">Rename</a></span><span runat="server" visible="false" id="panelDelete">&nbsp;
                <asp:LinkButton ID="cmdDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                    OnClick="cmdDelete_Click">Delete</asp:LinkButton></span><span runat="server" id="panelEdit"
                        visible="false">&nbsp;<a runat="server" href="" id="linkEdit">Edit</a></span><span
                            runat="server" id="panelExtractHere" visible="false">&nbsp;<asp:LinkButton ID="cmdExtractHere"
                                runat="server" onclick="cmdExtractHere_Click">Extract Here</asp:LinkButton></span>
        <div style="float: right" id="panelVersioning" runat="server" visible="false">
            <asp:LinkButton ID="cmdCheckOut" runat="server" Visible="false" OnClick="cmdCheckOut_Click"><img class="icon" alt="Check-Out" src="/Content/Assets/Images/Common/ico_flag_red.gif" />Check-Out</asp:LinkButton>
            <asp:LinkButton ID="cmdCancelCheckOut" runat="server" Visible="false" OnClick="cmdCancelCheckOut_Click"><img class="icon" alt="Undo Check-Out" src="/Content/Assets/Images/Common/ico_flag_yellow.gif" />Undo Check-Out</asp:LinkButton>
        </div>
    </div>
    <div class="file-view">
        <br />
        <img alt="File or folder thumbnail" runat="server" id="imgThumbnail" />
        <br />
        <br />
        <h3 id="lblFileName" runat="server">
            file name goes here</h3>
        <div class="field-line">
            <span class="field-label">Date Modified:</span><strong id="lblDateModified" runat="server"></strong></div>
        <div id="panelSize" runat="server" class="field-line">
            <span class="field-label">Size:</span><strong id="lblSize" runat="server">0kb</strong>
        </div>
        <div class="field-line">
            <span class="field-label">Permalink:</span><strong><a target="_blank" href="" runat="server"
                id="linkPermalink">&nbsp;</a></strong></div>
        <div class="field-line">
            <span id="lblStatus" runat="server" enableviewstate="false" style="color: Red"></span>
        </div>
        <div runat="server" id="panelVersions" visible="false">
            <br />
            <br />
            <h4 style="padding-bottom: 5px">
                Version History</h4>
            <asp:GridView ID="gridVersions" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" CellPadding="2" DataKeyNames="Id" DataSourceID="ObjectDataSourceFolders"
                ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="No versions found."
                PageSize="20">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="VersionDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                        HeaderText="Date" SortExpression="VersionDate" />
                    <asp:BoundField DataField="User" HeaderText="User" SortExpression="User">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ActivityString" HeaderText="Activity" SortExpression="ActivityString">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
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
            <asp:ObjectDataSource ID="ObjectDataSourceFolders" SelectMethod="SelectVersions"
                runat="server" TypeName="WCMS.WebSystem.WebParts.FileManager.FileView">
                <SelectParameters>
                    <asp:ControlParameter Name="fileId" ControlID="hFileId" DefaultValue="-1" Type="Int32"
                        PropertyName="Value" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</div>
