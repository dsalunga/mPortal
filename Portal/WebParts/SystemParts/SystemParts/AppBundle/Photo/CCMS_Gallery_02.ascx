<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Photo.CONTENTCMS_Gallery" CodeBehind="CCMS_Gallery_02.ascx.cs" %>
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="viewGrid" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="ControlBox">
                    <asp:Button ID="btnNew" Width="75px" runat="server" Text="New" OnClick="btnNew_Click" />
                    <asp:Button ID="btnDelete" Width="75px" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                    &nbsp;
                    <asp:Button ID="cmdRegenerate" runat="server" Text="Recreate Thumbnails" OnClick="cmdRegenerate_Click" />
                    <asp:Button ID="cmdBatchUpload" runat="server" Text="Batch Upload..." OnClick="cmdBatchUpload_Click" />
                    <div style="float: right">
                        Album:
                        <asp:DropDownList ID="cboAlbum" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                            DataSourceID="SqlDataSource2" DataTextField="Title" DataValueField="CategoryID"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="-1"># All Album #</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
                        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
                        PageSize="15">
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
                                    <input type='checkbox' value='<%# Eval("Id")%>' name='chkChecked' />
                                </ItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Preview">
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <img src='<%# Eval("ThumbUrl") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Caption" HeaderText="Caption" SortExpression="Caption"
                                HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Title" HeaderText="Album" SortExpression="Title" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PhotoName" HeaderText="File Name" SortExpression="PhotoName"
                                HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DateCreated" HeaderText="Upload Date" SortExpression="DateCreated"
                                HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Active" SortExpression="IsActive">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Image runat="server" ImageUrl='<%# WCMS.WebHelper.SetStateImage(Eval("IsActive")) %>'
                                        ID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="50" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select" TypeName="WCMS.WebSystem.WebParts.Photo.CONTENTCMS_Gallery">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cboAlbum" DefaultValue="-1" Name="albumId" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="Gallery_Get" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList1" Name="categoryId" PropertyName="SelectedValue"
                                Type="Int32" DefaultValue="-1" />
                        </SelectParameters>
                    </asp:SqlDataSource>--%>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewDetails" runat="server">
        <table width="100%" border="0">
            <tr>
                <td style="width: 100px">ID:
                </td>
                <td>
                    <asp:Literal ID="litID" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Caption:
                </td>
                <td>
                    <asp:TextBox ID="txtCaption" Width="450px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <!--
            <tr>
                <td>
                    Thumbnail:</td>
                <td>
                    <asp:TextBox ID="txtThumbnail" Width="450px" runat="server">
                    </asp:TextBox>
                    <asp:Button ID="btnThumbnail" Width="75px" runat="server" Text="Upload..." CausesValidation="False">
                    </asp:Button></td>
            </tr>
            -->
            <tr>
                <td>Image File:<asp:RequiredFieldValidator ID="rfvImage" runat="server" ControlToValidate="txtImageURL"
                    ErrorMessage="Image File" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtImageURL" Width="450px" runat="server">
                    </asp:TextBox>
                    <asp:Button ID="btnImageURL" Width="75px" runat="server" Text="Upload..." CausesValidation="False"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>Album:
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategoryID" runat="server" DataSourceID="SqlDataSource2"
                        DataTextField="Title" DataValueField="CategoryID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="GalleryCategory_Get" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>Web Site:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSites" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked="True"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>Date Created:
                </td>
                <td>
                    <asp:Literal ID="litDateCreated" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="ControlBox">&nbsp;<asp:Button ID="btnUpdate" CssClass="Command" Width="75px" runat="server" Text="Update"
                    OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnCancel" CssClass="Command" Width="75px" runat="server" Text="Cancel"
                        OnClick="btnCancel_Click" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="vsMessages" runat="server" HeaderText="The following are required:"
                        ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewBatchUpload" runat="server">
        <br />
        <div>
            <strong style="font-size: larger">Upload Image Collection</strong>
        </div>
        <br />
        <table>
            <tr>
                <td>To Album:&nbsp;</td>
                <td><strong><span runat="server" id="lblAlbumName"></span></strong></td>
            </tr>
            <tr>
                <td>Archive File:<asp:RequiredFieldValidator ID="rfvCollection" runat="server" ControlToValidate="txtPhotoCollection"
                    ErrorMessage="Archive File" ForeColor="Red">*</asp:RequiredFieldValidator></td>
                <td>
                    <asp:TextBox ID="txtPhotoCollection" Width="350px" runat="server">
                    </asp:TextBox><asp:Button ID="cmdUploadCollection" Width="75px" runat="server" Text="Upload..."
                        CausesValidation="False" /></td>
            </tr>
            <tr>
                <td></td>
                <td><em style="font-size: smaller">Supported archive formats: <strong>.ZIP</strong>, .7Z, .GZ, .BZ2</em>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="ControlBox" colspan="2">
                    <asp:Button ID="cmdProcessCollection" CssClass="Command" runat="server" Text="Process Collection Now!"
                        OnClick="cmdProcessCollection_Click" /></td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Label ID="lblBatchUploadStatus" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <asp:Button ID="cmdBatchUploadDone" Visible="false" Width="75px" runat="server" Text="Done"
            OnClick="cmdBatchUploadDone_Click" />
    </asp:View>
</asp:MultiView>
<br />
<asp:Label ID="lblNotify" runat="server" ForeColor="Red"></asp:Label>