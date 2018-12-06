﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Service.Interface;
using DAL.Context;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Enum;
using Model.Model;
using Model.Model.Dto;

namespace API.Service
{
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

        public async Task<bool> AuthorizeAsync(string encryptedSessionToken, List<RoleType> roles)
        {
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(AuthorizeAsync),
                    encryptedSessionToken
                }.ToString());

                if (string.IsNullOrEmpty(encryptedSessionToken))
                {
                    return false;
                }


                var sessionData = _encryptionService.Decrypt<SessionTokenData>(encryptedSessionToken);
                if (sessionData == null)
                {
                    return false;
                }

                var account = await _context.Account.FirstOrDefaultAsync(acc => acc.AccountId == sessionData.AccountId);
                if(account == null)
                {
                    return false;
                }

                return roles.Contains((RoleType)account.RoleId);
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

        public async Task<AlpApiResponse> CreateAccount(NewAccountDto newAccount)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(CreateAccount),
                    newAccount.EmployeeId,
                    newAccount.Username,
                    newAccount.RoleType
                }.ToString());

                if (!newAccount.Validate())
                {
                    response.Success = false;
                    response.Message = "Hiba az új felhasználó adatainak validálása során!";
                    return response;
                }

                var entity = await _context.Employee.FirstOrDefaultAsync(employee => employee.EmployeeId == newAccount.EmployeeId);
                if(entity == null)
                {
                    response.Success = false;
                    response.Message = "Nem található a választott felhasználó!";
                    return response;
                }

                var entityWithUserName = await _context.Account.Include(acc => acc.Employee).FirstOrDefaultAsync(acc => acc.UserName == newAccount.Username);
                if(entityWithUserName != null)
                {
                    response.Success = false;
                    response.Message = $"Ilyen felhasználónevű felhasználó már szerepel a rendszerben: {entityWithUserName.Employee?.EmployeeName}";
                    return response;
                }

                var account = new Account
                {
                    EmployeeId = entity.EmployeeId,
                    UserName = newAccount.Username,
                    Password = newAccount.Password,
                    RoleId = (int)newAccount.RoleType
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

        public Task<AlpApiResponse> CreateAccount(int employeeId)
        {
            throw new NotImplementedException();
        }

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
    }
}
