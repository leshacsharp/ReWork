using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Model.ViewModels.Job
{
    public class JobsViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<JobInfo> Jobs { get; set; }
    }
}
