using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
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

        JobInfo FindById(int jobId);
        IEnumerable<JobInfo> FindUserJobs(string customerId, int page, int countJobsOnPage);
        IEnumerable<JobInfo> FindJobs(FindJobsParams findParams);


        int UserJobsCount(string customerId);
        int JobsCount(int? skillIdint, int priceFrom, string keyWords);
    }
}
