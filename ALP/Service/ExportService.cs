using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ALP.Extensions;
using ALP.Service.Interface;
using ALP.ViewModel.Inventory;
using Common.Model.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ALP.Service
{
    /// <summary>
    /// Handles excel export
    /// </summary>
    public class ExportService : IExportService
    {
        /// <summary>
        /// Number of rows that are not data
        /// </summary>
        private static readonly int HeaderOffset = 2;

        /// <summary>
        /// Injected service
        /// </summary>
        private readonly IAlpLoggingService<ExportService> _loggingService;

        public ExportService(IAlpLoggingService<ExportService> loggingService)
        {
            _loggingService = loggingService;
        }

        /// <summary>
        /// Export
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="itemTypes"></param>
        public void ExportToExcel(List<ItemDto> itemList, List<ItemPropertyType> itemTypes)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ExportToExcel)
            });

            // Configure save file dialog box
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = $"Leltár export {DateTime.Now.ToString("yyyyMMdd_HHmmss")}", // Default file name
                DefaultExt = ".xlsx", // Default file extension
                Filter = "Excel Sheet (.xlsx)|*.xlsx" // Filter files by extension
            };

            // Show save file dialog box
            var result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result != true)
            {
                return;
            }

            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Worksheets.Add(DateTime.Today.ToShortDateString());
                var workSheet = excelPackage.Workbook.Worksheets.First();

                var range = workSheet.Cells[1, 1, 1, itemTypes.Count];
                range.Merge = true;
                range.Value = $"Leltár export {DateTime.Now.ToString()}";
                range.Style.Font.Italic = true;

                for (int i = 0; i < itemTypes.Count; i++)
                {
                    var cell = workSheet.Cells[2, i + 1];
                    cell.Value = itemTypes[i].GetDescription();
                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                    cell.Style.Font.Bold = true;
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                for (int i = 0; i < itemList.Count; i++)
                {
                    for (int j = 0; j < itemTypes.Count; j++)
                    {
                        var cell = workSheet.Cells[i + HeaderOffset + 1, j + 1];
                        cell.Value = GetItemDataByItemPropertyType(itemList[i], itemTypes[j]);
                        cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                }

                workSheet.Cells.AutoFitColumns();

                // Save document
                var excelFile = new FileInfo(dlg.FileName);
                excelPackage.SaveAs(excelFile);

                //Open in excel
                var isExcelInstalled = Type.GetTypeFromProgID("Excel.Application") != null ? true : false;
                if (isExcelInstalled)
                {
                    System.Diagnostics.Process.Start(excelFile.ToString());
                }
            }
        }

        /// <summary>
        /// "Mapping" the item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        private string GetItemDataByItemPropertyType(ItemDto item, ItemPropertyType propertyType)
        {
            switch (propertyType)
            {
                case ItemPropertyType.InventoryNumber:
                    return item.InventoryNumber;
                case ItemPropertyType.OldInventoryNumber:
                    return item.OldInventoryNumber;
                case ItemPropertyType.SerialNumber:
                    return item.SerialNumber;
                case ItemPropertyType.AccreditationNumber:
                    return item.AccreditationNumber;
                case ItemPropertyType.YellowNumber:
                    return item.YellowNumber?.ToString();
                case ItemPropertyType.ItemName:
                    return item.ItemName;
                case ItemPropertyType.ManufacturerModelType:
                    return item.ManufacTurerModelType;
                case ItemPropertyType.ItemNature:
                    return item.ItemNature?.Name;
                case ItemPropertyType.ItemType:
                    return item.ItemType?.Name;
                case ItemPropertyType.ProductionYear:
                    return item.ProductionYear?.ToShortDateString();
                case ItemPropertyType.Department:
                    return item.Department?.Name;
                case ItemPropertyType.Section:
                    return item.Section?.Name;
                case ItemPropertyType.Employee:
                    return item.Employee?.Name;
                case ItemPropertyType.Building:
                    return item.Building?.Name;
                case ItemPropertyType.Floor:
                    return item.Floor?.Name;
                case ItemPropertyType.Room:
                    return item.Room;
                case ItemPropertyType.ItemState:
                    return item.ItemState?.Name;
                case ItemPropertyType.DateOfCreation:
                    return item.DateOfCreation?.ToShortDateString();
                case ItemPropertyType.BruttoPrice:
                    return item.BruttoPrice?.ToString();
                case ItemPropertyType.Comment:
                    return item.Comment;
                case ItemPropertyType.DateOfScrap:
                    return item.DateOfScrap?.ToShortDateString();
                default:
                    return null;
            }
        }
    }
}
