using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DF.Domain.Abstract;
using DF.Core;
using DF.Repositories;
using System.Web.Security;

namespace DF.UI.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _repository;
        private IUnitOfWork _unitOfWork;
        private IUser _user;
        private IUserManager _manager;
        public UserController(IUserRepository repository, IUnitOfWork unitOfWork,IUser user, IUserManager manager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _repository.UnitOfWork = unitOfWork;
            _user = user;
            _manager = manager;
            _manager.UserRepository = _repository;

       
        }
        
        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Title = "Add User";
            return View(_user);
        }

        [HttpPost]
        public ActionResult Add(DF.Domain.Concrete.User user)
        {
            return Save(user);  
        }

        private ActionResult Save(DF.Domain.Concrete.User user)
        {
            try
            {
                _manager.UserRepository = _repository;
                TempData["Result"] = _manager.Save(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.SaveResult = "There was an error saving the user.  No changes were made.";
                return View("AddUser", user);
            }
        }

        public ViewResult Edit(Guid id)
        {
            var user = _repository.Where(u => u.Id == id).SingleOrDefault();
            if (user == null)
            {
            }
            return View("Add", user);
        }

        [HttpPost]
        public ActionResult Edit(DF.Domain.Concrete.User user)
        {
            ValidateModel(user);
            return Save(user);
        }

        public ViewResult Index()
        {
            ViewBag.Message = TempData["Result"];
            ViewBag.Title = "Users";
            return View(_repository.All().OrderBy(u => u.FirstName).Take(20).ToList());
        }

        public ActionResult Delete(Guid id)
        {
            _manager.Delete(id);
            return null;
        }

        public ViewResult Detail(Guid id)
        {   
            var user = _manager.GetById(id);
            return View(user);
        }

    }
}
