using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net;

namespace WCMS.Common.Utilities
{
    public static class ImageUtil
    {
        private static readonly Dictionary<string, string> ImageTypes = new Dictionary<string, string>
        {
            {".jpg", "JPEG File"},
            {".jpeg", "JPEG File"},
            {".png", "PNG File"},
            {".gif", "GIF File"},
            {".bmp", "BMP File"},
            {".pcx", "PCX File"},
            {".tiff", "TIFF File"}
        };

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
                if (codec.FormatID == format.Guid)
                    return codec;
            return null;
        }
        public static bool IsValidImage(string filename)
        {
            var ext = Path.GetExtension(filename).ToLower();

            return ImageTypes.ContainsKey(ext);
        }

        /// <summary>
        /// Maintains the aspect ratio of the thumbnail
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="targetImage"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="fixOrientation"></param>
        /// <returns></returns>
        public static bool GenerateThumbnail(string sourceImage, string targetImage, int maxWidth, int maxHeight, bool fixOrientation = false)
        {
            using (var image = Image.FromFile(sourceImage))
                return CreateThumbnail(image, targetImage, maxWidth, maxHeight, fixOrientation);
        }

        /// <summary>
        /// Uses the thumbnail sizes as is
        /// </summary>
        /// <param name="source"></param>
        /// <param name="thumbnail"></param>
        /// <param name="thumbWidth"></param>
        /// <param name="thumbHeight"></param>
        /// <param name="format"></param>
        /// <param name="fixOrientation"></param>
        /// <returns></returns>
        public static bool GenerateThumbnail(string source, string thumbnail, int thumbWidth, int thumbHeight, ImageFormat format, bool fixOrientation = false)
        {
            using (var image = new Bitmap(source))
            {
                if (fixOrientation)
                    FixOrientation(image);
                return CreateThumbnail(image, thumbnail, thumbWidth, thumbHeight, format);
            }
        }

        public static bool CreateThumbnail(Image sourceImage, string targetFile, int thumbWidth, int thumbHeight, ImageFormat format)
        {
            var callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            // Target Image
            var destImage = new Bitmap(thumbWidth, thumbHeight);
            destImage.SetResolution(96, 96); // 72, 72

            // Target Quality
            var g = Graphics.FromImage(destImage);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            // Resize the original
            g.DrawImage(sourceImage, 0, 0, thumbWidth, thumbHeight);
            g.Dispose();

            SaveToFile(destImage, targetFile, format);

            sourceImage.Dispose();
            return true;
        }

        public static void SaveToFile(Image destImage, string targetFile, long compression = 50)
        {
            SaveToFile(destImage, targetFile, destImage.RawFormat, compression);
        }

        public static ImageFormat GetImageFormat(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentException(
                    string.Format("Unable to determine file extension for fileName: {0}", fileName));

            switch (extension.ToLower())
            {
                case @".bmp":
                    return ImageFormat.Bmp;

                case @".gif":
                    return ImageFormat.Gif;

                case @".ico":
                    return ImageFormat.Icon;

                case @".jpg":
                case @".jpeg":
                    return ImageFormat.Jpeg;

                case @".png":
                    return ImageFormat.Png;

                case @".tif":
                case @".tiff":
                    return ImageFormat.Tiff;

                case @".wmf":
                    return ImageFormat.Wmf;

                default:
                    return null;
            }
        }

        public static void UpdateExifThumbnail(Image Pic)
        {
            #region Create a new standard thumbnail property.
            //Max of 160x160 in same aspect ratio as main image
            //Image SmallThumb = CreateThumbnail( //tlhintoq.GDI.Graphics.CalculatedThumbnail(NewThumbnail, 160, 160);
            /*MemoryStream MS = new MemoryStream();
            SmallThumb.Save(MS, ImageFormat.Jpeg);
            SmallThumb.Dispose();
            MS.Position = 0;
            byte[] smallthumbbytes = MS.ToArray();

            var PropertyItems = Pic.PropertyItems;
            PropertyItems[0].Id = 0x501b; // PropertyTage ThumbnailData
            PropertyItems[0].Type = 1;
            PropertyItems[0].Len = smallthumbbytes.Length;
            PropertyItems[0].Value = smallthumbbytes;
            Pic.SetPropertyItem(PropertyItems[0]);

            #region Create a new Thumbnail Compression property.
            PropertyItems = Pic.PropertyItems;
            PropertyItems[0].Id = 0x5023; // PropertyTagThumbnailCompression
            PropertyItems[0].Type = (short)ImagePropertyTagValueTypes.ArrayOfUnisignedShort3;
            PropertyItems[0].Len = 2;
            PropertyItems[0].Value = new byte[] { 6, 0 };
            Pic.SetPropertyItem(PropertyItems[0]); */
            #endregion
        }

