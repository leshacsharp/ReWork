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
            CreateJobViewModel createJobModel = new CreateJobViewModel();
            createJobModel.Skills = GetCategories();
            return View(createJobModel);
        }

        [HttpPost]
        public ActionResult Create(CreateJobViewModel jobModel)
        {
            if (!ModelState.IsValid)
            {
                jobModel.Skills = GetCategories();
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

                editJobModel.SelectedSkills = job.SkillsId;
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
        public ActionResult Delete(int id)
        {
            _jobService.DeleteJob(id);
            _commitProvider.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
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



        [HttpGet]
        public ActionResult MyJobs(int? page)
        {
            int jobsCountOnPage = 1;
            int pageNumber = page ?? 1;

            string userId = User.Identity.GetUserId();
            IEnumerable<JobInfo> jobs = _jobService.FindUserJobs(userId, pageNumber, jobsCountOnPage);

            PageInfo pageInfo = new PageInfo() { CurrentPage = pageNumber, ItemsOnPage = jobsCountOnPage };
            pageInfo.TotalItems = _jobService.UserJobsCount(userId);

            JobsViewModel jobsModel = new JobsViewModel() { PageInfo = pageInfo, Jobs = jobs };
            return View(jobsModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Jobs(int? page, int? skillId, string keyWords, int priceFrom = 0)
        {
            int jobsCountOnPage = 1;
            int pageNumber = page ?? 1;

            FindJobsParams findParams = new FindJobsParams()
            { Page = pageNumber, CountJobsOnPage = jobsCountOnPage, SkillId = skillId, KeyWords = keyWords, PriceFrom = priceFrom };

            IEnumerable<JobInfo> jobs = _jobService.FindJobs(findParams);

            PageInfo pageInfo = new PageInfo() { CurrentPage = pageNumber, ItemsOnPage = jobsCountOnPage };
            pageInfo.TotalItems = _jobService.JobsCount(skillId, priceFrom, keyWords);

            JobsViewModel jobsModel = new JobsViewModel() { PageInfo = pageInfo, Jobs = jobs };
            return View(jobsModel);
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