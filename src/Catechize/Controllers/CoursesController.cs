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
        [Authorize(Roles="Master, Administrator, Manager")]
        public ActionResult NewCourse()
        {
            return View(new Course() { IsEnabled = true });
        }

        [HttpPost]
        [Authorize(Roles = "Master, Administrator, Manager")]
        public ActionResult NewCourse(Course course)
        {
            if (TryUpdateModel(course))
            {
                _db.Courses.Add(course);
                _db.SaveChanges();

                return RedirectToAction("ViewCourse", new { courseName = course.Identifier });
            }

            return View(course);
        }

        [HttpGet]
        public ActionResult EditCourse(string courseName)
        {
            Course course = _db.Courses.Where(c => c.Identifier.Equals(courseName, StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault();

            return View(course);
        }

        [HttpPost]
        public ActionResult EditCourse(Course course)
        {
            if (TryUpdateModel(course))
            {
                _db.Entry(course).State = System.Data.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("ViewCourse", new { courseName = course.Identifier });
            }

            return View(course);
        }

        public ActionResult Index()
        {
            return View("ListAllCourses", _db.Courses.ToList());
        }

        public ActionResult ViewCourse(string courseName)
        {
            Course course = _db.Courses.Where(c => c.Identifier.Equals(courseName, StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault();

            return View(course);
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
