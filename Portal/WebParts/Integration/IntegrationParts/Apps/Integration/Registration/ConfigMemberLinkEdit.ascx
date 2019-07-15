<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigMemberLinkEdit.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.ConfigMemberLinkEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<div class="member-link-form">
    <%--<h2>Member Link</h2>--%>
    <span class="note">NOTE: Updating this data does not sync the changes to External DB automatically.
        You need to update the External DB separately.</span>
    <br />
    <br />
    <div class="FieldRow">
        <span class="FieldLabel">Name</span><span><asp:TextBox ID="txtName" runat="server"
            Columns="25" ReadOnly="True" Enabled="False" CssClass="input"></asp:TextBox>
        </span>
    </div>
    <div class="FieldRow">
        <span class="FieldLabel">Email</span><span><asp:TextBox ID="txtEmail" runat="server"
            Columns="30" ReadOnly="True" Enabled="False" CssClass="input"></asp:TextBox>
        </span>
    </div>
    <div class="FieldRow">
        <span class="FieldLabel">Group ID</span><span><asp:TextBox ID="txtExternalIDNo" runat="server"
            Columns="15" CssClass="input"></asp:TextBox>
        </span>
    </div>
    <div class="FieldRow">
        <span class="FieldLabel">Membership Date</span><span><asp:TextBox ID="txtMembershipDate"
            runat="server" Columns="15" CssClass="input"></asp:TextBox>
            <asp:CalendarExtender ID="txtMembershipDate_CalendarExtender" runat="server" Enabled="True"
                Format="yyyy-MM-dd" TargetControlID="txtMembershipDate">
            </asp:CalendarExtender>
        </span>
    </div>
    <div class="FieldRow min-bottom-margin">
        <span class="FieldLabel">Photo Path</span><span><asp:TextBox ID="txtPhotoPath" runat="server"
            Columns="85" CssClass="input"></asp:TextBox>
        </span><span runat="server" id="panelExternalSync" visible="false">
            <asp:Button ID="cmdExternalSync" runat="server" Text="External Sync" CssClass="btn btn-default btn-sm" OnClick="cmdExternalSync_Click" /></span>
    </div>
    <img src="/Content/Assets/Images/nophoto.png" width="300" runat="server" id="memberPhoto" style="border: solid 2px #aaa; margin: 2px" />
    <br />
    <br />
    <div>
        <asp:Button CssClass="btn btn-primary" ID="cmdSubmit" runat="server" Text="Update"
            OnClick="cmdSubmit_Click" />
        &nbsp;
        <asp:Button CssClass="btn btn-default" ID="cmdCancel" runat="server" Text="Cancel"
            OnClick="cmdCancel_Click" />
    </div>
    <br />
    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
</div>
