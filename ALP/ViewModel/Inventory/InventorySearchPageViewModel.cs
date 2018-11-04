using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.ViewModel.Inventory
{
    public class InventorySearchPageViewModel : AlpViewModelBase
    {
        public InventorySearchPageViewModel()
        {

            Initialization = InitializeAsync();
        }

        protected override Task InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
