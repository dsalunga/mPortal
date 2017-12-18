<%@ Page Language="c#" Inherits="des.Web.CMS.TemplateOrphans"
    MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Codebehind="OrphanPageItems.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="header">
                Misplaced Page Items</td>
        </tr>
        <tr>
            <td class="control_box">
                <asp:Button ID="cmdDelete" runat="server" Width="85px" Text="Delete" OnClick="cmdDelete_Click" Height="30px" />&nbsp;
                <asp:Button ID="cmdMoveTo" runat="server" Width="85px" Text="Move To:"
                    OnClick="cmdMoveTo_Click" Height="30px"></asp:Button>&nbsp;
                <asp:DropDownList ID="cboPlaceholders" runat="server" DataSourceID="SqlDataSource2"
                    DataTextField="Name" DataValueField="PlaceholderID">
                </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="CMS.SELECT_PageTemplateItems" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="SitePageID" QueryStringField="SitePageID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="Vertical"
                    Width="100%" AutoGenerateColumns="False" DataKeyNames="SitePageItemID" OnRowCommand="GridView1_RowCommand">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="CommonSectionItemTemplateID" HeaderText="CommonSectionItemTemplateID" SortExpression="CommonSectionItemTemplateID"
                            Visible="False" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "SitePageItemID", "<input type='checkbox' value='{0}' name='chkChecked'>") %>
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                        <asp:TemplateField HeaderText="Active">
                            <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Image runat="server" ImageUrl='<%# des.WebHelper.SetStateImage(DataBinder.Eval(Container, "DataItem.IsActive")) %>'
                                    ID="Image1" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="CommonSectionTemplateName" HeaderText="Control Template"
                            SortExpression="CommonSectionTemplateName" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:TemplateField HeaderText="CMS">
                            <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="cms" ImageUrl="Images/ico_tools.gif"
                                    AlternateText="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SitePageItemID") %>'>
                                </asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="edit_item" ImageUrl="~/Admin/Images/ico_edit.gif"
                                    AlternateText="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SitePageItemID") %>'>
                                </asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="CMS.SELECT_SitePageItems_ORPHANS" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="SitePageID" QueryStringField="SitePageID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource></td>
        </tr>
    </table>
</asp:Content>
