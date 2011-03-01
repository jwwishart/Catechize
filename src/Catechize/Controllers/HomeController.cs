using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.InteropServices;

namespace Catechize.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult TestErrors()
        {
            IDogDog dog = new IDogDog();
            var message = dog.Sound();
            throw new ArgumentException(message);
        }
    }
}

[System.Runtime.InteropServices.CoClass(typeof(Animal)), ComImport, Guid("C2871EB5-CDE1-4d57-BDE0-505CAA30A4A5")]
public interface IDogDog
{
    string Sound();
}

public class Animal : IDogDog
{
    public virtual string Sound()
    {
        return "Animal Sound";
    }
}

public class Dog : Animal
{
    public override string Sound()
    {
        return "Woof!";
    }
}