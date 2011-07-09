using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Templating.ValueProcessors
{
    public class SubTemplateValueProcessor : TemplateValueProcessor, ITemplateValueProcessor
    {
        private ITemplateEngine Engine { get; set; }
        private ITemplateLoader Loader { get; set; }

        public SubTemplateValueProcessor( ITemplateEngine engine, ITemplateLoader loader ) {
            this.Engine = engine;
            this.Loader = loader;
        }

        public override string Key {
            get { return "template"; }
        }

        public override string ProcessValue( string value ) {
            return ProcessValue( value, null );
        }

        public override string ProcessValue( string value, object source ) {
            var templateContent = Loader.Load( value );

            return Engine.ProcessTemplate( templateContent, source );
        }
    }
}
