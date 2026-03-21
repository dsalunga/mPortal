<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebResource.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebResourceController" %>
<h1 class="central page-header">Text Resource</h1>
<table border="0" width="100%">
    <tr>
        <td>Title:
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" Columns="85" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 120px">Content Type:
        </td>
        <td>
            <asp:DropDownList ID="cboContentType" runat="server" CssClass="input" DataTextField="Text" DataValueField="Id">
            </asp:DropDownList>
            &nbsp;Rank:&nbsp;
                        <asp:TextBox ID="txtRank" Columns="10" runat="server" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Physical Path:
        </td>
        <td>
            <asp:TextBox ID="txtPhysicalPath" runat="server" Columns="85" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Permalink:
        </td>
        <td>
            <asp:TextBox ID="txtPermalink" ReadOnly="true" runat="server" Columns="80" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:TextBox ID="txtContent" runat="server" Columns="105" Rows="26" TextMode="MultiLine" CssClass="input"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="control-box" runat="server" id="trControlBox">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
            OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel"
            CausesValidation="False" OnClick="cmdCancel_Click" />
        <div class="pull-right">
            <asp:Button ID="cmdOpen" runat="server" Text="Open Existing..." CausesValidation="False"
                OnClick="cmdOpen_Click" Visible="False" CssClass="btn btn-default" />
        </div>
    </div>
</div>
<label style="color: Red; padding: 5px" id="divMessage" runat="server"></label>
