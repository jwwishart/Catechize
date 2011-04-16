using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Catechize.Model;
using Catechize.Services;
using System.Data.Entity;

namespace Catechize.Controllers
{
    public class CoursesController : Controller
    {
        CatDbContext _db = new CatDbContext();


        // Constructor
        // 

        public CoursesController()
        {
            //if (services == null) throw new NullReferenceException("services cannot be null");

            //this.services = services;
        }


        // Actions
        //

        [HttpGet]
        public ActionResult NewCourse()
        {
            return View(new Course() { IsEnabled = true });
        }

        [HttpPost]
        public ActionResult NewCourse(Course course)
        {
            if (TryUpdateModel(course))
            {
                _db.Courses.Add(course);
                _db.Courses.Create();
            }

            return View(course);
        }

        [HttpGet]
        public ActionResult EditCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public ActionResult Index()
        {
            return View("ListAllCourses", _db.Courses.ToList());
        }

        public ActionResult ViewCourse(string courseName)
        {
            return View("ShowCourseDetails");
        }

        public ActionResult Register(string courseName)
        {
            return View();
        }

        [Authorize(Roles="master, administrator")] 
        public ActionResult Enable()
        {
            return View();
        }

        [Authorize(Roles = "master, administrator")] 
        public ActionResult Disable()
        {
            return View();
        }

    } // End Controller
}
