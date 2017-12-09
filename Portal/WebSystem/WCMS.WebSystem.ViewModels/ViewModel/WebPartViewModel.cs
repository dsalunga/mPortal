using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;

namespace WCMS.WebSystem.ViewModel
{
    public class WebPartViewModel
    {
        public static TreeNode GenerateModuleChooserTree(int templateId, string hTemplateClientId, bool showAll = true)
        {
            IEnumerable<WPart> parts = null;
            if (WSession.Current.IsAdministrator)
                parts = WPart.GetList();
            else
                parts = WPart.GetPermissibleList(WSession.Current.UserId);

            WebPartControlTemplate currentTemplate = WebPartControlTemplate.Get(templateId);
            if (currentTemplate == null)
                currentTemplate = parts.First().Controls.First().Templates.First();

            var currentPart = currentTemplate.Part;
            

            TreeNode nodeRoot = new TreeNode("<strong>Web Parts</strong>");
            nodeRoot.SelectAction = TreeNodeSelectAction.Expand;
            nodeRoot.Expand();

            foreach (WPart part in parts)
            {
                if (part.IsActive && (part.Id != WConstants.CentralPartId || showAll || currentPart.Id == part.Id))
                {
                    // WebParts-level
                    TreeNode nodePart = new TreeNode(part.Name);
                    nodePart.SelectAction = TreeNodeSelectAction.Expand;

                    var controls = part.Controls; //.OrderBy(i=>i.Name);
                    if (controls.Count() > 0)
                    {
                        foreach (WebPartControl control in controls)
                        {
                            // WebPartControl-level
                            if (showAll || control.IsEntryPoint || currentTemplate.PartControlId == control.Id)
                            {
                                TreeNode nodeControl = new TreeNode(control.Name);
                                nodeControl.SelectAction = TreeNodeSelectAction.Expand;

                                var templates = control.Templates;
                                foreach (WebPartControlTemplate template in templates)
                                {
                                    // WebPartControlTemplate-level
                                    TreeNode nodeTemplate = new TreeNode(template.Name, template.Id.ToString());
                                    nodeTemplate.SelectAction = TreeNodeSelectAction.None;
                                    nodeTemplate.Text = string.Format("<a href=\"#\" onclick=\"ViewTemplate('{0}','{1}');return false;\" class=\"module_treenode\"><strong>{2}<strong></a>",
                                        template.Id, hTemplateClientId, template.TemplateEngineId == TemplateEngineTypes.Razor ? template.Name : template.Name + " (ASCX)");

                                    nodeControl.ChildNodes.Add(nodeTemplate);
                                }

                                if (templates.Count() > 0)
                                {
                                    if (currentTemplate.PartControlId == control.Id)
                                        nodeControl.Expand();

                                    nodePart.ChildNodes.Add(nodeControl);
                                }
                            }
                        }

                        if (currentTemplate.PartControl.PartId == part.Id)
                            nodePart.Expand();

                        nodeRoot.ChildNodes.Add(nodePart);
                    }
                }
            }
            return nodeRoot;
        }
    }
}
