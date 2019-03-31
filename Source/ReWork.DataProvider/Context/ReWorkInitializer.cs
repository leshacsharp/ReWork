using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReWork.DataProvider.Context
{
    public class ReWorkInitializer : DropCreateDatabaseIfModelChanges<ReWorkContext>
    {
        protected override void Seed(ReWorkContext context)
        {
            base.Seed(context);
        }
    }
}
