<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Media.CMS_Media" Codebehind="CMS_Media.ascx.cs" %>
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
					<asp:BoundColumn Visible="False" DataField="MediaID" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="MediaID" HeaderText="&lt;input type=&quot;checkbox&quot; name=&quot;chkCheckedMain&quot; onclick=&quot;CheckAll(this, 'chkChecked');&quot;&gt;"
						DataFormatString="&lt;input type=&quot;checkbox&quot; value=&quot;{0}&quot; name=&quot;chkChecked&quot;&gt;">
						<HeaderStyle Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Name" HeaderText="Title"></asp:BoundColumn>
					<asp:BoundColumn DataField="MediaVersion" HeaderText="Brand"></asp:BoundColumn>
					<asp:BoundColumn DataField="Agency" HeaderText="Blurb"></asp:BoundColumn>
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
		<td class="control_box"><asp:button id="cmdDelete" runat="server" Width="96px" Text="Remove" onclick="cmdDelete_Click"></asp:button></td>
	</tr>
	<tr>
		<td height="25">&nbsp;</td>
	</tr>
	<tr>
		<td><strong>All Ads:</strong>
		</td>
	</tr>
	<tr>
		<td class="control_box">
			<table border="0" width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td width="100%">By Category:&nbsp;
						<asp:dropdownlist id="cboTypes" runat="server"></asp:dropdownlist>&nbsp;By 
						Author:&nbsp;
						<asp:dropdownlist id="cboWriters" runat="server"></asp:dropdownlist>&nbsp;
						<asp:button id="cmdView" runat="server" Width="80px" Text="Show" onclick="cmdView_Click"></asp:button></td>
					<td nowrap>Show Content:&nbsp;
						<asp:DropDownList id="cboSites" runat="server" AutoPostBack="True" onselectedindexchanged="cboSites_SelectedIndexChanged"></asp:DropDownList></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><asp:datagrid id="grdAds" AutoGenerateColumns="False" runat="server" Width="100%" BorderColor="#CCCCCC"
				BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" PageSize="15" AllowPaging="True">
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="grid_header"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="MediaID" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="MediaID" HeaderText="&lt;input type=&quot;checkbox&quot; name=&quot;chkCheckedMainAll&quot; onclick=&quot;CheckAll(this, 'chkCheckedAll');&quot;&gt;"
						DataFormatString="&lt;input type=&quot;checkbox&quot; value=&quot;{0}&quot; name=&quot;chkCheckedAll&quot;&gt;">
						<HeaderStyle Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Name" HeaderText="Title"></asp:BoundColumn>
					<asp:BoundColumn DataField="MediaVersion" HeaderText="Brand"></asp:BoundColumn>
					<asp:BoundColumn DataField="Agency" HeaderText="Blurb"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton Runat="server" CommandName="edit" ImageUrl="../../_CMS/images/ico_edit.gif" ID="Imagebutton3"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Left" Position="TopAndBottom" PageButtonCount="25" CssClass="grid_pager"
					Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
	<tr>
		<td class="control_box"><asp:button id="cmdInsert" runat="server" Width="100px" Text="Insert" onclick="btnInsert_Click"></asp:button></td>
	</tr>
</table>
