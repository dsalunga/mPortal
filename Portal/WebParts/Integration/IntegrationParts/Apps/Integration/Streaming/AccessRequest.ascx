<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccessRequest.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.Streaming.AccessRequest" %>
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewRequestForm" runat="server">
        <asp:HiddenField ID="hLocation" ClientIDMode="Static" runat="server" Value="" />
        <div class="alert alert-warning" runat="server" id="lblAlert" visible="false">You have already requested for accesss last {0}.</div>
        <p>Please enter your reason for requesting this access. After submitting, the administrator will review your request and a notification email will be sent to you.</p>
        <br />
        <div>Reason for your request:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Reason for your request" ForeColor="Red" ControlToValidate="txtReason">*</asp:RequiredFieldValidator></div>
        <div class="row" style="margin-bottom: 10px">
            <div class="col-md-8 col-sm-10 col-xs-12">
                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" Rows="4" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="cmdRequest" CssClass="btn btn-success" runat="server" Text="Submit Request" OnClick="cmdRequest_Click" />&nbsp;<a href="#" runat="server" id="linkCancel" class="btn btn-default">Cancel</a>
            </div>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:" ShowMessageBox="True" ShowSummary="False" />
        <script type="text/javascript">
            ResolveGeoIp(function (loc) { $('#hLocation').val(loc); });
        </script>
    </asp:View>
    <asp:View ID="viewDone" runat="server">
        Thank you! Your request has been submitted. The administrator will review your request and a notification email will be sent to <span runat="server" id="lblEmail"></span>.
        <br />
        <br />
        <a href="#" runat="server" id="linkContinue" class="btn btn-primary">Done</a>
    </asp:View>
</asp:MultiView>
