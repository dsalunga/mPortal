<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Photo.AdminAlbumEdit" CodeBehind="AdminAlbumEdit.ascx.cs" %>
<table width="100%" border="0">
    <tr>
        <td>Album Title:<asp:RequiredFieldValidator ID="rfvAlbum" runat="server" ControlToValidate="txtTitle"
            ErrorMessage="Album Title" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Width="450px" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Folder Name:<asp:RequiredFieldValidator ID="rfvFolderName" runat="server" ControlToValidate="txtFolderName"
            ErrorMessage="Folder Name" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtFolderName" runat="server" Width="350px" CssClass="input"></asp:TextBox>&nbsp;
                    <em>(a suggestion will be provided based on the Title)</em>
        </td>
    </tr>
    <tr>
        <td>Album Image:<asp:RequiredFieldValidator ID="rfvImageURL" runat="server" ControlToValidate="txtImageURL"
            ErrorMessage="Album Image" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td class="min-bottom-margin">
            <asp:TextBox ID="txtImageURL" runat="server" Width="350px" CssClass="input"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnImageURL" Text="Upload..." runat="server" CssClass="btn btn-default btn-sm" CausesValidation="False" />
        </td>
    </tr>
    <tr>
        <td>Album Image Width:
        </td>
        <td>
            <asp:TextBox ID="txtWidth" runat="server" Columns="10" Text="250" CssClass="input"></asp:TextBox>&nbsp;
                    <em>in pixels (250px default, -1 = use original)</em>
        </td>
    </tr>
    <tr>
        <td>Photo Thumbnail Height:
        </td>
        <td>
            <asp:TextBox ID="txtPhotoHeight" runat="server" Columns="10" Text="75" CssClass="input"></asp:TextBox>&nbsp;
                    <em>in pixels (75px default)</em>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="btnUpdate" CssClass="btn btn-primary" Text="Update" runat="server"
            OnClick="btnUpdate_Click" />
        <asp:Button ID="btnCancel" CssClass="btn btn-default" Text="Cancel" runat="server"
            OnClick="btnCancel_Click" CausesValidation="False" />
    </div>
</div>
<div>
    <asp:Label ID="lblNotify" runat="server" ForeColor="Red"></asp:Label>
</div>
