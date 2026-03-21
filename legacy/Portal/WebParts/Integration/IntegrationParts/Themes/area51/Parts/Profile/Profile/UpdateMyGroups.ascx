<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateMyGroups.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.UpdateMyGroups" %>
<div runat="server" visible="false" id="panelLastUpdate" clientidmode="static">
    You have updated your profile last <strong runat="server" id="lblLastUpdate">&lt;UNKNOWN&gt;</strong>
</div>
<div runat="server" visible="false" id="panelNotice" style="background-color: Yellow; padding: 5px"
    clientidmode="static">
    <h4>
        <strong>NOTE</strong>:&nbsp;Please select your Groups before proceeding with any
        activities in the Portal. Thank you!</h4>
</div>
<span id="panelLocaleGroups" runat="server" clientidmode="static" class="input-list">
    <br />
    <h4 class="heading colr">Locale Group</h4>
    <asp:RadioButtonList CssClass="aspnet-radio" DataTextField="Name" DataValueField="Id" ID="rblLocaleGroups"
        runat="server">
    </asp:RadioButtonList>
</span><span id="panelMinistries" runat="server" clientidmode="static" class="input-list">
    <br />
    <h4 class="heading colr">Committees &amp; Ministries</h4>
    <asp:CheckBoxList CssClass="aspnet-checkbox" RepeatDirection="Vertical" RepeatLayout="Flow" RepeatColumns="1"
        DataTextField="Name" DataValueField="Id" ID="cblMinistries" runat="server">
    </asp:CheckBoxList>
</span><span id="panelSpecialGroups" runat="server" clientidmode="static" class="input-list">
    <br />
    <br />
    <h4 class="heading colr">Special Groups</h4>
    <asp:CheckBoxList CssClass="aspnet-checkbox" RepeatDirection="Vertical" RepeatLayout="Flow" RepeatColumns="1"
        DataTextField="Name" DataValueField="Id" ID="cblSpecialGroups" runat="server">
    </asp:CheckBoxList>
</span>
<br />
<br />
<div>
    <asp:Button CssClass="btn btn-primary" ID="cmdSubmit" runat="server" Text="Update"
        Enabled="False" OnClick="cmdSubmit_Click" />
    &nbsp;
    <asp:Button CssClass="btn btn-default" ID="cmdCancel" runat="server" Text="Cancel"
        OnClick="cmdCancel_Click" Visible="False" />
</div>
<br />
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
<br />
<br />
