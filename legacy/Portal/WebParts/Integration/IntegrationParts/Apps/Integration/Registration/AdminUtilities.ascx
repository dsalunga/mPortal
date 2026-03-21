<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminUtilities.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.AdminUtilities" %>
<fieldset>
    <legend>External Utilities</legend>
    <div style="padding: 10px">
        <div class="Header">
            Delete Attendance
        </div>
        <table>
            <tr>
                <td class="no-bottom-margin">Attendance ID:&nbsp;<asp:TextBox ID="txtAttendanceId" CssClass="input" runat="server"></asp:TextBox>&nbsp;
                </td>
                <td>
                    <asp:Button ID="cmdDelete" runat="server" Text="Delete" CssClass="btn btn-default btn-sm" OnClick="cmdDelete_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
        </table>
    </div>
    <div style="padding: 10px">
        <div class="Header">
            Create Attendance
        </div>
        <table>
            <tr>
                <td>Group ID:&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtExternalIDNo" CssClass="input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Service Schedule ID:&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtServiceScheduleId" CssClass="input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="cmdCreateAttendance" runat="server" Text="Create" CssClass="btn btn-default btn-sm" />
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
        </table>
    </div>
</fieldset>
<br />
<fieldset>
    <legend>Integration Ext Utilities</legend>
    <div style="padding: 10px">
        <div class="Header">
            GetUserInfo1
        </div>
        <div class="no-bottom-margin">
            Group ID:&nbsp;<asp:TextBox ID="txtSearchExternalId" CssClass="input" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="cmdONESubmit" runat="server" Text="Submit" OnClick="cmdONESubmit_Click" CssClass="btn btn-default btn-sm" />
        </div>
        <br />
        <table>
            <tr>
                <td>Username:</td>
                <td><strong>
                    <span runat="server" id="lblUsername"></span></strong></td>
            </tr>
            <tr>
                <td>Group ID:</td>
                <td><strong>
                    <span runat="server" id="lblExternalId"></span></strong></td>
            </tr>
            <tr>
                <td>Email:</td>
                <td><strong>
                    <span runat="server" id="lblEmail"></span></strong></td>
            </tr>
            <tr>
                <td>Last Name:</td>
                <td><strong>
                    <span runat="server" id="lblLastName"></span></strong></td>
            </tr>
            <tr>
                <td>First Name:</td>
                <td><strong>
                    <span runat="server" id="lblFirstName"></span></strong></td>
            </tr>
            <tr>
                <td>Middle Name:</td>
                <td><strong>
                    <span runat="server" id="lblMiddleName"></span></strong></td>
            </tr>
        </table>
    </div>
</fieldset>
<br />
<span runat="server" id="lblMessage" visible="false" style="padding: 3px; background-color: Red; color: White"></span>