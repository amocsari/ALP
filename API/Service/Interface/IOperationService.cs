using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model.Dto;
using Model.Model;

namespace API.Service
{
    public interface IOperationService
    {
        Task<AlpApiResponse> QueueOperations(List<OperationDto> operationList);
    }
}
