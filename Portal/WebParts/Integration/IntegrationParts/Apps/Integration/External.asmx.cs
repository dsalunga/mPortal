using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.CommonWS;

namespace WCMS.WebSystem.Apps.Integration
{
    /// <summary>
    /// Summary description for External
    /// </summary>
    [WebService(Namespace = "http://someorg.org/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ExternalService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //[WebMethod]
        //public List<Service> GetService()
        //{
        //    ExternalDBEntities db = new ExternalDBEntities();
        //    return db.Services.ToList();
        //}

        //[WebMethod]
        //public List<Service> GetService2(int serviceId = -1, int serviceTypeId = -1, int scheduleTypeId = -1, string serviceCode = "")
        //{
        //    ExternalDBEntities db = new ExternalDBEntities();
        //    return (from i in db.Services
        //            where (serviceId == -1 || i.ServiceID == serviceId)
        //                && (serviceTypeId == -1 || i.ServiceTypeID == serviceTypeId)
        //                && (scheduleTypeId == -1 || i.ScheduleTypeID == scheduleTypeId)
        //                && (string.IsNullOrEmpty(serviceCode) || i.ServiceCode.Equals(serviceCode, StringComparison.InvariantCultureIgnoreCase))
        //            select i).ToList();
        //}

        //[WebMethod]
        //public List<ServiceSchedule> GetServiceSchedule()
        //{
        //    ExternalDBEntities db = new ExternalDBEntities();
        //    return db.ServiceSchedules.ToList();
        //}

        //[WebMethod]
        //public List<ServiceSchedule> GetServiceSchedule2(int serviceScheduleId = -1, int localeId = -1, int serviceId = -1)
        //{
        //    ExternalDBEntities db = new ExternalDBEntities();
        //    return (from i in db.ServiceSchedules
        //            where (serviceScheduleId == -1 || i.ServiceScheduleID == serviceScheduleId)
        //                && (localeId == -1 || i.LocaleID == localeId)
        //                && (serviceId == -1 || i.ServiceID == serviceId)
        //            select i).ToList();
        //}

        //[WebMethod]
        //public List<ServiceSchedule> GetServiceSchedule3(DateTime serviceDateRangeStart, DateTime serviceDateRangeEnd, int localeId = -1, int serviceId = -1)
        //{
        //    ExternalDBEntities db = new ExternalDBEntities();
        //    return (from i in db.ServiceSchedules
        //            where
        //                    (localeId == -1 || i.LocaleID == localeId)
        //                && (serviceId == -1 || i.ServiceID == serviceId)
        //                && (i.StartServiceDateTime >= serviceDateRangeStart && i.StartServiceDateTime <= serviceDateRangeEnd)
        //            select i).ToList();
        //}

        //[WebMethod]
        //public List<ServiceType> GetServiceType()
        //{
        //    ExternalDBEntities db = new ExternalDBEntities();
        //    return db.ServiceTypes.ToList();
        //}

        //[WebMethod]
        //public List<ServiceType> GetServiceType2(int serviceTypeId = -1, string serviceTypeName = "")
        //{
        //    ExternalDBEntities db = new ExternalDBEntities();
        //    return (from i in db.ServiceTypes
        //            where (serviceTypeId == -1 || i.ServiceTypeID == serviceTypeId)
        //                && (string.IsNullOrEmpty(serviceTypeName) || i.ServiceTypeName.Equals(serviceTypeName, StringComparison.InvariantCultureIgnoreCase))
        //            select i).ToList();
        //}
    }
}
