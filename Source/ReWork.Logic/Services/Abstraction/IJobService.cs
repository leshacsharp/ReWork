using ReWork.Model.Entities;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IJobService 
    {
        void CreateJob(Job job);
        Job GetJobById(int id);
    }
}
