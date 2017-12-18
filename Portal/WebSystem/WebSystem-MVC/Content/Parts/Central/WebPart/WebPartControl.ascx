<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartControl.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartControlController" %>

<h1 class="central page-header">Web Part Control
</h1>
<table width="100%">
    <tr>
        <td width="125">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name" SetFocusOnError="True" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Identity:<asp:RequiredFieldValidator ID="rfvIdentity" runat="server" ErrorMessage="Control #"
            SetFocusOnError="True" ControlToValidate="txtIdentity">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtIdentity" runat="server" Columns="45" CssClass="input"></asp:TextBox>&nbsp;
                        <asp:CheckBox ID="chkEntryPoint" CssClass="aspnet-checkbox" runat="server" Checked="true" Text="Is Entry Point" />
        </td>
    </tr>
    <tr runat="server" visible="false" id="panelPath">
        <td>Path:<asp:RequiredFieldValidator ID="rfvPath" ForeColor="Red" runat="server" ControlToValidate="txtPath"
            ErrorMessage="Path">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtPath" runat="server" Columns="75" CssClass="input"></asp:TextBox>
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
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
        <td>Admin Control:
        </td>
        <td>
            <asp:DropDownList ID="cboPartAdmins" runat="server" CssClass="input">
                <asp:ListItem Selected="True" Value="-1"># Select Admin #</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr runat="server" visible="false" id="panelConfig">
        <td></td>
        <td>
            <asp:DropDownList ID="cboConfig" runat="server" CssClass="input" DataSourceID="ObjectDataSource1"
                DataTextField="Name" DataValueField="FileName" AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value=""># Select Config #</asp:ListItem>
            </asp:DropDownList>&nbsp;<em>(Deprecated)</em>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                TypeName="WCMS.WebSystem.WebParts.Central.WebPartControlController">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="-1" Name="partId" QueryStringField="PartId" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" />
    </div>
</div>
