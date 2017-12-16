using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Social;

namespace WCMS.WebSystem.WebParts.Social.ViewModel
{
    public static class EventViewModelHelper
    {
        public static WallEventViewModel CreateViewModel(WallUpdate update)
        {
            var plugin = WallPlugin.Provider.GetByEventType(update.EventTypeId);
            if (plugin != null)
            {
                WallEventViewModel item = (WallEventViewModel)Activator.CreateInstance(plugin.TypeObject);
                if (item != null)
                {
                    item.WallUpdate = update;
                    return item;
                }

                //case WallEventTypes.StatusUpdate:
                //    return new ProfileUpdateEventViewModel(update);

                //case WallEventTypes.ArticleUpdate:
                //    return new ArticleUpdateEventViewModel(update);

                //case WallEventTypes.GenericWallPost:
                //    return new GenericWallPostEventViewModel(update);
            }

            return null;
        }
    }
}
