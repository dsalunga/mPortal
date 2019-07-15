<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyPreferences.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.MyPreferences" %>
<div runat="server" clientidmode="static" id="panelNotice" style="background-color: Yellow; padding: 3px">
    <strong>INSTRUCTION</strong>:&nbsp;Please tick the group that you want to make PRIVATE.
</div>
<div style="float: left; clear: both">
    <!-- # start of Personal Information # -->
    <div runat="server" id="panelLocaleGroups" visible="false" class="input-list" clientidmode="static">
        <h4 class="heading colr">Locale Groups</h4>
        <asp:CheckBoxList RepeatDirection="Vertical" CssClass="aspnet-checkbox" DataTextField="Name" DataValueField="Id"
            ID="cblLocaleGroups" runat="server">
        </asp:CheckBoxList>
    </div>
    <div id="panelCommittees" runat="server" visible="false" class="input-list" clientidmode="static">
        <br />
        <h4 class="heading colr">Committees &amp; Ministries</h4>
        <asp:CheckBoxList RepeatDirection="Vertical" CssClass="aspnet-checkbox" RepeatColumns="1" DataTextField="Name"
            DataValueField="Id" ID="cblMinistries" runat="server">
        </asp:CheckBoxList>
    </div>
    <div id="panelSpecialGroups" runat="server" visible="false" class="input-list" clientidmode="static">
        <br />
        <h4 class="heading colr">Special Groups</h4>
        <asp:CheckBoxList RepeatDirection="Vertical" CssClass="aspnet-checkbox" RepeatColumns="1" DataTextField="Name"
            DataValueField="Id" ID="cblSpecialGroups" runat="server">
        </asp:CheckBoxList>
    </div>
    <br />
    <div>
        <asp:Button CssClass="btn btn-primary Command" Width="85px" ID="cmdSubmit" runat="server" Text="Update"
            OnClick="cmdSubmit_Click" />
        &nbsp;
        <asp:Button CssClass="btn Command" Width="85px" ID="cmdCancel" runat="server" Text="Cancel"
            OnClick="cmdCancel_Click" Visible="False" />
    </div>
    <br />
    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <br />
</div>
