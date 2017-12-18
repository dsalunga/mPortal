<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CKEditor.ascx.cs" Inherits="WCMS.WebSystem.Controls.CKEditor" %>
<%@ Register Src="TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<uc1:TabControl ThemeName="green" ID="TabControl1" runat="server" OnSelectedTabChanged="TabControl1_SelectedTabChanged" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewText" runat="server">
        <asp:TextBox ID="txtValueText" runat="server" Width="100%" Rows="20" TextMode="MultiLine"></asp:TextBox>
    </asp:View>
    <asp:View ID="viewHtml" runat="server">
        <asp:TextBox ID="txtValue" runat="server" TextMode="MultiLine"></asp:TextBox>
        <script type="text/javascript">
            window.onload = function () {
                CKEDITOR.replace('<%=txtValue.ClientID%>',
                 {
                     filebrowserBrowseUrl: '/components/ckfinder/ckfinder.html',
                     filebrowserImageBrowseUrl: '/components/ckfinder/ckfinder.html?type=Images',
                     filebrowserFlashBrowseUrl: '/components/ckfinder/ckfinder.html?type=Flash',
                     filebrowserUploadUrl: '/components/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',
                     filebrowserImageUploadUrl: '/components/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images',
                     filebrowserFlashUploadUrl: '/components/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'
                 });
            }
        </script>
    </asp:View>
</asp:MultiView>