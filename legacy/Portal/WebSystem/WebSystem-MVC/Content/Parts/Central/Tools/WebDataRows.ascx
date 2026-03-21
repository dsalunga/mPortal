<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebDataRows.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.WebDataRows" %>
<style type="text/css">
    .HeaderStyleLeft th {
        text-align: left;
    }

    INPUT, TEXTAREA {
        font-family: Verdana, Arial;
        font-size: 12px; /* color: #333333; */ /* filter: alpha(opacity=50); */
    }

    th.HeaderStyleCenter {
        text-align: center;
    }
</style>
<h1 class="central page-header">
    Table Rows Editor
</h1>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        CellPadding="4" DataSourceID="sourceHeaders" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="True" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
        PageSize="50">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" CssClass="HeaderStyleLeft" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" value='<%# Eval("Id") %>' name="chkChecked">
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle CssClass="HeaderStyleCenter" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButtonEdit" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButtonDelete" runat="server" CommandName="Custom_Delete"
                        ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="sourceHeaders" runat="server" SelectMethod="Select" TypeName="WCMS.WebSystem.WebParts.Central.Tools.WebDataRows">
        <SelectParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="Id" Type="Int32" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
