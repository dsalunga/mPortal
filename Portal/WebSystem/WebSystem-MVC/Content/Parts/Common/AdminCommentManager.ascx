<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminCommentManager.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Common.AdminCommentManager" %>
<asp:HiddenField runat="server" ID="hObjectId" Value="-1" />
<asp:HiddenField runat="server" ID="hRecordId" Value="-1" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdDelete" runat="server" Text="Delete" CssClass="btn btn-default" OnClick="cmdDelete_Click" OnClientClick="return confirm('Are you you want to delete the selected items?');" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" runat="server"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-default" Text="Search" OnClick="cmdSearch_Click" />&nbsp;<asp:Button
                ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default" />
        </div>
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" CssClass="table table-borderless" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
        PageSize="15">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                </HeaderTemplate>
                <ItemTemplate>
                    <input type='checkbox' value='<%# Eval("Id") %>' name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="center" Width="40px" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                        CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="UserName" HeaderText="User" SortExpression="UserName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="UserEmail" HeaderText="Email" SortExpression="UserEmail"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Content" HeaderText="Message" SortExpression="Content"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="DateCreated" HeaderText="Date & Time" SortExpression="DateCreated"
                HeaderStyle-HorizontalAlign="Left" />
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Common.AdminCommentManager">
        <SelectParameters>
            <asp:ControlParameter ControlID="hObjectId" DefaultValue="-1" Name="objectId" PropertyName="Value" Type="Int32" />
            <asp:ControlParameter ControlID="hRecordId" DefaultValue="-1" Name="recordId" PropertyName="Value" Type="Int32" />
            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>