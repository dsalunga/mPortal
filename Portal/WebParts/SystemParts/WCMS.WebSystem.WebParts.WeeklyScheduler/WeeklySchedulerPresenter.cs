using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Common;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.WeeklyScheduler
{
    public class WeeklySchedulerPresenter : TemplatePresenterBase
    {
        private WeeklySchedulerPresenter() { }

        public WeeklySchedulerPresenter(string xmlPath)
            : base(xmlPath)
        {

        }

        public string Render()
        {
            StringBuilder sbContent = new StringBuilder();


            sbContent.Append(GetSection("Styles"));

            #region Container



            NamedValueProvider provider = new NamedValueProvider();
            StringBuilder sbBody = new StringBuilder();

            // MAIN HEADER
            {
                // DAY-CONTAINER
                StringBuilder sbDayHeader = new StringBuilder();
                string dayHeaderTemplate = GetSection("DAY-HEADER");
                string[] testDays = { "Wednesday", "Thursday", "Friday" };
                foreach (var testDay in testDays)
                {
                    TemplateFragment fragment = new TemplateFragment(dayHeaderTemplate);
                    fragment.Add("DAY-LABEL", testDay);
                    sbDayHeader.Append(fragment.ToString());
                }
                provider.Add("DAY-CONTAINER", sbDayHeader.ToString());

                // DATE-CONTAINER
                StringBuilder sbDateHeader = new StringBuilder();
                string dateHeaderTemplate = GetSection("DATE-HEADER");
                var testDates = new int[] { 24, 27, 28 };
                foreach (var testDate in testDates)
                {
                    TemplateFragment fragment = new TemplateFragment(dateHeaderTemplate);
                    fragment.Add("DATE-LABEL", testDate.ToString());
                    sbDateHeader.Append(fragment.ToString());
                }
                provider.Add("DATE-CONTAINER", sbDateHeader.ToString());

                provider.Add("DATE-RANGE", "November 24, 27-28, 2010");
                provider.Add("TOPIC-HEADER", GetSection("TOPIC-HEADER"));

                // HEADER + ITEM-HEADER
                sbBody.Append(Substituter.Substitute(GetSection("HEADER"), provider));
            }

            // ITEMS
            {
                StringBuilder sbItems = new StringBuilder();
                string[] tempTopics = { "TOPIC", "DECLAMATION & TULA", "VERSE MEMORIZATION", "BIBLE QUIZ", "PREACHING" };

                // TOPICS
                foreach (var tempTopic in tempTopics)
                {
                    // Row
                    TemplateFragment topicRowFragment = new TemplateFragment(GetSection("TOPIC-ROW"));
                    topicRowFragment.ContentKey = "TOPIC-ROW";

                    // Topic Label
                    TemplateFragment topicFragment = new TemplateFragment(GetSection("TOPIC-ITEM"));
                    topicFragment.Add("CONTENT", tempTopic);
                    topicRowFragment.Fragments.Add(topicFragment);

                    // Cell Container
                    for (int i = 0; i < 3; i++)
                    {
                        TemplateFragment cellContainerFragment = new TemplateFragment(GetSection("CELL-CONTAINER"));
                        cellContainerFragment.ContentKey = "ITEM-LIST";
                        topicRowFragment.Fragments.Add(cellContainerFragment);

                        // Cell 1
                        TemplateFragment cellOneFragment = new TemplateFragment(GetSection("FIRST-CELL"));
                        cellOneFragment.Add("CONTENT", "Bro Jun");
                        cellContainerFragment.Fragments.Add(cellOneFragment);

                        // Cell 2
                        TemplateFragment cellTwo = new TemplateFragment(GetSection("SUCCEEDING-CELL"));
                        cellTwo.Add("CONTENT", "Bro Greg");
                        cellContainerFragment.Fragments.Add(cellTwo);
                    }

                    sbBody.Append(topicRowFragment.ToString());
                }
            }

            provider = new NamedValueProvider();
            provider.Add("CONTENT", sbBody.ToString());

            // FOOTER
            provider.Add("FOOTER", GetSection("FOOTER"));


            sbContent.Append(Substituter.Substitute(GetSection("CONTAINER"), provider));

            #endregion

            return sbContent.ToString();
        }
    }
}
