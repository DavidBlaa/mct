using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Helpers;
using MCT.Web.Models.Test;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml;

namespace MCT.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {

            testTimperiods();

            //SetParentTest();

            //testIntractions();



            //WikipediaReader wReader = new WikipediaReader();
            //var x = wReader.GetScientificName("Blumenkohl");
            //x = wReader.GetName("Brassica");


            return View();
        }

        

        public ActionResult Svg()
        {

            

            return View();
        }

        public ActionResult GetSvg()
        {
            Tuple<string> model;

            string path = Path.Combine(AppConfigHelper.GetWorkspace(), "Beetplaner", "beet1.svg");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            return this.Content(xmlDoc.InnerXml);
        }

        public ActionResult Beet()
        {
            BeetModel model = new BeetModel();

            //test Pflanze 1
            PflanzenModel p1 = new PflanzenModel()
            {
                Name = "Blumenkohl",
                X = 0,
                Y = 0
            };

            PflanzenModel p2 = new PflanzenModel()
            {
                Name = "Tomate",
                X = 500,
                Y = 200
            };

            model.Pflanzen.Add(p1);
            model.Pflanzen.Add(p2);

            return View(model);
        }

        public ActionResult Pflanze()
        {

            PflanzenModel model = new PflanzenModel()
            {
                Name = "Gurke",
                X = 50,
                Y = 20
            };

            return PartialView(model);
        }

        private void testIntractions()
        {
            SubjectManager subjectManager = new SubjectManager();


            Subject subject = new Subject();
            subject.Name = "ALTER FETTE SCHEIßE 3";
            subject.Description = "ES FUNKT 3";
            subjectManager.Create(subject);

            Subject Object = new Subject();
            Object.Name = "ALTER FETTE SCHEIßE 3";
            Object.Description = "ES FUNKT 3";
            subjectManager.Create(Object);

            Predicate positive =
                subjectManager.GetAll<Predicate>().Where(p => p.Name.ToLower().Equals("positiv")).FirstOrDefault();

            Predicate predicate = new Predicate();
            predicate.Parent = positive;
            predicate.Name = "X";

            InteractionManager interactionManager = new InteractionManager();
            Interaction interaction = new Interaction();
            interaction.Subject = subject;
            interaction.Object = Object;
            interaction.Predicate = predicate;

            interactionManager.Create(interaction);
            interactionManager.Delete(interaction);



        }

        private void testTimperiods()
        {
            SubjectManager subjectManager = new SubjectManager();

            Subject subject = new Subject();
            subject.Name = "ALTER FETTE SCHEIßE 3";
            subject.Description = "ES FUNKT 3";
            subjectManager.Create(subject);

            Sowing start = new Sowing();
            start.AssignedTo = subject;
            start.StartArea = TimePeriodArea.Anfang;
            start.StartMonth = TimePeriodMonth.Januar;
            start.EndArea = TimePeriodArea.Anfang;
            start.EndMonth = TimePeriodMonth.Februar;
            start.Start = true;


            Harvest ende = new Harvest();
            ende.AssignedTo = subject;
            ende.StartArea = TimePeriodArea.Mitte;
            ende.StartMonth = TimePeriodMonth.März;
            ende.EndArea = TimePeriodArea.Ende;
            ende.EndMonth = TimePeriodMonth.Juli;
            ende.Start = false;


            //ende = subjectManager.Create(ende);
            start.Next = ende;

            //start = subjectManager.Create(start);

            subject.TimePeriods.Add(start);
            subject.TimePeriods.Add(ende);

            subjectManager.Update(subject);


            var s = subjectManager.Get(subject.Id);

        }

        private void SetParentTest()
        {
            SubjectManager subjectManager = new SubjectManager();

            var a = subjectManager.GetAll<Plant>().FirstOrDefault(s => s.Id == 299);
            var b = subjectManager.GetAll<Node>().FirstOrDefault(s => s.Id == 240);

            a.Parent = b;

            subjectManager.Update(a);
            a.Parent = null;
            subjectManager.Update(a);


        }
    }
}