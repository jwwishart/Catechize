using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Templating
{
    public interface ITemplateLoader
    {
        string Load( string templateName );
    }

    public abstract class TemplateLoader : ITemplateLoader
    {
        public abstract string Load( string templateName );
    }
}
