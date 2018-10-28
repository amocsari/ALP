using ALP.Service;
using GalaSoft.MvvmLight;
using Model.Dto;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ALP.ViewModel.Lookup.Building
{
    public class BuildingListViewModel: ViewModelBase
    {
        private ObservableCollection<LookupListItemViewModel<BuildingDto>> buildings;
        public ObservableCollection<LookupListItemViewModel<BuildingDto>> Buildings
        {
            get{return buildings;}
            set
            {
                if(value != buildings)
                {
                    Set(ref buildings, value);
                }
            }
        }

        private readonly IBuildingApiService _buildingApiService;
        private Task Initialization { get; set; }

        public BuildingListViewModel(IBuildingApiService buildingApiService)
        {
            _buildingApiService = buildingApiService;

            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var reply = await _buildingApiService.GetAllBuildings();

            if(reply != null)
            {
                var result = reply.Select(building => new LookupListItemViewModel<BuildingDto>(building, OnListitemDoubleClickCommand, OnLockCommand));
            }
        }

        private void OnListitemDoubleClickCommand(BuildingDto obj)
        {
            throw new NotImplementedException();
        }

        private void OnLockCommand(BuildingDto obj)
        {
            throw new NotImplementedException();
        }
    }
}
