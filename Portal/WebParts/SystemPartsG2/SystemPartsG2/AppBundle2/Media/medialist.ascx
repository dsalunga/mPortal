<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Media.MediaList" enableViewState="False" Codebehind="MediaList.ascx.cs" %>
<table width="100%" border="0" cellspacing="2" cellpadding="0">
	<tr>
		<td valign="top" class="content">
			<table width="100%" border="0" cellspacing="2" cellpadding="0">
				<tr>
					<td align="center" valign="top"><asp:Panel ID="pPlayer" Runat="server" Visible="False">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>
										<asp:Literal id="lLoadedMedia" runat="server"></asp:Literal></TD>
								</TR>
								<TR>
									<TD height="5"></TD>
								</TR>
								<TR>
									<TD class="portalh" align="center">
										<asp:Literal id="lLoadedMediaTitle" runat="server"></asp:Literal></TD>
								</TR>
								<TR>
									<TD class="contentb" align="center">
										<asp:Literal id="lBlurb" runat="server"></asp:Literal></TD>
								</TR>
								<TR>
									<TD>
										<HR noShade SIZE="1">
									</TD>
								</TR>
							</TABLE>
						</asp:Panel>
					</td>
				</tr>
				<tr>
					<td valign="top">
						<table width="100%" border="0" cellspacing="2" cellpadding="0">
							<asp:Repeater id="rProducts" runat="server">
								<ItemTemplate>
									<tr>
										<td valign="top">
											<table width="100%" border="0" cellspacing="2" cellpadding="0">
												<tr valign="top">
													<td width="26%" align="center"><img src='/Assets/Uploads/Image/SECTIONS/MG/<%# DataBinder.Eval(Container.DataItem, "Thumbnail") %>' width="100"></td>
													<td width="74%" class="content"><span class="contentb">Title:&nbsp;</span><%# DataBinder.Eval(Container.DataItem, "Name") %><br>
														<span class="contentb">Brand:&nbsp;</span><%# DataBinder.Eval(Container.DataItem, "MediaVersion") %><br>
														<span class="contentb"></span>
														<%# DataBinder.Eval(Container.DataItem, "Agency") %>
														<br>
														<br>
														<a href='<%# CreateLink(DataBinder.Eval(Container.DataItem, "MediaID").ToString()) %>' class="portall" title="Watch Video">
															Watch Video</a></td>
												</tr>
											</table>
										</td>
									</tr>
								</ItemTemplate>
								<SeparatorTemplate>
									<tr>
										<td height="1"><hr noshade size="1">
										</td>
									</tr>
								</SeparatorTemplate>
							</asp:Repeater>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
