﻿using System;
using System.Collections.Generic;
using System.Text;
using Common.Model.Enum;

namespace Common.Model
{
    public class InventoryItemFilterInfo
    {
        public List<string> Id { get; set; }
        public string ManufacturerAndType { get; set; }
        public int? BruttoPriceMin { get; set; }
        public int? BruttoPriceMax { get; set; }
        public DateTime? DateOfCreationMin { get; set; }
        public DateTime? DateOfCreationMax { get; set; }
        //TODO: int?
        public DateTime? YearOfManufactureMin { get; set; }
        public DateTime? YearOfManufactureMax { get; set; }
        public DateTime? DateOfScrapMin { get; set; }
        public DateTime? DateOfScrapMax { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{{ Id = {Id}");
            sb.Append($", ManufacturerAndType = {ManufacturerAndType}");
            sb.Append($", BruttoPriceMin = {BruttoPriceMin}");
            sb.Append($", BruttoPriceMax = {BruttoPriceMax}");
            sb.Append($", DateOfCreationMin = {DateOfCreationMin?.ToShortDateString()}");
            sb.Append($", DateOfCreationMax = {DateOfCreationMax?.ToShortDateString()}");
            sb.Append($", YearOfManufactureMin = {YearOfManufactureMin?.ToShortDateString()}");
            sb.Append($", YearOfManufactureMax = {YearOfManufactureMax?.ToShortDateString()}");
            sb.Append($", DateOfScrapMin = {DateOfScrapMin?.ToShortDateString()}");
            sb.Append($", DateOfScrapMax = {DateOfScrapMax?.ToShortDateString()}");
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
