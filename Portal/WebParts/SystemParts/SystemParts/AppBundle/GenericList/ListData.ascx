<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListData.ascx.cs" Inherits="WCMS.WebSystem.WebParts.GenericForm.ListData" %>
<div class="control-box">
    <div>
        <asp:Button ID="cmdDelete" CssClass="btn btn-default" runat="server" Text="Delete" OnClick="cmdDelete_Click"
            OnClientClick="return confirm('Are you sure you want to delete the selected items?');" />
        <asp:Button ID="cmdDownload" runat="server" CssClass="btn btn-default" Text="Download XML"
            OnClick="cmdDownload_Click" />
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        CellPadding="4" ForeColor="#333333" Width="100%" AutoGenerateColumns="False"
        DataKeyNames="Id" OnRowCommand="GridView1_RowCommand" PageSize="14" DataSourceID="ObjectDataSource1"
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
                    <input type='checkbox' value='<%# Eval("Id") %>' name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Results">
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Custom_Edit"
                        ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif" ID="Imagebutton2" NAME="Imagebutton2" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Id" HeaderText="Row ID" ItemStyle-Width="100px" SortExpression="Id"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="DateTimeTaken" HeaderText="Date &amp; Time Taken" SortExpression="DateTimeTaken"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.GenericForm.ListData">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="listId" QueryStringField="ListId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
