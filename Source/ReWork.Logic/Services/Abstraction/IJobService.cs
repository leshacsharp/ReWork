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
        void CreateJob(CreateJobParams createJobParams);
        void Edit(EditJobParams editJobParams);
        void DeleteJob(int jobId);

        Job FindById(int jobId);
        IEnumerable<Job> FindUserJobs(string customerId);
        IEnumerable<Job> FindJobs(int? skillId, int priceFrom, string keyWords);


        int UserJobsCount(string customerId);
        int JobsCount(int? skillIdint, int priceFrom, string keyWords);
    }
}
