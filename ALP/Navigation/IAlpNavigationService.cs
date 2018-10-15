using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Navigation
{
    public interface IAlpNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}
