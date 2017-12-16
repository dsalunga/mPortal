<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Newsletter.CMS_eNewsletter" Codebehind="CMS_Enewsletter.ascx.cs" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="control_box">
            <asp:Button ID="btnDelete" runat="server" Width="85px" Text="Delete" OnClick="btnDelete_Click"
                Height="30px" />
            <asp:Button ID="btnDownloadCSV" runat="server" Text="Download List" OnClick="btnDownloadCSV_Click"
                Height="30px" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None"
                Width="100%" AutoGenerateColumns="False" DataKeyNames="eNewsletterID" OnRowCommand="GridView1_RowCommand"
                PageSize="15">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" value='<%# Eval("eNewsletterID") %>' name="chkChecked" />
                        </ItemTemplate>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active" SortExpression="IsActive">
                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# des.WebHelper.SetStateImage(DataBinder.Eval(Container, "DataItem.IsActive")) %>'
                                ID="Image1"></asp:Image>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="EmailAddress" HeaderText="E-mail Address" SortExpression="EmailAddress" />
                </Columns>
                <PagerSettings PageButtonCount="50" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="Newsletter.SELECT_eNewsLetterAll" SelectCommandType="StoredProcedure">
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
