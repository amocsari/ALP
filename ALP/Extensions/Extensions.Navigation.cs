using ALP.Navigation;
using System;

namespace ALP
{
    public static partial class Extensions
    {
        public static AlpNavigationService RegisterPage(this AlpNavigationService navigationService, string key, Uri page)
        {
            return navigationService.RegisterPage(key, page);
        }
    }
}
