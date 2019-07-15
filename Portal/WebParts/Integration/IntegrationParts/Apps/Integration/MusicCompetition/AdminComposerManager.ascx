<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminComposerManager.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.MusicCompetition.AdminComposerManagerView" %>
<div class="control-box no-bottom-margin">
    <div>
        <asp:DropDownList ID="cboCompetition" CssClass="input" AppendDataBoundItems="true" AutoPostBack="true" runat="server" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="cboCompetition_SelectedIndexChanged">
            <asp:ListItem Text="- Select Competition -" Value="-1"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="cmdNew" runat="server" Text="New Composer" CssClass="btn btn-default btn-sm" OnClick="cmdNew_Click" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" runat="server" CssClass="input"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" CssClass="btn btn-default btn-sm" />&nbsp;<asp:Button
                ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default btn-sm" />
        </div>
    </div>
</div>
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
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="NickName" HeaderText="Nick Name" SortExpression="NickName"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Locale" HeaderText="Locale" SortExpression="Locale"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Entry" HeaderText="Entry" SortExpression="Entry"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Work" HeaderText="Work" SortExpression="Work"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="PhotoFile" HeaderText="Photo File" SortExpression="PhotoFile"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Competition" HeaderText="Competition" SortExpression="Competition"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="IsActive" ItemStyle-HorizontalAlign="Center" HeaderText="Active"
            SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center" />
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
    TypeName="WCMS.WebSystem.Apps.MusicCompetition.AdminComposerManagerView">
    <SelectParameters>
        <asp:ControlParameter ControlID="cboCompetition" DefaultValue="-2" Name="competitionId" PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>