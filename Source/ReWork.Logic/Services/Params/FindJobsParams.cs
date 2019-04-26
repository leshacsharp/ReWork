namespace ReWork.Logic.Services.Params
{
    public class FindJobsParams
    {
        public int Page { get; set; }

        public int CountJobsOnPage { get; set; }

        public int[] SkillsId { get; set; }

        public int PriceFrom { get; set; }

        public string KeyWords { get; set; }
    }
}
