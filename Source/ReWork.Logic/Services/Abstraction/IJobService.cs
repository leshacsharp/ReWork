using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IJobService
    {
        void CreateJob(CreateJobParams jobParams);
        void DeleteJob(int id);
    }
}
