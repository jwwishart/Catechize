using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Globalization;

public enum ApplicationMode {
    Development,
    Staging,
    Production
}

namespace Catechize.Services
{
    public class Settings
    {
        public static ApplicationMode AppMode {
            get {
                ApplicationMode result = ApplicationMode.Development;

                if (false == Enum.TryParse(ConfigurationManager.AppSettings["AppMode"], out result))
                {
                    throw new ConfigurationErrorsException("AppMode setting in the appSettings configuration section was not set to either Development, Staging or Production");
                }

                return result;
            }
        }

    }
}