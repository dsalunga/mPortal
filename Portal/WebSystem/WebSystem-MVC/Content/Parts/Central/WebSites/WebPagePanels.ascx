<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPagePanels.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebPagePanels" %>
<%@ Register Src="../Controls/WebGenericTab.ascx" TagName="WebGenericTab" TagPrefix="uc1" %>
<uc1:WebGenericTab ID="WebGenericTab1" runat="server" />
<br />
<div>
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless" AllowPaging="True" AllowSorting="True"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField Visible="false">
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
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton4" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' ToolTip="Edit" />
                    <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="View Elements" CommandName="View-Elements" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                        AlternateText="View Elements" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PanelUsage" HeaderText="Panel Usage" SortExpression="PanelUsage"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PanelName" HeaderText="Panel Name" SortExpression="PanelName"
                HeaderStyle-HorizontalAlign="Left" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Central.WebSites.WebPagePanels">
        <SelectParameters>
            <asp:QueryStringParameter Name="pageId" QueryStringField="PageId" Type="Int32" DefaultValue="-1" />
            <asp:QueryStringParameter Name="masterId" QueryStringField="MasterPageId" Type="Int32"
                DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
</div>
