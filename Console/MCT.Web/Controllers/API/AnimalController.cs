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

    public class AnimalController : ApiController
    {

        [Route("api/animal")]
        // GET api/<controller>
        public List<AnimalModel> Get()
        {
            List<AnimalModel> list = new List<AnimalModel>();
 
            SubjectManager manager = new SubjectManager();
            var animals = manager.GetAll<Animal>();

            animals.ToList().ForEach(n => list.Add(AnimalModel.Convert(n)));


            return list;
        }

        [Route("api/animal/{id}")]
        // GET api/<controller>/5
        public AnimalModel Get(int id)
        {
            SubjectManager manager = new SubjectManager();
            var animal = manager.GetAll<Animal>().Where(p => p.Id.Equals(id)).FirstOrDefault();

            AnimalModel model = AnimalModel.Convert(animal);

            model.Interactions = AnimalModel.ConverInteractionModels(manager.GetAllDependingInteractions(animal, true).ToList());

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