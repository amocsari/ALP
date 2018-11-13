using ALP.Navigation;
using System;

namespace ALP
{
    /// <summary>
    /// Used to define extension methods used in the client project
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Registers a new page key - URI pair to the given nagivationservice
        /// </summary>
        /// <param name="navigationService">The navigationservice, in which the page will be registered</param>
        /// <param name="key">The key of the registered page</param>
        /// <param name="uri">The URI of the registered page</param>
        /// <returns></returns>
        public static AlpNavigationService RegisterPage(this AlpNavigationService navigationService, string key, Uri uri)
        {
            return navigationService.RegisterPage(key, uri);
        }
    }
}
