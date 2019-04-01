using ReWork.DataProvider.Entities;
using System.Data.Entity;


namespace ReWork.DataProvider.Context
{
    public class ReWorkInitializer : DropCreateDatabaseIfModelChanges<ReWorkContext>
    {
        protected override void Seed(ReWorkContext context)
        {
            base.Seed(context);

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
