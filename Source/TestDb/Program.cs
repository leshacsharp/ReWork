using FirstQuad.Common.Helpers;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace TestDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ReWorkContext db = new ReWorkContext())
            {
                db.Database.Log = Console.Write;


              

                // var b = a.SingleOrDefault();
                int jobId = 6;

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

   
}
