using System;
namespace Catechize.Templating
{
    public interface ITemplateValueProcessor
    {
        ITemplateEngine TemplateEngine { get; set; }
        string Key { get; }

        string ProcessValue( string value );
        string ProcessValue( string value, object source );
    }


    [Serializable]
    public abstract class TemplateValueProcessor : ITemplateValueProcessor
    {
        public ITemplateEngine TemplateEngine { get; set; }
        public abstract string Key { get; }

        public abstract string ProcessValue( string value );
        public abstract string ProcessValue( string value, object source );
    }
}
