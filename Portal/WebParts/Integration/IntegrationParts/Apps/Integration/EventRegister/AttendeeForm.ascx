<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="Static" ClassName="WCMS.WebSystem.Apps.Integration.EventRegister.AttendeeFormView" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.Framework.Core" %>
<%@ Import Namespace="WCMS.Framework.Security" %>
<%@ Import Namespace="WCMS.Framework.Utilities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { }
    }

    public void Initialize(string managerGroup)
    {
        cboCountries.DataSource = WCMS.Framework.Core.Shared.Country.GetList();
        cboCountries.DataBind();

        if (!string.IsNullOrEmpty(managerGroup))
        {
            var group = WebGroup.SelectNode(managerGroup);
            if (group != null)
            {
                cboCoordinator.DataSource = group.Users;
                cboCoordinator.DataBind();
            }
        }
    }

    public void LoadData(string groupFilter, int userId = -1, int memberId = -1, string externalId = "")
    {
        var currentUser = WSession.Current.User;
        if (cboCoordinator.Items.FindByValue(currentUser.Id.ToString()) == null)
            cboCoordinator.Items.Add(new ListItem(currentUser.FirstAndLastName, currentUser.Id.ToString()));

        var user = userId > 0 ? WebUser.Get(userId) : null;
        if (user != null)
        {
            this.hUserId.Value = userId.ToString();

            txtEmail.Text = user.Email;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            this.Gender = user.Gender;
            txtMobileNumber.Text = user.MobileNumber;

            var link = MemberLink.Provider.GetByUserId(user.Id);
            if (link != null)
            {
                txtExternalId.Text = link.ExternalIdNo;
                txtMembershipDate.Text = link.MembershipDate.ToString("yyyy-MM-dd");
                WebHelper.SetCboValue(cboCountries, link.LocaleCountryCode);
                txtLocale.Text = link.Locale;
                txtNickname.Text = link.Nickname;
                memberPhoto.Src = link.GetPhotoPathIfNull();

                if (user.IsActive)
                {
                    txtFirstName.ReadOnly = true;
                    txtLastName.ReadOnly = true;
                    if (!string.IsNullOrEmpty(link.ExternalIdNo))
                        txtExternalId.ReadOnly = true;
                    txtMembershipDate.ReadOnly = true;
                    txtMembershipDate_CalendarExtender.Enabled = false;
                }
            }
            else
            {
                memberPhoto.Src = user.GetPhotoPath();
            }

            var group = WebGroup.SelectNode(groupFilter);
            var ug = WebUserGroup.Get(group.Id, user.Id);
            if (ug != null)
            {
                var createdBy = ug.CreatedBy;
                if (createdBy != null)
                {
                    if (cboCoordinator.Items.FindByValue(createdBy.Id.ToString()) == null)
                        cboCoordinator.Items.Add(new ListItem(createdBy.FirstAndLastName, createdBy.Id.ToString()));

                    WebHelper.SetCboValue(cboCoordinator, createdBy.Id);
                }
            }
        }
        else if (memberId > 0 && !string.IsNullOrEmpty(externalId))
        {
            this.hMemberId.Value = memberId.ToString();

            var client = WCMS.WebSystem.Apps.Integration.ExternalMemberWS.MemberSoapClient.GetNewClientInstance();
            var status = client.GetMembershipStatus(memberId);
            if (status != null)
            {
                var profile = client.GetProfile(externalId, status.MembershipDate);
                if (profile != null)
                {
                    txtEmail.Text = profile.Email;
                    txtFirstName.Text = profile.FirstName;
                    txtLastName.Text = profile.LastName;
                    if (!string.IsNullOrEmpty(profile.Gender))
                        this.Gender = char.Parse(profile.Gender);
                    txtMobileNumber.Text = profile.Mobile;

                    txtExternalId.Text = externalId;
                    txtMembershipDate.Text = status.MembershipDate.ToString("yyyy-MM-dd");
                    txtNickname.Text = profile.NickName;
                }
            }

            var photoUrl = MemberLink.GetPhotoPath(memberId);
            memberPhoto.Src = photoUrl;
            hPhotoUrl.Value = photoUrl;
        }

        if (!WSession.Current.IsAdministrator)
            cboCoordinator.Enabled = false;
    }

    public WebUser UpdateData(string groupFilter, string managerGroupFilter, ParameterizedWebObject paramSet)
    {
        var group = WebGroup.SelectNode(groupFilter);
        var managerGroup = WebGroup.SelectNode(managerGroupFilter);
        if (group != null && managerGroup != null)
        {
            WebUserGroup ug = null;
            var memberId = DataHelper.GetId(hMemberId.Value);
            var photoUrl = hPhotoUrl.Value.Trim();
            var session = WSession.Current;
            var isUserCoordinator = session.User.IsMemberOf(managerGroup.Id);

            var createdById = DataHelper.GetId(cboCoordinator.SelectedValue);
            if (createdById == -1)
                createdById = session.UserId;

            var userId = DataHelper.GetId(hUserId.Value);
            var externalId = txtExternalId.Text.Trim();
            var existingUser = true;
            WebUser user = userId > 0 ? WebUser.Get(userId) : null;
            if (user == null)
            {
                existingUser = false;
                user = new WebUser();
                user.Status = AccountStatus.PENDING; // This account will be activated in succeeding code below
                user.UserName = externalId;
                user.Password = OtpCodeGenerator.Generate();
                user.PasswordExpiryDate = WConstants.DateTimeMinValue.AddDays(1);
                if (memberId > 0 && !string.IsNullOrEmpty(photoUrl))
                    user.PhotoPath = photoUrl;
            }
            else
            {
                // Existing user
                ug = WebUserGroup.Get(group.Id, user.Id);
                if (ug != null && (session.IsAdministrator || isUserCoordinator)) // || ug.CreatedById <= WConstants.NULL_ID))
                {
                    //if (ug.CreatedById > 0 && ug.CreatedById != cUserId)
                    //{
                    //    if (ug.CreatedById != cUserId)
                    //    {
                    //        lblStatus.InnerHtml = "This attendee is already being managed by another coordinator - " + ug.CreatedBy.FirstAndLastName;
                    //        lblStatus.Visible = true;
                    //        return user;
                    //    }
                    //}
                    if (ug.CreatedById != createdById)
                    {
                        ug.CreatedById = createdById;
                        ug.Update();
                    }
                }
            }

            user.FirstName = txtFirstName.Text.Trim();
            user.LastName = txtLastName.Text.Trim();
            user.Email = txtEmail.Text.Trim();
            user.Gender = this.Gender;
            user.MobileNumber = txtMobileNumber.Text.Trim();
            user.Update();

            var countryCode = DataHelper.GetInt32(cboCountries.SelectedValue);
            var link = MemberLink.Provider.GetByUserId(user.Id);
            if (link == null)
            {
                link = new MemberLink();
                link.Approved = MemberAccountStatus.Approved;
                link.UserId = user.Id;
                link.IsPrivate = true;
                link.MemberId = memberId;
            }

            link.Nickname = txtNickname.Text.Trim();
            link.ExternalIdNo = externalId;
            link.MembershipDate = DataHelper.GetDateTime(txtMembershipDate.Text.Trim());
            link.Locale = txtLocale.Text.Trim();
            link.LocaleCountryCode = countryCode;
            link.Update();

            var address = existingUser ? user.Addresses.FirstOrDefault(i => i.Tag.Equals(AddressTags.Home)) : user.NewAddress(AddressTags.Home);
            if (address == null)
                address = user.NewAddress(AddressTags.Home);
            address.CountryCode = countryCode;
            address.Update();

            if (ug == null)
                user.AddToGroup(group.Id, 1, createdById);

            if (!existingUser)
                user.AddToGroup("Members");

            HandlePhotoUpdate(user);

            // Activate Account
            if (!existingUser || user.Status == AccountStatus.PENDING)
                MemberHelper.ActivateAccount(user.Id, group.Id, paramSet, HttpContext.Current);

            return user;
        }
        return null;
    }

    private bool HandlePhotoUpdate(WebUser user)
    {
        if (photoUpload.HasFile)
        {
            var fileName = photoUpload.PostedFile.FileName;
            if (ImageUtil.IsValidImage(fileName))
            {
                var photoSize = 600; //DataHelper.GetInt32(element.GetParameterValue("PhotoSize"), 600);
                var ext = Path.GetExtension(fileName);
                var previewUrl = AccountHelper.UploadPhotoForPreview(user.Id, photoUpload, photoSize);
                var thumbSize = 200; //DataHelper.GetInt32(element.GetParameterValue("ThumbSize"), 200);

                AccountHelper.FinalizePhotoUpload(user, -1, ext, thumbSize);
                return true;
            }
        }
        return false;
    }

    public char Gender
    {
        get { return char.Parse(cboGender.SelectedValue); }
        set { cboGender.SelectedValue = value.ToString(); }
    }
