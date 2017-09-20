using MCT.DB.Entities;
using MCT.DB.Services;
using System.Linq;
using System.Web.Mvc;


namespace MCT.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            SetParentTest();

            //testIntractions();

            //SubjectManager subjectManager = new SubjectManager();

            //Subject subject = new Subject();

            //subject.Name = "ALTER FETTE SCHEIßE 3";
            //subject.Description = "ES FUNKT 3";

            //subjectManager.Create(subject);

            //subject.Name = "Upadte";
            //subjectManager.Update(subject);


            //Node pnode = new Node();
            //pnode.Name = "ParentNodetest";

            //subjectManager.Create(pnode);

            //Node node = new Node();
            //node.Name = "Nodetest";
            //node.Parent = pnode;

            //subjectManager.Create(node);

            //node.Rank = TaxonRank.Order;

            //var x = subjectManager.GetAll<Subject>();

            //string root = AppConfigHelper.GetRoot();
            //string ws = AppConfigHelper.GetWorkspaceForClient();

            //Media media = new Media();
            //media.ImagePath="Images/Empty.png";

            //DateManager dm = new DateManager();

            //TimePeriod tp = new TimePeriod();

            //tp.StartArea = TimePeriodArea.Anfang;
            //tp.StartMonth = TimePeriodMonth.Januar;
            //tp.EndArea = TimePeriodArea.Voll;
            //tp.EndMonth = TimePeriodMonth.Dezember;

            //dm.Create<TimePeriod>(tp);

            //Plant p = new Plant();
            //p.Name = "plant mit Blühezeit";
            //p.Bloom.Add(tp);

            //subjectManager.Create<Plant>(p);

            //var x = subjectManager.GetAll<Plant>();

            //Plant X = new Plant();
            //X.Name = "test";

            //TimePeriod tp = new TimePeriod();
            //tp.StartArea = TimePeriodArea.Anfang;
            //tp.StartMonth = TimePeriodMonth.Februar;
            //tp.EndArea = TimePeriodArea.Anfang;
            //tp.EndMonth = TimePeriodMonth.Februar;
            //tp.Type = TimePeriodType.Harvest;

            //X.Harvest.Add(tp);
            //subjectManager.Create<Plant>(X);
            //var y = subjectManager.GetAll<Plant>().Where(p=>p.Name.Equals("test")).FirstOrDefault();

            //var z = subjectManager.GetAll<Plant>();

            //NHibernateHelper.ReIndex();
            //NHibernateHelper.Search();


            //WikipediaReader wReader = new WikipediaReader();
            //var x = wReader.GetScientificName("Blumenkohl");
            //x = wReader.GetName("Brassica");


            return View();
        }

        public ActionResult Svg()
        {

            return View();
        }

        private void testIntractions()
        {
            //SubjectManager subjectManager = new SubjectManager();


            //Subject subject = new Subject();
            //subject.Name = "ALTER FETTE SCHEIßE 3";
            //subject.Description = "ES FUNKT 3";
            //subjectManager.Create(subject);

            //Subject Object = new Subject();
            //Object.Name = "ALTER FETTE SCHEIßE 3";
            //Object.Description = "ES FUNKT 3";
            //subjectManager.Create(Object);

            //Predicate positive =
            //    subjectManager.GetAll<Predicate>().Where(p => p.Name.ToLower().Equals("positiv")).FirstOrDefault();

            //Predicate predicate = new Predicate();
            //predicate.Parent = positive;
            //predicate.Name = "X";

            //InteractionManager interactionManager = new InteractionManager();
            //Interaction interaction = new Interaction();
            //interaction.Subject = subject;
            //interaction.Object = Object;
            //interaction.Predicate = predicate;

            //interactionManager.Create(interaction);
            //interactionManager.Delete(interaction);



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