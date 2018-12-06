using System.Collections.Generic;
using ALP.ViewModel.Inventory;
using Common.Model.Dto;

namespace ALP.Service.Interface
{
    public interface IExportService
    {
        void ExportToExcel(List<ItemDto> itemList, List<ItemPropertyType> itemTypes);
    }
}
