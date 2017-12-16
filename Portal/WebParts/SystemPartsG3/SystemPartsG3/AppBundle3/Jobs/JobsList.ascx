<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobsList.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Jobs.JobsList" %>

<h1>Job Search</h1>
<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Search" />
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

<table cellspacing='0' style='font-family:arial;'>
    <tr>
        <td style='font-size:16px;color:#FF6600'><b>what</b></td>
        <td style='font-size:16px;color:#FF6600'><b>where</b></td>
        <td> </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtWhat" runat="server" Width="150px" />
        </td>
        <td>
            <asp:TextBox ID="txtWhere" runat="server" Width="150px" />
        </td>
        <td>
            <asp:Button ID="btnFindJobs" Text="Find Jobs" runat="server" 
                onclick="btnFindJobs_Click" />
        </td>
    </tr>
    <tr>
        <td style='font-size:10px; vertical-align: top'>job title, keywords or company</td>
        <td colspan='2' style='font-size:10px;padding:0px;margin:0px;border:0px; vertical-align: top' >
            <table cellpadding='0' style='padding:0px;margin:0px;border:0px; width: 100%'>
                <tr>
                    <td style='font-size:10px;padding:0px;margin:0px;border:0px;vertical-align: top'>city, state or zip</td>
                    <td style='font-size:13px; text-align:right'>
        
                        <span id=indeed_at><a href="http://www.indeed.com/?indpubnum=3462123152151013" 
                            style="text-decoration:none; color: #000">jobs</a> by 
                            <a href="http://www.indeed.com/?indpubnum=3462123152151013" title="Job Search">
                                <img src="http://www.indeed.com/p/jobsearch.gif" style="border: 0;vertical-align: middle;" alt="job search" />
                            </a>
                        </span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
