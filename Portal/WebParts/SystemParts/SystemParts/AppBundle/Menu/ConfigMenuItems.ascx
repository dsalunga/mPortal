<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Menu.ConfigMenuItems"
    CodeBehind="ConfigMenuItems.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="Controls/AdminTabControl.ascx" TagName="AdminTabControl" TagPrefix="uc2" %>
<uc2:AdminTabControl ID="AdminTabControl1" runat="server" />
<table border="0" width="100%">
    <tr>
        <td valign="top">
            <div style="padding: 5px 20px 5px 5px">
                <asp:TreeView ID="TreeView1" runat="server" ImageSet="Msdn" NodeIndent="18" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" />
                    <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
                        Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px" />
                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                        NodeSpacing="1px" VerticalPadding="2px" />
                </asp:TreeView>
            </div>
        </td>
        <td valign="top" style="width: 100%">
            <div class="control-box no-bottom-margin">
                <div>
                    <asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-default btn-sm" Text="Add" OnClick="cmdAdd_Click" />
                    <asp:Button ID="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete?');"
                        runat="server" CssClass="btn btn-default btn-sm" Text="Delete" OnClick="cmdDelete_Click" />
                    &nbsp;
                        <asp:Button ID="cmdMove" runat="server" CssClass="btn btn-default btn-sm" Text="Move To:" OnClick="cmdMove_Click" />
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="input" />
                    <asp:Button ID="cmdGO" runat="server" CssClass="btn btn-default btn-sm" Text="GO" OnClick="cmdGO_Click" />
                </div>
            </div>
            <div class="table-responsive clear-left">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
                    CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" AutoGenerateColumns="False"
                    Width="100%" PageSize="50" GridLines="None" OnRowCommand="GridView1_RowCommand">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type='checkbox' value='<%# Eval("Id") %>' name='chkChecked' />
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <HeaderStyle HorizontalAlign="Center" Width="18px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                    ID="Imagebutton1" AlternateText="Edit Site" CommandArgument='<%# Eval("Id") %>' /><%--<asp:ImageButton
                                    ID="ImageButton3" runat="server" CommandName="View_ChildNodes" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                                    AlternateText="Children" ToolTip="Children" CommandArgument='<%# Eval("Id") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Text" HeaderText="Text" SortExpression="Text" HeaderStyle-HorizontalAlign="Left"
                        HtmlEncode="false">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>--%>
                        <asp:HyperLinkField DataNavigateUrlFields="TextUrl" DataTextField="Text" HeaderText="Text"
                            SortExpression="Text" HeaderStyle-HorizontalAlign="Left" />
                        <asp:HyperLinkField DataNavigateUrlFields="PageUrl" DataTextField="PageUrl" HeaderText="Page Url"
                            SortExpression="PageUrl" Target="_blank" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="Target" HeaderText="Target" SortExpression="Target" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Active" SortExpression="Active">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Permission" SortExpression="CheckPermission">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Image ID="Image2" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("CheckPermission")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                TypeName="WCMS.WebSystem.WebParts.Menu.ConfigMenuItems">
                <SelectParameters>
                    <asp:ControlParameter ControlID="TreeView1" Name="parentId" PropertyName="SelectedNode.Value"
                        Type="Int32" DefaultValue="-1" />
                    <asp:QueryStringParameter Name="menuId" QueryStringField="MenuId" Type="Int32" DefaultValue="-1" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
</table>
