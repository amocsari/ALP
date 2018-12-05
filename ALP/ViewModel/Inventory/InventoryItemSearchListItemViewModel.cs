using Common.Model.Dto;
using GalaSoft.MvvmLight;

namespace ALP.ViewModel.Inventory
{
    public class InventoryItemSearchListItemViewModel: ViewModelBase
    {
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
