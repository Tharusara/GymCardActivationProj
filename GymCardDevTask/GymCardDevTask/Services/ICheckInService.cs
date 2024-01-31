using System.Threading.Tasks;
using VirtuagymDevTask.Models;

namespace VirtuagymDevTask.Services
{
    public interface ICheckInService
    {
        Task<User> CheckUserValidity(int id);
    }
}
