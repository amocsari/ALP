﻿using ALP.Service;
using ALP.Service.Interface;
using Common.Model.Dto;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ALP.ViewModel.Lookup
{
    /// <summary>
    /// Used to Edit an instance of a ItemType
    /// </summary>
    public class LookupItemTypeEditorWindowViewModel : LookupEditorWindowViewModel<ItemTypeDto>
    {
        /// <summary>
        /// Selectable itemNatures
        /// </summary>
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

        /// <summary>
        /// Currently selected itemNature
        /// </summary>
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
                    Parameter.ItemNatureId = value.Id;
                }
            }
        }

        /// <summary>
        /// Api that communicates with the server
        /// Makes ItemNature related requests
        /// </summary>
        private readonly ILookupApiService<ItemNatureDto> _itemNatureApiService;

        private readonly IAlpDialogService _dialogService;
        private readonly IAlpLoggingService<LookupItemTypeEditorWindowViewModel> _loggingService;

        /// <summary>
        /// Constructor
        /// Handles Dependency Injection and Initialization
        /// </summary>
        /// <param name="locationApiService"></param>
        public LookupItemTypeEditorWindowViewModel(ILookupApiService<ItemNatureDto> itemNatureApiService, IAlpDialogService dialogService, IAlpLoggingService<LookupItemTypeEditorWindowViewModel> loggingService)
        {
            _itemNatureApiService = itemNatureApiService;
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
                var itemNatureList = await _itemNatureApiService.GetAll();
                ItemNatures = new ObservableCollection<ItemNatureDto>(itemNatureList);
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
