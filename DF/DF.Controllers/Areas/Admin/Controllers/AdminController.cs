using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DF.Domain.Abstract;
using DF.Core;
using DF.Repositories;
using DF.Domain.Enums;

namespace DF.UI.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        
        private IUnitOfWork _unitOfWork;
        private IUser _user;
        private IUserManager _manager;
        private IUserRepository _repository;
        public AdminController(IUserRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _repository.UnitOfWork = unitOfWork;
        }

        public ViewResult Index()
        {
            return View();
        }

        //[ChildActionOnly]
        //public PartialViewResult AdminMenu()
        //{
        //    return PartialView("AdminMenu", _repository.Where(m => m.MenuTypeId == (int)MenuTypes.ADMIN && m.ParentId == null).Include("Menu1").ToList());
        //}
    }
}
