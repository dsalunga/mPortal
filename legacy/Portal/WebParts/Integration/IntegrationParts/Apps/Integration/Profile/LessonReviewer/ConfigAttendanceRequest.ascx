<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigAttendanceRequest.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.LessonReviewer.ConfigAttendanceRequest" %>
<div runat="server">
    <br />
    <h3>Make-Up Session Info</h3>
    <table>
        <tr>
            <td style="width: 150px">Name
            </td>
            <td>
                <span style="font-weight: bold" id="lblName" runat="server"></span>
            </td>
        </tr>
        <%--<tr>
            <td>Locale Group
            </td>
            <td>
                <span runat="server" id="lblLocaleGroup" style="font-weight: bold">Brown</span>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2">&nbsp;
            </td>
        </tr>
        <tr>
            <td>Service Type
            </td>
            <td>
                <span runat="server" id="lblServiceType" style="font-weight: bold"></span>
            </td>
        </tr>
        <tr>
            <td>Service Date
            </td>
            <td>
                <span runat="server" id="lblServiceDate" style="font-weight: bold"></span>
            </td>
        </tr>
        <tr runat="server" id="panelServiceDuration" visible="false">
            <td>Service Duration
            </td>
            <td>
                <span runat="server" id="lblServiceDuration" style="font-weight: bold"></span>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;
            </td>
        </tr>
        <tr>
            <td>Playback Date
            </td>
            <td>
                <span style="font-weight: bold" runat="server" id="lblPlaybackDate">February 22, 2011
                    / 8:22 PM to 10:25 PM</span>
            </td>
        </tr>
        <tr>
            <td>Playback Duration
            </td>
            <td>
                <span runat="server" id="lblPlaybackDuration" style="font-weight: bold">2 hours, 15
                    minutes</span>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div id="panelMemberRemarks" runat="server">
        <h3>Member's Remarks</h3>
        <table>
            <tr>
                <td style="width: 150px; vertical-align: top">Reason for being absent
                </td>
                <td style="vertical-align: top">
                    <span style="font-weight: bold" id="lblReason" runat="server"></span>
                </td>
            </tr>
            <tr runat="server" id="panelAdditionalNotes" visible="false">
                <td style="vertical-align: top">Additional notes
                </td>
                <td style="vertical-align: top">
                    <span runat="server" id="lblNotes" style="font-weight: bold">Brown</span>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <h3 style="margin-bottom: 5px">Approval Status</h3>
    <img src="" runat="server" id="imageStatus" alt="" />
    <div runat="server" id="panelApprovalInfo" visible="false">
        <br />
        <div style="padding-top: 3px;">
            <table>
                <tr id="panelCouncillorNotes" runat="server" visible="false">
                    <td style="width: 150px; vertical-align: top">Councillor Notes
                    </td>
                    <td style="vertical-align: top">
                        <span runat="server" id="lblCouncillorNotes"></span>
                    </td>
                </tr>
                <tr id="panelAssignedCouncillor" runat="server" visible="false">
                    <td style="width: 150px">Assigned Councillor
                    </td>
                    <td>
                        <span runat="server" id="lblAssignedCouncillor" style="font-weight: bold"></span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <br />
    <div style="margin-top: 5px" runat="server" id="panelApproval" visible="false">
        <div>
            <asp:RadioButton ClientIDMode="Static" ID="radioApprove" GroupName="Attendance" Text="<strong>Approve</strong>"
                runat="server" Checked="True" />
        </div>
        <div>
            <asp:RadioButton ID="radioReject" GroupName="Attendance" ClientIDMode="Static" Text="<strong>Disapprove</strong> (please enter reason below)"
                runat="server" />
        </div>
        <div>
            <asp:RadioButton ClientIDMode="Static" ID="radioSendMessage" GroupName="Attendance"
                Text="<strong>Send Message Only</strong> (enter your message below)" runat="server" />
        </div>
        <div>
            <br />
            <asp:CheckBox ID="chkNotes" runat="server" Text="Notes to member" ClientIDMode="Static" />
            <div style="padding-left: 20px">
                <asp:TextBox ClientIDMode="Static" ID="txtNote" runat="server" Columns="40" MaxLength="4000"
                    Rows="5" TextMode="MultiLine" CssClass="input">
                </asp:TextBox>
                <br />
                <asp:CheckBox ID="chkSendCopy" runat="server" Text="Send me a copy" ClientIDMode="Static" />
                <br />
                <br />
            </div>
        </div>
    </div>
    <div style="margin-top: 5px">
        <asp:Button ID="cmdLogin" OnClientClick="return SubmitApproval();" ClientIDMode="Static"
            CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="cmdLogin_Click" />&nbsp;<a href="#" runat="server" id="linkClose" class="btn btn-default">Close</a>
    </div>
    <br />
    <div style="color: Red">
        <asp:Literal ID="lblMsg" runat="server" EnableViewState="False"></asp:Literal>
    </div>
</div>
<div runat="server" id="panelSuccess" visible="false">
    <h3>Attendance Sent for Councillor's Approval!</h3>
    <div>
        Thank you! You have successfully completed your Make-Up session.
    </div>
    <br />
    <br />
    <div>
        <a href="" runat="server" id="linkStartOver"><strong>Return to Make-Up Home</strong></a>
    </div>
</div>
<div runat="server" id="panelNotRequired" visible="false">
    <h3>Attendance Not Required</h3>
    <div>
        There is already an attendance entry for this Make-Up session or an attendance request
        has been submitted for worker's approval.
    </div>
    <br />
    <br />
    <div>
        <a href="" runat="server" id="linkMakeUpHome"><strong>Return to Make-Up Home</strong></a>
    </div>
</div>
<div runat="server" id="panelError" visible="false">
    <h3>Invalid Make-Up session.</h3>
</div>
<script type="text/javascript">
    function SubmitApproval() {
        if ($("#chkNotes").is(":checked") && $("#txtNote").val() == "") {
            if ($("#radioReject").is(":checked")) {
                alert("Please enter the reason for rejecting this attendance request.");
            }
            else {
                alert("Please enter a message.");
            }
            return false;
        }
        return true;
    }

    function InitInputs() {
        var rNote = $("#radioApprove");

        if (rNote.length > 0) {
            rNote.change(function () {
                EnableCheckBox();
            });

            $("#radioReject").change(function () {
                EnableCheckBox();
            });

            $("#radioSendMessage").change(function () {
                EnableCheckBox();
            });

            $("#chkNotes").change(function () {
                EnableTextBox();
            });

            EnableCheckBox();
            EnableTextBox();
        }
    }

    function EnableCheckBox() {
        var cNotes = $("#chkNotes");
        if ($("#radioApprove").is(":checked")) {
            $(cNotes).attr("disabled", false);
        }

        if ($("#radioReject").is(":checked") || $("#radioSendMessage").is(":checked")) {
            $(cNotes).attr("checked", true);
            $(cNotes).attr("disabled", true);
        }
        EnableTextBox();
    }

    function EnableTextBox() {
        $("#txtNote").attr("disabled", !$("#chkNotes").is(":checked"));
        $("#chkSendCopy").attr("disabled", !$("#chkNotes").is(":checked"));
    }

    $(document).ready(function () {
        InitInputs();
    })
</script>
