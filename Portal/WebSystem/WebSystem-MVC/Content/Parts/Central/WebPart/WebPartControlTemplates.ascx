<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartControlTemplates.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartControlTemplatesController" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>

<%@ Register Src="../Controls/WebPartControlTab.ascx" TagName="WebPartControlTab"
    TagPrefix="uc1" %>
<uc1:WebPartControlTab ID="WebPartControlTab1" runat="server" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-default" Text="Add" OnClick="cmdAdd_Click" />
        <%--<asp:Button ID="cmdDone" runat="server" Text="Done" Height="30px" Width="85px" OnClick="cmdDone_Click"
                Font-Bold="True" />--%>
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
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
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                        AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="FileName" HeaderText="File" SortExpression="FileName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Identity" HeaderText="Identity" SortExpression="Identity"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="TemplateEngine" HeaderText="Engine" SortExpression="TemplateEngine"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:HyperLinkField DataNavigateUrlFields="QuickPreviewUrl" Target="_blank" HeaderText="Quick Preview" Text="Preview" />
            <asp:TemplateField HeaderText="Standalone" SortExpression="Standalone">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <img src="<%# WebHelper.SetStateImageInt(Eval("Standalone")) %>" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Central.WebPartControlTemplatesController">
        <SelectParameters>
            <asp:QueryStringParameter Name="partControlId" QueryStringField="PartControlId" Type="Int32"
                DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
