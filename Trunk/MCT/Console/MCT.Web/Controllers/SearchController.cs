using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Search()
        {
            SubjectManager subjectManager = new SubjectManager();

            //Get all subjects
            var subjects = subjectManager.GetAll<Node>();

            SearchModel Model = new SearchModel();

            //convert all subjects to subjectModels
            subjects.ToList().ForEach(s => Model.Subjects.Add(SubjectModel.Convert(s)));

            return View(Model);
        }
    }
}