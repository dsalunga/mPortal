<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigMemberLink.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.ConfigMemberLink" %>
<br />
<div class="control-box">
    <div>
        <a
            runat="server" id="linkEdit" class="btn btn-default" href="#">Edit</a>&nbsp;<a
                runat="server" id="linkAccount" class="btn btn-default" href="#" title="Open the CMS Account Settings of this Member">Account</a>
        &nbsp;<asp:LinkButton ID="cmdSync"
            runat="server" OnClick="cmdSync_Click" Enabled="false" CssClass="btn btn-default">Sync</asp:LinkButton>
        <div class="pull-right">
            <a runat="server" id="linkRefresh"
                href="#" class="btn btn-default">Refresh</a>&nbsp;
            <a runat="server" id="linkClose" class="btn btn-default" href="#">Close</a>
        </div>
    </div>
</div>
<br />
<div>
    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
</div>
<h2>Member Profile</h2>
<div>
    Last updated on <strong runat="server" id="lblLastUpdate">&lt;UNKNOWN&gt;</strong>
</div>
<br />
<div>
    <div class="FieldLine">
        <span class="FieldLabel">Name:</span> <strong runat="server" id="lblFullName"></strong>
    </div>
    <div class="FieldLine">
        <span class="FieldLabel">Email Address:</span> <strong runat="server" id="lblEmailAddress"></strong>
    </div>
    <div class="FieldLine">
        <span class="FieldLabel">User Name:</span> <strong runat="server" id="lblUserName"></strong>
    </div>
    <br />
    <div class="FieldLine">
        <span class="FieldLabel">Group ID #:</span> <strong runat="server" id="lblExternalIdNo"></strong><span runat="server" id="panelLinkExternal" visible="false">
            <asp:TextBox ID="txtExternalID" Columns="12" runat="server"></asp:TextBox>
            <asp:Button ID="cmdLinkExternal" runat="server" Text="Update External Link" CssClass="btn btn-default" OnClick="cmdLinkExternal_Click" />
            <br />
            <br />
        </span>
    </div>
    <div class="FieldLine">
        <span class="FieldLabel">Date of Membership:</span> <strong runat="server" id="lblDateOfMembership"></strong>
    </div>
    <div class="FieldLine">
        <span class="FieldLabel">External Account Status:</span> <strong runat="server" id="lblApproved"></strong>
        <br />
        <div id="panelIntegrationApproval" runat="server" visible="false">
            <asp:Button ID="cmdApprove" runat="server" Text="Approve" CssClass="btn btn-default" OnClick="cmdApprove_Click" />
            <asp:CheckBox ID="chkPortalSendEmail" Text="Send Email" runat="server" CssClass="aspnet-checkbox" Enabled="false" />
            <br />
        </div>
    </div>
    <div class="FieldLine">
        <span class="FieldLabel">Portal Account Status:</span> <strong runat="server" id="lblCMSAccount"></strong>
        <br />
        <div id="panelCMSApproval" runat="server" visible="false">
            <asp:Button ID="cmdAccountApprove" runat="server" Text="Activate" OnClick="cmdAccountApprove_Click" CssClass="btn btn-default" />
            <asp:CheckBox ID="chkAccountSendEmail" Text="Send Email" CssClass="aspnet-checkbox" runat="server" Enabled="false" />
            <br />
        </div>
    </div>
