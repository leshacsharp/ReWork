using ReWork.Logic.Services.Params;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IJobService
    {
        void CreateJob(CreateJobParams createJobParams);
        void Edit(EditJobParams editJobParams);
        void DeleteJob(int jobId);

        void DeleteEmployeeFromJob(int jobId);
        void FinishJob(int jobId);
        void ViewJob(int jobId, string userId);
        bool UserViewExists(int jobId, string userId);

        JobInfo FindJob(int jobId);
        MyJobInfo FindCustomerJob(int jobId);

        IEnumerable<JobInfo> FindCustomerJobs(string customerId, DateTime? fromDate);
        IEnumerable<JobInfo> FindEmployeeJobs(string employeeId, DateTime? fromDate);
        IEnumerable<JobInfo> FindJobs(int[] skillsId, string keyWords, int priceFrom);
        IEnumerable<JobInfo> FindRelevantJobs(int[] skillsId, string keyWords, int priceFrom, int[] emloyeeSkillsId = null);
    }
}
