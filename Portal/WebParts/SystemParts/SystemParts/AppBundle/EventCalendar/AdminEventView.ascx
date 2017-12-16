<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminEventView.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.EventCalendar.AdminEventView" %>
<style type="text/css">
    div.wp-Event-Manager div.field {
        margin-bottom: 15px;
        clear: both;
    }

        div.wp-Event-Manager div.field.inline {
            float: left;
            clear: right;
            margin-right: 20%;
        }

    div.wp-Event-Manager div.return-link {
        margin-top: 50px;
    }
</style>
<div class="wp-Event-Manager">
    <div class="field">
        <h2 id="lblEventSubject" runat="server" style="padding: 3px; display: inline;">Event Title
        </h2>
    </div>
    <div class="field inline">
        <div>
            <strong>Date</strong>
        </div>
        <div id="lblEventDate" runat="server">
            Date
        </div>
    </div>
    <div class="field inline">
        <div>
            <strong>Time</strong>
        </div>
        <div id="lblEventTime" runat="server">
            Time
        </div>
    </div>
    <div runat="server" id="panelRecurrence" visible="false" class="field inline">
        <div>
            <strong>Recurrence</strong>
        </div>
        <div id="lblRecurrence" runat="server">
            Recurrence
        </div>
    </div>
    <div class="field">
        <div>
            <strong>Location</strong>
        </div>
        <div id="lblEventLocation" runat="server">
            Location
        </div>
    </div>
    <%--<div class="field">
        <div>
            <strong>Recurrence</strong></div>
        <div runat="server" id="lblRecurrence">
            Recurrence</div>
    </div>--%>
    <div class="field">
        <div>
            <strong>Description</strong>
        </div>
        <div runat="server" id="lblEventDescription">
            Content
        </div>
    </div>
</div>
<br />
<span id="lblStatus" runat="server" style="color: Red;" enableviewstate="false"></span>
<br />
<div class="control-box">
    <div>
        <asp:Button ID="cmdEdit" runat="server" CssClass="btn btn-default" Text="Edit" OnClick="cmdEdit_Click" />
        <asp:Button CausesValidation="false" ID="cmdDelete" CssClass="btn btn-default" runat="server"
            Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
            OnClick="cmdDelete_Click" />
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Close" OnClick="cmdCancel_Click"
            CausesValidation="False" />
        <div class="pull-right">
            <asp:Button CausesValidation="false" ID="cmdSendReminder" CssClass="btn btn-default" runat="server"
                Text="Send Reminder" OnClientClick="return confirm('Are you sure you want to send a reminder now?');"
                OnClick="cmdSendReminder_Click" />
        </div>
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
