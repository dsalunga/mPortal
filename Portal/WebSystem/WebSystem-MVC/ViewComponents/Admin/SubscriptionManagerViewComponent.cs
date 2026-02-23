using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Subscription manager. Replaces SubscriptionManager.ascx (Central/Misc).
    /// Usage: @await Component.InvokeAsync("SubscriptionManager")
    /// </summary>
    public class SubscriptionManagerViewComponent : WViewComponent
    {
        public SubscriptionManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedListId = 0)
        {
            var model = new SubscriptionManagerViewModel
            {
                SelectedListId = selectedListId,
                Lists = new List<SubscriptionListItem>(),
                Subscribers = new List<SubscriberItem>()
            };

            return View(model);
        }
    }

    public class SubscriptionManagerViewModel
    {
        public int SelectedListId { get; set; }
        public List<SubscriptionListItem> Lists { get; set; } = new List<SubscriptionListItem>();
        public List<SubscriberItem> Subscribers { get; set; } = new List<SubscriberItem>();
        public string ErrorMessage { get; set; }
    }

    public class SubscriptionListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubscriberCount { get; set; }
        public bool IsActive { get; set; }
    }

    public class SubscriberItem
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime? SubscribedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
