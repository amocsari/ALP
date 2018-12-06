using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Model;
using Model.Model.Dto;

namespace ALP.Service.Interface
{
    public interface IAccountApiService
    {
        Task<SessionData> Login(string username, string password);
        Task Logout(string encryptedSessionToken);
        Task ChangePassword(ChangePasswordRequest changePasswordRequest);
    }
}
