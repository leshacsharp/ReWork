using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IRoleService
    {
        IEnumerable<string> GetAll();
    }
}
