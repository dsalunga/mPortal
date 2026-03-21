<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberVisitEntry.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.MemberVisitEntry" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<div class="wp-ODKVisit wp-ODKVisit-Entry no-bottom-margin">
    <asp:HiddenField runat="server" ID="hBaseGroup" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hTagFilter" runat="server" ClientIDMode="Static" Value="" />
    <div class="row">
        <div class="col-md-6 col-sm-6">
            <label for="txtMember">
                Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name"
                    ControlToValidate="txtMember">*</asp:RequiredFieldValidator></label>
            <div id="panelMemberInput" runat="server" class="form-inline">
                <asp:TextBox ID="txtMember" CssClass="form-control" Columns="50" ClientIDMode="Static" MaxLength="246" runat="server"
                    Width="250px"></asp:TextBox>
                <a href='#' runat="server" id="linkProfile" visible="false" title="View details"
                    target="_blank" style="margin-right: 15px"><strong><span id="lblName" runat="server"></span></strong></a>&nbsp;
                <div class="btn-group" role="group" aria-label="Member Selector">
                    <button id="cmdBrowse" onclick="BrowseClick();" type="button"
                        value="Select..." class="btn btn-default btn-xs" title="Search"><span class="glyphicon glyphicon-search" aria-hidden="true"></span>&nbsp;Select...</button>&nbsp;
                <button id="cmdReset" class="btn btn-default btn-xs" runat="server" title="Clear selection" causesvalidation="false" onserverclick="cmdReset_Click">
                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                </button>
                    <asp:Button ID="cmdVerify" CausesValidation="false" ClientIDMode="Static" OnClick="cmdVerify_Click"
                        runat="server" Style="display: none" />
                </div>
                <asp:HiddenField runat="server" ID="hUserName" ClientIDMode="Static" />
            </div>
        </div>
        <div class="col-md-6 col-sm-6">
            <label for="cboGroup">
                Group:</label>
            <asp:DropDownList ID="cboGroup" CssClass="form-control control-auto" DataTextField="Name" DataValueField="Id" runat="server"
                AppendDataBoundItems="true">
                <asp:ListItem Value="-1" Text=""></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label for="txtAddress">
                    Address:</label>
                <asp:TextBox TextMode="MultiLine" Rows="3" ID="txtAddress" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-6 col-sm-6 col-xs-6">
            <label for="txtContactNo">
                Contact No:</label>
            <asp:TextBox ID="txtContactNo" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-6">
            <label for="txtMembershipDate">
                Date of Membership:<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ErrorMessage="Date of Membership" ControlToValidate="txtMembershipDate">*</asp:RequiredFieldValidator></label>
            <asp:TextBox ID="txtMembershipDate" CssClass="form-control" placeholder="YYYY-MM-DD" ClientIDMode="Static" MaxLength="246" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="txtMembershipDate_CalendarExtender" runat="server" Enabled="True"
                Format="yyyy-MMM-dd" TargetControlID="txtMembershipDate">
            </asp:CalendarExtender>
        </div>
    </div>
    <br />
    <br />
    <br />
    <br />
    <div class="row">
        <div class="col-md-6 col-sm-6 col-xs-6">
            <label for="txtDateVisited">
                Date Visited:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="Date Visited" ControlToValidate="txtDateVisited">*</asp:RequiredFieldValidator></label>
            <asp:TextBox ID="txtDateVisited" CssClass="form-control" ClientIDMode="Static" MaxLength="246" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="txtDateVisited_CalendarExtender" runat="server" Enabled="True"
                Format="yyyy-MMM-dd" TargetControlID="txtDateVisited">
            </asp:CalendarExtender>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-6">
            <label for="cboTimesVisited">
                No. of Times Visited:</label>
            <asp:DropDownList ID="cboTimesVisited" CssClass="form-control" ClientIDMode="Static" runat="server" Width="50px">
                <asp:ListItem>0</asp:ListItem>
                <asp:ListItem Selected="True">1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>9</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <label for="txtStatus">
                Case Status:</label>
            <asp:TextBox ID="txtStatus" CssClass="form-control" ClientIDMode="Static" runat="server" Columns="80" Rows="4"
                TextMode="MultiLine"></asp:TextBox>

        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <label for="txtActualReport">
                Councillor's Observation:</label>
            <asp:TextBox ID="txtActualReport" CssClass="form-control" ClientIDMode="Static" runat="server" Columns="80"
                Rows="16" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <label for="txtActionTaken">
                Action Taken:</label>
            <asp:TextBox ID="txtActionTaken" CssClass="form-control" ClientIDMode="Static" runat="server" Columns="80"
                Rows="6" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-6 col-sm-6 col-xs-6">
            <span id="panelTags" runat="server">
                    <label for="txtTags">
                        Record Classification:</label>
                    <asp:DropDownList ID="cboTag" CssClass="form-control" runat="server">
                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                        <asp:ListItem>Non-sensitive</asp:ListItem>
                        <asp:ListItem>Sensitive</asp:ListItem>
                    </asp:DropDownList>
                <br />
            </span>
        </div>
    </div>
    <div>
        <label>
            Date Entered:</label>
        <strong><span runat="server" id="lblDateEntered"></span></strong>
    </div>
    <div>
        <label>
            Entered By:</label>
        <strong><span runat="server" id="lblEnteredBy"></span></strong>
    </div>
    <br />
    <div id="buttonBarRow">
        <div id="buttonBar" class="buttonBar control-box">
            <div>
                <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="cmdUpdate_Click" />&nbsp;
            <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="cmdCancel_Click"
                CausesValidation="False" />
            </div>
        </div>
    </div>
    <br />
    <asp:Label CssClass="Header" Style="color: Red" ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following fields are required:"
        ShowMessageBox="True" ShowSummary="False" />
    <script type="text/javascript">
        function BrowseClick() {
            var baseGroup = $("#hBaseGroup").val();
            ShowAccountBrowser('hUserName', 21, 1, 0, 0, 'cmdVerify', baseGroup);
        }
    </script>
</div>
