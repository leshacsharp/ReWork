using ReWork.DataProvider.Context;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.Repositories.Abstraction;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class SectionRepository : ISectionRepository
    {
        private ReWorkContext _db;
        public SectionRepository(ReWorkContext db)
        {
            _db = db;
        }

        public void Create(Section item)
        {
            _db.Sections.Add(item);
        }

        public void Delete(Section item)
        {
            _db.Sections.Remove(item);
        }

        public Section FindSectionByTitle(string title)
        {
            return _db.Sections.First(p => p.Title.Equals(title));
        }

        public void Update(Section item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
