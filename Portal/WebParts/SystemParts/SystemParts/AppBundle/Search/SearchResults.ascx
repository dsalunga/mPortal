<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Search.GenericSearchResult" Codebehind="SearchResults.ascx.cs" %>
<br />
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td>
            <asp:TextBox ID="txtSearch" runat="server" Columns="40" /></td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="cmdFind" Text=" Search " runat="server" OnClick="cmdFind_Click" /></td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Selected="True">This site only</asp:ListItem>
                <asp:ListItem>All sites</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
</table>
<br />
Search Results
<hr style="height: 1px; width: 98%; text-align: left" />
<asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" EmptyDataText="0 Result Found."
    GridLines="None" AutoGenerateColumns="False" Width="100%" AllowPaging="True"
    AllowSorting="True">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="font-weight: bold"><a href='<%# Eval("URL") %>'><%# Eval("SectionName") %></a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# Eval("Blurb")%>
                        </td>
                    </tr>
                </table>
                <br />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="Search.SELECT_Results" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
    <SelectParameters>
        <asp:Parameter Name="SiteID" Type="Int32" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="Search" Type="String" ConvertEmptyStringToNull="true" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:Literal ID="litNotify" runat="server"></asp:Literal>
