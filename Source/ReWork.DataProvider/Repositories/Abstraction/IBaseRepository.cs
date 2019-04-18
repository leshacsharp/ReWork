using ReWork.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IBaseRepository
    {
        IDbContext Db { get; set; }
    }
}
