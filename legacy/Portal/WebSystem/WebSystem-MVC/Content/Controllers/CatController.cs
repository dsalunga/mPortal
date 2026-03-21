using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WCMS.WebSystem.Content.Controllers
{
    public class CatController : ApiController
    {
        // GET: api/Cat
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Cat/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Cat
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Cat/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cat/5
        public void Delete(int id)
        {
        }
    }
}
