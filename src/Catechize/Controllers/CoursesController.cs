using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Catechize.Helpers;
using Catechize.Model;
using Catechize.Services;
using System.Data.Entity;

namespace Catechize.Controllers
{
    public class CoursesController : Controller
    {
        protected ICourseService CourseService { get; set; }


        // Constructor
        // 

        public CoursesController(ICourseService courseService)
        {
            if (courseService == null) throw new NullReferenceException("courseService cannot be null");

            this.CourseService = courseService;
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
                CourseService.Create(course);

                return RedirectToAction("ViewCourse", new { courseName = course.CourseID });
            }

            return View(course);
        }

        [HttpGet]
        [Authorize(Roles = "Master, Administrator, Manager")]
        public ActionResult EditCourse(string courseName)
        {
            return View(CourseService.GetByID(courseName));
        }

        [HttpPost]
        [Authorize(Roles = "Master, Administrator, Manager")]
        public ActionResult EditCourse(Course course)
        {
            if (TryUpdateModel(course))
            {
                CourseService.Update(course);
                return RedirectToAction("ViewCourse", new { courseName = course.CourseID });
            }

            return View(course);
        }

        public ActionResult Index()
        {
            var courses = CourseService.Query();

            if (User.IsStudent() || User.IsAnonymous() )
            {
                return View("ListCourses_Student", courses.Where(c => c.IsEnabled == true).ToList<Course>());
            }
            else
            {
                return View("ListCourses_Admin", courses.ToList<Course>());
            }
        }

        public ActionResult ViewCourse(string courseName)
        {
            Course course = CourseService.GetByID(courseName);

            if (User.IsStudent() || User.IsAnonymous())
            {
                if (false == course.IsEnabled)
                    return this.RedirectToUnauthorized();

                return View("ViewCourse_Student", course);
            }
            else
            {
                return View("ViewCourse_Admin", course);
            }
        }

        public ActionResult Register(string courseName)
        {
            return View();
        }

        public ActionResult Unregister(string courseName)
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
