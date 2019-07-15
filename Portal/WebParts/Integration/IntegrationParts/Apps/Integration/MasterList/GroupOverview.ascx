<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupOverview.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.GroupOverview" %>
<%@ Register Src="Controls/WebGroupTab.ascx" TagName="WebGroupTab" TagPrefix="uc1" %>

<uc1:WebGroupTab ID="WebGroupTab1" runat="server" SelectedTab="GroupOverview" />

<div class="form-horizontal">
    <%--<div class="control-group">
        <div class="controls">
            <h3>Group Details</h3>
        </div>
    </div>--%>
    <div class="control-group">
        <label class="control-label" for="txtName">NAME OF GROUP</label>
        <div class="controls">
            <span runat="server" id="lblName"></span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtParent">ZONE/DISTRICT/DIVISION</label>
        <div class="controls">
            <span runat="server" id="lblParent"></span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtMentors">MENTORS</label>
        <div class="controls">
            <span runat="server" id="lblMentors"></span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtConductors">CONDUCTORS</label>
        <div class="controls">
            <span runat="server" id="lblConductors"></span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtAssignedCouncillor">ASSIGNED COUNCILLOR</label>
        <div class="controls">
            <span runat="server" id="lblOwner"></span>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtDescription">BRIEF HISTORY</label>
        <div class="controls">
            <span runat="server" id="lblDescription"></span>
        </div>
    </div>
    <%--<div class="control-group">
        <div class="controls">
            <h3>Misc. Details</h3>
        </div>
    </div>--%>
    <br />
    <div class="control-group">
        <label class="control-label" for="txtManagers">GROUP MANAGERS</label>
        <div class="controls">
            <span runat="server" id="lblManagers"></span>
        </div>
    </div>
    <%--<div class="control-group">
        <label class="control-label" for="txtOfficers">OFFICERS</label>
        <div class="controls">
        </div>
    </div>
    <div class="control-group">
        <label class="control-label" for="txtActivities">ACTIVITIES</label>
        <div class="controls">
        </div>
    </div>--%>
    <div class="control-group">
        <label class="control-label">UPDATED ON</label>
        <div class="controls">
            <span runat="server" id="lblDateModified"></span>
        </div>
    </div>
    <div class="control-group" id="panelUpdate" runat="server" visible="false">
        <div class="controls">
            <a href="#" id="linkUpdate" runat="server" class="btn btn-primary">Edit</a>
            <%--<button id="cmdUpdate" runat="server" class="btn btn-primary">Edit</button>--%>
        </div>
    </div>
</div>
