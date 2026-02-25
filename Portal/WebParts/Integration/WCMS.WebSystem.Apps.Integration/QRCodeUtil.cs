using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Apps.Integration
{
    public class QRCodeUtil
    {
        public static string EncodeServiceSchedule(int serviceScheduleId)
        {
            var code = DateTime.Now.Minute.ToString("D2").Replace("0", "9") + serviceScheduleId + DateTime.Now.Second.ToString("D2").Replace("0", "9");
            code = String.Format("{0:X}", DataUtil.GetInt64(DataUtil.ReverseString(code)));
            return code;
        }

        public static int DecodeServiceSchedule(string hex)
        {
            var number = DataUtil.ReverseString(Convert.ToInt64(hex, 16).ToString());
            return DataUtil.GetInt32(number.Substring(2, number.Length - 4));
        }

        public static string BuildQRCodeString(string encodedServiceSchedule)
        {
            return string.Format("https://someorg.org/a?s={0}", encodedServiceSchedule);
        }

        public static Image CreateQRCode(string s, int width, int height)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(s, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);

            var qr = Image.Load<Rgba32>(qrCodeBytes);

            // Crop the QR code (remove border padding)
            var qrCropPx = 50;
            var cropRect = new Rectangle(qrCropPx, qrCropPx, qr.Width - qrCropPx * 2, qr.Height - qrCropPx * 2);
            qr.Mutate(x => x.Crop(cropRect));

            // Scale to requested size
            qr.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Max
            }));

            return qr;
        }

        public static Image CreateQRCode(int serviceScheduleId, int width, int height)
        {
            var code = BuildQRCodeString(EncodeServiceSchedule(serviceScheduleId));
            return CreateQRCode(code, width, height);
        }
    }
}
