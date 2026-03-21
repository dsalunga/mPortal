<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogAttendance.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.LessonReviewer.LogAttendance" %>
<input type="hidden" id="hShowExitAlert" value="0" />
<div runat="server" clientidmode="Static" id="panelAttendanceAuth" class="integration-lessonreviewer">
    <div runat="server" clientidmode="Static" id="panelMsg" visible="false">
        <div class="alert alert-danger">
            <asp:Literal ID="lblMsg" runat="server" EnableViewState="False"></asp:Literal>
        </div>
    </div>
    <div runat="server" clientidmode="Static" id="panelNote">
        Kindly provide the reason for your being absent during the Gathering / Service.
    </div>
    <br />
    <div runat="server" clientidmode="Static" id="panelAttendanceInfo">
        <h3>
            Attendance Information</h3>
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
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Playback Date
                </td>
                <td>
                    <span style="font-weight: bold" runat="server" id="lblPlaybackDate"></span>
                </td>
            </tr>
            <tr>
                <td>
                    Playback Duration
                </td>
                <td>
                    <span runat="server" id="lblPlaybackDuration" style="font-weight: bold"></span>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div runat="server" clientidmode="Static" id="panelSubmitAttendance">
        <h3>
            Submit Attendance</h3>
        <div>
            <div>
                Remarks or reason for being absent</div>
            <div>
                <asp:TextBox ID="txtAbsentReason" ClientIDMode="Static" runat="server" Columns="65"
                    MaxLength="4000" Rows="6" TextMode="MultiLine"></asp:TextBox>
            </div>
            <%--<div style="margin-top: 5px">
                <asp:CheckBox ID="chkNote" CssClass="aspnet-checkbox" runat="server" Text="Include notes (use this if you continued a previous session or you'll not be able to complete this one)"
                    ClientIDMode="Static" />
                <div style="margin-left: 20px" id="panelReasonNotes">
                    <div>
                        <asp:RadioButton ClientIDMode="Static" ID="radioNote" GroupName="Attendance-Note"
                            Text="Select note:" runat="server" Checked="True" />
                        <asp:DropDownList ID="cboNotes" ClientIDMode="Static" runat="server">
                            <asp:ListItem Selected="True">I have attended the 1st half, this is for the 2nd half</asp:ListItem>
                            <asp:ListItem>I have attended the 2nd half, this is for the 1st half</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:RadioButton ID="radioCustomNote" GroupName="Attendance-Note" ClientIDMode="Static"
                            Text="Custom note:" runat="server" />
                        <div style="margin-left: 20px">
                            <asp:TextBox ClientIDMode="Static" ID="txtCustomNote" runat="server" Columns="40"
                                MaxLength="4000" Rows="5" TextMode="MultiLine"></asp:TextBox></div>
                    </div>
                </div>
            </div>--%>
            <br />
            <div style="margin-top: 5px">
                <asp:Button ID="cmdLogin" OnClientClick="return SubmitAttendance();" ClientIDMode="Static"
                    CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="cmdLogin_Click" />&nbsp;<asp:Button
                        ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
                        OnClientClick="return CancelSession();" />
            </div>
            <br />
        </div>
    </div>
</div>
<div runat="server" id="panelSuccess" clientidmode="Static" visible="false" class="integration-lessonreviewer">
    <h4>
        Thank you! Your attendance was sent for worker's approval!</h4>
    <div>
        You have successfully completed your Make-Up session.</div>
    <br />
    <div>
        <a href="" runat="server" id="linkStartOver" class="btn btn-primary">Make-Up Home</a>
    </div>
    <div runat="server" id="panelLeaveComment" visible="false">
        <hr style="border-width: 0px; color: #f00; background-color: #ddd; height: 2px;" />
        <br />
        <h3 style="color: #3E973C">
            Please tell us about your experience...</h3>
        <span>Comments, suggestions and criticisms are welcome :)
            You may also tell us about your overall experience in using the Portal. Your feedback
            is very much appreciated and would help us in improving the Portal -- by providing
            the relevant services and making it easy to use. </span>
        <br />
        <br />
        <div>
            <a href="#" target="_blank" runat="server" id="linkLeaveComment" class="btn btn-default btn-sm">LEAVE A COMMENT</a>
        </div>
        <br />
        <span>Our sincere apology in case you have experienced any
            difficulties in using the Make-Up Service and the Portal. Thanks be to God!</span>
    </div>
