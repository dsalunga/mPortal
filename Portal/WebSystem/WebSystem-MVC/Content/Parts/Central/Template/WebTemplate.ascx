<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebTemplate.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Template.WebTemplateController" %>
<h1 class="central page-header">
    <asp:Literal ID="lMessage" runat="server" Text="Web Template"></asp:Literal>
</h1>
<table width="100%">
    <tr>
        <td width="130">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
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
    <tr>
        <td>File Name:<asp:RequiredFieldValidator ID="rfvControlFile" runat="server" ControlToValidate="txtControlURL"
            ErrorMessage="Control File" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td class="min-bottom-margin">
            <asp:TextBox ID="txtControlURL" runat="server" Columns="50"></asp:TextBox>&nbsp;
                        <input id="cmdFile" onclick="Upload('txtControlURL', '~/Templates', '&FileOnly=true');"
                            type="button" value="Upload" name="cmdFile" runat="server" class="btn btn-default" />
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
    <tr runat="server" id="panelTheme">
        <td>Theme:
        </td>
        <td>
            <asp:DropDownList ID="cboTheme" runat="server"
                DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                <asp:ListItem Value="-1" Text=""></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr runat="server" id="panelDefaultPanel">
        <td>Default Panel:
        </td>
        <td>
            <asp:DropDownList ID="cboPanels" runat="server" DataSourceID="ObjectDataSource1"
                DataTextField="Name" DataValueField="Id">
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetPanels"
                TypeName="WCMS.WebSystem.WebParts.Central.Template.WebTemplateController">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="-1" Name="templateId" QueryStringField="TemplateId"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
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
    <tr>
        <td>Template Engine:
        </td>
        <td>
            <asp:DropDownList ID="cboTemplateEngine" runat="server">
                <asp:ListItem Value="1">ASPX</asp:ListItem>
                <asp:ListItem Value="2">Razor</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkStandalone" runat="server" Text="Standalone / Primary / Entry Point" CssClass="aspnet-checkbox" Checked="true" />
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick="cmdUpdate_Click" CssClass="btn btn-primary" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" CssClass="btn btn-default" />
    </div>
</div>
