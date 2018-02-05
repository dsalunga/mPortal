using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;
using QRCoder;
using System.Drawing.Text;
using System.IO;

namespace WCMS.WebSystem.Apps.Integration.EventRegister
{
    public class EventRegisterUtil
    {
        public static void DownloadCard(WebUser user)
        {
            // Create temporary folder
            var tmpPath = WebHelper.MapPath(WebHelper.CombineAddress(WConfig.TempFolder, "EventRegister"));

            if (!Directory.Exists(tmpPath))
                System.IO.Directory.CreateDirectory(tmpPath);

            var cardFilename = EventRegisterUtil.GenerateCard(user, tmpPath);
            WebHelper.DownloadFile(cardFilename);
        }

        public static string GenerateCard(WebUser user, string savePath)
        {
            var context = HttpContext.Current;
            var link = MemberLink.Provider.GetByUserId(user.Id);

            var assetPath = context.Server.MapPath("~/Content/Parts/Integration/EventRegister/assets");
            var id = Image.FromFile(assetPath + @"\blank-final.jpg");
            var qrGenerator = new QRCodeGenerator();
            var qrCode = qrGenerator.CreateQrCode(link.ExternalIdNo, QRCodeGenerator.ECCLevel.Q);
            var qr = qrCode.GetGraphic(20);
            var qrCropPx = 15;

            var resizedQR = ImageUtil.ScaleImage(qr, 150, 150);
            //var newQRSize = ImageUtil.ScaleImage(qr, 165, 165); //new Size((int)(qr.Size.Width / 3.8), (int)(qr.Size.Height / 3.8));
            //var resizedQR = new Bitmap(qr, newQRSize.Width, newQRSize.Height);

            // Resize QR
            //using (var graphics = Graphics.FromImage(qr))
            //{
            //    // Crop and draw QR code
            //    graphics.DrawImage(qr,
            //        new Rectangle(new Point(0, 0), newQRSize), new Rectangle(0,0, qr.Width, qr.Height), GraphicsUnit.Pixel);
            //}

            using (var graphics = Graphics.FromImage(id))
            {
                var photoXY = new Point(785, 760); //, 800);

                // Overlay Photo
                var photoUrl = user.GetPhotoPath("200x200");
                var noPhoto = user.Gender == GenderTypes.Female ? "/content/assets/images/female.jpg" : "/content/assets/images/male.jpg"; // "/content/assets/images/500px-smiley.png";
                if (string.IsNullOrEmpty(photoUrl) || photoUrl.EndsWith("nophoto.png", StringComparison.InvariantCultureIgnoreCase))
                    photoUrl = noPhoto;

                var photo = ImageUtil.GetImageFromUrl(photoUrl);
                if (photo == null && !photoUrl.Equals(noPhoto))
                    photo = ImageUtil.GetImageFromUrl(noPhoto);

                if (photo != null)
                {
                    var picBoxSize = new Point(299, 299);
                    var scaledPhoto = ImageUtil.ScaleImage(photo, picBoxSize.X, picBoxSize.Y);
                    graphics.DrawImage(scaledPhoto, new Rectangle(450 + (picBoxSize.X - scaledPhoto.Width) / 2, photoXY.Y + (picBoxSize.Y - scaledPhoto.Height) / 2, scaledPhoto.Width, scaledPhoto.Height),
                        new Rectangle(0, 0, scaledPhoto.Width, scaledPhoto.Height), GraphicsUnit.Pixel);
                }

                // Crop and draw QR code
                var cropRect = new Rectangle(qrCropPx, qrCropPx, resizedQR.Width - qrCropPx * 2, resizedQR.Height - qrCropPx * 2);
                graphics.DrawImage(resizedQR, new Rectangle(photoXY.X, photoXY.Y, resizedQR.Width, resizedQR.Height), cropRect, GraphicsUnit.Pixel);

                var fontReklame = new PrivateFontCollection();
                var fontGothicBold = new PrivateFontCollection();
                fontReklame.AddFontFile(assetPath + @"\fonts\ReklameScript-Regular.otf");
                fontGothicBold.AddFontFile(assetPath + @"\fonts\GOTHICB.TTF");

                // Prepare Nickname
                var nickContainerSize = new Point(1030, 330);
                var nickContainerXY = new Point(88, photoXY.Y + 391);
                var displayName = string.IsNullOrEmpty(link.Nickname) ? user.FirstName : link.Nickname;

                var fontSize = 85f;
                var font = new Font(fontReklame.Families[0], fontSize);
                var nickSize = graphics.MeasureString(displayName, font);
                for (int i = 0; i < 30; i++)
                {
                    if (ImageUtil.CanFitInBox(nickContainerSize, new Point((int)nickSize.Width, (int)nickSize.Height), new Point(5, 5)))
                    {
                        break;
                    }
                    else
                    {
                        fontSize -= 2;
                        font = new Font(fontReklame.Families[0], fontSize);
                        nickSize = graphics.MeasureString(displayName, font);
                    }
                }

                // Draw NIckname
                var nicknameXY = new Point((int)(nickContainerXY.X + (nickContainerSize.X - nickSize.Width) / 2), (int)(nickContainerXY.Y + (nickContainerSize.Y - nickSize.Height) / 2));
                var textBrush = new SolidBrush(Color.Black);
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.DrawString(displayName, font, textBrush, nicknameXY.X, nicknameXY.Y);

                // Draw Brother/Sister
                if ( false) // || user.Gender == GenderTypes.Male || user.Gender == GenderTypes.Female)
                {
                    var initSize = fontSize / 2.3;
                    fontSize = (int)initSize;
                    font = new Font(fontReklame.Families[0], fontSize);

                    var genderText = user.Gender == GenderTypes.Male ? "Mr" : "Ms";
                    var genderBoxSize = new Point((int)(nickContainerSize.X - nickSize.Width) / 2, (int)(nickContainerSize.Y / 2));
                    if (genderBoxSize.X < 200)
                        genderBoxSize.X = 200;

                    var genderSize = graphics.MeasureString(genderText, font);
                    for (int i = 0; i < 10; i++)
                    {
                        if (fontSize <= 4 || ImageUtil.CanFitInBox(genderBoxSize, new Point((int)nickSize.Width, (int)nickSize.Height), new Point(5, 5)))
                        {
                            break;
                        }
                        else
                        {
                            fontSize -= 2;
                            font = new Font(fontReklame.Families[0], fontSize);
                            genderSize = graphics.MeasureString(genderText, font);
                        }
                    }

                    var genderXY = new Point((int)(nickContainerXY.X + (genderBoxSize.X - genderSize.Width)), (int)(nickContainerXY.Y + (genderBoxSize.Y - genderSize.Height) / 2));
                    if (genderXY.X < nickContainerXY.X)
                        genderXY.X = nickContainerXY.X;
                    if ((genderXY.Y + genderSize.Height) > nicknameXY.Y)
                        genderXY.Y = nickContainerXY.Y + 2;

                    textBrush = new SolidBrush(Color.Black);
                    graphics.DrawString(genderText, font, textBrush, genderXY.X, genderXY.Y);
                }

                // Draw Group ID
                var fontGothic = new PrivateFontCollection();
                fontGothic.AddFontFile(assetPath + @"\fonts\GOTHIC.TTF");
                graphics.DrawString(link.ExternalIdNo, new Font(fontGothic.Families[0], 6f, FontStyle.Regular), Brushes.White, photoXY.X, photoXY.Y + resizedQR.Height + 3);

                var country = link.LocaleCountry;
                if (country != null)
                {
                    var countryContainerSize = new Point(1080, 160); // 80
                    var countryContainerRightXY = new Point(60, photoXY.Y + 826); //840); // 1680
                    var flagLeftMargin = 15;
                    var flagContainerSize = new Point(120, 80);
                    var countryNameContainerSize = new Point(countryContainerSize.X - (flagLeftMargin + flagContainerSize.X), 164); // 160); //80
                    var countryName = string.IsNullOrEmpty(country.ShortName) ? country.CountryName.ToUpper() : country.ShortName.ToUpper();
                    var countryCode = string.IsNullOrEmpty(country.ISOCode) ? "US" : country.ISOCode;

                    // Calculate Country Name Size
                    var countryFontSize = 35f;
                    var countryFont = new Font(fontGothicBold.Families[0], countryFontSize, FontStyle.Bold);
                    var countryNameSize = graphics.MeasureString(countryName, countryFont);
                    for (int i = 0; i < 15; i++)
                    {
                        if (ImageUtil.CanFitInBox(countryNameContainerSize, new Point((int)countryNameSize.Width, (int)countryNameSize.Height), new Point(0, 0)))
                        {
                            break;
                        }
                        else
                        {
                            countryFontSize -= 2;
                            countryFont = new Font(fontGothicBold.Families[0], countryFontSize, FontStyle.Bold);
                            countryNameSize = graphics.MeasureString(countryName, countryFont);
                        }
                    }

                    // Adjust Margins
                    var countryHorizMargin = (int)(countryContainerSize.X - (countryNameSize.Width + flagLeftMargin + flagContainerSize.X)) / 2;

                    // Draw Country Name
                    graphics.DrawString(countryName, countryFont, Brushes.White,
                        countryContainerRightXY.X + countryHorizMargin,
                        countryContainerRightXY.Y + (countryNameContainerSize.Y - countryNameSize.Height) / 2); // Center Y: top 0 + country name container rem margin

                    // Draw Flag
                    using (var flag = Image.FromFile(context.Server.MapPath("~/Content/Assets/Images/flags/large/" + countryCode + ".png")))
                    {
                        if (flag != null)
                            graphics.DrawImage(flag,
                                new Rectangle(
                                    countryContainerRightXY.X + countryHorizMargin + (int)countryNameSize.Width + flagLeftMargin,
                                    countryContainerRightXY.Y + (countryContainerSize.Y - flagContainerSize.Y) / 2, // Center Y: top 0 + flag cont extra space
                                    flagContainerSize.X,
                                    flagContainerSize.Y),
                                new Rectangle(0, 0, flag.Width, flag.Height), GraphicsUnit.Pixel);
                    }

                    try
                    {
                        if (photo != null)
                            photo.Dispose();
                        qr.Dispose();
                        resizedQR.Dispose();
                    }
                    catch { }
                }
            }

            var jpgEncoder = ImageUtil.GetEncoder(ImageFormat.Jpeg);
            var ep = new EncoderParameters();
            ep.Param[0] = new EncoderParameter(Encoder.Quality, (long)100);

            var cardFilename = string.Format(@"{0}\{1} {2} {3}.jpg", savePath, link.ExternalIdNo, user.FirstName, user.LastName);
            id.Save(cardFilename, jpgEncoder, ep);

            try
            {
                id.Dispose();
            }
            catch { }

            return cardFilename;
        }
        public static DataSet Select(int groupId, string keyword)
        {
            var query = new WQuery(true);
            query.SetOpen("Attendee");
            var createdById = WSession.Current.UserId;
            var isAdmin = WSession.Current.IsAdministrator;
            var keywordL = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            var groupUsers = isAdmin ? WebUserGroup.Provider.GetByGroupId(groupId, -1) : WebUserGroup.Provider.GetByCreatedById(groupId, createdById, -1);

            return DataHelper.ToDataSet(from i in WebUser.GetList(groupId)
                                        let gu = groupUsers.FirstOrDefault(gu => i.Id == gu.UserId && (isAdmin || gu.CreatedById == createdById))
                                        let link = MemberLink.Provider.GetByUserId(i.Id)
                                        where link != null && (isAdmin || gu != null) && (string.IsNullOrEmpty(keywordL) ||
                                                (
                                                    i.UserName.ToLower().Contains(keywordL) ||
                                                    i.FullName.ToLower().Contains(keywordL) ||
                                                    i.Email.ToLower().Contains(keywordL)))
                                        let createdBy = gu.CreatedBy
                                        let country = link.LocaleCountry
                                        select new
                                        {
                                            i.Id,
                                            i.UserName,
                                            i.FirstName,
                                            i.LastName,
                                            i.Email,
                                            i.MobileNumber,
                                            i.DateCreated,
                                            i.LastUpdate,
                                            i.LastLogin,
                                            link.Nickname,
                                            MemberUrl = string.Format("/Account/?UserId={0}", i.Id),
                                            ExternalId = link.ExternalIdNo,
                                            PhotoUrl = i.GetPhotoPath("200x200"),
                                            CountryName = country == null ? "" : country.CountryName,
                                            EditUrl = query.Set(WebColumns.UserId, i.Id).BuildQuery(),
                                            CoordinatorName = createdBy == null ? "" : createdBy.FirstAndLastName,
                                            CoordinatorUrl = createdBy == null ? "#" : string.Format("/Account/?UserId={0}", createdBy.Id)
                                        });
        }

