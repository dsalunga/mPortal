<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartAdminEntry.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartAdminEntry" %>
<h1 class="central page-header">App Admin Control
</h1>
<table width="100%">
    <tr>
        <td style="width: 130px">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Owner App:
        </td>
        <td>
            <asp:DropDownList ID="cboParts" runat="server" DataTextField="Name" DataValueField="Id" CssClass="input">
                <asp:ListItem Selected="True" Value="-1"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>File Name:<asp:RequiredFieldValidator ID="rfvControlFile" runat="server" ControlToValidate="txtControlURL"
            ErrorMessage="Control File">*</asp:RequiredFieldValidator>
        </td>
        <td class="min-bottom-margin">
            <asp:TextBox ID="txtControlURL" runat="server" Columns="55" CssClass="input"></asp:TextBox>&nbsp;
                        <input id="cmdFile" class="btn btn-default btn-sm" type="button" value="Upload"
                            name="cmdFile" runat="server">
        </td>
    </tr>
    <tr>
        <td>Template Engine:
        </td>
        <td>
            <asp:DropDownList ID="cboTemplateEngine" runat="server" CssClass="input">
                <asp:ListItem Value="1">ASPX</asp:ListItem>
                <asp:ListItem Value="2">Razor</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkActive" CssClass="aspnet-checkbox" runat="server" Text="Active" Checked="True" />
            &nbsp;
                        <asp:CheckBox ID="chkVisible" CssClass="aspnet-checkbox" runat="server" Text="Visible" Checked="True" />
            &nbsp;
                        <asp:CheckBox ID="chkInSiteContext" CssClass="aspnet-checkbox" runat="server" Text="Site Context" Checked="false" />
            &nbsp;
                        <asp:CheckBox ID="chkAutoTitle" CssClass="aspnet-checkbox" runat="server" Text="Set Title" Checked="true" />
        </td>
    </tr>
    <%--<tr>
                    <td style="width: 130px">
                        Identity:<asp:RequiredFieldValidator ID="rfvPage" runat="server" ControlToValidate="txtIdentity"
                            ErrorMessage="Page ID">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox Enabled="false" ID="txtIdentity" runat="server" Columns="20">1</asp:TextBox>
                    </td>
                </tr>--%>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" />
    </div>
</div>

