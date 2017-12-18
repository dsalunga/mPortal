<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebParameterSets.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Misc.WebParameterSets" %>
<h2>Parameter Sets</h2>
<div class="control-box">
    <div class="no-bottom-margin">
        <asp:Button ID="cmdNew" runat="server" Text="Create New" CssClass="btn btn-sm btn-default" OnClick="cmdNew_Click" />
        <div class="pull-right">
            <asp:DropDownList ID="cboSites" CssClass="input" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboSites_SelectedIndexChanged">
                <asp:ListItem Text="" Value="-2"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
    CellPadding="4" DataKeyNames="Id" ForeColor="#333333" GridLines="None" Width="100%"
    OnRowCommand="GridView1_RowCommand" DataSourceID="ObjectDataSource1">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="">
            <HeaderStyle HorizontalAlign="center" Width="18px" />
            <ItemStyle HorizontalAlign="center" />
            <ItemTemplate>
                <%--<asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    ID="Imagebutton4" AlternateText="View Properties" ToolTip="Edit Properties" CommandArgument='<%# Eval("Id") %>' />--%>
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="View_Parameters" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                    AlternateText="View Parameters" ToolTip="View Parameters" CommandArgument='<%# Eval("Id") %>' />
                <%--<asp:ImageButton ID="ImageButton2" Visible="false" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                    CommandArgument='<%# Eval("Id") %>' ToolTip="Delete" OnClientClick="return confirm('Are you you want to delete this item?');" />--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Name" HeaderText="Name"
            SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
        <%--<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />--%>
        <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" HeaderStyle-HorizontalAlign="Left" />
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
    TypeName="WCMS.WebSystem.WebParts.Central.Misc.WebParameterSets"></asp:ObjectDataSource>
