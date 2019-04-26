using FirstQuad.Common.Helpers;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TestDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ReWorkContext db = new ReWorkContext())
            {
                db.Database.Log = Console.Write;

                //var filter = PredicateBuilder.True<Job>();


                //filter = filter.AndAlso<Job>(p => p.Skills.Any(f => f.Id.Equals(2)));



                //filter = filter.AndAlso(p => p.Title.Contains("site") || p.Description.Contains("site"));


                //filter = filter.AndAlso(p => p.Price >= 100);


                //var jobs = db.Jobs.Include(p => p.Skills).Where(p => p.Id > 0)
                //                    // .OrderByDescending(p => p.DateAdded)
                //                     //.Skip(0 * 1)
                //                    // .Take(1)

                //                     .ToList();

                //foreach (var it in jobs)
                //{
                //    foreach (var skill in it.Skills)
                //    {
                //        Console.WriteLine(skill.Title);
                //    }
                //}


                //int jobId = 6;

                //var offers = from o in db.Offers
                //             join e in db.EmployeeProfiles on o.EpmployeeId equals e.Id
                //             join u in db.Users on e.Id equals u.Id into ju
                //             from u in ju.DefaultIfEmpty()
                //             where o.JobId == jobId
                //             select new OfferInfo
                //             {
                //                 Id = o.Id,
                //                 Text = o.Text,
                //                 AddedDate = o.AddedDate,
                //                 ImplementationDays = o.ImplementationDays,
                //                 OfferPayment = o.OfferPayment,

                //                 EmployeeId = e.Id,
                //                 UserName = u.UserName ?? null
                //             };


                //foreach (var it in offers)
                //{
                //    Console.WriteLine(it.UserName);
                //}

                // var jobs = db.Jobs.Include("Skills").ToList();


                int jobId = 1;

                //var a = (from j in db.Jobs
                //         join sj in (from sj in db.SkillJobs
                //                     join s in db.Skills on sj.SkillId equals s.Id
                //                     select sj)
                //                     on j.Id equals sj.JobId

                //         where j.Id == jobId
                //         select new JobInfo()
                //         {
                //             Id = j.Id,
                //             Title = j.Title,
                //             Description = j.Description,
                //             Price = j.Price,
                //             PriceDiscussed = j.PriceDiscussed,
                //             DateAdded = (DateTime)j.DateAdded,

                //             Skills = j.Skills.Select(p => p.Skill.Title)
                //         }).ToList();


                //var jobs = db.Jobs.Where(p => true).Select(
                //                    j => new JobInfo()
                //                    {
                //                        Id = j.Id,
                //                        Title = j.Title,
                //                        Description = j.Description,
                //                        Price = j.Price,
                //                        PriceDiscussed = j.PriceDiscussed,
                //                        DateAdded = (DateTime)j.DateAdded,
                //                        CountOffers = j.Offers.Count(),

                //                      //  SkillsId = j.Skills.Select(p => p.Id),
                //                       // Skills = j.Skills.Select(p => p.Title)
                //                    });


                //foreach (var it in jobs)
                //{
                //    Console.WriteLine(it.CountOffers);
                //    foreach (var it2 in it.Skills)
                //    {
                //        Console.WriteLine(it2);
                //    }
                //}
                //var a = from j in db.Jobs
                //        join s in db.Skills
                //        where j.Id == jobId
                //        select new { j,s };

                //foreach (var it in a)
                //{
                //    Console.WriteLine(it.s.Title);
                //}


            }
        }
    }

    class OfferInfo
    {
        public int Id { get; set; }

        public string UserName { get; set; }
      
        public string Text { get; set; }

        public DateTime? AddedDate { get; set; }

        public int? ImplementationDays { get; set; }

        public int OfferPayment { get; set; }

        public string EmployeeId { get; set; }
    }

    class JobInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public bool PriceDiscussed { get; set; }

        public DateTime DateAdded { get; set; }

        public int CountOffers { get; set; }


        public IEnumerable<int> SkillsId { get; set; }

        public IEnumerable<string> Skills { get; set; }
    }
}
