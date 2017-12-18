<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartTemplatePanels.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartTemplatePanels" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="../Controls/WebPartControlTemplateTab.ascx" TagName="WebPartControlTab"
    TagPrefix="uc1" %>

<uc1:WebPartControlTab ID="WebPartControlTemplateTab1" runat="server" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdAdd" CssClass="btn btn-default" runat="server" Text="Add New"
            OnClick="cmdAdd_Click" />
        <asp:Button ID="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete?');"
            runat="server" Text="Delete" OnClick="cmdDelete_Click" CssClass="btn btn-default" />
        <div class="pull-right">
            <asp:Button ID="cmdParse" OnClientClick="return confirm('This will parse the template file and inserts all placeholders found, continue?');"
                runat="server" Text="Parse" OnClick="cmdParse_Click" CssClass="btn btn-default" />
        </div>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" CssClass="table table-borderless"
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
        <asp:TemplateField HeaderText="Edit">
            <HeaderStyle HorizontalAlign="Center" Width="20px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton4" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="PanelName" HeaderText="Panel Name" SortExpression="PanelName"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" HeaderStyle-HorizontalAlign="Left" />
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetDataSet"
    TypeName="WCMS.WebSystem.WebParts.Central.WebPartTemplatePanels">
    <SelectParameters>
        <asp:QueryStringParameter Name="templateId" QueryStringField="PartControlTemplateId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>