        public static DataSet SelectDownload(int groupId, string keyword)
        {
            var users = SelectDownloadRaw(groupId, keyword);
            return DataHelper.ToDataSet(
                    from i in users
                    let link = MemberLink.Provider.GetByUserId(i.Id)
                    let country = link != null ? link.LocaleCountry : null
                    let countryName = country != null ? country.CountryName : ""
                    orderby countryName
                    select new
                    {
                        i.Id,
                        ExternalID = link != null ? link.ExternalIdNo : "",
                        i.FirstName,
                        i.LastName,
                        Nickname = link != null ? link.Nickname : "",
                        i.Email,
                        i.MobileNumber,
                        Country = countryName,
                        Locale = link != null ? link.Locale : "",
                        i.Gender,
                        i.LastUpdate
                    });
        }

        public static IEnumerable<WebUser> SelectDownloadRaw(int groupId, string keyword)
        {
            var createdById = WSession.Current.UserId;
            var isAdmin = WSession.Current.IsAdministrator;
            var keywordL = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            var groupUsers = isAdmin ? WebUserGroup.Provider.GetByGroupId(groupId, -1) : WebUserGroup.Provider.GetByCreatedById(groupId, createdById, -1);

            var data = from i in WebUser.GetList(groupId)
                       let gu = groupUsers.FirstOrDefault(gu => i.Id == gu.UserId && (isAdmin || gu.CreatedById == createdById))
                       where (string.IsNullOrEmpty(keywordL) ||
                               (i.UserName.ToLower().Contains(keywordL) ||
                                   i.FullName.ToLower().Contains(keywordL) ||
                                   i.Email.ToLower().Contains(keywordL))) &&
                            (isAdmin || gu != null)
                       //let link = MemberLink.Provider.GetByUserId(i.Id)
                       //let createdBy = gu.CreatedBy
                       //let country = link.HomeAddressCountry
                       select i;
            return data;
        }
    }
}