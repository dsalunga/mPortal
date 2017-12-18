<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartControlTemplate.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartControlTemplateController" %>
<h1 class="central page-header">Web Part Template
</h1>
<table width="100%">
    <tr>
        <td width="125">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <%--<tr>
                    <td>
                        File Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFileName" Enabled="false" runat="server" Columns="45"></asp:TextBox>&nbsp;
                        <asp:Button ID="cmdUpload" runat="server" Text="Upload" CausesValidation="false"
                            Width="85px" Enabled="False" />
                    </td>
                </tr>--%>
    <tr>
        <td>Path:<asp:RequiredFieldValidator ID="rfvPath" runat="server" ControlToValidate="txtPath"
            ErrorMessage="Path">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtPath" runat="server" Columns="75" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Identity:<asp:RequiredFieldValidator ID="rfvTemplateID" runat="server" ControlToValidate="txtIdentity"
            ErrorMessage="Identity">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtIdentity" runat="server" Columns="25" CssClass="input"></asp:TextBox>

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
            <asp:CheckBox ID="chkHidden" CssClass="aspnet-checkbox" runat="server" Text="Hidden" ClientIDMode="Static" Enabled="False" /><br />
            <asp:CheckBox ID="chkStandalone" CssClass="aspnet-checkbox" runat="server" Text="Standalone" ClientIDMode="Static" Checked="false" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <%--<tr>
                    <td valign="top">
                        Thumbnail:
                    </td>
                    <td valign="top">
                        <asp:Image Width="96px" ID="imageThumbnail" ImageUrl="~/Content/Assets/Images/PartThumb.jpg" runat="server" />
                        <asp:FileUpload ID="FileUpload1" runat="server" Enabled="False" />
                    </td>
                </tr>--%>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdateContinue" runat="server" Text="Update"
            OnClick="cmdUpdateContinue_Click" CssClass="btn btn-primary" />
        <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click" Enabled="False" Visible="false" />
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" />
    </div>
</div>
