using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALP.Service.Interface;
using Common.Model.Dto;
using Model.Enum;
using Model.Model;

namespace ALP.Service
{
    public class OperationService : IOperationService
    {
        private readonly IAlpApiService _apiService;
        private readonly IAlpLoggingService<OperationService> _loggingService;

        public OperationService(IAlpApiService apiService, IAlpLoggingService<OperationService> loggingService)
        {
            _apiService = apiService;
            _loggingService = loggingService;
        }

        public async Task ChangeDepartment(List<int> itemIds, int departmentId, bool priority)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ChangeDepartment),
                itemIds,
                departmentId,
                priority
            });

            var operations = itemIds.Select(itemId => new OperationDto
            {
                ItemId = itemId,
                OperationType = OperationType.ChangeDepartment,
                PayLoadId = departmentId,
                Priority = priority
            }).ToList();

            await _apiService.PostAsync("Operation/QueueOperations", operations);
        }

        public async Task ChangeOwner(List<int> itemIds, int ownerId, bool priority)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ChangeDepartment),
                itemIds,
                ownerId,
                priority
            });

            var operations = itemIds.Select(itemId => new OperationDto
            {
                ItemId = itemId,
                OperationType = OperationType.ChangeOwner,
                PayLoadId = ownerId,
                Priority = priority
            }).ToList();

            await _apiService.PostAsync("Operation/QueueOperations", operations);
        }

        public async Task ChangeOwnerToDepartmentChief(List<int> itemIds, bool priority)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ChangeDepartment),
                itemIds,
                priority
            });

            var operations = itemIds.Select(itemId => new OperationDto
            {
                ItemId = itemId,
                OperationType = OperationType.ChangeOwnerToDepartmentChief,
                Priority = priority
            }).ToList();

            await _apiService.PostAsync("Operation/QueueOperations", operations);
        }

        public async Task Scrap(List<int> itemIds, bool priority)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ChangeDepartment),
                itemIds,
                priority
            });

            var operations = itemIds.Select(itemId => new OperationDto
            {
                ItemId = itemId,
                OperationType = OperationType.Scrap,
                Priority = priority
            }).ToList();

            await _apiService.PostAsync("Operation/QueueOperations", operations);
        }
    }
}
