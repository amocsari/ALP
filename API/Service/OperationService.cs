using Common.Model.Dto;
using DAL.Context;
using Microsoft.Extensions.Logging;
using Model.Model;
using System;
using System.Threading.Tasks;
using DAL.Extensions;
using System.Collections.Generic;

namespace API.Service
{
    public class OperationService : IOperationService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<OperationService> _logger;
        private readonly IItemService _itemService;

        public OperationService(IAlpContext context, ILogger<OperationService> logger, IItemService itemService)
        {
            _context = context;
            _logger = logger;
            _itemService = itemService;
        }

        public async Task<AlpApiResponse> QueueOperations(List<OperationDto> operationList)
        {
            var response = new AlpApiResponse();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(QueueOperations),
                    operationList = operationList?.ToString()
                }.ToString());

                foreach (var dto in operationList)
                {
                    var entity = dto.DtoToEntity();
                    await _context.Operation.AddAsync(entity);
                }

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
