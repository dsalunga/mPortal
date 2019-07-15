<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileUpdate.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.ProfileUpdateController" %>
<%--<%@ Register Src="~/Content/Controls/PhoneNumber.ascx" TagName="PhoneNumber" TagPrefix="uc1" %>--%>
<asp:HiddenField ID="hFirstLogin" Value="0" runat="server" />
<div runat="server" visible="false" id="panelLastUpdate">
    You have updated your profile last <strong runat="server" id="lblLastUpdate">&lt;UNKNOWN&gt;</strong>
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
        Church ID #: <strong runat="server" id="lblUserName"></strong>
    </div>
    <div style="padding-bottom: 4px">
        Email Address: <strong runat="server" id="lblEmailAddress"></strong>
    </div>
</div>
<div style="float: left; clear: both">
    <!-- # start of Personal Information # -->
    <br />
    <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
        <asp:View ID="viewProfile" runat="server">
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
            <h4 class="heading colr">PERSONAL Information</h4>
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
            <br />
            <div class="FieldLine">
                <asp:Label ID="Label1" CssClass="FieldLabel" AssociatedControlID="txtEmail" runat="server">Email</asp:Label>
                <asp:TextBox Columns="40" Enabled="false" ID="txtEmail" CssClass="input" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
            <div id="Div2" class="FieldLine" runat="server" visible="false">
                <asp:Label ID="Label2" CssClass="FieldLabel" AssociatedControlID="txtNewEmail" runat="server">New Email</asp:Label>
                <asp:TextBox Columns="40" ID="txtNewEmail" runat="server"></asp:TextBox>
            </div>
            <div class="FieldLine">
                <label runat="server" id="lblMobileNumber" class="FieldLabel">
                    Mobile Number</label>
                <asp:TextBox ID="txtMobileNumber" CssClass="input" placeholder="Country Code + Mobile Number" runat="server"></asp:TextBox>
            </div>
            <div class="FieldLine">
                <label runat="server" id="lblHomePhone" class="FieldLabel">
                    Home Phone</label>
                <asp:TextBox ID="txtHomePhone" CssClass="input" placeholder="Country Code + Area Code + Phone Number" runat="server"></asp:TextBox>
            </div>
            <br />
            <div class="FieldLine">
                <asp:Label CssClass="FieldLabel" ID="Label3" AssociatedControlID="txtHomeAddress1"
                    runat="server">Address Line 1</asp:Label>
                <asp:TextBox Columns="60" ID="txtHomeAddress1" CssClass="input" runat="server"></asp:TextBox>
            </div>
            <div class="FieldLine">
                <asp:Label CssClass="FieldLabel" AssociatedControlID="txtHomeAddress2" runat="server">Address Line 2</asp:Label>
                <asp:TextBox Columns="60" ID="txtHomeAddress2" CssClass="input" runat="server"></asp:TextBox>
            </div>
            <div id="Div3" class="FieldLine" runat="server" visible="false">
                <asp:Label CssClass="FieldLabel" ID="Label4" AssociatedControlID="cboHomeAddressState"
                    runat="server">State (US Only)</asp:Label>
                <asp:DropDownList ID="cboHomeAddressState" runat="server" DataSourceID="ObjectDataSourceUSStates"
                    DataTextField="StateName" DataValueField="StateCode" AppendDataBoundItems="true">
                    <asp:ListItem Text="- Select -" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSourceUSStates" runat="server" SelectMethod="GetUSStates"
                    TypeName="WCMS.WebSystem.WebParts.Profile.ProfileUpdateController"></asp:ObjectDataSource>
            </div>
            <div class="FieldLine">
                <asp:Label CssClass="FieldLabel" ID="Label5" AssociatedControlID="txtHomeAddressZipCode"
                    runat="server">Zip Code</asp:Label>
                <asp:TextBox Columns="10" ID="txtHomeAddressZipCode" CssClass="input" runat="server"></asp:TextBox>
            </div>
            <div class="FieldLine">
                <asp:Label CssClass="FieldLabel" ID="Label6" AssociatedControlID="cboHomeAddressCountry"
                    runat="server">Country</asp:Label>
                <asp:DropDownList ID="cboHomeAddressCountry" AutoPostBack="false" runat="server" DataSourceID="ObjectDataSourceCountries"
                    DataTextField="CountryName" DataValueField="CountryCode" CssClass="input" AppendDataBoundItems="true">
                    <asp:ListItem Text="- Select -" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSourceCountries" runat="server" SelectMethod="GetCountries"
                    TypeName="WCMS.WebSystem.WebParts.Profile.ProfileUpdateController"></asp:ObjectDataSource>
            </div>
            <br />
            <div class="FieldLine">
                <asp:Label ID="Label7" CssClass="FieldLabel" AssociatedControlID="txtStatusText"
                    runat="server">Profile Status</asp:Label>
                <asp:TextBox Columns="50" ID="txtStatusText" MaxLength="1499" CssClass="input" runat="server" Rows="4"
                    TextMode="MultiLine"></asp:TextBox>
            </div>
            <br />
            <br />
            <!-- # Start of Work Information # -->
            <h4 class="heading colr">WORK Information</h4>
            <div class="FieldLine">
                <asp:Label CssClass="FieldLabel" ID="Label8" AssociatedControlID="txtWorkAddress1"
                    runat="server">Address Line 1</asp:Label>
                <asp:TextBox ID="txtWorkAddress1" runat="server" CssClass="input" Columns="60"></asp:TextBox>
            </div>
            <div class="FieldLine">
                <asp:Label ID="Label9" CssClass="FieldLabel" AssociatedControlID="txtWorkAddress2"
                    runat="server">Address Line 2</asp:Label>
                <asp:TextBox ID="txtWorkAddress2" runat="server" CssClass="input" Columns="60"></asp:TextBox>
            </div>
            <div id="Div4" class="FieldLine" runat="server" visible="false">
                <asp:Label ID="Label10" CssClass="FieldLabel" AssociatedControlID="cboWorkAddressState"
                    runat="server">State (US Only)</asp:Label>
                <asp:DropDownList ID="cboWorkAddressState" runat="server" DataSourceID="ObjectDataSourceUSStates"
                    DataTextField="StateName" DataValueField="StateCode" AppendDataBoundItems="true">
                    <asp:ListItem Text="- Select -" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="FieldLine">
                <asp:Label ID="Label11" CssClass="FieldLabel" AssociatedControlID="txtWorkAddressZipCode"
                    runat="server">Zip Code</asp:Label>
                <asp:TextBox ID="txtWorkAddressZipCode" runat="server" CssClass="input" Columns="10"></asp:TextBox>
            </div>
            <div class="FieldLine">
                <asp:Label ID="Label12" CssClass="FieldLabel" AssociatedControlID="cboWorkAddressCountry"
                    runat="server">Country</asp:Label>
                <asp:DropDownList ID="cboWorkAddressCountry" runat="server" DataSourceID="ObjectDataSourceCountries"
                    DataTextField="CountryName" DataValueField="CountryCode" AppendDataBoundItems="true" CssClass="input">
                    <asp:ListItem Text="- Select -" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <br />
            <div class="FieldLine">
                <asp:Label CssClass="FieldLabel" ID="Label13" AssociatedControlID="txtWorkDesignation"
                    runat="server">Work Designation</asp:Label>
                <asp:TextBox ID="txtWorkDesignation" runat="server" CssClass="input" Columns="40"></asp:TextBox>
            </div>
            <div class="FieldLine">
                <asp:Label CssClass="FieldLabel" ID="Label14" AssociatedControlID="txtWorkPhone"
                    runat="server">Work Phone</asp:Label>
                <asp:TextBox ID="txtWorkPhone" placeholder="Country Code + Area Code + Phone Number" runat="server" CssClass="input"></asp:TextBox>
            </div>
            <div id="panelPrivacy" runat="server" clientidmode="static">
                <br />
                <br />
                <h4 class="heading colr">Privacy Settings</h4>
                <label class="checkbox inline">
                    <input runat="server" type="checkbox" id="chkPrivate" value="chkPrivate" />
                    Make my contact information private (only Admin can view)
                </label>
            </div>
            <br />
            <br />
            <div>
                <asp:Button CssClass="btn btn-primary" ID="cmdSubmit" runat="server" Text="Update"
                    Enabled="False" OnClick="cmdSubmit_Click" />
                &nbsp;
                <asp:Button CssClass="btn btn-default" ID="cmdCancel" runat="server" Text="Cancel"
                    OnClick="cmdCancel_Click" Visible="False" />
            </div>
        </asp:View>
        <asp:View ID="viewNewEmailConfirmed" runat="server">
            <h3>Your new email address is now confirmed!</h3>
        </asp:View>
        <asp:View ID="viewInvalidRequest" runat="server">
            <h3>Your request is invalid.</h3>
        </asp:View>
    </asp:MultiView>
    <br />
    <br />
</div>
