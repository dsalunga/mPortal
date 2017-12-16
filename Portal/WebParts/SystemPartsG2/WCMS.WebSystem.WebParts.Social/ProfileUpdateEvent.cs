namespace WCMS.Framework.Social
{
    public class ProfileUpdateEvent : WallUpdateEventBase
    {
        public ProfileUpdateEvent(int userId)
            : base()
        {
            _update.EventTypeId = WallEventTypes.StatusUpdate;

            _update.UpdateObjectId = WebObjects.WebUser;
            _update.UpdateRecordId = userId;

            _update.ByObjectId = WebObjects.WebUser;
            _update.ByRecordId = userId;
        }

        //public string ToStatusString()
        //{
        //    WebUser user = WebUser.Get(_update.UpdateRecordId);
        //    if (user != null)
        //    {
        //        var genderAddr = user.Gender == GenderTypes.Male ? "his" : user.Gender == GenderTypes.Female ? "her" : "his/her";

        //        return string.Format("{0} updated {1} profile.", user.FirstAndLastName, genderAddr);
        //    }

        //    return string.Empty;
        //}
    }
}
