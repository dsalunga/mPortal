using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Apps.Integration
{
    public class QRCodeUtil
    {
        public static string EncodeServiceSchedule(int serviceScheduleId)
        {
            var code = DateTime.Now.Minute.ToString("D2").Replace("0", "9") + serviceScheduleId + DateTime.Now.Second.ToString("D2").Replace("0", "9");
            code = String.Format("{0:X}", DataHelper.GetInt64(DataHelper.ReverseString(code)));
            return code;
        }

        public static int DecodeServiceSchedule(string hex)
        {
            var number = DataHelper.ReverseString(Convert.ToInt64(hex, 16).ToString());
            return DataUtil.GetInt32(number.Substring(2, number.Length - 4));
        }

        public static string BuildQRCodeString(string encodedServiceSchedule)
        {
            return string.Format("https://someorg.org/a?s={0}", encodedServiceSchedule);
        }

        public static System.Drawing.Image CreateQRCode(string s, int width, int height)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCode = qrGenerator.CreateQrCode(s, QRCodeGenerator.ECCLevel.Q);
            var qr = qrCode.GetGraphic(20);

            var qrCropPx = 50;
            var newSize = new Point(qr.Width - qrCropPx * 2, qr.Height - qrCropPx * 2);
            var target = new Bitmap(newSize.X, newSize.Y);
            using (var graphics = Graphics.FromImage(target))
            {
                // Crop and draw QR code
                var cropRect = new Rectangle(qrCropPx, qrCropPx, newSize.X, newSize.Y);
                graphics.DrawImage(qr, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
            }

            var resizedQR = ImageUtil.ScaleImage(target, width, height);
            return resizedQR;
        }

        public static Image CreateQRCode(int serviceScheduleId, int width, int height)
        {
            var code = BuildQRCodeString(EncodeServiceSchedule(serviceScheduleId));
            return CreateQRCode(code, width, height);
        }
    }
}
