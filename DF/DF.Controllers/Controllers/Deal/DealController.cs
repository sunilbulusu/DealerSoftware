using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DF.Domain.Abstract;
using DF.Core;
using DF.Domain.Concrete;
using DF.Repositories;
using DF.Domain.Enums;

namespace DF.UI.Controllers
{
    public class DealController : Controller
    {
        
        private IUnitOfWork _unitOfWork;
        private IDeal _user;
        private IDealManager _manager;
        private IDealRepository _repository;
        public DealController(IDealRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _repository.UnitOfWork = unitOfWork;
        }

        public ViewResult Index()
        {
            var items = _repository.All().Include("Buyer.Contact").Include("Dealership").Include("Lender").ToList();
            return View(items);
        }

        public ViewResult Details(Guid id)
        {
            var deal = _repository.All().Where(d => d.Id == id).ToList().First();
            return View(deal);
        }

        public ActionResult EditDealPartial(Guid id)
        {
            var deal = _repository.Where(d => d.Id == id);

            return PartialView("_editDealPartial", deal);
        }

        public ActionResult ViewDealPartial(Guid id)
        {
            var deal = _repository.Where(d => d.Id == id);

            return PartialView("_viewDealPartial", deal);
        }

    }
}
