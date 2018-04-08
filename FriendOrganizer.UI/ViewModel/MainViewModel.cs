using System;
using System.Threading.Tasks;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private readonly Func<IFriendDetailViewModel> _friendDetailViewModelCreator;
        private IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private IFriendDetailViewModel _friendDetailViewModel;
        public INavigationViewModel NavigationViewModel { get;  }

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get => _friendDetailViewModel;
            set
            {
                _friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(INavigationViewModel navigationViewModel,Func< IFriendDetailViewModel> friendDetailViewModelCreator, IEventAggregator eventAggregator,IMessageDialogService messageDialogService)
        {
            NavigationViewModel = navigationViewModel;
            _friendDetailViewModelCreator = friendDetailViewModelCreator;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);

        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            if (FriendDetailViewModel!=null && FriendDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You have made changes. Navigate away?", "Question");

                if (result==MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = _friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
       

        
    }
}