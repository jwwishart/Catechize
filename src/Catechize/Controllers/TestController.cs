using System.Web.Mvc;
using Catechize.Templating;
using System;

namespace Catechize.Controllers
{
    public class TestController : Controller
    {

        public ActionResult LinkToTest()
        {
            return View();
        }

        public ActionResult GravatarTest()
        {
            return View();
        }

        public ActionResult TemplateTest() {
            ITemplateEngine engine = TemplatingFactory.GetTemplateEngine();

            var result = engine.ProcessTemplate(
                TemplateEngine.LoadFileText(
                    Server.MapPath( "~/Templates/TestTemplate.html" ) ),
                    new {
                        Name = "Justin Wishart"
                        ,
                        Age = 31
                        ,
                        href = "www.bing.com"
                        ,
                        text = "Bing!!!"
                        ,
                        WOWS = new {
                            WOW = "This is the wow message",
                            NONWOW = "This is a non-wow message",
                            SubWow = new {
                                SubWowProperty = DateTime.Now.ToString()
                            }
                        }
                    } );

            return View((object)result);
        }
    }
}