</div>
<div runat="server" id="panelNotRequired" clientidmode="Static" visible="false" class="integration-lessonreviewer">
    <h3>
        Attendance Not Required</h3>
    <div>
        There is already an attendance entry for this Make-Up session or an attendance request
        has been submitted for worker's approval.</div>
    <br />
    <br />
    <div>
        <a href="" runat="server" id="linkMakeUpHome" class="btn btn-default">Make-Up Home</a>
    </div>
</div>
<div runat="server" id="panelError" clientidmode="Static" visible="false" class="integration-lessonreviewer">
    <h3>
        Invalid session data. Please contact the ADDCIT team.</h3>
</div>
<script type="text/javascript">
    /*
    function InitInputs() {
        var chkNote = $("#chkNote");
        if (chkNote.length > 0) {
            $("#hShowExitAlert").val("1");

            // chkNote
            chkNote.change(function () {
                var checked = $(this).is(":checked");

                EnableAllInputs(!checked);

                if (checked) {
                    EnableRadioInputs();
                }
            });

            // radioNote
            $("#radioNote").change(function () {
                EnableRadioInputs();
            });

            // radioCustomNote
            $("#radioCustomNote").change(function () {
                EnableRadioInputs();
            });

            EnableAllInputs(true);
        }
    }

    function EnableAllInputs(disabled) {
        $("#panelReasonNotes").css("display", disabled ? "none" : "");

        $("#radioNote").attr("disabled", disabled);
        $("#cboNotes").attr("disabled", disabled);
        $("#radioCustomNote").attr("disabled", disabled);
        $("#txtCustomNote").attr("disabled", disabled);
    }
    */

    function SubmitAttendance() {
        if ($("#txtAbsentReason").val() == "") {
            alert("Please enter your reason for being absent.");
            return false;
        }

        /*
        if ($("#chkNote").is(":checked") && $("#radioCustomNote").is(":checked") && $("#txtCustomNote").val() == "") {
            alert("Please enter a custom note, otherwise uncheck the Include notes option or select a note from the preset.");
            return false;
        }
        */

        $("#hShowExitAlert").val("0");
        return true;
    }

    function CancelSession() {
        var userAnswer = confirm("WARNING: You will be not able to submit your attendance. Are you sure you want to cancel your Make-Up session?");
        if (userAnswer) {
            $("#hShowExitAlert").val("0");
        }
        return userAnswer;
    }

    /*
    function EnableRadioInputs() {
        var radioNoteChecked = $("#radioNote").is(":checked");
        var radioCustomNoteChecked = $("#radioCustomNote").is(":checked");

        if (radioNoteChecked && !radioCustomNoteChecked) {
            $("#txtCustomNote").attr("disabled", true);
            $("#cboNotes").attr("disabled", false);
        }
        else if (!radioNoteChecked && radioCustomNoteChecked) {
            $("#txtCustomNote").attr("disabled", false);
            $("#cboNotes").attr("disabled", true);
        }
        else {
            $("#txtCustomNote").attr("disabled", true);
            $("#cboNotes").attr("disabled", true);
        }
    }
    */

    $(document).ready(function () {
        //InitInputs();
        ExecuteKeepAlive();
    });

    window.onbeforeunload = function (evt) {
        var showAlert = $("#hShowExitAlert").val() == "1";
        if (showAlert) {
            return "*****************************\n-- WARNING --\nPLEASE SUBMIT YOUR ATTENDANCE BEFORE LEAVING THIS PAGE!!! Please CANCEL this window to continue submitting your attendance.\n*****************************";
        }
    }
</script>
