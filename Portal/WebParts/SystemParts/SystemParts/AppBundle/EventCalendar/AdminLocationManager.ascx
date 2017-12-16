<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminLocationManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.EventCalendar.AdminLocationManager" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<uc1:TabControl ID="tabView" runat="server" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdNew" runat="server" Text="New Location" CssClass="btn btn-default btn-sm" OnClick="cmdNew_Click" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" runat="server" CssClass="input"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" CssClass="btn btn-default btn-sm" />&nbsp;<asp:Button
                ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default btn-sm" />
        </div>
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
        PageSize="15">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="center" Width="40px" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton4" AlternateText="Edit Item" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                        CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Bookable" HeaderText="Bookable" SortExpression="Bookable" HeaderStyle-HorizontalAlign="Left" />
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
        TypeName="WCMS.WebSystem.WebParts.EventCalendar.AdminLocationManager">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>
