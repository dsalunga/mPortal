<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Ads.CONTENT_AdItem_04" Codebehind="CONTENT_AdItem_04.ascx.cs" %>
<table width="600">
	<tr>
		<td>
			<table width="100%">
				<tr>
					<td width="125">Alternate Text:</td>
					<td><asp:textbox id="txtAlternateText" runat="server" Width="100%"></asp:textbox></td>
				</tr>
				<tr>
					<td>Image:</td>
					<td><asp:textbox id="txtFilename" runat="server" Width="168px" ReadOnly="True"></asp:textbox>&nbsp;
						<INPUT id="cmdFile" style="WIDTH: 88px; HEIGHT: 20px" type="button" value="Upload" name="cmdFile"
							runat="server"></td>
				</tr>
				<tr>
					<td>Link:</td>
					<td><asp:textbox id="txtNavigateUrl" runat="server" Width="100%"></asp:textbox></td>
				</tr>
				<tr>
					<td>Keyword:</td>
					<td><asp:textbox id="txtKeyword" runat="server" Width="100%"></asp:textbox></td>
				</tr>
				<tr>
					<td>Impressions:</td>
					<td><asp:textbox id="txtImpressions" runat="server" Width="100%">100</asp:textbox></td>
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
