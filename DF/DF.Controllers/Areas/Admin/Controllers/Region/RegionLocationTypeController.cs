using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DF.Domain.Abstract;
using DF.Core;
using DF.Repositories;

namespace DF.UI.Areas.Admin.Controllers
{
    public class RegionLocationTypeController : Controller
    {
        private IRegionLocationTypeRepository _repository;
        private IUnitOfWork _unitOfWork;
        private IRegionLocationType _regionlocationtype;
        private IRegionLocationTypeManager _manager;
        public RegionLocationTypeController(IRegionLocationTypeRepository repository, IUnitOfWork unitOfWork,IRegionLocationType regionlocationtype, IRegionLocationTypeManager manager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _repository.UnitOfWork = unitOfWork;
            _regionlocationtype = regionlocationtype;
            _manager = manager;
            _manager.RegionLocationTypeRepository = _repository;
        }
        
        [HttpGet]
        public ViewResult AddRegionLocationType()
        {
            ViewBag.Title = "Add RegionLocationType";
            return View(_regionlocationtype);
        }

        [HttpPost]
        public ActionResult AddRegionLocationType(DF.Domain.Concrete.RegionLocationType regionlocationtype)
        {
            return SaveRegionLocationType(regionlocationtype);  
        }

        private ActionResult SaveRegionLocationType(DF.Domain.Concrete.RegionLocationType regionlocationtype)
        {
            try
            {
                _manager.RegionLocationTypeRepository = _repository;
                TempData["Result"] = _manager.Save(regionlocationtype);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.SaveResult = "There was an error saving the region type.  No changes were made.";
                return View("AddRegionLocationType", regionlocationtype);
            }
        }

        public ViewResult EditRegionLocationType(Guid id)
        {
            var regionlocationtype = _repository.Where(u => u.Id == id).SingleOrDefault();
            if (regionlocationtype == null)
            {
            }
            return View("AddRegionLocationType", regionlocationtype);
        }

        [HttpPost]
        public ActionResult EditRegionLocationType(DF.Domain.Concrete.RegionLocationType regionlocationtype)
        {
            ValidateModel(regionlocationtype);
            return SaveRegionLocationType(regionlocationtype);
        }

        public ViewResult Index()
        {
            ViewBag.Message = TempData["Result"];
            ViewBag.Title = "RegionLocationType";
            return View(_repository.All().OrderBy(u => u.Name).Take(20).ToList());
        }

        public ActionResult Delete(Guid id)
        {
            _manager.Delete(id);
            return null;
        }

        public ViewResult Detail(Guid id)
        {
            var regionlocationtype = _manager.GetById(id);
            return View(regionlocationtype);
        }

    }
}

