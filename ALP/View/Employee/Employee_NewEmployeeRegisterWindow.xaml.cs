using ALP.ViewModel.Employee;
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

namespace ALP.View.Employee
{
    /// <summary>
    /// Interaction logic for NewEmployeeRegirterWindow.xaml
    /// </summary>
    public partial class NewEmployeeRegisterWindow : Window
    {
        public NewEmployeeRegisterWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(UsernameBox.Text) || string.IsNullOrEmpty(NewPasswordBox.Password) || string.IsNullOrEmpty(NewPasswordBoxRe.Password))
            {
                return;
            }
            var dataContext = (NewEmployeeRegisterWindowViewModel)DataContext;
            await dataContext.RegisterUser(UsernameBox.Text, NewPasswordBox.Password, NewPasswordBoxRe.Password);
            DialogResult = true;
        }
    }
}
