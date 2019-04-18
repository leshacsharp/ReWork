using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
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
