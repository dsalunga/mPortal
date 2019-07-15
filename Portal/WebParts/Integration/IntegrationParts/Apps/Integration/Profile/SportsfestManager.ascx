<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SportsfestManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.SportsfestManager" %>
<div class="control-box">
    <div>
        <asp:Button ID="cmdDownload" runat="server" Text="Download (CSV)" CssClass="Command"
            OnClick="cmdDownload_Click" />
        <asp:Button ID="cmdDownloadXml" runat="server" Text="Download (XML)" CssClass="Command"
            OnClick="cmdDownloadXml_Click" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" runat="server"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" />&nbsp;<asp:Button
                ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" Width="55px" />
        </div>
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
        PageSize="15">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="center" Width="18px" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                        CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');"
                        ToolTip="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="GroupColor" HeaderText="Color" SortExpression="GroupColor"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ShirtSize" HeaderText="T-Shirt" SortExpression="ShirtSize" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Locale" HeaderText="Locale" SortExpression="Locale" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Suggestion" HeaderText="Suggestion" SortExpression="Suggestion"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="EntryDate" HeaderText="Date" SortExpression="EntryDate"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Sports" HeaderText="Sports" SortExpression="Sports" HeaderStyle-HorizontalAlign="Left" />
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
        TypeName="WCMS.WebSystem.WebParts.Profile.SportsfestManager">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>