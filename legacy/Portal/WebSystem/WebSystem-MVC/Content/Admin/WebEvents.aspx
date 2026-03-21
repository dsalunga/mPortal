<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="CMS.SystemEvents" Codebehind="SystemEvents.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="header">
                System Events</td>
        </tr>
        <tr>
            <td valign="middle" nowrap class="ControlBox">
                <asp:Button ID="cmdAddFull" Visible="false" runat="server" Text="Add" Width="80px" OnClick="cmdAddFull_Click" />
                <asp:Button ID="cmdDelete" runat="server" Text="Delete" Width="80px" OnClick="cmdDelete_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="Vertical"
                    Width="100%" AutoGenerateColumns="False" DataKeyNames="SystemEventID" OnRowCommand="GridView1_RowCommand" PageSize="15">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "SystemEventID", "<input type='checkbox' value='{0}' name='chkChecked'>")%>
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="EventDateTime" HeaderText="Date/Time" SortExpression="EventDateTime" />
                        <asp:BoundField DataField="EventType" HeaderText="Event Type" SortExpression="EventType" />
                        <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                        
                        <asp:TemplateField HeaderText="Edit">
                            <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" CommandName="edit_item" ImageUrl="~/Admin/Images/ico_edit.gif"
                                    ID="Imagebutton1" AlternateText="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SystemEventID") %>'>
                                </asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="CMS.SELECT_SystemEvents" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>