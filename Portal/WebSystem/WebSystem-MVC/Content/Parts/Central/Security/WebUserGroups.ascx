﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserGroups.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.WebUserGroups" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="../Controls/WebUserTab.ascx" TagName="WebUserTab" TagPrefix="uc1" %>

<uc1:WebUserTab ID="WebUserTab1" runat="server" />
<div id="rowControls" class="controls control-box" runat="server">
    <div>
        <asp:Button ID="cmdAdd" runat="server" Text="Add..." OnClientClick="return Add_Click();"
            ClientIDMode="Static" OnClick="cmdAdd_Click" CssClass="btn btn-default btn-sm" />
        <asp:TextBox ID="txtId" runat="server" CssClass="input" Columns="50" ClientIDMode="Static"></asp:TextBox>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
    GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" PageSize="15">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Actions">
            <HeaderStyle HorizontalAlign="Center" Width="75px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                    CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Name"
            SortExpression="Name" />
        <asp:BoundField DataField="DateJoined" HeaderStyle-HorizontalAlign="Left" HeaderText="Date Joined"
            SortExpression="DateJoined" />
        <asp:TemplateField HeaderText="Active" SortExpression="Active">
            <HeaderStyle HorizontalAlign="Center" Width="30px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton ToolTip="Toggle Active Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                    ID="Image1" CommandName="ToggleActive" CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
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
    TypeName="WCMS.WebSystem.WebParts.Central.Security.WebUserGroups">
    <SelectParameters>
        <asp:QueryStringParameter Name="userId" QueryStringField="UserId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<script type="text/javascript">
    function Add_Click() {
        var addValue = $("#txtId").val().Trim();

        if (addValue == "") {
            ShowAccountBrowser("txtId", <% =WCMS.Framework.WebObjects.WebGroup %>, 0, 0, 1, "cmdAdd");
            return false;
        }

        return true;
    }
</script>
