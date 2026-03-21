<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeeklyScheduler.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.WeeklyScheduler.WeeklyScheduler" %>
<div id="panelSource" runat="server" visible="false">
    <style type="text/css">
        .weekly-scheduler
        {
            margin-left: 4.65pt;
            border-collapse: collapse;
        }
        
        .weekly-scheduler .day-wrapper
        {
            border-width: 1.5pt 1.5pt medium medium;
            border-style: solid solid none none;
            background: none repeat scroll 0% 0% rgb(217, 217, 217);
            padding: 0in 5.4pt;
            height: 19.5pt;
            vertical-align: bottom;
            white-space: nowrap;
        }
        
        .weekly-scheduler .subject-label
        {
            border-width: 1.5pt;
            border-style: solid;
            vertical-align: middle;
            border-color: windowtext windowtext black;
        }
        
        .weekly-scheduler .day-wrapper div
        {
            margin-bottom: 0.0001pt;
            text-align: center;
            line-height: normal;
            font-weight: bold;
            font-size: 14pt;
            color: black;
        }
        
        .weekly-scheduler .date-wrapper
        {
            white-space: nowrap;
            vertical-align: bottom;
            border-width: medium 1.5pt 1.5pt medium;
            border-style: none solid solid none;
            background: none repeat scroll 0% 0% rgb(217, 217, 217);
            padding: 0in 5.4pt;
        }
        
        .weekly-scheduler .date-wrapper div
        {
            margin-bottom: 0.0001pt;
            text-align: center;
            line-height: normal;
            color: black;
            font-weight: bold;
        }
        
        .weekly-scheduler .column-filler
        {
            padding: 0in 5.4pt;
            vertical-align: bottom;
            white-space: nowrap;
        }
        
        .weekly-scheduler .schedule-header
        {
            white-space: nowrap;
            vertical-align: bottom;
            padding: 0in 5.4pt;
        }
        
        .weekly-scheduler .schedule-header div
        {
            margin-bottom: 0.0001pt;
            text-align: center;
            line-height: normal;
            font-weight: bold;
            font-size: 14pt;
            color: black;
        }
        
        .weekly-scheduler .schedule-date
        {
            white-space: nowrap;
            vertical-align: bottom;
            padding: 0in 5.4pt;
        }
        
        .weekly-scheduler .schedule-date div
        {
            margin-bottom: 0.0001pt;
            text-align: center;
            line-height: normal;
            color: black;
            font-weight: bold;
            font-style: italic;
        }
        
        .weekly-scheduler .schedule-topic
        {
            white-space: nowrap;
            border-width: medium 1pt 1pt 1.5pt;
            border-style: none solid solid;
            border-color: -moz-use-text-color windowtext black;
            background: none repeat scroll 0% 0% rgb(217, 217, 217);
            padding: 0in 5.4pt;
        }
        
        .weekly-scheduler .schedule-topic div
        {
            margin-bottom: 0.0001pt;
            line-height: normal;
            color: black;
            font-weight: bold;
        }
        
        .weekly-scheduler .schedule-entry-container
        {
            border-width: 1pt;
            border-style: solid;
            padding: 0in 1pt;
            white-space: nowrap;
        }
        
        .weekly-scheduler .schedule-entry
        {
            text-align: center;
            margin-bottom: 0.0001pt;
            line-height: normal;
            color: black;
        }
        
        .weekly-scheduler .schedule-entry-successor
        {
            border-style: dashed none none none;
            border-width: 1pt medium medium medium;
        }
        
        .weekly-scheduler .footer-note
        {
            padding: 0in 5.4pt;
            vertical-align: bottom;
            white-space: nowrap;
        }
        
        .weekly-scheduler .footer-note div
        {
            margin-bottom: 0.0001pt;
            line-height: normal;
            color: black;
            font-weight: bold;
        }
    </style>
    <table cellspacing="0" cellpadding="0" border="0" class="weekly-scheduler">
        <tbody>
            <tr>
                <td class="schedule-header" colspan="4">
                    <div>
                        KNC Destino
                    </div>
                </td>
            </tr>
            <tr>
                <td class="schedule-date" colspan="4">
                    <div>
                        November 24, 27-28, 2010</div>
                </td>
            </tr>
            <tr>
                <td class="column-filler">
                    &nbsp;
                </td>
                <td class="column-filler">
                    &nbsp;
                </td>
                <td class="column-filler">
                    &nbsp;
                </td>
                <td class="column-filler">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="day-wrapper subject-label" rowspan="2" style="">
                    <div>
                        SUBJECT</div>
                </td>
                <td class="day-wrapper">
                    <div>
                        Wednesday</div>
                </td>
                <td class="day-wrapper">
                    <div>
                        Saturday</div>
                </td>
                <td class="day-wrapper">
                    <div>
                        Sunday</div>
                </td>
            </tr>
            <tr>
                <td class="date-wrapper">
                    <div>
                        24</div>
                </td>
                <td class="date-wrapper">
                    <div>
                        27</div>
                </td>
                <td class="date-wrapper">
                    <div>
                        28</div>
                </td>
            </tr>
            <tr style="">
                <td class="schedule-topic">
                    <div>
                        LIBRENG TAWAG</div>
                </td>
                <td class="schedule-entry-container">
                    <div class="schedule-entry">
                        x</div>
                </td>
                <td class="schedule-entry-container">
                    <div class="schedule-entry">
                        x</div>
                    <div class="schedule-entry schedule-entry-successor">
                        x</div>
                </td>
                <td class="schedule-entry-container">
                    <div class="schedule-entry">
                        Sis. Mae</div>
                    <div class="schedule-entry schedule-entry-successor">
                        Sis. Pia</div>
                    <div class="schedule-entry schedule-entry-successor">
                        Bro. Greg</div>
                </td>
            </tr>
            <tr>
                <td class="column-filler">
                    &nbsp;
                </td>
                <td class="column-filler">
                    &nbsp;
                </td>
                <td class="column-filler">
                    &nbsp;
                </td>
                <td class="column-filler">
                    &nbsp;
                </td>
            </tr>
            <tr style="">
                <td class="column-filler">
                    &nbsp;
                </td>
                <td class="column-filler">
                    &nbsp;
                </td>
                <td class="column-filler">
                    &nbsp;
                </td>
                <td class="column-filler">
                    &nbsp;
                </td>
            </tr>
            <tr style="">
                <td class="footer-note">
                    <div>
                        Prepared by:</div>
                </td>
                <td class="footer-note">
                    &nbsp;
                </td>
                <td class="footer-note">
                    <div>
                        Noted by:</div>
                </td>
                <td class="footer-note">
                    &nbsp;
                </td>
            </tr>
            <tr style="">
                <td class="footer-note" colspan="2">
                    <div>
                        <em>Bro. Jun Gundayao</em></div>
                </td>
                <td class="footer-note" colspan="2">
                    <div>
                        <em>Bro. Robert Navoa</em></div>
                </td>
            </tr>
        </tbody>
    </table>
</div>
