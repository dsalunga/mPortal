using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Utilities.EmailUpdater
{
    public class EmailUpdaterTask : AgentTaskBase
    {
        public override void Execute()
        {
            Console.WriteLine("[{0}] {1} Execution started.", TaskName, DateTime.Now);

            try
            {
                string sourceXmlFile = Attributes["SourceXmlFile"];
                string sourceXPath = Attributes["SourceXPath"];

                if (!string.IsNullOrWhiteSpace(sourceXmlFile) && File.Exists(sourceXmlFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(sourceXmlFile);

                    XmlNodeList nodes = xdoc.SelectNodes(sourceXPath);
                    if (nodes.Count > 0)
                    {
                        var users = WebUser.GetList();

                        // Loop all recipients, build email message and send
                        foreach (XmlNode node in nodes)
                        {
                            try
                            {
                                // Migrate the emails

                                string oldEmail = XmlUtil.GetNodeText(node, "OldEmail");
                                string newEmail = XmlUtil.GetNodeText(node, "NewEmail");
                                string userFullName = XmlUtil.GetNodeText(node, "Name");

                                if (!string.IsNullOrWhiteSpace(oldEmail) && !string.IsNullOrWhiteSpace(newEmail) && !string.IsNullOrWhiteSpace(userFullName))
                                {
                                    var user = users.FirstOrDefault(u => u.Email.Equals(oldEmail, StringComparison.InvariantCultureIgnoreCase));
                                    if (user != null)
                                    {
                                        Console.WriteLine("[{0}] {1} Updating \"{2}, {3}\" to {4}", TaskName, DateTime.Now, userFullName, oldEmail, newEmail);

                                        user.Email2 = user.Email;
                                        user.Email = newEmail;
                                        user.Update();
                                    }
                                    else
                                    {
                                        Console.WriteLine("[{0}] {1} \"{2}, {3}\" not found, skipping...", TaskName, DateTime.Now, userFullName, oldEmail);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                LogHelper.WriteLog(ex);

                                Console.WriteLine("[{0}] {1} Error encountered while preparing or sending email:", TaskName, DateTime.Now);
                                Console.WriteLine(ex);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("[{0}] {1} SourceXmlFile does not contain any node or Xpath is invalid: {2}.", TaskName, DateTime.Now, sourceXPath);
                    }
                }
                else
                {
                    Console.WriteLine("[{0}] {1} SourceXmlFile is invalid or missing: {2}.", TaskName, DateTime.Now, sourceXmlFile);
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
