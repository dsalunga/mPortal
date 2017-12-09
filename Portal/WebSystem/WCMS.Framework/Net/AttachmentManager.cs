using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

namespace WCMS.Framework.Net
{
    public class AttachmentManager
    {
        public static List<LinkedResource> GenerateAttachementsFromPath(string localPath)
        {
            List<LinkedResource> attachments = new List<LinkedResource>();
            IEnumerable<string> linkedFiles = Directory.GetFiles(localPath);

            // Remove unwanted files
            linkedFiles = linkedFiles.Where(delegate(string linkedFile)
            {
                string ext = Path.GetExtension(linkedFile);
                return (ext.Equals(".png") || ext.Equals(".jpg") || ext.Equals(".gif") || ext.Equals(".bmp"));
            });

            foreach (string linkedFile in linkedFiles)
            {
                LinkedResource attachment = new LinkedResource(linkedFile);
                attachment.ContentId = Path.GetFileName(linkedFile);
                attachment.TransferEncoding = TransferEncoding.Base64;
                attachments.Add(attachment);
            }

            return attachments;
        }

        public static bool GenerateAttachments(MailMessage mail, string localPath)
        {
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");
            var attachments = GenerateAttachementsFromPath(localPath);
            foreach (var attachment in attachments)
            {
                htmlView.LinkedResources.Add(attachment);
            }

            mail.AlternateViews.Add(htmlView);

            return true;
        }

        public static AlternateView GenerateAlternateView(string msgBody, string localPath)
        {
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(msgBody, null, "text/html");
            var attachments = GenerateAttachementsFromPath(localPath);
            foreach (var attachment in attachments)
            {
                htmlView.LinkedResources.Add(attachment);
            }

            return htmlView;
        }
    }
}
