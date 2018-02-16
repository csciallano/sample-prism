using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using PrismBoilerplate.Models;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrismBoilerplate.ViewModels
{
    public class RootPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Command used to navigate to another page.
        /// </summary>
        public ICommand NavigateCommand { get; }

        /// <summary>
        /// Command used to send message to every view models.
        /// </summary>
        public ICommand SendMessageCommand { get; }

        /// <summary>
        /// Command used to log out.
        /// </summary>
        public ICommand LogoutCommand { get; }

        /// <summary>
        /// Private accessor to the canNavigate value.
        /// </summary>
        private bool canNavigate;

        /// <summary>
        /// Event messaging by prism.
        /// </summary>
        private readonly IEventAggregator eventMessaging;

        /// <summary>
        /// Property used to know if the user can navigate
        /// </summary>
        public bool CanNavigate
        {
            get => this.canNavigate; set => SetProperty(ref this.canNavigate, value);
        }

        /// <summary>
        /// Initializes a new instance of the class <see cref="RootPageViewModel"/>.
        /// </summary>
        public RootPageViewModel(IEventAggregator eventMessaging, INavigationService navigationService)
             : base(navigationService)
        {
            this.eventMessaging = eventMessaging;

            this.NavigateCommand = new DelegateCommand<string>(Navigate).ObservesCanExecute(() => CanNavigate);
            this.SendMessageCommand = new DelegateCommand(SendMessage);
            this.LogoutCommand = new DelegateCommand(() =>
            {
                this.NavigationService.GoBackToRootAsync();
                this.NavigationService.NavigateAsync("/LoginPage");
            });

            // Just to illustrate the enable / disable button
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                this.CanNavigate = true;
            });
        }

        private void Navigate(string pageName)
        {
            // Navigate to the next page
            this.NavigationService.NavigateAsync($"NavigationPage/{pageName}");
        }

        /// <summary>
        /// This method is used to send a message.
        /// </summary>
        private void SendMessage()
        {
            var message = new Message
            {
                Content = "A message decoupled from everything"
            };

            // Publicateur du message
            this.eventMessaging.GetEvent<MessageEvent>().Publish(message);
        }
    }
}
