<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebTemplates.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Template.WebTemplates" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="../Controls/WebThemeHome.ascx" TagName="WebThemeHome" TagPrefix="uc1" %>

<uc1:WebThemeHome ID="WebThemeHome1" runat="server" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-default" Text="Add" OnClick="cmdAdd_Click" />
        <asp:Button ID="cmdParseDirectory" runat="server" Text="Parse Templates" CssClass="btn btn-default"
            OnClick="cmdParseDirectory_Click" />
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
        PageSize="20">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Actions" Visible="false">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' /><asp:ImageButton ID="ImageButton2"
                            runat="server" CommandName="View_Placeholders" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                            AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' Visible="false" /><asp:ImageButton ID="ImageButton4"
                                runat="server" CommandName="DeleteItem" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                                AlternateText="Delete" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Name" HeaderText="Name"
                SortExpression="NameUrl" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <%--<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />--%>
            <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Identity" HeaderText="Folder Name" SortExpression="Identity"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ThemeName" HeaderText="Theme" SortExpression="ThemeName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ParentName" HeaderText="Parent" SortExpression="ParentName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PanelName" HeaderText="Primary Panel" SortExpression="PanelName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="SkinName" HeaderText="Default Skin" SortExpression="SkinName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Standalone" SortExpression="Standalone">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Standalone")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Central.Template.WebTemplates">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-2" Name="themeId" QueryStringField="ThemeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
