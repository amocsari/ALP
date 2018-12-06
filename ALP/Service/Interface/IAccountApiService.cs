using System.Threading.Tasks;
using Model.Model;

namespace ALP.Service.Interface
{
    public interface IAccountApiService
    {
        Task<SessionData> Login(string username, string password);
        Task Logout(string encryptedSessionToken);
        Task ChangePassword(ChangePasswordRequest changePasswordRequest);
        Task RegisterAccount(RegisterAccountRequest registerAccountRequest);
    }
}
