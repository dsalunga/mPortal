<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Ads.CONTENT_AdCategory_02" Codebehind="CONTENT_AdCategory_02.ascx.cs" %>
<table width="600">
	<tr>
		<td>
			<table width="100%">
				<tr>
					<td width="125">Category Name:</td>
					<td><asp:textbox id="txtName" runat="server" Width="100%"></asp:textbox></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<hr>
		</td>
	</tr>
	<tr>
		<td align="right" class="control_box"><asp:button id="cmdUpdate" runat="server" Width="80px" Text="Update" onclick="cmdUpdate_Click"></asp:button>&nbsp;
			<asp:button id="cmdCancel" runat="server" Width="80px" Text="Cancel" onclick="cmdCancel_Click"></asp:button></td>
	</tr>
</table>
