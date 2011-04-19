using System.Globalization;

namespace Catechize.Model
{
    public abstract class HasCulture
    {
        public CultureInfo Culture { get; set; }
        public string CultureName
        {
            get
            {
                if (Culture != null)
                    return Culture.Name;

                return string.Empty;
            }
            set
            {
                Culture = new CultureInfo(value);
            }
        }
    }
}
