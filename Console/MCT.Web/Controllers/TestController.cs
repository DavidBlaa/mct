using MCT.DB.Services;
using MCT.IO;
using System.Web.Mvc;


namespace MCT.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            SubjectManager subjectManager = new SubjectManager();


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

            WikipediaReader wReader = new WikipediaReader();

            var x = wReader.GetScientificName("Blumenkohl");

            x = wReader.GetName("Brassica");


            return View();
        }


    }
}