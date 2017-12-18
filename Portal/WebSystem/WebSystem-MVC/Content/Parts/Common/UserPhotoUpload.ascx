<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserPhotoUpload.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Common.UserPhotoUpload" %>
<%--
    Parameters:
        ThumbSize: 200
        PhotoSize: 600
        ReturnUrl:

        [X] Site: WCMS.UserPhotoPath: /Content/Assets/User-Photos
--%>
<asp:HiddenField ID="hExtension" runat="server" Value="" />
<asp:HiddenField ID="hReturnUrl" runat="server" Value="/" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewUpload" runat="server">
        <div class="Header">Upload Your Profile Photo</div>
        <br />
        <asp:FileUpload ID="photoUpload" ClientIDMode="Static" runat="server" />
        <br />
        <br />
        <span id="uploadPanel">
            <asp:Button ID="cmdUpload" runat="server" CssClass="btn btn-primary" Text="Upload Now" OnClick="cmdUpload_Click" />&nbsp;</span>
        <asp:Button ID="cmdUploadCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="cmdUploadCancel_Click" />
        <script type="text/javascript">
            $(document).ready(function () {
                $("#photoUpload").change(function () {
                    var fileName = $(this).val();
                    $("#uploadPanel").css("display", fileName ? "" : "none");
                });

                $("#uploadPanel").css("display", "none");
            });
        </script>
    </asp:View>
    <asp:View ID="viewPreview" runat="server">
        <div class="Header">Photo Preview</div>
        <br />
        <asp:Image ID="imagePreview" runat="server" />
        <br />
        <br />
        <asp:Button ID="cmdAccept" CssClass="btn btn-primary" runat="server" Text="Accept & Update" OnClick="cmdAccept_Click" />&nbsp;
        <asp:Button ID="cmdReUpload" CssClass="btn btn-default" runat="server" Text="Re-Upload" OnClick="cmdReUpload_Click" />&nbsp;
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click" />&nbsp;
    </asp:View>
</asp:MultiView>
<span id="lblMsg" runat="server" enableviewstate="false" style="color: red"></span>