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
            { CustomerUserName = User.Identity.Name, SkillsId = jobModel.SelectedSkills, Title = jobModel.Title, Description = jobModel.Description, Price = jobModel.Price, PriceDiscussed = jobModel.PriceDiscussed };

            _jobService.CreateJob(jobParams);
            _commitProvider.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            JobInfo job = _jobService.FindById(id);
            if(job != null)
            {
                EditJobViewModel editJobModel = new EditJobViewModel()
                { Id = job.Id, Title = job.Title, Description = job.Description, Price = job.Price, PriceDiscussed = job.PriceDiscussed };

                editJobModel.SelectedSkills = job.Skills.Select(p => p.Id);
                editJobModel.Skills = GetCategories();

                return View(editJobModel);
            }

            return View("Error");
        }

        [HttpPost]
        public ActionResult Edit(EditJobViewModel editJobModel)
        {
            if(!ModelState.IsValid)
            {
                editJobModel.Skills = GetCategories();
                return View(editJobModel);
            }

            EditJobParams editJobParams = new EditJobParams()
            { Id = editJobModel.Id, SkillsId = editJobModel.SelectedSkills, Title = editJobModel.Title, Description = editJobModel.Description, Price = editJobModel.Price, PriceDiscussed = editJobModel.PriceDiscussed };

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

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(int id)
        {
            JobInfo job = _jobService.FindById(id);
            IEnumerable<OfferInfo> offers = _offerService.FindJobOffers(id);

            DetailsJobViewModel detailsJobModel = new DetailsJobViewModel() { Job = job, Offers = offers };

            return job != null ? View(detailsJobModel) : View("Error");
        }



        [AllowAnonymous]
        [HttpGet]
        public ActionResult Jobs()
        {
            IEnumerable<SectionInfo> sections = _sectionService.GetSectionsInfo(); 
            return View(sections);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Jobs(int[] skillsId, string keyWords, int priceFrom = 0)
        {
            IEnumerable<JobInfo> jobs = _jobService.FindJobs(skillsId, keyWords, priceFrom);
            return Json(jobs);
        }


        private IEnumerable<SelectListItem> GetCategories()
        {
            IEnumerable<Section> sections = _sectionService.GetAll();
            IEnumerable<Skill> skills = _skillService.GetAll();

            IEnumerable<SelectListGroup> groups = sections.Select(p => new SelectListGroup() { Name = p.Title }).ToList();

            var skillsWithSections = sections.Join(skills, p => p.Id, p => p.SectionId,
                                                 (section, skill) => new
                                                 {
                                                     Id = skill.Id,
                                                     Title = skill.Title,
                                                     SectionTitle = section.Title
                                                 });

            IEnumerable<SelectListItem> groupData = skillsWithSections.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.Title,
                Group = groups.First(sec => sec.Name.Equals(p.SectionTitle))
            });

            return groupData;
        }
    }
}