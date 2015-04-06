﻿using MCT.DB.Entities;
using MCT.DB.Services;
using MCT.Helpers;
using MCT.IO;
using MCT.Web.Helpers;
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

            Media media = new Media();
            media.ImagePath="Images/Empty.png";

            DateManager dm = new DateManager();

            TimePeriod tp = new TimePeriod();

            tp.StartArea = TimePeriodArea.Start;
            tp.StartMonth = TimePeriodMonth.Januar;
            tp.EndArea = TimePeriodArea.Full;
            tp.EndMonth = TimePeriodMonth.Dezember;

            dm.Create<TimePeriod>(tp);

            Plant p = new Plant();
            p.Name = "plant mit Blühezeit";
            p.Bloom.Add(tp);

            subjectManager.Create<Plant>(p);

            var x = subjectManager.Get(1);

            return View();
        }

        public ActionResult LoadData()
        {
            // test create seeddata

            SeedDataGenerator.GenerateDays();


            // test create plants from file

            AsciiReader reader = new AsciiReader();

            string path = Path.Combine(AppConfigHelper.GetWorkspace(), "SeedData.txt");

            if (DataReader.FileExist(path))
            {
                Stream fileStream = reader.Open(path);

                List<Node> nodes = reader.ReadFile(fileStream, "SeedData.txt", "Plant");

                SubjectManager manager = new SubjectManager();

                foreach (var node in nodes)
                {
                    Plant plant = (Plant)node;
                    if (plant.Cultivation != null)
                    {
                        manager.Create<Cultivation>(plant.Cultivation);
                    }

                    manager.Create<Plant>(plant);
                }
            }

            return View("Index");
        }
    }
}