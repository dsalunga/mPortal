<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminSongScoreManager.ascx.cs" Inherits="WCMS.WebSystem.Apps.MusicCompetition.AdminSongScoreManager" %>
<div class="control-box no-bottom-margin">
    <div>
        <asp:DropDownList ID="cboCompetition" CssClass="input" AppendDataBoundItems="true" AutoPostBack="true" runat="server" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="cboCompetition_SelectedIndexChanged">
            <asp:ListItem Text="-- Select Competition --" Value="-1"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="cmdDelete" runat="server" Text="Delete" CssClass="btn btn-default btn-sm" OnClick="cmdDelete_Click" OnClientClick="return confirm('Are you you want to delete the selected items?');" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" runat="server" CssClass="input"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="btn btn-default btn-sm" OnClick="cmdSearch_Click" />&nbsp;<asp:Button
                ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default btn-sm" />
        </div>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
    GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
    PageSize="35">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
        <asp:TemplateField HeaderText="">
            <HeaderStyle HorizontalAlign="center" Width="20px" />
            <ItemStyle HorizontalAlign="center" />
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                    CommandArgument='<%# Eval("Id") %>' ToolTip="Delete" OnClientClick="return confirm('Are you you want to delete this item?');" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Entry" HeaderText="Song" SortExpression="Entry"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Judge" HeaderText="Judge" SortExpression="Judge"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Musicality" HeaderText="Musicality" SortExpression="Musicality"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="LyricsMessage" HeaderText="Lyrics/Message" SortExpression="LyricsMessage"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="OverallImpact" HeaderText="Over-all Impact" SortExpression="OverallImpact"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total"
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
    TypeName="WCMS.WebSystem.Apps.MusicCompetition.AdminSongScoreManager">
    <SelectParameters>
        <asp:ControlParameter ControlID="cboCompetition" DefaultValue="-2" Name="competitionId" PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>