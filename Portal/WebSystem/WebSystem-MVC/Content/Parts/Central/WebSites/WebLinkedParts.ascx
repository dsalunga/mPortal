<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebLinkedParts.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebLinkedParts" %>
<%@ Register Src="../Controls/WebPageTab.ascx" TagName="WebPageTab" TagPrefix="uc1" %>
<uc1:WebPageTab ID="WebPageTab1" runat="server" />
<br />
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
                Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Actions">
                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton4" runat="server" CommandName="Custom_Exec" ImageUrl="~/Content/Assets/Images/Common/Actions/WebInsertHyperlinkHS.png"
                                AlternateText="Execute" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PartConfigName" HeaderText="Name" SortExpression="PartConfigName"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="PartName" HeaderText="Part" SortExpression="PartName"
                        HeaderStyle-HorizontalAlign="Left" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                TypeName="WCMS.WebSystem.WebParts.Central.WebSites.WebLinkedParts">
                <SelectParameters>
                    <asp:QueryStringParameter Name="pageId" QueryStringField="PageId" Type="Int32" DefaultValue="-1" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
