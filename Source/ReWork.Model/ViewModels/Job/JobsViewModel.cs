using System.Collections.Generic;

namespace ReWork.Model.ViewModels.Job
{
    public class JobsViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<Entities.Job> Jobs { get; set; }
    }
}
