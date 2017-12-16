<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.SiteList.ListSimple1" enableViewState="False" Codebehind="ListSimpleHoriz1.ascx.cs" %>
<table>
	<asp:Repeater id="rList" runat="server">
		<ItemTemplate>
			<tr>
				<td><a onclick='<%# EvalLink(DataBinder.Eval(Container.DataItem, "IsActive")) %>' href='<%# DataBinder.Eval(Container.DataItem, "SiteURL") %>' title='<%# DataBinder.Eval(Container.DataItem, "SiteName") %>'><font size="1"><%# DataBinder.Eval(Container.DataItem, "SiteName") %></font></a></td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
</table>
