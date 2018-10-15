using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ALP.Navigation
{
    public class AlpNavigationService : IAlpNavigationService, INotifyPropertyChanged
    {
        private static Dictionary<string, Uri> pageKeys = new Dictionary<string, Uri>();
        private static List<string> history = new List<string>();
        public string CurrentPageKey { get; private set; }

        public object Parameter { get; private set; }

        public void GoBack()
        {
            if (history.Count > 1)
            {
                history.RemoveAt(history.Count - 1);
                NavigateTo(history.Last());
            }
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

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
                    history.Add(pageKey);
                    CurrentPageKey = pageKey;
                }
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
