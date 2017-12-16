namespace WCMS.Framework.Social
{
    public class GenericWallEvent : WallUpdateEventBase
    {
        public GenericWallEvent(string wallPost, int userId = -1)
            : base()
        {
            if (userId == -1)
                userId = WSession.Current.UserId;

            _update.EventTypeId = WallEventTypes.GenericWallPost;

            _update.UpdateObjectId = WebObjects.WebUser;
            _update.UpdateRecordId = userId;

            _update.ByObjectId = WebObjects.WebUser;
            _update.ByRecordId = userId;

            _update.Content = wallPost;
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
