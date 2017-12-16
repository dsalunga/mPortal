<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.SiteList.ListSimple1" EnableViewState="False" Codebehind="ListSimpleHoriz1.ascx.cs" %>
<asp:Repeater ID="rList" runat="server">
    <ItemTemplate>
        <a onclick='<%# EvalLink(DataBinder.Eval(Container.DataItem, "IsActive")) %>' href='<%# DataBinder.Eval(Container.DataItem, "SiteURL") %>'
            title='<%# DataBinder.Eval(Container.DataItem, "SiteName") %>' class="linkgn">
            <%# DataBinder.Eval(Container.DataItem, "SiteName") %>
        </a>
        <!--
		<a onclick='<%# EvalLink(DataBinder.Eval(Container.DataItem, "IsActive")) %>' href='/projects/<%# DataBinder.Eval(Container.DataItem, "SiteID") %>.aspx' title='<%# DataBinder.Eval(Container.DataItem, "SiteName") %>' class="linkgn">
			<%# DataBinder.Eval(Container.DataItem, "SiteName") %>
		</a>
		-->
    </ItemTemplate>
    <SeparatorTemplate>
        |
    </SeparatorTemplate>
</asp:Repeater>
&nbsp;&nbsp; 