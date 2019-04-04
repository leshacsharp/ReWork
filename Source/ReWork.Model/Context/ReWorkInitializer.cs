using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.Model.Entities;
using ReWork.Model.Migrations;
using System;
using System.Data.Entity;


namespace ReWork.Model.Context
{
    public class ReWorkInitializer : DropCreateDatabaseIfModelChanges<ReWorkContext>
    {
        protected override void Seed(ReWorkContext context)
        {
            base.Seed(context);

            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            IdentityRole roleAdmin = new IdentityRole() { Name = "admin"};
            IdentityRole roleUser = new IdentityRole() { Name = "user" };

            roleManager.Create(roleAdmin);
            roleManager.Create(roleUser);

            UserStore<User> userStore = new UserStore<User>(context);
            UserManager<User> userManager = new UserManager<User>(userStore);

            User admin = new User() { UserName="alex", Email = "alex@yandex.ru", RegistrationdDate = DateTime.Now};

            userManager.Create(admin, "123456");
            userManager.AddToRole(admin.Id, "admin");

            Section section = new Section() { Title = "Programming" };
            Skill skill1 = new Skill() { Title = "C#", Section = section};
            Skill skill2 = new Skill() { Title = "Java", Section = section };

            context.Sections.Add(section);
            context.Skills.Add(skill1);
            context.Skills.Add(skill2);

            context.SaveChanges();
        }
    }
}
