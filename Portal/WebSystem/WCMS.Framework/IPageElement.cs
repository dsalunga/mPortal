namespace WCMS.Framework
{
    public interface IPageElement : IWebObject
    {
        int Active { get; set; }
        bool IsActive { get; }
        int MasterPageId { get; }
        WebMasterPage MasterPage { get; }
        int PartControlTemplateId { get; set; }
        WebPartControlTemplate PartControlTemplate { get; }
        int Rank { get; set; }
        string Name { get; set; }
        //WebObjectContent ObjectContent { get; }
        int TemplatePanelId { get; }
        WebTemplatePanel Panel { get; }
        int UsePartTemplatePath { get; set; }
        WSite Site { get; }
    }
}
