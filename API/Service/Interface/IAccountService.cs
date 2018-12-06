using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Enum;
using Model.Model;

namespace API.Service
{
    public interface IAccountService
    {
        Task<AlpApiResponse<SessionData>> Login(LoginData loginData);
        Task<AlpApiResponse> Logout(string encryptedSessionToken);
        Task<AlpApiResponse> ChangePassword(ChangePasswordRequest changePasswordRequest, string sessionToken);
        bool AuthorizeAsync(string encryptedSessionToken, List<RoleType> roles);
        RoleType? GetRoleTypeFromToken(string encryptedSessionToken);
        Task<AlpApiResponse> RegisterAccount(RegisterAccountRequest registerAccountRequest);
    }
}
