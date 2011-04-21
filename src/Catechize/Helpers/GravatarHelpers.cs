using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Text;

namespace Catechize.Helpers
{
    public enum GravatarDefault
    {
        FileNotFound,
        MysteryMan,
        Identicon,
        MonsterID,
        Wavatar,
        Retro
    }

    // Kudos to Rob Connery http://blog.wekeroad.com/2010/01/20/my-favorite-helpers-for-aspnet-mvc
    public static class GravatarHelpers
    {
        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email)
        {
            var url = GetGravatarUrl(helper, CleanupEmail(email), 40, GetDefaultGravatarString(GravatarDefault.MysteryMan));
            return MvcHtmlString.Create(ConstructImgTag(url));
        }

        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int size)
        {
            var url = GetGravatarUrl(helper, CleanupEmail(email), size, GetDefaultGravatarString(GravatarDefault.MysteryMan));
            return MvcHtmlString.Create(ConstructImgTag(url));
        }

        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int size, string defaultImageUrl)
        {
            var url = GetGravatarUrl(helper, CleanupEmail(email), size, UrlEncode(helper, defaultImageUrl));
            return MvcHtmlString.Create(ConstructImgTag(url));
        }

        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int size, GravatarDefault defaultImage)
        {
            var mode = GetDefaultGravatarString(defaultImage);

            var url = GetGravatarUrl(helper, CleanupEmail(email), size, mode);
            return MvcHtmlString.Create(ConstructImgTag(url));
        }

        public static string GetDefaultGravatarString(GravatarDefault defaultGravatar) {
            var mode = String.Empty;
            switch (defaultGravatar)
            {
                case GravatarDefault.FileNotFound:
                    mode = "404";
                    break;
                case GravatarDefault.Identicon:
                    mode = "identicon";
                    break;
                case GravatarDefault.MysteryMan:
                    mode = "mm";
                    break;
                case GravatarDefault.MonsterID:
                    mode = "monsterid";
                    break;
                case GravatarDefault.Wavatar:
                    mode = "wavatar";
                    break;
                case GravatarDefault.Retro:
                    mode = "retro";
                    break;
                default:
                    mode = "mm";
                    break;
            }
            return mode;
        }


        private static string ConstructImgTag(string src)
        {
            var result = "<img src=\"{0}\" alt=\"Gravatar\" class=\"gravatar\" />";
            return String.Format(result, src);
        }

        static string GetGravatarUrl(HtmlHelper helper, string email, int size, string defaultImage)
        {
            string result = "http://www.gravatar.com/avatar/{0}?s={1}&r=PG";
            string emailMD5 = EncryptMD5(CleanupEmail(email));

            result = (string.Format(result,
                        EncryptMD5(email), size.ToString()));

            if (false == String.IsNullOrEmpty(defaultImage))
                result += "&d=" + defaultImage;
        
            return result;
        }

        private static string UrlEncode(HtmlHelper helper, string url)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            return urlHelper.Encode(url);
        }

        private static string CleanupEmail(string email)
        {
            email = email.Trim();
            email = email.ToLower();

            return email;
        }

        private static string EncryptMD5(string value)
        {
            byte[] bytes;

            using (var md5 = MD5.Create())
            {
                bytes = Encoding.ASCII.GetBytes(value);
                bytes = md5.ComputeHash(bytes);
            }

            return String.Concat(bytes.Select(t => t.ToString("x2")));
        }
    }
}