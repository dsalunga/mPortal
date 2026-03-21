<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SessionStart.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.LessonReviewer.MakeUpStart" %>
<style type="text/css">
    ul.makeup-steps {
        margin-left: 20px;
    }
    ul.makeup-steps li
    {
        margin-bottom: 10px;
    }
    
    ul.makeup-steps li strong
    {
        display: block;
    }
</style>
<asp:HiddenField ID="hAllowed" Value="1" runat="server" ClientIDMode="Static" />
<div id="panelPendingApproval" style="display: none;">
    <div class="alert alert-warning">
        <strong>Hello!</strong> You already submitted an attendance for this gathering, pending for worker's review. Are you sure you want to continue?
    </div>
</div>
<div id="panelErrorMgs" class="alert alert-danger" style="display: none;">
    Sorry, but the <strong>Make-Up Streaming Service is not accessible</strong>. Please make sure you
    are connected to the Singapore Locale Network and check whether the Make-Up Streaming
    Service is up. Kindly approach the assigned worker for assistance. You cannot continue
    your Make-Up session.
</div>
<div id="panelVideoNotAvailable" class="alert alert-danger" style="display: none;">
    Sorry, but the <strong>Services Video files for this Make-Up session are not available</strong>.
    Kindly approach the assigned worker for assistance. You cannot continue your Make-Up
    session.
</div>
<div class="integration-lessonreviewer">
    <br />
    <h4>
        Make-Up Session</h4>
    <table>
        <tr>
            <td style="width: 150px">
                Name
            </td>
            <td>
                <span style="font-weight: bold" id="lblName" runat="server"></span>
            </td>
        </tr>
        <%--<tr>
            <td>
                Locale Group
            </td>
            <td>
                <span runat="server" id="lblLocaleGroup" style="font-weight: bold">Brown</span>
            </td>
        </tr>--%>
        <%--<tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>--%>
        <tr>
            <td>
                Service Type
            </td>
            <td>
                <span runat="server" id="lblServiceType" style="font-weight: bold"></span>
            </td>
        </tr>
        <tr>
            <td>
                Service Date
            </td>
            <td>
                <span runat="server" id="lblServiceDate" style="font-weight: bold"></span>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <h4>
        Make-Up Process Outline</h4>
    <ul class="makeup-steps">
        <li><strong>Step 1</strong> Reading of News, Announcements, and Updates</li>
        <li><strong>Step 2</strong> Video Playback</li>
        <li><strong>Final Step</strong> Logging of Attendance - to be authorized by the assigned
            worker</li>
    </ul>
    <br />
    <br />
    <div id="panelButtons" style="display: none">
        <asp:Button ID="cmdContinue" CssClass="btn btn-primary" ClientIDMode="Static" runat="server"
            Text="Begin Make-Up" OnClick="cmdContinue_Click" />&nbsp;<asp:Button
                ID="cmdCancel" CssClass="btn btn-default" ClientIDMode="Static" runat="server" Text="Cancel"
                Width="75px" OnClick="cmdCancel_Click" />
    </div>
</div>
<div id="panelLoading">
    <br />
    <h3>
        <img src="/Content/Assets/Images/roller.gif" alt="Loading..." />&nbsp;Checking Make-Up Streaming
        Service...
    </h3>
</div>
<asp:Literal ID="scriptMakeUpTest" runat="server"></asp:Literal>
<asp:Literal ID="scriptPlaybackTest" runat="server"></asp:Literal>
<script type="text/javascript">
    $(document).ready(function () {
        DisableInputs(true);

        // Streaming Service Check
        if (typeof makeUpTest == "undefined" || makeUpTest != "OK") {
            // Failed
            FailedCheck();
        }
        else {
            // Playback Check
            if (typeof playbackTest == "undefined" || playbackTest != "OK") {
                // Failed
                FailedVideoCheck();
            }
            else {
                // Success
                HideLoader();
                DisableInputs(false);

                if ($("#hAllowed").val() != "1") {
                    PendingAttendance();
                }
            }
        }

        $("#panelButtons").css("display", "");
    });

    function DisableInputs(disabled) {
        $("#cmdContinue").attr("disabled", disabled);
    }

    function FailedCheck() {
        HideLoader();
        $("#panelErrorMgs").css("display", "");
    }

    function FailedVideoCheck() {
        HideLoader();
        $("#panelVideoNotAvailable").css("display", "");
    }

    function PendingAttendance() {
        HideLoader();
        $("#panelPendingApproval").css("display", "");
    }

    function HideLoader() {
        $("#panelLoading").css("display", "none");
    }
</script>
