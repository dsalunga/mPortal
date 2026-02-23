namespace WCMS.Framework
{
    /// <summary>
    /// DI-compatible interface for WSession. Register as scoped in ASP.NET Core DI.
    /// Replaces static WSession.Current accessor for .NET 10 cross-platform compatibility.
    /// </summary>
    public interface IWSession
    {
        int UserId { get; set; }
        bool IsLoggedIn { get; }
        bool IsAdministrator { get; }
        bool IsSiteManager { get; }
        int UserType { get; }
        WebUser User { get; }
        int InDesign { get; set; }
        bool IsInDesign { get; }

        void Login(int userId, bool rememberLogin = false);
        void Logout();
        void Update();
    }
}