        public static void SaveToFile(Image destImage, string targetFile, ImageFormat format, long compression = 50)
        {
            var codecEncoder = GetEncoder(format);

            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, compression);          // 100% Percent Compression (Lowest quality)
            //encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100);          // Opposite of Compression

            using (var memStream = new MemoryStream())
            {
                destImage.Save(memStream, codecEncoder, encoderParameters);   // jpg format
                destImage.Dispose();

                byte[] matriz = memStream.ToArray();
                using (var fileStream = new FileStream(targetFile, FileMode.Create, FileAccess.ReadWrite))
                    fileStream.Write(matriz, 0, matriz.Length);
            }
        }

        public static bool CreateThumbnail(Image imageSource, string thumbnailImage, int maxWidth, int maxHeight, bool fixOrientation = false)
        {
            int thumbWidth = maxWidth;
            int thumbHeight = maxHeight;

            if (fixOrientation)
                FixOrientation(imageSource);

            double ratioWidth = (double)imageSource.Width / maxWidth;
            double ratioHeight = (double)imageSource.Height / maxHeight;

            if (ratioWidth > ratioHeight)
                thumbHeight = (imageSource.Height * maxWidth) / imageSource.Width;
            else
                thumbWidth = (imageSource.Width * maxHeight) / imageSource.Height;

            return ImageUtil.CreateThumbnail(imageSource, thumbnailImage, thumbWidth, thumbHeight, ImageFormat.Jpeg);
        }

        public static bool GenerateThumbnailW(string sourceImage, string thumbnailImage, int width)
        {
            using (Image imageSource = new Bitmap(sourceImage))
            {
                int height = (int)((double)imageSource.Height * width) / imageSource.Width; ;
                return ImageUtil.GenerateThumbnail(sourceImage, thumbnailImage, width, height, ImageFormat.Jpeg);
            }
        }
        public static bool GenerateThumbnailH(string sourceImage, string thumbnailImage, int height)
        {
            using (Image imageSource = new Bitmap(sourceImage))
            {
                int iWidth = (int)((double)imageSource.Width * height) / imageSource.Height;
                return ImageUtil.GenerateThumbnail(sourceImage, thumbnailImage, iWidth, height, ImageFormat.Jpeg);
            }
        }

        public static bool CanFitInBox(Point container, Point box, Point margin)
        {
            return !(container.X < (box.X + margin.X * 2) || container.Y < (box.Y + margin.Y * 2));
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        public static Image FixOrientation(Image image)
        {
            if (image.PropertyIdList.Contains(0x0112))
            {
                int rotationValue = image.GetPropertyItem(0x0112).Value[0];
                switch (rotationValue)
                {
                    case 1: // landscape, do nothing
                        break;

                    case 8: // rotated 90 right
                        // de-rotate:
                        image.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                        break;

                    case 3: // bottoms up
                        image.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                        break;

                    case 6: // rotated 90 left
                        image.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                        break;
                }
            }

            return image;
        }

        public static Image FixOrientation(string fileName)
        {
            var image = Image.FromFile(fileName);
            return FixOrientation(image);
        }

        public static Image GetImageFromUrl(string url)
        {
            try
            {
                if (url.Contains("://"))
                {
                    var client = new WebClient();
                    var bytes = client.DownloadData(url);
                    using (var ms = new MemoryStream(bytes))
                        return Image.FromStream(ms);
                }
                else
                {
                    return Image.FromFile(WebHelper.MapPath(url));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }

            return null;
        }

        private static bool ThumbnailCallback()
        {
            return false;
        }
    }
}
