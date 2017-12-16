<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Media.CONTENT_Media_02" Codebehind="CONTENT_Media_02.ascx.cs" %>
<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<table width="800">
	<tr>
		<td>
			<table width="100%">
				<tr>
					<td width="100">
						Title:</td>
					<td><asp:textbox id="txtName" Width="100%" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td vAlign="top">Brand:</td>
					<td><asp:textbox id="txtVersion" Width="100%" runat="server"></asp:textbox></td>
				</tr>
				<!--
				<tr>
					<td vAlign="top">Length:</td>
					<td><asp:textbox id="txtLength" Width="100%" runat="server"></asp:textbox></td>
				</tr>
				-->
				<tr>
					<td vAlign="top">Blurb:</td>
					<td><asp:textbox id="txtAgency" Width="100%" runat="server" TextMode="MultiLine" Rows="4"></asp:textbox></td>
				</tr>
				<tr>
					<td vAlign="top">Content:</td>
					<td><FCKEDITORV2:FCKEDITOR id="txtContent" runat="server" Height="600px"></FCKEDITORV2:FCKEDITOR></td>
				</tr>
				<tr>
					<td>Thumbnail:</td>
					<td><asp:textbox id="txtFilename" Width="168px" runat="server"></asp:textbox>&nbsp; <INPUT id="cmdFile" style="WIDTH: 88px; HEIGHT: 20px" type="button" value="Upload" name="cmdFile"
							runat="server"></td>
				</tr>
				<tr>
					<td>Owner Site:</td>
					<td><asp:DropDownList id="cboSites" runat="server"></asp:DropDownList></td>
				<tr>
					<td>Rank:</td>
					<td><asp:dropdownlist id="cboRank" runat="server"></asp:dropdownlist><asp:checkbox id="chkActive" runat="server" Text="Active"></asp:checkbox></td>
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
		<td align="right" class="control_box"><asp:button id="cmdUpdate" Width="80px" runat="server" Text="Update" onclick="cmdUpdate_Click"></asp:button>&nbsp;
			<asp:button id="cmdCancel" Width="80px" runat="server" Text="Cancel" onclick="cmdCancel_Click"></asp:button></td>
	</tr>
</table>
