using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Catechize.Templating.ValueProcessors
{
    public class AnchorValueProcessor : TemplateValueProcessor, ITemplateValueProcessor
    {
        public override string Key {
            get { return "a"; }
        }

        public override string ProcessValue( string value ) {
            return ProcessValue( value, null );
        }

        public override string ProcessValue( string value, object source ) {
            var template = "<a href='{0}' target='_blank'>{1}</a>";
            string href = string.Empty;
            string text = string.Empty;

            var attributes = value.Split( '|' );

            foreach ( string attr in attributes ) {
                var kv = attr.Split( '=' );

                if ( kv[0].Equals( "href", StringComparison.OrdinalIgnoreCase ) ) {
                    var tempHref = kv[1].Trim();
                    href = tempHref;

                    if ( tempHref.IndexOf( "http://" ) != 1 || tempHref.IndexOf( "https://" ) != 1 ) {
                        href = "http://" + href;
                    }
                }

                if ( kv[0].Equals( "text", StringComparison.OrdinalIgnoreCase ) ) {
                    text = kv[1].Trim();
                }
            }

            if ( href == string.Empty || href == "http://" ) {
                href = ReflectDataSource( "href", source );

                if ( href.IndexOf( "http://" ) != 1 || href.IndexOf( "https://" ) != 1 ) {
                    href = "http://" + href;
                }
            }

            if ( text == string.Empty ) {
                text = ReflectDataSource( "text", source );
            }

            return String.Format( template, href, text );
        }

        private string ReflectDataSource( string propertyName, object source ) {
            if ( source.GetType().GetProperty( propertyName) != null ) {
                var propValue = source.GetType().GetProperty( propertyName).GetValue( source, new object[] { } );

                if ( propValue == null )
                    return string.Empty;
                else
                    return propValue.ToString();
            }

            return string.Empty;
        }
    }
}
