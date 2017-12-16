<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstanceEditView.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Incident.InstanceEditView" %>
<div class="form-horizontal">
    <div class="form-group">
        <label for="txtName" class="col-sm-2 control-label">Name<asp:RequiredFieldValidator ID="rfvCaption" runat="server" ControlToValidate="txtName"
            ErrorMessage="Name">*</asp:RequiredFieldValidator></label>
        <div class="col-sm-9">
            <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label for="txtDescription" class="col-sm-2 control-label">Description</label>
        <div class="col-sm-9">
            <asp:TextBox ID="txtDescription" ClientIDMode="Static" runat="server" CssClass="form-control" Rows="4"
                TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label for="txtIncidentPrefix" class="col-sm-2 control-label">Prefix</label>
        <div class="col-sm-9">
            <asp:TextBox ID="txtIncidentPrefix" ClientIDMode="Static" Text="IN" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-offset-2 col-sm-10">
            <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
            OnClick="cmdUpdate_Click" />
        <asp:Button ID="cmdCancel" CssClass="btn" runat="server" Text="Cancel"
            OnClick="cmdCancel_Click" CausesValidation="False" />
        </div>
    </div>
</div>
<div class="control-box">
    <div>
        
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
