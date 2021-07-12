using MCT.DB.Services;
using MCT.Web.Models;
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
    public class InteractionsController : ApiController
    {
        [Route("api/interactions")]
        // GET api/<controller>
        public List<InteractionModel> Get()
        {
            List<InteractionModel> tmp = new List<InteractionModel>();

            InteractionManager manager = new InteractionManager();
            var interactions = manager.GetAll();

            interactions.ToList().ForEach(a => tmp.Add(InteractionModel.Convert(a)));

            return tmp;
        }

        [Route("api/interactions({id}")]
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