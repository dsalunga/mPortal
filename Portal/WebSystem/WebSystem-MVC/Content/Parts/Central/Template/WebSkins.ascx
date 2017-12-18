<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSkins.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Template.WebSkins" %>
<%@ Register Src="../Controls/WebThemeHome.ascx" TagName="WebThemeHome" TagPrefix="uc1" %>
<%@ Register Src="../Controls/WebTemplateHome.ascx" TagName="WebTemplateHome" TagPrefix="uc1" %>
<uc1:WebTemplateHome ID="WebTemplateHome1" runat="server" Visible="false" />
<uc1:WebThemeHome ID="WebThemeHome1" runat="server" Visible="false" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdAdd" runat="server" Text="Add" OnClick="cmdAdd_Click"
            CssClass="btn btn-default" />
        <asp:Button ID="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete?');"
            runat="server" Text="Delete" OnClick="cmdDelete_Click" CssClass="btn btn-default" />
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True"
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
                <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
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
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.Template.WebSkins">
    <SelectParameters>
        <asp:QueryStringParameter Name="templateId" DefaultValue="-2" QueryStringField="TemplateId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>