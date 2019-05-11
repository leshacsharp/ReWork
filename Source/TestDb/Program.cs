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

                string recivedId = "sdg";

                var feedBacks = (from f in db.FeedBacks
                                 join j in db.Jobs on f.JobId equals j.Id
                                 join s in db.Users on f.SenderId equals s.Id
                                 join r in db.Users on f.ReceiverId equals r.Id
                                 where f.ReceiverId == recivedId
                                 select new FeedBackInfo()
                                 {
                                     Text = f.Text,
                                     AddedDate = f.AddedDate,
                                     QualityOfWork = f.QualityOfWork,

                                     ReceiverName = r.UserName,
                                     ReceiverId = r.Id,
                                     SenderName = s.UserName,
                                     SenderId = s.Id,

                                     JobId = j.Id,
                                     jobTitle = j.Title
                                 }).ToList();

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
