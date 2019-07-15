<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsDashboard.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.LessonReviewer.NewsDashboard" %>
<style>
    .news-title:hover, .news-item:hover {
        text-decoration: none;
    }
</style>
<div id="panelLoading">
    <h3>
        <img src="/Content/Assets/Images/roller.gif" alt="Loading..." />&nbsp;Checking Make-Up Streaming
        Service...
    </h3>
</div>
<div id="panelErrorMgs" class="alert alert-danger" style="display: none;">
    Sorry, but the Make-Up Streaming Service is not accessible. Please make sure you
    are connected to the Singapore Locale Network and check whether the Make-Up Streaming
    Service is up. Kindly approach the assigned worker for assistance. You cannot continue
    your Make-Up session.
</div>
<br />
<div id="panelButtons" style="display: none">
    <div style="margin-bottom: 5px" class="aspnet-checkbox">
        <asp:CheckBox ID="chkConfirm" runat="server" ClientIDMode="Static" Text="I confirm that I have read all the news and updates or I am already aware." />
    </div>
    <div>
        <asp:Button ID="cmdContinue" CssClass="btn btn-primary" ClientIDMode="Static"
            runat="server" Text="Continue to Playback" OnClick="cmdContinue_Click" />&nbsp;<asp:Button
                ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="cmdCancel_Click" />
    </div>
</div>
<asp:Literal ID="scriptMakeUpTest" runat="server"></asp:Literal>
<script type="text/javascript">
    $(document).ready(function () {
        DisableInputs(true);

        // Streaming Service Check
        if (typeof makeUpTest == "undefined" || makeUpTest != "OK") {
            // Failed
            FailedCheck();
        }
        else {
            // Success
            HideLoader();
            DisableInputs(false);

            $("#chkConfirm").change(function () {
                var checked = $(this).is(":checked");
                $("#cmdContinue").attr("disabled", !checked);
            });

            var confirmChecked = $("#chkConfirm").is(":checked");
            $("#cmdContinue").attr("disabled", !confirmChecked);
        }

        $("#panelButtons").css("display", "");

        ExecuteKeepAlive();
    });

    function DisableInputs(disabled) {
        $("#chkConfirm").attr("disabled", disabled);
        $("#cmdContinue").attr("disabled", disabled);
    }

    function FailedCheck() {
        HideLoader();

        $("#panelErrorMgs").css("display", "");
    }

    function HideLoader() {
        $("#panelLoading").css("display", "none");
    }
</script>
