using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.UI.Event;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
  public class NavigationViewModel :ViewModelBase, INavigationViewModel
    {
    private IFriendLookupDataService _friendLookupService;
        private readonly IEventAggregator _eventAggregator;
        private NavigationItemViewModel _selectedFriend;

        public NavigationViewModel(IFriendLookupDataService friendLookupService,IEventAggregator eventAggregator)
    {
      _friendLookupService = friendLookupService;
        _eventAggregator = eventAggregator;
        _eventAggregator.GetEvent<AfterFriendSavedEvent>().Subscribe(AfterFriendSaved);
        Friends = new ObservableCollection<NavigationItemViewModel>();
    }

        private void AfterFriendSaved(AfterFriendSavedEventArgs obj)
        {
            var lookupItem = Friends.Single(f => f.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;

        }

        public async Task LoadAsync()
    {
      var lookup = await _friendLookupService.GetFriendLookupAsync();
      Friends.Clear();
      foreach (var item in lookup)
      {
        Friends.Add(new NavigationItemViewModel(item.Id,item.DisplayMember));
      }
    }


        public NavigationItemViewModel SelectedFriend
        {
            get => _selectedFriend;
            set
            {
                if (Equals(value, _selectedFriend)) return;
                _selectedFriend = value;
                OnPropertyChanged();
                if (_selectedFriend!=null)
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                        .Publish(_selectedFriend.Id);
                }
            }
        }

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

    
  }
}
