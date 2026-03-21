<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WCMS.WebSystem.WebParts.Download._Sections_Download_CCMS_Downloasd_01" Codebehind="PM_Downloads_Master_01.ascx.cs" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="control_box">
            <asp:Button ID="cmdNew" Width="85px" runat="server" Text="New" OnClick="cmdNew_Click"
                Height="30px" />
            <asp:Button ID="cmdDelete" Width="85px" runat="server" Text="Delete" OnClick="cmdDelete_Click"
                Height="30px" Visible="False" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                AllowPaging="True" AllowSorting="True" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                DataKeyNames="DownloadID" OnRowCommand="GridView1_RowCommand" Width="100%">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField Visible="False">
                        <HeaderTemplate>
                            <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" value='<%# Eval("DownloadID") %>' name="chkChecked" />
                        </ItemTemplate>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/_CMS/Images/ico_edit.gif"
                                AlternateText="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.DownloadID") %>'>
                            </asp:ImageButton>
                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" ImageUrl="~/_CMS/Images/ico_exit.gif"
                                AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete the selected items?');"
                                CommandArgument='<%# DataBinder.Eval(Container, "DataItem.DownloadID") %>'></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Download" SortExpression="Name" />
                    <asp:HyperLinkField DataNavigateUrlFields="Filename" DataNavigateUrlFormatString="/Assets/Uploads/Image/SECTIONS/Download/{0}"
                        DataTextField="Filename" HeaderText="Filename" SortExpression="Filename" />
                    <asp:TemplateField HeaderText="Date" SortExpression="FileDate">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("FileDate", "{0:MMMM d, yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                    <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="Download.SELECT_Downloads" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </td>
    </tr>
</table>
