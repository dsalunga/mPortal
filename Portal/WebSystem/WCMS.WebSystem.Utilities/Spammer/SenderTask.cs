using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Utilities.Spammer
{
    public class SenderTask : AgentTaskBase
    {
        public override void Execute()
        {
            Console.WriteLine("[{0}] {1} Execution started.", TaskName, DateTime.Now);

            try
            {
                string messageTemplate = Attributes["MessageTemplate"];
                string messageTemplatePath = Attributes["MessageTemplatePath"];
                string recipientEmailKey = Attributes["RecipientEmailKey"];
                string replyTo = Attributes["ReplyTo"];
                string subject = Attributes["Subject"];

                string sourceXmlPath = Attributes["SourceXmlPath"];
                string sourceXPath = Attributes["SourceXPath"];
                string sourceXmlData = Attributes["SourceXmlData"];

                //var valueKeys = DataHelper.ParseDelimitedStringToList(Attributes["ValueKeys"], ',');
                int sendInterval = DataHelper.GetInt32(Attributes["SendInterval"], 500); // In milliseconds


                if (!string.IsNullOrEmpty(sourceXPath) && ((!string.IsNullOrWhiteSpace(sourceXmlPath)) || (!string.IsNullOrEmpty(sourceXmlData))))
                {
                    var xdoc = new XmlDocument();

                    if (!string.IsNullOrEmpty(sourceXmlData))
                    {
                        xdoc.LoadXml(sourceXmlData);
                    }
                    else
                    {
                        var sourceXmlFileEval = FileHelper.EvalPath(sourceXmlPath);
                        if (File.Exists(sourceXmlFileEval))
                            xdoc.Load(FileHelper.EvalPath(sourceXmlFileEval));
                        else
                            throw new Exception("SourceXmlPath does not exist: " + sourceXmlPath);
                    }

                    XmlNodeList nodes = xdoc.SelectNodes(sourceXPath);
                    if (nodes.Count > 0)
                    {
                        if (string.IsNullOrEmpty(messageTemplate) && !string.IsNullOrEmpty(messageTemplatePath))
                        {
                            var messageTemplatePathEval = FileHelper.EvalPath(messageTemplatePath);
                            if (File.Exists(messageTemplatePathEval))
                                messageTemplate = FileHelper.ReadFile(messageTemplatePathEval);
                            else
                                throw new Exception("MessageTemplatePath does not exist: " + messageTemplatePath);
                        }

                        if (string.IsNullOrEmpty(messageTemplate))
                            throw new Exception("MessageTemplate is empty");

                        // Loop all recipients, build email message and send
                        foreach (XmlNode node in nodes)
                        {
                            var values = new NamedValueProvider(XmlUtil.GetValues(node));

                            string recipientEmail = values.GetValue(recipientEmailKey);
                            if (!string.IsNullOrWhiteSpace(recipientEmail))
                            {
                                try
                                {
                                    // Build email message

                                    var emailMessage = Substituter.Substitute(messageTemplate, values);
                                    using (WebMailMessage email = new WebMailMessage())
                                    {
                                        if (!string.IsNullOrWhiteSpace(replyTo))
                                        {
                                            email.ReplyToList.Clear();
                                            email.ReplyToList.Add(replyTo);
                                        }

                                        email.To.Add(recipientEmail);
                                        email.Body = emailMessage;
                                        email.Subject = Substituter.Substitute(subject, values);
                                        email.Send();

                                        Console.WriteLine("[{0}] {1} Email sent to: {2} ", TaskName, DateTime.Now, recipientEmail);
                                    }

                                    if (sendInterval > 0)
                                    {
                                        Thread.Sleep(sendInterval);
                                        Console.WriteLine("[{0}] {1} Sleeping for {2}ms...", TaskName, DateTime.Now, sendInterval);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogHelper.WriteLog(ex);

                                    Console.WriteLine("[{0}] {1} Error encountered while preparing or sending email:", TaskName, DateTime.Now);
                                    Console.WriteLine(ex);
                                }
                            }
                            else
                            {
                                Console.WriteLine("[{0}] {1} Recipient email is empty.", TaskName, DateTime.Now);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("[{0}] {1} SourceXml does not contain any node or Xpath is invalid: {2}.", TaskName, DateTime.Now, sourceXPath);
                    }
                }
                else
                {
                    Console.WriteLine("[{0}] {1} SourceXmlFile|SourceXmlData|SourceXPath is invalid or missing: {2}.", TaskName, DateTime.Now, sourceXmlPath);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                Console.WriteLine("[{0}] {1} An error has encountered:", TaskName, DateTime.Now);
                Console.WriteLine(ex);
            }

            Console.WriteLine("[{0}] {1} Execution completed.", TaskName, DateTime.Now);
        }
    }
}
