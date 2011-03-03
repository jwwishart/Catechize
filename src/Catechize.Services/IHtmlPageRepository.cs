﻿using System;
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
        void Save(string pageKey, CultureInfo language, string newContent);
    }
}

namespace Catechize.Services.SqlServer
{
    public class HtmlPageRepository : IHtmlPageRepository
    {
        public void Create(string pageKey, CultureInfo language, string content)
        {
            throw new NotImplementedException();
        }

        public string GetPage(string pageKey, CultureInfo language)
        {
            throw new NotImplementedException();
        }

        public void Save(string pageKey, CultureInfo language, string newContent)
        {
            throw new NotImplementedException();
        }
    }
}