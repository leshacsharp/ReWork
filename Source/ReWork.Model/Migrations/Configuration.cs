namespace ReWork.Model.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security.DataProtection;
    using ReWork.Model.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<ReWork.Model.Context.ReWorkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReWork.Model.Context.ReWorkContext context)
        {
            base.Seed(context);

            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists("moderator") && !roleManager.RoleExists("user"))
            {
                IdentityRole roleModerator = new IdentityRole() { Name = "moderator" };
                IdentityRole roleUser = new IdentityRole() { Name = "user" };

                roleManager.Create(roleModerator);
                roleManager.Create(roleUser);
            }
            
            UserStore<User> userStore = new UserStore<User>(context);
            UserManager<User> userManager = new UserManager<User>(userStore);


            var moderator = userManager.FindByName("alex");
            if (moderator == null)
            {
                moderator = new User() { UserName = "alex", Email = "star4enko.aleksey2015@yandex.ru", FirstName = "Aleksey", LastName = "Programmer", RegistrationdDate = DateTime.UtcNow };

                string pathToDefaultImage = HttpContext.Current.Server.MapPath("~/Content/cube-512.png");
                byte[] defaultImage = File.ReadAllBytes(pathToDefaultImage);
                moderator.Image = defaultImage;

                userManager.Create(moderator, "123456");
                userManager.AddToRole(moderator.Id, "user");
                userManager.AddToRole(moderator.Id, "moderator");

                CustomerProfile customerProfile = new CustomerProfile() { User = moderator };
                context.CustomerProfiles.Add(customerProfile);

                DpapiDataProtectionProvider dataProtectionProvider = new DpapiDataProtectionProvider();
                userManager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create());

                string token = userManager.GenerateEmailConfirmationToken(moderator.Id);
                userManager.ConfirmEmail(moderator.Id, token);
            }

            var user = userManager.FindByName("anton");
            if (user == null)
            {
                user = new User() { UserName = "anton", Email = "star4enko.aleksey2015@yandex.ru", FirstName = "Anton", LastName = "Antonio", RegistrationdDate = DateTime.UtcNow };

                string pathToDefaultImage = HttpContext.Current.Server.MapPath("~/Content/cube-512.png");
                byte[] defaultImage = File.ReadAllBytes(pathToDefaultImage);
                user.Image = defaultImage;

                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, "user");

                var customerProfile = new CustomerProfile() { User = user };
                context.CustomerProfiles.Add(customerProfile);

                var dataProtectionProvider = new DpapiDataProtectionProvider();
                userManager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create());

                string token = userManager.GenerateEmailConfirmationToken(user.Id);
                userManager.ConfirmEmail(user.Id, token);
            }


            Section programming = context.Sections.SingleOrDefault(p => p.Title.Equals("Programming"));
            Section design = context.Sections.SingleOrDefault(p => p.Title.Equals("Design sites"));

            if (programming == null && design == null)
            {
                programming = new Section() { Title = "Programming" };
                design = new Section() { Title = "Design sites" };
                

                context.Sections.Add(programming);
                context.Sections.Add(design);
            }


            Skill skillCs = context.Skills.FirstOrDefault(p => p.Title.Equals("C#"));
            Skill skillJa = context.Skills.FirstOrDefault(p => p.Title.Equals("Java"));
            Skill skillCss = context.Skills.FirstOrDefault(p => p.Title.Equals("Java"));
            Skill skillScss = context.Skills.FirstOrDefault(p => p.Title.Equals("Java"));
          

            if (skillCs == null && skillJa == null && skillCss == null && skillScss == null)
            {
                skillCs = new Skill() { Title = "C#", Section = programming };
                skillJa = new Skill() { Title = "Java", Section = programming };
                skillCss = new Skill() { Title = "CSS", Section = design };
                skillScss = new Skill() { Title = "SCSS", Section = design };
               

                context.Skills.Add(skillCs);
                context.Skills.Add(skillJa);
                context.Skills.Add(skillCss);
                context.Skills.Add(skillScss);
             
            }


            var websiteJob = new Job()
            {
                Id = 1,
                CustomerId = moderator.Id,
                Customer = moderator.CustomerProfile,
                Title = "create website",
                Description = "web site must be beautifull, topic this web site its cars",
                Price = 1000,
                PriceDiscussed = true,
                DateAdded = DateTime.UtcNow
            };

            var designJob = new Job()
            {
                Id = 2,
                CustomerId = moderator.Id,
                Customer = moderator.CustomerProfile,
                Title = "create design for my site",
                Description = "crate beautifully design, i want see my website in gray colors",
                Price = 3000,
                PriceDiscussed = true,
                DateAdded = DateTime.UtcNow
            };

            websiteJob.Skills.Add(skillCs);
            websiteJob.Skills.Add(skillJa);
            designJob.Skills.Add(skillCss);
            designJob.Skills.Add(skillScss);

            context.Jobs.AddOrUpdate(websiteJob);
            context.Jobs.AddOrUpdate(designJob);

            context.SaveChanges();
        }
    }
}
