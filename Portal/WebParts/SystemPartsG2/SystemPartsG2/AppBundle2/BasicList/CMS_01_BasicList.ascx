<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.BasicList._Sections_BasicList_CMS_01_BasicList"
    CodeBehind="CMS_01_BasicList.ascx.cs" %>
<div runat="server" id="divTabNav">
    <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
                <div class="tab_button" runat="server" id="divBasic">
                    <asp:LinkButton ID="cmdBasic" runat="server" Text="LIST" OnClick="cmdBasic_Click"
                        CausesValidation="False" /></div>
            </td>
            <td style="width: 3px;" nowrap>
            </td>
            <td>
                <div class="tab_button_blur" runat="server" id="divAdvanced">
                    <asp:LinkButton ID="cmdAdvanced" runat="server" Text="OPTIONS" OnClick="cmdAdvanced_Click"
                        CausesValidation="False" /></div>
            </td>
            <td style="width: 100%">
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <div class="tab_bar">
                </div>
            </td>
        </tr>
    </table>
    <div style="height: 10px">
    </div>
</div>
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewBasic" runat="server">
        <asp:MultiView ID="multiViewList" runat="server" ActiveViewIndex="0">
            <asp:View ID="viewGrid" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="control_box">
                            <asp:Button ID="cmdAdd" Width="80px" Text="Add" runat="server" OnClick="cmdAdd_Click" />
                            <asp:Button ID="cmdDelete" Width="80px" Text="Delete" runat="server" OnClick="cmdDelete_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="Vertical"
                                Width="100%" AutoGenerateColumns="False" DataKeyNames="ListItemID" OnRowCommand="GridView1_RowCommand">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("ListItemID", "<input type='checkbox' value='{0}' name='chkChecked'>")%>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Field1" HeaderText="Field1" SortExpression="Field1" />
                                    <asp:BoundField DataField="Field2" HeaderText="Field2" SortExpression="Field2" />
                                    <asp:BoundField DataField="Field3" HeaderText="Field3" SortExpression="Field3" />
                                    <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" ItemStyle-HorizontalAlign="center"
                                        ItemStyle-Width="30px" />
                                    <asp:TemplateField HeaderText="Edit">
                                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit_item" ImageUrl="~/Images/Common/ico_edit.gif"
                                                AlternateText="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ListItemID") %>'>
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                SelectCommand="BasicListItem_Get" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="true">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="SitePageItemID" QueryStringField="PageElementId"
                                        Type="Int32" />
                                    <asp:QueryStringParameter Name="PageType" QueryStringField="PageType" DefaultValue="22"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="viewDetails" runat="server">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td width="110">
                                        Field1:<asp:RequiredFieldValidator ID="rfvField1" runat="server" ControlToValidate="txtField1"
                                            ErrorMessage="Field1">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtField1" runat="server" Columns="75"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="110">
                                        Field2:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtField2" runat="server" Columns="75"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="110">
                                        Field3:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtField3" runat="server" Columns="75"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="110">
                                        Rank:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRank" runat="server" Columns="25"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="control_box">
                            <asp:HiddenField ID="hiddenID" runat="server" />
                            <asp:Button ID="cmdUpdate" runat="server" Width="80px" Text="Update" OnClick="cmdUpdate_Click" />
                            <asp:Button ID="cmdCancel" runat="server" Width="80px" Text="Cancel" OnClick="cmdCancel_Click"
                                CausesValidation="False" />
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
                    ShowMessageBox="True" ShowSummary="False" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label></asp:View>
        </asp:MultiView>
    </asp:View>
    <asp:View ID="viewAdvanced" runat="server">
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                Page Size:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPageSize" runat="server" Columns="50">25</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Repeat Columns:
                            </td>
                            <td>
                                <asp:TextBox ID="txtRepeatColumns" runat="server" Columns="50">1</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cell Padding:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCellPadding" runat="server" Columns="50">2</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Text Color:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTextColor" runat="server" Columns="50">white</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Alternating Background-Color:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlternateColor" runat="server" Columns="50">#1A1A1A</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Item Template:
                            </td>
                            <td>
                                <asp:DropDownList ID="cboItemTemplate" runat="server">
                                    <asp:ListItem Selected="True" Value="Single.ascx">Single Column</asp:ListItem>
                                    <asp:ListItem Value="TwoColumns.ascx">Two Columns</asp:ListItem>
                                    <asp:ListItem Value="TwoRows.ascx">Two Rows</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Grid Lines:
                            </td>
                            <td>
                                <asp:DropDownList ID="cboGridLines" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="cmdUpdateProperties" runat="server" Text="Update" Width="80px" OnClick="cmdUpdateProperties_Click" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
    </asp:View>
</asp:MultiView>