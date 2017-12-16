<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Ads.CONTENT_AdItems_03" Codebehind="CONTENT_AdItems_03.ascx.cs" %>
<table width="100%">
	<tr>
		<td vAlign="middle" noWrap class="control_box"><asp:button id="cmdAdd" runat="server" Width="80px" Text="Add" onclick="cmdAdd_Click"></asp:button>&nbsp;
			<asp:button id="cmdDelete" runat="server" Width="80px" Text="Delete" onclick="cmdDelete_Click"></asp:button>&nbsp;
			<asp:button id="cmdRender" runat="server" Width="88px" Text="Render XML" onclick="cmdRender_Click"></asp:button>&nbsp;
			<asp:button id="cmdDone" runat="server" Width="80px" Text="Done" onclick="cmdDone_Click"></asp:button>&nbsp;
			<asp:Label id="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
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
					<asp:BoundColumn Visible="False" DataField="AdItemID" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="AdItemID" HeaderText="&lt;input type=&quot;checkbox&quot; name=&quot;chkCheckedMain&quot; onclick=&quot;CheckAll(this, 'chkChecked');&quot;&gt;"
						DataFormatString="&lt;input type=&quot;checkbox&quot; value=&quot;{0}&quot; name=&quot;chkChecked&quot;&gt;">
						<HeaderStyle Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="AlternateText" HeaderText="Alternate Text"></asp:BoundColumn>
					<asp:BoundColumn DataField="NavigateUrl" HeaderText="Link"></asp:BoundColumn>
					<asp:BoundColumn DataField="ImageUrl" HeaderText="Image"></asp:BoundColumn>
					<asp:BoundColumn DataField="Keyword" HeaderText="Keyword"></asp:BoundColumn>
					<asp:BoundColumn DataField="Impressions" HeaderText="Impressions"></asp:BoundColumn>
					<asp:BoundColumn DataField="Hits" HeaderText="Hits">
						<HeaderStyle Width="100px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Appearance" HeaderText="Appearance">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
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
