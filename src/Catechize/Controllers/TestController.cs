using System.Web.Mvc;
using Catechize.Templating;
using System;
using System.Collections;

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

        public ActionResult RenderTemplateTest() {
            object [] model = {
                new { Label="Name", Value="Justin Wishart"},
                new { Label="Age", Value="31"}
            };

            return View( model );
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
