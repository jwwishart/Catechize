using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace Catechize.Templating.Web
{
    public class WebFileTemplateLoader : TemplateLoader
    {
        private string Directory { get; set; }
        private string Extension { get; set; }

        public WebFileTemplateLoader( string appRelativePath, string templateExtension ) {
            this.Directory = HttpContext.Current.Server
                .MapPath( appRelativePath )
                .TrimEnd('/') + "/";

            this.Extension = '.' + templateExtension.TrimStart('.');
        }       

        public override string Load( string templateName ) {
            return TemplateEngine.LoadFileText(Directory + templateName + Extension);
        }
    }
}
