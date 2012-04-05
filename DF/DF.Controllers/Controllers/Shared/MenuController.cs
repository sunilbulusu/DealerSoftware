using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DF.Domain.Abstract;
using DF.Repositories;
using DF.Core;
using DF.Domain.Enums;
using Magic.Extensions;

namespace DF.UI.Controllers
{
    public class MainMenuController : Controller
    {
        private IMenuRepository _repository;
        private IUnitOfWork _unitOfWork;
        private IMenuManager _manager;
        public MainMenuController(IMenuRepository repository, IUnitOfWork unitOfWork, IMenuManager manager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _repository.UnitOfWork = unitOfWork;
            _manager = manager;
        }

        [ChildActionOnly]
        public PartialViewResult Menu(System.Web.Routing.RouteValueDictionary routeValues)
        {
            ViewBag.RouteController = routeValues["controller"] ?? string.Empty;
            ViewBag.RouteAction = routeValues["action"] ?? string.Empty;
            return PartialView("MainMenu", _repository.All().Where(m => m.MenuTypeId == 1).OrderBy(m=>m.SortOrder).ToList());
        }

    }
}
