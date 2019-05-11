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
        void DeleteEmployeeFromJob(int jobId);

        JobInfo FindJob(int jobId);
        MyJobInfo FindCustomerJob(int jobId);
        IEnumerable<JobInfo> FindCustomerJobs(string customerId, DateTime? fromDate);
        IEnumerable<JobInfo> FindEmployeeJobs(string employeeId, DateTime? fromDate);
        IEnumerable<JobInfo> FindJobs(int[] skillsId, string keyWords, int priceFrom);
    }
}
