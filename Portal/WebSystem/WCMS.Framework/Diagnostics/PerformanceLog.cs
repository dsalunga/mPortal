using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace WCMS.Framework.Diagnostics
{
    public class PerformanceLog
    {
        private static List<PerformanceLog> _cache;

        static PerformanceLog()
        {
            _cache = new List<PerformanceLog>();
        }

        public PerformanceLog() { }

        public PerformanceLog(string content, TimeSpan duration, int pageId)
            : this(content, duration, pageId, DateTime.Now) { }

        public PerformanceLog(string content, TimeSpan duration, int pageId, DateTime logDateTime)
        {
            this.Id = logDateTime.Ticks;
            this.Content = content;
            this.Duration = duration;
            this.PageId = pageId;
            this.LogDateTime = logDateTime;
        }

        public long Id { get; set; }
        public DateTime LogDateTime { get; set; }
        public string Content { get; set; }
        public TimeSpan Duration { get; set; }
        public int PageId { get; set; }

        public static void AddToCache(PerformanceLog item)
        {
            if (_cache.Count > 250)
                _cache.Clear();

            _cache.Add(item);
        }

        public static List<PerformanceLog> Cache { get { return _cache; } }

        public static void EndLog(string content, Stopwatch sw, int pageId)
        {
            if (WConfig.EnablePerfLogging && sw != null)
            {
                sw.Stop();

                PerformanceLog item = new PerformanceLog(content, sw.Elapsed, pageId);
                PerformanceLog.AddToCache(item);
            }
        }

        public static void EndLogNoCheck(string content, Stopwatch sw, int pageId)
        {
            if (sw != null)
            {
                sw.Stop();

                PerformanceLog item = new PerformanceLog(content, sw.Elapsed, pageId);
                PerformanceLog.AddToCache(item);
            }
        }

        public static Stopwatch StartLog()
        {
            if (WConfig.EnablePerfLogging)
            {
                var sw = new Stopwatch();
                sw.Start();

                return sw;
            }

            return null;
        }

        public static Stopwatch StartLogNoCheck()
        {
            var sw = new Stopwatch();
            sw.Start();

            return sw;
        }

        public static void AddLogRunning(string content, Stopwatch sw, int pageId)
        {
            if (WConfig.EnablePerfLogging && sw != null)
            {
                PerformanceLog perfLog = new PerformanceLog(content, sw.Elapsed, pageId);
                PerformanceLog.AddToCache(perfLog);
            }
        }

        public static void Log(Func<int> action, string content, int pageId = -1)
        {
            if (WConfig.EnablePerfLogging)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                // Execute Action
                var id = action();

                sw.Stop();

                PerformanceLog perfLog = new PerformanceLog(content, sw.Elapsed, pageId == -1 ? id : pageId);
                PerformanceLog.AddToCache(perfLog);
            }
            else
            {
                // Execute Action
                action();
            }
        }
    }
}
