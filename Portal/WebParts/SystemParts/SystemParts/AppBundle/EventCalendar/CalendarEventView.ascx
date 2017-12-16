<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarEventView.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.EventCalendar.CalendarEventView" %>
<div style="font-family: verdana,helvetica,sans-serif; font-size: 12pt">
    <table width="100%" cellspacing="0" cellpadding="0" align="center">
        <tbody>
            <tr>
                <td height="75" bgcolor="#AEC76C" style="background-image: url(/WebParts/EventCalendar/Templates/green_email_template/earth_greener_bg_head.jpg);"
                    align="right">
                </td>
            </tr>
            <tr>
                <td width="100%" height="100%" bgcolor="#AEC76C" valign="top">
                    <table width="100%" height="100%" style="table-layout: fixed;" cellspacing="0" cellpadding="0"
                        align="center">
                        <tbody>
                            <tr>
                                <td width="10">
                                </td>
                                <td valign="top" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                    <table width="100%" style="white-space: normal;" cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td height="10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-family: verdana,helvetica,sans-serif; font-size: 10pt; color: rgb(122, 95, 167);">
                                                        <span style="color: rgb(127, 63, 0);">Dear Brethren,</span><br />
                                                        <br />
                                                        <span style="color: rgb(127, 63, 0);">This is a reminder for <strong id="lblEventSubject"
                                                            runat="server">{Event:Subject}</strong> to be held on <strong id="lblEventDate" runat="server">
                                                                {Event:Date}</strong> at the <strong id="lblEventLocation" runat="server">{Event:Location}</strong>
                                                            at <strong id="lblEventTime" runat="server">{Event:Time}</strong>.<br />
                                                            <br />
                                                            <span id="lblEventDescription" runat="server">{Event:Description}</span>
                                                            <br />
                                                            Please do not miss this important event.
                                                            <br />
                                                            <br />
                                                            <br />
                                                        </span>
                                                        <br />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="10">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td width="10">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="160" bgcolor="#AEC76C" background="/WebParts/EventCalendar/Templates/green_email_template/earth_greener_bg_body.jpg"
                    align="right">
                </td>
            </tr>
        </tbody>
    </table>
</div>
<br />
<a runat="server" id="linkCalendarView" href="#">return to Calendar View</a>