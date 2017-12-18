using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Security;
using System.Drawing;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class _CMS_Controls_UserProfileForm : System.Web.UI.UserControl
    {
        private const string PHOTO_FOLDER = "/Content/Assets/Uploads/Image/Photo";
        private const string DEFAULT_IMAGE = PHOTO_FOLDER + "/silho.gif";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imagePreview.ImageUrl = DEFAULT_IMAGE;
                imagePhoto.ImageUrl = DEFAULT_IMAGE;

                MultiView1.SetActiveView(viewProfile);
            }
        }

        public void Initialize()
        {
            cboUserProviders.Items[0].Text = AccountConstants.DefaultProviderName;
            cboUserProviders.DataSource = UserProvider.Provider.GetList();
            cboUserProviders.DataBind();
        }

        public void LoadData(int userId)
        {
            this.hiddenUserId.Value = userId.ToString();

            Initialize();

            WebUser user = WebUser.Get(userId);
            if (user != null)
            {
                this.UserName = user.UserName;
                this.EmailAddress = user.Email;
                this.Email2 = user.Email2;

                this.FirstName = user.FirstName;
                this.MiddleName = user.MiddleName;
                this.LastName = user.LastName;

                this.NameSuffix = user.NameSuffix;
                this.Gender = user.Gender;

                MobileNumber = user.MobileNumber;
                PhoneNumber = user.TelephoneNumber;
                StatusText = user.StatusText;
                PhotoPath = user.PhotoPath;

                Status = user.Status;
                ProviderId = user.ProviderId;

                if (user.IsLoginLockedOut)
                    panelLockOut.Visible = true;

                //string imageFile = sPhotoDir + "/" + userId + ".jpg";
                //if (File.Exists(MapPath(imageFile)))
                //{
                //    imagePreview.ImageUrl = imageFile;
                //    imagePhoto.ImageUrl = imageFile;
                //}
                //else
                //{
                //imagePreview.ImageUrl = sDefaultImage;
                //imagePhoto.ImageUrl = sDefaultImage;
                //}

                //txtUsername.ReadOnly = true;
            }
        }

        public void SetEntryMode(bool full)
        {
            panelUsername.Visible = full;
            panelEmail2nd.Visible = full;
            panelStatusText.Visible = full;
            panelActive.Visible = full;
            panelSuffix.Visible = full;
            panelPhotoPath.Visible = full;

            panelActive.Visible = full;
            panelProvider.Visible = full;
        }

        public WebUser UpdateData()
        {
            var userName = this.UserName;
            if (string.IsNullOrEmpty(userName))
                userName = Guid.NewGuid().ToString("D");

            int userId = DataHelper.GetId(hiddenUserId.Value);
            var user = userId > 0 ? WebUser.Get(userId) : new WebUser();
            user.FirstName = this.FirstName;
            user.UserName = userName;
            user.MiddleName = this.MiddleName;
            user.LastName = this.LastName;
            user.Email = this.EmailAddress;
            user.Email2 = this.Email2;
            user.Status = Status;
            user.Gender = this.Gender;
            user.NameSuffix = this.NameSuffix;
            user.MobileNumber = MobileNumber;
            user.TelephoneNumber = PhoneNumber;
            user.StatusText = StatusText;
            user.PhotoPath = PhotoPath;
            user.ProviderId = ProviderId;

            if (user.IsLoginLockedOut && chkResetLockout.Checked)
                user.IsLoginLockedOut = false;

            user.Update();
            return user;
        }

        protected void imagePhoto_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.SetActiveView(viewPicture);
        }

        protected void cmdUpload_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                lblUploadMessage.Text = "* Select a file to upload *";
                return;
            }

            if (!Directory.Exists(MapPath(PHOTO_FOLDER)))
                Directory.CreateDirectory(MapPath(PHOTO_FOLDER));

            string uploadFile = FileUpload1.FileName; // original file
            string destFilename = Membership.GetUser(this.UserName).ProviderUserKey + ".jpg"; // create a jpeg version of filename
            string tempFilename = "TempImage" + DateTime.Now.Second + Path.GetExtension(uploadFile); // create a temporary filename
            FileUpload1.SaveAs(MapPath(WConstants.AdminDataFolder + tempFilename)); // upload to temp folder

            // GET SIZE RATIO
            System.Drawing.Image imageSource = new Bitmap(MapPath(WConstants.AdminDataFolder + tempFilename));
            int iWidth = 150;
            int iHeight = (imageSource.Height * iWidth) / imageSource.Width;

            // resize the image / create thumbnail
            ImageUtil.CreateThumbnail(
                imageSource,
                MapPath(PHOTO_FOLDER + "/" + destFilename),
                iWidth,
                iHeight,
                System.Drawing.Imaging.ImageFormat.Jpeg
            );

            imagePreview.ImageUrl = PHOTO_FOLDER + "/" + destFilename; // show preview
            imagePhoto.ImageUrl = imagePreview.ImageUrl;
            hiddenImageFilename.Value = destFilename;
        }

        protected void cmdImageOK_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewProfile);
        }

        public string UserName
        {
            get { return txtUsername.Text.Trim(); }
            set { txtUsername.Text = value; }
        }

        public string FirstName
        {
            get { return FullNamePicker1.FirstName; }
            set { FullNamePicker1.FirstName = value; }
        }

        public string MiddleName
        {
            get { return FullNamePicker1.MiddleName; }
            set { FullNamePicker1.MiddleName = value; }
        }

        public string LastName
        {
            get { return FullNamePicker1.LastName; }
            set { FullNamePicker1.LastName = value; }
        }

        public string NameSuffix
        {
            get { return txtSuffix.Text.Trim(); }
            set { txtSuffix.Text = value; }
        }

        public string MobileNumber
        {
            get { return txtMobileNumber.Text.Trim(); }
            set { txtMobileNumber.Text = value; }
        }

        public string PhotoPath
        {
            get { return txtPhotoPath.Text.Trim(); }
            set { txtPhotoPath.Text = value; }
        }

        public string PhoneNumber
        {
            get { return txtPhoneNumber.Text.Trim(); }
            set { txtPhoneNumber.Text = value; }
        }

        public int Status
        {
            get { return DataHelper.GetInt32(cboStatus.SelectedValue); }
            set { WebHelper.SetCboValue(cboStatus, value); }
        }

        public int ProviderId
        {
            get { return DataHelper.GetInt32(cboUserProviders.SelectedValue); }
            set { WebHelper.SetCboValue(cboUserProviders, value); }
        }

        public string EmailAddress
        {
            get { return txtEmail.Text.Trim(); }
            set { txtEmail.Text = value; }
        }

        public string Email2
        {
            get { return txtEmail2.Text.Trim(); }
            set { txtEmail2.Text = value; }
        }

        public char Gender
        {
            get { return char.Parse(cboGender.SelectedValue); }
            set { cboGender.SelectedValue = value.ToString(); }
        }

        public string StatusText
        {
            get { return txtStatusText.Text.Trim(); }
            set { txtStatusText.Text = value; }
        }
    }
}