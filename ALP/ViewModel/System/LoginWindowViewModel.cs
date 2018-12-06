using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using ALP.Service.Interface;
using Model.Model;
using System.Threading.Tasks;

namespace ALP.ViewModel
{
    public class LoginWindowViewModel : ViewModelBase, IDialogViewModel<User, object>
    {
        public User ReturnParameter { get; set; }
        public object Parameter { get; set; }

        private readonly IAccountApiService _accountApiService;
        
        public ICommand CancelCommand { get; private set; }

        public LoginWindowViewModel(IAccountApiService accountApiService)
        {
            _accountApiService = accountApiService;
            
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
        }

        private void OnCancelCommand(Window window)
        {
            window.Close();
        }

        public async Task Login(string username, string password)
        {
            ReturnParameter = await _accountApiService.Login(username, password);
        }

    }
}
