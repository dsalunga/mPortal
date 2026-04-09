<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileUpdate.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.Account.ProfileUpdateController" %>
<div class="container">
    <div>
        <asp:HiddenField ID="hFirstLogin" Value="0" runat="server" />
        <div runat="server" visible="false" id="panelLastUpdate">
            You have updated your profile last <span runat="server" id="lblLastUpdate" style="font-style: italic">&lt;UNKNOWN&gt;</span>
        </div>
        <div runat="server" visible="false" id="panelNotice" style="background-color: Yellow; padding: 5px">
            <h4>
                <strong>NOTE</strong>:&nbsp;Please update your profile before proceeding with any
        activities in the Portal. Thank you!</h4>
        </div>
        <div id="panelBasicInfo" style="float: left;" runat="server" visible="false">
            <br />
            <img src="/Content/Assets/Images/user_green.png" width="300" id="memberPhoto" runat="server" style="border: solid 2px #aaa; float: left; margin: 2px" />
            <div style="padding-bottom: 4px">
                Name: <strong runat="server" id="lblFullName"></strong>
            </div>
            <div style="padding-bottom: 4px">
                Group ID #: <strong runat="server" id="lblUserName"></strong>
            </div>
            <div style="padding-bottom: 4px">
                Email Address: <strong runat="server" id="lblEmailAddress"></strong>
            </div>
        </div>
    </div>
    <div class="row">
        <!-- # start of Personal Information # -->
        <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
            <asp:View ID="viewProfile" runat="server">
                <div class="col-md-6">
                    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                    <h3 class="page-header">Personal</h3>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label15" AssociatedControlID="txtFirstName"
                            runat="server">First Name</asp:Label>
                        <asp:TextBox Columns="50" ID="txtFirstName" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label16" AssociatedControlID="txtMiddleName"
                            runat="server">Middle Name</asp:Label>
                        <asp:TextBox Columns="50" ID="txtMiddleName" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label17" AssociatedControlID="txtLastName" runat="server">Last Name</asp:Label>
                        <asp:TextBox Columns="50" ID="txtLastName" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label19" AssociatedControlID="txtNickname" runat="server">Nickname</asp:Label>
                        <asp:TextBox ID="txtNickname" Columns="40" runat="server" CssClass="input"></asp:TextBox>
                    </div>
                    <h3 class="page-header">Contact</h3>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" AssociatedControlID="txtEmail" runat="server">Email</asp:Label>
                        <asp:TextBox Columns="40" CssClass="input" ID="txtEmail" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </div>
                    <div class="FieldLine" runat="server" id="panelEmailPending" visible="false" clientidmode="Static">
                        <div id="alertEmail" class="alert alert-warning fade in" style="padding: 5px">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true" style="font-weight: normal">Cancel</button>
                            <strong runat="server" id="lblActiveEmail" clientidmode="Static">synthetic_user@example.test</strong> (active), new - pending confirmation
                        </div>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" AssociatedControlID="txtEmail2" runat="server">Email (Secondary)</asp:Label>
                        <asp:TextBox Columns="40" ID="txtEmail2" CssClass="input" ClientIDMode="Static" runat="server" placeholder="Enter your Secondary Email"></asp:TextBox>
                    </div>
                    <div class="FieldLine" runat="server" id="panelEmailPending2" visible="false" clientidmode="Static">
                        <div id="alertEmail2" class="alert alert-warning fade in" style="padding: 5px">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true" style="font-weight: normal">Cancel</button>
                            <strong runat="server" id="lblActiveEmail2" clientidmode="Static">synthetic_user@example.test</strong> (active), new - pending confirmation
                        </div>
                    </div>
                    <div class="FieldLine" style="margin-bottom: 15px;">
                        <span class="text-muted">* You need to confirm your new email in order to take effect.</span>
                    </div>
                    <br />
                    <div class="FieldLine">
                        <label runat="server" id="lblMobileNumber" class="FieldLabel">
                            Mobile Number</label>
                        <asp:TextBox ID="txtMobileNumber" CssClass="input" placeholder="Country Code + Mobile Number" runat="server"></asp:TextBox>
                        <%--<uc1:phonenumber id="mobileNumber" runat="server" />--%>
                    </div>
                    <div class="FieldLine">
                        <label runat="server" id="lblHomePhone"
                            class="FieldLabel">
                            Home Phone</label>
                        <asp:TextBox ID="txtHomePhone" CssClass="input" placeholder="Country Code + Area Code + Phone Number" runat="server"></asp:TextBox>
                        <%--<uc1:PhoneNumber ID="homePhone" runat="server" />--%>
                    </div>
                    <br />
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label1" AssociatedControlID="txtHomeAddress1"
                            runat="server">Address Line 1</asp:Label>
                        <asp:TextBox Columns="50" CssClass="input" ID="txtHomeAddress1" runat="server"></asp:TextBox>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label13" AssociatedControlID="txtHomeAddress2"
                            runat="server">Line 2</asp:Label>
                        <asp:TextBox Columns="50" CssClass="input" ID="txtHomeAddress2" runat="server"></asp:TextBox>
                    </div>
                    <div class="FieldLine" runat="server" visible="false">
                        <asp:Label CssClass="FieldLabel" ID="Label2" AssociatedControlID="cboHomeAddressState"
                            runat="server">State (US Only)</asp:Label>
                        <asp:DropDownList CssClass="input" ID="cboHomeAddressState" runat="server" DataSourceID="ObjectDataSourceUSStates"
                            DataTextField="StateName" DataValueField="StateCode" AppendDataBoundItems="true">
                            <asp:ListItem Text="- Select -" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSourceUSStates" runat="server" SelectMethod="GetUSStates"
                            TypeName="WCMS.WebSystem.WebParts.Profile.ProfileUpdateController"></asp:ObjectDataSource>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label4" AssociatedControlID="txtHomeAddressZipCode"
                            runat="server">Zip Code</asp:Label>
                        <asp:TextBox Columns="10" CssClass="input" ID="txtHomeAddressZipCode" runat="server"></asp:TextBox>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label3" AssociatedControlID="cboHomeAddressCountry"
                            runat="server">Country</asp:Label>
                        <asp:DropDownList ID="cboHomeAddressCountry" CssClass="input" AutoPostBack="false" runat="server" DataSourceID="ObjectDataSourceCountries"
                            DataTextField="CountryName" DataValueField="CountryCode" AppendDataBoundItems="true">
                            <asp:ListItem Text="- Select -" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSourceCountries" runat="server" SelectMethod="GetCountries"
                            TypeName="WCMS.WebSystem.WebParts.Profile.ProfileUpdateController"></asp:ObjectDataSource>
                    </div>
                    <br />
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" AssociatedControlID="txtStatusText" runat="server">Profile Status</asp:Label>
                        <asp:TextBox Columns="50" CssClass="input" ID="txtStatusText" MaxLength="1499" runat="server" Rows="4"
                            TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <!-- # Start of Work Information # -->
                    <h3 class="page-header">Other</h3>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" AssociatedControlID="cboWorkAddressCountry" runat="server">Gender</asp:Label>
                        <asp:DropDownList ID="cboGender" CssClass="input" runat="server">
                            <asp:ListItem Selected="True" Text="" Value="U"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" AssociatedControlID="cboWorkAddressCountry" runat="server">Marital Status</asp:Label>
                        <asp:DropDownList ID="cboMaritalStatus" CssClass="input" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="" Value="-1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Single" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Married" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Widowed" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <h3 class="page-header">Work</h3>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label7" AssociatedControlID="txtWorkAddress1"
                            runat="server">Address Line 1</asp:Label>
                        <asp:TextBox ID="txtWorkAddress1" CssClass="input" runat="server" Columns="50"></asp:TextBox>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" AssociatedControlID="txtWorkAddress2" runat="server">Line 2</asp:Label>
                        <asp:TextBox ID="txtWorkAddress2" CssClass="input" runat="server" Columns="50"></asp:TextBox>
                    </div>
                    <div class="FieldLine" runat="server" visible="false">
                        <asp:Label CssClass="FieldLabel" AssociatedControlID="cboWorkAddressState" runat="server">State (US Only)</asp:Label>
                        <asp:DropDownList CssClass="input" ID="cboWorkAddressState" runat="server" DataSourceID="ObjectDataSourceUSStates"
                            DataTextField="StateName" DataValueField="StateCode" AppendDataBoundItems="true">
                            <asp:ListItem Text="- Select -" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" AssociatedControlID="txtWorkAddressZipCode" runat="server">Zip Code</asp:Label>
                        <asp:TextBox ID="txtWorkAddressZipCode" CssClass="input" runat="server" Columns="10"></asp:TextBox>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" AssociatedControlID="cboWorkAddressCountry" runat="server">Country</asp:Label>
                        <asp:DropDownList ID="cboWorkAddressCountry" CssClass="input" runat="server" DataSourceID="ObjectDataSourceCountries"
                            DataTextField="CountryName" DataValueField="CountryCode" AppendDataBoundItems="true">
                            <asp:ListItem Text="- Select -" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label11" AssociatedControlID="txtWorkDesignation"
                            runat="server">Work Designation</asp:Label>
                        <asp:TextBox ID="txtWorkDesignation" CssClass="input" runat="server" Columns="40"></asp:TextBox>
                    </div>
                    <div class="FieldLine">
                        <asp:Label CssClass="FieldLabel" ID="Label12" AssociatedControlID="txtWorkPhone"
                            runat="server">Work Phone</asp:Label>
                        <asp:TextBox ID="txtWorkPhone" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div runat="server" id="panelPrivacy">
                        <h3 class="page-header">Privacy Settings</h3>
                        <label class="checkbox inline" style="font-weight: normal">
                            <input runat="server" type="checkbox" id="chkPrivate" checked="checked" value="chkPrivate" />
                            Make my contact information private (only Admin can view)
                        </label>
                    </div>
                </div>
                <div class="col-lg-12">
                    <br />
                    <div>
                        <asp:Button CssClass="btn btn-primary" ID="cmdSubmit" runat="server" Text="Update"
                            Enabled="False" OnClick="cmdSubmit_Click" />
                        &nbsp;
                <asp:Button CssClass="btn btn-default" ID="cmdCancel" runat="server" Text="Cancel"
                    OnClick="cmdCancel_Click" />
                    </div>
                </div>

                <script type="text/javascript">
                    $(document).ready(function () {
                        $('#alertEmail').bind('close.bs.alert', function () {
                            $('#txtEmail').val($('#lblActiveEmail').text());
                            $('#panelEmailPending').hide();
                        });
                        $('#alertEmail2').bind('close.bs.alert', function () {
                            $('#txtEmail2').val($('#lblActiveEmail2').text());
                            $('#panelEmailPending2').hide();
                        });
                    });
                </script>
            </asp:View>
            <asp:View ID="viewNewEmailConfirmed" runat="server">
                <h3>Your new email address is now confirmed!</h3>
                <a href="#" class="btn btn-primary" id="linkContinue" runat="server">Continue</a>
            </asp:View>
            <asp:View ID="viewInvalidRequest" runat="server">
                <h3>Your request is invalid.</h3>
            </asp:View>
        </asp:MultiView>
    </div>
</div>
