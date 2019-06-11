using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Context;
using ReWork.Model.EntitiesInfo;
using ReWork.Model.ViewModels.Job;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private IJobService _jobService;
        private IEmployeeProfileService _employeeService;
        private IOfferService _offerService;
        private ISectionService _sectionService;
        private ISkillService _skillService;
        private INotificationService _notificationService;
        private ICommitProvider _commitProvider;

        public JobController(IJobService jobService, IEmployeeProfileService employeeService, IOfferService offerService, ISectionService sectionService, ISkillService skillService, INotificationService notificationService, ICommitProvider commitProvider)
        {
            _jobService = jobService;
            _employeeService = employeeService;
            _offerService = offerService;
            _sectionService = sectionService;
            _skillService = skillService;
            _notificationService = notificationService;
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

            return RedirectToAction("Jobs", "Job");
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

            return RedirectToAction("myjobs", "job");
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

            if(User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                bool userViewExists = _jobService.UserViewExists(id, userId);

                if (!userViewExists && job.CustomerId != userId)
                {
                    _jobService.ViewJob(id, userId);
                    _commitProvider.SaveChanges();
                }
            }
               
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

            _jobService.FinishJob(id);

            string senderId = User.Identity.GetUserId();
            string notifyText = $"You successfully finish job - {job.Title} <a href='/profile/createfeedback?reciverId={senderId}&jobId={id}'> please add review about your customer</a>";
            _notificationService.CreateNotification(senderId, job.EmployeeId, notifyText);

            _commitProvider.SaveChanges();
            _notificationService.RefreshNotifications(job.EmployeeId);

             return View(job);
        }

        [HttpPost]
        public ActionResult EmployeeExistsInJob(int jobId)
        {
            var job = _jobService.FindCustomerJob(jobId);
            bool employeeExists = (job != null && job.EmployeeId != null) ? true : false;

            return Json(employeeExists);
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
            int[] employeeSkills = null;

            if(User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                bool employeeExists = _employeeService.EmployeeProfileExists(userId);

                if(employeeExists)
                {
                    var employee = _employeeService.FindEmployee(userId);
                    employeeSkills = employee.Skills.Select(p => p.Id).ToArray();
                }
            }

            var jobs = _jobService.FindRelevantJobs(skillsId, keyWords, priceFrom, employeeSkills);
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