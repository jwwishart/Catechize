using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Templating.ValueProcessors;
using Catechize.Templating.Web;

namespace Catechize.Templating
{
    public class TemplatingFactory
    {
        public static ITemplateEngine GetTemplateEngine() {
            var engine = new WebTemplateEngine(new TemplateTokenizer());

            engine.RegisterValueProcessor( new DateTimeNowPropertyReflectory() );
            engine.RegisterValueProcessor( new UtilTemplateProcessor() );
            engine.RegisterValueProcessor( new ReflectionValueProcessor() );
            engine.RegisterValueProcessor( new AnchorValueProcessor() );
            engine.RegisterValueProcessor(
                new SubTemplateValueProcessor( engine, GetTemplateLoader() ) );

            return engine;
        }

        public static ITemplateLoader GetTemplateLoader() {
            var result = new WebFileTemplateLoader("~/Templates/", "html");

            return result;
        }

        public static ITemplateTokenizer GetTokenizer() {
            return new TemplateTokenizer();
        }
    }
}
