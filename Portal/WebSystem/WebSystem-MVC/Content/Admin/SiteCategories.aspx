<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="CMS.SiteCategories" Title="Untitled Page" Codebehind="SiteCategories.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" width="100%">
        <tr>
            <td valign="top">
                <div style="padding: 5px">
                    <asp:TreeView ID="TreeView1" runat="server" ImageSet="Msdn" NodeIndent="18" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <ParentNodestyle Font-Bold="False" />
                        <HoverNodestyle Font-Underline="True" />
                        <SelectedNodestyle BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
                            Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px" />
                        <Nodestyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                            Nodespacing="1px" VerticalPadding="2px" />
                    </asp:TreeView>
                </div>
            </td>
            <td valign="top" style="width: 100%">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="middle" nowrap class="control_box">
                            <asp:Button ID="cmdAdd" runat="server" Width="85px" Text="Add" OnClick="cmdAdd_Click" Height="30px" />
                            <asp:Button ID="cmdDelete" runat="server" Width="85px" Text="Delete" OnClick="cmdDelete_Click" Height="30px" />
                            &nbsp;
                            <asp:Button ID="cmdMove" runat="server" Text="Move To:" OnClick="cmdMove_Click" />
                            <asp:DropDownList ID="DropDownList1" runat="server" />
                            <asp:Button ID="cmdGO" runat="server" Text="GO" OnClick="cmdGO_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" AutoGenerateColumns="False"
                                Width="100%" PageSize="15" GridLines="None" OnRowCommand="GridView1_RowCommand">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField Visible="False" DataField="SiteCategoryID" HeaderText="ID"></asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "SiteCategoryID", "<input type='checkbox' value='{0}' name='chkChecked'>") %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" CommandName="edit_item" ImageUrl="~/Admin/Images/ico_edit.gif"
                                                ID="Imagebutton1" AlternateText="Edit Site" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SiteCategoryID") %>'>
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank">
                                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Menu" SortExpression="ShowInMenu">
                                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Image runat="server" ImageUrl='<%# des.WebHelper.SetStateImage(DataBinder.Eval(Container, "DataItem.ShowInMenu")) %>'
                                                ID="Image2"></asp:Image>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SiteCategoryName" HeaderText="Category Name" SortExpression="SiteCategoryName" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                SelectCommand="CMS.SELECT_SiteCategories" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="TreeView1" Name="ParentID" PropertyName="SelectedNode.Value"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
