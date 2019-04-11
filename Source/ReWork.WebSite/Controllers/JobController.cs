using ReWork.Common;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.ViewModels.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{

    public class JobController : Controller
    {
        private IJobService _jobService;
        private ISectionService _sectionService;
        private ISkillService _skillService;

        public JobController(IJobService jobService, ISectionService sectionService, ISkillService skillService)
        {
            _jobService = jobService;
            _sectionService = sectionService;
            _skillService = skillService;
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

            return RedirectToAction("Index", "Home");
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