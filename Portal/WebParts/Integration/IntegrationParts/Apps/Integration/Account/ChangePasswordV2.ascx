<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePasswordV2.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.ChangePasswordV2" %>
<%@ Register Src="~/Content/Parts/Central/Controls/ChangePasswordForm.ascx" TagName="ChangePasswordForm"
    TagPrefix="uc1" %>

<div class="container">
    <div class="row">
        <asp:HiddenField ID="hUserId" runat="server" Value="-1" />
        <asp:HiddenField ID="hEmailDomainFilter" runat="server" Value="" />
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="viewUpdate" runat="server">
                <%--<h1 class="page-header" style="margin-top: 0">Change Password</h1>--%>
                <div class="grid-padding">
                    <uc1:ChangePasswordForm ID="ChangePasswordForm1" runat="server" />
                </div>
                <div runat="server" id="panelError" visible="false">
                    <div style="font-size: larger; color: White; background-color: Red; padding: 2px; display: inline;">
                        <span runat="server" id="lblError" enableviewstate="false"></span>
                    </div>
                    <br />
                    <br />
                </div>
                <div id="panelButtons">
                    <asp:Button CssClass="btn btn-primary" ID="cmdSubmit" runat="server" Text="Update"
                        OnClick="cmdSubmit_Click" ClientIDMode="Static" OnClientClick="prepareAndSubmit();" />
                    &nbsp;
            <a class="btn btn-default" href="/Account/">Cancel</a>
                </div>
                <br />
                <div runat="server" clientidmode="static" id="panelUpdateNote">
                </div>
                <br />
                <span class="alert alert-warning" id="panelAlert">Updating your password may take a little while.</span>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
                    ShowMessageBox="True" ShowSummary="False" />
            </asp:View>
            <asp:View ID="viewDisplayNote" runat="server">
                <div runat="server" clientidmode="static" id="panelPageNote">
                    Password update not allowed.
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</div>
