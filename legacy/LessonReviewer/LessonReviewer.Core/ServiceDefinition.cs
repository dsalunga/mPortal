using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.LessonReviewer.Core
{
    public class ServiceDefinition
    {
        public ServiceDefinition()
        {
            AllowSeek = true;
            InstancesByDate = true;

            Text = string.Empty;
            Value = string.Empty;
        }

        public string Text { get; set; }
        public string Value { get; set; }
        public bool AllowSeek { get; set; }
        public bool AllowSegmented { get; set; }
        public bool InstancesByDate { get; set; }
    }
}
