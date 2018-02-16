using Prism.Events;
using Prism.Navigation;
using PrismBoilerplate.Models;
using System.Collections.ObjectModel;

namespace PrismBoilerplate.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// The list of received messages
        /// </summary>
        private ObservableCollection<string> messages;
        public ObservableCollection<string> Messages
        {
            get { return messages; }
            set { SetProperty(ref messages, value); }
        }

        /// <summary>
        /// Initializes a new instance of the class <see cref="MainPageViewModel"/>.
        /// </summary>
        /// <param name="dialogService">The dialog service</param>
        /// <param name="eventMessaging">The event messaging service</param>
        public MainPageViewModel(IEventAggregator eventMessaging, INavigationService navigationService)
             : base(navigationService)
        {
            eventMessaging.GetEvent<MessageEvent>().Subscribe(HandleIncomingMessages);
            this.Messages = new ObservableCollection<string>();
        }

        /// <summary>
        /// Handle the message received from 
        /// </summary>
        /// <param name="msg"></param>
        private void HandleIncomingMessages(Message msg)
        {
            if (!string.IsNullOrEmpty(msg.Content))
            {
                this.Messages.Add(msg.Content);
            }
        }
    }
}