</div>
<br />
<img src="/Content/Assets/Images/nophoto.png" width="300" runat="server" id="memberPhoto" style="border: solid 2px #aaa; margin: 2px" />
<br />
<div style="float: left; clear: both">
    <!-- # start of Personal Information # -->
    <br />
    <h3>PERSONAL Information</h3>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label1" AssociatedControlID="txtHomeAddress1"
            runat="server">Address Line 1:</asp:Label>
        <span id="txtHomeAddress1" runat="server" style="font-weight: bold"></span>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label13" AssociatedControlID="txtHomeAddress2"
            runat="server">Address Line 2:</asp:Label>
        <span id="txtHomeAddress2" runat="server" style="font-weight: bold"></span>
    </div>
    <div class="FieldLine" id="divHomeState" runat="server" visible="false">
        <asp:Label CssClass="FieldLabel" ID="Label2" AssociatedControlID="txtHomeAddressState"
            runat="server">State (US Only):</asp:Label>
        <span id="txtHomeAddressState" runat="server" style="font-weight: bold"></span>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label4" AssociatedControlID="txtHomeAddressZipCode"
            runat="server">Zip Code:</asp:Label>
        <span id="txtHomeAddressZipCode" runat="server" style="font-weight: bold"></span>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label3" AssociatedControlID="txtHomeAddressCountry"
            runat="server">Country:</asp:Label>
        <span id="txtHomeAddressCountry" runat="server" style="font-weight: bold"></span>
    </div>
    <br />
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label5" AssociatedControlID="txtMobile" runat="server">Mobile #:</asp:Label>
        <span id="txtMobile" runat="server" style="font-weight: bold"></span>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label6" AssociatedControlID="txtHomePhone" runat="server">Home Phone #:</asp:Label>
        <span id="txtHomePhone" runat="server" style="font-weight: bold"></span>
    </div>
    <br />
    <!-- # Start of Work Information # -->
    <h3>WORK Information</h3>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label7" AssociatedControlID="txtWorkAddress1"
            runat="server">Address Line 1:</asp:Label>
        <span id="txtWorkAddress1" runat="server" style="font-weight: bold"></span>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label14" AssociatedControlID="txtWorkAddress2"
            runat="server">Address Line 2:</asp:Label>
        <span id="txtWorkAddress2" runat="server" style="font-weight: bold"></span>
    </div>
    <div class="FieldLine" id="divWorkState" runat="server" visible="false">
        <asp:Label CssClass="FieldLabel" ID="Label8" AssociatedControlID="txtWorkAddressState"
            runat="server">State (US Only):</asp:Label>
        <span id="txtWorkAddressState" runat="server" style="font-weight: bold"></span>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label10" AssociatedControlID="txtWorkAddressZipCode"
            runat="server">Zip Code:</asp:Label>
        <span id="txtWorkAddressZipCode" runat="server" style="font-weight: bold"></span>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label9" AssociatedControlID="txtWorkAddressCountry"
            runat="server">Country:</asp:Label>
        <span id="txtWorkAddressCountry" runat="server" style="font-weight: bold"></span>
    </div>
    <br />
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label11" AssociatedControlID="txtWorkDesignation"
            runat="server">Work Designation:</asp:Label>
        <span id="txtWorkDesignation" runat="server" style="font-weight: bold"></span>
    </div>
    <div class="FieldLine">
        <asp:Label CssClass="FieldLabel" ID="Label12" AssociatedControlID="txtWorkPhone"
            runat="server">Work Phone #:</asp:Label>
        <span id="txtWorkPhone" runat="server" style="font-weight: bold"></span>
    </div>
    <br />
    <h3>Locale Group</h3>
    <asp:BulletedList DataTextField="Name" DataValueField="Id" ID="rblLocaleGroups" runat="server">
    </asp:BulletedList>
    <br />
    <h3>Committees &amp; Ministries</h3>
    <asp:BulletedList DataTextField="Name" DataValueField="Id" ID="cblMinistries" runat="server">
    </asp:BulletedList>
    <br />
    <h3>Special Groups</h3>
    <asp:BulletedList DataTextField="Name" DataValueField="Id" ID="cblSpecialGroups"
        runat="server">
    </asp:BulletedList>
    <br />
    <div>
        <asp:Button CssClass="btn btn-primary" Width="85px" ID="cmdCancel" runat="server" Text="OK"
            OnClick="cmdCancel_Click" Visible="False" />
    </div>
</div>