</script>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<asp:HiddenField ID="hUserId" runat="server" Value="-1" />
<asp:HiddenField ID="hMemberId" runat="server" Value="-1" />
<asp:HiddenField ID="hPhotoUrl" runat="server" Value="" />
<div runat="server" id="lblStatus" class="alert alert-danger" visible="false" enableviewstate="false"></div>
<table>
    <tr>
        <td>Group ID<asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtExternalId"
            ErrorMessage="Group ID" SetFocusOnError="True" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtExternalId" CssClass="input" runat="server" Columns="15"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Email<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
            ErrorMessage="Email Address" ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Valid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ForeColor="Red">*</asp:RegularExpressionValidator>
        </td>
        <td>
            <asp:TextBox ID="txtEmail" CssClass="input" runat="server" Columns="30"></asp:TextBox>
        </td>
    </tr>
    <%--<tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>--%>
    <tr>
        <td style="vertical-align: top">ID Photo</td>
        <td>
            <img src="/content/assets/images/nophoto.png" class="img-responsive" width="300" runat="server" id="memberPhoto" style="border: solid 1px #ccc; margin: 2px 0 2px 0; border-radius: 4px" />
            <asp:FileUpload ID="photoUpload" ClientIDMode="Static" runat="server" />
            <br />
        </td>
    </tr>
    <tr>
        <td>First Name<asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
            ErrorMessage="First Name" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtFirstName" CssClass="input" runat="server" Columns="30"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Last Name<asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
            ErrorMessage="Last Name" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtLastName" CssClass="input" runat="server" Columns="30"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Nickname
        </td>
        <td>
            <asp:TextBox ID="txtNickname" CssClass="input" runat="server" Columns="15"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Gender
        </td>
        <td>
            <asp:DropDownList ID="cboGender" runat="server" CssClass="input">
                <asp:ListItem Selected="True" Text="" Value="U"></asp:ListItem>
                <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Contact #
        </td>
        <td>
            <asp:TextBox ID="txtMobileNumber" CssClass="input" runat="server" Columns="15"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
        <td>Date of Membership<asp:RequiredFieldValidator ID="rfvDateOfMembership" runat="server" ErrorMessage="Date of Membership" ForeColor="Red" SetFocusOnError="True" ControlToValidate="txtMembershipDate">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtMembershipDate" placeholder="Membership Date (YYYY-MM-DD)" runat="server"
                Columns="30" CssClass="input"></asp:TextBox>
            <asp:CalendarExtender ID="txtMembershipDate_CalendarExtender" runat="server" Enabled="True"
                TargetControlID="txtMembershipDate" Format="yyyy-MM-dd" DefaultView="Years">
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>Country<asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="Country" ControlToValidate="cboCountries" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:DropDownList DataTextField="CountryName" DataValueField="CountryCode" ID="cboCountries"
                runat="server" AppendDataBoundItems="true" CssClass="input">
                <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Locale Chapter<asp:RequiredFieldValidator ID="rfvGroupLocale" runat="server" ErrorMessage="Group Locale" ControlToValidate="txtLocale" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtLocale" CssClass="input-xlarge" ToolTip="Group Locale" placeholder="Group Locale"
                runat="server" Columns="30"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Coordinator
        </td>
        <td>
            <asp:DropDownList ID="cboCoordinator" DataTextField="FirstAndLastName" DataValueField="Id" runat="server" CssClass="input">
                <asp:ListItem Selected="True" Text="" Value="-1"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<script type="text/javascript">
    function checkIfExisting(id, checkExternal) {
        if (id) {
            var userId = parseInt($('#hUserId').val());
            var memberId = parseInt($('#hMemberId').val());
            if (userId == -1 && memberId == -1) {
                $.getJSON("/api/v1/member/getuserid?checkExternal=" + checkExternal + "&id=" + id, function (data) {
                    if (data) {
                        if (data.userId && data.userId > 0) {
                            location.href = location.href + '&UserId=' + data.userId;
                        } else if (data.memberId && data.memberId > 0) {
                            location.href = location.href + '&MemberId=' + data.memberId + '&ExternalId=' + $('#txtExternalId').val();
                        }
                    }
                });
            }
        }
    }

    $(document).ready(function () {
        $('#txtExternalId').blur(function () {
            checkIfExisting($('#txtExternalId').val(), 1);
        });

        $('#txtEmail').blur(function () {
            checkIfExisting($('#txtEmail').val(), 0);
        });
    });
</script>
