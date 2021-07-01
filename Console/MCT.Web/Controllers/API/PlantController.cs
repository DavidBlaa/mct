using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace MCT.Web.Controllers.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class PlantController : ApiController
    {

        [Route("api/plant")]
        // GET api/<controller>
        public List<PlantModel> Get()
        {
            List<PlantModel> list = new List<PlantModel>();
 
            SubjectManager manager = new SubjectManager();
            var plants = manager.GetAll<Plant>();

            plants.ToList().ForEach(n => list.Add(PlantModel.Convert(n)));


            return list;
        }

        [Route("api/plant/{id}")]
        // GET api/<controller>/5
        public PlantModel Get(int id)
        {
            SubjectManager manager = new SubjectManager();
            var plant = manager.GetAll<Plant>().Where(p => p.Id.Equals(id)).FirstOrDefault();

            PlantModel model = PlantModel.Convert(plant);

            model.Interactions = PlantModel.ConverInteractionModels(manager.GetAllDependingInteractions(plant, true).ToList());


            return model;
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

    //public class Species
    //{
    //    public long Id { get; set; }
    //    public string Name { get; set; }
    //    public string ScientificName { get; set; }
    //    public string Description { get; set; }

    //    public static Species ConvertTo(DB.Entities.Species species)
    //    {
    //        Species sam = new Species();

    //        sam.Id = species.Id;
    //        sam.Name = species.Name;
    //        sam.ScientificName = species.ScientificName;
    //        sam.Description = species.Description;

    //        return sam;
    //    }

    //    public static Species ConvertTo(DB.Entities.Subject subject)
    //    {
    //        Species sam = new Species();

    //        sam.Id = subject.Id;
    //        sam.Name = subject.Name;
    //        sam.ScientificName = "not available";
    //        sam.Description = subject.Description;

    //        return sam;
    //    }
    //}
}