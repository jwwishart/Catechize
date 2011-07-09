using System;

namespace Catechize.Templating.ValueProcessors
{
    public class UtilTemplateProcessor : TemplateValueProcessor, ITemplateValueProcessor
    {
        public override string ProcessValue( string value ) {
            switch ( value.ToLower() ) {
                case "date":
                    return DateTime.Now.ToString( "dd-MMM-yyyy" );
                case "time":
                    return DateTime.Now.ToString( "hh:mm:ss tt" );
                case "version":
                    return this.TemplateEngine.EngineVersion.ToString();
                case "engine":
                    return this.GetType().Name;
                default:
                    return "<b style='color: red'>TEMPLATE ERROR: value invalid</b>";
            }
        }

        public override string ProcessValue( string value, object source ) {
            return ProcessValue( value );
        }

        public override string Key {
            get { return "util"; }
        }
    }
}
