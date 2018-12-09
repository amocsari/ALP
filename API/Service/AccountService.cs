using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Service.Interface;
using DAL.Context;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Enum;
using Model.Model;

namespace API.Service
{
    /// <summary>
    /// Handles Account based database methods
    /// Handles Authentication
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAlpContext _context;
        private readonly IEncryptionService _encryptionService;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAlpContext context, IEncryptionService encryptionService, ILogger<AccountService> logger)
        {
            _context = context;
            _encryptionService = encryptionService;
            _logger = logger;
        }

        /// <summary>
        /// Checks by an sessionToken, who the user is
        /// then decides if the user has a certain role requires
        /// </summary>
        /// <param name="encryptedSessionToken"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool Authorize(string encryptedSessionToken, List<RoleType> roles)
        {
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(Authorize),
                    encryptedSessionToken
                }.ToString());

                var roleType = GetRoleTypeFromToken(encryptedSessionToken);
                if (!roleType.HasValue)
                {
                    return false;
                }

                return roles.Contains(roleType.Value);
            }catch(Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                return false;
            }
        }

        /// <summary>
        /// Changes the password of an account
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <param name="sessionToken"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> ChangePassword(ChangePasswordRequest changePasswordRequest, string sessionToken)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ChangePassword),
                    sessionToken
                }.ToString());

                if (string.IsNullOrEmpty(sessionToken))
                {
                    response.Success = false;
                    response.Message = "Validásiós hiba a jelszóváltoztatás kérésben!";
                    return response;
                }

                var session = _encryptionService.Decrypt<SessionTokenData>(sessionToken);
                if(session?.AccountId == null)
                {
                    response.Success = false;
                    response.Message = "Validásiós hiba a jelszóváltoztatás kérésben!";
                    return response;
                }

                if (!changePasswordRequest.Validate())
                {
                    response.Success = false;
                    response.Message = "Validációs hiba a jelszóváltoztatás kérésben!";
                    return response;
                }

                var account = await _context.Account.FirstOrDefaultAsync(acc => acc.AccountId == session.AccountId);
                if(account == null)
                {
                    response.Success = false;
                    response.Message = "Nincs ilyen felhasználó az adatbázisban!";
                    return response;
                }

                if(changePasswordRequest.OldPassword != account.Password)
                {
                    response.Success = false;
                    response.Message = "Hibás régi jelszó!";
                    return response;
                }

                if(changePasswordRequest.OldPassword == changePasswordRequest.NewPassword)
                {
                    response.Success = false;
                    response.Message = "Az új jelszó nem egyezhet meg a régi jelszóval!";
                    return response;
                }

                account.Password = changePasswordRequest.NewPassword;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// /decrypts the token into a role
        /// </summary>
        /// <param name="encryptedSessionToken"></param>
        /// <returns></returns>
        public RoleType? GetRoleTypeFromToken(string encryptedSessionToken)
        {

            if (string.IsNullOrEmpty(encryptedSessionToken))
            {
                return null;
            }


            var sessionData = _encryptionService.Decrypt<SessionTokenData>(encryptedSessionToken);
            if (sessionData == null)
            {
                return null;
            }

            var account = _context.Account.FirstOrDefault(acc => acc.AccountId == sessionData.AccountId);
            if (account == null)
            {
                return null;
            }

            return (RoleType)account.RoleId;
        }

        /// <summary>
        /// logs the user in, creating a sessiontoken it can use to access the server
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse<SessionData>> Login(LoginData loginData)
        {
            var response = new AlpApiResponse<SessionData>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(Login),
                    loginData.Username
                }.ToString());

                if (!loginData.Validate())
                {
                    response.Success = false;
                    response.Message = "Hibás felhasználónév vagy jelszó!";
                    return response;
                }

                var user = await _context.Account.FirstOrDefaultAsync(account => account.UserName == loginData.Username && account.Password == loginData.Password);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Hibás felhasználónév vagy jelszó!";
                    return response;
                }

                var session = new SessionTokenData
                {
                    SessionStart = DateTime.UtcNow,
                    UserName = user.UserName,
                    RoleId = user.RoleId,
                    AccountId = user.AccountId
                };

                var encryptedSessionToken = _encryptionService.Encrypt(session);

                if (string.IsNullOrEmpty(encryptedSessionToken))
                {
                    response.Success = false;
                    response.Message = "Hiba történt a bejelentkezés során!";
                    return response;
                }

                var sessionData = new SessionData
                {
                    RoleId = user.RoleId,
                    Token = encryptedSessionToken
                };

                user.Token = encryptedSessionToken;
                await _context.SaveChangesAsync();

                response.Value = sessionData;
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Logs the account out, destroying its sessiontoken
        /// </summary>
        /// <param name="encryptedSessionToken"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> Logout(string encryptedSessionToken)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(Logout)
                }.ToString());

                if (string.IsNullOrEmpty(encryptedSessionToken))
                {
                    response.Success = false;
                    response.Message = "Érvénytelen session Token!";
                    return response;
                }

                var sessionData = _encryptionService.Decrypt<SessionTokenData>(encryptedSessionToken);
                if (sessionData == null)
                {
                    response.Success = false;
                    response.Message = "Sikertelen kijelentkezés!";
                    return response;
                }

                var user = await _context.Account.FirstOrDefaultAsync(account => account.AccountId == sessionData.AccountId);
                if(user == null)
                {
                    response.Success = false;
                    response.Message = "Sikertelen kijelentkezés!";
                    return response;
                }

                user.Token = string.Empty;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Registeres a new account for an employee
        /// </summary>
        /// <param name="registerAccountRequest"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> RegisterAccount(RegisterAccountRequest registerAccountRequest)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(RegisterAccount),
                    registerAccountRequest.EmployeeId,
                    registerAccountRequest.Username
                }.ToString());

                if (!registerAccountRequest.Validate())
                {
                    response.Success = false;
                    response.Message = "Hiba az új felhasználó adatainak validálása során!";
                    return response;
                }

                var entity = await _context.Employee.FirstOrDefaultAsync(employee => employee.EmployeeId == registerAccountRequest.EmployeeId);
                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "Nem található a választott felhasználó!";
                    return response;
                }

                if (!entity.DepartmentId.HasValue)
                {
                    response.Success = false;
                    response.Message = "Csak akkor adható hozzáférés a munkavállónak a leltár rendszerhez, ha hozzá van rendelve egy osztályhoz!";
                    return response;
                }

                var accu = await _context.Account.FirstOrDefaultAsync(ac => ac.EmployeeId == registerAccountRequest.EmployeeId);
                if(accu != null)
                {
                    response.Success = false;
                    response.Message = "Ehhez a munkavállalóhoz már tartozik felhasználó!";
                }

                var entityWithUserName = await _context.Account.Include(acc => acc.Employee).FirstOrDefaultAsync(acc => acc.UserName == registerAccountRequest.Username);
                if (entityWithUserName != null)
                {
                    response.Success = false;
                    response.Message = $"Ilyen felhasználónevű felhasználó már szerepel a rendszerben: {entityWithUserName.Employee?.EmployeeName}";
                    return response;
                }

                var account = new Account
                {
                    EmployeeId = entity.EmployeeId,
                    UserName = registerAccountRequest.Username,
                    Password = registerAccountRequest.Password,
                    RoleId = (int)RoleType.DepartmentInventoryOperator
                };

                await _context.Account.AddAsync(account);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
