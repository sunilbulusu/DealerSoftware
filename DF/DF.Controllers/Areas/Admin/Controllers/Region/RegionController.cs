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
    public class RegionController : Controller
    {
        private IRegionRepository _repository;
        private IUnitOfWork _unitOfWork;
        private IRegion _region;
        private IRegionManager _manager;

        public RegionController(IRegionRepository repository, IUnitOfWork unitOfWork,IRegion region, IRegionManager manager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _repository.UnitOfWork = unitOfWork;
            _region = region;
            _manager = manager;
            _manager.RegionRepository = _repository;
        }

        #region properties
        #endregion

        [HttpGet]
        public ViewResult AddRegion()
        {
            ViewBag.Title = "Add Region";
            return View(_region);
        }

        [HttpPost]
        public ActionResult AddRegion(DF.Domain.Concrete.Region region)
        {
            return SaveRegion(region);  
        }

        private ActionResult SaveRegion(DF.Domain.Concrete.Region region)
        {
            try
            {
                _manager.RegionRepository = _repository;
                TempData["Result"] = _manager.Save(region);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.SaveResult = "There was an error saving the region.  No changes were made.";
                return View("AddRegion", region);
            }
        }

        public ViewResult EditRegion(Guid id)
        {
            var region = _repository.Where(u => u.Id == id).SingleOrDefault();
            if (region == null)
            {
            }
            return View("AddRegion", region);
        }

        [HttpPost]
        public ActionResult EditRegion(DF.Domain.Concrete.Region region)
        {
            ValidateModel(region);
            return SaveRegion(region);
        }

        public ViewResult Index()
        {
            ViewBag.Message = TempData["Result"];
            ViewBag.Title = "Region";
            return View(_repository.All().OrderBy(u => u.Name).Take(20).ToList());
        }

        public ActionResult Delete(Guid id)
        {
            _manager.Delete(id);
            return null;
        }

        public ViewResult Detail(Guid id)
        {
            var region = _manager.GetById(id);
            return View(region);
        }

        public PartialViewResult AddRegionForTeam(Guid id)
        {
            return PartialView("RegionList", _manager.GetRegionsToAddForTeam(id));
        }
    }
}

