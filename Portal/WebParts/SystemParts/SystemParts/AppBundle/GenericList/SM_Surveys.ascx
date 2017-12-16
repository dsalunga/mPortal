<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.c_Surveys"
    CodeBehind="SM_Surveys.ascx.cs" %>
<div>
    <asp:DataGrid ID="grd" AutoGenerateColumns="False" runat="server" Width="100%" BorderColor="#CCCCCC"
        BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowPaging="True"
        PageSize="15">
        <FooterStyle ForeColor="#000066" BackColor="White" />
        <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
        <ItemStyle ForeColor="#000066" />
        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="grid_header"
            BackColor="#006699" />
        <Columns>
            <asp:BoundColumn Visible="False" DataField="ListId" HeaderText="ID"></asp:BoundColumn>
            <asp:BoundColumn DataField="ListId" HeaderText="&lt;input type=&quot;checkbox&quot; name=&quot;chkCheckedMain&quot; onclick=&quot;CheckAll(this, 'chkChecked');&quot;&gt;"
                DataFormatString="&lt;input type=&quot;checkbox&quot; value=&quot;{0}&quot; name=&quot;chkChecked&quot;&gt;">
                <HeaderStyle Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="Title" HeaderText="Selected Surveys" HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
            <asp:TemplateColumn Visible="False" HeaderText="Edit">
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton4" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
        <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" PageButtonCount="25"
            CssClass="grid_pager" Mode="NumericPages" />
    </asp:DataGrid>
</div>
<div class="control-box">
    <div>
        <asp:Button ID="cmdDelete" CssClass="btn btn-default" runat="server" Text="Remove" />
    </div>
</div>
<div>
    &nbsp;
</div>
<div>
    <table border="0" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="100%">
                <asp:Button ID="cmdSort" Text="Sort" runat="server" Width="56px" CausesValidation="False"></asp:Button>
                <asp:DropDownList ID="cboSort" runat="server">
                    <asp:ListItem Value="Name" Selected="True">Survey Title</asp:ListItem>
                    <asp:ListItem Value="CategoryName">Category</asp:ListItem>
                    <asp:ListItem Value="Rank">Rank</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cboOrder" runat="server">
                    <asp:ListItem Value="ASC" Selected="True">ASC</asp:ListItem>
                    <asp:ListItem Value="DESC">DESC</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td nowrap>Show Content:&nbsp;
                        <asp:DropDownList ID="cboSites" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>
<div>
    <asp:DataGrid ID="grdAds" AutoGenerateColumns="False" runat="server" Width="100%"
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White"
        CellPadding="3" PageSize="15" AllowPaging="True">
        <FooterStyle ForeColor="#000066" BackColor="White" />
        <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999" />
        <ItemStyle ForeColor="#000066" />
        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="grid_header"
            BackColor="#006699" />
        <Columns>
            <asp:BoundColumn Visible="False" DataField="ListId" HeaderText="ID"></asp:BoundColumn>
            <asp:BoundColumn DataField="ListId" HeaderText="&lt;input type=&quot;checkbox&quot; name=&quot;chkCheckedMainAll&quot; onclick=&quot;CheckAll(this, 'chkCheckedAll');&quot;&gt;"
                DataFormatString="&lt;input type=&quot;checkbox&quot; value=&quot;{0}&quot; name=&quot;chkCheckedAll&quot;&gt;">
                <HeaderStyle Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="Title" HeaderText="All Surveys" HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
            <asp:TemplateColumn Visible="False" HeaderText="Edit">
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton3" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
        <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" CssClass="grid_pager"
            Mode="NumericPages" />
    </asp:DataGrid>
</div>
<div class="control-box">
    <div>
        <asp:Button ID="cmdInsert" CssClass="btn btn-default" runat="server" Text="Insert"
            OnClick="cmdInsert_Click" />
    </div>
</div>
