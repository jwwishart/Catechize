using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Catechize.Templating.ValueProcessors
{
    public class ReflectionValueProcessor : TemplateValueProcessor, ITemplateValueProcessor
    {
        public override string Key {
            get { return "data"; }
        }

        public override string ProcessValue( string value ) {
            return ProcessValue( value, null );
        }

        public override string ProcessValue( string value, object source ) {
            string result = string.Empty;

            if ( source == null )
                return string.Empty;

            return GetValue( value, source ).ToString();
        }

        private object GetValue( string valuePath, object source) {
            if ( valuePath.IndexOf( "." ) == -1 ) {
                return GetPropertyValue( valuePath, source );
            } else {
                var newSource = GetPropertyValue(GetNextPropertyName(valuePath), source);
                return GetValue(RemoveCurrentProperty(valuePath), newSource);
            }
        }

        private string GetNextPropertyName( string valuePath ) {
            int nextPeriod = valuePath.IndexOf(".");

            if (nextPeriod == -1)
                return valuePath;

            return valuePath.Substring( 0, nextPeriod );
        }

        private string RemoveCurrentProperty( string valuePath ) {
            return valuePath.Remove( 0, GetNextPropertyName( valuePath ).Length + 1 );
        }

        private object GetPropertyValue( string propertyName, object source ) {
            var propInfo = source.GetType().GetProperty( propertyName
                , BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase );

            if ( propInfo == null )
                return string.Empty;
            else {
                return propInfo.GetValue( source, new object[] { } );
            }        
        }

    }
}
