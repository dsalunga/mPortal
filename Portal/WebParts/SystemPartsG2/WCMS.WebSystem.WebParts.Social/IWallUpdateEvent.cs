namespace WCMS.Framework.Social
{
    public interface IWallUpdateEvent
    {
        //string ToStatusString();
        int Update();
        WallUpdate WallUpdate { get; }
    }
}
