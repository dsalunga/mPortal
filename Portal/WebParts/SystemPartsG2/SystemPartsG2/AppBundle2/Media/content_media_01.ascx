<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Media.CONTENT_Media_01" Codebehind="CONTENT_Media_01.ascx.cs" %>
<table width="100%">
	<tr>
		<td vAlign="middle" noWrap class="control_box"><asp:button id="cmdAdd" Text="Add" Width="80px" Height="24px" runat="server" onclick="cmdAdd_Click"></asp:button>&nbsp;
			<asp:button id="cmdDelete" Text="Delete" Width="80px" Height="24px" runat="server" onclick="cmdDelete_Click"></asp:button></td>
	</tr>
	<tr>
		<td><asp:datagrid id="grd" Width="100%" runat="server" AllowPaging="True" PageSize="15" BorderColor="#CCCCCC"
				BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AutoGenerateColumns="False">
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
					<asp:BoundColumn DataField="Rank" HeaderText="Rank">
						<HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Active">
						<HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:Image Runat="server" ImageUrl='<%# SetStateImage(DataBinder.Eval(Container, "DataItem.IsActive")) %>' ID="Image2">
							</asp:Image>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="Name" HeaderText="Title"></asp:BoundColumn>
					<asp:BoundColumn DataField="MediaVersion" HeaderText="Brand">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="MediaLength" HeaderText="Length"></asp:BoundColumn>
					<asp:BoundColumn DataField="Agency" HeaderText="Blurb"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton Runat="server" CommandName="edit" ImageUrl="../../_CMS/images/ico_edit.gif" ID="Imagebutton1"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Left" Position="TopAndBottom" PageButtonCount="25" CssClass="grid_pager"
					Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</tr>
</table>
