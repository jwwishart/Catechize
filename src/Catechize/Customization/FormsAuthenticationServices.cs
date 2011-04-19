using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;

namespace Catechize.Services
{
    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        private IMembershipService MembershipService { get; set; }

        public FormsAuthenticationService(IMembershipService membership)
        {
            this.MembershipService = membership;
        }

        public void SignIn(string username, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(username)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            
            IEnumerable<string> roles = MembershipService.GetRoles(username)
                        .AsEnumerable<string>();

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, 
                username, 
                DateTime.Now, 
                DateTime.Now.Add(FormsAuthentication.Timeout),
                createPersistentCookie, 
                String.Join(",", roles), 
                FormsAuthentication.FormsCookiePath);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(ticket));

            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
