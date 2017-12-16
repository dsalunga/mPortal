<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Photo.CMS_Category" CodeBehind="CMS_Category.ascx.cs" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<uc1:TabControl ID="TabControl1" runat="server" SelectedIndex="0" OnSelectedTabChanged="TabControl1_SelectedTabChanged" />
<asp:HiddenField ID="hObjectId" runat="server" Value="-1" />
<asp:HiddenField ID="hRecordId" runat="server" Value="-1" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewBasic" runat="server">
        <div>
            <strong>Selected Albums</strong>
            <asp:GridView ID="GridViewInserted" runat="server" AllowPaging="True" AllowSorting="True"
                CellPadding="4" DataSourceID="ObjectDataSource2" ForeColor="#333333" GridLines="None"
                Width="100%" AutoGenerateColumns="False" DataKeyNames="Id"
                PageSize="10">
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
                            <input type="checkbox" name="chkCheckedMainInserted" onclick="CheckAll(this, 'chkCheckedInserted');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type='checkbox' value='<%# Eval("Id")%>' name='chkCheckedInserted' />
                        </ItemTemplate>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Preview">
                        <HeaderStyle HorizontalAlign="Left" Width="20px" />
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <img src='<%# Eval("ThumbUrl") %>' width="80" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Title" HeaderText="Name" SortExpression="Title"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PhotoWidth" HeaderText="Photo Width" SortExpression="PhotoWidth"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PhotoHeight" HeaderText="Photo Height" SortExpression="PhotoHeight"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ImageFile" HeaderText="Thumbnail File" SortExpression="ImageFile"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="SelectInserted" TypeName="WCMS.WebSystem.WebParts.Photo.CMS_Category">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hObjectId" DefaultValue="-1" Name="objectId" PropertyName="Value" Type="Int32" />
                    <asp:ControlParameter ControlID="hRecordId" DefaultValue="-1" Name="recordId" PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <div class="control-box" id="rowRemove" runat="server">
            <div>
                <asp:Button ID="btnRemove" CssClass="btn btn-default" runat="server" Text="Remove"
                    OnClick="btnRemove_Click" />
            </div>
        </div>
        <div>
            &nbsp;
        </div>
        <div>
            All Albums:
        </div>
        <div>
            <asp:GridView ID="GridViewPool" runat="server" AllowPaging="True" AllowSorting="True"
                CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
                Width="100%" AutoGenerateColumns="False" DataKeyNames="Id"
                PageSize="10">
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
                            <input type="checkbox" name="chkCheckedMainPool" onclick="CheckAll(this, 'chkCheckedPool');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type='checkbox' value='<%# Eval("Id")%>' name='chkCheckedPool' />
                        </ItemTemplate>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Preview">
                        <HeaderStyle HorizontalAlign="Left" Width="20px" />
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <img src='<%# Eval("ThumbUrl") %>' width="80" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Title" HeaderText="Name" SortExpression="Title"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PhotoWidth" HeaderText="Photo Width" SortExpression="PhotoWidth"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PhotoHeight" HeaderText="Photo Height" SortExpression="PhotoHeight"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ImageFile" HeaderText="Thumbnail File" SortExpression="ImageFile"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectPool" TypeName="WCMS.WebSystem.WebParts.Photo.CMS_Category">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hObjectId" DefaultValue="-1" Name="objectId" PropertyName="Value" Type="Int32" />
                    <asp:ControlParameter ControlID="hRecordId" DefaultValue="-1" Name="recordId" PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <div class="control-box" id="rowInsert" runat="server">
            <div>
                <asp:Button ID="btnInsert" CssClass="btn btn-default" runat="server" Text="Insert"
                    OnClick="btnInsert_Click" />
            </div>
        </div>
    </asp:View>
    <asp:View ID="viewAdvanced" runat="server">
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 130px">Display Method:
                            </td>
                            <td>
                                <asp:DropDownList ID="cboControls" runat="server">
                                    <%--<asp:ListItem Selected="True" Value="Album.ascx">Album View</asp:ListItem>--%>
                                    <asp:ListItem Value="Thumbnails.ascx">Standard</asp:ListItem>
                                    <asp:ListItem Value="FancyBoxThumbnails.ascx">Fancy Box</asp:ListItem>
                                    <%--<asp:ListItem Value="Slideshow.ascx">Slide Show</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold">
                                <br />
                                Album View
                            </td>
                        </tr>
                        <tr>
                            <td>Columns:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlbumColumns" runat="server" Columns="25">2</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Padding:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlbumPadding" runat="server" Columns="25">15</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold">
                                <br />
                                Thumbnail View
                            </td>
                        </tr>
                        <tr>
                            <td>Columns (Width):
                            </td>
                            <td>
                                <asp:TextBox ID="txtThumbColumns" runat="server" Columns="25">4</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Rows (Height):
                            </td>
                            <td>
                                <asp:TextBox ID="txtThumbRows" runat="server" Columns="25">5</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: bold">
                                <br />
                                Photo View
                            </td>
                        </tr>
                        <tr>
                            <td>Max. Photo Width:
                            </td>
                            <td>
                                <asp:TextBox ID="txtMaxPhotoWidth" runat="server" Columns="15">700</asp:TextBox>&nbsp;<em>(in
                                    pixels)</em>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<tr>
                <td align="right">
                    <asp:Button ID="cmdUpdate" runat="server" Text="Update" Width="80px" OnClick="cmdUpdate_Click" />
                </td>
            </tr>--%>
        </table>
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
    </asp:View>
</asp:MultiView>
<br />
<br />
