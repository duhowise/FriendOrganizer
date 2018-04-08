using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.Wrapper;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private readonly IFriendRepository _repository;
        private readonly IEventAggregator _eventAggregator;
        private FriendWrapper _friend;
        private bool _hasChages;


        public FriendDetailViewModel(IFriendRepository repository,IEventAggregator eventAggregator)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
            SaveCommand =new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
          await  _repository.SaveAsync();
            HasChanges = _repository.HasChanges();
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Publish(
                new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

       

        public async Task LoadAsync(int friendId)
        {
           var friend = await _repository.GetByIdAsync(friendId);
            Friend=new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _repository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            
        }


        public bool HasChanges
        {
            get => _hasChages;
            set
            {
                _hasChages = value; 
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }


        public FriendWrapper Friend
        {
            get => _friend;
            set
            {
                if (Equals(value, _friend)) return;
                _friend = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get;  }

    }
}