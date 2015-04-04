using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Helpers;
using MCT.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            SubjectManager subjectManager = new SubjectManager();


            Subject subject = new Subject();

            subject.Name = "ALTER FETTE SCHEIßE 3";
            subject.Description = "ES FUNKT 3";

            subjectManager.Create(subject);

            subject.Name = "Upadte";
            subjectManager.Update(subject);

            Node pnode = new Node();
            pnode.Name = "ParentNodetest";

            subjectManager.Create(pnode);

            Node node = new Node();
            node.Name = "Nodetest";
            node.Parent = pnode;

            subjectManager.Create(node);

            node.Type = TaxonType.Order;

            var x = subjectManager.GetAll<Subject>();

            string root = AppConfigHelper.GetRoot();
            string ws = AppConfigHelper.GetWorkspaceForClient();

            


            return View();
        }

        public ActionResult Create(Subject subject)
        {
            if (Subject.IsEmtpy(subject))
                return View(new Subject());
            else
            {
                SubjectManager subjectManager = new SubjectManager();
                subjectManager.Create(subject);
                return View(subject);
            }
        }

        public ActionResult Show()
        { 
            SubjectManager subjectManager = new SubjectManager();
            return View(subjectManager.GetAll<Subject>());
        }

        [HttpGet]
        public ActionResult CreatePlant()
        {
            return View(new Plant());
        }

        [HttpPost]
        public ActionResult CreatePlant(Plant plant)
        {
            SubjectManager subjectManager = new SubjectManager();
            subjectManager.Create(plant);

            return View(plant);
        }

        [HttpGet]
        public ActionResult CreateAnimal()
        {
            return View(new Animal());
        }

        [HttpPost]
        public ActionResult CreateAnimal(Animal animal)
        {
            SubjectManager subjectManager = new SubjectManager();
            subjectManager.Create(animal);
            return View(animal);
        }

        [HttpGet]
        public ActionResult CreateEffect()
        {
            return View(new Effect());
        }

        [HttpPost]
        public ActionResult CreateEffect(Effect effect)
        {
            SubjectManager subjectManager = new SubjectManager();
            subjectManager.Create(effect);
            return View(effect);
        }


        public ActionResult LoadData()
        {
            AsciiReader reader = new AsciiReader();

            string path = Path.Combine(AppConfigHelper.GetWorkspace(), "SeedData.txt");

            if (DataReader.FileExist(path))
            {
                Stream fileStream = reader.Open(path);

                List<Node> nodes = reader.ReadFile(fileStream, "SeedData.txt", "Plant");

                SubjectManager manager = new SubjectManager();

                foreach (var node in nodes)
                {
                    manager.Create<Plant>((Plant)node);
                }
            }

            return View("Index");
        }
    }
}