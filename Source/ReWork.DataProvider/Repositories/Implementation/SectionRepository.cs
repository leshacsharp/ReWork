using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class SectionRepository : ISectionRepository
    {
        private IDbContext _db;
        public SectionRepository(IDbContext db)
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
            return _db.Sections.SingleOrDefault(p => p.Title.Equals(title));
        }

       
        public void Update(Section item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Section> GetAll()
        {
            return _db.Sections.ToList();
        }
    }
}
