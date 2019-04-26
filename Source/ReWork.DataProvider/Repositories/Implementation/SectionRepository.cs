using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class SectionRepository : BaseRepository, ISectionRepository
    {
        public void Create(Section item)
        {
            Db.Sections.Add(item);
        }

        public void Delete(Section item)
        {
            Db.Sections.Remove(item);
        }

        public Section FindSectionByTitle(string title)
        {
            return Db.Sections.SingleOrDefault(p => p.Title == title);
        }

        public IEnumerable<SectionInfo> GetSectionsInfo()
        {
            return (from se in Db.Sections
                    join sk in Db.Skills on se.Id equals sk.SectionId into joined
                    select new SectionInfo()
                    {
                        Title = se.Title,
                        Skills = joined.Select(p => new SkillSectionInfo()
                        {
                            Id = p.Id,
                            Title = p.Title,
                            CountJobs = p.Jobs.Count,
                            CountEmployees = p.Employes.Count
                        })
                    }).ToList();
        }
       
        public void Update(Section item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Section> GetAll()
        {
            return Db.Sections.ToList();
        }
    }
}
