<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskView.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Agent.TaskView" %>
<table border="0" width="100%">
    <tr>
        <td style="width: 125px">Name:
        </td>
        <td>
            <strong><span runat="server" id="lblName"></span></strong>
        </td>
    </tr>
    <tr>
        <td>Description:
        </td>
        <td>
            <span runat="server" id="lblDescription"></span>
        </td>
    </tr>
    <tr>
        <td>Recurrence:
        </td>
        <td>
            <span id="lblRecurrence" runat="server"></span>,&nbsp;<span id="lblOccursEvery" runat="server"></span>&nbsp;(Occurrence)
        </td>
    </tr>
    <tr>
        <td>Weekdays:
        </td>
        <td>
            <span id="lblWeekdays" runat="server"></span>
        </td>
    </tr>
    <tr>
        <td>Start Date:
        </td>
        <td>
            <span id="lblStartDate" runat="server"></span>
        </td>
    </tr>
    <tr>
        <td>End Date:
        </td>
        <td>
            <span id="lblEndDate" runat="server"></span>
        </td>
    </tr>
    <tr>
        <td valign="top">Type Name:
        </td>
        <td>
            <span id="lblTypeName" runat="server"></span>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkEnabled" Enabled="false" CssClass="aspnet-checkbox" runat="server" Text="Enabled" Checked="True"></asp:CheckBox>
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
            <span id="lblExecutionStartDate" runat="server"></span>
        </td>
    </tr>
    <tr>
        <td valign="top">Recent End Date:
        </td>
        <td>
            <span id="lblExecutionEndDate" runat="server"></span>
        </td>
    </tr>
    <tr>
        <td valign="top">Recent Status:
        </td>
        <td>
            <span id="lblExecutionStatus" runat="server"></span>
        </td>
    </tr>
    <tr>
        <td valign="top">Recent Message:
        </td>
        <td>
            <span id="lblExecutionMessage" runat="server"></span>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-default" runat="server" Text="Edit"
            OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Close"
            OnClick="cmdCancel_Click" CausesValidation="False" />
        <div class="pull-right">
            <asp:Button ID="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                CssClass="btn btn-default" runat="server" Text="Delete" OnClick="cmdDelete_Click" />
            <asp:Button ID="cmdExecute" CssClass="btn btn-default" runat="server" Text="Execute" OnClick="cmdExecute_Click" />
            <asp:Button ID="cmdForceExecute" CssClass="btn btn-default" runat="server" Text="Force Execute"
                OnClick="cmdForceExecute_Click" />
        </div>
    </div>
</div>
<br />
<span id="lblStatus" runat="server" style="color: Red;" enableviewstate="false"></span>
<table width="100%">
    <tr runat="server" id="rowParameters">
        <td>
            <table border="0" cellpadding="0">
                <tr>
                    <td rowspan="2">
                        <a id="linkParameters" runat="server" href="">
                            <img src="/Content/Assets/Images/piece.png" class="TaskListIcon" border="0" /></a>
                    </td>
                    <td class="Header">Parameters
                    </td>
                </tr>
                <tr>
                    <td valign="top">Place description here
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <br />
        </td>
    </tr>
</table>
