using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ALP.Navigation
{
    /// <summary>
    /// Handles the navigation of pages inside the mainFrame node of the MainWindow
    /// </summary>
    public class AlpNavigationService : IAlpNavigationService
    {
        /// <summary>
        /// A dictinary that stores the keys of the pages that the user can navigate to, paired with their respective URI-s
        /// </summary>
        private static Dictionary<string, Uri> pageKeys = new Dictionary<string, Uri>();
        /// <summary>
        /// The navigation backstack
        /// </summary>
        private static Stack<string> backStack = new Stack<string>();
        /// <summary>
        /// The key of the current page
        /// </summary>
        public string CurrentPageKey { get { return backStack.Last(); } }

        /// <summary>
        /// The Navigation Parameter
        /// </summary>
        public object Parameter { get; private set; }

        /// <summary>
        /// Returns the mainFrame to the previous page
        /// </summary>
        public void GoBack()
        {
            if (backStack.Count > 1)
            {
                backStack.Pop();
                NavigateTo(CurrentPageKey);
            }
        }

        /// <summary>
        /// Navigates to the page with the matching key
        /// Calls the other NavigateTo with null as parameter
        /// </summary>
        /// <param name="pageKey">The key of the desired page</param>
        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        /// <summary>
        /// Navigates to the page with the matching key
        /// Also sets the navigation parameter 
        /// </summary>
        /// <param name="pageKey">The key of the desired page</param>
        /// <param name="parameter">The sent navigation parameter</param>
        public void NavigateTo(string pageKey, object parameter)
        {
            lock (pageKeys)
            {
                if (pageKeys.ContainsKey(pageKey))
                {
                    var frame = (Frame)GetFrameworkElementByName(Application.Current.MainWindow, "mainFrame");
                    if (frame != null)
                    {
                        frame.Source = pageKeys[pageKey];
                    }

                    Parameter = parameter;
                    backStack.Push(pageKey);
                }
            }
        }

        /// <summary>
        /// Registers a new page key - URI pair by adding it to the Dictionary
        /// </summary>
        /// <param name="key">Key of the registered page</param>
        /// <param name="page">URI of the registered page</param>
        /// <returns></returns>
        public AlpNavigationService RegisterPage(string key, Uri page)
        {
            lock (pageKeys)
            {
                if (pageKeys.ContainsKey(key))
                {
                    pageKeys[key] = page;
                }
                else
                {
                    pageKeys.Add(key, page);
                }
            }

            return this;
        }

        /// <summary>
        /// Finds an element by looking through the descendant nodes of the parent element
        /// </summary>
        /// <param name="parent">The root element, which the searched element is a descendant of</param>
        /// <param name="name">The name of the searched element</param>
        /// <returns></returns>
        private static FrameworkElement GetFrameworkElementByName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < count; i++)
            {
                var frameWorkElement = (FrameworkElement)VisualTreeHelper.GetChild(parent, i);
                if (frameWorkElement != null)
                {
                    if (frameWorkElement.Name == name)
                    {
                        return frameWorkElement;
                    }

                    frameWorkElement = GetFrameworkElementByName(frameWorkElement, name);
                    if (frameWorkElement != null)
                    {
                        return frameWorkElement;
                    }
                }
            }
            return null;
        }
    }
}
