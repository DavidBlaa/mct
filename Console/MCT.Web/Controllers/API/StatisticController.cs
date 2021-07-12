using MCT.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MCT.Web.Controllers.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StatisticController : ApiController
    {
        // GET api/<controller>
        [Route("api/statistic")]
        public Dictionary<string,int> Get()
        {
            return ModelHelper.GetStatistics();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}