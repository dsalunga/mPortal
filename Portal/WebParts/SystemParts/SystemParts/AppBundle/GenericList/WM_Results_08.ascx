<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.CMS_Results"
    CodeBehind="WM_Results_08.ascx.cs" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="ControlBox">
            <asp:Button ID="cmdDelete" CssClass="Command" runat="server" Text="Delete" OnClick="cmdDelete_Click">
            </asp:Button>
            <asp:Button ID="cmdDownload" runat="server" CssClass="Command" Text="Download XML"
                OnClick="cmdDownload_Click"></asp:Button>
            <asp:Button ID="cmdDone" runat="server" Text="Done" OnClick="cmdDone_Click" CssClass="Command" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                CellPadding="4" ForeColor="#333333" Width="100%" AutoGenerateColumns="False"
                DataKeyNames="ResponseID" OnRowCommand="GridView1_RowCommand" PageSize="14" DataSourceID="ObjectDataSource1"
                BorderStyle="None">
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
                            <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type='checkbox' value='<%# Eval("ResponseID") %>' name='chkChecked' />
                        </ItemTemplate>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Answers">
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandArgument='<%# Eval("ResponseID") %>' CommandName="Custom_Edit"
                                ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif" ID="Imagebutton2" NAME="Imagebutton2" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ResponseID" HeaderText="Response ID" ItemStyle-Width="100px"
                        SortExpression="ResponseID" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DateTimeTaken" HeaderText="Date &amp; Time Taken" SortExpression="DateTimeTaken"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                TypeName="WCMS.WebSystem.WebParts.GenericForm.CMS_Results">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="-1" Name="ListId" QueryStringField="ListId"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
</table>
