<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberEdit.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.MemberEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Controls/WebGroupTab.ascx" TagName="WebGroupTab" TagPrefix="uc1" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<uc1:webgrouptab id="WebGroupTab1" runat="server" selectedtab="Members" />
<asp:HiddenField ID="hGroupId" runat="server" Value="-1" />
<div class="form-horizontal">
    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
    <div class="control-group">
        <div class="controls">
            <img src="#" width="300" runat="server" id="memberPhoto" style="border: solid 2px #aaa; margin: 2px 0 2px 0" />
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtFirstName">
            First Name<asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                ErrorMessage="First Name" ForeColor="Red">*</asp:RequiredFieldValidator></label>
        <div class="controls">
            <asp:TextBox Columns="50" ID="txtFirstName" runat="server" placeholder="First Name"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtMiddleName">Middle Name</label>
        <div class="controls">
            <asp:TextBox Columns="50" ID="txtMiddleName" runat="server" placeholder="Middle Name"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtLastName">
            Last Name<asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                ErrorMessage="Last Name" ForeColor="Red">*</asp:RequiredFieldValidator></label>
        <div class="controls">
            <asp:TextBox Columns="50" ID="txtLastName" runat="server" placeholder="Last Name"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="control-group">
        <label class="control-label" for="txtExternalID">
            Group ID<asp:RequiredFieldValidator ID="rfvExternalID" runat="server" ControlToValidate="txtExternalID"
                ErrorMessage="Group ID" ForeColor="Red">*</asp:RequiredFieldValidator></label>
        <div class="controls">
            <asp:TextBox CssClass="col-md-2" ID="txtExternalID" runat="server" placeholder="Group ID"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtMembershipDate">Date of Membership</label>
        <div class="controls">
            <asp:TextBox ID="txtMembershipDate" CssClass="col-md-2" placeholder="YYYY-MM-DD" runat="server"
                ClientIDMode="Static" Columns="30"></asp:TextBox>
            <asp:CalendarExtender ID="txtMembershipDateCalendarExtender" runat="server" Enabled="True"
                TargetControlID="txtMembershipDate" Format="yyyy-MM-dd" DefaultView="Years">
            </asp:CalendarExtender>
        </div>
    </div>
    <br />
    <div class="control-group">
        <label class="control-label" for="txtEmail">
            Email<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Email" ForeColor="Red">*</asp:RequiredFieldValidator></label>
        <div class="controls">
            <asp:TextBox Columns="40" CssClass="col-md-3" ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtMobileNumber">Mobile Number</label>
        <div class="controls">
            <asp:TextBox Columns="40" ID="txtMobileNumber" runat="server" placeholder="Country Code + Mobile Number"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtHomePhone">Home Phone</label>
        <div class="controls">
            <asp:TextBox Columns="40" ID="txtHomePhone" runat="server" placeholder="Country Code + Area Code + Phone Number"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="control-group">
        <label class="control-label" for="txtHomeAddress1">Address Line 1</label>
        <div class="controls">
            <asp:TextBox CssClass="col-md-4" ID="txtHomeAddress1" runat="server" placeholder="Address Line 1"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtHomeAddress2">Address Line 2</label>
        <div class="controls">
            <asp:TextBox CssClass="col-md-4" ID="txtHomeAddress2" runat="server" placeholder="Address Line 2"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtHomeAddressZipCode">Zip Code</label>
        <div class="controls">
            <asp:TextBox CssClass="col-md-2" ID="txtHomeAddressZipCode" runat="server" placeholder="Zip Code"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="cboHomeAddressCountry">Country</label>
        <div class="controls">
            <asp:DropDownList ID="cboHomeAddressCountry" runat="server" DataSourceID="ObjectDataSourceCountries"
                DataTextField="CountryName" DataValueField="CountryCode" AppendDataBoundItems="true">
                <asp:ListItem Text="- Select -" Value="-1"></asp:ListItem>
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSourceCountries" runat="server" SelectMethod="GetCountries"
                TypeName="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.MemberEdit"></asp:ObjectDataSource>
        </div>
    </div>
    <br />
    <div class="control-group">
        <label class="control-label" for="cboOfficerPosition">
            <asp:CheckBox ID="chkIsOfficer" CssClass="aspnet-checkbox" ClientIDMode="Static" Text="Position (Officers)" runat="server" /></label>
        <div class="controls">
            <asp:DropDownList ID="cboOfficerPosition" runat="server" ClientIDMode="Static">
                <asp:ListItem Text="- Select -" Value=""></asp:ListItem>
                <asp:ListItem Text="President (Coordinator)" Value="President"></asp:ListItem>
                <asp:ListItem Text="Vice President" Value="Vice President"></asp:ListItem>
                <asp:ListItem Text="Secretary" Value="Secretary"></asp:ListItem>
                <asp:ListItem Text="Treasurer" Value="Treasurer"></asp:ListItem>
                <asp:ListItem Text="Sector Leader" Value="Sector Leader"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="cboVoiceDesignation">Voice Designation</label>
        <div class="controls">
            <asp:DropDownList ID="cboVoiceDesignation" runat="server">
                <asp:ListItem Text="- Select -" Value=""></asp:ListItem>
                <asp:ListItem Text="Soprano" Value="Soprano"></asp:ListItem>
                <asp:ListItem Text="Mezzo-soprano" Value="Mezzo-soprano"></asp:ListItem>
                <asp:ListItem Text="Contralto" Value="Contralto"></asp:ListItem>
                <asp:ListItem Text="Countertenor" Value="Countertenor"></asp:ListItem>
                <asp:ListItem Text="Tenor" Value="Tenor"></asp:ListItem>
                <asp:ListItem Text="Baritone" Value="Baritone"></asp:ListItem>
                <asp:ListItem Text="Bass" Value="Bass"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="cboVoiceDesignation">Responsibilities</label>
        <div class="controls">
            <asp:CheckBox ID="chkAssignedCouncillor" Text="Assigned Councillor" CssClass="aspnet-checkbox" runat="server" /><br />
            <asp:CheckBox ID="chkGroupMentor" Text="Group Mentor" CssClass="aspnet-checkbox" runat="server" /><br />
            <asp:CheckBox ID="chkGroupConductor" Text="Group Conductor" CssClass="aspnet-checkbox" runat="server" /><br />
            <br />
            <asp:CheckBox ID="chkGroupManager" Text="Group Manager (Coordinator)" CssClass="aspnet-checkbox" runat="server" /><br />
        </div>
    </div>
    <br />
    <div class="control-group">
        <div class="controls">
            <asp:Button CssClass="btn btn-primary" ID="cmdSubmit" runat="server" Text="Update"
                OnClick="cmdSubmit_Click" />
            &nbsp;
            <a href="#" runat="server" id="linkCancel" class="btn btn-default">Cancel</a>
            <%--<asp:Button CssClass="btn btn-default" ID="cmdCancel" runat="server" Text="Cancel"
                    OnClick="cmdCancel_Click" />--%>
        </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:" ShowMessageBox="True" ShowSummary="False" />

    <script type="text/javascript">
        $(document).ready(function () {
            var togglePosition = function () {
                $("#cboOfficerPosition").attr("disabled", !$("#chkIsOfficer").is(":checked"));
            }

            $("#chkIsOfficer").change(togglePosition);

            togglePosition();
        });
    </script>
</div>
