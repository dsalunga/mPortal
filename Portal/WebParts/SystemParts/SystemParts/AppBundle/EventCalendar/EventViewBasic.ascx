<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventViewBasic.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.EventCalendar.EventViewBasic" %>
<style type="text/css">
    div.wp-Event-Manager div.field
    {
        margin-bottom: 15px;
        clear: both;
        font-size: larger;
    }
    
    div.wp-Event-Manager div.field.inline
    {
        float: left;
        clear: right;
        margin-right: 5%;
    }
    
    div.wp-Event-Manager div.return-link
    {
        margin-top: 50px;
    }
</style>
<div class="wp-Event-Manager">
    <div class="field">
        <h2 id="lblEventSubject" runat="server" style="padding: 3px; display: inline;">
            Event Title
        </h2>
    </div>
    <div class="field inline">
        <div>
            <strong>Date</strong></div>
        <div id="lblEventDate" runat="server">
            Date</div>
    </div>
    <div class="field inline">
        <div>
            <strong>Time</strong></div>
        <div id="lblEventTime" runat="server">
            Time</div>
    </div>
    <div class="field">
        <div>
            <strong>Location</strong></div>
        <div id="lblEventLocation" runat="server">
            Location</div>
    </div>
    <%--<div class="field">
        <div>
            <strong>Recurrence</strong></div>
        <div runat="server" id="lblRecurrence">
            Recurrence</div>
    </div>--%>
    <div class="field">
        <div>
            <strong>Description</strong></div>
        <div runat="server" id="lblEventDescription">
            Content</div>
    </div>
    <strong><em>Permalink:</em></strong>
    <br />
    <a runat="server" id="linkPermalink" href="#"></a>
    <br />
    <div class="return-link">
        <strong><a runat="server" id="linkCalendarView" href="#">Back to Calendar</a></strong>
    </div>
</div>
