namespace WCMS.Framework.Social
{
    public abstract class WallUpdateEventBase : IWallUpdateEvent
    {
        protected WallUpdate _update;

        public WallUpdate WallUpdate { get { return _update; } }

        protected WallUpdateEventBase()
        {
            _update = new WallUpdate();
        }

        public WallUpdateEventBase(WallUpdate update)
        {
            this._update = update;
        }

        public int Update()
        {
            return _update.Update();
        }
    }
}
