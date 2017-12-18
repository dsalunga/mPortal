<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskEditor.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Agent.TaskEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<table border="0">
    <tr>
        <td>Name:<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top">Description:
        </td>
        <td>
            <asp:TextBox ID="txtDesription" runat="server" Columns="75" Rows="3" TextMode="MultiLine" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Recurrence:
        </td>
        <td>
            <asp:DropDownList ID="cboRecurrence" runat="server" DataTextField="Value" DataValueField="Key" CssClass="input">
            </asp:DropDownList>
            &nbsp;Every:&nbsp;<asp:TextBox ID="txtOccursEvery" runat="server" CssClass="input" Columns="10"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Weekdays:
        </td>
        <td>
            <asp:CheckBoxList CssClass="checkbox-list" ID="cblWeekdays" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td>Start Date:
        </td>
        <td>
            <asp:TextBox ID="txtStartDate" runat="server" Columns="30" CssClass="input"></asp:TextBox>
            <asp:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" Enabled="True"
                TargetControlID="txtStartDate">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>End Date:
        </td>
        <td>
            <asp:TextBox ID="txtEndDate" runat="server" Columns="30" CssClass="input"></asp:TextBox>
            <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" Enabled="True"
                TargetControlID="txtEndDate">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td valign="top">Type Name:
        </td>
        <td>
            <asp:TextBox ID="txtTypeName" runat="server" Columns="75" CssClass="input" Rows="3" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkEnabled" runat="server" Text="Enabled" Checked="True"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
        <td valign="top">Recent Start Date:
        </td>
        <td>
            <asp:TextBox ID="txtExecutionStartDate" runat="server" Columns="30" CssClass="input" ReadOnly="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top">Recent End Date:
        </td>
        <td>
            <asp:TextBox ID="txtExecutionEndDate" runat="server" Columns="30" CssClass="input" ReadOnly="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top">Recent Status:
        </td>
        <td>
            <asp:DropDownList ID="cboExecutionStatus" runat="server" CssClass="input" DataTextField="Value" DataValueField="Key">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td valign="top">Recent Message:
        </td>
        <td>
            <asp:TextBox ID="txtExecutionMessage" runat="server" Columns="75" CssClass="input" Rows="3" TextMode="MultiLine"
                ReadOnly="True"></asp:TextBox>
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
