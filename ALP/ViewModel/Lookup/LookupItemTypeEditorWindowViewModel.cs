using ALP.Service;
using Model.Dto;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ALP.ViewModel.Lookup
{
    public class LookupItemTypeEditorWindowViewModel : LookupEditorWindowViewModel<ItemTypeDto>
    {
        private ObservableCollection<ItemNatureDto> itemNatures;
        public ObservableCollection<ItemNatureDto> ItemNatures
        {
            get { return itemNatures; }
            set
            {
                if (value != ItemNatures)
                {
                    Set(ref itemNatures, value);
                }
            }
        }

        public ItemNatureDto SelectedItemNature
        {
            get
            {
                return Parameter.ItemNature;
            }
            set
            {
                if (Parameter.ItemNature == null || !Parameter.ItemNature.Equals(value))
                {
                    Parameter.ItemNature = value;
                }
            }
        }

        private readonly ILookupApiService<ItemNatureDto> _itemNatureApiService;

        public LookupItemTypeEditorWindowViewModel(ILookupApiService<ItemNatureDto> itemNatureApiService)
        {
            _itemNatureApiService = itemNatureApiService;
            Initialization = InitializeAsync();
        }

        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                var itemNatureList = await _itemNatureApiService.GetAll();
                ItemNatures = new ObservableCollection<ItemNatureDto>(itemNatureList);
            }
            catch (Exception)
            {
                //TODO: logging
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
