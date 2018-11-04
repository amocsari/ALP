using System.Collections.Generic;
using Common.Model.Enum;

namespace Model.Model
{
    public class ItemDisplay
    {
        public int ItemId { get; set; }
        public Dictionary<ItemPropertyType, string> DisplayValues { get; set; }
    }
}
