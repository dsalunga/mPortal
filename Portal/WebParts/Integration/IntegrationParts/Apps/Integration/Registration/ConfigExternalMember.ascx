<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigExternalMember.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.ConfigExternalMember" %>
<div class="ams-member-form">
    <%--<h2>External Member</h2>--%>
    <span class="note">NOTE: Please take caution in changing any data, this will update
        the PROD External DB directly. Updating this data does not sync the changes to Portal
        DB automatically.</span>
    <br />
    <br />
    <div class="FieldRow">
        <span class="FieldLabel">First Name</span><span><asp:TextBox ID="txtFirstName" runat="server"
            Columns="25" CssClass="input"></asp:TextBox>
        </span>
    </div>
    <div class="FieldRow">
        <span class="FieldLabel">Middle Name</span><span><asp:TextBox ID="txtMiddleName"
            runat="server" Columns="25" CssClass="input"></asp:TextBox>
        </span>
    </div>
    <div class="FieldRow">
        <span class="FieldLabel">Last Name</span><span><asp:TextBox ID="txtLastName" runat="server"
            Columns="25" CssClass="input"></asp:TextBox>
        </span>
    </div>
    <div class="FieldRow">
        <span class="FieldLabel">Group ID</span><span><asp:TextBox ID="txtExternalIDNo" runat="server"
            Columns="15" CssClass="input"></asp:TextBox>
        </span>
    </div>
    <div class="FieldRow">
        <span class="FieldLabel">Temporary ID</span><span><asp:TextBox ID="txtTempExternalID"
            runat="server" Columns="15" CssClass="input"></asp:TextBox>
        </span>
    </div>
    <div class="FieldRow">
        <span class="FieldLabel">Email</span><span><asp:TextBox ID="txtEmail" runat="server"
            Columns="30" CssClass="input"></asp:TextBox>
        </span>
    </div>
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
