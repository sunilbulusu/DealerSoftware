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
    public class RegionLocationController : Controller
    {
        private IRegionLocationRepository _repository;
        private IUnitOfWork _unitOfWork;
        private IRegionLocation _regionlocation;
        private IRegionLocationManager _manager;
        public RegionLocationController(IRegionLocationRepository repository, IUnitOfWork unitOfWork,IRegionLocation regionlocation, IRegionLocationManager manager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _repository.UnitOfWork = unitOfWork;
            _regionlocation = regionlocation;
            _manager = manager;
            _manager.RegionLocationRepository = _repository;
        }
        
        [HttpGet]
        public ViewResult AddRegionLocation()
        {
            ViewBag.Title = "AddRegionLocation";
            return View(_regionlocation);
        }

        [HttpPost]
        public ActionResult AddRegionLocation(DF.Domain.Concrete.RegionLocation regionlocation)
        {
            return SaveRegionLocation(regionlocation);  
        }

        private ActionResult SaveRegionLocation(DF.Domain.Concrete.RegionLocation regionlocation)
        {
            try
            {
                _manager.RegionLocationRepository = _repository;
                TempData["Result"] = _manager.Save(regionlocation);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.SaveResult = "There was an error saving the region location.  No changes were made.";
                return View("AddRegionLocation", regionlocation);
            }
        }

        public ViewResult EditRegionLocation(Guid id)
        {
            var regionlocation = _repository.Where(u => u.Id == id).SingleOrDefault();
            if (regionlocation == null)
            {
            }
            return View("AddRegionLocation", regionlocation);
        }

        [HttpPost]
        public ActionResult EditRegionLocation(DF.Domain.Concrete.RegionLocation regionlocation)
        {
            ValidateModel(regionlocation);
            return SaveRegionLocation(regionlocation);
        }

        public ViewResult Index(Guid id)
        {
            ViewBag.Message = TempData["Result"];
            ViewBag.Title = "RegionLocation";
            //return View(_repository.Where(r=>r.RegionId ==id).Include("Region").Include("RegionLocationType").OrderBy(u => u.Value).Take(20).ToList());

            var list = _repository.Where(r => r.RegionId == id).Include("Region").Include("RegionLocationType").OrderBy(u => u.Value).Take(20).ToList();
            return View(list);
        }

        public ActionResult Delete(Guid id)
        {
            _manager.Delete(id);
            return null;
        }

        public ViewResult Detail(Guid id)
        {
            var regionlocation = _manager.GetById(id);
            return View(regionlocation);
        }

    }
}

