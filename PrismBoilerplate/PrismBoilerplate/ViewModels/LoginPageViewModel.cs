using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;

namespace PrismBoilerplate.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        /// <summary>
        /// The login value bound to the view
        /// </summary>
        private string userName;

        /// <summary>
        /// Public property for the username.
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        /// <summary>
        /// The password value bound to the view
        /// </summary>
        private string password;

        /// <summary>
        /// Public property for the password.
        /// </summary>
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        /// <summary>
        /// Command used to log in the user
        /// </summary>
        public ICommand LoginCommand { get; }

        /// <summary>
        /// Command used to register the user
        /// </summary>
        public ICommand RegisterCommand { get; }

        /// <summary>
        /// Dialog service used to display alerts
        /// </summary>
        private readonly IPageDialogService dialogService;

        /// <summary>
        /// Initializes a new instance of the class <see cref="LoginPageViewModel"/>
        /// </summary>
        /// <param name="navigationService">The app navigation service</param>
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            this.dialogService = dialogService;

            Title = "PrimSampleApp";

            LoginCommand = new DelegateCommand(Login, CanLogin)
                .ObservesProperty(() => UserName)
                .ObservesProperty(() => Password);

            RegisterCommand = new DelegateCommand(Register);
        }

        /// <summary>
        /// This method returns true when the login is available
        /// </summary>
        /// <returns></returns>
        private bool CanLogin() => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);

        /// <summary>
        /// This private method is used to log in the user.
        /// </summary>
        private void Login()
        {
            // Navigate to the next page
            this.NavigationService.NavigateAsync("/RootPage/NavigationPage/MainPage");
        }

        /// <summary>
        /// This method is used to register a user
        /// </summary>
        private void Register()
        {
            // Displays an alert
            this.dialogService.DisplayAlertAsync("Warning", "It's not possible to register !", "Cancel");
        }
    }
}
