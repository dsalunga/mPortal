<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextEditor.ascx.cs"
    Inherits="WCMS.WebSystem.Controls.TextEditor" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>--%>
<uc1:TabControl ThemeName="green" ID="TabControl1" CssClass="portal-tabpanel-narrow" runat="server" OnSelectedTabChanged="TabControl1_SelectedTabChanged" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewText" runat="server">
        <asp:TextBox ID="txtValueText" runat="server" Width="100%" Rows="20" TextMode="MultiLine"></asp:TextBox>
    </asp:View>
    <%--<asp:View ID="viewAjaxEditor" runat="server">
        <HTMLEditor:Editor ID="txtAjaxValue" runat="server" Height="350px" Width="100%" AutoFocus="true" />
    </asp:View>--%>
    <asp:View ID="viewHtml" runat="server">
        <FCKeditorV2:FCKeditor ID="txtValue" runat="server" Height="350px" ToolbarSet="Default">
        </FCKeditorV2:FCKeditor>
    </asp:View>
</asp:MultiView>