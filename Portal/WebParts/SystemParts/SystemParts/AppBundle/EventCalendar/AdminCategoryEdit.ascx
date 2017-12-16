﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminCategoryEdit.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.EventCalendar.AdminCategoryEdit" %>
<table border="0">
    <tr>
        <td>Name:<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" Columns="75" CssClass="input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Template:
        </td>
        <td>
            <asp:DropDownList ID="cboTemplates" DataTextField="Name" CssClass="input" DataValueField="Id"
                runat="server" AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value="-1"># None #</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
            OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel"
            OnClick="cmdCancel_Click" CausesValidation="False" />
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
