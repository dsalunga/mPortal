<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Photo.CMS_Gallery" CodeBehind="CMS_Gallery.ascx.cs" %>
<div runat="server" id="divTabNav">
    <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            <td style="height: 19px">
                <div class="tab_button" runat="server" id="divBasic">
                    <asp:LinkButton ID="cmdBasic" runat="server" Text="PICTURES" OnClick="cmdBasic_Click" /></div>
            </td>
            <td style="width: 3px; height: 19px;" nowrap="nowrap" />
            <td style="height: 19px">
                <div class="tab_button_blur" runat="server" id="divAdvanced">
                    <asp:LinkButton ID="cmdAdvanced" runat="server" Text="OPTIONS" OnClick="cmdAdvanced_Click" /></div>
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
                    Selected Images:
                </td>
            </tr>
            <tr>
                <td class="control_box">
                    <asp:Button ID="btnRemove" OnClientClick="return confirm('Are you sure you want to remove selected items?');"
                        runat="server" Width="75px" Text="Remove" OnClick="btnRemove_Click" />&nbsp;Album:
                    <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                        DataSourceID="SqlDataSource3" DataTextField="Title" DataValueField="CategoryID"
                        OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="">[ ALL ALBUM ]</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                        CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="Vertical"
                        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id">
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
                                    <input type="checkbox" name="chkCheckedMain2" onclick="CheckAll(this, 'chkChecked2');">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type='checkbox' value='<%# Eval("Id")%>' name='chkChecked2' />
                                </ItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Image runat="server" ImageUrl='<%# WCMS.WebHelper.SetStateImage(Eval("IsActive")) %>'
                                        ID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Caption" HeaderText="Image Caption" SortExpression="Caption" />
                            <asp:BoundField DataField="Title" HeaderText="Album Title" SortExpression="Title" />
                            <asp:BoundField DataField="DateCreated" HeaderText="Picture Date" SortExpression="DateCreated" />
                            <asp:TemplateField HeaderText="Edit" Visible="False">
                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit_item" ImageUrl="~/Images/ico_edit.gif"
                                        AlternateText="Edit" CommandArgument='<%# Eval("GalleryID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="50" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="GalleryLink_GetTypeId" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="ObjectId" QueryStringField="ObjectId" Type="Int32" />
                            <asp:QueryStringParameter Name="RecordId" QueryStringField="RecordId" Type="Int32" />
                            <asp:ControlParameter ControlID="DropDownList2" Name="CategoryId" PropertyName="SelectedValue"
                                Type="Int32" ConvertEmptyStringToNull="true" />
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
                    All Images:
                </td>
            </tr>
            <tr>
                <td class="control_box">
                    <asp:Button ID="btnInsert" runat="server" Width="75px" Text="Insert" OnClick="btnInsert_Click" />&nbsp;Album:
                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                        DataSourceID="SqlDataSource3" DataTextField="Title" DataValueField="CategoryID"
                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="">[ ALL ALBUM ]</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="GalleryCategory_Get" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="Vertical"
                        Width="100%" AutoGenerateColumns="False" DataKeyNames="GalleryID">
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
                                    <input type='checkbox' value='<%# Eval("GalleryID")%>' name='chkChecked' />
                                </ItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Image runat="server" ImageUrl='<%# WCMS.WebHelper.SetStateImage(Eval("IsActive")) %>'
                                        ID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Caption" HeaderText="Image Caption" SortExpression="Caption" />
                            <asp:BoundField DataField="Title" HeaderText="Album Title" SortExpression="Title" />
                            <asp:BoundField DataField="DateCreated" HeaderText="Picture Date" SortExpression="DateCreated" />
                            <asp:TemplateField HeaderText="Edit" Visible="False">
                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit_item" ImageUrl="~/Images/ico_edit.gif"
                                        AlternateText="Edit" CommandArgument='<%# Eval("GalleryID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="50" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="GalleryLink_GetTypeId" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="ObjectId" QueryStringField="ObjectId" Type="Int32" />
                            <asp:QueryStringParameter Name="RecordId" QueryStringField="RecordId" Type="Int32" />
                            <asp:Parameter Name="InsertedOnly" Type="Boolean" DefaultValue="false" />
                            <asp:ControlParameter ControlID="DropDownList1" Name="CategoryId" PropertyName="SelectedValue"
                                Type="Int32" ConvertEmptyStringToNull="true" />
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
                                    <asp:ListItem Selected="True" Value="Album.ascx">Album View</asp:ListItem>
                                    <asp:ListItem Value="Thumbnails.ascx">Thumbnail View</asp:ListItem>
                                    <asp:ListItem Value="FullView.ascx">Full Image View</asp:ListItem>
                                    <asp:ListItem Value="Slideshow.ascx">Slide Show</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold">
                                <br />
                                Album View<hr style="height: 1px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">
                                Columns:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlbumColumns" runat="server" Columns="25">2</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">
                                Padding:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlbumPadding" runat="server" Columns="25">15</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold">
                                <br />
                                Thumbnail View / [ Slide Show ]<hr style="height: 1px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">
                                Columns / [ Width ]:
                            </td>
                            <td>
                                <asp:TextBox ID="txtThumbColumns" runat="server" Columns="25">4</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">
                                Rows / [ Height ]:
                            </td>
                            <td>
                                <asp:TextBox ID="txtThumbRows" runat="server" Columns="25">5</asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="cmdUpdate" runat="server" Text="Update" Width="80px" OnClick="cmdUpdate_Click" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
    </asp:View>
</asp:MultiView>