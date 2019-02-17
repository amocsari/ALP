using Common.Model.Dto;
using DAL.Context;
using Microsoft.Extensions.Logging;
using Model.Model;
using System;
using System.Threading.Tasks;
using DAL.Extensions;
using System.Collections.Generic;
using Model.Enum;
using Microsoft.EntityFrameworkCore;

namespace API.Service
{
    /// <summary>
    /// handles operations
    /// </summary>
    public class OperationService : IOperationService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<OperationService> _logger;
        private readonly IAccountService _accountService;
        private readonly IItemService _itemService;

        public OperationService(IAlpContext context, ILogger<OperationService> logger, IItemService itemService, IAccountService accountService)
        {
            _context = context;
            _logger = logger;
            _accountService = accountService;
            _itemService = itemService;
        }

        /// <summary>
        /// queues the operation for later handling
        /// if the user is admin, it is immediately handled
        /// </summary>
        /// <param name="operationList"></param>
        /// <param name="sessionToken"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse<List<ItemDto>>> QueueOperations(List<OperationDto> operationList, string sessionToken)
        {
            var response = new AlpApiResponse<List<ItemDto>>();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(QueueOperations),
                    operationList = operationList?.ToString()
                }.ToString());

                var roleType = _accountService.GetRoleTypeFromToken(sessionToken);
                response.Value = new List<ItemDto>();

                foreach (var dto in operationList)
                {
                    var entity = dto.DtoToEntity();
                    entity.DateOfCompletion = DateTime.Now;
                    await _context.Operation.AddAsync(entity);
                    await _context.SaveChangesAsync();

                    if (roleType == RoleType.Admin)
                    {
                        var result = await DoOperation(entity.OperationId);
                        if (!result.Success)
                        {
                            var item = await _context.Item.FirstOrDefaultAsync(i => i.ItemId == dto.ItemId);
                            if (item != null)
                            {
                                response.Value.Add(item.EntityToDto());
                            }
                        }
                    }
                }
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
        /// handles the operation
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> DoOperation(int operationId)
        {
            var response = new AlpApiResponse();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(DoOperation),
                    operationId
                }.ToString());

                var operation = await _context.Operation.FirstOrDefaultAsync(op => op.OperationId == operationId);
                if (operation == null)
                {
                    response.Success = false;
                    response.Message = "A művelet nem található az adatbázisban!";
                    return response;
                }

                var operationType = (OperationType)operation.OperationType;

                switch (operationType)
                {
                    case OperationType.Scrap:
                        response = await _itemService.Scrap(operation.ItemId);
                        break;
                    case OperationType.ChangeOwner:
                        response = await _itemService.ChangeOwner(operation.ItemId, operation.PayLoadId);
                        break;
                    case OperationType.ChangeDepartment:
                        response = await _itemService.ChangeDepartment(operation.ItemId, operation.PayLoadId);
                        break;
                    case OperationType.ChangeOwnerToDepartmentChief:
                        response = await _itemService.ChangeOwnerToDepartmentChief(operation.ItemId);
                        break;
                }

                operation.DateOfCompletion = DateTime.Now;
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
