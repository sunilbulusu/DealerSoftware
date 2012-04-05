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
    public class TeamController : Controller
    {
        private ITeamRepository _repository;
        private IUnitOfWork _unitOfWork;
        private ITeam _team;
        private ITeamManager _manager;

        public TeamController(ITeamRepository repository, IUnitOfWork unitOfWork,ITeam team, ITeamManager manager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _repository.UnitOfWork = unitOfWork;
            _team = team;
            _manager = manager;
            _manager.TeamRepository = _repository;
        }
        
        [HttpGet]
        public ViewResult AddTeam()
        {
            ViewBag.Title = "Add Team";
            return View(_team);
        }

        [HttpPost]
        public ActionResult AddTeam(DF.Domain.Concrete.Team team)
        {
            return SaveTeam(team);  
        }

        private ActionResult SaveTeam(DF.Domain.Concrete.Team team)
        {
            try
            {
                _manager.TeamRepository = _repository;
                TempData["Result"] = _manager.Save(team);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.SaveResult = "There was an error saving the team.  No changes were made.";
                return View("AddTeam", team);
            }
        }

        public ViewResult EditTeam(Guid id)
        {
            var team = _repository.Where(u => u.Id == id).SingleOrDefault();
            if (team == null)
            {
            }
            return View("AddTeam", team);
        }

        [HttpPost]
        public ActionResult EditTeam(DF.Domain.Concrete.Team team)
        {
            ValidateModel(team);
            return SaveTeam(team);
        }

        public ViewResult Index()
        {
            ViewBag.Message = TempData["Result"];
            ViewBag.Title = "Team";
            return View(_repository.All().OrderBy(u => u.Name).Take(20).ToList());
        }

        public ActionResult Delete(Guid id)
        {
            _manager.Delete(id);
            return null;
        }

        public ViewResult Detail(Guid id)
        {
            var team = _repository.Where(r => r.Id == id)
                .Include("TeamRegion.Region.RegionLocation.RegionLocationType")
                .Include("UserTeam.TeamRole.Role")
                .Include("UserTeam.User")
                .SingleOrDefault();

            ViewBag.Title = "Team Details";
            return View(team);
        }

        [HttpPost]
        public JsonResult RemoveRegion(Guid id)
        {
            return Json(new { @id = id, message = "success" });
        }
        
        [HttpPost]
        public PartialViewResult AddRegion(Guid id, Guid regionId)
        {
            _manager.AddRegion(id, regionId);
            return RegionsItemPartial(id, regionId);
        }

        [HttpGet]
        public PartialViewResult RegionsItemPartial(Guid Id, Guid regionId)
        {
            var teamRegion = _repository.Where(r => r.Id == Id).Select(t => t.TeamRegion.Where(tr => tr.RegionId == regionId)).FirstOrDefault();
            return PartialView(teamRegion);
        }
    }
}

