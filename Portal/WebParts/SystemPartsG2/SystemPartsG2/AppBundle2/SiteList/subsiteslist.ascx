<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.SiteList.SubsitesList" EnableViewState="False" Codebehind="SubsitesList.ascx.cs" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td valign="top">
            <img src="/Assets/Uploads/Image/HOME/Portal/label06.gif" width="232" height="30"></td>
    </tr>
    <tr>
        <td valign="top">
            <img src="/Assets/Uploads/Image/HOME/Portal/shadow_right.gif" width="232" height="14"></td>
    </tr>
    <tr>
        <td valign="top">
            <table width="100%" border="0" cellspacing="4" cellpadding="0">
                <asp:Repeater ID="rList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td width="21%" align="center" valign="top">
                                <img src='/Assets/Uploads/Image/LOGO/<%# DataBinder.Eval(Container.DataItem, "SiteImage") %>'></td>
                            <td width="79%" valign="top" class="content">
                                <a onclick='<%# EvalLink(DataBinder.Eval(Container.DataItem, "IsActive")) %>' href='<%# DataBinder.Eval(Container.DataItem, "SiteURL") %>'
                                    title='<%# DataBinder.Eval(Container.DataItem, "SiteName") %>' class="contentbgb">
                                    <%# DataBinder.Eval(Container.DataItem, "SiteName") %>
                                </a>
                                <br>
                                <%# DataBinder.Eval(Container.DataItem, "description") %>
                            </td>
                            <!--
							<td width="79%" valign="top" class="content"><a onclick='<%# EvalLink(DataBinder.Eval(Container.DataItem, "IsActive")) %>' href='/projects/<%# DataBinder.Eval(Container.DataItem, "SiteID") %>.aspx' title='<%# DataBinder.Eval(Container.DataItem, "SiteName") %>' class="contentbgb"><%# DataBinder.Eval(Container.DataItem, "SiteName") %></a><br>
								<%# DataBinder.Eval(Container.DataItem, "description") %>
							</td>
							-->
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </td>
    </tr>
    <tr>
        <td valign="top">
            &nbsp;</td>
    </tr>
</table>
