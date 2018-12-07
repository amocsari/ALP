using Microsoft.Win32;
using System.IO;
using System.Linq;
using ALP.Service.Interface;
using OfficeOpenXml;
using System.Collections.Generic;
using System;
using Common.Model;

namespace ALP.Service
{
    /// <summary>
    /// Imports items from an excel file
    /// </summary>
    public class ImportService : IImportService
    {
        /// <summary>
        /// Number of rows that are not data
        /// </summary>
        private static readonly int HeaderRowCount = 2;

        /// <summary>
        /// Worksheet
        /// </summary>
        private ExcelWorksheet workSheet;

        /// <summary>
        /// Imports data from excel
        /// </summary>
        /// <returns></returns>
        public List<ImportedItem> ImportFromXls()
        {
            var importedItems = new List<ImportedItem>();
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
            };

            if (fileDialog.ShowDialog() == true)
            {
                if (File.Exists(fileDialog.FileName))
                {
                    var file = new FileInfo(fileDialog.FileName);

                    var excelPackage = new ExcelPackage(file);
                    workSheet = excelPackage.Workbook.Worksheets.FirstOrDefault();

                    var rowCount = HeaderRowCount + 1;

                    while (true)
                    {

                        try
                        {
                            var importedItem = ReadRow(rowCount);
                            if (ValidateItem(importedItem))
                            {
                                importedItems.Add(importedItem);
                            }
                            else
                            {
                                var nextRow = ReadRow(rowCount + 1);
                                if (!ValidateItem(nextRow))
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            //TODO: exception handling
                        }

                        rowCount++;
                    }
                }
            }
            else
            {
                return null;
            }

            return importedItems;
        }

        /// <summary>
        /// Reads a row and parses the data
        /// </summary>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        private ImportedItem ReadRow(int rowCount)
        {
            var importedItem = new ImportedItem();
            var yellowNumber = workSheet.Cells[rowCount, 1].Value;
            importedItem.YellowNumber = Convert.ToInt32(yellowNumber);
            importedItem.InventoryNumber = (string)workSheet.Cells[rowCount, 2].Value;
            importedItem.OldInventoryNumber = (string)workSheet.Cells[rowCount, 3].Value;
            importedItem.Name = (string)workSheet.Cells[rowCount, 4].Value;
            importedItem.SerialNumber = (string)workSheet.Cells[rowCount, 5].Value;
            importedItem.OwnerName = (string)workSheet.Cells[rowCount, 8].Value;
            return importedItem;
        }

        /// <summary>
        /// Validates the read item
        /// </summary>
        /// <param name="importedItem"></param>
        /// <returns></returns>
        private bool ValidateItem(ImportedItem importedItem)
        {
            if (importedItem == null)
            {
                return false;
            }

            if (!importedItem.YellowNumber.HasValue
                && string.IsNullOrEmpty(importedItem.InventoryNumber)
                && string.IsNullOrEmpty(importedItem.OldInventoryNumber)
                && string.IsNullOrEmpty(importedItem.SerialNumber))
            {
                return false;
            }

            if (string.IsNullOrEmpty(importedItem.Name))
            {
                return false;
            }

            return true;
        }
    }
}
