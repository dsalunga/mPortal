namespace WCMS.Common
{
    public interface INamedObjectProvider
    {
        object GetValue(string key);
        bool ContainsKey(string key);
        object this[string key] { get; set; }
        void Remove(string key);
    }
}
