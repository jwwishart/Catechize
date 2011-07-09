using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Templating.ValueProcessors
{
    public class DateTimeNowPropertyReflectory : TemplateValueProcessor, ITemplateValueProcessor {
        public override string ProcessValue(string value)
        {
            var date = DateTime.Now;

            return ProcessValue( value, date );
        }

        public override string ProcessValue( string value, object dateTime ) {
            if ( dateTime == null )
                dateTime = DateTime.Now;
            if ( dateTime is DateTime == false )
                dateTime = DateTime.Now; 

            var propInfo = dateTime.GetType().GetProperty( value );
            return propInfo.GetValue( dateTime, new object[] { } ).ToString();
        }

        public override string Key {
            get { return "date"; }
        }
    }
}
