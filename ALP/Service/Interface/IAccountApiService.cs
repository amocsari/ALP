using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Model;

namespace ALP.Service.Interface
{
    public interface IAccountApiService
    {
        Task<SessionData> Login(string username, string password);
    }
}
