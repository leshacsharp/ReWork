namespace ReWork.Logic.Services.Params
{
    public class ChangePasswordParams
    {
        public string UserName { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
