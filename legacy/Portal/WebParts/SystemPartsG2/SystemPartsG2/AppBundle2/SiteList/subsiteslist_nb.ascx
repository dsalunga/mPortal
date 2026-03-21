<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.SiteList.SubsitesList" enableViewState="False" Codebehind="SubsitesList.ascx.cs" %>
<table width="100%" border="0" cellspacing="4" cellpadding="0">
	<asp:Repeater id="rList" runat="server">
		<ItemTemplate>
			<tr>
				<td width="75" align="center" valign="top"><img src='/Uploads/Image/LOGO/<%# DataBinder.Eval(Container.DataItem, "SiteImage") %>'></td>
				<td valign="top" class="content"><a onclick='<%# EvalLink(DataBinder.Eval(Container.DataItem, "IsActive")) %>' href='<%# DataBinder.Eval(Container.DataItem, "SiteURL") %>' title='<%# DataBinder.Eval(Container.DataItem, "SiteName") %>' class="contentbgb"><%# DataBinder.Eval(Container.DataItem, "SiteName") %></a><br>
					<%# DataBinder.Eval(Container.DataItem, "Description") %>
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
</table>
