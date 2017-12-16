<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Ads.CMS_Ad" Codebehind="CMS_Ad.ascx.cs" %>
<table width="100%" border="0">
	<tr>
		<td><strong>Selected Ad:</strong>
		</td>
	</tr>
	<tr>
		<td><asp:datagrid id="grd" AutoGenerateColumns="False" runat="server" Width="100%" BorderColor="#CCCCCC"
				BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3">
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="grid_header"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="AdID" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="AdID" DataFormatString="&lt;input type=&quot;radio&quot; value=&quot;{0}&quot; name=&quot;chkChecked&quot;&gt;">
						<HeaderStyle Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Name" HeaderText="Ad Name"></asp:BoundColumn>
					<asp:BoundColumn DataField="Hits" HeaderText="Hits">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="FirstName" HeaderText="User"></asp:BoundColumn>
					<asp:BoundColumn DataField="AdCategoryName" HeaderText="Category"></asp:BoundColumn>
					<asp:BoundColumn DataField="AdvertisementFile" HeaderText="XML File"></asp:BoundColumn>
					<asp:TemplateColumn Visible="False" HeaderText="Items">
						<HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton Runat="server" CommandName="items" ImageUrl="../../_CMS/images/ico_pages.gif" ID="Imagebutton1"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText="Edit">
						<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton Runat="server" CommandName="edit" ImageUrl="../../_CMS/images/ico_edit.gif" ID="Imagebutton4"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Left" CssClass="grid_pager" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
	<tr>
		<td><asp:button id="cmdDelete" runat="server" Width="96px" Text="Remove" Visible="False"></asp:button></td>
	</tr>
	<tr>
		<td height="25">&nbsp;</td>
	</tr>
	<tr>
		<td><strong>All Ads:</strong>
		</td>
	</tr>
	<tr>
		<td>By Category:&nbsp;
			<asp:dropdownlist id="cboTypes" runat="server"></asp:dropdownlist>&nbsp;By 
			Author:&nbsp;
			<asp:dropdownlist id="cboWriters" runat="server"></asp:dropdownlist>&nbsp;
			<asp:button id="cmdView" runat="server" Width="80px" Text="Show" onclick="cmdView_Click"></asp:button></td>
	</tr>
	<tr>
		<td><asp:datagrid id="grdAds" AutoGenerateColumns="False" runat="server" Width="100%" BorderColor="#CCCCCC"
				BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" PageSize="15" AllowPaging="True">
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="grid_header"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="AdID" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="AdID" DataFormatString="&lt;input type=&quot;radio&quot; value=&quot;{0}&quot; name=&quot;chkChecked&quot;&gt;">
						<HeaderStyle Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Name" HeaderText="Ad Name"></asp:BoundColumn>
					<asp:BoundColumn DataField="Hits" HeaderText="Hits">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="FirstName" HeaderText="User"></asp:BoundColumn>
					<asp:BoundColumn DataField="AdCategoryName" HeaderText="Category"></asp:BoundColumn>
					<asp:BoundColumn DataField="AdvertisementFile" HeaderText="XML File"></asp:BoundColumn>
					<asp:TemplateColumn Visible="False" HeaderText="Items">
						<HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton Runat="server" CommandName="items" ImageUrl="../../_CMS/images/ico_pages.gif" ID="Imagebutton2"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText="Edit">
						<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton Runat="server" CommandName="edit" ImageUrl="../../_CMS/images/ico_edit.gif" ID="Imagebutton3"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Left" Position="TopAndBottom" CssClass="grid_pager" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
	<tr>
		<td><asp:button id="cmdInsert" runat="server" Width="100px" Text="Insert" onclick="btnInsert_Click"></asp:button></td>
	</tr>
</table>
