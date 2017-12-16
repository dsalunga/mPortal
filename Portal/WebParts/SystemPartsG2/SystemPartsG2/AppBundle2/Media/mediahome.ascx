<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Media.MediaHome" enableViewState="False" Codebehind="MediaHome.ascx.cs" %>
<table width="100%" border="0" cellspacing="2" cellpadding="0">
	<tr>
		<td valign="top" class="content">
			<table width="100%" border="0" cellspacing="2" cellpadding="0">
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
														<span class="contentb">Version:&nbsp;</span><%# DataBinder.Eval(Container.DataItem, "MediaVersion") %><br>
														<span class="contentb">Length:&nbsp;</span><%# DataBinder.Eval(Container.DataItem, "MediaLength") %><br>
														<span class="contentb">Agency:&nbsp;</span><%# DataBinder.Eval(Container.DataItem, "Agency") %><br>
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
										<td height="1"><hr noshade>
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