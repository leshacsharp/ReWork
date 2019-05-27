using FirstQuad.Common.Helpers;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.Entities.Common;
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

            }
        }
    }
}
