using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using ReWork.Model.ViewModels;
using ReWork.Model.ViewModels.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private IJobService _jobService;
        private IOfferService _offerService;
        private ISectionService _sectionService;
        private ISkillService _skillService; 
        private ICommitProvider _commitProvider;

        public JobController(IJobService jobService, IOfferService offerService, ISectionService sectionService, ISkillService skillService, ICommitProvider commitProvider)
        {
            _jobService = jobService;
            _offerService = offerService;
            _sectionService = sectionService;
            _skillService = skillService;
            _commitProvider = commitProvider;
        } 

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Skills = GetCategories();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateJobViewModel jobModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Skills = GetCategories();
                return View(jobModel);
            }

            CreateJobParams jobParams = new CreateJobParams()
            {
                CustomerId = User.Identity.GetUserId(),
                SkillsId = jobModel.SelectedSkills,
                Title = jobModel.Title,
                Description = jobModel.Description,
                Price = jobModel.Price,
                PriceDiscussed = jobModel.PriceDiscussed
            };

            _jobService.CreateJob(jobParams);
            _commitProvider.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            JobInfo job = _jobService.FindJob(id);
            if (job == null)
                return View("Error");

            EditJobViewModel editJobModel = new EditJobViewModel()
            {
                JobId = job.Id,
                Title = job.Title,
                Description = job.Description,
                Price = job.Price,
                PriceDiscussed = job.PriceDiscussed
            };

            editJobModel.SelectedSkills = job.Skills.Select(p => p.Id);
            ViewBag.Skills = GetCategories();

            return View(editJobModel);
        }

        [HttpPost]
        public ActionResult Edit(EditJobViewModel editJobModel)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Skills = GetCategories();
                return View(editJobModel);
            }

            EditJobParams editJobParams = new EditJobParams()
            {
                JobId = editJobModel.JobId,
                SkillsId = editJobModel.SelectedSkills,
                Title = editJobModel.Title,
                Description = editJobModel.Description,
                Price = editJobModel.Price,
                PriceDiscussed = editJobModel.PriceDiscussed
            };

            _jobService.Edit(editJobParams);
            _commitProvider.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public void Delete(int id)
        {
            _jobService.DeleteJob(id);
            _commitProvider.SaveChanges();
        }

        [HttpPost]
        public void DeleteEmployeeFromJob(int id)
        {
            _jobService.DeleteEmployeeFromJob(id);
            _commitProvider.SaveChanges();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(int id)
        {
            JobInfo job = _jobService.FindJob(id);
            if (job == null)
                return View("Error");

            var offers = _offerService.FindJobOffers(id);
            DetailsJobViewModel detailsJobModel = new DetailsJobViewModel() { Job = job, Offers = offers };

            return View(detailsJobModel);
        }

        [HttpGet]
        public ActionResult FinishJob(int id)
        {
            var job = _jobService.FindCustomerJob(id);
            if (job == null)
                return View("Error");

            return View(job);
        }



        [AllowAnonymous]
        [HttpGet]
        public ActionResult Jobs()
        {
            var sections = _sectionService.GetSectionsInfo(); 
            return View(sections);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Jobs(int[] skillsId, string keyWords, int priceFrom = 0)
        {
            var jobs = _jobService.FindJobs(skillsId, keyWords, priceFrom);
            return Json(jobs);
        }


        private IEnumerable<SelectListItem> GetCategories()
        {
            var sections = _sectionService.GetAll();
            var skills = _skillService.GetAll();

            var groups = (from se in sections
                          select new SelectListGroup() { Name = se.Title }).ToList();

            var skillsWithSections = (from se in sections
                     join sk in skills on se.Id equals sk.SectionId
                     select new
                     {
                         Id = sk.Id,
                         Title = sk.Title,
                         SectionTitle = se.Title
                     }).ToList();

            var groupData = (from sk in skillsWithSections
                     select new SelectListItem()
                     {
                         Value = sk.Id.ToString(),
                         Text = sk.Title,
                         Group = groups.First(sec => sec.Name.Equals(sk.SectionTitle))
                     }).ToList();

            return groupData;
        }
    }
}