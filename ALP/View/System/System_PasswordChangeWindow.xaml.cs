using ALP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ALP.View
{
    /// <summary>
    /// Interaction logic for PasswordChange.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        public PasswordChangeWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(OldPasswordBox.Password) || string.IsNullOrEmpty(NewPasswordBox.Password) || string.IsNullOrEmpty(NewPasswordBoxRe.Password))
            {
                return;
            }
            var dataContext = (PasswordChangeWindowViewModel)DataContext;
            await dataContext.ChangePassword(OldPasswordBox.Password, NewPasswordBox.Password, NewPasswordBoxRe.Password);
            DialogResult = true;
        }
    }
}
