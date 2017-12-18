<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebObjectPermissions.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.WebObjectPermissions" %>
<h1 class="central page-header">
    Object Permissions
</h1>
<div class="control-box">
    <div>
        <asp:Button CssClass="btn btn-default" ID="cmdDelete" runat="server" Text="Remove Permission" OnClick="cmdDelete_Click" />
        <asp:Button ID="cmdDone" runat="server" CssClass="btn btn-default" Text="Back" OnClick="cmdDone_Click" />
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
    CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None"
    AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
    Width="100%" PageSize="15">
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
                <input type='checkbox' value='<%# Eval("Id")%>' name='chkChecked' />
            </ItemTemplate>
            <HeaderStyle Width="15px" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
    </Columns>
    <PagerSettings PageButtonCount="25" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="CMS.SELECT_RolePermissions" SelectCommandType="StoredProcedure"
    CancelSelectOnNullParameter="True">
    <SelectParameters>
        <asp:ControlParameter ControlID="hidRecordId" Name="recordId" PropertyName="Value"
            Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:HiddenField ID="hidRecordId" runat="server" />
<asp:HiddenField ID="hidObjectId" runat="server" />
<br />
<br />
<br />
<div class="big_bold">
    Available Permissions
</div>
<div class="control-box">
    <div>
        <asp:Button ID="cmdInsert" runat="server" CssClass="btn btn-default" Text="Grant Permission" OnClick="cmdInsert_Click" />
    </div>
</div>
<asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
    CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None"
    AutoGenerateColumns="False" DataKeyNames="Id" Width="100%" PageSize="15">
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
                <input type="checkbox" name="chkCheckedMain2" onclick="CheckAll(this, 'chkChecked2');" />
            </HeaderTemplate>
            <ItemTemplate>
                <input type='checkbox' value='<%# Eval("Id")%>' name='chkChecked2' />
            </ItemTemplate>
            <HeaderStyle Width="15px" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
    </Columns>
    <PagerSettings PageButtonCount="25" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="CMS.SELECT_Permissions" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="True">
    <SelectParameters>
        <asp:ControlParameter ControlID="hiddenRoleID" Name="RoleName" PropertyName="Value"
            Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
