using System;
using System.IO;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public class WebAttachment : NamedWebObject, ISelfManager
    {
        private static IWebAttachmentProvider _provider;

        static WebAttachment()
        {
            _provider = WebObject.ResolveProvider<WebAttachment, IWebAttachmentProvider>();
        }

        public WebAttachment()
        {
            FilePath = string.Empty;
            BatchGuid = string.Empty;

            DateUploaded = WConstants.DateTimeMinValue;
            Size = 0;

            UserId = -1;
            ObjectId = -1;
            RecordId = -1;
        }

        public string FilePath { get; set; }
        public Int64 Size { get; set; }
        public DateTime DateUploaded { get; set; }
        public int UserId { get; set; }
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string BatchGuid { get; set; }

        public string SizeString { get { return FileHelper.GetSizeString(Size); } }
        public string Extension { get { return Path.GetExtension(Name); } }
        public string AbsPath { get { return WebHelper.MapPath(FilePath); } }
        public WebUser User { get { return WebUser.Get(UserId); } }

        public override int OBJECT_ID { get { throw new NotImplementedException(); } }

        public static IWebAttachmentProvider Provider { get { return _provider; } }

        public void Download()
        {
            WebHelper.DownloadFile(this.FilePath, this.Name);
        }

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            var absPath = AbsPath;
            if (File.Exists(absPath))
                File.Delete(absPath);

            return _provider.Delete(this.Id);
        }

        public bool Refresh()
        {
            throw new NotImplementedException();
        }

        #endregion

        public static void MarkPermanent(string batchGuid, int recordId)
        {
            var items = _provider.GetList(batchGuid);
            foreach (var item in items)
            {
                var newFilePath = item.FilePath.Replace(
                    string.Format("/O-1-{0}", item.BatchGuid),
                    string.Format("/O{0}-{1}", recordId, item.BatchGuid));

                var absPath = item.AbsPath;
                if (File.Exists(absPath))
                    File.Move(absPath, WebHelper.MapPath(newFilePath));

                item.FilePath = newFilePath;
                item.RecordId = recordId;
                item.Update();
            }
        }

        public static WebAttachment Upload(FileUpload fileUpload, int objectId, int recordId, string batchGuid)
        {
            if (fileUpload != null && fileUpload.HasFile)
            {
                var fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                var ext = Path.GetExtension(fileName);
                var fileNameWE = Path.GetFileNameWithoutExtension(fileName);
                var newFileName = string.Format("O{0}-{1}-{2}{3}", recordId, batchGuid,
                    fileNameWE.Length > 50 ? fileNameWE.Substring(0, 50) : fileNameWE, ext);
                var filePath = WebHelper.CombineAddress(
                    string.Format("{0}/{1}", WConfig.AttachmentBasePath, objectId), newFileName);
                var absFilePath = WebHelper.MapPath(filePath);

                var absFolder = FileHelper.GetFolder(absFilePath, '\\');
                if (!Directory.Exists(absFolder))
                    Directory.CreateDirectory(absFolder);

                WebAttachment item = new WebAttachment();
                item.Name = fileName;
                item.FilePath = filePath;
                item.Size = fileUpload.PostedFile.ContentLength;
                item.UserId = WSession.Current.UserId;
                item.ObjectId = objectId;
                item.RecordId = recordId;
                item.BatchGuid = batchGuid;

                // Upload the file
                fileUpload.PostedFile.SaveAs(absFilePath);

                item.DateUploaded = DateTime.Now;
                item.Update();

                return item;
            }

            return null;
        }

    }
}
