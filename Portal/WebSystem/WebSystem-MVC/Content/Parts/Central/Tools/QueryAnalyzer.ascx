<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryAnalyzer.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.QueryAnalyzerPresenter" %>
<table width="100%">
    <tr>
        <td>Connection String:
            <%--<asp:TextBox ID="txtCode" runat="server" Columns="10"></asp:TextBox>--%>&nbsp;
            <asp:TextBox ID="txtQS" runat="server" CssClass="span6"></asp:TextBox>
            <asp:CheckBox ID="chkCustom" runat="server" CssClass="aspnet-checkbox" Text="Custom" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtQuery" runat="server" Rows="20" Width="100%" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdExecute" CssClass="btn btn-default" runat="server" Text="Execute"
            OnClick="cmdExecute_Click"></asp:Button>&nbsp;<asp:RadioButton ID="chkQuery" runat="server"
                Text="Query" GroupName="radioOptions" CssClass="aspnet-radio" Checked="True" />&nbsp;<asp:RadioButton ID="chkDownload"
                    runat="server" Text="Download" CssClass="aspnet-radio" GroupName="radioOptions" />&nbsp;
            <asp:RadioButton ID="chkUpload" CssClass="aspnet-radio" runat="server" Text="Upload" GroupName="radioOptions" />
        <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
            <asp:CheckBox ID="chkSchema" CssClass="aspnet-checkbox" runat="server" Text="Schema" />
    </div>
</div>
<div>
    &nbsp;
</div>
<div>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <asp:PlaceHolder ID="phGrid" runat="server"></asp:PlaceHolder>
</div>
