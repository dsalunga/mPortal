<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.SiteList.ListSimple1" EnableViewState="False" Codebehind="ListSimpleHoriz1.ascx.cs" %>
<table>
    <tr>
        <asp:Repeater ID="rList" runat="server">
            <ItemTemplate>
                <td>
                    <a onclick='<%# EvalLink(DataBinder.Eval(Container.DataItem, "IsActive")) %>' href='<%# DataBinder.Eval(Container.DataItem, "SiteURL") %>'
                        title='<%# DataBinder.Eval(Container.DataItem, "SiteName") %>'><font size="1"><%# DataBinder.Eval(Container.DataItem, "SiteName") %></font>
                    </a>
                </td>
                <!--
				<td><a onclick='<%# EvalLink(DataBinder.Eval(Container.DataItem, "IsActive")) %>' href='/projects/<%# DataBinder.Eval(Container.DataItem, "SiteID") %>.aspx' title='<%# DataBinder.Eval(Container.DataItem, "SiteName") %>'><font size="1"><%# DataBinder.Eval(Container.DataItem, "SiteName") %></font></a></td>
				-->
            </ItemTemplate>
            <SeparatorTemplate>
                <td>
                    |</td>
            </SeparatorTemplate>
        </asp:Repeater>
    </tr>
</table>
