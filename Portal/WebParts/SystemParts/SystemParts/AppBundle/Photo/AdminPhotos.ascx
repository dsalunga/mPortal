<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Photo.AdminPhotosView" CodeBehind="AdminPhotos.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>

<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="viewGrid" runat="server">
        <div class="control-box no-bottom-margin">
            <div>
                <asp:Button ID="btnNew" CssClass="btn btn-default btn-sm" runat="server" Text="New" OnClick="btnNew_Click" />
                <asp:Button ID="btnDelete" CssClass="btn btn-default btn-sm" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="return confirm('Delete selected item(s)');" />
                &nbsp;
                    <asp:Button ID="cmdRegenerate" CssClass="btn btn-default btn-sm" runat="server" Text="Recreate Thumbnails" OnClick="cmdRegenerate_Click" />
                <asp:Button ID="cmdBatchUpload" CssClass="btn btn-default btn-sm" runat="server" Text="Batch Upload..." OnClick="cmdBatchUpload_Click" />
                <div class="pull-right">
                        <asp:DropDownList ID="cboAlbum" runat="server" CssClass="input" AppendDataBoundItems="True" AutoPostBack="True"
                            DataSourceID="ObjectDataSource2" DataTextField="Title" DataValueField="Id"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="-1"># All Album #</asp:ListItem>
                        </asp:DropDownList>
                </div>
            </div>
        </div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
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
                        <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type='checkbox' value='<%# Eval("Id")%>' name='chkChecked' />
                    </ItemTemplate>
                    <HeaderStyle Width="15px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                            AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Preview">
                    <HeaderStyle HorizontalAlign="Left" Width="20px" />
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <img src='<%# Eval("ThumbUrl") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Caption" HeaderText="Caption" SortExpression="Caption"
                    HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="AlbumTitle" HeaderText="Album" SortExpression="AlbumTitle" HeaderStyle-HorizontalAlign="Left">
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
                        <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImage(Eval("IsActive")) %>'
                            ID="Image1" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="50" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select" TypeName="WCMS.WebSystem.WebParts.Photo.AdminPhotosView">
            <SelectParameters>
                <asp:ControlParameter ControlID="cboAlbum" DefaultValue="-1" Name="albumId" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
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
                    <asp:TextBox ID="txtCaption" Width="450px" runat="server" CssClass="input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Image File:<asp:RequiredFieldValidator ID="rfvImage" runat="server" ControlToValidate="txtImageURL"
                    ErrorMessage="Image File" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td class="min-bottom-margin">
                    <asp:TextBox ID="txtImageURL" Width="450px" runat="server" CssClass="input">
                    </asp:TextBox>
                    <asp:Button ID="btnImageURL" CssClass="btn btn-default btn-sm" runat="server" Text="Upload..." CausesValidation="False"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>Album:
                </td>
                <td>
                    <asp:DropDownList ID="cboAlbumEdit" runat="server" CssClass="input" DataSourceID="ObjectDataSource2"
                        DataTextField="Title" DataValueField="Id">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetAlbums" TypeName="WCMS.WebSystem.WebParts.Photo.AdminPhotosView"></asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td>Web Site:
                </td>
                <td class="min-bottom-margin">
                    <asp:DropDownList ID="ddlSites" runat="server" CssClass="input">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkIsActive" CssClass="aspnet-checkbox" runat="server" Text="Active" Checked="True"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>Date Created:
                </td>
                <td>
                    <asp:Literal ID="litDateCreated" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <br />
        <div class="control-box">
            <div>
                <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
                    OnClick="btnUpdate_Click" />
                <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Cancel"
                    OnClick="btnCancel_Click" CausesValidation="False" />
            </div>
        </div>
        <div>
            <asp:ValidationSummary ID="vsMessages" runat="server" HeaderText="The following are required:"
                ShowMessageBox="True" ShowSummary="False" />
        </div>
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
                <td class="min-bottom-margin">
                    <asp:TextBox ID="txtPhotoCollection" Width="350px" CssClass="input" runat="server">
                    </asp:TextBox>&nbsp;<asp:Button ID="cmdUploadCollection" CssClass="btn btn-default btn-sm" runat="server" Text="Upload..."
                        CausesValidation="False" /></td>
            </tr>
            <tr>
                <td></td>
                <td><em style="font-size: smaller">Supported archive formats: <strong>.ZIP</strong>, .7Z, .GZ, .BZ2</em>
                </td>
            </tr>
        </table>
        <div>&nbsp;</div>
        <div class="control-box" runat="server" id="panelBatchActions">
            <div>
                <asp:Button ID="cmdProcessCollection" CssClass="btn btn-primary" runat="server" Text="Process Now!"
                    OnClick="cmdProcessCollection_Click" />&nbsp;<asp:Button ID="cmdBatchUploadCancel" CssClass="btn btn-default" runat="server" Text="Cancel"
                        CausesValidation="False" OnClick="cmdBatchUploadCancel_Click" />
            </div>
        </div>
        <br />
        <br />
        <asp:Label ID="lblBatchUploadStatus" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <asp:Button ID="cmdBatchUploadDone" Visible="false" CssClass="btn btn-default" runat="server" Text="Done"
            OnClick="cmdBatchUploadDone_Click" />
    </asp:View>
</asp:MultiView>
<br />
<asp:Label ID="lblNotify" runat="server" ForeColor="Red"></asp:Label>