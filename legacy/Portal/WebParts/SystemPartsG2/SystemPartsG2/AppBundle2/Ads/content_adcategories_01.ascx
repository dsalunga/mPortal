<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Ads.CONTENT_AdCategories_01" Codebehind="CONTENT_AdCategories_01.ascx.cs" %>
<table width="100%">
	<tr>
		<td vAlign="middle" noWrap class="control_box"><asp:button id="cmdAdd" runat="server" Width="80px" Text="Add" onclick="cmdAdd_Click"></asp:button>&nbsp;
			<asp:button id="cmdDelete" runat="server" Width="80px" Text="Delete" onclick="cmdDelete_Click"></asp:button></td>
	</tr>
	<tr>
		<td><asp:datagrid id="grd" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3"
				BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#CCCCCC" PageSize="15"
				AllowPaging="True">
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="grid_header"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="AdCategoryID" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="AdCategoryID" HeaderText="&lt;input type=&quot;checkbox&quot; name=&quot;chkCheckedMain&quot; onclick=&quot;CheckAll(this, 'chkChecked');&quot;&gt;"
						DataFormatString="&lt;input type=&quot;checkbox&quot; value=&quot;{0}&quot; name=&quot;chkChecked&quot;&gt;">
						<HeaderStyle Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Name" HeaderText="Category Name"></asp:BoundColumn>
					<asp:TemplateColumn Visible="False" HeaderText="Items">
						<HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton Runat="server" CommandName="items" ImageUrl="../../_CMS/images/ico_pages.gif" ID="Imagebutton2"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton Runat="server" CommandName="edit" ImageUrl="../../_CMS/images/ico_edit.gif" ID="Imagebutton1"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Left" Position="TopAndBottom" CssClass="grid_pager" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
</table>
