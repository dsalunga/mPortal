<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobSearch.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Jobs.JobSearch" %>
<h1>
    Job Search</h1>
<table cellspacing='0' style='font-family: arial;'>
    <tr>
        <td style='font-size: 16px; color: #FF6600'>
            <b>what</b>
        </td>
        <td colspan="2" style='font-size: 16px; color: #FF6600'>
            <b>where</b>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtWhat" runat="server" Width="150px" />
        </td>
        <td>
            <asp:TextBox ID="txtWhere" runat="server" Width="150px" />
        </td>
        <td>
            <asp:DropDownList ID="ddlCountry" runat="server">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnFindJobs" Text="Find Jobs" runat="server" OnClick="btnFindJobs_Click" />
        </td>
    </tr>
    <tr>
        <td style='font-size: 10px; vertical-align: top'>
            job title, keywords or company
        </td>
        <td colspan='2' style='font-size: 10px; padding: 0px; margin: 0px; border: 0px; vertical-align: top'>
            <table cellpadding='0' style='padding: 0px; margin: 0px; border: 0px; width: 100%'>
                <tr>
                    <td style='font-size: 10px; padding: 0px; margin: 0px; border: 0px; vertical-align: top'>
                        city, state or zip
                    </td>
                    <td style='font-size: 13px; text-align: right'>
                        <span id="indeed_at"><a href="http://www.indeed.com/?indpubnum=3462123152151013"
                            style="text-decoration: none; color: #000">jobs</a> by <a href="http://www.indeed.com/?indpubnum=3462123152151013"
                                title="Job Search">
                                <img src="http://www.indeed.com/p/jobsearch.gif" style="border: 0; vertical-align: middle;"
                                    alt="job search" />
                            </a></span>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<asp:ListView ID="lvJobSearchResults" ItemPlaceholderID="PlcID" runat="server">
    <LayoutTemplate>
        <br />
        <center>
            <h2 style="font-family: Verdana; color: #1877d6">
                <i>Jobs Search Results</i>
            </h2>
        </center>
        <asp:Label ID="lblNoJobsSearch" Text="text" runat="server" Style="float: right" />
        <br />
        <asp:PlaceHolder ID="PlcID" runat="server"></asp:PlaceHolder>
    </LayoutTemplate>
    <ItemTemplate>
        <asp:HyperLink ID="lnkJobTitle" runat="server" Target="_blank" NavigateUrl='<%# "http://www.indeed.com/rc/clk?jk=" 
                + System.Web.HttpUtility.ParseQueryString((new Uri(XPath("url").ToString())).Query).Get("jk") %>'>
            <%# XPath("jobtitle")%>
        </asp:HyperLink>
        <br />
        <i>
            <%# XPath("company")%>
            - <em style="color: #8c0017">
                <%# XPath("city")%>
            </em></i>
        <br />
        <span style="font-size: 12px; color: #66803a">
            <%# XPath("snippet")%>
        </span>
        <br />
        <i><em style="color: #8c0017">
            <%# XPath("formattedLocationFull")%>
            -
            <%# XPath("formattedRelativeTime")%>
        </em></i>
    </ItemTemplate>
    <ItemSeparatorTemplate>
        <hr style="color: Orange" />
    </ItemSeparatorTemplate>
</asp:ListView>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
    GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
    PageSize="15">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Actions">
            <HeaderStyle HorizontalAlign="center" Width="40px" />
            <ItemStyle HorizontalAlign="center" />
            <ItemTemplate>
                <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Images/Common/ico_edit.gif"
                    ID="Imagebutton4" AlternateText="Edit Item" CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" HeaderStyle-HorizontalAlign="Left" />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <AlternatingRowStyle BackColor="White" />
    <PagerSettings PageButtonCount="25" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Jobs.JobSearch"></asp:ObjectDataSource>
<br />
<br />
<div style="text-align: center;">
    <asp:Repeater ID="rptPager" runat="server">
        <ItemTemplate>
            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
        </ItemTemplate>
    </asp:Repeater>
</div>
<asp:XmlDataSource ID="xdsJobSearchResult" runat="server" EnableCaching="false" />
