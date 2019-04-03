using ReWork.Logic.Dto;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IJobService 
    {
        void CreateJob(JobDto job);
        JobDto GetJobById(int id);
    }
}
