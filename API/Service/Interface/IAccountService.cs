using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using Model.Enum;
using Model.Model;
using Model.Model.Dto;

namespace API.Service
{
    public interface IAccountService
    {
        Task<AlpApiResponse<SessionData>> Login(LoginData loginData);
        Task<AlpApiResponse> Logout(string encryptedSessionToken);
        Task<AlpApiResponse> CreateAccount(int employeeId);
        Task<AlpApiResponse> ChangePassword(ChangePasswordRequest changePasswordRequest, string sessionToken);
        Task<bool> AuthorizeAsync(string encryptedSessionToken, List<RoleType> roles);
    }
}
