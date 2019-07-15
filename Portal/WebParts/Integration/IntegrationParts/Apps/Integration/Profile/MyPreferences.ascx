<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyPreferences.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.MyPreferences" %>
<div runat="server" id="panelNotice" style="background-color: Yellow; padding: 3px">
    <h4>
        <strong>INSTRUCTION</strong>:&nbsp;Please tick the group that you want to make PRIVATE.</h4>
</div>
<div style="float: left; clear: both">
    <!-- # start of Personal Information # -->
    <div runat="server" id="panelLocaleGroups" visible="false" class="input-list" clientidmode="static">
        <br />
        <h3>Locale Groups</h3>
        <asp:CheckBoxList RepeatDirection="Vertical" DataTextField="Name" DataValueField="Id"
            ID="cblLocaleGroups" runat="server">
        </asp:CheckBoxList>
    </div>
    <div id="panelCommittees" runat="server" visible="false" class="input-list" clientidmode="static">
        <br />
        <h3>Committees &amp; Ministries</h3>
        <asp:CheckBoxList RepeatDirection="Vertical" RepeatColumns="1" DataTextField="Name"
            DataValueField="Id" ID="cblMinistries" runat="server">
        </asp:CheckBoxList>
    </div>
    <div id="panelSpecialGroups" runat="server" visible="false" class="input-list" clientidmode="static">
        <br />
        <h3>Special Groups</h3>
        <asp:CheckBoxList RepeatDirection="Vertical" RepeatColumns="1" DataTextField="Name"
            DataValueField="Id" ID="cblSpecialGroups" runat="server">
        </asp:CheckBoxList>
    </div>
    <br />
    <div>
        <asp:Button CssClass="btn btn-primary" ID="cmdSubmit" runat="server" Text="Update"
            OnClick="cmdSubmit_Click" />
        &nbsp;
        <asp:Button CssClass="btn btn-default" ID="cmdCancel" runat="server" Text="Cancel"
            OnClick="cmdCancel_Click" Visible="False" />
    </div>
    <br />
    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <br />
</div>
