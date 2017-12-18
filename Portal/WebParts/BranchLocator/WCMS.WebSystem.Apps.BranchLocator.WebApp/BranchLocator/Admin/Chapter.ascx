<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Chapter.ascx.cs" Inherits="WCMS.WebSystem.Apps.BranchLocator.WebOfficeController" %>
<h3 runat="server" id="lblHeader">New Chapter</h3>
<div role="tabpanel">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation"><a href="#" runat="server" id="linkChapters" role="tab">Chapters</a></li>
        <li role="presentation" class="active"><a href="#" aria-controls="chapter" role="tab" data-toggle1="tab">Chapter</a></li>
        <li role="presentation"><a href="#" runat="server" id="linkLocation" role="tab" data-toggle1="tab">Location</a></li>
        <li role="presentation"><a href="#" runat="server" id="linkAnnouncements" role="tab" data-toggle1="tab">Announcements</a></li>
    </ul>

    <!-- Tab panes -->
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="chapter">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="txtName" class="col-sm-2 col-md-2 control-label">
                        Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                            ErrorMessage="Name" ForeColor="Red">*</asp:RequiredFieldValidator></label>
                    <div class="col-sm-8 col-md-6">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtAddressLine1" class="col-sm-2 col-md-2 control-label">Address:</label>
                    <div class="col-sm-8 col-md-6">
                        <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="cboCountries" class="col-sm-2 col-md-2 control-label">Country:</label>
                    <div class="col-sm-4">
                        <asp:DropDownList DataTextField="CountryName" CssClass="input1 form-control" AppendDataBoundItems="true" DataValueField="CountryCode" ID="cboCountries"
                            runat="server">
                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtPhoneNumber" class="col-sm-2 col-md-2 control-label">Phone Number:</label>
                    <div class="col-sm-4 col-md-3">
                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtMobileNumber" class="col-sm-2 col-md-2 control-label">Mobile Number:</label>
                    <div class="col-sm-4 col-md-3">
                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtEmailAddress" class="col-sm-2 col-md-2 control-label">Email Address:</label>
                    <div class="col-sm-4 col-md-3">
                        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtServiceSchedule" class="col-sm-2 col-md-2 control-label">Services Schedule:</label>
                    <div class="col-sm-8 col-md-6">
                        <asp:TextBox ID="txtServiceSchedule" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtMoreInfo" class="col-sm-2 col-md-2 control-label">More Info:</label>
                    <div class="col-sm-8 col-md-6">
                        <asp:TextBox ID="txtMoreInfo" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        &nbsp;
                    </div>
                </div>
                <div class="form-group">
                    <label for="cboChapterType" class="col-sm-2 col-md-2 control-label">Type:</label>
                    <div class="col-sm-3"><asp:DropDownList ID="cboChapterType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">Regular Locale</asp:ListItem>
                            <asp:ListItem Value="1">District Office</asp:ListItem>
                            <asp:ListItem Value="2">Division Office</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="cboDivision" class="col-sm-2 col-md-2 control-label">Division:</label>
                    <div class="col-sm-6"><asp:DropDownList DataTextField="Name" AppendDataBoundItems="true" CssClass="form-control" DataValueField="Id" ID="cboDivision"
                            runat="server">
                            <asp:ListItem Text="" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtDistrictNo" class="col-sm-2 col-md-2 control-label">District No:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtDistrictNo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtLocaleId" class="col-sm-2 col-md-2 control-label">Locale ID:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtLocaleId" runat="server" CssClass="form-control" placeholder="Locale ID"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        &nbsp;
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtParent" class="col-sm-2 col-md-2 control-label">Parent ID:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtParent" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="cboAccessType" class="col-sm-2 col-md-2 control-label">Access Type:</label>
                    <div class="col-sm-3 col-md-2"><asp:DropDownList ID="cboAccessType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">Public</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Internal</asp:ListItem>
                            <asp:ListItem Value="2">Restricted</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtName" class="col-sm-2 col-md-2 control-label">Last Update:</label>
                    <div class="col-sm-6">
                        <span runat="server" id="lblLastUpdate"></span>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-md-offset-2 col-sm-10 col-md-6">
                        <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="cmdUpdate_Click" />
                        <a href="#" runat="server" id="linkCancel" class="btn btn-default">Cancel</a>
                        <asp:Button Visible="false" ID="cmdDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this item?');" CssClass="btn btn-danger pull-right" Text="Delete" OnClick="cmdDelete_Click" />
                        <br />
                        <span runat="server" id="lblStatus" class="label label-warning" visible="false">Update success!</span>
                    </div>
                </div>
            </div>
        </div>
        <%--<div role="tabpanel" class="tab-pane" id="profile">bbb...</div>--%>
    </div>
</div>
