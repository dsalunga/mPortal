using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration
{
    public class GenericRegistration : NamedWebObject, ISelfManager
    {
        private static IRegistrationProvider _provider;

        static GenericRegistration()
        {
            _provider = new RegistrationSqlProvider();
        }

        public GenericRegistration()
        {
            EntryDate = DateTime.Now;
            Age = -1;

            Country = string.Empty;
            Locale = string.Empty;
            ExternalId = string.Empty;
            Designation = string.Empty;
            Airline = string.Empty;
            PlaceType = string.Empty;
            Gender = string.Empty;
        }

        public DateTime EntryDate { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string Locale { get; set; }
        public string ExternalId { get; set; }
        public string Designation { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Airline { get; set; }
        public string FlightNo { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Address { get; set; }
        public string PlaceType { get; set; }
        public string Gender { get; set; }

        public static IRegistrationProvider Provider
        {
            get { return _provider; }
        }

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        #endregion

        public override int OBJECT_ID
        {
            get { return -1; }
        }
    }
}
