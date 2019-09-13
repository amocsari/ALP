using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ALP.ViewModel;

namespace ALP.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button_Click(sender, e);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (LoginWindowViewModel) DataContext;
            if(string.IsNullOrEmpty(UsernameBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                return;
            }
            await dataContext.Login(UsernameBox.Text, PasswordBox.Password);
            DialogResult = true;
            Close();
        }
    }
}
