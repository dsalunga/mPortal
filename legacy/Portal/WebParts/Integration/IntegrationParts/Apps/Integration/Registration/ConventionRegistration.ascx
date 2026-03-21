<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConventionRegistration.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.ConventionRegistrationController" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<style type="text/css">
    #panelRegistration div.FieldLine > label
    {
        margin: 2px;
        font-weight: bold;
        font-size: 14px;
    }
    
    #panelRegistration div.FieldLine .field-hint
    {
        margin: 2px;
    }
    
    #panelRegistration input, #panelRegistration textarea, #panelRegistration select
    {
        font-size: 14px;
    }
    
    #panelRegistration .FieldLine
    {
        margin-bottom: 20px;
    }
    
    #panelRegistration .required-indicator, #panelRegistration #lblStatus
    {
        color: Red;
        font-size: 14px;
    }
</style>
<div id="panelRegistration" runat="server" clientidmode="static">
    <asp:HiddenField ID="hRegisterOnce" runat="server" Value="0" />
    <img src="/Assets/Uploads/Image/Sites/integration-portal/MCGI_ConventionRegistration2011v2.jpg"
        width="915" height="167" alt="MCGIsg ConventionRegistration 2011 Banner" style="display: none;" />
    <div style="border: none; margin: 0px; padding: 0px; width: 915px; height: 167px;
        background-color: black;">
    </div>
    <br />
    <br />
    <h2>
        FIRST ASIA & OCEANIA CONVENTION DELEGATES REGISTRATION
    </h2>
    <div id="panelRegistrationForm" runat="server" clientidmode="static">
        <div>
            <strong>Please register by INDIVIDUAL and NOT BY GROUP</strong>
        </div>
        <div class="required-indicator">
            * Required</div>
        <br />
        <br />
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="lblCountry" AssociatedControlID="cboCountry"
                runat="server">COUNTRY <span class="required-indicator">*</span><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator5" runat="server" ErrorMessage="COUNTRY" ControlToValidate="cboCountry"
                    ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <br />
            <asp:DropDownList ID="cboCountry" runat="server">
                <asp:ListItem Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Value="Australia">Australia</asp:ListItem>
                <asp:ListItem Value="Brunei">Brunei</asp:ListItem>
                <asp:ListItem Value="Cambodia">Cambodia</asp:ListItem>
                <asp:ListItem Value="China">China</asp:ListItem>
                <asp:ListItem Value="Guam">Guam</asp:ListItem>
                <asp:ListItem Value="Hongkong">Hongkong</asp:ListItem>
                <asp:ListItem Value="Indonesia">Indonesia</asp:ListItem>
                <asp:ListItem Value="Japan">Japan</asp:ListItem>
                <asp:ListItem Value="Macau">Macau</asp:ListItem>
                <asp:ListItem Value="Malaysia">Malaysia</asp:ListItem>
                <asp:ListItem Value="Middle East">Middle East</asp:ListItem>
                <asp:ListItem Value="New Zealand">New Zealand</asp:ListItem>
                <asp:ListItem Value="Others">Others</asp:ListItem>
                <asp:ListItem Value="Papua New Guinea">Papua New Guinea</asp:ListItem>
                <asp:ListItem Value="Saipan">Saipan</asp:ListItem>
                <asp:ListItem Value="South Korea">South Korea</asp:ListItem>
                <asp:ListItem Value="Taiwan">Taiwan</asp:ListItem>
                <asp:ListItem Value="Thailand">Thailand</asp:ListItem>
                <asp:ListItem Value="Vietnam">Vietnam</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="lblLocale" AssociatedControlID="txtLocale" runat="server">
                LOCAL <span class="required-indicator">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                    runat="server" ErrorMessage="LOCAL" ControlToValidate="txtLocale" ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <br />
            <asp:TextBox Columns="60" ID="txtLocale" runat="server"></asp:TextBox>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="Label1" AssociatedControlID="txtExternalId" runat="server">
            CHURCH ID</asp:Label>
            <br />
            <asp:TextBox Columns="20" ID="txtExternalId" runat="server"></asp:TextBox>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="lblName" AssociatedControlID="txtName" runat="server">
                NAME <span class="required-indicator">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="NAME" ControlToValidate="txtName" ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <br />
            <asp:TextBox Columns="50" ID="txtName" runat="server"></asp:TextBox>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="lblAge" AssociatedControlID="cboAge" runat="server">
                AGE <span class="required-indicator">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    runat="server" ErrorMessage="AGE" ControlToValidate="cboAge" ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <br />
            <asp:DropDownList ID="cboAge" runat="server">
                <asp:ListItem Text="" Value=""></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="Label9" AssociatedControlID="cboGender" runat="server">
                GENDER <span class="required-indicator">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                    runat="server" ErrorMessage="GENDER" ControlToValidate="cboGender" ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <br />
            <asp:DropDownList ID="cboGender" runat="server">
                <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="Label2" AssociatedControlID="txtDesignation"
                runat="server">DESIGNATION <span class="required-indicator">*</span><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator7" runat="server" ErrorMessage="DESIGNATION" ControlToValidate="txtDesignation"
                    ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <div class="field-hint">
                e.g. member, officer, worker, etc</div>
            <asp:TextBox Columns="60" ID="txtDesignation" runat="server"></asp:TextBox>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="Label3" AssociatedControlID="txtArrivalDate"
                runat="server">DATE AND TIME OF ARRIVAL (Singapore) <span class="required-indicator">*</span><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator8" runat="server" ErrorMessage="DATE AND TIME OF ARRIVAL" ControlToValidate="txtArrivalDate"
                    ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <div class="field-hint">
                date and time</div>
            <asp:TextBox Columns="25" ID="txtArrivalDate" runat="server"></asp:TextBox>&nbsp;<img
                onclick="ShowDateTimePicker(WCMS.Dom.Get('<%= txtArrivalDate.ClientID %>'));"
                alt="Select date/time..." longdesc="Select date/time..." src="/Content/Assets/Images/calendar.gif"
                style="width: 17px; height: 17px; cursor: pointer" />
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="Label4" AssociatedControlID="txtAirline" runat="server">
                AIRLINE <span class="required-indicator">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                    runat="server" ErrorMessage="AIRLINE" ControlToValidate="txtAirline" ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <div class="field-hint">
                e.g. Qantas Airways, Tiger Airways, Jetstar, etc.</div>
            <asp:TextBox Columns="50" ID="txtAirline" runat="server"></asp:TextBox>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="Label5" AssociatedControlID="txtDepartureDate"
                runat="server">FLIGHT NO. <span class="required-indicator">*</span><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="FLIGHT NO." ControlToValidate="txtFlightNo"
                    ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <br />
            <asp:TextBox Columns="30" ID="txtFlightNo" runat="server"></asp:TextBox>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="Label6" AssociatedControlID="txtDepartureDate"
                runat="server">DATE OF DEPARTURE (Singapore) <span class="required-indicator">*</span><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator10" runat="server" ErrorMessage="DATE OF DEPARTURE"
                    ControlToValidate="txtDepartureDate" ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <div class="field-hint">
                mm/dd/yyyy</div>
            <asp:TextBox Columns="25" ID="txtDepartureDate" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="txtDepartureDate_CalendarExtender" runat="server" Enabled="True"
                TargetControlID="txtDepartureDate">
            </asp:CalendarExtender>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="Label7" AssociatedControlID="rblPlaceToStay"
                runat="server">DO YOU HAVE ANY PLACE TO STAY? <span class="required-indicator">*</span><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" ErrorMessage="DO YOU HAVE ANY PLACE TO STAY?" ControlToValidate="rblPlaceToStay"
                    ForeColor="Red">*</asp:RequiredFieldValidator></asp:Label>
            <br />
            <asp:RadioButtonList ID="rblPlaceToStay" runat="server" ClientIDMode="Static">
                <asp:ListItem>YES</asp:ListItem>
                <asp:ListItem>NO</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="lblAddress" AssociatedControlID="txtAddress"
                runat="server">IF YES, PLEASE PROVIDE YOUR ADDRESS</asp:Label>
            <br />
            <asp:TextBox Columns="60" ID="txtAddress" ClientIDMode="Static" runat="server" Rows="4"
                TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="FieldLine">
            <asp:Label CssClass="FieldLabel" ID="Label8" AssociatedControlID="txtPlaceType" runat="server">
                TYPE OF PLACE TO STAY</asp:Label>
            <div class="field-hint">
                e.g. hotel, service apartment, friends</div>
            <asp:TextBox Columns="50" ClientIDMode="Static" ID="txtPlaceType" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button CssClass="Command" Width="85px" ID="cmdSubmit" runat="server" Text="REGISTER"
                OnClick="cmdSubmit_Click" />
        </div>
        <br />
        <asp:Label ID="lblStatus" ClientIDMode="Static" runat="server"></asp:Label>
        <br />
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
            ShowMessageBox="True" ShowSummary="False" />
        <script type="text/javascript">
            $(document).ready(function () {
                var rblPlaceToStayYes = $("#rblPlaceToStay_0");
                var rblPlaceToStayNo = $("#rblPlaceToStay_1");

                function disableAddress(enabled) {
                    $("#txtAddress").attr("disabled", !enabled);
                    $("#txtPlaceType").attr("disabled", !enabled);
                };

                var radioChanged = function () {
                    disableAddress(rblPlaceToStayYes.attr("checked"));
                }

                rblPlaceToStayYes.change(radioChanged);
                rblPlaceToStayNo.change(radioChanged);

                radioChanged();
            });
        </script>
    </div>
    <div id="panelDone" runat="server" visible="false">
        Thank you for registering. Goodbye!
        <br />
        <br />
        <a href="" runat="server" id="linkReturn">Return to REGISTRATION.</a>
    </div>
</div>
