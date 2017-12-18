<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebTheme.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Template.WebThemeViewController" %>
<h1 class="central page-header">
            <asp:Literal ID="lMessage" runat="server" Text="Web Template"></asp:Literal>
        </h1>
<table>
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td width="120">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Name" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Columns="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Folder Name:<asp:RequiredFieldValidator ID="rfvIdentity" runat="server" ControlToValidate="txtIdentity"
                        ErrorMessage="Identity" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdentity" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="panelParent">
                    <td>Parent:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboParent" runat="server"
                            DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr runat="server" id="panelDefaultTemplate">
                    <td>Default Template:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboTemplates" runat="server"
                            DataTextField="Name" DataValueField="Id">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" id="panelDefaultSkin">
                    <td>Default Skin:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboSkins" DataTextField="Name" DataValueField="Id" runat="server"
                            AppendDataBoundItems="True">
                            <asp:ListItem Selected="True" Value="-1"># None #</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" CssClass="btn btn-default" />
    </div>
</div>
