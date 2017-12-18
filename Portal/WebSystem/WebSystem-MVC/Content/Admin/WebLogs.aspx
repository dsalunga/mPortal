<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="_CMS_SiteLogs" Title="Untitled Page" Codebehind="SiteLogs.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="header">
                Site Log Viewer</td>
        </tr>
        <tr>
            <td>
                <br />
                <span style="font-weight: bold">Please specify Site Log period.</span><br />
                From Date:
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>&nbsp;To Date:<asp:TextBox
                    ID="txtTo" runat="server"></asp:TextBox>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="ControlBoxRed">
                <asp:Button ID="cmdSites" runat="server" Text="View by Sites" OnClick="cmdSites_Click"
                    Height="30px" />
                <asp:Button ID="cmdSiteSections" runat="server" Text="View by Sections" Height="30px"
                    OnClick="cmdSiteSections_Click" />
                <asp:Button ID="cmdLogs" runat="server" Text="View Log Details" Height="30px" OnClick="cmdLogs_Click" />
                <asp:CheckBox ID="chkDownload" runat="server" Text="Download XML" /></td>
        </tr>
        <tr>
            <td>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="viewSites" runat="server">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                            CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None"
                            Width="100%" PageSize="15">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <AlternatingRowStyle BackColor="White" />
                            <PagerSettings PageButtonCount="25" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                            SelectCommand="CMS.SELECT_SiteLogs" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtFrom" Name="FromDate" PropertyName="Text" Type="DateTime"
                                    ConvertEmptyStringToNull="true" />
                                <asp:ControlParameter ControlID="txtTo" Name="ToDate" PropertyName="Text" Type="DateTime"
                                    ConvertEmptyStringToNull="true" />
                                <asp:Parameter Name="Type" Type="int32" DefaultValue="1" ConvertEmptyStringToNull="true" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:View>
                    <asp:View ID="viewSiteSections" runat="server">
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                            SelectCommand="CMS.SELECT_SiteLogs" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtFrom" Name="FromDate" PropertyName="Text" Type="DateTime"
                                    ConvertEmptyStringToNull="true" />
                                <asp:ControlParameter ControlID="txtTo" Name="ToDate" PropertyName="Text" Type="DateTime"
                                    ConvertEmptyStringToNull="true" />
                                <asp:Parameter Name="Type" Type="int32" DefaultValue="2" ConvertEmptyStringToNull="true" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                            CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None"
                            Width="100%" PageSize="15">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <AlternatingRowStyle BackColor="White" />
                            <PagerSettings PageButtonCount="25" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="viewLogDetails" runat="server">
                        <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True"
                            CellPadding="4" DataSourceID="SqlDataSource3" ForeColor="#333333" GridLines="None"
                            Width="100%" PageSize="15">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <AlternatingRowStyle BackColor="White" />
                            <PagerSettings PageButtonCount="25" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                            SelectCommand="CMS.SELECT_SiteLogs" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtFrom" Name="FromDate" PropertyName="Text" Type="DateTime"
                                    ConvertEmptyStringToNull="true" />
                                <asp:ControlParameter ControlID="txtTo" Name="ToDate" PropertyName="Text" Type="DateTime"
                                    ConvertEmptyStringToNull="true" />
                                <asp:Parameter Name="Type" Type="Int32" ConvertEmptyStringToNull="true" DefaultValue="0" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
    </table>
</asp:Content>
