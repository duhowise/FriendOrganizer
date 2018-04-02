using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private readonly IFriendDataService _dataService;
        private Friend _friend;

       
        public FriendDetailViewModel(IFriendDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task LoasdAsync(int friendId)
        {
           Friend= await _dataService.GetByIdAsync(friendId);
        }

        public Friend Friend
        {
            get => _friend;
            set
            {
                if (Equals(value, _friend)) return;
                _friend = value;
                OnPropertyChanged();
            }
        }

    }
}