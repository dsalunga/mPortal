<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Setup.ascx.cs" ClientIDMode="Static" Inherits="WCMS.WebSystem.Apps.Integration.Streaming.Setup" %>
<%@ Register Src="../ServicePicker.ascx" TagName="ServicePicker" TagPrefix="uc1" %>

<div class="live-stream-setup" data-locale-id="-1">
    <asp:HiddenField ID="hServiceScheduleId" runat="server" Value="-1" />
    <div class="row">
        <div class="col-md-4 col-sm-6">
            Stream Type:
            <asp:DropDownList runat="server" ID="cboStreamType" CssClass="form-control">
                <asp:ListItem Value="0">Live Session / Event</asp:ListItem>
                <asp:ListItem Value="1">Playback / Make-Up</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-6">
            <div class="checkbox">
                <label>
                    <input type="checkbox" runat="server" enableviewstate="true" id="chkSchedule" value="Attendance">
                    Require Attendance
                </label>
                <div class="panel-selected-service" style="padding-left: 20px; padding-top: 5px">
                    <button type="button" class="selected-service btn btn-success btn-sm1"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;<span id="lblServiceSelected">Select Schedule</span></button>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="service-picker hidden" style="max-width: 675px; padding-left: 20px">
                <uc1:ServicePicker ID="ServicePicker1" runat="server" />
                <br />
            </div>
        </div>
    </div>
    <div class="row form-inline1">
        <div class="col-md-4 col-sm-6">
            Start Time:
            <div class="input-group">
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-btn">
                    <button title="Select date/time..." onclick="showDatePicker(); //ShowDateTimePicker(WCMS.Dom.Get('<% =txtStartDate.ClientID %>'));" type="button" class="btn btn-default">
                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                    </button>
                    <%--<button title="Clear" id="btnClearStartDate" onclick="$('#txtStartDate').val('');toggleStartTimeBtn();" type="button" class="btn btn-default hide">
                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                    </button>--%>
                </span>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-6">
            Duration:
        <asp:DropDownList CssClass="form-control" runat="server" ID="cboDuration">
            <asp:ListItem Value="00:00"></asp:ListItem>
            <asp:ListItem>00:30</asp:ListItem>
            <asp:ListItem>01:00</asp:ListItem>
            <asp:ListItem>01:30</asp:ListItem>
            <asp:ListItem>02:00</asp:ListItem>
            <asp:ListItem>02:30</asp:ListItem>
            <asp:ListItem>03:00</asp:ListItem>
            <asp:ListItem>03:30</asp:ListItem>
            <asp:ListItem>04:00</asp:ListItem>
            <asp:ListItem>04:30</asp:ListItem>
            <asp:ListItem>05:00</asp:ListItem>
            <asp:ListItem>05:30</asp:ListItem>
            <asp:ListItem>06:00</asp:ListItem>
            <asp:ListItem>06:30</asp:ListItem>
            <asp:ListItem>07:00</asp:ListItem>
            <asp:ListItem>07:30</asp:ListItem>
            <asp:ListItem>08:00</asp:ListItem>
            <asp:ListItem>08:30</asp:ListItem>
            <asp:ListItem>09:00</asp:ListItem>
            <asp:ListItem>09:30</asp:ListItem>
            <asp:ListItem>10:00</asp:ListItem>
            <asp:ListItem>10:30</asp:ListItem>
            <asp:ListItem>11:00</asp:ListItem>
            <asp:ListItem>11:30</asp:ListItem>
            <asp:ListItem>12:00</asp:ListItem>
            <asp:ListItem>12:30</asp:ListItem>
            <asp:ListItem>13:00</asp:ListItem>
            <asp:ListItem>13:30</asp:ListItem>
            <asp:ListItem>14:00</asp:ListItem>
            <asp:ListItem>14:30</asp:ListItem>
            <asp:ListItem>15:00</asp:ListItem>
            <asp:ListItem>15:30</asp:ListItem>
            <asp:ListItem>16:00</asp:ListItem>
            <asp:ListItem>16:30</asp:ListItem>
        </asp:DropDownList>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="cmdUpdate_Click" />
        </div>
    </div>
</div>
<div id="datePicker" class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <%--<iframe width="280" height="385" style="bor" src="/Content/Windows/DateTimePicker.aspx?Value=<% =txtStartDate.ClientID %>"></iframe>--%>
            <div class="embed-responsive embed-responsive-4by3" style="height: 315px; margin-top: 15px">
                <iframe id="iframeDatePicker" class="embed-responsive-item" data-src="/Content/Windows/DateTimePicker.aspx?Mode=inline&Value=" src=""></iframe>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function toggleStartTimeBtn() {
        if ($('#txtStartDate').val() === '') {
            $('#btnClearStartDate').addClass('hide');
        } else {
            $('#btnClearStartDate').removeClass('hide');
        }
    }

    function selectServiceSchedule(jItem) {
        var id = jItem.data('schedule-id');
        var serviceName = jItem.data('service');
        var serviceTime = jItem.data('time');
        var serviceDate = jItem.data('date');
        $('#hServiceScheduleId').val(id);

        var existing = $('.service-picker-item-selected');
        if (existing.length > 0) {
            existing.removeClass('service-picker-item-selected');
        }

        jItem.addClass('service-picker-item-selected');
        $('#lblServiceSelected').html(serviceName + ' ' + serviceDate + ' ' + serviceTime);

        // Hide the picker
        $('.service-picker').addClass('hidden');
    };

    function toggleServicePicker() {
        if ($('#chkSchedule').is(':checked')) {
            if ($('#hServiceScheduleId').val() == -1) {
                $('.service-picker').removeClass('hidden');
            }
            $('.panel-selected-service').removeClass('hidden');
        } else {
            $('.service-picker').addClass('hidden');
            $('.panel-selected-service').addClass('hidden');
        }
    }

    $(document).ready(function () {
        var localeId = parseInt($('.live-stream-setup').data('locale-id'));
        var serviceScheduleId = parseInt($('#hServiceScheduleId').val());
        if (serviceScheduleId != -1) {
            $('#chkSchedule').attr('checked', true);
            var selectedService = $("[data-schedule-id='" + serviceScheduleId + "']");
            if (selectedService.length > 0) {
                selectServiceSchedule(selectedService);
            }
        }
        toggleServicePicker();

        $('#chkSchedule').change(function () {
            toggleServicePicker();
        });

        $('.selected-service').click(function () {
            $('.service-picker').toggleClass('hidden');
        });

        $('.service-picker-item').click(function () {
            selectServiceSchedule($(this));
        });

        //$('#txtStartDate').change(toggleStartTimeBtn);
        //toggleStartTimeBtn();
    });

    function showDatePicker() {
        window.returnValue = {
            callback: function (value) {
                if (value) {
                    $('#txtStartDate').val(value);
                }
                $('#datePicker').modal('hide');
            }
        }
        var src = $('#iframeDatePicker').data('src') + $('#txtStartDate').val();
        $('#iframeDatePicker').attr('src', src);
        $('#datePicker').modal('show');
    }
</script>
