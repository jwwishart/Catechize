using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catechize.Services
{
    public interface ICatechizeControllerService
    {
        ICourseRepository CourseRepo { get; }
        IStudentRepository StudentRepo { get; }
        IPageRepository PageRepo { get; }
        IPartRepository PartRepo { get; }

        IHtmlPageRepository HtmlPageRepo { get; }
    }

    public class DefaultCatechizeControllerService : ICatechizeControllerService
    {
        private ICourseRepository courseRepo;
        private IStudentRepository studentRepo;
        private IPageRepository pageRepo;
        private IPartRepository partRepo;
        private IHtmlPageRepository htmlPageRepo;

        public DefaultCatechizeControllerService(
            ICourseRepository courseRepo,
            IStudentRepository studentRepo,
            IPageRepository pageRepo,
            IPartRepository partRepo,
            IHtmlPageRepository htmlPageRepo)
        {
            this.courseRepo = courseRepo;
            this.studentRepo = studentRepo;
            this.pageRepo = pageRepo;
            this.partRepo = partRepo;
            this.htmlPageRepo = htmlPageRepo;
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