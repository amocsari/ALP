﻿using ALP.Service;
using ALP.Service.Interface;
using Common.Model.Dto;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ALP.ViewModel.Lookup
{
    /// <summary>
    /// Used to Edit an instance of a FloorDto
    /// </summary>
    public class LookupFloorEditorWindowViewModel : LookupEditorWindowViewModel<FloorDto>
    {
        /// <summary>
        /// Selectable buildings
        /// </summary>
        private ObservableCollection<BuildingDto> buildings;
        public ObservableCollection<BuildingDto> Buildings
        {
            get { return buildings; }
            set
            {
                if (value != buildings)
                {
                    Set(ref buildings, value);
                }
            }
        }

        /// <summary>
        /// Currently selected building
        /// </summary>
        public BuildingDto SelectedBuilding
        {
            get
            {
                return Parameter.Building;
            }
            set
            {
                if (Parameter.Building == null || !Parameter.Building.Equals(value))
                {
                    Parameter.Building = value;
                    Parameter.BuildingId = value.Id;
                }
            }
        }

        /// <summary>
        /// Api that communicates with the server
        /// Makes Building related requests
        /// </summary>
        private readonly ILookupApiService<BuildingDto> _buildingApiService;

        private readonly IAlpDialogService _dialogService;
        private readonly IAlpLoggingService<LookupFloorEditorWindowViewModel> _loggingService;

        /// <summary>
        /// Constructor
        /// Handles Dependency Injection and Initialization
        /// </summary>
        /// <param name="locationApiService"></param>
        public LookupFloorEditorWindowViewModel(ILookupApiService<BuildingDto> buildingApiService, IAlpDialogService dialogService, IAlpLoggingService<LookupFloorEditorWindowViewModel> loggingService)
        {
            _buildingApiService = buildingApiService;
            _dialogService = dialogService;
            _loggingService = loggingService;

            Initialization = InitializeAsync();
        }

        /// <summary>
        /// Async data loading during initialization
        /// </summary>
        /// <returns></returns>
        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                var buildingList = await _buildingApiService.GetAll();
                Buildings = new ObservableCollection<BuildingDto>(buildingList);
            }
            catch (Exception e)
            {
                _loggingService.LogFatal("Error during Initialization!", e);
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
