using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALP.Ninject;
using ALP.ViewModel;

namespace ALP.Navigation
{
    public partial class ViewModelLocator
    {
        public const string SystemSettings = "System_Settings";

        public MainWindowViewModel MainWindowViewModel { get => IocKernel.Get<MainWindowViewModel>(); }
    }
}
