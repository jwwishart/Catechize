using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catechize.Services
{
    public interface ICatechizeControllerService
    {
        IMembershipService MembershipService { get; }
        IRoleService RoleService { get; }
        ICourseRepository CourseRepo { get; }
        IStudentRepository StudentRepo { get; }
        IPageRepository PageRepo { get; }
        IPartRepository PartRepo { get; }

        IHtmlPageRepository HtmlPageRepo { get; }

        /*
         * Put some of the major methods in this service
         * GetUserByID(userID)
         * IsUserInRole(userID, role)
         * etc...
         * 
         */
    }

    public class DefaultCatechizeControllerService : ICatechizeControllerService
    {
        private IMembershipService membershipService;
        private IRoleService roleService;
        private ICourseRepository courseRepo;
        private IStudentRepository studentRepo;
        private IPageRepository pageRepo;
        private IPartRepository partRepo;
        private IHtmlPageRepository htmlPageRepo;

        public DefaultCatechizeControllerService(
            IMembershipService membershipService,
            IRoleService roleService,
            ICourseRepository courseRepo,
            IStudentRepository studentRepo,
            IPageRepository pageRepo,
            IPartRepository partRepo,
            IHtmlPageRepository htmlPageRepo)
        {
            this.membershipService = membershipService;
            this.roleService = roleService;
            this.courseRepo = courseRepo;
            this.studentRepo = studentRepo;
            this.pageRepo = pageRepo;
            this.partRepo = partRepo;
            this.htmlPageRepo = htmlPageRepo;
        }

        public IMembershipService MembershipService
        { 
            get { return this.membershipService; } 
        }

        public IRoleService RoleService
        {
            get { return this.roleService; }
        }

        public ICourseRepository CourseRepo
        {
            get { return this.courseRepo; }
        }

        public IStudentRepository StudentRepo
        {
            get { return this.studentRepo; }
        }

        public IPageRepository PageRepo
        {
            get { return this.pageRepo; }
        }

        public IPartRepository PartRepo
        {
            get { return this.partRepo; }
        }

        public IHtmlPageRepository HtmlPageRepo
        {
            get { return this.htmlPageRepo; }
        }
    }
}