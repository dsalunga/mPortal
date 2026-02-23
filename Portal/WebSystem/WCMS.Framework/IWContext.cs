using WCMS.Framework.Core;

namespace WCMS.Framework
{
    /// <summary>
    /// DI-compatible interface for WContext. Register as scoped in ASP.NET Core DI.
    /// Replaces WContext.GetInstance() and HttpContext.Current dependencies.
    /// </summary>
    public interface IWContext
    {
        IWSession Session { get; }
        int RecordId { get; set; }
        int ObjectId { get; set; }
        int PartControlId { get; set; }
        int PartAdminId { get; set; }
        int ContextType { get; }
        int PageId { get; }
        WPage Page { get; }
        WSite Site { get; }
        WQuery Query { get; set; }
        PageElementBase Element { get; }
        string BasePath { get; set; }

        string this[string key] { get; set; }
        string Get(string key);
        int GetId(string key);
        int GetInt32(string key);
        WContext Set(string name, object value);
    }
}
