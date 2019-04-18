using ReWork.Model.Entities;
using System.Collections.Generic;

namespace ReWork.Model.ViewModels.Account
{
    public class UsersViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<User> Users { get; set;}
    }
}
