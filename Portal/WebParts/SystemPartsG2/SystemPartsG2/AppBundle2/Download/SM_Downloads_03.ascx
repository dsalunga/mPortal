<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Download._Sections_Download_SM_Downloads_03"
    CodeBehind="SM_Downloads_03.ascx.cs" %>
<div runat="server" id="divTabNav">
    <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            <td style="height: 19px">
                <div class="tab_button" runat="server" id="divBasic">
                    <asp:LinkButton ID="cmdBasic" runat="server" Text="Downloads" OnClick="cmdBasic_Click" /></div>
            </td>
            <td style="width: 3px; height: 19px;" nowrap="nowrap" />
            <td style="height: 19px">
                <div class="tab_button_blur" runat="server" id="divAdvanced">
                    <asp:LinkButton ID="cmdAdvanced" runat="server" Text="Options" OnClick="cmdAdvanced_Click" /></div>
            </td>
            <td style="width: 100%; height: 19px" />
        </tr>
        <tr>
            <td colspan="4">
                <div class="tab_bar" />
            </td>
        </tr>
    </table>
    <div style="height: 10px" />
</div>
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewBasic" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Selected Downloads:
                </td>
            </tr>
            <tr>
                <td class="control_box">
                    <asp:Button ID="btnRemove" runat="server" Width="85px" Text="Remove" OnClick="btnRemove_Click"
                        Height="30px" />&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                        CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None"
                        Width="100%" AutoGenerateColumns="False" DataKeyNames="DownloadLocationID" OnRowCommand="GridView2_RowCommand">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input type="checkbox" name="chkCheckedMain2" onclick="CheckAll(this, 'chkChecked2');">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type="checkbox" value='<%# Eval("DownloadLocationID") %>' name="chkChecked2" />
                                </ItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions">
                                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/_CMS/Images/ico_edit.gif"
                                        AlternateText="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.DownloadID") %>'>
                                    </asp:ImageButton>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" ImageUrl="~/_CMS/Images/ico_exit.gif"
                                        AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete the selected items?');"
                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.DownloadID") %>'></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Download" SortExpression="Name" />
                            <asp:HyperLinkField DataNavigateUrlFields="Filename" DataNavigateUrlFormatString="/Assets/Uploads/Image/SECTIONS/Download/{0}"
                                DataTextField="Filename" HeaderText="Filename" SortExpression="Filename" />
                            <asp:TemplateField HeaderText="Date" SortExpression="FileDate">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FileDate", "{0:MMMM d, yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                            <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
                        </Columns>
                        <PagerSettings PageButtonCount="50" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="Download.SELECT_DownloadLocations" SelectCommandType="StoredProcedure"
                        CancelSelectOnNullParameter="False">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="PageType" QueryStringField="PageType" Type="String" />
                            <asp:QueryStringParameter Name="SitePageItemID" QueryStringField="SitePageItemID"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    All Downloads:
                </td>
            </tr>
            <tr>
                <td class="control_box">
                    <asp:Button ID="cmdNew" Width="85px" runat="server" Text="New" OnClick="cmdNew_Click"
                        Height="30px" />
                    <asp:Button ID="btnInsert" runat="server" Text="Insert as Selected" OnClick="btnInsert_Click"
                        Height="30px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None"
                        Width="100%" AutoGenerateColumns="False" DataKeyNames="DownloadID" OnRowCommand="GridView1_RowCommand">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type="checkbox" value='<%# Eval("DownloadID") %>' name="chkChecked" />
                                </ItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions">
                                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/_CMS/Images/ico_edit.gif"
                                        AlternateText="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.DownloadID") %>'>
                                    </asp:ImageButton>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" ImageUrl="~/_CMS/Images/ico_exit.gif"
                                        AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete the selected items?');"
                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.DownloadID") %>'></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Download" SortExpression="Name" />
                            <asp:HyperLinkField DataNavigateUrlFields="Filename" DataNavigateUrlFormatString="/Assets/Uploads/Image/SECTIONS/Download/{0}"
                                DataTextField="Filename" HeaderText="Filename" SortExpression="Filename" />
                            <asp:TemplateField HeaderText="Date" SortExpression="FileDate">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FileDate", "{0:MMMM d, yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                            <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
                        </Columns>
                        <PagerSettings PageButtonCount="50" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="Download.SELECT_DownloadLocations" SelectCommandType="StoredProcedure"
                        CancelSelectOnNullParameter="False">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="PageType" QueryStringField="PageType" Type="String" />
                            <asp:QueryStringParameter Name="SitePageItemID" QueryStringField="SitePageItemID"
                                Type="Int32" />
                            <asp:Parameter Name="InsertedOnly" Type="Boolean" DefaultValue="false" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewAdvanced" runat="server">
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 130px">
                                Initial Display:
                            </td>
                            <td>
                                <asp:DropDownList ID="cboControls" runat="server">
                                    <asp:ListItem Selected="True" Value="DetailedList.ascx">Detailed List</asp:ListItem>
                                    <asp:ListItem Value="BasicList.ascx">Basic List</asp:ListItem>
                                    <asp:ListItem Value="BasicList_A3.ascx">Basic List / A3</asp:ListItem>
                                    <asp:ListItem Value="GroupByYear.ascx">Group by Year</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">
                                Repeat Columns:
                            </td>
                            <td>
                                <asp:TextBox ID="txtColumns" runat="server" Columns="25">1</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">
                                Cell Padding:
                            </td>
                            <td>
                                <asp:TextBox ID="txtRows" runat="server" Columns="25">5</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">
                                Max. Records:
                            </td>
                            <td>
                                <asp:TextBox ID="txtMaxRecords" runat="server" Columns="25">-1</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkForceDownload" Text="Force Download" Checked="True" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="cmdUpdate" runat="server" Text="Update" Width="85px" OnClick="cmdUpdate_Click"
                        Height="30px" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
    </asp:View>
</asp:MultiView>