using ReWork.Model.Context;
using ReWork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TestDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ReWorkContext db = new ReWorkContext())
            {
                db.Database.Log = Console.Write;

                EmployeeProfile profile = db.EmployeeProfiles.SingleOrDefault(p => p.User.UserName.Equals("anton"));

               // EmployeeProfile profile = db.EmployeeProfiles.Where(p => p.User.UserName.Equals("anton")).SingleOrDefault();
                Console.WriteLine(profile.Age);
            }
        }
    }
}
