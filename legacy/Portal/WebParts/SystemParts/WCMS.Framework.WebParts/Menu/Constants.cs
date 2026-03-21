using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.Menu
{
    public struct MenuConstants
    {
        public const int Menu_ObjectId = 59;
        public const int MenuItem_ObjectId = 60;
    }

    public struct GenericMenuConstants
    {
        public const string UpOneLevelKey = "UpOneLevel";
        public const string SeeAlsoKey = "SeeAlso";
    }

    public struct RenderModes
    {
        public const int Absolute = 0;
        public const int Relative = 1;
    }

    public struct MenuItemTypes
    {
        public const int Container = 0;
        public const int MenuItem = 1;
    }

    public struct ItemPositions
    {
        public const int Before = 0;
        public const int After = 1;
        public const int Child = 2;
    }

    public class ParameterKeys
    {
        public const string ItemTemplate = "ItemTemplate";
        public const string FirstItemTemplate = "FirstItemTemplate";
        public const string SelectedItemTemplate = "SelectedItemTemplate";
        public const string Header = "Header";
        public const string BodyTemplate = "BodyTemplate";
        public const string Footer = "Footer";
        public const string Separator = "Separator";
        public const string SeparatorItem = "SeparatorItem";
        public const string HeaderItem = "HeaderItem";

        public const string UpOneLevelTemplate = "UpOneLevelTemplate";
        public const string SeeAlsoTemplate = "SeeAlsoTemplate";

        public const string AlternateItemTemplate = "AlternateItemTemplate";
        public const string AlternateParentItemTemplate = "AlternateParentItemTemplate";
        public const string AlternateRootItemTemplate = "AlternateRootItemTemplate";

        public const string ParentItemTemplate = "ParentItemTemplate";
        public const string SelectParentItemTemplate = "SelectedParentItemTemplate";
        public const string SelectedRootItemTemplate = "SelectedRootItemTemplate";
        public const string RootItemTemplate = "RootItemTemplate";
        public const string RootItemNoChildrenTemplate = "RootItemNoChildrenTemplate";
        public const string ContainerTemplate = "ContainerTemplate";

        public const string MenuId = "MenuId";
    }

    public class TemplateKeys
    {
        public const string Url = "Url";
        public const string Target = "Target";
        public const string TargetProperty = "TargetProperty";
        public const string Text = "Text";
    }

    public class ContextKeys
    {
        public const string Children = "Children";
        public const string Content = "Content";
    }
}
