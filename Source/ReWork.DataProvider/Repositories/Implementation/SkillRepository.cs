﻿using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class SkillRepository : BaseRepository, ISkillRepository
    {      
        public void Create(Skill item)
        {
            Db.Skills.Add(item);
        }

        public void Delete(Skill item)
        {
            Db.Skills.Remove(item);
        }

        public Skill FindSkillByTitle(string title)
        {
            return Db.Skills.FirstOrDefault(p => p.Title == title);
        }

        public Skill FindById(int id)
        {
            return Db.Skills.Find(id);
        }

        public void Update(Skill item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Skill> GetAll()
        {
            return Db.Skills.ToList();
        }
    }
}
