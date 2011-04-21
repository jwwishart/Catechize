using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Catechize.Services
{
    /// <summary>
    /// Interface for simple Html page loading/editing/creation etc.
    /// </summary>
    public interface IHtmlPageRepository
    {
        void Create(string pageKey, CultureInfo language, string content);
        string GetPage(string pageKey, CultureInfo language);
        void Update(string pageKey, CultureInfo language, string newContent);
        void Delete(string pageKey, CultureInfo language);
    }
}

