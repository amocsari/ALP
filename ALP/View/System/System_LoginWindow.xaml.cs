using System.Windows;
using System.Windows.Controls;
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (LoginWindowViewModel) DataContext;
            await dataContext.Login(UsernameBox.Text, PasswordBox.Password);
            DialogResult = true;
            Close();
        }
    }
}
