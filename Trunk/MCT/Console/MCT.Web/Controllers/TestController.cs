﻿using MCT.DB.Entities;
using MCT.DB.Services;
using System;
using System.Collections.Generic;
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

            //subject.Name ="Upadte";
            //subjectManager.Update(subject);

            var x = subjectManager.GetAll<Subject>();

            


            return View();
        }
    }
}