using System.Threading.Tasks;
using DAL.Entity;
using Model.Model;

namespace API.Service
{
    public interface IAccountService
    {
        Task<AlpApiResponse<string>> Login(string username, string password);
    }
}
