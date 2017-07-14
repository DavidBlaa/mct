using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Web.Models.Tree;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;

namespace MCT.Web.Controllers
{
    public class TreeController : Controller
    {
        // GET: Tree
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetJson()
        {
            SubjectManager subjectManager = new SubjectManager();

            //treeRootElement
            TreeElement root = new TreeElement();
            root.name = "ROOT";

            TreeElement noTaxon = new TreeElement();
            noTaxon.name = "No Parents";

            var rootChilds = subjectManager.GetAll<Node>().Where(n => n.Parent == null);



            foreach (var c in rootChilds)
            {
                TreeElement te = GetTreeElement(c);

                switch (c.Rank)
                {
                    case TaxonRank.Species:
                        noTaxon.children.Add(te);
                        break;
                    case TaxonRank.SubSpecies: noTaxon.children.Add(te); break;
                    default:
                        root.children.Add(GetTreeElement(c));
                        break;
                }


            }

            //root.children.Add(noTaxon);


            return Json(JsonConvert.SerializeObject(root), JsonRequestBehavior.AllowGet);
        }

        private TreeElement GetTreeElement(Node node)
        {
            SubjectManager subjectManager = new SubjectManager();


            if (node != null)
            {
                TreeElement tmp = new TreeElement();

                tmp.id = node.Id;
                tmp.name = node.Name;

                var children = subjectManager.GetAll<Node>().Where(c => c.Parent != null && c.Parent.Id.Equals(node.Id));
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        tmp.children.Add(GetTreeElement(child));
                    }

                }

                return tmp;
            }

            return null;
        }

    }
}