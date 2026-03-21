<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartControls.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartControls" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="../Controls/WebPartTab.ascx" TagName="WebPartTab" TagPrefix="uc1" %>

<uc1:WebPartTab ID="WebPartTab1" runat="server" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdAdd" runat="server" Text="Add" OnClick="cmdAdd_Click" CssClass="btn btn-default" />
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="true" AllowSorting="True" CssClass="table table-borderless"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
        PageSize="50">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    AlternateText="Edit" ToolTip="Edit" CommandArgument='<%# Eval("Id") %>' />--%>
                    <asp:ImageButton ID="ImageButton4" runat="server" CommandName="Templates" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                        AlternateText="Templates" ToolTip="Templates" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                        AlternateText="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="NameUrl" HeaderText="Name" SortExpression="Name" DataTextField="Name" />
            <%--<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />--%>
            <asp:BoundField DataField="Identity" HeaderText="Identity" SortExpression="Identity"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PartAdmin" HeaderText="Admin" SortExpression="PartAdmin"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ConfigFileName" HeaderText="Config (Deprecated)" SortExpression="ConfigFileName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Entry Point" SortExpression="EntryPoint">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("EntryPoint")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="QuickPreviewUrl" Target="_blank" HeaderText="Quick Preview" Text="Preview" />
        </Columns>
    </asp:GridView>
</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.WebPartControls">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="-1" Name="partId" QueryStringField="PartId"
            Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
