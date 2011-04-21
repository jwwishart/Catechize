using System.Collections.Generic;
using System.Globalization;

namespace Catechize.Model
{
    public abstract class Translatable<T> where T : HasCulture
    {
        private IList<T> _translations = new List<T>();

        public IList<T> Translations {
            get
            {
                return _translations;
            }
            set
            {
                _translations = value;
            }
        }

        public T GetLanguage(CultureInfo cultureInfo)
        {
            if (Translations == null)
                return null;

            foreach (var cLang in this.Translations)
            {
                if (cLang.Culture.Equals(cultureInfo))
                    return cLang;
            }

            return null;
        }

        public T GetLanguage(string cultureName)
        {
            return GetLanguage(new CultureInfo(cultureName));
        }
    }
}
