using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model.Dto;
using Model.Enum;
using Model.Model;

namespace API.Service
{
    public interface IOperationService
    {
        Task<AlpApiResponse<List<ItemDto>>> QueueOperations(List<OperationDto> operationList, string sessionToken);
        Task<AlpApiResponse> DoOperation(int operationId);
    }
}
