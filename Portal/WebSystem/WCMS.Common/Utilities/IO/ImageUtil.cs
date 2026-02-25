using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Processing;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Cross-platform image utilities using SixLabors.ImageSharp.
    /// Replaces System.Drawing.Common which is not supported on Linux.
    /// </summary>
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

        public static bool IsValidImage(string filename)
        {
            var ext = Path.GetExtension(filename).ToLower();
            return ImageTypes.ContainsKey(ext);
        }

        /// <summary>
        /// Maintains the aspect ratio of the thumbnail.
        /// </summary>
        public static bool GenerateThumbnail(string sourceImage, string targetImage, int maxWidth, int maxHeight, bool fixOrientation = false)
        {
            using var image = Image.Load(sourceImage);
            if (fixOrientation)
                FixOrientation(image);

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(maxWidth, maxHeight),
                Mode = ResizeMode.Max
            }));

            SaveToFile(image, targetImage);
            return true;
        }

        /// <summary>
        /// Uses the thumbnail sizes as is (no aspect ratio maintenance).
        /// </summary>
        public static bool GenerateThumbnailExact(string source, string thumbnail, int thumbWidth, int thumbHeight, bool fixOrientation = false)
        {
            using var image = Image.Load(source);
            if (fixOrientation)
                FixOrientation(image);

            image.Mutate(x => x.Resize(thumbWidth, thumbHeight));
            SaveToFile(image, thumbnail);
            return true;
        }

        public static bool GenerateThumbnailW(string sourceImage, string thumbnailImage, int width)
        {
            using var image = Image.Load(sourceImage);
            int height = (int)((double)image.Height * width / image.Width);

            image.Mutate(x => x.Resize(width, height));
            SaveToFile(image, thumbnailImage);
            return true;
        }

        public static bool GenerateThumbnailH(string sourceImage, string thumbnailImage, int height)
        {
            using var image = Image.Load(sourceImage);
            int width = (int)((double)image.Width * height / image.Height);

            image.Mutate(x => x.Resize(width, height));
            SaveToFile(image, thumbnailImage);
            return true;
        }

        /// <summary>
        /// Scales an image to fit within the given dimensions while maintaining aspect ratio.
        /// Returns the resized image as a new Image instance (caller owns disposal).
        /// </summary>
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var clone = image.Clone(x => x.Resize(new ResizeOptions
            {
                Size = new Size(maxWidth, maxHeight),
                Mode = ResizeMode.Max
            }));
            return clone;
        }

        /// <summary>
        /// Auto-rotates based on EXIF orientation tag, then removes the tag.
        /// </summary>
        public static void FixOrientation(Image image)
        {
            image.Mutate(x => x.AutoOrient());
        }

        /// <summary>
        /// Loads an image from file, auto-orients it, and returns it.
        /// </summary>
        public static Image FixOrientation(string fileName)
        {
            var image = Image.Load(fileName);
            image.Mutate(x => x.AutoOrient());
            return image;
        }

        /// <summary>
        /// Saves the image to the specified file. Format is inferred from the file extension.
        /// Quality defaults to JPEG 75.
        /// </summary>
        public static void SaveToFile(Image image, string targetFile, int quality = 75)
        {
            var encoder = GetEncoderForFile(targetFile, quality);
            image.Save(targetFile, encoder);
        }

        /// <summary>
        /// Returns the appropriate IImageEncoder for the given filename extension.
        /// </summary>
        public static IImageEncoder GetEncoderForFile(string fileName, int quality = 75)
        {
            string extension = Path.GetExtension(fileName)?.ToLower();
            return extension switch
            {
                ".jpg" or ".jpeg" => new JpegEncoder { Quality = quality },
                ".png" => new PngEncoder(),
                ".gif" => new GifEncoder(),
                ".bmp" => new BmpEncoder(),
                ".tif" or ".tiff" => new TiffEncoder(),
                _ => new JpegEncoder { Quality = quality }
            };
        }

        /// <summary>
        /// Downloads an image from a URL or loads from a local path.
        /// Returns null on failure.
        /// </summary>
        public static Image GetImageFromUrl(string url)
        {
            try
            {
                if (url.Contains("://"))
                {
                    using var client = new HttpClient();
                    var bytes = client.GetByteArrayAsync(url).GetAwaiter().GetResult();
                    return Image.Load(bytes);
                }
                else
                {
                    var mappedPath = WebUtil.MapPath(url);
                    return Image.Load(mappedPath);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }

            return null;
        }
    }
}
