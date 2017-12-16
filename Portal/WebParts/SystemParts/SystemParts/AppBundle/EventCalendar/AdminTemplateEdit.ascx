<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminTemplateEdit.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.EventCalendar.AdminTemplateEdit" %>
<%@ Register Src="../../Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<table border="0" width="800">
    <tr>
        <td>Name:<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>ForeColor:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="txtForeColor" ErrorMessage="ForeColor">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <div style="float: left">
                <asp:TextBox ID="txtForeColor" runat="server" Columns="30" CssClass="input"></asp:TextBox>
                <asp:ColorPickerExtender ID="txtForeColor_ColorPickerExtender" runat="server" Enabled="True"
                    TargetControlID="txtForeColor" SampleControlID="panelForeColorSample">
                </asp:ColorPickerExtender>
            </div>
            <div style="width: 50px; height: 20px; float: left" runat="server" id="panelForeColorSample">
            </div>
        </td>
    </tr>
    <tr>
        <td>BackColor:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
            ControlToValidate="txtBackColor" ErrorMessage="BackColor">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <div style="float: left">
                <asp:TextBox ID="txtBackColor" runat="server" Columns="30" CssClass="input"></asp:TextBox>
                <asp:ColorPickerExtender ID="txtBackColor_ColorPickerExtender" runat="server" Enabled="True"
                    TargetControlID="txtBackColor" SampleControlID="panelBackColorSample">
                </asp:ColorPickerExtender>
            </div>
            <div style="width: 50px; height: 20px; float: left" runat="server" id="panelBackColorSample">
            </div>
        </td>
    </tr>
    <tr>
        <td valign="top">E-mail Template:
        </td>
        <td>
            <uc2:TextEditor ID="txtTemplate" runat="server" />
        </td>
    </tr>
    <tr>
        <td valign="top">SMS Template:
        </td>
        <td>
            <asp:TextBox ID="txtSmsContent" runat="server" Columns="70" Rows="8" CssClass="input"
                TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
            OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel"
            OnClick="cmdCancel_Click" CausesValidation="False" />
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
