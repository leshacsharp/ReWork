using System.Threading.Tasks;

namespace ReWork.Logic.Services.Abstraction
{
    public interface ISendMessageService<T> where T : new()
    { 
        void AddMessage(T item);
        Task Send(T item);
        Task SendAll();
    }
}
