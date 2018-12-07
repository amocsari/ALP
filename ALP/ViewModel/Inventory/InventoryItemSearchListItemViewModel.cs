using Common.Model.Dto;
using GalaSoft.MvvmLight;

namespace ALP.ViewModel.Inventory
{
    /// <summary>
    /// viewmodel of one item
    /// </summary>
    public class InventoryItemSearchListItemViewModel: ViewModelBase
    {
        /// <summary>
        /// stored item
        /// </summary>
        private ItemDto value;
        public ItemDto Value
        {
            get { return value; }
            set
            {
                if (value != this.value)
                {
                    Set(ref this.value, value);
                }
            }
        }

        /// <summary>
        /// bound to the checkbox
        /// </summary>
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    Set(ref isSelected, value);
                }
            }
        }
    }
}
