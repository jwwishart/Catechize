using System.Collections.Generic;
using System.Globalization;

namespace Catechize.Model
{
    public abstract class Translatable<T> where T : class, IHasCulture
    {
        public IList<T> Translations { get; set; }

        public T GetLanguage(CultureInfo cultureInfo)
        {
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
